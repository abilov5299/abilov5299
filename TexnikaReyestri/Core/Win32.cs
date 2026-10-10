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

        [DllImport("dwmapi.dll")] private static extern int DwmSetWindowAttribute(IntPtr h, int attr, ref int val, int size);
        [DllImport("gdi32.dll")] private static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int w, int h);
        public const int CS_DROPSHADOW = 0x20000;
        private static readonly bool Win11 = Environment.OSVersion.Version.Build >= 22000;

        /// <summary>Çərçivəsiz pəncərəyə yumru künc verir: Windows 11-də DWM (hamar), Windows 10-da Region.</summary>
        public static void Yumru(Form f)
        {
            if (Win11) { int v = 2; DwmSetWindowAttribute(f.Handle, 33, ref v, sizeof(int)); return; }   // DWMWA_WINDOW_CORNER_PREFERENCE = ROUND
            void Set() => f.Region = f.WindowState == FormWindowState.Maximized ? null
                : Region.FromHrgn(CreateRoundRectRgn(0, 0, f.Width + 1, f.Height + 1, 16, 16));
            Set(); f.Resize += (_, _) => Set();
        }

        /// <summary>Çərçivəsiz pəncərəni başlıq zolağından sürüşdürmək üçün.</summary>
        public static void DragWindow(Form f) { ReleaseCapture(); SendMessage(f.Handle, WM_NCLBUTTONDOWN, (IntPtr)HTCAPTION, IntPtr.Zero); }
    }
}
