namespace TexnikaReyestri.Data
{
    /// <summary>Hazır hesabat: başlıq, sütunlar, sətirlər, (varsa) cəmi sətri və izah.</summary>
    public sealed record Hesabat(string Ad, string[] Sutun, List<string[]> Setir, string[] Cem, string Qeyd);

    public static partial class TexnikaRepo
    {
        public static readonly string[] HesabatNovleri =
        {
            "Ümumi icra vəziyyəti", "Qəbul, təmir və təhvil", "İcra müddəti üzrə nəzarət",
            "Təhvil gözlənilən texnikalar", "Ehtiyat hissələri üzrə təminat", "Texnika üzrə faktiki material sərfi", "Məktubların icra vəziyyəti"
        };
        public static readonly string[] HesabatAdlari =
        {
            "Zavodlar üzrə ümumi icra vəziyyəti", "Dövr ərzində qəbul, təmir və təhvil", "İcra müddəti üzrə nəzarət",
            "Təmiri tamamlanıb, təhvil gözlənilən texnikalar", "Ehtiyat hissələri üzrə təminat", "Texnika üzrə faktiki material sərfi", "Məktubların icra vəziyyəti"
        };
        /// <summary>Dövr (Başlanğıc – Son) istifadə edən hesabatlar.</summary>
        public static bool DovrLazim(int nov) => nov is 1 or 5 or 6;

        /// <summary>nov: HesabatNovleri indeksi. texnika (Guid), material: faktiki sərf hesabatında seçim və axtarış. tamTemin: ehtiyat hissələri hesabatında tam təmin edilmişlər (əks halda qalığı olanlar). zavod: null = bütün zavodlar. tarix: hesabat tarixi (müddət hesabı üçün). d1, d2: dövr.</summary>
        public static async Task<Hesabat> HesabatAsync(int nov, string zavod, DateTime tarix, DateTime d1, DateTime d2, bool tamTemin = false, string texnika = null, string material = null)
        {
            await using var con = await LocalDb.AcAsync();
            using var c = con.CreateCommand();
            c.Parameters.AddWithValue("$z", (object)zavod ?? DBNull.Value);
            c.Parameters.AddWithValue("$b", Vaxt.Iso(tarix)); c.Parameters.AddWithValue("$d1", Vaxt.Iso(d1)); c.Parameters.AddWithValue("$d2", Vaxt.Iso(d2));
            c.Parameters.AddWithValue("$m1", new DateTimeOffset(d1.Date).ToUnixTimeMilliseconds()); c.Parameters.AddWithValue("$m2", new DateTimeOffset(d2.Date.AddDays(1)).ToUnixTimeMilliseconds());
            int n = ReyestrFiltr.Yaxin, m = Math.Max(n + 1, ReyestrFiltr.Sari);
            c.Parameters.AddWithValue("$yaxin", Vaxt.Iso(tarix.AddDays(n))); c.Parameters.AddWithValue("$sari", Vaxt.Iso(tarix.AddDays(m)));
            const string zw = "Silinib=0 AND ($z IS NULL OR TemirZavodu=$z)";
            var zavodlar = Secimler.Zavodlar.Where(z => zavod == null || z == zavod).ToArray();

            async Task<Dictionary<string, long[]>> Say(string sql)
            {
                c.CommandText = sql; var d = new Dictionary<string, long[]>();
                await using var r = await c.ExecuteReaderAsync();
                while (await r.ReadAsync()) { var a = new long[r.FieldCount - 1]; for (int i = 1; i < r.FieldCount; i++) a[i - 1] = r.IsDBNull(i) ? 0 : Convert.ToInt64(r.GetValue(i)); d[r.IsDBNull(0) ? "" : r.GetString(0)] = a; }
                return d;
            }
            string F(double v) => v.ToString("#,0.##").Replace(",", " ");
            Hesabat Cedvel(string[] sutun, Dictionary<string, long[]> d, int k, bool cemSutun, string qeyd)
            {
                var rows = new List<string[]>(); var cem = new long[k];
                foreach (var z in zavodlar)
                {
                    var a = d.GetValueOrDefault(z) ?? new long[k];
                    for (int i = 0; i < k; i++) cem[i] += a[i];
                    rows.Add(new[] { z }.Concat(a.Select(x => F(x))).Concat(cemSutun ? new[] { F(a.Sum()) } : Array.Empty<string>()).ToArray());
                }
                var cs = new[] { "Cəmi" }.Concat(cem.Select(x => F(x))).Concat(cemSutun ? new[] { F(cem.Sum()) } : Array.Empty<string>()).ToArray();
                return new Hesabat(HesabatAdlari[nov], sutun, rows, cs, qeyd);
            }

            switch (nov)
            {
                case 0:
                    return Cedvel(new[] { "Zavod", "Təmiri davam edir", "Təmir tamamlanıb, təhvil gözlənilir", "Təhvil verilib", "Cəmi" },
                        await Say($"SELECT TemirZavodu, SUM({DavamSql}), SUM({GozlSql}), SUM({TehvilSql}) FROM Texnikalar WHERE {zw} GROUP BY 1;"), 3, true,
                        null);
                case 1:
                    var q = await Say($"SELECT TemirZavodu, SUM(DaxilolmaTarixi BETWEEN $d1 AND $d2), SUM(FaktikiBitme BETWEEN $d1 AND $d2) FROM Texnikalar WHERE {zw} GROUP BY 1;");
                    // Təhvil tarixi: statusun "Təhvil verilib"ə dəyişdiyi an (tarixçə); tarixçəsi olmayanlar üçün faktiki bitmə
                    var tv = await Say($@"SELECT TemirZavodu, COUNT(*) FROM Texnikalar t WHERE {zw} AND Status='Təhvil verilib' AND (
  EXISTS (SELECT 1 FROM Tarixce h WHERE h.TexnikaId=t.Id AND h.Sahe='Statusu' AND h.Yeni='Təhvil verilib' AND h.Tarix>=$m1 AND h.Tarix<$m2)
  OR (NOT EXISTS (SELECT 1 FROM Tarixce h WHERE h.TexnikaId=t.Id AND h.Sahe='Statusu' AND h.Yeni='Təhvil verilib') AND FaktikiBitme BETWEEN $d1 AND $d2)) GROUP BY 1;");
                    foreach (var z in zavodlar) q[z] = new[] { q.GetValueOrDefault(z)?[0] ?? 0, q.GetValueOrDefault(z)?[1] ?? 0, tv.GetValueOrDefault(z)?[0] ?? 0 };
                    return Cedvel(new[] { "Zavod", "Dövrdə qəbul edilib", "Dövrdə təmir tamamlanıb", "Dövrdə təhvil verilib" }, q, 3, false,
                        $"Dövr: {d1:dd.MM.yyyy} – {d2:dd.MM.yyyy}");
                case 2:
                    return Cedvel(new[] { "Zavod", $"{n + 1}–{m} gün qalıb", $"0–{n} gün qalıb", "İcra müddəti keçib", "Müəyyən edilməyib", "Cəmi" },
                        await Say($@"SELECT TemirZavodu, SUM(GozlenilenBitme > $yaxin AND GozlenilenBitme <= $sari), SUM(GozlenilenBitme >= $b AND GozlenilenBitme <= $yaxin),
  SUM(GozlenilenBitme < $b AND GozlenilenBitme > ''), SUM(IFNULL(GozlenilenBitme,'')='') FROM Texnikalar WHERE {zw} AND {DavamSql} GROUP BY 1;"), 4, false,
                        "Yalnız təmiri davam edən texnikalar. Hədlər Admin → Sistem bölməsindən götürülür.");
                case 3:
                    c.CommandText = $"SELECT TemirZavodu, IFNULL(Marka,''), IFNULL(ZavodNo,''), FaktikiBitme, CAST(julianday($b)-julianday(FaktikiBitme) AS INTEGER) FROM Texnikalar " +
                                    $"WHERE {zw} AND {GozlSql} ORDER BY FaktikiBitme LIMIT 5000;";
                    var l = new List<string[]>();
                    await using (var r = await c.ExecuteReaderAsync())
                        while (await r.ReadAsync()) l.Add(new[] { r.IsDBNull(0) ? "" : r.GetString(0), r.GetString(1), r.GetString(2), Vaxt.Tarix(r.GetValue(3)), r.IsDBNull(4) ? "" : r.GetInt64(4).ToString() });
                    return new Hesabat(HesabatAdlari[nov], new[] { "Zavod", "Texnika", "Zavod nömrəsi", "Faktiki bitmə", "Gözləyir (gün)" }, l, new[] { "Cəmi", l.Count + " texnika", "", "", "" },
                        "Faktiki bitmə tarixi yazılıb, status hələ \"Təhvil verilib\" deyil");
                case 4:
                    // Bölgü hər ehtiyat hissəsi üzrədir (məktubun hissələri): eyni məktubun hissələri hər iki siyahıda ola bilər. Qalıq = tələb olunan − qəbul edilmiş.
                    var hr = new List<string[]>();
                    foreach (var x in await SorguRepo.TeminatAsync(zavod))
                    {
                        double qal = Math.Max(0, x.Teleb - x.Qebul);
                        if ((qal <= 0) != tamTemin) continue;
                        hr.Add(new[] { $"{x.MektubNo} · {Vaxt.Tarix(x.MektubTarix)}", x.Texnika ?? "", x.Ad + (string.IsNullOrEmpty(x.Kod) ? "" : " / " + x.Kod), x.Vahid, F(x.Teleb), F(x.Qebul), F(qal), x.TeminEden });
                    }
                    return new Hesabat(HesabatAdlari[nov], new[] { "Məktubun nömrəsi və tarixi", "Hansı texnika üçün", "Ehtiyat hissəsinin adı / kodu", "Ölçü vahidi", "Tələb olunan", "Qəbul edilmiş", "Qalıq", "Təmin edən qurum" }, hr, null,
                        $"{Core.Lang.T(tamTemin ? "Tam təmin edilmiş ehtiyat hissələri" : "Təmin edilməmiş ehtiyat hissələri")}: {hr.Count}");
                case 5:
                    // Texnika kartlarındakı "Faktiki sərfiyyat" qeydləri: sərf tarixi dövrdədir; texnika və material üzrə cəmlənir
                    // texnika: seçilmiş texnikanın Guid-i (null — hamısı)
                    c.CommandText = $"SELECT IFNULL(Marka,''), IFNULL(ZavodNo,''), Serfiyyat FROM Texnikalar WHERE {zw} AND IFNULL(Serfiyyat,'')<>'' AND ($tg IS NULL OR Guid=$tg) ORDER BY Marka, ZavodNo;";
                    c.Parameters.AddWithValue("$tg", (object)texnika ?? DBNull.Value);
                    var sr = new List<string[]>();
                    string mq = (material ?? "").Trim();
                    await using (var r = await c.ExecuteReaderAsync())
                        while (await r.ReadAsync())
                        {
                            string tex = $"{r.GetString(0)} ({r.GetString(1)})";
                            foreach (var g in TexnikaJson.Serfler(r.GetString(2))
                                         .Where(x => string.CompareOrdinal(x.Tarix ?? "", Vaxt.Iso(d1)) >= 0 && string.CompareOrdinal(x.Tarix ?? "", Vaxt.Iso(d2)) <= 0)
                                         .Where(x => mq.Length == 0 || (x.Ad ?? "").Contains(mq, StringComparison.CurrentCultureIgnoreCase) || (x.Kod ?? "").Contains(mq, StringComparison.CurrentCultureIgnoreCase))
                                         .GroupBy(x => (Ad: (x.Ad ?? "").Trim(), Kod: (x.Kod ?? "").Trim(), Vahid: (x.Vahid ?? "").Trim())))
                                sr.Add(new[] { tex, g.Key.Ad + (g.Key.Kod.Length == 0 ? "" : " / " + g.Key.Kod), g.Key.Vahid, F(g.Sum(x => x.Miqdar)) });
                        }
                    return new Hesabat(HesabatAdlari[nov], new[] { "Texnika / zavod nömrəsi", "Materialın və ya ehtiyat hissəsinin adı", "Ölçü vahidi", "Faktiki sərf edilmiş miqdar" }, sr, null,
                        $"{Core.Lang.T("Sərf tarixi")}: {d1:dd.MM.yyyy} – {d2:dd.MM.yyyy}. {Core.Lang.T("Məlumat texnika kartındakı \"Faktiki sərfiyyat\" qeydlərindən avtomatik gəlir")}.");
                default:
                    return Cedvel(new[] { "Zavod", "Cəmi məktub", "İcrası davam edir", "İcra başa çatıb", "İcra müddəti keçib" },
                        await Say(@"SELECT Zavod, COUNT(*), SUM(IFNULL(CavabVez,'İcrası davam edir')='İcrası davam edir'), SUM(CavabVez='İcra başa çatıb'),
  SUM(IFNULL(CavabVez,'İcrası davam edir')='İcrası davam edir' AND IcraMuddeti < $b) FROM Sorgular WHERE Silinib=0 AND ($z IS NULL OR Zavod=$z) AND QeydTarixi BETWEEN $d1 AND $d2 GROUP BY 1;"), 4, false,
                        $"Dövr: məktubun qeydiyyat tarixi {d1:dd.MM.yyyy} – {d2:dd.MM.yyyy}");
            }
        }

        /// <summary>Son paketi 7 gündən köhnə olan zavodlar (üst qurumda): "Zavod üzrə məlumat dd.MM.yyyy tarixinə aiddir".</summary>
        public static async Task<List<string>> KohneMelumatAsync(string zavod)
        {
            var l = new List<string>();
            if (Session.Tip == QurumTip.Zavod) return l;
            var dt = await ZavodIcmalAsync();
            foreach (System.Data.DataRow r in dt.Rows)
            {
                if (zavod != null && (string)r["Zavod"] != zavod) continue;
                if (r["SonPaket"] is long t)
                {
                    var d = DateTimeOffset.FromUnixTimeMilliseconds(t).LocalDateTime;
                    if ((DateTime.Now - d).TotalDays >= 7) l.Add(string.Format(Core.Lang.T("{0} üzrə məlumat {1} tarixinə aiddir."), Core.Lang.T((string)r["Zavod"]), d.ToString("dd.MM.yyyy")));
                }
                else l.Add(string.Format(Core.Lang.T("{0} üzrə hələ paket idxal olunmayıb."), Core.Lang.T((string)r["Zavod"])));
            }
            return l;
        }
    }
}
