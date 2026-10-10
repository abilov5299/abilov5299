using System.ComponentModel;
using TexnikaReyestri.Core;

namespace TexnikaReyestri.Controls
{
    public enum PanelFill { Card, Alt, Nav, Transparent }

    /// <summary>Yumru künclü, kölgəli kart. İçinə istənilən kontrolu atmaq olar.</summary>
    [ToolboxItem(true), Description("Yumru künclü kölgəli kart")]
    public class RoundPanel : Panel, IThemed
    {
        private int _radius = 10; private bool _shadow = true; private PanelFill _fill = PanelFill.Card; private bool _border = true;

        public RoundPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            Padding = new Padding(14, 12, 14, 12);
            BackColor = Color.Transparent;
        }

        [Category("Görünüş"), DefaultValue(10)] public int CornerRadius { get => _radius; set { _radius = value; Invalidate(); } }
        [Category("Görünüş"), DefaultValue(true)] public bool ShadowVisible { get => _shadow; set { _shadow = value; Invalidate(); } }
        [Category("Görünüş"), DefaultValue(true)] public bool BorderVisible { get => _border; set { _border = value; Invalidate(); } }
        [Category("Görünüş"), DefaultValue(PanelFill.Card)] public PanelFill Fill { get => _fill; set { _fill = value; Invalidate(); } }

        public Color FillColor => _fill switch { PanelFill.Alt => Theme.Alt, PanelFill.Nav => Theme.Nav, PanelFill.Transparent => Color.Transparent, _ => Theme.Card };

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Valideynin fonu (kölgə üçün şəffaf görünüş)
            if (Parent != null)
            {
                var st = e.Graphics.Save();
                e.Graphics.TranslateTransform(-Left, -Top);
                using (var pe = new PaintEventArgs(e.Graphics, new Rectangle(Left, Top, Width, Height)))
                { InvokePaintBackground(Parent, pe); InvokePaint(Parent, pe); }
                e.Graphics.Restore(st);
            }
            var g = e.Graphics; Gfx.Smooth(g);
            var r = new RectangleF(3, 2, Width - 7, Height - 7);
            if (_shadow && _fill != PanelFill.Transparent) Gfx.Shadow(g, r, _radius, Theme.Shadow, 5);
            using var path = Gfx.Round(r, _radius);
            using (var b = new SolidBrush(FillColor)) g.FillPath(b, path);
            if (_border && _fill == PanelFill.Card) using (var pen = new Pen(Theme.Line)) g.DrawPath(pen, path);
        }

        public void OnThemeChanged() => Invalidate(true);
    }
}
