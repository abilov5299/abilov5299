using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Services;

namespace TexnikaReyestri.Pages.Admin
{
    public partial class AdmSecurityPage : PageBase
    {
        public AdmSecurityPage() { InitializeComponent(); }

        public override async Task OnShownAsync()
        {
            numMin.Value = await SistemRepo.ParamAsync("ParolMin", 8); numExp.Value = await SistemRepo.ParamAsync("ParolMuddet", 90);
            numLock.Value = await SistemRepo.ParamAsync("Blok", 5); numIdle.Value = await SistemRepo.ParamAsync("Fealiyyetsiz", 15);
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            await SistemRepo.ParamYazAsync("ParolMin", ((int)Math.Max(6, numMin.Value)).ToString()); await SistemRepo.ParamYazAsync("ParolMuddet", ((int)numExp.Value).ToString());
            await SistemRepo.ParamYazAsync("Blok", ((int)Math.Max(3, numLock.Value)).ToString()); await SistemRepo.ParamYazAsync("Fealiyyetsiz", ((int)numIdle.Value).ToString());
            await SistemRepo.AdminLogAsync($"Təhlükəsizlik: parol ≥{numMin.Value}, müddət {numExp.Value} gün, blok {numLock.Value} cəhd, çıxış {numIdle.Value} dəq");
            Toast.Show(this, "Yadda saxlanıldı");
        }

        private async void btnAdminPw_Click(object sender, EventArgs e)
        {
            if (txtNew.Text.Length < 12) { Toast.Show(this, "Admin parolu ən azı 12 simvol olmalıdır.", ToastKind.Error); return; }
            if (txtNew.Text != txtNew2.Text) { Toast.Show(this, "Admin parolları eyni deyil.", ToastKind.Error); return; }
            await AuthService.AdminTeyinAsync(txtNew.Text); txtNew.Clear(); txtNew2.Clear();
            await SistemRepo.AdminLogAsync("Admin parolu dəyişdirildi"); Toast.Show(this, "Admin parolu dəyişdirildi");
        }
    }
}
