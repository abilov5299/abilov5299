using System.ComponentModel;
using TexnikaReyestri.Core;

namespace TexnikaReyestri.Controls
{
    /// <summary>Təmir mərhələləri xətti. Steps — mərhələlərin adları, CurrentStep — hazırkı mərhələ (0-dan).</summary>
    [ToolboxItem(true), Description("Mərhələ xətti (Qəbul → ... → Təhvil)")]
    public class StepperControl : Control, IThemed, ILocalizable
    {
        private string[] _steps = { "Qəbul", "Defekt aşkarlama", "Hissə təminatı", "Təmir", "Sınaq", "Təhvil" };
        private int _current = 3;

        public StepperControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            Size = new Size(700, 60); Font = new Font("Segoe UI", 8.5f);
        }

        [Category("Məzmun")] public string[] Steps { get => _steps; set { _steps = value ?? Array.Empty<string>(); Invalidate(); } }
        [Category("Məzmun"), DefaultValue(3), Description("Hazırkı mərhələ. Sonuncudan böyük olsa, hamısı tamamlanmış görünür")]
        public int CurrentStep { get => _current; set { _current = value; Invalidate(); } }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics; var bc = Parent is RoundPanel rp ? rp.FillColor : Theme.Card;
            using (var bb = new SolidBrush(bc)) g.FillRectangle(bb, ClientRectangle);
            Gfx.Smooth(g);
            int n = _steps.Length; if (n == 0) return;
            float cw = Width / (float)n; int cy = 16, rad = 11;
            bool allDone = _current >= n;
            for (int i = 1; i < n; i++)
            {
                float x1 = cw * (i - 1) + cw / 2, x2 = cw * i + cw / 2;
                using var pen = new Pen(i <= _current ? Theme.Ok : Theme.Line, 3);
                g.DrawLine(pen, x1 + rad, cy, x2 - rad, cy);
            }
            for (int i = 0; i < n; i++)
            {
                float cx = cw * i + cw / 2; var r = new RectangleF(cx - rad, cy - rad, rad * 2, rad * 2);
                bool done = i < _current || allDone, cur = i == _current && !allDone;
                using (var b = new SolidBrush(done ? Theme.Ok : bc)) g.FillEllipse(b, r);
                using (var pen = new Pen(done ? Theme.Ok : cur ? Theme.Accent : Theme.Line, 3)) g.DrawEllipse(pen, r);
                using var nf = new Font(done ? Icons.FontName : "Segoe UI", done ? 8f : 8f, FontStyle.Bold);
                TextRenderer.DrawText(g, done ? Icons.Check : (i + 1).ToString(), nf, Rectangle.Round(r), done ? Color.White : cur ? Theme.Accent : Theme.Muted,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                using var lf = new Font(Font, cur ? FontStyle.Bold : FontStyle.Regular);
                TextRenderer.DrawText(g, Lang.T(_steps[i]), lf, new Rectangle((int)(cx - cw / 2), cy + rad + 6, (int)cw, 20), cur ? Theme.Ink : Theme.Muted,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
            }
        }

        public void OnThemeChanged() => Invalidate();
        public void OnLanguageChanged() => Invalidate();
    }
}
