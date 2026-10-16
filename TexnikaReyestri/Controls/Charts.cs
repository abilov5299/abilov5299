using System.ComponentModel;
using TexnikaReyestri.Core;

namespace TexnikaReyestri.Controls
{
    public enum ChartColor { Chart1, Chart2 }

    /// <summary>Üfüqi zolaqlı siyahı (Texnika növü üzrə, zavodlar üzrə...). SetItems ilə doldurulur.</summary>
    [ToolboxItem(true), DefaultEvent("ItemClicked"), Description("Üfüqi zolaq diaqramı")]
    public class BarList : Control, IThemed, ILocalizable
    {
        private (string label, double value)[] _items = { ("Əsas döyüş tankı", 41), ("Piyadaların döyüş maşını", 33), ("Zirehli transportyor", 27) };
        private int _hover = -1;
        public event EventHandler<string> ItemClicked;

        public BarList()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            Size = new Size(320, 120); Font = new Font("Segoe UI", 8.5f);
        }

        [Category("Görünüş"), DefaultValue(ChartColor.Chart1)] public ChartColor BarColor { get; set; } = ChartColor.Chart1;
        [Category("Görünüş"), DefaultValue(150)] public int LabelWidth { get; set; } = 150;
        [Category("Görünüş"), DefaultValue(22)] public int RowHeight { get; set; } = 22;

        private int RH => _items.Length == 0 ? RowHeight : Math.Max(14, Math.Min(RowHeight, Height / _items.Length));
        public void SetItems(IEnumerable<(string, double)> items) { _items = items.ToArray(); Invalidate(); }

        protected override void OnMouseMove(MouseEventArgs e) { int h = e.Y / RH; if (h >= _items.Length) h = -1; if (h != _hover) { _hover = h; Cursor = h >= 0 && ItemClicked != null ? Cursors.Hand : Cursors.Default; Invalidate(); } base.OnMouseMove(e); }
        protected override void OnMouseLeave(EventArgs e) { _hover = -1; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnMouseClick(MouseEventArgs e) { int h = e.Y / RH; if (h >= 0 && h < _items.Length) ItemClicked?.Invoke(this, _items[h].label); base.OnMouseClick(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics; var bc = Parent is RoundPanel rp ? rp.FillColor : Theme.Card;
            using (var bb = new SolidBrush(bc)) g.FillRectangle(bb, ClientRectangle);
            Gfx.Smooth(g);
            double max = _items.Length == 0 ? 1 : Math.Max(1, _items.Max(i => i.value));
            var col = BarColor == ChartColor.Chart1 ? Theme.Chart1 : Theme.Chart2;
            int vw = 48, tw = Width - LabelWidth - vw - 10;
            using var bf = new Font(Font, FontStyle.Bold);
            int rh = RH; float bh = Math.Min(12, rh - 6);
            for (int i = 0; i < _items.Length; i++)
            {
                int y = i * rh; var (l, v) = _items[i];
                TextRenderer.DrawText(g, Lang.T(l), Font, new Rectangle(0, y, LabelWidth - 6, rh), Theme.Ink, TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
                var track = new RectangleF(LabelWidth, y + rh / 2f - bh / 2f, tw, bh);
                using (var tb = new SolidBrush(Theme.Alt)) g.FillRectangle(tb, track);
                var bar = new RectangleF(track.X, track.Y, (float)(tw * v / max), bh);
                using (var b = new SolidBrush(i == _hover ? Theme.Mix(col, Color.White, 0.2f) : col)) { using var p = Gfx.Round(bar, 3); g.FillPath(b, p); }
                TextRenderer.DrawText(g, v.ToString("#,0").Replace(",", " "), bf, new Rectangle(Width - vw, y, vw, rh), Theme.Ink, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        public void OnThemeChanged() => Invalidate();
        public void OnLanguageChanged() => Invalidate();
    }

    /// <summary>Aylar üzrə iki seriyalı sütun diaqramı (Qəbul / Təhvil).</summary>
    [ToolboxItem(true), Description("İki seriyalı sütun diaqramı")]
    public class ColumnChart : Control, IThemed, ILocalizable
    {
        private string[] _labels = { "May", "İyn", "İyl", "Avq", "Sen", "Okt" };
        private double[] _a = { 12, 9, 13, 11, 12, 4 }, _b = { 10, 11, 9, 12, 10, 3 };
        private readonly ToolTip _tip = new();
        private int _hover = -1;

        public ColumnChart()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            Size = new Size(600, 160); Font = new Font("Segoe UI", 8f);
        }

        [Category("Məzmun")] public string SeriesAName { get; set; } = "qəbul";
        [Category("Məzmun")] public string SeriesBName { get; set; } = "təhvil";

        public void SetData(string[] labels, double[] a, double[] b) { _labels = labels; _a = a; _b = b; Invalidate(); }

        private int PL = 30; private const int PB = 20, PT = 8;   // PL: sol boşluq, ən uzun ox rəqəminə görə
        protected override void OnMouseMove(MouseEventArgs e)
        {
            int n = _labels.Length; if (n == 0) return;
            float cw = (Width - PL) / (float)n; int i = (int)((e.X - PL) / cw);
            if (e.X < PL || i >= n) i = -1;
            if (i != _hover) { _hover = i; Invalidate(); _tip.SetToolTip(this, i < 0 ? "" : $"{Lang.T(_labels[i])}: {_a[i]} {Lang.T(SeriesAName)} · {_b[i]} {Lang.T(SeriesBName)}"); }
            base.OnMouseMove(e);
        }
        protected override void OnMouseLeave(EventArgs e) { _hover = -1; Invalidate(); base.OnMouseLeave(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics; var bc = Parent is RoundPanel rp ? rp.FillColor : Theme.Card;
            using (var bb = new SolidBrush(bc)) g.FillRectangle(bb, ClientRectangle);
            Gfx.Smooth(g);
            int n = _labels.Length; if (n == 0) return;
            double mx = Math.Max(1, Math.Max(_a.DefaultIfEmpty(0).Max(), _b.DefaultIfEmpty(0).Max()));
            // 4 bərabər, tam ədədli addım (1, 2, 5 × 10ⁿ) — oxda təkrarlanan rəqəm olmasın
            double raw = Math.Max(1, mx / 4), p10 = Math.Pow(10, Math.Floor(Math.Log10(raw))), st = new[] { 1, 2, 5, 10 }.Select(m => m * p10).First(x => x >= raw - 1e-9);
            double max = Math.Max(1, Math.Round(st)) * 4;
            PL = Math.Max(30, TextRenderer.MeasureText(max.ToString("0"), Font).Width + 8);
            float H = Height - PB - PT; float Y(double v) => PT + H * (float)(1 - v / max);
            using var gp = new Pen(Theme.Line);
            for (int k = 0; k <= 4; k++)
            {
                double v = max * k / 4; float y = Y(v);
                g.DrawLine(gp, PL, y, Width, y);
                TextRenderer.DrawText(g, Math.Round(v).ToString(), Font, new Rectangle(0, (int)y - 8, PL - 6, 16), Theme.Muted, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
            float cw = (Width - PL) / (float)n, bw = Math.Min(18, cw / 3);
            for (int i = 0; i < n; i++)
            {
                float cx = PL + cw * i + cw / 2;
                if (i == _hover) using (var hb = new SolidBrush(Theme.Alt)) g.FillRectangle(hb, cx - cw / 2 + 4, PT, cw - 8, H);
                Bar(g, cx - bw - 1, bw, Y(_a[i]), H + PT, Theme.Chart1);
                Bar(g, cx + 1, bw, Y(_b[i]), H + PT, Theme.Chart2);
                TextRenderer.DrawText(g, Lang.T(_labels[i]), Font, new Rectangle((int)(cx - cw / 2), Height - PB + 2, (int)cw, PB), Theme.Muted, TextFormatFlags.HorizontalCenter);
            }
        }

        private static void Bar(Graphics g, float x, float w, float top, float bottom, Color c)
        {
            if (bottom - top < 1) return;
            using var p = Gfx.Round(new RectangleF(x, top, w, bottom - top + 4), 4);   // yalnız yuxarı künclər görünür
            var st = g.Save(); g.SetClip(new RectangleF(x - 1, top - 1, w + 2, bottom - top + 1));
            using var b = new SolidBrush(c); g.FillPath(b, p); g.Restore(st);
        }

        public void OnThemeChanged() => Invalidate();
        public void OnLanguageChanged() => Invalidate();
    }

    /// <summary>Status bölgüsü: üfüqi bölünmüş zolaq + əfsanə (say və faiz).</summary>
    [ToolboxItem(true), DefaultEvent("StatusClicked"), Description("Status bölgüsü zolağı")]
    public class StatusBreakdown : Control, IThemed, ILocalizable
    {
        private (string status, long count)[] _items = { ("Qəbul edilib", 9), ("Təmirdə", 46), ("Hissə gözləyir", 17), ("Təhvil verilib", 46), ("Təmirə yararsız", 6) };
        public event EventHandler<string> StatusClicked;

        public StatusBreakdown()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            Size = new Size(340, 140); Font = new Font("Segoe UI", 8.5f); Cursor = Cursors.Hand;
        }

        public void SetItems(IEnumerable<(string, long)> items) { _items = items.ToArray(); Invalidate(); }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            int row = (e.Y - 32) / 20;
            if (e.Y >= 32 && row >= 0 && row < _items.Length) StatusClicked?.Invoke(this, _items[row].status);
            else if (e.Y < 24) { long tot = Math.Max(1, _items.Sum(i => i.count)); float x = 0; foreach (var it in _items) { float w = Width * it.count / (float)tot; if (e.X >= x && e.X < x + w) { StatusClicked?.Invoke(this, it.status); break; } x += w; } }
            base.OnMouseClick(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics; var bc = Parent is RoundPanel rp ? rp.FillColor : Theme.Card;
            using (var bb = new SolidBrush(bc)) g.FillRectangle(bb, ClientRectangle);
            long tot = Math.Max(1, _items.Sum(i => i.count)); float x = 0;
            foreach (var it in _items)
            {
                float w = Width * it.count / (float)tot;
                using var b = new SolidBrush(Theme.StatusBar(it.status)); g.FillRectangle(b, x, 0, Math.Max(0, w - 2), 22); x += w;
            }
            using var bf = new Font(Font, FontStyle.Bold);
            for (int i = 0; i < _items.Length; i++)
            {
                int y = 32 + i * 20; var (s, c) = _items[i];
                using (var b = new SolidBrush(Theme.StatusBar(s))) g.FillRectangle(b, 0, y + 5, 10, 10);
                TextRenderer.DrawText(g, Lang.T(Data.Secimler.Ekran(s)), Font, new Rectangle(18, y, Width - 120, 20), Theme.Ink, TextFormatFlags.VerticalCenter | TextFormatFlags.NoPrefix);
                TextRenderer.DrawText(g, c.ToString("#,0").Replace(",", " "), bf, new Rectangle(Width - 110, y, 60, 20), Theme.Ink, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
                TextRenderer.DrawText(g, $"{Math.Round(c * 100.0 / tot)}%", Font, new Rectangle(Width - 46, y, 46, 20), Theme.Muted, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            }
        }

        public void OnThemeChanged() => Invalidate();
        public void OnLanguageChanged() => Invalidate();
    }
}
