using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Services;

namespace TexnikaReyestri.Pages.Admin
{
    public partial class AdmKeysPage : PageBase
    {
        public AdmKeysPage() { InitializeComponent(); grid.RowTemplate.Height = 34; }

        public override async Task OnShownAsync()
        {
            if (!KeyStore.Var) await Task.Run(KeyStore.Yarat);
            var (i, s) = await Task.Run(KeyStore.AciqAcarlar); vFp.Text = KeyStore.BarmaqIzi(i, s);
            grid.Rows.Clear(); var q = await SistemRepo.QurumlarAsync();
            foreach (var (k, iz, t) in await KeyStore.EtibarlilarAsync()) grid.Rows.Add(Lang.T(q.FirstOrDefault(x => x.Kod == k)?.Ad ?? k), iz, Vaxt.Yaz(t));
        }

        private async void btnExportKey_Click(object sender, EventArgs e)
        {
            string kod = await SistemRepo.ParamStrAsync("KomputerQurumu");
            using var d = new SaveFileDialog { Filter = "Açıq açar|*.trkey", FileName = $"{kod}_acar.trkey" };
            if (d.ShowDialog(this) != DialogResult.OK) return;
            var q = (await SistemRepo.QurumlarAsync()).First(x => x.Kod == kod);
            string qk = Session.QurumKod, qa = Session.QurumAd; Session.QurumKod = q.Kod; Session.QurumAd = q.Ad;
            try { KeyStore.AciqAcariYaz(d.FileName); } finally { Session.QurumKod = qk; Session.QurumAd = qa; }
            await SistemRepo.AdminLogAsync($"Açıq açar yazıldı: {Path.GetFileName(d.FileName)}"); Toast.Show(this, $"{Path.GetFileName(d.FileName)} yazıldı");
        }

        private async void btnImportKey_Click(object sender, EventArgs e)
        {
            using var d = new OpenFileDialog { Filter = "Açıq açar|*.trkey" };
            if (d.ShowDialog(this) != DialogResult.OK) return;
            try
            {
                var a = KeyStore.Oxu(d.FileName);
                if (!Ui.Tesdiq(this, "Açarı etibar etmək", $"{Lang.T("Qurum")}: {a.QurumAd} ({a.Qurum})\n{Lang.T("Barmaq izi")}: {a.BarmaqIzi}\n\n{Lang.T("Barmaq izini həmin qurumla telefonla və ya şəxsən yoxlayın. Eynidirsə, təsdiqləyin.")}", "Etibar et")) return;
                await KeyStore.EtibarEtAsync(a); await SistemRepo.AdminLogAsync($"Açar etibar edildi: {a.Qurum} {a.BarmaqIzi}");
                Toast.Show(this, "Açar əlavə edildi"); await OnShownAsync();
            }
            catch (Exception ex) { Toast.Show(this, ex.Message, ToastKind.Error); }
        }

        private async void btnNewKey_Click(object sender, EventArgs e)
        {
            if (!Ui.Tesdiq(this, "Yeni açar yaratmaq", "Köhnə açar ləğv olunacaq. Alıcı qurum yeni açıq açarı idxal edənə qədər bu kompüterin paketləri qəbul edilməyəcək. Bunu yalnız açarın oğurlandığından şübhələndikdə edin.", "Yarat", true)) return;
            await Task.Run(KeyStore.Yarat); await SistemRepo.AdminLogAsync("Yeni açar yaradıldı"); Toast.Show(this, "Yeni açar yaradıldı. Açıq açarı alıcı quruma yenidən verin.", ToastKind.Warn); await OnShownAsync();
        }
    }
}
