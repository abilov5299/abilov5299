using System.ComponentModel;
using TexnikaReyestri.Core;

namespace TexnikaReyestri.Controls
{
    public enum CaptionKind { Minimize, Maximize, Close }

    /// <summary>Başlıq düyməsi (—, □/❐, ✕). Şriftdən asılı deyil, xətlərlə çəkilir. Maximize pəncərə böyüdüləndə avtomatik "Bərpa" ikonuna keçir.</summary>
    [ToolboxItem(true), DefaultEvent("Click"), Description("Pəncərə başlıq düyməsi: kiçilt / böyüt / bağla")]
    public class CaptionButton : Control, IThemed
    {
        private CaptionKind _kind; private bool _hover, _down;

        public CaptionButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            Size = new Size(46, 48); Cursor = Cursors.Hand;
        }

        [Category("Görünüş"), DefaultValue(CaptionKind.Minimize)] public CaptionKind Kind { get => _kind; set { _kind = value; Invalidate(); } }

        protected override void OnMouseEnter(EventArgs e) { _hover = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _hover = _down = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnMouseDown(MouseEventArgs e) { _down = true; Invalidate(); base.OnMouseDown(e); }
        protected override void OnMouseUp(MouseEventArgs e) { _down = false; Invalidate(); base.OnMouseUp(e); }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            var f = FindForm(); if (f == null) return;
            if (_kind == CaptionKind.Minimize) f.WindowState = FormWindowState.Minimized;
            else if (_kind == CaptionKind.Close) f.Close();
            else
                using (Ui.Dondur(f)) f.WindowState = f.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            var f = FindForm(); if (f != null) { f.Resize -= FormResize; f.Resize += FormResize; }
        }
        private void FormResize(object s, EventArgs e) => Invalidate();

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            Color back = Gfx.BackOf(this), fore = Theme.Title;
            if (_hover || _down)
            {
                back = _kind == CaptionKind.Close ? (_down ? Theme.Mix(Theme.Bad, Color.Black, 0.15f) : Theme.Bad) : (_down ? Theme.Line : Theme.Alt);
                if (_kind == CaptionKind.Close) fore = Color.White;
            }
            using (var b = new SolidBrush(back)) g.FillRectangle(b, ClientRectangle);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            float s = 10 * DeviceDpi / 96f, cx = Width / 2f, cy = Height / 2f;
            using var p = new Pen(fore, Math.Max(1f, DeviceDpi / 96f));
            switch (_kind)
            {
                case CaptionKind.Minimize: g.DrawLine(p, cx - s / 2, cy, cx + s / 2, cy); break;
                case CaptionKind.Close: g.DrawLine(p, cx - s / 2, cy - s / 2, cx + s / 2, cy + s / 2); g.DrawLine(p, cx + s / 2, cy - s / 2, cx - s / 2, cy + s / 2); break;
                default:
                    if (FindForm()?.WindowState == FormWindowState.Maximized)
                    {
                        float k = s * 0.8f, o = s * 0.2f;
                        g.DrawRectangle(p, cx - s / 2, cy - s / 2 + o, k, k);
                        g.DrawLines(p, new[] { new PointF(cx - s / 2 + o, cy - s / 2 + o), new PointF(cx - s / 2 + o, cy - s / 2), new PointF(cx + s / 2, cy - s / 2), new PointF(cx + s / 2, cy - s / 2 + k), new PointF(cx - s / 2 + k, cy - s / 2 + k) });
                    }
                    else g.DrawRectangle(p, cx - s / 2, cy - s / 2, s, s);
                    break;
            }
        }

        public void OnThemeChanged() => Invalidate();
    }
}
