using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Services;

namespace TexnikaReyestri.Forms
{
    public partial class ChangePasswordForm : Form
    {
        private readonly string _login;
        public ChangePasswordForm() { InitializeComponent(); }
        public ChangePasswordForm(string login) : this() { _login = login; }
        protected override void OnLoad(EventArgs e) { Ui.Hazirla(this); BackColor = Theme.Line; base.OnLoad(e); }

        private async void btnOk_Click(object sender, EventArgs e)
        {
            if (txtNew.Text != txtNew2.Text) { lblErr.Text = Lang.T("Parollar eyni deyil."); return; }
            var x = AuthService.ParolQaydasi(txtNew.Text, await SistemRepo.ParamAsync("ParolMin", 8));
            if (x != null) { lblErr.Text = Lang.T(x); return; }
            await AuthService.ParolDeyisAsync(_login, txtNew.Text, false);
            await SistemRepo.LogAsync("Giriş", $"{_login}: parol dəyişdirildi");
            DialogResult = DialogResult.OK;
        }
        private void btnCancel_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;
    }
}
