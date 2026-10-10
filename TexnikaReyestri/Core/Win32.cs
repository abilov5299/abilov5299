using System.Runtime.InteropServices;

namespace TexnikaReyestri.Core
{
    internal static class Win32
    {
        public const int WM_NCLBUTTONDOWN = 0xA1, HTCAPTION = 0x2, WM_NCHITTEST = 0x84, WM_MOUSEWHEEL = 0x020A;
        public const int HTLEFT = 10, HTRIGHT = 11, HTTOP = 12, HTTOPLEFT = 13, HTTOPRIGHT = 14, HTBOTTOM = 15, HTBOTTOMLEFT = 16, HTBOTTOMRIGHT = 17;

        [DllImport("user32.dll")] public static extern bool ReleaseCapture();
        [DllImport("user32.dll")] public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        [DllImport("user32.dll")] public static extern IntPtr WindowFromPoint(Point p);

        /// <summary>Çərçivəsiz pəncərəni başlıq zolağından sürüşdürmək üçün.</summary>
        public static void DragWindow(Form f) { ReleaseCapture(); SendMessage(f.Handle, WM_NCLBUTTONDOWN, (IntPtr)HTCAPTION, IntPtr.Zero); }
    }
}
