using System.ComponentModel;
using TexnikaReyestri.Core;

namespace TexnikaReyestri.Controls
{
    /// <summary>Başlıqsız TabControl: işləyəndə yuxarı tab zolağı gizlənir (Designer-də görünür ki, səhifələri redaktə edə biləsiniz). Keçidləri ChipTabBar edir.</summary>
    [ToolboxItem(true), Description("Başlıqsız səhifə qrupu (ChipTabBar ilə birlikdə)")]
    public class PageTabs : TabControl
    {
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x1328 && !DesignMode) { m.Result = (IntPtr)1; return; }   // TCM_ADJUSTRECT: başlıq yeri = 0
            base.WndProc(ref m);
        }
    }

    /// <summary>Bütün qrupları sətirlərə bölünən "çip" düymələri kimi göstərir (prototipdəki kimi). Target — idarə etdiyi TabControl.</summary>
    [ToolboxItem(true), DefaultEvent("SelectedIndexChanged"), Description("Bütün qrupları görünən çip-tab zolağı")]
    public class ChipTabBar : Control, IThemed, ILocalizable
    {
        private TabControl _target; private int _hover = -1; private readonly List<Rectangle> _r = new();

        public ChipTabBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            Font = new Font("Segoe UI Semibold", 9f); Dock = DockStyle.Top; Height = 44; Padding = new Padding(0, 6, 0, 6); BackColor = Color.Transparent; Cursor = Cursors.Hand;
        }

        [Category("Davranış"), Description("Keçid edəcəyi TabControl"), DefaultValue(null)]
        public TabControl Target
        {
            get => _target;
            set
            {
                if (_target != null) { _target.SelectedIndexChanged -= TargetChanged; _target.ControlAdded -= TargetLayout; _target.ControlRemoved -= TargetLayout; }
                _target = value;
                if (_target != null) { _target.SelectedIndexChanged += TargetChanged; _target.ControlAdded += TargetLayout; _target.ControlRemoved += TargetLayout; }
                Hesabla();
            }
        }
        [Category("Görünüş"), DefaultValue(8)] public int Gap { get; set; } = 8;

        private void TargetChanged(object s, EventArgs e) => Invalidate();
        private void TargetLayout(object s, ControlEventArgs e) => Hesabla();
        protected override void OnSizeChanged(EventArgs e) { base.OnSizeChanged(e); Hesabla(); }
        protected override void OnFontChanged(EventArgs e) { base.OnFontChanged(e); Hesabla(); }

        private string Metn(int i) => Lang.T(_target.TabPages[i].Text);

        private void Hesabla()
        {
            _r.Clear();
            if (_target == null || Width <= 0) { Invalidate(); return; }
            int x = Padding.Left, y = Padding.Top, h = Font.Height + 14;
            for (int i = 0; i < _target.TabCount; i++)
            {
                int w = TextRenderer.MeasureText(Metn(i), Font).Width + 26;
                if (x > Padding.Left && x + w > Width - Padding.Right) { x = Padding.Left; y += h + Gap; }
                _r.Add(new Rectangle(x, y, w, h)); x += w + Gap;
            }
            int need = (_r.Count > 0 ? _r[^1].Bottom : y) + Padding.Bottom;
            if (Height != need && Dock is DockStyle.Top or DockStyle.Bottom or DockStyle.None) Height = need;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics; Gfx.ParentBack(this, g); Gfx.Smooth(g);
            if (_target == null) return;
            if (_r.Count != _target.TabCount) Hesabla();
            for (int i = 0; i < _r.Count; i++)
            {
                bool sel = i == _target.SelectedIndex, hov = i == _hover;
                var r = _r[i];
                using var p = Gfx.Round(new RectangleF(r.X + .5f, r.Y + .5f, r.Width - 1, r.Height - 1), r.Height / 2f);
                using (var b = new SolidBrush(sel ? Theme.Nav : hov ? Theme.Alt : Theme.Card)) g.FillPath(b, p);
                if (!sel) using (var pen = new Pen(Theme.Line)) g.DrawPath(pen, p);
                TextRenderer.DrawText(g, Metn(i), Font, r, sel ? Color.White : Theme.Ink, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPrefix);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e) { int h = _r.FindIndex(r => r.Contains(e.Location)); if (h != _hover) { _hover = h; Invalidate(); } base.OnMouseMove(e); }
        protected override void OnMouseLeave(EventArgs e) { _hover = -1; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            int i = _r.FindIndex(r => r.Contains(e.Location));
            if (i >= 0 && _target != null) _target.SelectedIndex = i;
        }

        public void OnThemeChanged() => Invalidate();
        public void OnLanguageChanged() => Hesabla();
    }
}
