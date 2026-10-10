using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Services;

namespace TexnikaReyestri.Forms
{
    public partial class LoginForm : Form
    {
        private readonly List<DateTime> _logoKlik = new();
        /// <summary>Gizli admin girişi tələb olunduqda true (Program.cs admin pəncərəsini açır).</summary>
        public bool AdminIstendi { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
            pnlForm.MouseDown += (_, e) => { if (e.Button == MouseButtons.Left) Win32.DragWindow(this); };
            pnlBrand.MouseDown += (_, e) => { if (e.Button == MouseButtons.Left) Win32.DragWindow(this); };
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            cmbLang.SelectedIndex = Ui.DilIndex;
            btnTheme.Glyph = Ui.TemaGlyph;
            Ui.Hazirla(this);
            ActiveControl = txtUser;
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
    }
}
