using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;

namespace TexnikaReyestri.Services
{
    /// <summary>
    /// "Çap et (PDF)" düymələri: klikdə iki seçim — "Çap et" (PDF müvəqqəti hazırlanır, çap pəncərəsi açılır)
    /// və "PDF kimi saxla" (fayl seçilir, saxlanır və açılır).
    /// </summary>
    public static class CapService
    {
        /// <param name="yarat">Verilən yola PDF yazır.</param>
        public static void Menyu(Control duyme, string faylAdi, Func<string, Task> yarat)
        {
            var m = new ContextMenuStrip { ShowImageMargin = false, Font = new Font("Segoe UI", 10f), Renderer = new Renderer(), Padding = new Padding(2) };
            m.Items.Add(Lang.T("Çap et"), null, async (_, _) => await CapAsync(duyme, yarat));
            m.Items.Add(Lang.T("PDF kimi saxla"), null, async (_, _) => await SaxlaAsync(duyme, faylAdi, yarat));
            foreach (ToolStripItem i in m.Items) { i.Padding = new Padding(10, 6, 24, 6); i.ForeColor = Theme.Ink; }
            m.Closed += (_, _) => duyme.BeginInvoke(m.Dispose);
            m.Show(duyme, new Point(0, duyme.Height + 2));
        }

        private static async Task SaxlaAsync(Control owner, string faylAdi, Func<string, Task> yarat)
        {
            using var d = new SaveFileDialog { Filter = "PDF (*.pdf)|*.pdf", FileName = faylAdi };
            if (d.ShowDialog(owner) != DialogResult.OK) return;
            if (!await Ui.Is(owner, Lang.T("PDF hazırlanır..."), _ => yarat(d.FileName))) return;
            Toast.Show(owner, Lang.T("PDF kimi saxlanıldı"));
            try { System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(d.FileName) { UseShellExecute = true }); } catch { }
        }

        private static async Task CapAsync(Control owner, Func<string, Task> yarat)
        {
            string q = Path.Combine(Path.GetTempPath(), "TexnikaReyestri", "Cap");
            Directory.CreateDirectory(q);
            foreach (var f in Directory.GetFiles(q, "*.pdf")) try { if (File.GetLastWriteTime(f) < DateTime.Now.AddDays(-1)) File.Delete(f); } catch { }
            string yol = Path.Combine(q, $"cap_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
            if (!await Ui.Is(owner, Lang.T("PDF hazırlanır..."), _ => yarat(yol))) return;
            await CapPenceresiAsync(owner, yol);
        }

        /// <summary>PDF proqramın içində göstərilir və Windows çap pəncərəsi açılır. WebView2 yoxdursa — Windows-un "print" əmri, o da olmasa PDF oxuyucuda açılır.</summary>
        private static async Task CapPenceresiAsync(Control owner, string yol)
        {
            var f = new Form { Text = Lang.T("Çap et"), StartPosition = FormStartPosition.CenterParent, Size = new Size(1000, 760), ShowInTaskbar = false, Icon = owner.FindForm()?.Icon, MinimizeBox = false };
            var w = new WebView2 { Dock = DockStyle.Fill, DefaultBackgroundColor = Color.White };
            f.Controls.Add(w);
            try
            {
                f.Show(owner.FindForm());
                var env = await CoreWebView2Environment.CreateAsync(null, Path.Combine(AppPaths.Root, "WebView"));
                await w.EnsureCoreWebView2Async(env);
                var hazir = new TaskCompletionSource<bool>();
                w.CoreWebView2.NavigationCompleted += (_, e) => hazir.TrySetResult(e.IsSuccess);
                w.CoreWebView2.Navigate(new Uri(yol).AbsoluteUri);
                if (await hazir.Task)
                {
                    await Task.Delay(400);   // PDF görüntüləyicisi səhifəni çəksin
                    w.CoreWebView2.ShowPrintUI(CoreWebView2PrintDialogKind.System);
                    return;
                }
                throw new InvalidOperationException();
            }
            catch
            {
                f.Close();
                try { System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(yol) { UseShellExecute = true, Verb = "print" }); }
                catch { try { System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(yol) { UseShellExecute = true }); } catch { } }
            }
        }

        private sealed class Renderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e) => e.Graphics.Clear(Theme.Card);
            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) { using var p = new Pen(Theme.Line); e.Graphics.DrawRectangle(p, 0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1); }
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                if (!e.Item.Selected) return;
                using var b = new SolidBrush(Theme.Sel); e.Graphics.FillRectangle(b, 2, 0, e.Item.Width - 4, e.Item.Height);
            }
            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e) { e.TextColor = Theme.Ink; base.OnRenderItemText(e); }
        }
    }
}
