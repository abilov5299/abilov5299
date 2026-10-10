using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Services;

namespace TexnikaReyestri.Pages.Admin
{
    public partial class AdmPcPage : PageBase
    {
        public AdmPcPage() { InitializeComponent(); }

        public override async Task OnShownAsync()
        {
            string kod = await SistemRepo.ParamStrAsync("KomputerQurumu");
            vQurum.Text = Lang.T((await SistemRepo.QurumlarAsync()).FirstOrDefault(q => q.Kod == kod)?.Ad ?? "—");
            vDb.Text = AppPaths.Db; vSize.Text = File.Exists(AppPaths.Db) ? $"{new FileInfo(AppPaths.Db).Length / 1_048_576.0:0.0} MB" : "—";
            vCount.Text = (await TexnikaRepo.SayAsync(new ReyestrFiltr())).ToString("#,0").Replace(",", " ");
            vApp.Text = Application.ProductVersion;
        }

        private async void btnCheck_Click(object sender, EventArgs e)
        {
            bool ok = false; await Ui.Is(this, "Bütövlük yoxlanılır...", async _ => ok = await LocalDb.ButovlukAsync());
            await SistemRepo.AdminLogAsync("Bütövlük yoxlaması: " + (ok ? "xəta yoxdur" : "XƏTA"));
            Toast.Show(this, ok ? "Bütövlük yoxlaması: xəta tapılmadı" : "Bazada xəta tapıldı! Ehtiyat nüsxədən bərpa edin.", ok ? ToastKind.Ok : ToastKind.Error);
        }

        private async void btnVacuum_Click(object sender, EventArgs e)
        {
            long evvel = new FileInfo(AppPaths.Db).Length;
            if (await Ui.Is(this, "Baza sıxılır...", _ => LocalDb.XidmetAsync(true)))
            { long sonra = new FileInfo(AppPaths.Db).Length; await SistemRepo.AdminLogAsync($"Baza sıxıldı: {evvel / 1_048_576.0:0.0} → {sonra / 1_048_576.0:0.0} MB"); Toast.Show(this, $"{evvel / 1_048_576.0:0.0} MB → {sonra / 1_048_576.0:0.0} MB"); await OnShownAsync(); }
        }

        private async void btnDemo_Click(object sender, EventArgs e)
        {
            int n = (int)numDemo.Value;
            if (!Ui.Tesdiq(this, "Nümunə məlumat yarat", $"{n:#,0} {Lang.T("nümunə qeyd bazaya yazılacaq. Bu, yalnız sınaq üçündür.")}", "Yarat")) return;
            var sw = System.Diagnostics.Stopwatch.StartNew();
            if (await Ui.Is(this, "Nümunə məlumat yazılır...", ov => n == 10 ? DemoData.On10Async(Session.ZavodFiltri) : DemoData.YaratAsync(n, Session.ZavodFiltri, new Progress<int>(i => ov.Report(i * 100 / n, $"{i:#,0} / {n:#,0}")), default)))
            { await SistemRepo.AdminLogAsync($"Nümunə məlumat: {n} qeyd"); Toast.Show(this, $"{n:#,0} qeyd {sw.Elapsed.TotalSeconds:0.0} saniyəyə yazıldı"); await OnShownAsync(); }
        }
    }
}
