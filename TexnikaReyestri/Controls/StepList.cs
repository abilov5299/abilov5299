using System.ComponentModel;
using TexnikaReyestri.Core;

namespace TexnikaReyestri.Controls
{
    /// <summary>Şaquli addımlar siyahısı (ixrac/idxal prosesi): tamamlanan ✓, gedən fırlanır, xətalı ✕.</summary>
    [ToolboxItem(true), Description("Proses addımları siyahısı")]
    public class StepList : Control, IThemed, ILocalizable
    {
        private string[] _items = { "Addım 1", "Addım 2", "Addım 3" }; private int _cur = -1; private bool _failed;
        private readonly System.Windows.Forms.Timer _t = new() { Interval = 40 }; private float _a;

        public StepList()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            Size = new Size(320, 150); Font = new Font("Segoe UI", 9f);
            _t.Tick += (_, _) => { _a = (_a + 12) % 360; Invalidate(); };
        }

        [Category("Məzmun")] public string[] Items { get => _items; set { _items = value ?? Array.Empty<string>(); Invalidate(); } }
        /// <summary>-1: başlanmayıb; Items.Length: hamısı tamamlanıb.</summary>
        [Category("Məzmun"), DefaultValue(-1)] public int Current { get => _cur; set { _cur = value; _failed = false; Sync(); } }
        public void Fail() { _failed = true; Sync(); }
        private void Sync() { if (_cur >= 0 && _cur < _items.Length && !_failed) _t.Start(); else _t.Stop(); Invalidate(); }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics; var bc = Parent is RoundPanel rp ? rp.FillColor : Theme.Card;
            using (var bb = new SolidBrush(bc)) g.FillRectangle(bb, ClientRectangle);
            Gfx.Smooth(g);
            for (int i = 0; i < _items.Length; i++)
            {
                int y = i * 28 + 2; var r = new Rectangle(2, y, 20, 20);
                bool done = i < _cur, run = i == _cur && !_failed, fail = i == _cur && _failed;
                if (done || fail) using (var b = new SolidBrush(fail ? Theme.Bad : Theme.Ok)) g.FillEllipse(b, r);
                else using (var p = new Pen(Theme.Line, 2)) g.DrawEllipse(p, r);
                if (run) using (var p = new Pen(Theme.Accent, 2.4f)) g.DrawArc(p, r, _a, 270);
                string gl = done ? Icons.Check : fail ? Icons.Cancel : (i + 1).ToString();
                TextRenderer.DrawText(g, gl, done || fail ? Icons.Font(7f) : new Font("Segoe UI", 7.5f, FontStyle.Bold), r, done || fail ? Color.White : Theme.Muted, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                using var f = new Font(Font, run || fail ? FontStyle.Bold : FontStyle.Regular);
                TextRenderer.DrawText(g, Lang.T(_items[i]), f, new Rectangle(30, y, Width - 30, 20), fail ? Theme.Bad : done || run ? Theme.Ink : Theme.Muted, TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            }
        }

        protected override void Dispose(bool disposing) { if (disposing) _t.Dispose(); base.Dispose(disposing); }
        public void OnThemeChanged() => Invalidate();
        public void OnLanguageChanged() => Invalidate();
    }
}
