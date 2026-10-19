using System.ComponentModel;
using TexnikaReyestri.Core;

namespace TexnikaReyestri.Controls
{
    /// <summary>
    /// Temaya uyğun cədvəl. Skrol zolağı gizlidir (siçan təkəri ilə sürüşür), titrəmə yoxdur (DoubleBuffered).
    /// Adı "Status" olan sütunda nişan, DataGridViewButtonColumn-da yumru düymə çəkilir.
    /// Böyük siyahılar üçün VirtualMode = true və CellValueNeeded istifadə olunur.
    /// </summary>
    [ToolboxItem(true), Description("Temalı cədvəl (gizli skrol, status nişanları)")]
    public class ModernGrid : DataGridView, IThemed
    {
        public ModernGrid()
        {
            DoubleBuffered = true;
            BorderStyle = BorderStyle.None; CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None; EnableHeadersVisualStyles = false;
            RowHeadersVisible = false; AllowUserToAddRows = false; AllowUserToDeleteRows = false; AllowUserToResizeRows = false;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect; MultiSelect = false; ReadOnly = true;
            ScrollBars = ScrollBars.None;          // zolaq görünmür, sürüşdürmə təkərlədir
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;   // AllCells böyük siyahıda donma yaradır
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing; ColumnHeadersHeight = 38;
            RowTemplate.Height = 34; Font = new Font("Segoe UI", 9f);
            ApplyColors();
        }

        public void ApplyColors()
        {
            BackgroundColor = Theme.Card; GridColor = Theme.Alt;
            ColumnHeadersDefaultCellStyle.BackColor = Theme.Card; ColumnHeadersDefaultCellStyle.ForeColor = Theme.Muted;
            ColumnHeadersDefaultCellStyle.SelectionBackColor = Theme.Card; ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 8.5f);
            ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 0, 0);
            DefaultCellStyle.BackColor = Theme.Card; DefaultCellStyle.ForeColor = Theme.Ink;
            DefaultCellStyle.SelectionBackColor = Theme.Sel; DefaultCellStyle.SelectionForeColor = Theme.Ink; DefaultCellStyle.Padding = new Padding(8, 0, 0, 0);
            AlternatingRowsDefaultCellStyle.BackColor = Theme.Alt;
            Invalidate();
        }

        public void OnThemeChanged() => ApplyColors();

        // Xanadakı dəyər lüğətdə varsa (status, nəticə, zavod adı...) ekranda tərcüməsi göstərilir; dəyərin özü dəyişmir.
        // Redaktə olunan xanalara toxunulmur: tərcümə bazaya yazılmasın.
        protected override void OnCellFormatting(DataGridViewCellFormattingEventArgs e)
        {
            base.OnCellFormatting(e);
            if (Lang.Current == Language.Az || e.Value is not string s || s.Length == 0 || e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var c = this[e.ColumnIndex, e.RowIndex];
            if (!c.ReadOnly || c is DataGridViewComboBoxCell) return;
            string t = Lang.T(s);
            if (!ReferenceEquals(t, s)) { e.Value = t; e.FormattingApplied = true; }
        }

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            base.OnCellPainting(e);
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.Handled) return;
            var col = Columns[e.ColumnIndex];
            if (col.Name == "Status" && e.Value is string s && s.Length > 0)
            {
                e.PaintBackground(e.CellBounds, true);
                var f = new Font("Segoe UI Semibold", 8f);
                int w = TextRenderer.MeasureText(Lang.T(Data.Secimler.Ekran(s)), f).Width + 14;
                StatusChip.Draw(e.Graphics, new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + (e.CellBounds.Height - 22) / 2, w, 22), s, f);
                e.Handled = true;
            }
            else if (col is DataGridViewButtonColumn)
            {
                e.PaintBackground(e.CellBounds, true);
                string t = Lang.T(e.FormattedValue as string ?? (col as DataGridViewButtonColumn).Text ?? "");
                var f = new Font("Segoe UI Semibold", 8f);
                int w = TextRenderer.MeasureText(t, f).Width + 18;
                var r = new Rectangle(e.CellBounds.Right - w - 10, e.CellBounds.Y + (e.CellBounds.Height - 24) / 2, w, 24);
                Gfx.Smooth(e.Graphics);
                using (var p = Gfx.Round(r, 6)) using (var b = new SolidBrush(Theme.Nav)) e.Graphics.FillPath(b, p);
                TextRenderer.DrawText(e.Graphics, t, f, r, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                e.Handled = true;
            }
        }

        protected override void OnCellMouseEnter(DataGridViewCellEventArgs e)
        { Cursor = e.ColumnIndex >= 0 && e.RowIndex >= 0 && Columns[e.ColumnIndex] is DataGridViewButtonColumn ? Cursors.Hand : Cursors.Default; base.OnCellMouseEnter(e); }

        /// <summary>Təkərlə sürüşdürüləndə (məs. növbəti səhifəni yükləmək üçün).</summary>
        public event EventHandler WheelScrolled;

        /// <summary>Gizli skrol zolağı ilə təkər: hər addım 3 sətir.</summary>
        public void ScrollRows(int wheelDelta)
        {
            if (RowCount == 0) return;
            int first = Math.Max(0, FirstDisplayedScrollingRowIndex), maxFirst = Math.Max(0, RowCount - DisplayedRowCount(false));
            int n = Math.Clamp(first - Math.Sign(wheelDelta) * 3, 0, maxFirst);
            if (n != first) FirstDisplayedScrollingRowIndex = n;
            WheelScrolled?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnMouseWheel(MouseEventArgs e) => ScrollRows(e.Delta);
    }
}
