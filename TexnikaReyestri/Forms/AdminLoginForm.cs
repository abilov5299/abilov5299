using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Services;

namespace TexnikaReyestri.Forms
{
    /// <summary>Gizli admin girişi. 3 yanlış cəhddən sonra 5 dəqiqəlik bağlanır.</summary>
    public partial class AdminLoginForm : Form
    {
        private static int _yanlis; private static DateTime _blok = DateTime.MinValue;
        public AdminLoginForm() { InitializeComponent(); }
        protected override void OnLoad(EventArgs e) { Ui.Hazirla(this); BackColor = Theme.Line; base.OnLoad(e); }

        private async void btnOk_Click(object sender, EventArgs e)
        {
            if (DateTime.Now < _blok) { lblErr.Text = $"{Lang.T("Admin girişi müvəqqəti bağlanıb")}: {(int)(_blok - DateTime.Now).TotalMinutes + 1} {Lang.T("dəq")}"; return; }
            btnOk.Enabled = false;
            bool ok = await AuthService.AdminYoxlaAsync(txtPass.Text);
            btnOk.Enabled = true;
            if (!ok)
            {
                _yanlis++; await SistemRepo.AdminLogAsync("Uğursuz admin girişi cəhdi");
                if (_yanlis >= 3) { _blok = DateTime.Now.AddMinutes(5); _yanlis = 0; lblErr.Text = Lang.T("3 yanlış cəhd: admin girişi 5 dəqiqəlik bağlandı."); }
                else lblErr.Text = Lang.T("Parol yanlışdır."); txtPass.SelectAll(); return;
            }
            _yanlis = 0; await SistemRepo.AdminLogAsync("Admin panelinə giriş");
            DialogResult = DialogResult.OK;
        }
        private void btnCancel_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;
    }
}
