using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;

namespace TexnikaReyestri.Pages
{
    public partial class LogsPage : PageBase
    {
        private bool _ilk = true;
        public LogsPage()
        {
            InitializeComponent(); grid.RowTemplate.Height = 32;
            dtFrom.Value = DateTime.Today.AddDays(-7); dtTo.Value = DateTime.Today; cmbType.SelectedIndex = 0;
        }

        public override async Task OnShownAsync() { _ilk = false; await AxtarAsync(); }   // hər açılışda təzə loglar

        private async Task AxtarAsync()
        {
            string nov = cmbType.SelectedIndex > 0 ? (string)new[] { null, "Giriş", "Giriş xətası", "Çıxış", "Əlavə", "Redaktə", "Silmə", "İxrac", "İdxal", "Xəta" }[cmbType.SelectedIndex] : null;
            var dt = await SistemRepo.LoglarAsync(dtFrom.Value, dtTo.Value, nov);
            grid.Rows.Clear();
            foreach (System.Data.DataRow r in dt.Rows) grid.Rows.Add(Vaxt.Yaz((long)r["Tarix"]), r["Istifadeci"], Lang.T(r["Zavod"] as string ?? "—"), r["Nov"], r["Mesaj"]);
        }

        private async void btnSearch_Click(object sender, EventArgs e) => await AxtarAsync();
    }
}
