using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Forms;
using TexnikaReyestri.Pages;
using TexnikaReyestri.Services;

namespace TelimatSekil
{
    /// <summary>
    /// Təlimat üçün ekran şəkilləri. Real bazanın SURƏTİNDƏ nümunə məlumat yaradır, proqramı hər rol üçün (zavod, xidmət, idarə)
    /// ekrandan kənarda açır və səhifələrin şəklini PrintWindow ilə çəkir. Düymələrin kiçik şəkilləri sekil/ikon/ qovluğuna,
    /// şəkillərdəki elementlərin yerləri (faizlə) sekil/yer.json-a yazılır — qur.py nömrələri oradan qoyur.
    /// İşə salmaq: dotnet run --project tools/TelimatSekil   (sonra: python docs/telimat/qur.py)
    /// Klaviatura və siçan göndərilmir: hər şey koddan idarə olunur.
    /// </summary>
    internal static class Program
    {
        const BindingFlags B = BindingFlags.NonPublic | BindingFlags.Instance;
        static string Cix;                                     // docs/telimat/sekil
        static readonly Dictionary<string, Dictionary<string, double[]>> Yer = new();
        static readonly HashSet<string> Ikonlar = new();
        const int W = 1440, H = 900;

        [DllImport("user32.dll")] static extern bool PrintWindow(IntPtr h, IntPtr hdc, uint f);

        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.DpiUnawareGdiScaled);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string kok = AppContext.BaseDirectory;
            while (!File.Exists(Path.Combine(kok, "TexnikaReyestri.slnx"))) kok = Path.GetDirectoryName(kok.TrimEnd('\\'))!;
            Cix = Path.Combine(kok, "docs", "telimat", "sekil");
            // Bazanın surəti (real baza dəyişmir)
            string root = Path.Combine(Path.GetTempPath(), "TelimatSekil");
            if (Directory.Exists(root)) Directory.Delete(root, true);
            Directory.CreateDirectory(Path.Combine(root, "Data"));
            File.Copy(@"C:\ProgramData\TexnikaReyestri\Data\reyestr.db", Path.Combine(root, "Data", "reyestr.db"));
            AppPaths.Root = root;
            LocalDb.Init(AppPaths.Db);
            Animator.Aktiv = false;
            Directory.CreateDirectory(Path.Combine(Cix, "ikon"));

            var surucu = new Form { ShowInTaskbar = false, Opacity = 0, StartPosition = FormStartPosition.Manual, Location = new Point(-20000, -20000), Size = new Size(10, 10) };
            surucu.Shown += async (_, _) =>
            {
                try
                {
                    var demo = await DemoAsync();
                    if (!Environment.GetCommandLineArgs().Contains("formlar"))
                        foreach (var rol in new[] { "zavod", "xidmet", "idare" }) await RolAsync(rol, demo);
                    // Giriş və admin formaları: ilişsə belə 30 saniyədən sonra davam edilir
                    await Task.WhenAny(FormlarAsync(), Gozle(30000));
                    File.WriteAllText(Path.Combine(Cix, "yer.json"), JsonSerializer.Serialize(Yer, new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping }), Encoding.UTF8);
                    Console.WriteLine($"Hazır: {Yer.Count} şəkil, {Ikonlar.Count} kiçik şəkil");
                }
                catch (Exception ex) { File.WriteAllText(Path.Combine(root, "xeta.txt"), ex.ToString()); Console.WriteLine(ex); }
                Environment.Exit(0);
            };
            Application.Run(surucu);
        }

        // ============================================================ Nümunə məlumat
        sealed record Demo(string Zavod, long V1, long V2, long V3, long Sorgu1, string V1Guid);

        static async Task<Demo> DemoAsync()
        {
            string z = "939 nömrəli hərbi hissə";
            await using (var con = await LocalDb.AcAsync()) { using var c = con.CreateCommand(); c.CommandText = "DELETE FROM Sorgular; UPDATE Texnikalar SET Serfiyyat=NULL, Teminat=NULL;"; await c.ExecuteNonQueryAsync(); }
            var tex = (await SorguRepo.TexnikaAxtarAsync("", z, 0)).Where(x => x.Status == "Təmirdə").Take(3).ToList();
            var id = new long[3];
            for (int i = 0; i < 3; i++) id[i] = (await TexnikaRepo.IdAsync(tex[i].Guid)).Value;
            string T(int gun) => Vaxt.Iso(DateTime.Today.AddDays(gun));
            SorguHisse H(string ad, string kod, string vahid, double teleb, int t, double qebul)
            {
                var h = new SorguHisse { Ad = ad, Kod = kod, Vahid = vahid, Teleb = teleb, Texnika = $"{tex[t].Marka} ({tex[t].ZavodNo})", TexGuid = tex[t].Guid };
                if (qebul > 0) h.Qebullar.Add(new HisseQebul { Tarix = T(-6), Miqdar = qebul });
                return h;
            }
            var s1 = new Sorgu();
            foreach (var (k, v) in new[] { ("MektubNo", "190/26"), ("QeydTarixi", T(-20)), ("TeminEden", "Təchizat idarəsi"), ("TeminIcraci", "mayor Səfərov N."), ("Telefon", "(012) 498-67-35"),
                                            ("IcraMuddeti", T(10)), ("CavabVez", "İcrası davam edir"), ("Zavod", z) }) s1.V[k] = v;
            s1.V["Qeydler"] = JsonSerializer.Serialize(new List<SorguQeyd> { new() { Tarix = T(-8), Netice = "Hissələr anbardadır, 10 gün ərzində göndəriləcək", Gozlenilen = T(2) } });
            s1.His.AddRange(new[] { H("Yanacaq filtri", "740-1117", "ədəd", 4, 0, 4), H("Mühərrik yağı", "M-10", "litr", 40, 0, 25), H("Akkumulyator", "6ST-190", "ədəd", 2, 1, 0) });
            long sid = await SorguRepo.SaxlaAsync(s1);
            var s2 = new Sorgu();
            foreach (var (k, v) in new[] { ("MektubNo", "214/26"), ("QeydTarixi", T(-12)), ("TeminEden", "3 saylı hərbi anbar"), ("TeminIcraci", "kapitan Həsənov E."), ("Telefon", "(012) 498-11-20"),
                                            ("IcraMuddeti", T(18)), ("CavabVez", "İcrası davam edir"), ("Zavod", z) }) s2.V[k] = v;
            s2.His.AddRange(new[] { H("Əyləc qəlibi", "4310-3501090", "dəst", 2, 2, 2), H("Hava filtri", "740-1109", "ədəd", 3, 1, 1) });
            await SorguRepo.SaxlaAsync(s2);
            await TexnikaRepo.SaxlaAsync(id[0], new() { ["Serfiyyat"] = TexnikaJson.Yaz(new List<Serf> {
                new() { Ad = "Yanacaq filtri", Kod = "740-1117", Vahid = "ədəd", Miqdar = 2, Tarix = T(-4), Anbar = true },
                new() { Ad = "Mühərrik yağı", Kod = "M-10", Vahid = "litr", Miqdar = 18, Tarix = T(-3), Anbar = true },
                new() { Ad = "Boya", Vahid = "kq", Miqdar = 3, Tarix = T(-2), Anbar = false } }) });
            await TexnikaRepo.SaxlaAsync(id[2], new() { ["Serfiyyat"] = TexnikaJson.Yaz(new List<Serf> { new() { Ad = "Əyləc qəlibi", Kod = "4310-3501090", Vahid = "dəst", Miqdar = 2, Tarix = T(-1), Anbar = true } }) });
            await TexnikaRepo.SaxlaAsync(id[1], new() { ["Teminat"] = Secimler.StTeminat });
            return new Demo(z, id[0], id[1], id[2], sid, tex[0].Guid);
        }

        // ============================================================ Rollar
        static async Task RolAsync(string rol, Demo d)
        {
            var q = await SistemRepo.QurumlarAsync();
            var qr = rol switch { "zavod" => q.First(x => x.Ad == d.Zavod), "xidmet" => q.First(x => x.Tip == QurumTip.UstQurum), _ => q.First(x => x.Tip == QurumTip.EnUstQurum) };
            Session.Login = "telimat"; Session.AdSoyad = rol == "zavod" ? "Məmmədov Rauf" : rol == "xidmet" ? "Əliyev Kamran" : "Quliyev Samir";
            Session.Rol = rol == "zavod" ? "Zavod istifadəçisi" : "Yalnız baxış"; Session.QurumKod = qr.Kod; Session.QurumAd = qr.Ad; Session.Tip = qr.Tip;
            Directory.CreateDirectory(Path.Combine(Cix, rol));

            var f = new MainForm();
            f.Show();
            f.WindowState = FormWindowState.Normal; f.StartPosition = FormStartPosition.Manual;
            f.Bounds = new Rectangle(-12000, -12000, W, H);
            await Gozle(2500);
            var m = MainForm.Current;
            async Task Ac(string key, Func<PageBase, Task> h = null) { await m.AcAsync(key, h); await Gozle(1200); }
            void C(string ad) => Cek(f, $"{rol}/{ad}.png");

            await Ac("home"); C("01_ana");
            typeof(MainForm).GetMethod("MenyuAc", B).Invoke(f, new object[] { true }); await Gozle(900); C("02_menyu");
            typeof(MainForm).GetMethod("MenyuAc", B).Invoke(f, new object[] { false }); await Gozle(900);
            await Ac("list"); C("03_reyestr");
            await m.DetalAcAsync(d.V1); await Gozle(1500); C("04_genis_bax");

            if (rol == "zavod")
            {
                // Texnika əlavə et: 5 bölmə (nümunə məlumatla doldurulmuş)
                await m.AcAsync("add", p => ((EditPage)p).YukleAsync(null)); await Gozle(1200);
                var e = (EditPage)Sehife(m, "add");
                var inn = (Dictionary<string, Control>)typeof(EditPage).GetField("_in", B).GetValue(e);
                ((ComboBox)inn["Nov"]).Text = "Zirehli texnika"; inn["Marka"].Text = "BMP-2"; inn["ZavodNo"].Text = "KD-688774"; inn["NaryadNo"].Text = "818/26";
                ((DateTimePicker)inn["NaryadTarixi"]).Checked = true; inn["GonderenQurum"].Text = "444 nömrəli hərbi hissə";
                inn["Nasazliqlar"].Text = "Transmissiyada kənar səs, sürət qutusu nasazdır"; inn["IcraMuddeti"].Text = "45";
                var tabs = (TabControl)typeof(EditPage).GetField("tabs", B).GetValue(e);
                for (int i = 0; i < 5; i++) { tabs.SelectedIndex = i; await Gozle(700); C($"05_elave_{i + 1}"); }
                typeof(EditPage).GetField("_ilk", B).SetValue(e, null);   // doldurulmuş nümunə forma: çıxanda "yadda saxlanmamış dəyişiklik" sualı verilməsin
                // Faktiki sərfiyyat: mövcud texnika, panel açıq
                await m.RedakteAcAsync(d.V1, 3); await Gozle(1500);
                var r = (EditPage)Sehife(m, "red");
                typeof(EditPage).GetMethod("SerfPaneli", B).Invoke(r, null); await Gozle(600);
                var malAd = (List<string>)typeof(EditPage).GetField("_malAd", B).GetValue(r);
                if (malAd.Count > 0) typeof(EditPage).GetMethod("MalSec", B).Invoke(r, new object[] { 0 });
                ((Control)typeof(EditPage).GetField("sMiqdar", B).GetValue(r)).Text = "1";
                await Gozle(500); C("05_elave_4_serf");
                await m.RedakteAcAsync(d.V2, 2); await Gozle(1500); C("05_elave_3_teminat");
                // Material axtarışı pəncərəsi
                await DialogCekAsync(rol + "/05_material_axtar.png", "Mühərrik", () =>
                {
                    var mal = new List<string> { "Yanacaq filtri · 740-1117 — qalıq: 2 ədəd", "Mühərrik yağı · M-10 — qalıq: 7 litr", "Hava filtri · 740-1109 — qalıq: 1 ədəd" };
                    Ui.SiyahidanSec(f, "Material / ehtiyat hissəsi", "Ad və ya kod üzrə axtarış", mal);
                });

                // Sorğu yarat
                await m.SorguAcAsync(d.Sorgu1); await Gozle(1500);
                var sp = (SorguYaratPage)Sehife(m, "sorgu");
                C("06_sorgu");
                typeof(SorguYaratPage).GetMethod("MektubPaneli", B).Invoke(sp, new object[] { false }); await Gozle(700); C("06_sorgu_mektub");
                await sp.YukleAsync(d.Sorgu1); await Gozle(800);
                var ph = (Control)typeof(SorguYaratPage).GetField("pnlHisse", B).GetValue(sp); ph.Visible = true;
                ((Control)typeof(SorguYaratPage).GetField("hAd", B).GetValue(sp)).Text = "Hava filtri";
                ((Control)typeof(SorguYaratPage).GetField("hKod", B).GetValue(sp)).Text = "740-1109";
                ((Control)typeof(SorguYaratPage).GetField("hVahid", B).GetValue(sp)).Text = "ədəd";
                ((Control)typeof(SorguYaratPage).GetField("hTeleb", B).GetValue(sp)).Text = "3";
                var tx = (await SorguRepo.TexnikaAxtarAsync("", d.Zavod, 0)).First(x => x.Guid == d.V1Guid);
                typeof(SorguYaratPage).GetField("_hTex", B).SetValue(sp, tx); typeof(SorguYaratPage).GetMethod("TexYaz", B).Invoke(sp, null);
                await Gozle(700); C("06_sorgu_hisse");
                ph.Visible = false;
                typeof(SorguYaratPage).GetMethod("QebulPaneli", B).Invoke(sp, null);
                ((Control)typeof(SorguYaratPage).GetField("qMiqdar", B).GetValue(sp)).Text = "10";
                await Gozle(700); C("06_sorgu_qebul");
                await sp.YukleAsync(d.Sorgu1); await Gozle(800);
                var sc = sp.Controls.OfType<Panel>().First(p => p.AutoScroll);
                sc.AutoScrollPosition = new Point(0, 2000);
                ((Control)typeof(SorguYaratPage).GetField("pnlQeyd", B).GetValue(sp)).Visible = true;
                await Gozle(700); sc.AutoScrollPosition = new Point(0, 2000); await Gozle(500); C("06_sorgu_icra");
                await sp.YukleAsync(null);
            }

            await Ac("tech"); C("07_teminat");
            await Ac("rep"); C("08_hesabat");
            var hp = (HesabatPage)Sehife(m, "rep");
            var nov = (ComboBox)typeof(HesabatPage).GetField("cmbNov", B).GetValue(hp);
            ((DateTimePicker)typeof(HesabatPage).GetField("dtBas", B).GetValue(hp)).Value = DateTime.Today.AddDays(-30);
            nov.SelectedIndex = 4; await Gozle(1500); C("08_hesabat_teminat");
            nov.SelectedIndex = 5; await Gozle(1500); C("08_hesabat_serf");
            if (rol == "zavod")
            {
                var t1 = (await SorguRepo.TexnikaAxtarAsync("", d.Zavod, 0)).First(x => x.Guid == d.V1Guid);
                typeof(HesabatPage).GetField("_tex", B).SetValue(hp, t1);
                await (Task)typeof(HesabatPage).GetMethod("GosterAsync", B).Invoke(hp, null); await Gozle(800); C("08_hesabat_serf_tex");
                typeof(HesabatPage).GetField("_tex", B).SetValue(hp, null);
            }
            nov.SelectedIndex = 0; await Gozle(800);
            await Ac("noti"); C("09_bildiris");
            await Ac("sync"); C("10_usb");
            await Ac("bak"); C("11_ehtiyat");
            await Ac("log"); C("12_loglar");
            f.Close(); f.Dispose(); await Gozle(500);
        }

        static PageBase Sehife(MainForm m, string key) => ((Dictionary<string, PageBase>)typeof(MainForm).GetField("_pages", B).GetValue(m))[key];

        // ============================================================ Ayrıca formalar
        static async Task FormlarAsync()
        {
            var l = new LoginForm { StartPosition = FormStartPosition.Manual, Location = new Point(-12000, -12000), ShowInTaskbar = false };
            l.Show(); await Gozle(2500); Console.WriteLine("login acildi"); Cek(l, "forms/00_giris.png"); l.Hide();
            var a = new AdminForm { StartPosition = FormStartPosition.Manual, Location = new Point(-12000, -12000), ShowInTaskbar = false };
            a.Show(); await Gozle(2500); Cek(a, "forms/21_admin.png"); a.Hide();
        }

        /// <summary>Modal pəncərənin şəkli: taymer pəncərəni tapır, axtarışa mətn yazır (koddan), çəkir və bağlayır.</summary>
        static async Task DialogCekAsync(string ad, string axtar, Action ac)
        {
            var t = new System.Windows.Forms.Timer { Interval = 400 }; int n = 0;
            t.Tick += (_, _) =>
            {
                var d = Application.OpenForms.Cast<Form>().LastOrDefault(x => x.Modal); if (d == null) return;
                if (n++ == 0) { d.Location = new Point(-12000, -12000); var tb = Hamisi(d).OfType<TextBox>().FirstOrDefault(); if (tb != null) tb.Text = axtar; return; }
                t.Stop(); Cek(d, ad); d.DialogResult = DialogResult.Cancel;
            };
            t.Start(); ac(); await Gozle(300);
        }

        // ============================================================ Çəkmə
        static async Task Gozle(int ms) { var t = DateTime.Now.AddMilliseconds(ms); while (DateTime.Now < t) { Application.DoEvents(); await Task.Delay(30); } }

        static IEnumerable<Control> Hamisi(Control k) { foreach (Control c in k.Controls) { yield return c; foreach (var x in Hamisi(c)) yield return x; } }

        static void Cek(Form f, string ad)
        {
            f.Refresh(); Application.DoEvents();
            using var b = new Bitmap(f.Width, f.Height, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(b)) { var hdc = g.GetHdc(); PrintWindow(f.Handle, hdc, 2); g.ReleaseHdc(hdc); }
            b.Save(Path.Combine(Cix, ad), ImageFormat.Png);
            // Elementlərin yerləri (faizlə) və düymələrin kiçik şəkilləri
            var y = new Dictionary<string, double[]>();
            foreach (var c in Hamisi(f))
            {
                if (!c.Visible) continue;
                var r = Gorunen(f, c); if (r.IsEmpty) continue;
                double[] m = { Math.Round((r.X + r.Width / 2.0) * 100 / f.Width, 1), Math.Round((r.Y + r.Height / 2.0) * 100 / f.Height, 1) };
                string t = (c.Text ?? "").Replace("\r", "").Replace("\n", " ").Trim();
                if (c is NavButton nb) { y.TryAdd("menyu:" + nb.PageKey, m); if (r.Width > 150) Ikon(b, r, "menyu_" + nb.PageKey); continue; }
                if (t.Length > 0 && t.Length < 90) y.TryAdd(t, m);
                if (!string.IsNullOrEmpty(c.Name)) y.TryAdd("#" + c.Name, m);
                if (c is FlatButton or CheckBox && t.Length > 0 && r.Width >= c.Width - 2 && r.Height >= c.Height - 2) Ikon(b, r, Slug(t));
            }
            Yer[ad] = y;
            Console.WriteLine(ad);
        }

        /// <summary>Kontrolun formada görünən düzbucaqlısı (valideynlərin sərhədinə görə kəsilir; görünmürsə boş).</summary>
        static Rectangle Gorunen(Form f, Control c)
        {
            var r = new Rectangle(f.PointToClient(c.PointToScreen(Point.Empty)), c.Size);
            for (var p = c.Parent; p != null && p != f; p = p.Parent)
                r.Intersect(new Rectangle(f.PointToClient(p.PointToScreen(Point.Empty)), p.ClientSize));
            r.Intersect(new Rectangle(Point.Empty, f.ClientSize));
            return r.Width < 4 || r.Height < 4 ? Rectangle.Empty : r;
        }

        static void Ikon(Bitmap b, Rectangle r, string ad)
        {
            if (!Ikonlar.Add(ad)) return;
            r.Inflate(2, 2); r.Intersect(new Rectangle(Point.Empty, b.Size));
            using var k = b.Clone(r, b.PixelFormat); k.Save(Path.Combine(Cix, "ikon", ad + ".png"), ImageFormat.Png);
        }

        /// <summary>Faylın adı: "Yadda saxla" → "yadda_saxla" (qur.py-dakı slug ilə eyni qayda).</summary>
        public static string Slug(string t)
        {
            var map = new Dictionary<char, string> { ['ə'] = "e", ['ı'] = "i", ['ö'] = "o", ['ü'] = "u", ['ş'] = "s", ['ç'] = "c", ['ğ'] = "g", ['i'] = "i" };
            var sb = new StringBuilder();
            foreach (char ch0 in t.ToLower(new System.Globalization.CultureInfo("az-Latn-AZ")))
            {
                char ch = ch0 == 'i' ? 'i' : ch0;
                if (map.TryGetValue(ch, out var s)) sb.Append(s); else if (ch is >= 'a' and <= 'z' or >= '0' and <= '9') sb.Append(ch); else sb.Append('_');
            }
            return System.Text.RegularExpressions.Regex.Replace(sb.ToString(), "_+", "_").Trim('_');
        }
    }
}
