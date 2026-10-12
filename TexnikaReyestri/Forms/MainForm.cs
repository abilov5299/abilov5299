using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Pages;
using TexnikaReyestri.Services;

namespace TexnikaReyestri.Forms
{
    /// <summary>
    /// Əsas pəncərə: sol ikon menyu, başlıq zolağı və səhifələr (pnlContent).
    /// Səhifələr bir dəfə yaradılır və yaddaşda qalır; keçid zamanı sürüşmə animasiyası olur.
    /// </summary>
    public partial class MainForm : Form
    {
        public static MainForm Current { get; private set; }
        public bool CixisEdildi { get; private set; }

        private readonly Dictionary<string, PageBase> _pages = new();
        private readonly System.Windows.Forms.Timer _badge = new() { Interval = 60_000 };
        private readonly System.Windows.Forms.Timer _idle = new() { Interval = 30_000 };
        private PageBase _aktiv; private string _aktivKey;
        private DateTime _sonFealiyyet = DateTime.Now;

        public MainForm()
        {
            InitializeComponent();
            Current = this;
            _badge.Tick += async (_, _) => await BadgeYenileAsync();
            _idle.Tick += Idle_Tick;
            Theme.Changed += (_, _) => { Ui.Hazirla(this); Invalidate(true); };
            Lang.Changed += (_, _) => { Lang.Apply(this); foreach (var p in _pages.Values) Lang.Apply(p); YerlesdirBaslig(); };
        }

        // ---------------- Açılış ----------------
        private async void MainForm_Load(object sender, EventArgs e)
        {
            cmbLang.SelectedIndex = Ui.DilIndex; btnTheme.Glyph = Ui.TemaGlyph;
            lblQurum.Text = Session.QurumAd;
            lblUser.Text = $"{Session.AdSoyad} · {Lang.T(Session.RolMetni)}";
            // Rola görə menyu: üst qurumlar texnika daxil etmir, İcmal isə yalnız üst qurumlarda var
            navAdd.Visible = Session.YazaBiler;
            navSummary.Visible = Session.Tip != QurumTip.Zavod;
            int y = 70;
            foreach (var n in new[] { navHome, navSummary, navRegistry, navAdd, navNoti, navSync, navLogs, navBackup }.Where(n => n.Visible)) { n.Top = y; y += 52; }
            Ui.Hazirla(this); YerlesdirBaslig();
            // Sınaq üçün: baza boşdursa bir dəfə 10 nümunə texnika yazılır
            if (await SistemRepo.ParamStrAsync("Numune10") == null && await TexnikaRepo.SayAsync(new ReyestrFiltr()) == 0)
            { await DemoData.On10Async(Session.ZavodFiltri); await SistemRepo.ParamYazAsync("Numune10", "1"); }
            await AcAsync("home");
            _badge.Start(); _idle.Start();
            Application.AddMessageFilter(new FealiyyetFiltri(() => _sonFealiyyet = DateTime.Now));
            // Arxa planda: gündəlik ehtiyat nüsxə və bildiriş qaydaları
            _ = Task.Run(async () =>
            {
                try { await BackupService.GundelikAsync(); } catch (Exception ex) { await SistemRepo.LogAsync("Xəta", "Ehtiyat nüsxə: " + ex.Message); }
                try { await NotificationService.YoxlaAsync(); } catch (Exception ex) { await SistemRepo.LogAsync("Xəta", "Bildirişlər: " + ex.Message); }
                BeginInvoke(new Action(async () =>
                {
                    int n = await BadgeYenileAsync();
                    if (n > 0) Toast.Show(this, $"{n} oxunmamış bildiriş", ToastKind.Warn);
                }));
            });
        }

        private void YerlesdirBaslig() { lblUser.Left = lblQurum.Right + 10; }

        // ---------------- Naviqasiya ----------------
        private async void nav_Click(object sender, EventArgs e)
        {
            if (sender is not NavButton b) return;
            // "Texnika daxil et": əvvəlki redaktə açıq qalmışdısa, boş yeni forma aç
            if (b.PageKey == "add") await AcAsync("add", p => ((EditPage)p).YeniRejim ? Task.CompletedTask : ((EditPage)p).YukleAsync(null));
            else await AcAsync(b.PageKey);
        }

        public async Task AcAsync(string key, Func<PageBase, Task> hazirliq = null)
        {
            if (!_pages.TryGetValue(key, out var page))
            {
                page = key switch
                {
                    "home" => new HomePage(), "sum" => new SummaryPage(), "list" => new RegistryPage(), "add" => new EditPage(),
                    "det" => new DetailPage(), "noti" => new NotificationsPage(), "sync" => new SyncPage(), "log" => new LogsPage(), "bak" => new BackupPage(),
                    _ => throw new ArgumentException(key)
                };
                page.Dock = DockStyle.Fill; page.Visible = false; pnlContent.Controls.Add(page); Ui.Hazirla(page); _pages[key] = page;
            }
            string navKey = key == "det" ? "list" : key;
            foreach (Control c in pnlNav.Controls) if (c is NavButton nb && nb.PageKey.Length > 0) nb.Active = nb.PageKey == navKey;
            if (hazirliq != null) await hazirliq(page);
            if (_aktiv != page)
            {
                var kohne = _aktiv; _aktiv = page; _aktivKey = key;
                await Animator.KecidAsync(pnlContent, kohne, page);
            }
            await page.OnShownAsync();
        }

        public Task ReyestrAcAsync(ReyestrFiltr f) => AcAsync("list", p => ((RegistryPage)p).FiltrTetbiqAsync(f));
        public Task DetalAcAsync(long id, IReadOnlyList<long> siyahi = null) => AcAsync("det", p => ((DetailPage)p).YukleAsync(id, siyahi));
        public Task RedakteAcAsync(long? id) => AcAsync("add", p => ((EditPage)p).YukleAsync(id));

        // ---------------- Bildiriş nişanı ----------------
        public async Task<int> BadgeYenileAsync()
        {
            int n = await SistemRepo.OxunmamisSayAsync();
            navNoti.Badge = n; lblBadge.Text = n > 99 ? "99+" : n.ToString(); lblBadge.Visible = n > 0;
            lblBadge.Left = btnBell.Right - lblBadge.Width + 4;
            return n;
        }

        private void btnBell_Click(object sender, EventArgs e)
        {
            var p = new NotificationPopup();
            var pt = btnBell.PointToScreen(new Point(btnBell.Width - p.Width, btnBell.Height + 4));
            p.Location = pt; p.Show(this);
        }

        // ---------------- Başlıq zolağı ----------------
        private void pnlTop_MouseDown(object sender, MouseEventArgs e) { if (e.Button == MouseButtons.Left && e.Clicks == 1) Win32.DragWindow(this); }
        private void pnlTop_Paint(object sender, PaintEventArgs e) { using var p = new Pen(Theme.Line); e.Graphics.DrawLine(p, 0, pnlTop.Height - 1, pnlTop.Width, pnlTop.Height - 1); }
        private void btnMax_Click(object sender, EventArgs e)
        {
            MaximizedBounds = Screen.FromControl(this).WorkingArea;
            using (Ui.Dondur(this)) WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        }

        private async void btnTheme_Click(object sender, EventArgs e) { Theme.Toggle(); btnTheme.Glyph = Ui.TemaGlyph; await Ui.TemaSaxla(); }

        private async void cmbLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLang.SelectedIndex < 0 || cmbLang.SelectedIndex == Ui.DilIndex) return;
            Lang.Set((Language)cmbLang.SelectedIndex); await Ui.DilSaxla();
        }

        // ---------------- Çıxış və bağlama ----------------
        private async void navOut_Click(object sender, EventArgs e)
        {
            if (!Ui.Tesdiq(this, "Hesabdan çıxış", "Hesabdan çıxmaq istəyirsiniz?", "Çıxış")) return;
            await SistemRepo.LogAsync("Çıxış", $"{Session.AdSoyad} çıxış etdi");
            CixisEdildi = true; Close();
        }

        private bool _baglamaTesdiq;
        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CixisEdildi || _baglamaTesdiq) return;
            e.Cancel = true;
            string msg = Lang.T("Proqram bağlanacaq.");
            if (Session.Tip == QurumTip.Zavod)
            {
                var (t, _) = await SyncService.SonIxracAsync();
                if (t == 0 || (DateTime.Now - DateTimeOffset.FromUnixTimeMilliseconds(t).LocalDateTime).TotalDays >= 3)
                    msg += "\n\n" + Lang.T("Son ixrac 3 gündən çox əvvəl olub. Bağlamazdan əvvəl USB-yə ixrac etmək tövsiyə olunur.");
            }
            if (!Ui.Tesdiq(this, "Proqramı bağlamaq", msg, "Bağla", true)) return;
            await SistemRepo.LogAsync("Çıxış", $"{Session.AdSoyad} proqramı bağladı");
            _baglamaTesdiq = true; Close();
        }

        // ---------------- Klaviatura qısayolları ----------------
        private async void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N && Session.YazaBiler) { e.Handled = true; await RedakteAcAsync(null); }
            else if (e.Control && e.KeyCode == Keys.F) { e.Handled = true; await AcAsync("list", p => { ((RegistryPage)p).AxtarisaFokus(); return Task.CompletedTask; }); }
            else if (e.KeyCode == Keys.F5 && _aktiv != null) { e.Handled = true; await _aktiv.OnShownAsync(); }
        }

        // ---------------- Fəaliyyətsizlikdə avtomatik çıxış ----------------
        private async void Idle_Tick(object sender, EventArgs e)
        {
            int dq = await SistemRepo.ParamAsync("Fealiyyetsiz", 15);
            if (dq > 0 && (DateTime.Now - _sonFealiyyet).TotalMinutes >= dq)
            {
                _idle.Stop(); await SistemRepo.LogAsync("Çıxış", $"{Session.AdSoyad}: {dq} dəqiqə fəaliyyətsizlik, avtomatik çıxış");
                CixisEdildi = true; Close();
            }
        }

        private sealed class FealiyyetFiltri : IMessageFilter
        {
            private readonly Action _a; public FealiyyetFiltri(Action a) => _a = a;
            public bool PreFilterMessage(ref Message m) { if (m.Msg is 0x0100 or 0x0201 or 0x0204 or 0x020A) _a(); return false; }
        }

        // ---------------- Çərçivəsiz pəncərənin ölçüsünü kənardan dəyişmək ----------------
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == Win32.WM_NCHITTEST && WindowState == FormWindowState.Normal)
            {
                var p = PointToClient(new Point((short)((long)m.LParam & 0xFFFF), (short)(((long)m.LParam >> 16) & 0xFFFF)));
                const int g = 6; bool l = p.X < g, r = p.X > ClientSize.Width - g, t = p.Y < g, b = p.Y > ClientSize.Height - g;
                if (t && l) m.Result = Win32.HTTOPLEFT; else if (t && r) m.Result = Win32.HTTOPRIGHT; else if (b && l) m.Result = Win32.HTBOTTOMLEFT;
                else if (b && r) m.Result = Win32.HTBOTTOMRIGHT; else if (l) m.Result = Win32.HTLEFT; else if (r) m.Result = Win32.HTRIGHT;
                else if (t) m.Result = Win32.HTTOP; else if (b) m.Result = Win32.HTBOTTOM;
            }
        }

        protected override void OnPaint(PaintEventArgs e) { base.OnPaint(e); if (WindowState == FormWindowState.Normal) Ui.Kenar(this, e); }
        // Böyüdüləndə tapşırıq panelini örtməsin (pəncərə hansı ekrandadırsa)
        protected override void OnMove(EventArgs e) { base.OnMove(e); if (WindowState == FormWindowState.Normal) MaximizedBounds = Screen.FromHandle(Handle).WorkingArea; }
        protected override void OnFormClosed(FormClosedEventArgs e) { _badge.Stop(); _idle.Stop(); Current = null; base.OnFormClosed(e); }

        protected override CreateParams CreateParams { get { var cp = base.CreateParams; cp.ClassStyle |= Win32.CS_DROPSHADOW; return cp; } }
        protected override void OnHandleCreated(EventArgs e) { base.OnHandleCreated(e); Win32.Yumru(this); }
    }
}
