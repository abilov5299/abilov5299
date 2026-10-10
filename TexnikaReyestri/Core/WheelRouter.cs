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
                if (c is ModernGrid g) { g.ScrollRows(delta); return true; }
                if (c is ScrollPanel sp && sp.ContentHeight > sp.ClientSize.Height) { sp.ScrollBy(-delta / 120 * 48); return true; }
            }
            return false;
        }

    }
}
