using TexnikaReyestri.Data;

namespace TexnikaReyestri.Services
{
    /// <summary>Sürət sınağı üçün nümunə məlumat (Admin → Sistem). Zavod nömrələri "NÜMUNƏ-" ilə başlayır.</summary>
    public static class DemoData
    {
        private static readonly string[] Nov = { "Zirehli texnika", "Zirehli texnika", "Nəqliyyat vasitəsi", "Digər texnika" };
        private static readonly string[] Marka = { "T-72 B", "T-72 M-1", "BMP-2", "BMP-1", "BTR-80", "BTR-70", "2S1", "KamAZ-4310" };
        private static readonly string[] St = { "Qəbul edilib", "Təmirdə", "Təmirdə", "Təmirdə", "Təmir tamamlanıb, təhvil gözlənilir", "Təhvil verilib", "Təhvil verilib", "Təmirə yararsız" };

        public static Task<int> YaratAsync(int say, string zavod, IProgress<int> p, CancellationToken ct)
        {
            var rnd = new Random(42);
            IEnumerable<Dictionary<string, object>> Qeydler()
            {
                for (int i = 0; i < say; i++)
                {
                    var dax = DateTime.Today.AddDays(-rnd.Next(1, 400)); var bas = dax.AddDays(rnd.Next(1, 10)); int mud = rnd.Next(20, 160);
                    string s = St[rnd.Next(St.Length)];
                    var fakt = s is "Təhvil verilib" or "Təmir tamamlanıb, təhvil gözlənilir" ? bas.AddDays(rnd.Next(20, 150)) : (DateTime?)null;
                    yield return new()
                    {
                        ["Nov"] = Nov[rnd.Next(Nov.Length)], ["Marka"] = Marka[rnd.Next(Marka.Length)], ["ZavodNo"] = $"NÜMUNƏ-{rnd.Next(100000, 999999)}{i}",
                        ["NaryadNo"] = $"{rnd.Next(100, 999)}/{dax:yy}", ["NaryadTarixi"] = Vaxt.Iso(dax.AddDays(-rnd.Next(1, 10))), ["GonderenQurum"] = $"{rnd.Next(100, 999)} nömrəli hərbi hissə",
                        ["IstehsalIli"] = (double)rnd.Next(1975, 2005), ["Kateqoriya"] = new[] { "I", "II", "III", "IV" }[rnd.Next(4)],
                        ["Gostericiler"] = $"[{{\"Nov\":\"Gediş, km\",\"Miqdar\":{rnd.Next(100, 90000)}}}]", ["Status"] = s,
                        ["TemirNovu"] = new[] { "Cari təmir", "Orta təmir", "Əsaslı təmir" }[rnd.Next(3)], ["Nasazliqlar"] = "Nümunə məlumat (sürət sınağı)",
                        ["TemirZavodu"] = zavod ?? Secimler.Zavodlar[rnd.Next(Secimler.Zavodlar.Length)], ["DaxilolmaTarixi"] = Vaxt.Iso(dax), ["BaslamaTarixi"] = Vaxt.Iso(bas),
                        ["IcraMuddeti"] = (double)mud, ["MuddetVahidi"] = "Gün", ["GozlenilenBitme"] = Vaxt.Iso(bas.AddDays(mud)),
                        ["FaktikiBitme"] = fakt is DateTime f ? Vaxt.Iso(f) : null,
                        ["TehvilTarixi"] = s == "Təhvil verilib" && fakt is DateTime f2 ? Vaxt.Iso(f2.AddDays(rnd.Next(1, 20))) : null,
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
            {   //  Növ, Marka, Zavod №, İl, Kateqoriya, Km, Motosaat, Atış, Status, Təmir növü, Daxil, Diaqnostika, Müddət (gün), Faktiki tamamlanma, Təhvil
                new object[] { "Zirehli texnika", "T-72 M-1", "UB-421537", 1986, "II", 6420, 1180, 312, "Təmirdə", "Orta təmir", -64, -58, 84, null, null },
                new object[] { "Zirehli texnika", "T-72 B", "KA-703118", 1989, "III", 8915, 1530, 488, "Təmirdə", "Əsaslı təmir", -121, -110, 105, null, null },
                new object[] { "Zirehli texnika", "BMP-2", "MP-552904", 1991, "II", 12480, 2210, 0, "Təmirdə", "Cari təmir", -12, -9, 49, null, null },
                new object[] { "Zirehli texnika", "BMP-1", "MP-118063", 1982, "IV", 21300, 3460, 0, "Təhvil verilib", "Orta təmir", -190, -184, 114, -76, -70 },
                new object[] { "Zirehli texnika", "BTR-80", "ZT-904417", 1994, "II", 31750, 0, 0, "Qəbul edilib", "Cari təmir", -3, -2, 45, null, null },
                new object[] { "Zirehli texnika", "BTR-70", "ZT-330291", 1984, "III", 44120, 0, 0, "Təmirdə", "Nasazlıqların aradan qaldırılması", -88, -80, 86, null, null },
                new object[] { "Zirehli texnika", "2S1 Qvozdika", "AQ-271650", 1979, "III", 9870, 1720, 640, "Təmir tamamlanıb, təhvil gözlənilir", "Əsaslı təmir", -150, -141, 129, -9, null },
                new object[] { "Zirehli texnika", "2S3 Akasiya", "AQ-604839", 1983, "II", 7640, 1395, 420, "Təmirə yararsız", "Əsaslı təmir", -40, -36, 30, null, null },
                new object[] { "Nəqliyyat vasitəsi", "KamAZ-4310", "TT-845120", 1997, "I", 58300, 0, 0, "Təhvil verilib", "Cari təmir", -60, -57, 32, -28, -25 },
                new object[] { "Nəqliyyat vasitəsi", "Ural-4320", "TT-219774", 1992, "II", 73900, 0, 0, "Qəbul edilib", "Orta təmir", -1, -1, 61, null, null },
            };
            IEnumerable<Dictionary<string, object>> Qeydler()
            {
                for (int i = 0; i < r.Length; i++)
                {
                    var x = r[i]; string st = (string)x[8]; int? Gun(int k) => x[k] == null ? null : Convert.ToInt32(x[k]);
                    var gl = new List<Gosterici> { new() { Nov = "Gediş, km", Miqdar = Convert.ToDouble(x[5]) } };
                    if (Convert.ToDouble(x[6]) > 0) gl.Add(new() { Nov = "Motosaat", Miqdar = Convert.ToDouble(x[6]) });
                    if (Convert.ToDouble(x[7]) > 0) gl.Add(new() { Nov = "Atəş sayı", Miqdar = Convert.ToDouble(x[7]) });
                    string gonderen = $"{(i + 1) * 111:000} nömrəli hərbi hissə";
                    yield return new()
                    {
                        ["Nov"] = x[0], ["Marka"] = x[1], ["ZavodNo"] = x[2], ["IstehsalIli"] = Convert.ToDouble(x[3]), ["Kateqoriya"] = x[4],
                        ["NaryadNo"] = $"{800 + i}/26", ["NaryadTarixi"] = D(Gun(10).Value - 3), ["DaxilolmaTarixi"] = D(Gun(10).Value), ["GonderenQurum"] = gonderen,
                        ["AktForma"] = i % 2 == 0 ? "Forma 12" : "Forma 12 (RAS)", ["AktNo"] = $"A-{120 + i}", ["AktTarixi"] = D(Gun(10).Value),
                        ["TemirZavodu"] = zavod ?? Secimler.Zavodlar[i % Secimler.Zavodlar.Length], ["MesulIcraci"] = new[] { "kapitan Məmmədov R.", "baş leytenant Əliyev T.", "mayor Hüseynov E." }[i % 3],
                        ["IstismarTarixi"] = $"{x[3]}-06-01", ["Gostericiler"] = TexnikaJson.Yaz(gl),
                        ["SonTemirNovu"] = i % 2 == 0 ? "Orta təmir" : "Cari təmir", ["SonTemirTarixi"] = $"{2014 + i % 5}-03-15", ["SonTemirIcraci"] = $"{(i + 3) * 37:000} nömrəli hərbi hissə",
                        ["Nasazliqlar"] = st == "Təmirə yararsız" ? "Gövdədə bərpaedilməz deformasiya" : "Mühərrikdə yağ sızması; transmissiyada səs; elektrik naqilləri yoxlanılmalıdır",
                        ["BaslamaTarixi"] = D(Gun(11).Value), ["TemirNovu"] = x[9], ["Status"] = st,
                        ["IcraMuddeti"] = Convert.ToDouble(x[12]), ["MuddetVahidi"] = "Gün", ["GozlenilenBitme"] = D(Gun(11).Value + Gun(12).Value),
                        ["GecikmeSebebi"] = Gun(11) + Gun(12) < 0 && Gun(13) == null ? "Ehtiyat hissələrin gecikməsi" : null,
                        ["FaktikiBitme"] = Gun(13) is int f ? D(f) : null, ["GorulenIsler"] = Gun(13) != null ? "Təmir işləri tam yerinə yetirilib, sınaqdan keçirilib" : null,
                        ["TehvilTarixi"] = Gun(14) is int tv ? D(tv) : null, ["TehvilSenedNo"] = Gun(14) != null ? $"TS-2026/{200 + i}" : null,
                        ["TehvilSenedTarixi"] = Gun(14) is int tv2 ? D(tv2) : null, ["TehvilAlan"] = Gun(14) != null ? gonderen : null,
                    };
                }
            }
            return TexnikaRepo.TopluYazAsync(Qeydler(), null, default);
        }

        /// <summary>Sınaq üçün təchizat sorğuları: hər zavodda (və ya yalnız verilən zavodda) bazadakı texnikalara bağlı məktublar və ehtiyat hissələri.</summary>
        public static async Task<int> SorgularAsync(string zavod, int hereZavod)
        {
            var r = new Random(); int say = 0;
            string[] hisse = { "Yağ filtri", "Yanacaq filtri", "Yanacaq nasosu", "Sürtünmə diski", "Akkumulyator", "Hava filtri", "Generator", "Tırtıl zənciri" };
            string[] qurum = { "Təminat idarəsi", "Təchizat idarəsi", "Logistika şöbəsi" };
            foreach (var z in Secimler.Zavodlar.Where(x => zavod == null || x == zavod))
            {
                var tex = await SorguRepo.TexnikaAxtarAsync("", z);
                if (tex.Count == 0) continue;
                for (int i = 0; i < hereZavod; i++)
                {
                    var qeyd = DateTime.Today.AddDays(-r.Next(0, 120)); bool cavab = r.Next(3) == 0;
                    var s = new Sorgu();
                    s.V["MektubNo"] = $"{r.Next(100, 999)}/{qeyd:yy}"; s.V["QeydTarixi"] = Vaxt.Iso(qeyd); s.V["Zavod"] = z;
                    s.V["IcraMuddeti"] = Vaxt.Iso(qeyd.AddDays(r.Next(10, 60))); s.V["TeminEden"] = qurum[r.Next(qurum.Length)];
                    s.V["TeminIcraci"] = "İcraçı " + (char)('A' + r.Next(5)); s.V["ZavodMesul"] = "İcraçı " + r.Next(1, 6);
                    s.V["CavabVez"] = cavab ? "İcra başa çatıb" : "İcrası davam edir"; s.V["Telefon"] = $"(012) 4{r.Next(10, 99)}-{r.Next(10, 99)}-{r.Next(10, 99)}";
                    s.V["Qeydler"] = System.Text.Json.JsonSerializer.Serialize(new List<SorguQeyd> { new() { Tarix = Vaxt.Iso(qeyd.AddDays(r.Next(1, 10))), Netice = "Hissələr anbardadır, göndəriləcək", Gozlenilen = Vaxt.Iso(qeyd.AddDays(r.Next(10, 30))) } });
                    s.Tex = tex.OrderBy(_ => r.Next()).Take(r.Next(1, 3)).ToList();
                    await SorguRepo.SaxlaAsync(s); say++;
                    // Ehtiyat hissələri texnikanın öz siyahısına yazılır; məktubun hissələri və nəticəsi oradan toplanır
                    foreach (var t in s.Tex)
                    {
                        if (await TexnikaRepo.IdAsync(t.Guid) is not long tid) continue;
                        var h = TexnikaJson.Hisseler((await TexnikaRepo.TapAsync(tid))?.GetValueOrDefault("Hisseler"));
                        for (int k = r.Next(1, 3); k > 0; k--)
                        {
                            int teleb = r.Next(1, 8), qebul = r.Next(0, teleb + 1); var y = new Hisse { Ad = hisse[r.Next(hisse.Length)], Vahid = "ədəd", Teleb = teleb };
                            if (qebul > 0) y.Qebullar.Add(new HisseQebul { Tarix = Vaxt.Iso(qeyd.AddDays(r.Next(1, 30))), Miqdar = qebul });
                            h.Add(y);
                        }
                        await TexnikaRepo.SaxlaAsync(tid, new() { ["Hisseler"] = TexnikaJson.Yaz(h), ["Teminat"] = TexnikaJson.Teminat(h) });
                    }
                }
            }
            return say;
        }
    }
}
