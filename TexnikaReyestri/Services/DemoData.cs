using TexnikaReyestri.Data;

namespace TexnikaReyestri.Services
{
    /// <summary>Sürət sınağı üçün nümunə məlumat (Admin → Sistem). Zavod nömrələri "NÜMUNƏ-" ilə başlayır.</summary>
    public static class DemoData
    {
        private static readonly string[] Nov = { "Əsas döyüş tankı", "Piyadaların döyüş maşını", "Zirehli transportyor", "Təkərli texnika", "Özüyeriyən artilleriya qurğusu" };
        private static readonly string[] Marka = { "T-72 B", "T-72 M-1", "BMP-2", "BMP-1", "BTR-80", "BTR-70", "2S1", "KamAZ-4310" };
        private static readonly string[] St = { "Qəbul edilib", "Təmirdə", "Təmirdə", "Ehtiyat hissələrinin təminatı gözlənilir", "Təhvil verilib", "Təhvil verilib", "Təmirə yararsız" };
        private static readonly string[] Mer = { "Qəbul", "Defekt aşkarlama", "Hissə təminatı", "Təmir", "Sınaq", "Təhvil" };

        public static Task<int> YaratAsync(int say, string zavod, IProgress<int> p, CancellationToken ct)
        {
            var rnd = new Random(42);
            IEnumerable<Dictionary<string, object>> Qeydler()
            {
                for (int i = 0; i < say; i++)
                {
                    var dax = DateTime.Today.AddDays(-rnd.Next(1, 400)); var bas = dax.AddDays(rnd.Next(1, 10));
                    string s = St[rnd.Next(St.Length)];
                    yield return new()
                    {
                        ["Nov"] = Nov[rnd.Next(Nov.Length)], ["Marka"] = Marka[rnd.Next(Marka.Length)], ["ZavodNo"] = $"NÜMUNƏ-{rnd.Next(100000, 999999)}{i}",
                        ["IstehsalIli"] = (double)rnd.Next(1975, 2005), ["Derece"] = new[] { "I", "II", "III", "IV" }[rnd.Next(4)],
                        ["Km"] = (double)rnd.Next(100, 90000), ["Motosaat"] = (double)rnd.Next(50, 5000), ["Status"] = s,
                        ["TemirNovu"] = new[] { "Cari", "Orta", "Əsaslı" }[rnd.Next(3)], ["Merhele"] = s == "Təhvil verilib" ? "Təhvil" : Mer[rnd.Next(5)],
                        ["TemirZavodu"] = zavod ?? Secimler.Zavodlar[rnd.Next(Secimler.Zavodlar.Length)], ["DaxilolmaTarixi"] = Vaxt.Iso(dax), ["BaslamaTarixi"] = Vaxt.Iso(bas),
                        ["GozlenilenBitme"] = Vaxt.Iso(bas.AddDays(rnd.Next(20, 160))),
                        ["FaktikiBitme"] = s == "Təhvil verilib" ? Vaxt.Iso(bas.AddDays(rnd.Next(20, 150))) : null,
                        ["Qeyd"] = "Nümunə məlumat (sürət sınağı)"
                    };
                }
            }
            return TexnikaRepo.TopluYazAsync(Qeydler(), p, ct);
        }

        /// <summary>
        /// Sınaq üçün 10 real görünüşlü texnika (bütün sahələr doldurulub). Baza boşdursa ilk girişdə bir dəfə avtomatik yazılır;
        /// Admin → PC → "10 nümunə texnika" ilə yenidən əlavə etmək olar. zavod = null → Zavod 1…7 arasında paylanır.
        /// </summary>
        public static Task<int> On10Async(string zavod)
        {
            var t = DateTime.Today;
            string D(int gun) => Vaxt.Iso(t.AddDays(gun));
            object[][] r =
            {   //  Növ, Marka, Zavod №, İl, İstehsalçı, Dərəcə, Km, Motosaat, Atış, Çap, Top markası, Status, Təmir növü, Mərhələ, Daxil, Başlama, Gözlənilən, Faktiki
                new object[] { "Əsas döyüş tankı", "T-72 M-1", "UB-421537", 1986, "Uralvaqonzavod", "II", 6420, 1180, 312, 125, "2A46M", "Təmirdə", "Orta", "Təmir", -64, -58, 26, null },
                new object[] { "Əsas döyüş tankı", "T-72 B", "KA-703118", 1989, "Uralvaqonzavod", "III", 8915, 1530, 488, 125, "2A46M", "Ehtiyat hissələrinin təminatı gözlənilir", "Əsaslı", "Hissə təminatı", -121, -110, -5, null },
                new object[] { "Piyadaların döyüş maşını", "BMP-2", "MP-552904", 1991, "Kurqanmaşzavod", "II", 12480, 2210, 0, 30, "2A42", "Təmirdə", "Cari", "Defekt aşkarlama", -12, -9, 40, null },
                new object[] { "Piyadaların döyüş maşını", "BMP-1", "MP-118063", 1982, "Kurqanmaşzavod", "IV", 21300, 3460, 0, 73, "2A28", "Təhvil verilib", "Orta", "Təhvil", -190, -184, -70, -76 },
                new object[] { "Zirehli transportyor", "BTR-80", "ZT-904417", 1994, "Arzamas maşınqayırma zavodu", "II", 31750, 0, 0, 14.5, "KPVT", "Qəbul edilib", "Cari", "Qəbul", -3, null, 45, null },
                new object[] { "Zirehli transportyor", "BTR-70", "ZT-330291", 1984, "Qorki avtomobil zavodu", "III", 44120, 0, 0, 14.5, "KPVT", "Təmirdə", "Orta", "Sınaq", -88, -80, 6, null },
                new object[] { "Özüyeriyən artilleriya qurğusu", "2S1 Qvozdika", "AQ-271650", 1979, "Xarkov traktor zavodu", "III", 9870, 1720, 640, 122, "2A31", "Təmirdə", "Əsaslı", "Təmir", -150, -141, -12, null },
                new object[] { "Özüyeriyən artilleriya qurğusu", "2S3 Akasiya", "AQ-604839", 1983, "Uralvaqonzavod", "II", 7640, 1395, 420, 152, "2A33", "Təmirə yararsız", "Əsaslı", "Defekt aşkarlama", -40, -36, null, null },
                new object[] { "Təkərli texnika", "KamAZ-4310", "TT-845120", 1997, "KamAZ", "I", 58300, 0, 0, null, null, "Təhvil verilib", "Cari", "Təhvil", -60, -57, -25, -28 },
                new object[] { "Təkərli texnika", "Ural-4320", "TT-219774", 1992, "UralAZ", "II", 73900, 0, 0, null, null, "Qəbul edilib", "Orta", "Qəbul", -1, null, 60, null },
            };
            string[] vez = { "Qənaətbəxş", "Təmir tələb edir", "Nasaz, dəyişdirilməlidir", "Yaxşı" };
            IEnumerable<Dictionary<string, object>> Qeydler()
            {
                for (int i = 0; i < r.Length; i++)
                {
                    var x = r[i]; string st = (string)x[11]; int? Gun(int k) => x[k] == null ? null : Convert.ToInt32(x[k]);
                    yield return new()
                    {
                        ["Nov"] = x[0], ["Marka"] = x[1], ["ZavodNo"] = x[2], ["IstehsalIli"] = Convert.ToDouble(x[3]), ["IstehsalciZavod"] = x[4], ["Derece"] = x[5],
                        ["OrtaTemir"] = i % 3 == 0 ? "2014" : "2011", ["EsasliTemir"] = i % 2 == 0 ? "2006" : null, ["HerbiHisse"] = $"{(i + 1) * 111:000} saylı h/h",
                        ["Km"] = Convert.ToDouble(x[6]), ["Motosaat"] = Convert.ToDouble(x[7]), ["Atislar"] = Convert.ToDouble(x[8]),
                        ["TopCapi"] = x[9] == null ? null : Convert.ToDouble(x[9]), ["TopMarka"] = x[10], ["LuleNo"] = x[9] == null ? null : $"L-{58000 + i * 731}",
                        ["VMuherrik"] = vez[i % 4], ["VSassi"] = vez[(i + 1) % 4], ["VLule"] = x[9] == null ? null : vez[(i + 2) % 4], ["VAis"] = x[9] == null ? null : vez[(i + 3) % 4],
                        ["VOptik"] = x[9] == null ? null : vez[i % 4], ["VDiger"] = i % 2 == 0 ? "Elektrik naqilləri yoxlanılmalıdır" : null, ["Qeyd"] = "Nümunə qeyd (sınaq üçün)",
                        ["Status"] = st, ["TemirNovu"] = x[12], ["KritikHisse"] = st == "Ehtiyat hissələrinin təminatı gözlənilir" ? "Yoxdur" : i % 3 == 0 ? "Qismən" : "Var",
                        ["BaslamaTarixi"] = Gun(15) is int b ? D(b) : null, ["GozlenilenBitme"] = Gun(16) is int g ? D(g) : null,
                        ["GecikmeSebebi"] = Gun(16) is int g2 && g2 < 0 && Gun(17) == null ? "Ehtiyat hissələrin gecikməsi" : null,
                        ["Merhele"] = x[13], ["YararsizSebeb"] = st == "Təmirə yararsız" ? "Gövdədə bərpaedilməz deformasiya" : null, ["TemirQeyd"] = null,
                        ["TemirZavodu"] = zavod ?? Secimler.Zavodlar[i % Secimler.Zavodlar.Length], ["DaxilolmaTarixi"] = D(Gun(14).Value), ["GonderenQurum"] = $"{(i + 1) * 111:000} saylı h/h",
                        ["QebulSenedi"] = $"QS-{2026}/{100 + i} ({Vaxt.Iso(t.AddDays(Gun(14).Value))})", ["IcraMuddeti"] = Gun(16) is int g3 && Gun(14) is int d0 ? (double)(g3 - d0) : null,
                        ["PlanDeyisikliyi"] = null, ["MesulIcraci"] = new[] { "kapitan Məmmədov R.", "baş leytenant Əliyev T.", "mayor Hüseynov E." }[i % 3],
                        ["TelebHisseler"] = st == "Ehtiyat hissələrinin təminatı gözlənilir" ? "Yanacaq nasosu 1 əd.; Sürtünmə diski 6 əd." : null, ["Mektublar"] = st == "Ehtiyat hissələrinin təminatı gözlənilir" ? $"M-{410 + i}" : null,
                        ["TeminEden"] = st == "Ehtiyat hissələrinin təminatı gözlənilir" ? "Təchizat idarəsi" : null, ["SorguStatus"] = st == "Ehtiyat hissələrinin təminatı gözlənilir" ? "Cavab gözlənilir" : null,
                        ["HisseDaxilolma"] = null, ["Senedler"] = null,
                        ["FaktikiBitme"] = Gun(17) is int f ? D(f) : null, ["TehvilSenedi"] = Gun(17) is int f2 ? $"TS-2026/{200 + i} ({D(f2)})" : null,
                    };
                }
            }
            return TexnikaRepo.TopluYazAsync(Qeydler(), null, default);
        }
    }
}
