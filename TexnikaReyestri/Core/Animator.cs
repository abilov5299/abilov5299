using TexnikaReyestri.Controls;

namespace TexnikaReyestri.Core
{
    /// <summary>Səhifə keçidi: yeni səhifə sağdan 30 piksel sürüşərək gəlir (≈180 ms). Ayarlarda söndürmək olar.</summary>
    public static class Animator
    {
        public static bool Aktiv { get; set; } = true;

        public static async Task KecidAsync(Control host, Control kohne, Control yeni)
        {
            if (!Aktiv || kohne == null || SystemInformation.TerminalServerSession)
            {
                if (kohne != null) kohne.Visible = false;
                yeni.Dock = DockStyle.Fill; yeni.Visible = true; yeni.BringToFront(); return;
            }
            yeni.Dock = DockStyle.None; yeni.Bounds = new Rectangle(30, 0, host.ClientSize.Width, host.ClientSize.Height);
            yeni.Visible = true; yeni.BringToFront(); kohne.Visible = false;
            var sw = System.Diagnostics.Stopwatch.StartNew(); const int ms = 180;
            while (sw.ElapsedMilliseconds < ms)
            {
                double k = sw.ElapsedMilliseconds / (double)ms, e = 1 - Math.Pow(1 - k, 3);   // ease-out
                yeni.Left = (int)(30 * (1 - e));
                await Task.Delay(10);
            }
            yeni.Left = 0; yeni.Dock = DockStyle.Fill;
        }
    }
}
