using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Services;

namespace TexnikaReyestri.Forms
{
    public partial class LoginForm : Form
    {
        private readonly List<DateTime> _logoKlik = new();
        private CheckBox chkXatirla; private LinkLabel lnkUnut; private Control loqoDev;
        /// <summary>Gizli admin girişi tələb olunduqda true (Program.cs admin pəncərəsini açır).</summary>
        public bool AdminIstendi { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
            pnlForm.MouseDown += (_, e) => { if (e.Button == MouseButtons.Left) Win32.DragWindow(this); };
            pnlBrand.MouseDown += (_, e) => { if (e.Button == MouseButtons.Left) Win32.DragWindow(this); };
            Opacity = 0;   // OnShown-da animasiya ilə görünür
            // Tab sırası: istifadəçi adı → parol → məni xatırla → daxil ol
            txtUser.TabIndex = 0; txtPass.TabIndex = 1; btnLogin.TabIndex = 3;
            chkXatirla = new CheckBox { Text = "Bu kompüterdə məni xatırla", AutoSize = true, TabIndex = 2, Font = new Font("Segoe UI", 9f), Location = new Point(txtPass.Left, txtPass.Bottom + 8) };
            lnkUnut = new LinkLabel { Text = "Unut", AutoSize = true, Visible = false, TabStop = false, Font = new Font("Segoe UI", 9f) };
            lnkUnut.LinkClicked += async (_, _) => { await SistemRepo.ParamYazAsync("XatirlananLogin", ""); txtUser.Text = ""; chkXatirla.Checked = false; lnkUnut.Visible = false; txtUser.Focus(); };
            txtPass.Parent.Controls.Add(chkXatirla); txtPass.Parent.Controls.Add(lnkUnut);
            chkXatirla.SizeChanged += (_, _) => lnkUnut.Location = new Point(chkXatirla.Right + 8, chkXatirla.Top + 1);
            if (btnLogin.Top < chkXatirla.Bottom + 8) { int d = chkXatirla.Bottom + 10 - btnLogin.Top; btnLogin.Top += d; lblError.Top += d; }
            txtUser.KeyDown += (_, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; txtPass.Focus(); } };
            // Proqramçı loqosu: brend panelinin sağ aşağı küncündə, solğun, yazısız
            loqoDev = new ProqramciLoqo { Size = new Size(22, 22) };
            pnlBrand.Controls.Add(loqoDev);
            void Dev() => loqoDev.Location = new Point(pnlBrand.ClientSize.Width - 36, pnlBrand.ClientSize.Height - 36);
            pnlBrand.Resize += (_, _) => Dev(); Dev();
            // Parola baxış gözü: basılı saxlayanda yox, klik ilə göstər/gizlət
            // Göz parol xanasının İÇİNDƏ, sağda; mətn onun altına düşməsin deyə sağ kənar boşluğu (EM_SETMARGINS)
            var goz = new Label { Text = "\uE7B3", Font = Icons.Font(11f), Dock = DockStyle.Right, Width = 26, TextAlign = ContentAlignment.MiddleCenter, Cursor = Cursors.Hand, Tag = "muted" };
            goz.Click += (_, _) => { txtPass.UseSystemPasswordChar = !txtPass.UseSystemPasswordChar; goz.Text = txtPass.UseSystemPasswordChar ? "\uE7B3" : "\uED1A"; txtPass.Focus(); txtPass.SelectionStart = txtPass.TextLength; };
            txtPass.Controls.Add(goz);
            void Kenar() { goz.BackColor = txtPass.BackColor; if (txtPass.IsHandleCreated) Win32.SendMessage(txtPass.Handle, 0xD3, (IntPtr)2, (IntPtr)(28 << 16)); }
            txtPass.HandleCreated += (_, _) => Kenar(); txtPass.BackColorChanged += (_, _) => Kenar();
            // Versiya yazısı brend panelinin aşağısında (Designer-də panel ölçüsündən əvvəl bərkidilirdi və yuxarı düşürdü)
            void Ver() => lblVersion.Top = pnlBrand.ClientSize.Height - lblVersion.Height - 24;
            lblVersion.Anchor = AnchorStyles.Left | AnchorStyles.Top; pnlBrand.Resize += (_, _) => Ver(); Ver();
        }

        private bool _xatirlaIcaze = true;

        private async void LoginForm_Load(object sender, EventArgs e)
        {
            cmbLang.SelectedIndex = Ui.DilIndex;
            btnTheme.Glyph = Ui.TemaGlyph;
            Ui.Hazirla(this);
            ActiveControl = txtUser;
            // "Məni xatırla": yalnız istifadəçi adı bu kompüterdə saxlanılır (parol yox)
            _xatirlaIcaze = await SistemRepo.ParamAsync("GirisXatirla", 1) == 1;
            chkXatirla.Visible = _xatirlaIcaze;
            string son = _xatirlaIcaze ? await SistemRepo.ParamStrAsync("XatirlananLogin") : null;
            if (!string.IsNullOrEmpty(son)) { txtUser.Text = son; chkXatirla.Checked = true; lnkUnut.Visible = true; ActiveControl = txtPass; }
            loqoDev.Visible = await SistemRepo.ParamAsync("ProqramciLoqo", 1) == 1;
        }

        // Kölgədən çıxırmış kimi: şəffaflıq 0→1 və 28 px aşağıdan yuxarı, yumşaq yavaşlama (ease-out). Parametrlər Admin → Sistem
        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (await SistemRepo.ParamAsync("GirisSes", 1) == 1) System.Media.SystemSounds.Asterisk.Play();
            int ms = await SistemRepo.ParamAsync("GirisAnimasiya", 900);
            if (ms <= 0) { Opacity = 1; return; }
            int y0 = Top; var sw = System.Diagnostics.Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < ms && !IsDisposed)
            {
                double t = sw.ElapsedMilliseconds / (double)ms, k = 1 - Math.Pow(1 - t, 3);
                Opacity = k; Top = y0 + (int)(28 * (1 - k));
                await Task.Delay(10);
            }
            if (!IsDisposed) { Opacity = 1; Top = y0; }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUser.Text.Trim().Length == 0) { lblError.Text = Lang.T("İstifadəçi adı və ya parol yanlışdır."); return; }
            btnLogin.Enabled = false; lblError.Text = "";
            try
            {
                var (n, qalan) = await AuthService.GirisAsync(txtUser.Text, txtPass.Text);
                switch (n)
                {
                    case GirisNetice.Yanlis:
                        lblError.Text = Lang.T("İstifadəçi adı və ya parol yanlışdır.") + (qalan < 5 ? $" ({qalan})" : "");
                        await SistemRepo.LogAsync("Giriş xətası", $"Uğursuz giriş: {txtUser.Text.Trim()}");
                        txtPass.SelectAll(); txtPass.Focus(); return;
                    case GirisNetice.Bloklanib:
                        lblError.Text = Lang.T("Bu hesab bloklanıb. Administratora müraciət edin.");
                        await SistemRepo.LogAsync("Giriş xətası", $"Bloklanmış hesab: {txtUser.Text.Trim()}"); return;
                    case GirisNetice.MuveqqetiParol:
                        using (var f = new ChangePasswordForm(Session.Login)) if (f.ShowDialog(this) != DialogResult.OK) { Session.Bitir(); return; }
                        break;
                }
                if (_xatirlaIcaze) await SistemRepo.ParamYazAsync("XatirlananLogin", chkXatirla.Checked ? txtUser.Text.Trim() : "");
                await SistemRepo.LogAsync("Giriş", $"{Session.AdSoyad} daxil oldu");
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex) { lblError.Text = ex.Message; }
            finally { btnLogin.Enabled = true; }
        }

        // Gizli admin: loqoya 3 saniyə ərzində 5 klik və ya Ctrl+Shift+F12
        private void logo_Click(object sender, EventArgs e)
        {
            _logoKlik.RemoveAll(t => (DateTime.Now - t).TotalSeconds > 3); _logoKlik.Add(DateTime.Now);
            if (_logoKlik.Count >= 5) { _logoKlik.Clear(); AdminAc(); }
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.F12) { e.Handled = true; AdminAc(); }
        }

        private void AdminAc() { AdminIstendi = true; DialogResult = DialogResult.Retry; }

        private async void btnTheme_Click(object sender, EventArgs e) { Theme.Toggle(); btnTheme.Glyph = Ui.TemaGlyph; Ui.Hazirla(this); Invalidate(true); await Ui.TemaSaxla(); }

        private async void cmbLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLang.SelectedIndex < 0 || cmbLang.SelectedIndex == Ui.DilIndex) return;
            Lang.Set((Language)cmbLang.SelectedIndex); Lang.Apply(this); Invalidate(true); await Ui.DilSaxla();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (Ui.Tesdiq(this, "Proqramı bağlamaq", "Proqram bağlanacaq.", "Bağla", true)) DialogResult = DialogResult.Abort;
        }

        protected override void OnMouseDown(MouseEventArgs e) { if (e.Button == MouseButtons.Left) Win32.DragWindow(this); base.OnMouseDown(e); }

        protected override CreateParams CreateParams { get { var cp = base.CreateParams; cp.ClassStyle |= Win32.CS_DROPSHADOW; return cp; } }
        protected override void OnHandleCreated(EventArgs e) { base.OnHandleCreated(e); Win32.Yumru(this); }
    }
}
