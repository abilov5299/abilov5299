using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Services;

namespace TexnikaReyestri.Forms
{
    /// <summary>İlk açılış: kompüterin qurumu, admin parolu, ilk istifadəçi və şifrələmə açarları.</summary>
    public partial class FirstRunForm : Form
    {
        private List<Qurum> _q;
        public FirstRunForm() { InitializeComponent(); }

        protected override async void OnLoad(EventArgs e)
        {
            Ui.Hazirla(this); BackColor = Theme.Line; base.OnLoad(e);
            _q = await SistemRepo.QurumlarAsync();
            foreach (var q in _q) cmbQurum.Items.Add(Lang.T(q.Ad));
            cmbQurum.SelectedIndex = _q.FindIndex(x => x.Tip == QurumTip.Zavod);
        }

        private async void btnOk_Click(object sender, EventArgs e)
        {
            lblErr.Text = "";
            if (cmbQurum.SelectedIndex < 0) { lblErr.Text = Lang.T("Qurumu seçin."); return; }
            if (txtAdmin.Text.Length < 12) { lblErr.Text = Lang.T("Admin parolu ən azı 12 simvol olmalıdır."); return; }
            if (txtAdmin.Text != txtAdmin2.Text) { lblErr.Text = Lang.T("Admin parolları eyni deyil."); return; }
            if (txtLogin.Text.Trim().Length < 3 || txtName.Text.Trim().Length < 3) { lblErr.Text = Lang.T("İstifadəçi adını və ad soyadı yazın."); return; }
            var x = AuthService.ParolQaydasi(txtPass.Text, 8); if (x != null) { lblErr.Text = Lang.T(x); return; }
            btnOk.Enabled = false;
            try
            {
                var q = _q[cmbQurum.SelectedIndex];
                await SistemRepo.ParamYazAsync("KomputerQurumu", q.Kod);
                await AuthService.AdminTeyinAsync(txtAdmin.Text);
                await AuthService.YaratAsync(txtLogin.Text, txtName.Text, q.Kod, q.Tip == QurumTip.Zavod ? "Zavod istifadəçisi" : "Yalnız baxış", txtPass.Text, false);
                if (!KeyStore.Var) await Task.Run(KeyStore.Yarat);
                await SistemRepo.AdminLogAsync($"İlk quraşdırma: kompüter \"{q.Ad}\" qurumuna bağlandı, ilk istifadəçi \"{txtLogin.Text.Trim()}\"");
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex) { lblErr.Text = ex.Message; btnOk.Enabled = true; }
        }
        private void btnCancel_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;
        protected override void OnMouseDown(MouseEventArgs e) { Win32.DragWindow(this); base.OnMouseDown(e); }
    }
}
