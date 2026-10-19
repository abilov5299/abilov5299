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
            MenyuQur();
            // Proqram həmişə tam ekran açılır (tapşırıq panelini örtmədən)
            StartPosition = FormStartPosition.Manual; MaximizedBounds = Screen.PrimaryScreen.WorkingArea; WindowState = FormWindowState.Maximized;
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
            navAdd.Visible = navSorgu.Visible = Session.YazaBiler;
            int y = 64;
            foreach (var n in new[] { navHome, navRegistry, navAdd, navTech, navSorgu, navRep, navNoti, navSync, navLogs }.Where(n => n.Visible)) { n.Top = y; n.Height = 48; y += 48; }
            MenyuAc(false);   // menyu hər dəfə bağlı açılır
            Ui.Hazirla(this); YerlesdirBaslig();
            // Sınaq üçün: baza boşdursa bir dəfə 10 nümunə texnika yazılır
            if (await SistemRepo.ParamStrAsync("Numune10") == null && await TexnikaRepo.SayAsync(new ReyestrFiltr()) == 0)
            { await DemoData.On10Async(Session.ZavodFiltri); await SistemRepo.ParamYazAsync("Numune10", "1"); }
            await AcAsync("home");
            _ = OnHazirlaAsync();
            _badge.Start(); _idle.Start();
            Application.AddMessageFilter(new FealiyyetFiltri(() => _sonFealiyyet = DateTime.Now));
            // Arxa planda: gündəlik ehtiyat nüsxə və bildiriş qaydaları
            _ = Task.Run(async () =>
            {
                await Task.Delay(15000);   // açılışda disk və CPU UI ilə yarışmasın
                try { await BackupService.GundelikAsync(); } catch (Exception ex) { await SistemRepo.LogAsync("Xəta", "Ehtiyat nüsxə: " + ex.Message); }
                try { await NotificationService.YoxlaAsync(); } catch (Exception ex) { await SistemRepo.LogAsync("Xəta", "Bildirişlər: " + ex.Message); }
                if (IsDisposed || !IsHandleCreated) return;
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
            if (pnlNav.Width > 64) MenyuAc(false);   // açıq menyu seçimdən sonra bağlanır
            // "Texnika daxil et": əvvəlki redaktə açıq qalmışdısa, boş yeni forma aç
            if (b.PageKey == "add") await AcAsync("add", p => ((EditPage)p).YeniRejim ? Task.CompletedTask : ((EditPage)p).YukleAsync(null));
            else if (b.PageKey == "sadd") await AcAsync("sadd", p => ((SorguEditPage)p).YeniRejim ? Task.CompletedTask : ((SorguEditPage)p).YukleAsync(0));
            else await AcAsync(b.PageKey);
        }

        // Loqoya klik: menyu açılır (ikon + ad) və ya bağlanır (yalnız ikon)
        private void logo_Click(object sender, EventArgs e) => MenyuAc(pnlNav.Width < 100);
        // Menyu məzmunun ÜSTÜNDƏ açılır (dock deyil): genişlənəndə səhifələr yenidən düzülmür — animasiya hamar olur
        private readonly System.Windows.Forms.Timer _menyuT = new() { Interval = 10 };
        private int _menyuHedef = 64, _menyuBasla = 64;
        private readonly System.Diagnostics.Stopwatch _menyuSaat = new();
        private const double MenyuMuddet = 180;   // ms; addım gecikəndə animasiya uzanmır, növbəti addımda yetişir
        private void MenyuEni(int w)
        {
            pnlNav.SuspendLayout();
            pnlNav.Width = w; foreach (Control c in pnlNav.Controls) if (c is NavButton nb) nb.Width = w;
            lblApp.Visible = w > 150;
            pnlNav.ResumeLayout(false);   // bir addımda bir dəfə düzülür
        }
        private void MenyuQur()
        {
            pnlNav.Dock = DockStyle.None; Padding = new Padding(1 + 64, 1, 1, 1);
            pnlNav.SetBounds(1, 1, 64, ClientSize.Height - 2); pnlNav.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left; pnlNav.BringToFront();
            _menyuT.Tick += (_, _) =>
            {
                double t = Math.Min(1, _menyuSaat.Elapsed.TotalMilliseconds / MenyuMuddet);
                double e = 1 - Math.Pow(1 - t, 3);   // ease-out
                int w = (int)Math.Round(_menyuBasla + (_menyuHedef - _menyuBasla) * e);
                if (t >= 1) { w = _menyuHedef; _menyuT.Stop(); }
                if (w != pnlNav.Width) { MenyuEni(w); pnlNav.Update(); }   // hər addım dərhal çəkilsin (iz qalmasın)
                if (t >= 1) { PerdeGizle(); if (_menyuHedef > 64) MezmunSol(_menyuHedef); }   // açıldı: səhifə bir dəfə sıxılır
            };
        }
        // Səhifə menyunun altında qalmır: sol boşluq menyunun eninə bərabərdir (animasiya boyu yox, yalnız sonda dəyişir)
        private void MezmunSol(int w)
        {
            if (Padding.Left == 1 + w) return;
            Padding = new Padding(1 + w, 1, 1, 1);
            PerformLayout(); pnlContent.Update();
        }
        private void MenyuAc(bool ac)
        {
            _menyuHedef = ac ? 236 : 64;
            if (!IsHandleCreated) { _menyuT.Stop(); MenyuEni(_menyuHedef); Padding = new Padding(1 + _menyuHedef, 1, 1, 1); return; }
            _menyuBasla = pnlNav.Width; _menyuSaat.Restart();
            if (ac) PerdeGizle();
            else { MezmunSol(64); PerdeGoster(); }   // bağlanır: səhifə menyunun arxasında genişlənir, sonra menyu çəkilir
            pnlNav.BringToFront(); _menyuT.Start();
        }

        // Bağlanarkən menyunun altında açılan səhifə hissəsi hər addımda yenidən çəkilməsin (donma olurdu):
        // animasiyanın əvvəlində bir dəfə şəkli çəkilir, animasiya boyu "pərdə" bu şəkli göstərir.
        private Panel _perde;
        private Bitmap _perdeSekil;
        private void PerdeGoster()
        {
            PerdeGizle();
            int en = pnlNav.Right - pnlContent.Left, hund = pnlContent.Height;
            if (en <= 0 || hund <= 0) return;
            try
            {
                using var tam = new Bitmap(pnlContent.Width, hund);
                pnlContent.DrawToBitmap(tam, new Rectangle(0, 0, tam.Width, hund));
                _perdeSekil = tam.Clone(new Rectangle(0, 0, Math.Min(en, tam.Width), hund), tam.PixelFormat);
            }
            catch { return; }   // şəkil alınmasa adi qaydada bağlanır
            if (_perde == null)
            {
                _perde = new BufPanel { Visible = false };
                _perde.Paint += (_, e) => { if (_perdeSekil != null) e.Graphics.DrawImageUnscaled(_perdeSekil, 0, 0); };
                Controls.Add(_perde);
            }
            _perde.SetBounds(pnlContent.Left, pnlContent.Top, _perdeSekil.Width, hund);
            _perde.Visible = true; _perde.BringToFront();
        }
        private void PerdeGizle()
        {
            if (_perde != null) _perde.Visible = false;
            _perdeSekil?.Dispose(); _perdeSekil = null;
        }
        private sealed class BufPanel : Panel
        {
            public BufPanel() { SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true); }
        }

        private PageBase Sehife(string key)
        {
            if (_pages.TryGetValue(key, out var page)) return page;
            page = key switch
            {
                "home" => new HomePage(), "list" => new RegistryPage(), "add" => new EditPage(), "tech" => new TechizatPage(), "sadd" => new SorguEditPage(), "rep" => new HesabatPage(),
                "det" => new DetailPage(), "noti" => new NotificationsPage(), "sync" => new SyncPage(), "set" => new TenzimlemePage(),
                _ => throw new ArgumentException(key)
            };
            page.Dock = DockStyle.Fill; page.Visible = false; pnlContent.Controls.Add(page); Ui.Hazirla(page); _pages[key] = page;
            return page;
        }

        /// <summary>
        /// Açılışdan sonra, istifadəçi ana səhifəyə baxarkən qalan səhifələr bir-bir arxa planda hazırlanır
        /// (kontrolların yaradılması və Windows pəncərələri). Menyuya ilk klikdə gözləmə olmur.
        /// </summary>
        private async Task OnHazirlaAsync()
        {
            var keys = new List<string> { "list", "det", "tech", "rep", "noti", "sync", "set" };
            if (Session.YazaBiler) keys.Insert(2, "add");
            foreach (var k in keys)
            {
                await Task.Delay(120);                       // UI-yə nəfəs: istifadəçinin klikləri arada işlənir
                if (IsDisposed || _pages.ContainsKey(k)) continue;
                var p = Sehife(k);
                // Görünməz səhifənin pəncərələri yaradılmır; aktiv səhifənin ARXASINDA qısa müddət görünən edib yaradırıq
                p.SendToBack(); p.Visible = true; p.CreateControl(); p.PerformLayout(); p.Visible = false;
            }
        }

        public async Task AcAsync(string key, Func<PageBase, Task> hazirliq = null)
        {
            if (key is "log" or "bak") { int t = key == "log" ? 1 : 0; key = "set"; hazirliq = p => ((TenzimlemePage)p).SecAsync(t); }
            var page = Sehife(key);
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
        public Task SorguAcAsync(long id) => AcAsync("sadd", p => ((SorguEditPage)p).YukleAsync(id));
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
