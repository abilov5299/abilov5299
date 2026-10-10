using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Services;

namespace TexnikaReyestri.Forms
{
    public partial class UserEditForm : Form
    {
        private readonly Istifadeci _u; private List<Qurum> _q;
        public UserEditForm() { InitializeComponent(); }
        public UserEditForm(Istifadeci u) : this() { _u = u; }

        protected override async void OnLoad(EventArgs e)
        {
            Ui.Hazirla(this); BackColor = Theme.Line; base.OnLoad(e);
            _q = await SistemRepo.QurumlarAsync();
            foreach (var q in _q) cmbQurum.Items.Add(Lang.T(q.Ad));
            foreach (var r in Secimler.Rollar) cmbRol.Items.Add(Lang.T(r));
            if (_u != null)
            {
                lblTitle.Text = Lang.T("İstifadəçini redaktə et"); txtLogin.Text = _u.Login; txtLogin.Enabled = false; txtName.Text = _u.AdSoyad;
                cmbQurum.SelectedIndex = _q.FindIndex(x => x.Kod == _u.QurumKod); cmbRol.SelectedIndex = Array.IndexOf(Secimler.Rollar, _u.Rol);
                ltxtPass.Visible = txtPass.Visible = chkTemp.Visible = false;
            }
            else { cmbQurum.SelectedIndex = Math.Max(0, _q.FindIndex(x => x.Kod == Session.QurumKod)); cmbRol.SelectedIndex = 0; txtPass.Text = AuthService.MuveqqetiParol(); }
        }

        private void cmbQurum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_q == null || cmbQurum.SelectedIndex < 0) return;
            bool zavod = _q[cmbQurum.SelectedIndex].Tip == QurumTip.Zavod;
            cmbRol.Enabled = zavod; if (!zavod) cmbRol.SelectedIndex = 2;
            lblRoleInfo.Text = Lang.T(!zavod ? "Üst qurumlarda bütün istifadəçilər yalnız baxış hüququna malikdir." : cmbRol.SelectedIndex switch
            { 0 => "Texnika daxil edir, redaktə edir, ixrac edir.", 1 => "İstifadəçi hüquqları + öz zavodunun istifadəçilərinin siyahısına baxır.", _ => "Yalnız baxır, heç nə dəyişə bilmir." });
        }

        private async void btnOk_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim().ToLowerInvariant(), ad = txtName.Text.Trim();
            if (login.Length < 3 || ad.Length < 3) { lblErr.Text = Lang.T("Login və ad soyad məcburidir."); return; }
            var q = _q[cmbQurum.SelectedIndex]; string rol = Secimler.Rollar[Math.Max(0, cmbRol.SelectedIndex)];
            try
            {
                if (_u == null)
                {
                    if ((await SistemRepo.IstifadecilerAsync()).Any(x => x.Login.Equals(login, StringComparison.OrdinalIgnoreCase))) { lblErr.Text = Lang.T("Bu login artıq var."); return; }
                    var x = AuthService.ParolQaydasi(txtPass.Text, await SistemRepo.ParamAsync("ParolMin", 8)); if (x != null) { lblErr.Text = Lang.T(x); return; }
                    await AuthService.YaratAsync(login, ad, q.Kod, rol, txtPass.Text, chkTemp.Checked);
                    await SistemRepo.AdminLogAsync($"Yeni istifadəçi: \"{login}\" ({q.Ad}, {rol})");
                }
                else
                {
                    await AuthService.YenileAsync(_u.Login, ad, q.Kod, rol);
                    await SistemRepo.AdminLogAsync($"\"{_u.Login}\" dəyişdi: {_u.QurumKod} → {q.Kod}, {_u.Rol} → {rol}");
                }
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex) { lblErr.Text = ex.Message; }
        }
        private void btnCancel_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;
    }
}
