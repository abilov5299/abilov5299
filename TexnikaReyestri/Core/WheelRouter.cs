using TexnikaReyestri.Controls;

namespace TexnikaReyestri.Core
{
    /// <summary>
    /// Siçan təkərini kursorun altındakı ScrollPanel və ya ModernGrid-ə yönləndirir
    /// (Windows təkəri fokuslu kontrola göndərir; gizli skrol üçün bu lazımdır).
    /// </summary>
    internal sealed class WheelRouter : IMessageFilter
    {
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg != Win32.WM_MOUSEWHEEL) return false;
            var p = Cursor.Position;
            var c = Control.FromHandle(Win32.WindowFromPoint(p));
            int delta = (short)((long)m.WParam >> 16);
            for (; c != null; c = c.Parent)
            {
                if (c is ComboBox { DroppedDown: true } or NumericUpDown) return false;
                if (c is ModernGrid g) { var pt = g.PointToClient(p); g.GetType(); InvokeWheel(g, delta, pt); return true; }
                if (c is ScrollPanel sp && sp.ContentHeight > sp.ClientSize.Height) { sp.ScrollBy(-delta / 120 * 48); return true; }
            }
            return false;
        }

        private static void InvokeWheel(ModernGrid g, int delta, Point pt)
        {
            if (g.RowCount == 0) return;
            int first = g.FirstDisplayedScrollingRowIndex, maxFirst = Math.Max(0, g.RowCount - g.DisplayedRowCount(false));
            int n = Math.Clamp(first - Math.Sign(delta) * 3, 0, maxFirst);
            if (n != first && n >= 0) g.FirstDisplayedScrollingRowIndex = n;
        }
    }
}
