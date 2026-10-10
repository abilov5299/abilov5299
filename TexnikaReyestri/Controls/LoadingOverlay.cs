using System.ComponentModel;
using TexnikaReyestri.Core;

namespace TexnikaReyestri.Controls
{
    /// <summary>
    /// Yüklənmə pərdəsi: altındakı ekranın solğun şəklini, fırlanan dairəni, mətni və progress zolağını göstərir.
    /// İstifadə: using (LoadingOverlay.Show(host, "Yüklənir...")) { await ...; }
    /// </summary>
    [ToolboxItem(false)]
    public sealed class LoadingOverlay : Control
    {
        private readonly System.Windows.Forms.Timer _t = new() { Interval = 30 };
        private Bitmap _snap; private float _angle; private string _msg; private int _progress = -1; private string _sub = "";

        private LoadingOverlay(Control host, string msg)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            _msg = msg; Bounds = new Rectangle(Point.Empty, host.ClientSize); Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _host = host;
            _t.Tick += (_, _) => { _angle = (_angle + 9) % 360; Invalidate(); };
        }
        private readonly Control _host;

        /// <summary>Pərdəni ekrana çıxarır (Ui.Is bunu iş uzun çəkəndə çağırır).</summary>
        public void Goster()
        {
            if (Parent != null || _host.IsDisposed) return;
            Bounds = new Rectangle(Point.Empty, _host.ClientSize);
            _host.Controls.Add(this); BringToFront(); Cursor = Cursors.WaitCursor; _t.Start();
        }

        public static LoadingOverlay Show(Control host, string message) => new(host, message);

        public void Report(int percent, string sub = null) { _progress = percent; if (sub != null) _sub = sub; Invalidate(); }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            if (_snap != null) g.DrawImageUnscaled(_snap, 0, 0);
            g.Clear(Theme.Bg);
            Gfx.Smooth(g);
            int cx = Width / 2, cy = Height / 2 - 30; var r = new Rectangle(cx - 21, cy - 21, 42, 42);
            using (var p = new Pen(Theme.Line, 4)) g.DrawEllipse(p, r);
            using (var p = new Pen(Theme.Nav == Theme.Title ? Theme.Nav : Theme.Title, 4)) g.DrawArc(p, r, _angle, 90);
            using var f = new Font("Segoe UI Semibold", 10f);
            TextRenderer.DrawText(g, Lang.T(_msg), f, new Rectangle(0, cy + 32, Width, 22), Theme.Title, TextFormatFlags.HorizontalCenter);
            if (_progress >= 0)
            {
                var br = new Rectangle(cx - 130, cy + 60, 260, 6);
                using (var b = new SolidBrush(Theme.Line)) g.FillRectangle(b, br);
                using (var b = new SolidBrush(Theme.Nav)) g.FillRectangle(b, br.X, br.Y, br.Width * Math.Clamp(_progress, 0, 100) / 100, br.Height);
            }
            if (_sub.Length > 0) { using var sf = new Font("Segoe UI", 8.5f); TextRenderer.DrawText(g, _sub, sf, new Rectangle(0, cy + 72, Width, 18), Theme.Muted, TextFormatFlags.HorizontalCenter); }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) { _t.Stop(); _t.Dispose(); _snap?.Dispose(); Parent?.Controls.Remove(this); }
            base.Dispose(disposing);
        }
    }
}
