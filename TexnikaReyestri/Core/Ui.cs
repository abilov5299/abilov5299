using TexnikaReyestri.Controls;
using TexnikaReyestri.Data;
using TexnikaReyestri.Forms;

namespace TexnikaReyestri.Core
{
    /// <summary>Formalar üçün ortaq köməkçilər: tema/dil tətbiqi, təsdiq pəncərəsi, yüklənmə ilə iş, səhv göstərmək.</summary>
    public static class Ui
    {
        /// <summary>Formanın bütün kontrollarına tema və dili tətbiq edir. Hər formanın Load hadisəsində çağırılır.</summary>
        public static void Hazirla(Control root) { Theme.Apply(root); Lang.Apply(root); }

        public static bool Tesdiq(IWin32Window owner, string basliq, string mesaj, string ok = "Bəli", bool tehlukeli = false)
        {
            using var d = new ConfirmDialog(basliq, mesaj, ok, tehlukeli);
            return d.ShowDialog(owner) == DialogResult.OK;
        }

        public static void Melumat(IWin32Window owner, string basliq, string mesaj)
        {
            using var d = new ConfirmDialog(basliq, mesaj, "Bağla", false, yalnizOk: true);
            d.ShowDialog(owner);
        }

        /// <summary>Uzun əməliyyat: yüklənmə pərdəsi göstərilir, UI donmur, xəta olsa toast çıxır.</summary>
        public static async Task<bool> Is(Control host, string mesaj, Func<LoadingOverlay, Task> isi)
        {
            using var ov = LoadingOverlay.Show(host, mesaj);
            try { await isi(ov); return true; }
            catch (OperationCanceledException) { return false; }
            catch (Exception ex) { await SistemRepo.LogAsync("Xəta", ex.Message); Toast.Show(host, ex.Message, ToastKind.Error); return false; }
        }


        // ---------- Tema və dil seçimi (kompüter üzrə yadda saxlanılır) ----------
        public static async Task SeciminiYukleAsync()
        {
            Theme.Set(await SistemRepo.ParamStrAsync("Tema") == "dark" ? ThemeMode.Dark : ThemeMode.Light, false);
            Lang.Set((await SistemRepo.ParamStrAsync("Dil")) switch { "en" => Language.En, "ru" => Language.Ru, _ => Language.Az });
        }

        public static Task TemaSaxla() => SistemRepo.ParamYazAsync("Tema", Theme.Mode == ThemeMode.Dark ? "dark" : "light");
        public static Task DilSaxla() => SistemRepo.ParamYazAsync("Dil", Lang.Current switch { Language.En => "en", Language.Ru => "ru", _ => "az" });
        public static int DilIndex => (int)Lang.Current;
        public static string TemaGlyph => Theme.Mode == ThemeMode.Dark ? Icons.Sun : Icons.Moon;

        /// <summary>Çərçivəsiz formanın kənarına nazik xətt çəkir.</summary>
        public static void Kenar(Form f, PaintEventArgs e) { using var p = new Pen(Theme.Line); e.Graphics.DrawRectangle(p, 0, 0, f.ClientSize.Width - 1, f.ClientSize.Height - 1); }
    }
}
