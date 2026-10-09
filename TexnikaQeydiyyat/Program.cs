using TexnikaQeydiyyat.Data;
using TexnikaQeydiyyat.Forms;

namespace TexnikaQeydiyyat
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.ThreadException += (s, e) => XetaGoster(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s, e) => XetaGoster(e.ExceptionObject as Exception);

            using var login = new LoginForm();
            if (login.ShowDialog() != DialogResult.OK) return;
            Application.Run(new MainForm());
        }

        private static void XetaGoster(Exception ex)
        {
            _ = LogService.YazAsync("Xeta", ex?.Message ?? "Naməlum xəta");
            MessageBox.Show(ex?.Message, "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
