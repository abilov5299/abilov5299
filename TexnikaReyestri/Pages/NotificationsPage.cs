using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Forms;

namespace TexnikaReyestri.Pages
{
    public partial class NotificationsPage : PageBase
    {
        private static readonly string[] Filtr = { null, "unread", "late", "soon", "part", "sync", "new" };
        private List<Bildiris> _l = new();

        public NotificationsPage() { InitializeComponent(); grid.RowTemplate.Height = 40; }

        public override async Task OnShownAsync()
        {
            string f = Filtr[segFilter.SelectedIndex];
            _l = await SistemRepo.BildirislerAsync(f is null or "unread" ? null : f, f == "unread");
            grid.Rows.Clear();
            foreach (var b in _l) grid.Rows.Add("●", Lang.T(b.Basliq), b.Metn, Lang.T(b.Zavod ?? ""), Vaxt.Yaz(b.Tarix));
            if (_l.Count == 0) grid.Rows.Add("", Lang.T("Bildiriş yoxdur"), "", "", "");
        }

        public static Color NovRengi(string nov) => nov switch { "late" => Theme.Bad, "soon" => Theme.Warn, "part" => Theme.C("#8A5CC2"), "sync" => Theme.C("#B4570B"), "new" => Theme.Info, _ => Theme.Muted };

        private void grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _l.Count) return;
            var b = _l[e.RowIndex];
            if (e.ColumnIndex == 0) { e.CellStyle.ForeColor = b.Oxunub ? Theme.Line : NovRengi(b.Nov); e.CellStyle.Font = new Font("Segoe UI", 12f); }
            else if (!b.Oxunub && e.ColumnIndex == 1) e.CellStyle.Font = new Font(grid.Font, FontStyle.Bold);
        }

        private async void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _l.Count) return;
            var b = _l[e.RowIndex];
            if (!b.Oxunub) { await SistemRepo.OxunduAsync(b.Id); await MainForm.Current.BadgeYenileAsync(); }
            if (b.TexnikaId is long id) await MainForm.Current.DetalAcAsync(id);
            else if (b.Nov is "late" or "soon") await MainForm.Current.ReyestrAcAsync(new ReyestrFiltr { Zavod = b.Zavod, Qrup = b.Nov == "late" ? "late" : "yaxin" });
            else await OnShownAsync();
        }

        private async void segFilter_SelectedIndexChanged(object sender, EventArgs e) => await OnShownAsync();
        private async void btnReadAll_Click(object sender, EventArgs e) { await SistemRepo.OxunduAsync(null); await MainForm.Current.BadgeYenileAsync(); await OnShownAsync(); }
    }
}
