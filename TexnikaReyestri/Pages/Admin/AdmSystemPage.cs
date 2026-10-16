using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;

namespace TexnikaReyestri.Pages.Admin
{
    public partial class AdmSystemPage : PageBase
    {
        private static readonly (string ad, int susma)[] P = { ("BildirisYaxin", 3), ("BildirisGec", 0), ("BildirisSorgu", 30), ("BildirisPaket", 7), ("NusxeSay", 30), ("IcraSari", 20) };
        public AdmSystemPage() { InitializeComponent(); }
        private NumericUpDown[] N => new[] { numSoon, numLate, numReq, numPkg, numKeep, numSari };

        public override async Task OnShownAsync()
        {
            for (int i = 0; i < P.Length; i++) N[i].Value = await SistemRepo.ParamAsync(P[i].ad, P[i].susma);
            chkAnim.Checked = await SistemRepo.ParamAsync("Animasiya", 1) == 1;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < P.Length; i++) await SistemRepo.ParamYazAsync(P[i].ad, ((int)N[i].Value).ToString());
            await SistemRepo.ParamYazAsync("Animasiya", chkAnim.Checked ? "1" : "0"); Animator.Aktiv = chkAnim.Checked; ReyestrFiltr.Yaxin = (int)numSoon.Value; ReyestrFiltr.Sari = (int)numSari.Value;
            await SistemRepo.AdminLogAsync("Sistem parametrləri dəyişdi"); Toast.Show(this, "Yadda saxlanıldı");
        }
    }
}
