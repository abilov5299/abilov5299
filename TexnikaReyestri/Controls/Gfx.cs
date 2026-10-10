using System.Drawing.Drawing2D;

namespace TexnikaReyestri.Controls
{
    internal static class Gfx
    {
        public static GraphicsPath Round(RectangleF r, float rad)
        {
            var p = new GraphicsPath();
            if (rad <= 0) { p.AddRectangle(r); return p; }
            float d = rad * 2;
            p.AddArc(r.X, r.Y, d, d, 180, 90);
            p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }

        /// <summary>Kontrolun arxasındakı görünən fon rəngi (şəffaf panelləri keçərək).</summary>
        public static Color BackOf(Control c)
        {
            for (var p = c.Parent; p != null; p = p.Parent)
            {
                if (p is RoundPanel rp) return rp.FillColor;
                if (p is PageBase) return Core.Theme.Bg;
                if (p.BackColor.A == 255) return p.BackColor;
            }
            return Core.Theme.Bg;
        }

        /// <summary>Şəffaf kontrolun arxa fonunu ucuz yolla çəkir: rəngli valideyn → düz rəng, səhifə → keşlənmiş bitmap kəsiyi.</summary>
        public static void ParentBack(Control c, Graphics g)
        {
            for (var p = c.Parent; p != null; p = p.Parent)
            {
                if (p is RoundPanel rp && rp.Fill != PanelFill.Transparent) { using var b = new SolidBrush(rp.FillColor); g.FillRectangle(b, c.ClientRectangle); return; }
                if (p is PageBase pb)
                {
                    var bmp = pb.Fon; if (bmp == null) break;
                    var o = pb.PointToClient(c.PointToScreen(Point.Empty));
                    g.DrawImage(bmp, new Rectangle(Point.Empty, c.Size), new Rectangle(o, c.Size), GraphicsUnit.Pixel);
                    return;
                }
                if (p is not RoundPanel && p.BackColor.A == 255) { using var b = new SolidBrush(p.BackColor); g.FillRectangle(b, c.ClientRectangle); return; }
            }
            using (var b2 = new SolidBrush(Core.Theme.Bg)) g.FillRectangle(b2, c.ClientRectangle);
        }

        public static void Smooth(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        }

        /// <summary>Yumşaq kölgə: bir neçə şəffaf qat.</summary>
        public static void Shadow(Graphics g, RectangleF r, float rad, Color c, int size)
        {
            for (int i = size; i > 0; i--)
            {
                var rr = r; rr.Inflate(i * 0.6f, i * 0.6f); rr.Offset(0, i * 0.5f);
                using var p = Round(rr, rad + i * 0.6f);
                using var b = new SolidBrush(Color.FromArgb(Math.Max(1, c.A / (size + 1)), c));
                g.FillPath(b, p);
            }
        }

        /// <summary>Qalxan loqosunun konturu (64×64 koordinatlarda).</summary>
        public static GraphicsPath Shield(RectangleF box)
        {
            float s = box.Width / 64f; float X(float v) => box.X + v * s; float Y(float v) => box.Y + v * s;
            var p = new GraphicsPath();
            p.AddLine(X(32), Y(10), X(49), Y(16));
            p.AddLine(X(49), Y(16), X(49), Y(31));
            p.AddBezier(X(49), Y(31), X(49), Y(42), X(42), Y(50), X(32), Y(54));
            p.AddBezier(X(32), Y(54), X(22), Y(50), X(15), Y(42), X(15), Y(31));
            p.AddLine(X(15), Y(31), X(15), Y(16));
            p.CloseFigure();
            return p;
        }

        /// <summary>Səhifə fonu: künclərdə işıq ləkələri və aşağı sağda solğun qalxan.</summary>
        public static void PageBackground(Graphics g, Rectangle r, bool watermark = true)
        {
            using (var b = new SolidBrush(Core.Theme.Bg)) g.FillRectangle(b, r);
            Glow(g, new PointF(r.Right, r.Top), Math.Max(r.Width, r.Height) * 0.45f, Core.Theme.Glow1);
            Glow(g, new PointF(r.Left, r.Bottom), Math.Max(r.Width, r.Height) * 0.5f, Core.Theme.Glow2);
            if (watermark && r.Width > 300)
            {
                float sz = Math.Min(380, r.Height * 0.7f);
                using var p = Shield(new RectangleF(r.Right - sz * 0.8f, r.Bottom - sz * 0.75f, sz, sz));
                using var wb = new SolidBrush(Core.Theme.Watermark);
                g.FillPath(wb, p);
            }
        }

        public static void Glow(Graphics g, PointF c, float rad, Color col)
        {
            if (rad < 2) return;
            using var p = new GraphicsPath(); p.AddEllipse(c.X - rad, c.Y - rad, rad * 2, rad * 2);
            using var b = new PathGradientBrush(p) { CenterColor = col, SurroundColors = new[] { Color.FromArgb(0, col) } };
            g.FillPath(b, p);
        }
    }
}
