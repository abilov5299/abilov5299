using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Forms;
using TexnikaReyestri.Services;

namespace TexnikaReyestri.Pages
{
    /// <summary>Üst qurum üçün zavodlar üzrə icmal.</summary>
    public partial class SummaryPage : PageBase
    {
        private System.Data.DataTable _dt;
        public SummaryPage() { InitializeComponent(); grid.RowTemplate.Height = 40; }

        public override async Task OnShownAsync()
        {
            int paket = await SistemRepo.ParamAsync("BildirisPaket", 7);
            await Ui.Is(this, "Məlumatlar yüklənir...", async _ =>
            {
                _dt = await Task.Run(TexnikaRepo.ZavodIcmalAsync);
                grid.Rows.Clear(); long c = 0, t = 0, g = 0; int kohne = 0;
                foreach (System.Data.DataRow r in _dt.Rows)
                {
                    long ci = Convert.ToInt64(r["Cemi"]), ti = Convert.ToInt64(r["Temirde"]), hi = Convert.ToInt64(r["HisseGozleyir"]), gi = Convert.ToInt64(r["Geciken"]);
                    double faiz = ti + hi > 0 ? Math.Round(gi * 100.0 / (ti + hi)) : 0;
                    string son = r["SonPaket"] is long sp ? Vaxt.Yaz(sp) : "—";
                    int gun = r["SonPaket"] is long sp2 ? (int)(DateTime.Now - DateTimeOffset.FromUnixTimeMilliseconds(sp2).LocalDateTime).TotalDays : -1;
                    if (gun >= paket) { son += $" · {gun} gün"; kohne++; }
                    int i = grid.Rows.Add(Lang.T((string)r["Zavod"]), ci, ti, hi, gi, faiz, r["OrtaGun"] is DBNull ? "—" : r["OrtaGun"], son);
                    grid.Rows[i].Tag = (string)r["Zavod"]; grid.Rows[i].Cells[7].Tag = gun >= paket;
                    c += ci; t += ti; g += gi;
                }
                cardTotal.Value = c.ToString("#,0").Replace(",", " "); cardRepair.Value = t.ToString("#,0").Replace(",", " ");
                cardLate.Value = g.ToString("#,0").Replace(",", " "); cardStale.Value = $"{kohne}";
                cardStale.Title = $"{paket}+ gün paket yoxdur";
            });
            lblAsOf.Text = $"{Lang.T("Son idxala görə")} · {DateTime.Now:dd.MM.yyyy}";
        }

        // "Gecikmə faizi" sütununda rəngli zolaq
        private void grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || grid.Columns[e.ColumnIndex].Name != "Faiz" || e.Value is not double f) return;
            e.PaintBackground(e.CellBounds, true);
            var r = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + e.CellBounds.Height / 2 - 4, e.CellBounds.Width - 70, 8);
            using (var b = new SolidBrush(Theme.Alt)) e.Graphics.FillRectangle(b, r);
            using (var b = new SolidBrush(f >= 20 ? Theme.Bad : f >= 10 ? Theme.Warn : Theme.Ok)) e.Graphics.FillRectangle(b, r.X, r.Y, (int)(r.Width * Math.Min(100, f) / 100), r.Height);
            TextRenderer.DrawText(e.Graphics, $"{f}%", grid.Font, new Rectangle(r.Right + 6, e.CellBounds.Y, 50, e.CellBounds.Height), Theme.Ink, TextFormatFlags.VerticalCenter);
            e.Handled = true;
        }

        private void grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == 4 && e.Value is long n && n > 0) e.CellStyle.ForeColor = Theme.Bad;
            if (e.ColumnIndex == 7 && grid.Rows[e.RowIndex].Cells[7].Tag is true) { e.CellStyle.ForeColor = Theme.Bad; e.CellStyle.Font = new Font(grid.Font, FontStyle.Bold); }
            if (e.ColumnIndex == 0) e.CellStyle.Font = new Font(grid.Font, FontStyle.Bold);
        }

        private async void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && grid.Rows[e.RowIndex].Tag is string z) await MainForm.Current.ReyestrAcAsync(new ReyestrFiltr { Zavod = z });
        }

        private async void card_Click(object sender, EventArgs e)
        {
            var c = (StatCard)sender; if (c == cardStale) return;
            await MainForm.Current.ReyestrAcAsync(new ReyestrFiltr { Status = c.FilterKey is "" or "late" ? null : c.FilterKey, Gecikenler = c.FilterKey == "late" });
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using var f = new ExcelExportForm(new ReyestrFiltr(), 0, null);
            f.ShowDialog(this);
        }
    }
}
