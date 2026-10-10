using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Services;

namespace TexnikaReyestri.Pages
{
    public partial class BackupPage : PageBase
    {
        private List<BackupService.Nusxe> _l = new();
        public BackupPage() { InitializeComponent(); grid.RowTemplate.Height = 36; }

        public override async Task OnShownAsync()
        {
            _l = BackupService.Siyahi(); grid.Rows.Clear();
            foreach (var n in _l) grid.Rows.Add(n.Tarix.ToString("dd.MM.yyyy HH:mm"), $"{n.Olcu / 1_048_576.0:0.0} MB", Lang.T(n.Avtomatik ? "Avtomatik" : "Əl ilə"), Path.GetFileName(n.Fayl));
            vFolder.Text = AppPaths.Backup; vLast.Text = _l.Count == 0 ? "—" : _l[0].Tarix.ToString("dd.MM.yyyy HH:mm");
            vSize.Text = File.Exists(AppPaths.Db) ? $"{new FileInfo(AppPaths.Db).Length / 1_048_576.0:0.0} MB" : "—";
            vKept.Text = $"{_l.Count} / {await SistemRepo.ParamAsync("NusxeSay", 30)}";
        }

        private async void btnNow_Click(object sender, EventArgs e)
        {
            btnNow.Enabled = false;
            if (await Ui.Is(this, "Ehtiyat nüsxə alınır...", ov => BackupService.AlAsync(false, new Progress<int>(p => ov.Report(p)))))
            { await SistemRepo.LogAsync("Nüsxə", "Ehtiyat nüsxə alındı (əl ilə)"); Toast.Show(this, "Ehtiyat nüsxə alındı"); await OnShownAsync(); }
            btnNow.Enabled = true;
        }

        private async void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _l.Count || grid.Columns[e.ColumnIndex].Name != "colRestore") return;
            var n = _l[e.RowIndex];
            if (!Ui.Tesdiq(this, "Bazanı bərpa etmək", $"{Lang.T("Baza bu tarixli nüsxəyə qaytarılacaq")}: {n.Tarix:dd.MM.yyyy HH:mm}. {Lang.T("Həmin tarixdən sonra edilən dəyişikliklər itəcək. İndiki baza əvvəlcə ayrıca nüsxə kimi saxlanılır. Proqram yenidən başlayacaq.")}", "Bərpa et", true)) return;
            if (await Ui.Is(this, "Baza bərpa olunur...", _ => BackupService.BerpaAsync(n)))
            { await SistemRepo.LogAsync("Nüsxə", $"Baza {n.Tarix:dd.MM.yyyy HH:mm} nüsxəsindən bərpa edildi"); Application.Restart(); Environment.Exit(0); }
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(AppPaths.Backup);
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("explorer.exe", AppPaths.Backup) { UseShellExecute = true });
        }
    }
}
