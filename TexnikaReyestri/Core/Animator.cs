using TexnikaReyestri.Controls;

namespace TexnikaReyestri.Core
{
    /// <summary>
    /// Səhifə keçidi. Canlı kontrollar hərəkət etdirilmir (bu, ləngiməyə səbəb olurdu):
    /// yeni səhifənin bir şəkli çəkilir və yalnız o şəkil 24 piksel sağdan sürüşərək gəlir (≈140 ms), sonra əsl səhifə göstərilir.
    /// </summary>
    public static class Animator
    {
        public static bool Aktiv { get; set; } = true;

        public static async Task KecidAsync(Control host, Control kohne, Control yeni)
        {
            yeni.Dock = DockStyle.Fill;
            if (!Aktiv || kohne == null || SystemInformation.TerminalServerSession || host.Width <= 0 || host.Height <= 0)
            { Goster(host, kohne, yeni); return; }

            Bitmap shot;
            try
            {
                yeni.Bounds = host.ClientRectangle; yeni.PerformLayout();
                shot = new Bitmap(host.ClientSize.Width, host.ClientSize.Height);
                yeni.DrawToBitmap(shot, new Rectangle(Point.Empty, shot.Size));
            }
            catch { Goster(host, kohne, yeni); return; }

            using var ov = new SnapPanel(shot, kohne is PageBase pb ? pb.Fon : null) { Bounds = host.ClientRectangle };
            host.Controls.Add(ov); ov.BringToFront(); ov.Refresh();
            host.SuspendLayout(); if (kohne != null) kohne.Visible = false; yeni.Visible = true; host.ResumeLayout(false);

            var sw = System.Diagnostics.Stopwatch.StartNew(); const int ms = 140;
            while (sw.ElapsedMilliseconds < ms)
            {
                double k = sw.ElapsedMilliseconds / (double)ms;
                ov.Offset = (int)(24 * Math.Pow(1 - k, 3)); ov.Alpha = (float)Math.Min(1, k * 1.6); ov.Refresh();
                await Task.Delay(8);
            }
            host.Controls.Remove(ov); shot.Dispose();
            yeni.BringToFront();
        }

        private static void Goster(Control host, Control kohne, Control yeni)
        {
            host.SuspendLayout();
            if (kohne != null) kohne.Visible = false;
            yeni.Visible = true; yeni.BringToFront();
            host.ResumeLayout(true);
        }

        private sealed class SnapPanel : Control
        {
            private readonly Bitmap _yeni, _fon; public int Offset = 24; public float Alpha;
            public SnapPanel(Bitmap yeni, Bitmap fon)
            {
                _yeni = yeni; _fon = fon;
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.Opaque, true);
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                var g = e.Graphics;
                if (_fon != null) g.DrawImageUnscaled(_fon, 0, 0); else g.Clear(Theme.Bg);
                if (Alpha >= 0.99f) { g.DrawImageUnscaled(_yeni, Offset, 0); return; }
                using var ia = new System.Drawing.Imaging.ImageAttributes();
                ia.SetColorMatrix(new System.Drawing.Imaging.ColorMatrix { Matrix33 = Alpha });
                g.DrawImage(_yeni, new Rectangle(Offset, 0, _yeni.Width, _yeni.Height), 0, 0, _yeni.Width, _yeni.Height, GraphicsUnit.Pixel, ia);
            }
        }
    }
}
