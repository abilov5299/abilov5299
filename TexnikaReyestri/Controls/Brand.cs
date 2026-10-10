using System.ComponentModel;
using System.Drawing.Drawing2D;
using TexnikaReyestri.Core;

namespace TexnikaReyestri.Controls
{
    /// <summary>Proqramın loqosu (qalxan + T). Ölçüsü istənilən qədər böyüdülə bilər, vektorla çəkilir.</summary>
    [ToolboxItem(true), DefaultEvent("Click"), Description("Texnika Reyestri loqosu")]
    public class LogoMark : Control
    {
        public LogoMark()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            Size = new Size(64, 64); BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e) => Draw(e.Graphics, new RectangleF(0, 0, Math.Min(Width, Height), Math.Min(Width, Height)));

        public static void Draw(Graphics g, RectangleF box)
        {
            Gfx.Smooth(g); float s = box.Width / 64f;
            using (var p = Gfx.Round(box, 14 * s)) using (var b = new SolidBrush(Theme.NavOn)) g.FillPath(b, p);
            using (var sh = Gfx.Shield(box)) using (var w = new SolidBrush(Color.White)) g.FillPath(w, sh);
            using var pen = new Pen(Theme.NavOn, 5 * s) { StartCap = LineCap.Round, EndCap = LineCap.Round };
            g.DrawLine(pen, box.X + 24 * s, box.Y + 24 * s, box.X + 40 * s, box.Y + 24 * s);
            g.DrawLine(pen, box.X + 32 * s, box.Y + 24 * s, box.X + 32 * s, box.Y + 43 * s);
            using var o = new SolidBrush(Theme.Accent); g.FillEllipse(o, box.X + 39 * s, box.Y + 40 * s, 10 * s, 10 * s);
        }
    }

    /// <summary>Giriş ekranının sol tünd paneli: şəbəkə naxışı, işıq ləkələri, böyük solğun qalxan, sağ kənarda kölgə.</summary>
    [ToolboxItem(true), Description("Brend paneli (giriş ekranı üçün)")]
    public class BrandPanel : Panel
    {
        public BrandPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            BackColor = Theme.Nav;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            var g = e.Graphics; var r = ClientRectangle;
            using (var b = new SolidBrush(Theme.Nav)) g.FillRectangle(b, r);
            using (var gp = new Pen(Color.FromArgb(13, 255, 255, 255)))
            { for (int x = 0; x < r.Width; x += 28) g.DrawLine(gp, x, 0, x, r.Height); for (int y = 0; y < r.Height; y += 28) g.DrawLine(gp, 0, y, r.Width, y); }
            Gfx.Smooth(g);
            Gfx.Glow(g, new PointF(r.Width * 0.2f, r.Height * 0.15f), r.Width * 0.7f, Color.FromArgb(70, 111, 177, 169));
            Gfx.Glow(g, new PointF(r.Width * 0.9f, r.Height * 0.95f), r.Width * 0.5f, Color.FromArgb(40, 224, 138, 0));
            float sz = r.Width * 1.05f;
            using (var sh = Gfx.Shield(new RectangleF(r.Width - sz * 0.62f, r.Height - sz * 0.72f, sz, sz)))
            using (var wb = new SolidBrush(Color.FromArgb(15, 255, 255, 255))) g.FillPath(wb, sh);
            using var edge = new LinearGradientBrush(new Rectangle(r.Width - 24, 0, 24, r.Height), Color.FromArgb(0, 0, 0, 0), Color.FromArgb(70, 0, 0, 0), LinearGradientMode.Horizontal);
            g.FillRectangle(edge, r.Width - 24, 0, 24, r.Height);
        }
    }

    /// <summary>
    /// Bütün səhifələrin (UserControl) əsas sinfi: fonda yumşaq kölgə ləkələri və solğun qalxan çəkir.
    /// Yeni səhifə: Add → User Control → sonra ": UserControl" yerinə ": PageBase" yazın.
    /// </summary>
    public class PageBase : UserControl, IThemed
    {
        public PageBase()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            BackColor = Theme.Bg; Padding = new Padding(22, 16, 22, 14);
        }

        // Fon (işıq ləkələri + qalxan) bir dəfə bitmap-ə çəkilir; uşaq panellər də ondan kəsik götürür — keçidlər sürətli olur.
        private Bitmap _bg;
        internal Bitmap Fon
        {
            get
            {
                if (Width <= 0 || Height <= 0) return null;
                if (_bg == null || _bg.Size != ClientSize)
                {
                    _bg?.Dispose(); _bg = new Bitmap(ClientSize.Width, ClientSize.Height);
                    using var g = Graphics.FromImage(_bg); Gfx.PageBackground(g, new Rectangle(Point.Empty, ClientSize));
                }
                return _bg;
            }
        }
        private void FonSil() { _bg?.Dispose(); _bg = null; }
        protected override void OnPaintBackground(PaintEventArgs e) { var b = Fon; if (b != null) e.Graphics.DrawImage(b, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel); }
        protected override void Dispose(bool disposing) { if (disposing) FonSil(); base.Dispose(disposing); }

        /// <summary>Səhifə göstəriləndə çağırılır (MainForm tərəfindən). Məlumatı burada yükləyin.</summary>
        public virtual Task OnShownAsync() => Task.CompletedTask;

        public virtual void OnThemeChanged() { FonSil(); BackColor = Theme.Bg; Invalidate(); }
    }

    /// <summary>Seçim düymələri qrupu (30 gün | 3 ay | 6 ay | 1 il). Items — mətnlər, SelectedIndex — seçilən.</summary>
    [ToolboxItem(true), DefaultEvent("SelectedIndexChanged"), Description("Seqmentli seçim")]
    public class SegmentControl : Control, IThemed, ILocalizable
    {
        private string[] _items = { "30 gün", "3 ay", "6 ay", "1 il" }; private int _sel = 2;
        public event EventHandler SelectedIndexChanged;

        public SegmentControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            Size = new Size(260, 30); Font = new Font("Segoe UI", 8.5f); Cursor = Cursors.Hand;
        }

        [Category("Məzmun")] public string[] Items { get => _items; set { _items = value ?? Array.Empty<string>(); Invalidate(); } }
        [Category("Məzmun"), DefaultValue(2)] public int SelectedIndex { get => _sel; set { if (_sel == value) return; _sel = value; Invalidate(); SelectedIndexChanged?.Invoke(this, EventArgs.Empty); } }

        protected override void OnMouseClick(MouseEventArgs e) { if (_items.Length > 0) SelectedIndex = Math.Clamp(e.X * _items.Length / Width, 0, _items.Length - 1); base.OnMouseClick(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            using (var pb = new SolidBrush(Gfx.BackOf(this))) g.FillRectangle(pb, ClientRectangle);
            Gfx.Smooth(g);
            var r = new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f);
            using var path = Gfx.Round(r, 7);
            using (var b = new SolidBrush(Theme.Card)) g.FillPath(b, path);
            float w = Width / (float)Math.Max(1, _items.Length);
            var clip = g.Save(); g.SetClip(path);
            for (int i = 0; i < _items.Length; i++)
            {
                var cr = new RectangleF(i * w, 0, w, Height);
                if (i == _sel) using (var sb = new SolidBrush(Theme.Nav)) g.FillRectangle(sb, cr);
                TextRenderer.DrawText(g, Lang.T(_items[i]), Font, Rectangle.Round(cr), i == _sel ? Color.White : Theme.Muted, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
            g.Restore(clip);
            using (var pen = new Pen(Theme.Line)) g.DrawPath(pen, path);
        }

        public void OnThemeChanged() => Invalidate();
        public void OnLanguageChanged() => Invalidate();
    }

    /// <summary>
    /// Gizli skrol zolaqlı panel: içindəki kontrollar siçan təkəri ilə sürüşür, zolaq görünmür.
    /// Kontrolları bu panelin içinə atın; məzmun paneldən uzun olanda avtomatik sürüşür.
    /// </summary>
    [ToolboxItem(true), Description("Zolaqsız sürüşən panel")]
    public class ScrollPanel : Panel
    {
        private int _offset;
        public ScrollPanel() { DoubleBuffered = true; AutoScroll = false; BackColor = Color.Transparent; }

        public int Offset => _offset;
        public int ContentHeight => Controls.Count == 0 ? 0 : Controls.Cast<Control>().Max(c => c.Bottom + _offset) + Padding.Bottom;

        public void ScrollBy(int dy)
        {
            int max = Math.Max(0, ContentHeight - ClientSize.Height);
            int n = Math.Clamp(_offset + dy, 0, max), d = n - _offset;
            if (d == 0) return;
            _offset = n;
            // Windows-un öz sürüşdürməsi: görünən hissə bir dəfə köçürülür, yalnız açılan zolaq yenidən çəkilir (donma və qırıq şəkil olmur)
            ScrollWindowEx(Handle, 0, -d, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, 0x0001 | 0x0002 | 0x0004); // SCROLLCHILDREN|INVALIDATE|ERASE
            Update();
        }

        public void ScrollToTop() => ScrollBy(-_offset);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int ScrollWindowEx(IntPtr h, int dx, int dy, IntPtr scroll, IntPtr clip, IntPtr rgn, IntPtr upd, int flags);
        protected override void OnMouseWheel(MouseEventArgs e) { ScrollBy(-e.Delta / 120 * 48); base.OnMouseWheel(e); }
    }
}
