using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Pages;

namespace TexnikaReyestri.Forms
{
    /// <summary>Başlıqdakı zəng düyməsinin açılan siyahısı (son 8 bildiriş).</summary>
    public partial class NotificationPopup : Form
    {
        private List<Bildiris> _l = new();
        public NotificationPopup() { InitializeComponent(); grid.RowTemplate.Height = 56; }

        private async void NotificationPopup_Load(object sender, EventArgs e)
        {
            Ui.Hazirla(this); BackColor = Theme.Line;
            _l = await SistemRepo.BildirislerAsync(limit: 8);
            foreach (var b in _l) grid.Rows.Add(b.Basliq);
            if (_l.Count == 0) grid.Rows.Add(Lang.T("Bildiriş yoxdur"));
        }

        private void grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _l.Count) return;
            var b = _l[e.RowIndex]; e.PaintBackground(e.CellBounds, true);
            var r = e.CellBounds; var dot = new Rectangle(r.X + 10, r.Y + 12, 10, 10);
            using (var br = new SolidBrush(b.Oxunub ? Theme.Line : NotificationsPage.NovRengi(b.Nov))) e.Graphics.FillEllipse(br, dot);
            using var fb = new Font("Segoe UI Semibold", 9f); using var fs = new Font("Segoe UI", 8f);
            TextRenderer.DrawText(e.Graphics, Lang.T(b.Basliq), fb, new Rectangle(r.X + 28, r.Y + 6, r.Width - 110, 18), Theme.Ink, TextFormatFlags.EndEllipsis);
            TextRenderer.DrawText(e.Graphics, Vaxt.Yaz(b.Tarix), fs, new Rectangle(r.Right - 100, r.Y + 7, 94, 16), Theme.Muted, TextFormatFlags.Right);
            TextRenderer.DrawText(e.Graphics, b.Metn, fs, new Rectangle(r.X + 28, r.Y + 26, r.Width - 36, 28), Theme.Muted, TextFormatFlags.WordBreak | TextFormatFlags.EndEllipsis);
            e.Handled = true;
        }

        private async void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _l.Count) return;
            var b = _l[e.RowIndex]; await SistemRepo.OxunduAsync(b.Id); Close();
            await MainForm.Current.BadgeYenileAsync();
            if (b.TexnikaId is long id) await MainForm.Current.DetalAcAsync(id); else await MainForm.Current.AcAsync("noti");
        }

        private async void lnkReadAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) { await SistemRepo.OxunduAsync(null); await MainForm.Current.BadgeYenileAsync(); Close(); }
        private async void lnkAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) { Close(); await MainForm.Current.AcAsync("noti"); }
        private void NotificationPopup_Deactivate(object sender, EventArgs e) => Close();
    }
}
