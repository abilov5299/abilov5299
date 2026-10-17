using System.ComponentModel;
using TexnikaReyestri.Core;

namespace TexnikaReyestri.Controls
{
    /// <summary>Sol ikon menyusunun düyməsi: aktiv vəziyyət xətti, bildiriş nişanı (Badge), ipucu (ToolTipText).</summary>
    [ToolboxItem(true), DefaultEvent("Click"), Description("Sol menyu ikon düyməsi")]
    public class NavButton : Control, IThemed
    {
        private string _glyph = Icons.Home; private bool _active, _hover; private int _badge; private bool _danger;

        public NavButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.Selectable | ControlStyles.ResizeRedraw, true);   // menyu açılanda ad tam yenidən çəkilsin
            Size = new Size(64, 52); Cursor = Cursors.Hand; TabStop = true;
        }

        [Category("Görünüş")] public string Glyph { get => _glyph; set { _glyph = value; Invalidate(); } }
        [Category("Davranış"), DefaultValue(false)] public bool Active { get => _active; set { _active = value; Invalidate(); } }
        [Category("Davranış"), DefaultValue(0)] public int Badge { get => _badge; set { _badge = value; Invalidate(); } }
        [Category("Görünüş"), DefaultValue(false), Description("Çıxış düyməsi üçün qırmızımtıl ikon")] public bool DangerStyle { get => _danger; set { _danger = value; Invalidate(); } }
        [Category("Davranış"), Description("Siçan üstünə gələndə görünən ad")] public string ToolTipText { get; set; } = "";
        [Category("Davranış"), Description("Hansı səhifəni açır (MainForm-da istifadə olunur)")] public string PageKey { get; set; } = "";

        private static readonly ToolTip Tip = new() { InitialDelay = 250 };
        protected override void OnMouseEnter(EventArgs e) { _hover = true; Invalidate(); Tip.SetToolTip(this, Width > 100 ? null : Lang.T(ToolTipText)); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _hover = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnKeyDown(KeyEventArgs e) { if (e.KeyCode is Keys.Enter or Keys.Space) OnClick(EventArgs.Empty); base.OnKeyDown(e); }
        protected override void OnGotFocus(EventArgs e) { Invalidate(); base.OnGotFocus(e); }
        protected override void OnLostFocus(EventArgs e) { Invalidate(); base.OnLostFocus(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics; Gfx.Smooth(g);
            using (var bg = new SolidBrush(Theme.Nav)) g.FillRectangle(bg, ClientRectangle);
            int s = 44; bool genis = Width > 100;   // menyu açıqdır: ikonun yanında ad
            var box = genis ? new RectangleF(10, (Height - s) / 2f, s, s) : new RectangleF((Width - s) / 2f, (Height - s) / 2f, s, s);
            var fon = genis ? new RectangleF(10, box.Y, Width - 20, s) : box;
            if (_active || _hover) using (var p = Gfx.Round(fon, 10)) using (var b = new SolidBrush(Theme.NavOn)) g.FillPath(b, p);
            if (_active) using (var b = new SolidBrush(Theme.Accent)) g.FillRectangle(b, 0, box.Y + 10, 4, s - 20);
            var fore = _danger ? Theme.C("#F0A8A3") : (_active || _hover ? Color.White : Theme.NavText);
            TextRenderer.DrawText(g, _glyph, Icons.Font(15f), Rectangle.Round(box), fore, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            if (genis) { using var tf = new Font("Segoe UI", 10f, _active ? FontStyle.Bold : FontStyle.Regular);
                TextRenderer.DrawText(g, Lang.T(ToolTipText), tf, new Rectangle((int)box.Right + 4, (int)box.Y, Width - (int)box.Right - 14, s), fore, TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis); }
            if (_badge > 0)
            {
                string t = _badge > 99 ? "99+" : _badge.ToString();
                using var f = new Font("Segoe UI", 7.5f, FontStyle.Bold);
                int w = Math.Max(16, TextRenderer.MeasureText(t, f).Width - 2);
                var br = new Rectangle((int)box.Right - w + 4, (int)box.Y - 2, w, 16);
                using (var p = Gfx.Round(br, 8)) using (var b = new SolidBrush(Theme.Bad)) g.FillPath(b, p);
                TextRenderer.DrawText(g, t, f, br, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
            if (Focused && ShowFocusCues) using (var pen = new Pen(Theme.Accent, 2)) { using var p = Gfx.Round(box, 10); g.DrawPath(pen, p); }
        }

        public void OnThemeChanged() => Invalidate();
    }
}
