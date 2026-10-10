using System.ComponentModel;
using TexnikaReyestri.Core;

namespace TexnikaReyestri.Controls
{
    public enum ButtonKind { Primary, Secondary, Danger, Ghost }

    /// <summary>Düz düymə: əsas (tünd firuzəyi), ikinci dərəcəli (boz), təhlükəli (qırmızı), şəffaf.</summary>
    [ToolboxItem(true), DefaultEvent("Click"), Description("Yumru künclü düz düymə, ikon ilə")]
    public class FlatButton : Button, IThemed, ILocalizable
    {
        private ButtonKind _kind = ButtonKind.Primary; private string _glyph = ""; private bool _hover, _down; private int _radius = 8;

        public FlatButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            FlatStyle = FlatStyle.Flat; FlatAppearance.BorderSize = 0; Cursor = Cursors.Hand;
            Font = new Font("Segoe UI Semibold", 9f); Size = new Size(120, 34); BackColor = Color.Transparent;
        }

        [Category("Görünüş"), DefaultValue(ButtonKind.Primary)] public ButtonKind Kind { get => _kind; set { _kind = value; Invalidate(); } }
        [Category("Görünüş"), DefaultValue(""), Description("Icons sinfindən simvol, məs. \\uE74E (Yadda saxla)")]
        public string Glyph { get => _glyph; set { _glyph = value ?? ""; Invalidate(); } }
        [Category("Görünüş"), DefaultValue(8)] public int CornerRadius { get => _radius; set { _radius = value; Invalidate(); } }

        protected override void OnMouseEnter(EventArgs e) { _hover = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _hover = false; _down = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnMouseDown(MouseEventArgs e) { _down = true; Invalidate(); base.OnMouseDown(e); }
        protected override void OnMouseUp(MouseEventArgs e) { _down = false; Invalidate(); base.OnMouseUp(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            using (var pb = new SolidBrush(Parent?.BackColor is { A: 255 } pc ? pc : Theme.Card)) g.FillRectangle(pb, ClientRectangle);
            if (Parent is RoundPanel rp) using (var b2 = new SolidBrush(rp.FillColor)) g.FillRectangle(b2, ClientRectangle);
            Gfx.Smooth(g);
            (Color back, Color fore) = _kind switch
            {
                ButtonKind.Secondary => (Theme.Line, Theme.Title),
                ButtonKind.Danger => (Theme.Bad, Color.White),
                ButtonKind.Ghost => (Color.Transparent, Theme.Title),
                _ => (Theme.Nav, Color.White)
            };
            if (!Enabled) { back = Theme.Mix(back, Theme.Card, 0.55f); fore = Theme.Mix(fore, Theme.Card, 0.45f); }
            else if (_down) back = Theme.Mix(back, Color.Black, 0.15f);
            else if (_hover) back = _kind == ButtonKind.Ghost ? Theme.Alt : Theme.Mix(back, Color.White, 0.12f);
            using (var path = Gfx.Round(new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f), _radius))
            using (var b = new SolidBrush(back)) g.FillPath(b, path);

            string text = Text; var tf = TextFormatFlags.VerticalCenter | TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine;
            int gw = 0; Font gf = null;
            if (_glyph.Length > 0) { gf = Icons.Font(Font.Size + 1); gw = TextRenderer.MeasureText(_glyph, gf).Width - 4; }
            int tw = string.IsNullOrEmpty(text) ? 0 : TextRenderer.MeasureText(text, Font).Width;
            int total = gw + (gw > 0 && tw > 0 ? 2 : 0) + tw, x = (Width - total) / 2;
            if (gw > 0) TextRenderer.DrawText(g, _glyph, gf, new Rectangle(x, 0, gw + 4, Height), fore, tf);
            if (tw > 0) TextRenderer.DrawText(g, text, Font, new Rectangle(x + gw + (gw > 0 ? 2 : 0), 0, tw + 4, Height), fore, tf);
            if (Focused && ShowFocusCues) using (var fp = new Pen(Theme.Accent, 2)) { using var p2 = Gfx.Round(new RectangleF(1, 1, Width - 3, Height - 3), _radius); g.DrawPath(fp, p2); }
        }

        public void OnThemeChanged() => Invalidate();
        public void OnLanguageChanged() => Invalidate();
    }
}
