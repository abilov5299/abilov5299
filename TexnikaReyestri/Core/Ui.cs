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

        /// <summary>Üç seçim: OK (ok), No (xeyr), Cancel (Ləğv et).</summary>
        public static DialogResult Secim(IWin32Window owner, string basliq, string mesaj, string ok, string xeyr)
        {
            using var d = new ConfirmDialog(basliq, mesaj, ok, false, xeyr: xeyr);
            return d.ShowDialog(owner);
        }

        /// <summary>Xanaya yalnız rəqəm yazılır (kesr: bir onluq ayırıcı da olar); yapışdırılan başqa simvollar silinir.</summary>
        public static void Reqemli(TextBox t, bool kesr = false)
        {
            bool Olar(char ch) => char.IsDigit(ch) || kesr && ch is ',' or '.';
            t.KeyPress += (_, e) => e.Handled = !char.IsControl(e.KeyChar) && !Olar(e.KeyChar) || e.KeyChar is ',' or '.' && (t.Text.Contains(',') || t.Text.Contains('.'));
            t.TextChanged += (_, _) => { var r = new string(t.Text.Where(Olar).ToArray()); if (r != t.Text) { t.Text = r; t.SelectionStart = r.Length; } };
        }

        public static void Melumat(IWin32Window owner, string basliq, string mesaj)
        {
            using var d = new ConfirmDialog(basliq, mesaj, "Bağla", false, yalnizOk: true);
            d.ShowDialog(owner);
        }

        /// <summary>Uzun əməliyyat: yüklənmə pərdəsi göstərilir, UI donmur, xəta olsa toast çıxır.</summary>
        /// <summary>İş 250 ms-dən uzun çəkərsə yüklənmə pərdəsi göstərilir (qısa işlərdə ekran qırpılmasın).</summary>
        public static async Task<bool> Is(Control host, string mesaj, Func<LoadingOverlay, Task> isi)
        {
            using var ov = LoadingOverlay.Show(host, mesaj);
            try
            {
                var t = isi(ov);
                if (await Task.WhenAny(t, Task.Delay(250)) != t) ov.Goster();
                await t; return true;
            }
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
        [System.Runtime.InteropServices.DllImport("user32.dll")] private static extern IntPtr SendMessage(IntPtr h, int m, IntPtr w, IntPtr l);
        [System.Runtime.InteropServices.DllImport("user32.dll")] private static extern bool RedrawWindow(IntPtr h, IntPtr r, IntPtr rgn, int f);

        /// <summary>using (Ui.Dondur(c)) { ... } — blok bitənə qədər c və uşaqları çəkilmir, sonra hamısı bir dəfəyə çəkilir.</summary>
        public static IDisposable Dondur(Control c)
        {
            if (c == null || !c.IsHandleCreated) return new Bos();
            SendMessage(c.Handle, 0x000B, IntPtr.Zero, IntPtr.Zero);   // WM_SETREDRAW = false
            return new Ac(c);
        }
        private sealed class Bos : IDisposable { public void Dispose() { } }
        private sealed class Ac : IDisposable
        {
            private readonly Control _c; public Ac(Control c) => _c = c;
            public void Dispose()
            {
                if (!_c.IsHandleCreated) return;
                SendMessage(_c.Handle, 0x000B, (IntPtr)1, IntPtr.Zero);
                RedrawWindow(_c.Handle, IntPtr.Zero, IntPtr.Zero, 0x0001 | 0x0004 | 0x0080 | 0x0100 | 0x0400);   // INVALIDATE|ERASE|ALLCHILDREN|UPDATENOW|FRAME
            }
        }

        /// <summary>
        /// Yazını valideynin sağ kənarına bərkidir (Padding nəzərə alınır). sol: bu yazı həmin kontrolun SOLUNDA durur.
        /// AutoSize yazılarda Designer-dəki Anchor=Right yeri səhv saxlayırdı (dil dəyişəndə en də dəyişir).
        /// </summary>
        public static void Saga(Control c, Control sol = null)
        {
            void Yerlesdir()
            {
                if (c.Parent == null) return;
                int sag = sol != null ? sol.Left - 12 : c.Parent.ClientSize.Width - c.Parent.Padding.Right;
                c.Left = Math.Max(c.Parent.Padding.Left, sag - c.Width);
            }
            c.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            c.ParentChanged += (_, _) => { if (c.Parent != null) { c.Parent.Resize -= H; c.Parent.Resize += H; } Yerlesdir(); };
            if (c.Parent != null) c.Parent.Resize += H;
            c.SizeChanged += H;
            if (sol != null) { sol.SizeChanged += H; sol.LocationChanged += H; }
            Yerlesdir();
            void H(object s, EventArgs e) => Yerlesdir();
        }

        public static void Kenar(Form f, PaintEventArgs e) { using var p = new Pen(Theme.Line); e.Graphics.DrawRectangle(p, 0, 0, f.ClientSize.Width - 1, f.ClientSize.Height - 1); }
    }
}
