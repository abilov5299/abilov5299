using System.Runtime.InteropServices;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Services;

namespace TexnikaReyestri.Pages
{
    /// <summary>
    /// Təlimat: istifadəçinin öz roluna uyğun təlimat (Zavod, Xidmət, İdarə) proqramın içində açılır (WebView2).
    /// Təlimatlar proqram qovluğundakı Telimat\*.html fayllarıdır (docs/telimat/qur.py ilə qurulur).
    /// Kompüterdə WebView2 mühiti yoxdursa, təlimat standart brauzerdə açılır.
    /// </summary>
    public class TelimatPage : PageBase
    {
        private WebView2 _web;
        private readonly Label lblXeta = new() { Dock = DockStyle.Fill, Tag = "muted", Font = new Font("Segoe UI", 10f), TextAlign = ContentAlignment.MiddleCenter, Visible = false };
        private bool _yuklendi;

        private static string Fayl => Session.Tip switch { QurumTip.UstQurum => "Xidmet.html", QurumTip.EnUstQurum => "Idare.html", _ => "Zavod.html" };
        private static string Yol => Path.Combine(AppContext.BaseDirectory, "Telimat", Fayl);

        public TelimatPage()
        {
            Padding = new Padding(0);
            Controls.Add(lblXeta);
        }

        public override async Task OnShownAsync()
        {
            if (_yuklendi) return;
            if (!File.Exists(Yol)) { Xeta(Lang.T("Təlimat faylı tapılmadı") + ":\n" + Yol, false); return; }
            try
            {
                _web = new WebView2 { Dock = DockStyle.Fill, DefaultBackgroundColor = Color.White };
                Controls.Add(_web); _web.BringToFront();
                var env = await CoreWebView2Environment.CreateAsync(null, Path.Combine(AppPaths.Root, "WebView"));
                await _web.EnsureCoreWebView2Async(env);
                var s = _web.CoreWebView2.Settings;
                s.AreDevToolsEnabled = false; s.IsStatusBarEnabled = false; s.AreDefaultContextMenusEnabled = false;
                _web.CoreWebView2.NewWindowRequested += (_, e) => e.Handled = true;   // təlimatdan kənara keçid yoxdur
                _web.CoreWebView2.Navigate(new Uri(Yol).AbsoluteUri);
                _yuklendi = true;
            }
            catch (Exception ex) when (ex is WebView2RuntimeNotFoundException or COMException or InvalidOperationException)
            {
                _web?.Dispose(); _web = null;
                Xeta(Lang.T("Bu kompüterdə daxili baxış mühiti (Microsoft Edge WebView2) yoxdur. Təlimat brauzerdə açılır."), true);
            }
        }

        private void Xeta(string m, bool brauzer)
        {
            lblXeta.Text = m; lblXeta.Visible = true; lblXeta.BringToFront();
            if (!brauzer) return;
            try { System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(Yol) { UseShellExecute = true }); }
            catch (Exception ex) { Toast.Show(this, ex.Message, ToastKind.Error); }
        }
    }
}
