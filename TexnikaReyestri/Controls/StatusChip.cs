using System.ComponentModel;
using TexnikaReyestri.Core;

namespace TexnikaReyestri.Controls
{
    /// <summary>Status nişanı ("Təmirdə", "Təhvil verilib"...). Rəng mətnə görə avtomatik seçilir.</summary>
    [ToolboxItem(true), Description("Rəngli status nişanı")]
    public class StatusChip : Control, IThemed, ILocalizable
    {
        public StatusChip()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            Font = new Font("Segoe UI Semibold", 8f); Size = new Size(90, 22); Text = "Təmirdə"; BackColor = Color.Transparent;
        }

        protected override void OnTextChanged(EventArgs e) { Width = Math.Max(30, TextRenderer.MeasureText(Lang.T(Text), Font).Width + 14); Invalidate(); base.OnTextChanged(e); }

        public static void Draw(Graphics g, Rectangle r, string status, Font f)
        {
            var (back, fore) = Theme.StatusColors(status);
            Gfx.Smooth(g);
            using (var p = Gfx.Round(r, r.Height / 2f)) using (var b = new SolidBrush(back)) g.FillPath(b, p);
            TextRenderer.DrawText(g, Lang.T(status), f, r, fore, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPrefix);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var bc = Parent is RoundPanel rp ? rp.FillColor : (Parent?.BackColor is { A: 255 } c ? c : Theme.Card);
            using (var bb = new SolidBrush(bc)) e.Graphics.FillRectangle(bb, ClientRectangle);
            Draw(e.Graphics, new Rectangle(0, 0, Width - 1, Height - 1), Text, Font);
        }

        public void OnThemeChanged() => Invalidate();
        public void OnLanguageChanged() { OnTextChanged(EventArgs.Empty); }
    }
}
