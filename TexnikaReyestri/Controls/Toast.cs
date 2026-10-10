using TexnikaReyestri.Core;

namespace TexnikaReyestri.Controls
{
    public enum ToastKind { Ok, Warn, Error, Info }

    /// <summary>Sağ aşağıda 3 saniyəlik bildiriş: Toast.Show(this, "Yadda saxlanıldı");</summary>
    public sealed class Toast : Form
    {
        private readonly System.Windows.Forms.Timer _t = new() { Interval = 16 };
        private readonly string _text; private readonly ToastKind _kind; private int _life;

        private Toast(string text, ToastKind kind)
        {
            _text = Lang.T(text); _kind = kind;
            FormBorderStyle = FormBorderStyle.None; ShowInTaskbar = false; StartPosition = FormStartPosition.Manual; TopMost = true;
            BackColor = Theme.C("#0B1414"); Opacity = 0; DoubleBuffered = true;
            using var f = new Font("Segoe UI", 9.5f);
            var sz = TextRenderer.MeasureText(_text, f, new Size(320, 200), TextFormatFlags.WordBreak);
            Size = new Size(Math.Max(220, sz.Width + 70), Math.Max(48, sz.Height + 26));
            _t.Tick += (_, _) =>
            {
                _life += 16;
                Opacity = _life < 200 ? _life / 200.0 : _life > 3000 ? Math.Max(0, 1 - (_life - 3000) / 300.0) : 1;
                if (_life > 3300) { _t.Stop(); Close(); }
            };
        }

        protected override bool ShowWithoutActivation => true;

        public static void Show(Control owner, string text, ToastKind kind = ToastKind.Ok)
        {
            var f = owner?.FindForm(); if (f == null) return;
            var t = new Toast(text, kind);
            var wa = f.RectangleToScreen(f.ClientRectangle);
            t.Location = new Point(wa.Right - t.Width - 20, wa.Bottom - t.Height - 20);
            t.Show(f); t._t.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics; Gfx.Smooth(g);
            var col = _kind switch { ToastKind.Warn => Theme.Warn, ToastKind.Error => Theme.Bad, ToastKind.Info => Theme.Info, _ => Theme.Ok };
            string gl = _kind switch { ToastKind.Warn => Icons.Warning, ToastKind.Error => Icons.Cancel, ToastKind.Info => Icons.Bell, _ => Icons.Check };
            var ib = new Rectangle(14, (Height - 28) / 2, 28, 28);
            using (var p = Gfx.Round(ib, 8)) using (var b = new SolidBrush(col)) g.FillPath(b, p);
            TextRenderer.DrawText(g, gl, Icons.Font(10f), ib, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            using var f = new Font("Segoe UI", 9.5f);
            TextRenderer.DrawText(g, _text, f, new Rectangle(52, 0, Width - 62, Height), Color.White, TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
        }
    }
}
