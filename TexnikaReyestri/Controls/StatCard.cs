using System.ComponentModel;
using TexnikaReyestri.Core;

namespace TexnikaReyestri.Controls
{
    public enum AccentKind { Nav, Info, Ok, Warn, Bad }

    /// <summary>Statistika kartı: ikon, başlıq, böyük rəqəm və alt yazı. Click hadisəsi ilə filtr açmaq olar.</summary>
    [ToolboxItem(true), DefaultEvent("Click"), Description("Statistika kartı")]
    public class StatCard : Control, IThemed, ILocalizable
    {
        private string _title = "Cəmi texnika", _value = "0", _sub = "", _glyph = Icons.List; private AccentKind _kind = AccentKind.Nav; private bool _hover;
        private Color? _subColor;

        public StatCard()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            Size = new Size(260, 84); Cursor = Cursors.Hand; BackColor = Color.Transparent;
        }

        [Category("Məzmun")] public string Title { get => _title; set { _title = value; Invalidate(); } }
        [Category("Məzmun")] public string Value { get => _value; set { _value = value; Invalidate(); } }
        [Category("Məzmun")] public string SubText { get => _sub; set { _sub = value; Invalidate(); } }
        [Category("Görünüş")] public string Glyph { get => _glyph; set { _glyph = value; Invalidate(); } }
        [Category("Görünüş"), DefaultValue(AccentKind.Nav)] public AccentKind Accent { get => _kind; set { _kind = value; Invalidate(); } }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color? SubColor { get => _subColor; set { _subColor = value; Invalidate(); } }
        [Category("Davranış"), Description("Klikləyəndə reyestrin açılacağı filtr")] public string FilterKey { get; set; } = "";

        public static Color AccentColor(AccentKind k) => k switch
        { AccentKind.Info => Theme.Info, AccentKind.Ok => Theme.Ok, AccentKind.Warn => Theme.Warn, AccentKind.Bad => Theme.Bad, _ => Theme.Nav };

        protected override void OnMouseEnter(EventArgs e) { _hover = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _hover = false; Invalidate(); base.OnMouseLeave(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            using (var pb = new SolidBrush(Gfx.BackOf(this))) g.FillRectangle(pb, ClientRectangle);
            Gfx.Smooth(g);
            var r = new RectangleF(3, 2, Width - 7, Height - 7);
            Gfx.Shadow(g, r, 10, Theme.Shadow, 5);
            using (var p = Gfx.Round(r, 10)) { using var b = new SolidBrush(Theme.Card); g.FillPath(b, p); using var pen = new Pen(_hover ? Theme.Nav : Theme.Line, _hover ? 1.6f : 1f); g.DrawPath(pen, p); }
            var ib = new RectangleF(r.X + 14, r.Y + (r.Height - 40) / 2, 40, 40);
            using (var p = Gfx.Round(ib, 10)) using (var b = new SolidBrush(AccentColor(_kind))) g.FillPath(b, p);
            TextRenderer.DrawText(g, _glyph, Icons.Font(14f), Rectangle.Round(ib), Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            int x = (int)ib.Right + 12, top = (int)r.Y + 12, w = (int)r.Right - x - 8;
            using var ft = new Font("Segoe UI", 7.5f, FontStyle.Bold);
            using var fv = new Font("Bahnschrift SemiBold", 19f);
            using var fs = new Font("Segoe UI", 7.5f);
            var tf = TextFormatFlags.NoPrefix | TextFormatFlags.EndEllipsis | TextFormatFlags.SingleLine;
            TextRenderer.DrawText(g, Lang.T(_title).ToUpper(), ft, new Rectangle(x, top, w, 16), Theme.Muted, tf);
            TextRenderer.DrawText(g, Lang.T(_value), fv, new Rectangle(x - 2, top + 13, w, 32), Theme.Title, tf);
            if (_sub.Length > 0) TextRenderer.DrawText(g, Lang.T(_sub), fs, new Rectangle(x, top + 45, w, 16), _subColor ?? Theme.Muted, tf);
        }

        public void OnThemeChanged() => Invalidate();
        public void OnLanguageChanged() => Invalidate();
    }
}
