using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Forms;
using TexnikaReyestri.Services;

namespace TexnikaReyestri
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += (_, e) => Xeta(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (_, e) => Xeta(e.ExceptionObject as Exception);
            Application.AddMessageFilter(new WheelRouter());

            LocalDb.Init(AppPaths.Db);
            Ui.SeciminiYukleAsync().GetAwaiter().GetResult();
            Animator.Aktiv = SistemRepo.ParamAsync("Animasiya", 1).GetAwaiter().GetResult() == 1;

            // İlk açılış: qurum, admin parolu, ilk istifadəçi
            if (!AuthService.AdminVarAsync().GetAwaiter().GetResult())
                using (var f = new FirstRunForm()) if (f.ShowDialog() != DialogResult.OK) return;

            while (true)
            {
                DialogResult r;
                using (var login = new LoginForm()) r = login.ShowDialog();
                if (r == DialogResult.Retry)          // gizli admin girişi
                {
                    using var al = new AdminLoginForm { StartPosition = FormStartPosition.CenterScreen };
                    if (al.ShowDialog() == DialogResult.OK) using (var a = new AdminForm()) a.ShowDialog();
                    continue;
                }
                if (r != DialogResult.OK) return;
                var main = new MainForm();
                Application.Run(main);
                if (!main.CixisEdildi) return;
                Session.Bitir();
            }
        }

        private static void Xeta(Exception ex)
        {
            try { SistemRepo.LogAsync("Xəta", ex?.ToString() ?? "Naməlum xəta").Wait(1000); } catch { }
            MessageBox.Show(ex?.Message ?? "Naməlum xəta", "Texnika Reyestri", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
