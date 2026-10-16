namespace TexnikaReyestri.Data
{
    /// <summary>Hazır hesabat: başlıq, sütunlar, sətirlər, (varsa) cəmi sətri və izah.</summary>
    public sealed record Hesabat(string Ad, string[] Sutun, List<string[]> Setir, string[] Cem, string Qeyd);

    public static partial class TexnikaRepo
    {
        public static readonly string[] HesabatNovleri =
        {
            "Ümumi icra vəziyyəti", "Qəbul, təmir və təhvil", "İcra müddəti üzrə nəzarət",
            "Təhvil gözlənilən texnikalar", "Ehtiyat hissələri üzrə təminat", "Məktubların icra vəziyyəti"
        };
        public static readonly string[] HesabatAdlari =
        {
            "Zavodlar üzrə ümumi icra vəziyyəti", "Dövr ərzində qəbul, təmir və təhvil", "İcra müddəti üzrə nəzarət",
            "Təmiri tamamlanıb, təhvil gözlənilən texnikalar", "Ehtiyat hissələri üzrə təminat", "Məktubların icra vəziyyəti"
        };
        /// <summary>Dövr (Başlanğıc – Son) istifadə edən hesabatlar.</summary>
        public static bool DovrLazim(int nov) => nov is 1 or 5;

        /// <summary>nov: HesabatNovleri indeksi. zavod: null = bütün zavodlar. tarix: hesabat tarixi (müddət hesabı üçün). d1, d2: dövr.</summary>
        public static async Task<Hesabat> HesabatAsync(int nov, string zavod, DateTime tarix, DateTime d1, DateTime d2)
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
                        "Cəmi = təmiri davam edən + təhvil gözlənilən + təhvil verilmiş");
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
                    c.CommandText = "SELECT Zavod, Hisseler FROM Sorgular WHERE Silinib=0 AND ($z IS NULL OR Zavod=$z);";
                    var h = new Dictionary<string, double[]>();
                    await using (var r = await c.ExecuteReaderAsync())
                        while (await r.ReadAsync())
                        {
                            var a = h.TryGetValue(r.IsDBNull(0) ? "" : r.GetString(0), out var x) ? x : h[r.IsDBNull(0) ? "" : r.GetString(0)] = new double[3];
                            a[0]++; foreach (var s in SorguRepo.HisseJson(r.IsDBNull(1) ? null : r.GetString(1))) { a[1] += s.Teleb; a[2] += s.Qebul; }
                        }
                    var hr = new List<string[]>(); var hc = new double[4];
                    foreach (var z in zavodlar)
                    {
                        var a = h.GetValueOrDefault(z) ?? new double[3]; double qal = Math.Max(0, a[1] - a[2]);
                        hc[0] += a[0]; hc[1] += a[1]; hc[2] += a[2]; hc[3] += qal;
                        hr.Add(new[] { z, F(a[0]), F(a[1]), F(a[2]), F(qal) });
                    }
                    return new Hesabat(HesabatAdlari[nov], new[] { "Zavod", "Sorğu sayı", "Tələb olunan", "Qəbul edilmiş", "Qalıq" }, hr, new[] { "Cəmi" }.Concat(hc.Select(F)).ToArray(),
                        "Təchizat sorğularındakı ehtiyat hissələrinin cəmi (ölçü vahidindən asılı olmayaraq)");
                default:
                    return Cedvel(new[] { "Zavod", "Cəmi məktub", "İcrası davam edir", "Cavab verilib", "İcra müddəti keçib" },
                        await Say(@"SELECT Zavod, COUNT(*), SUM(IFNULL(CavabVez,'İcrası davam edir')='İcrası davam edir'), SUM(CavabVez='Cavab verilib'),
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
                    if ((DateTime.Now - d).TotalDays >= 7) l.Add($"{r["Zavod"]} üzrə məlumat {d:dd.MM.yyyy} tarixinə aiddir.");
                }
                else l.Add($"{r["Zavod"]} üzrə hələ paket idxal olunmayıb.");
            }
            return l;
        }
    }
}
