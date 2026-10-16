using System.Globalization;
using System.Text.RegularExpressions;
using ClosedXML.Excel;
using TexnikaReyestri.Data;

namespace TexnikaReyestri.Services
{
    public enum SetirVeziyyet { Yeni, Yenilenecek, Xeberdarliq, Xeta, Bos }

    public sealed class IdxalSetri
    {
        public int Setir; public Dictionary<string, object> Deyerler = new(); public SetirVeziyyet Veziyyet; public string Qeyd = "";
    }

    public sealed class SutunUygun
    {
        public int Sutun; public string ExcelAd; public string Numune; public string SaheKod; public bool Avtomatik; public bool Shubheli;
    }

    /// <summary>Excel idxalı (4 addım: fayl → uyğunlaşdırma → yoxlama → nəticə) və ixracı (orijinal reyestr formatında).</summary>
    public static class ExcelService
    {
        // Faylınızdakı başlıqlar proqramdakı adlardan fərqlidirsə, burada uyğunluq yazılır (kiçik hərflə)
        private static readonly Dictionary<string, string> Alias = new()
        {
            ["o/t"] = "OrtaTemir", ["ə/t"] = "EsasliTemir", ["e/t"] = "EsasliTemir",
            ["maşının iş uçotu (km, son göstərici)"] = "Km", ["mühərrikin iş uçotu (motosaat, son göstərici)"] = "Motosaat",
            ["atışların uçotu (say, son göstərici)"] = "Atislar", ["topun və ya lülənin nömrəsi"] = "LuleNo",
            ["lülə və avtomatik doldurma sisteminin vəziyyəti"] = "VLule", ["atəşi idarəetmə sistemi və stabilizatorun vəziyyəti"] = "VAis",
            ["optik və termal cihazların vəziyyəti"] = "VOptik", ["təmir gecikirsə, gecikmə səbəbi"] = "GecikmeSebebi",
            ["əlaqəli məktubların nömrələri və çıxış tarixləri"] = "Mektublar", ["təmin edən idarə və həmin idarədə icraçı"] = "TeminEden",
            ["hissələr üzrə daxilolma tarixləri və miqdarları"] = "HisseDaxilolma", ["əlaqəli sənədlərin qeydiyyat nömrələri, tarixləri və fayl adları"] = "Senedler",
            ["icra müddəti (say)"] = "IcraMuddeti", ["istismarda olduğu h/h"] = "HerbiHisse", ["təmirə yararlı deyilsə, səbəbi"] = "YararsizSebeb",
        };

        // Azərbaycan hərfləri: "İ" → "i", "I" → "ı" (ToLowerInvariant "İ"-ni "i̇" edir və müqayisə pozulur)
        private static string Norm(string s) => Regex.Replace((s ?? "").Replace('İ', 'i').Replace('I', 'ı').ToLowerInvariant().Replace('\n', ' ').Replace('\r', ' '), @"\s+", " ").Trim();
        private static readonly Dictionary<string, string> AliasN = Alias.ToDictionary(a => Norm(a.Key), a => a.Value);

        public sealed class Fayl
        {
            public string Yol; public string Vereq; public int BaslikSetri; public int MelumatBaslayir; public List<SutunUygun> Sutunlar = new();
            public List<string> Vereqler = new();
        }

        /// <summary>Addım 1–2: faylı oxu, başlıq sətrini tap, sütunları avtomatik uyğunlaşdır.</summary>
        public static Fayl Ac(string yol, string vereq = null) 
        {
            using var wb = new XLWorkbook(yol);
            var f = new Fayl { Yol = yol, Vereqler = wb.Worksheets.Select(w => w.Name).ToList() };
            var ws = vereq == null ? wb.Worksheets.First() : wb.Worksheet(vereq); f.Vereq = ws.Name;
            var adlar = Saheler.Hamisi.ToDictionary(s => Norm(s.Ad), s => s.Kod);
            int best = 1, bestN = -1, lastCol = Math.Min(ws.LastColumnUsed()?.ColumnNumber() ?? 1, 300);
            for (int r = 1; r <= 12; r++)
            {
                int n = Enumerable.Range(1, lastCol).Count(c => Tap(Norm(ws.Cell(r, c).GetString()), adlar) != null);
                if (n > bestN) { bestN = n; best = r; }
            }
            f.BaslikSetri = best;
            // Alt başlıq sətri (məs. "o/t", "ə/t") varsa, məlumat ondan sonra başlayır
            bool alt = Enumerable.Range(1, lastCol).Any(c => ws.Cell(best + 1, c).GetString().Trim() is { Length: > 0 } t && AliasN.ContainsKey(Norm(t)));
            f.MelumatBaslayir = best + (alt ? 2 : 1);
            var istifade = new HashSet<string>();
            for (int c = 1; c <= lastCol; c++)
            {
                string ad = ws.Cell(best, c).GetString();
                if (alt && ws.Cell(best + 1, c).GetString().Trim().Length > 0) ad = ws.Cell(best + 1, c).GetString();
                if (string.IsNullOrWhiteSpace(ad)) { if (ws.Column(c).CellsUsed().Count() <= 1) continue; ad = "(adsız sütun)"; }
                string kod = Tap(Norm(ad), adlar); bool shub = false;
                if (kod == null) { kod = Oxsar(Norm(ad), adlar); shub = kod != null; }
                if (kod != null && !istifade.Add(kod)) kod = null;
                f.Sutunlar.Add(new SutunUygun { Sutun = c, ExcelAd = ad.Replace('\n', ' ').Trim(), Numune = ws.Cell(f.MelumatBaslayir, c).GetFormattedString(), SaheKod = kod, Avtomatik = kod != null && !shub, Shubheli = shub });
            }
            return f;
        }

        private static string Tap(string n, Dictionary<string, string> adlar) => n.Length == 0 ? null : adlar.TryGetValue(n, out var k) ? k : AliasN.TryGetValue(n, out var a) ? a : null;

        // Söz üst-üstə düşməsinə görə ən oxşar sahə (yoxlamaq lazımdır)
        private static string Oxsar(string n, Dictionary<string, string> adlar)
        {
            var w = n.Split(' ', ',', '(', ')', '/').Where(x => x.Length > 2).ToHashSet();
            if (w.Count == 0) return null;
            var (kod, skor) = adlar.Select(a => (a.Value, a.Key.Split(' ', ',', '(', ')', '/').Count(x => x.Length > 2 && w.Contains(x)) / (double)w.Count)).MaxBy(x => x.Item2);
            return skor >= 0.6 ? kod : null;
        }

        /// <summary>Addım 3: bütün sətirləri oxu və yoxla (bazaya heç nə yazılmır).</summary>
        public static async Task<List<IdxalSetri>> YoxlaAsync(Fayl f, IProgress<int> p, CancellationToken ct)
        {
            var movcud = await TexnikaRepo.ZavodNoGuidAsync();
            return await Task.Run(() =>
            {
                using var wb = new XLWorkbook(f.Yol); var ws = wb.Worksheet(f.Vereq);
                int last = ws.LastRowUsed()?.RowNumber() ?? 0, total = Math.Max(1, last - f.MelumatBaslayir + 1);
                var map = f.Sutunlar.Where(s => s.SaheKod != null).ToList(); var tipler = Saheler.Hamisi.ToDictionary(s => s.Kod, s => s.Nov);
                var fayldakiNo = new HashSet<string>(StringComparer.OrdinalIgnoreCase); var l = new List<IdxalSetri>();
                for (int r = f.MelumatBaslayir; r <= last; r++)
                {
                    ct.ThrowIfCancellationRequested();
                    var s = new IdxalSetri { Setir = r }; var qeyd = new List<string>(); bool xeta = false, xeb = false;
                    foreach (var m in map)
                    {
                        var cell = ws.Cell(r, m.Sutun); if (cell.IsEmpty()) continue;
                        object v = Deyer(cell, tipler[m.SaheKod], out string err);
                        if (err != null) { xeta = true; qeyd.Add($"{Saheler.Hamisi.First(x => x.Kod == m.SaheKod).Ad}: {err}"); continue; }
                        if (v != null) s.Deyerler[m.SaheKod] = v;
                    }
                    if (s.Deyerler.Count == 0) { s.Veziyyet = SetirVeziyyet.Bos; l.Add(s); continue; }
                    foreach (var k in new[] { "Nov", "Marka", "ZavodNo" }) if (!s.Deyerler.ContainsKey(k)) { xeta = true; qeyd.Add($"{Saheler.Hamisi.First(x => x.Kod == k).Ad} boşdur"); }
                    if (s.Deyerler.TryGetValue("IstehsalIli", out var il) && il is double y && (y < 1940 || y > DateTime.Today.Year + 1)) { xeb = true; qeyd.Add($"İl şübhəlidir ({y})"); }
                    s.Deyerler.TryAdd("Status", "Qəbul edilib");
                    if (s.Deyerler["Status"] is string sto) s.Deyerler["Status"] = Secimler.Baza(sto);   // ekran adı yazılıbsa
                    if (Session.ZavodFiltri != null) s.Deyerler.TryAdd("TemirZavodu", Session.ZavodFiltri);
                    if (s.Deyerler.TryGetValue("ZavodNo", out var zn))
                    {
                        string no = zn.ToString();
                        if (!fayldakiNo.Add(no)) { xeta = true; qeyd.Add("Faylda təkrarlanır"); }
                        else if (movcud.TryGetValue(no, out var g)) { s.Deyerler["Guid"] = g; if (!xeta) { s.Veziyyet = SetirVeziyyet.Yenilenecek; qeyd.Insert(0, "Bazada var → yenilənəcək"); } }
                    }
                    if (xeta) s.Veziyyet = SetirVeziyyet.Xeta; else if (s.Veziyyet != SetirVeziyyet.Yenilenecek) s.Veziyyet = xeb ? SetirVeziyyet.Xeberdarliq : SetirVeziyyet.Yeni;
                    s.Qeyd = string.Join("; ", qeyd); l.Add(s);
                    if ((r - f.MelumatBaslayir) % 200 == 0) p?.Report((r - f.MelumatBaslayir) * 100 / total);
                }
                return l;
            }, ct);
        }

        private static object Deyer(IXLCell c, Data.SaheNov nov, out string err)
        {
            err = null;
            switch (nov)
            {
                case Data.SaheNov.Tarix:
                    if (c.DataType == XLDataType.DateTime) return Vaxt.Iso(c.GetDateTime());
                    var t = c.GetFormattedString().Trim();
                    if (t is "-" or "") return null;
                    if (DateTime.TryParseExact(t, new[] { "dd.MM.yyyy", "dd.MM.yy", "d.M.yyyy", "yyyy-MM-dd", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out var d)) return Vaxt.Iso(d);
                    err = $"tarix yanlışdır ({t})"; return null;
                case Data.SaheNov.Reqem:
                    if (c.DataType == XLDataType.Number) return c.GetDouble();
                    var s = c.GetFormattedString().Trim().Replace(" ", "").Replace(',', '.');
                    if (s is "-" or "") return null;
                    if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out var v)) return v;
                    err = $"rəqəm deyil ({c.GetFormattedString()})"; return null;
                default:
                    var x = c.GetFormattedString().Trim(); return x.Length == 0 ? null : x;
            }
        }

        // ---------------- İxrac ----------------
        public sealed class IxracSecim
        {
            public ReyestrFiltr Filtr = new(); public long[] YalnizIdler; public bool BesSutun; public string[] SecilmisSutunlar;
            public bool QrupBasliqlari = true, StatusReng = true, GecikenQirmizi = true, DondurVeFiltr = true, IcmalVereqi = true;
        }

        public static async Task<int> IxracAsync(string yol, IxracSecim s, IProgress<int> p, CancellationToken ct)
        {
            var sahe = s.BesSutun ? Saheler.Hamisi.Where(x => Saheler.Reyestr.Contains(x.Kod)).ToArray()
                     : s.SecilmisSutunlar != null ? Saheler.Hamisi.Where(x => s.SecilmisSutunlar.Contains(x.Kod)).ToArray() : Saheler.Hamisi;
            long cemi = s.YalnizIdler?.Length ?? await TexnikaRepo.SayAsync(s.Filtr, ct);
            await using var con = await LocalDb.AcAsync(ct);
            using var cmd = con.CreateCommand();
            var w = TexnikaRepo.Sert(s.Filtr, cmd);
            if (s.YalnizIdler != null) w.Append($" AND Id IN ({string.Join(",", s.YalnizIdler)})");
            cmd.CommandText = $"SELECT {string.Join(",", sahe.Select(x => x.Kod))} FROM Texnikalar WHERE {w} ORDER BY IFNULL(GozlenilenBitme,''), Id;";
            return await Task.Run(async () =>
            {
                using var wb = new XLWorkbook(); var ws = wb.AddWorksheet("Reyestr");
                var teal = XLColor.FromHtml("#0F4C4A"); var light = XLColor.FromHtml("#DCEFEC");
                int row = 1;
                if (s.QrupBasliqlari)
                {
                    int c0 = 2;
                    foreach (var grp in sahe.GroupBy(x => x.Qrup))
                    {
                        int n = grp.Count(); var rng = ws.Range(1, c0, 1, c0 + n - 1); rng.Merge().Value = grp.Key.ToUpper();
                        rng.Style.Fill.BackgroundColor = teal; rng.Style.Font.FontColor = XLColor.White; rng.Style.Font.Bold = true; rng.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        c0 += n;
                    }
                    row = 2;
                }
                ws.Cell(row, 1).Value = "Sıra sayı";
                for (int i = 0; i < sahe.Length; i++) ws.Cell(row, i + 2).Value = sahe[i].Ad;
                var h = ws.Range(row, 1, row, sahe.Length + 1);
                h.Style.Fill.BackgroundColor = light; h.Style.Font.Bold = true; h.Style.Font.FontColor = teal; h.Style.Alignment.WrapText = true; h.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws.Row(row).Height = 45;
                int hdr = row, n2 = 0; string bugun = Vaxt.Iso(DateTime.Today);
                int si = Array.FindIndex(sahe, x => x.Kod == "Status"), gi = Array.FindIndex(sahe, x => x.Kod == "GozlenilenBitme");
                await using var r = await cmd.ExecuteReaderAsync(ct);
                while (await r.ReadAsync(ct))
                {
                    row++; n2++; ws.Cell(row, 1).Value = n2;
                    for (int i = 0; i < sahe.Length; i++)
                    {
                        if (r.IsDBNull(i)) continue;
                        var cell = ws.Cell(row, i + 2);
                        if (sahe[i].Nov == Data.SaheNov.Tarix && DateTime.TryParse(r.GetString(i), out var d)) { cell.Value = d; cell.Style.DateFormat.Format = "dd.MM.yyyy"; }
                        else if (sahe[i].Nov == Data.SaheNov.Reqem) cell.Value = r.GetDouble(i); else cell.Value = r.GetString(i);
                    }
                    if (s.StatusReng && si >= 0 && !r.IsDBNull(si))
                    { var (bk, fg) = Core.Theme.StatusColors(r.GetString(si)); ws.Cell(row, si + 2).Style.Fill.BackgroundColor = XLColor.FromColor(bk); ws.Cell(row, si + 2).Style.Font.FontColor = XLColor.FromColor(fg); }
                    if (s.GecikenQirmizi && gi >= 0 && !r.IsDBNull(gi) && string.CompareOrdinal(r.GetString(gi), bugun) < 0 && si >= 0 && !r.IsDBNull(si) && r.GetString(si) is "Qəbul edilib" or "Təmirdə" or "Hissə gözləyir" or "Qismən təmin edilib")
                    { ws.Cell(row, gi + 2).Style.Font.FontColor = XLColor.FromHtml("#C00000"); ws.Cell(row, gi + 2).Style.Font.Bold = true; }
                    if (n2 % 500 == 0) p?.Report((int)(n2 * 90 / Math.Max(1, cemi)));
                }
                ws.Columns(1, sahe.Length + 1).Width = 18; ws.Column(1).Width = 7;
                if (s.DondurVeFiltr) { ws.SheetView.FreezeRows(hdr); if (row > hdr) ws.Range(hdr, 1, row, sahe.Length + 1).SetAutoFilter(); }
                if (s.IcmalVereqi)
                {
                    var ic = wb.AddWorksheet("İcmal"); var dt = await TexnikaRepo.ZavodIcmalAsync();
                    string[] b = { "Zavod", "Cəmi", "Təmirdə", "Hissə gözləyir", "Gecikən", "Orta təmir (gün)" };
                    for (int i = 0; i < b.Length; i++) ic.Cell(1, i + 1).Value = b[i];
                    ic.Range(1, 1, 1, b.Length).Style.Fill.BackgroundColor = light; ic.Range(1, 1, 1, b.Length).Style.Font.Bold = true;
                    int rr = 2;
                    foreach (System.Data.DataRow z in dt.Rows)
                    {
                        if (Session.ZavodFiltri != null && (string)z["Zavod"] != Session.ZavodFiltri) continue;
                        ic.Cell(rr, 1).Value = (string)z["Zavod"]; for (int i = 1; i <= 4; i++) ic.Cell(rr, i + 1).Value = Convert.ToDouble(z[i]);
                        if (z["OrtaGun"] is not DBNull) ic.Cell(rr, 6).Value = Convert.ToDouble(z["OrtaGun"]); rr++;
                    }
                    ic.Columns().AdjustToContents();
                }
                var info = wb.AddWorksheet("Məlumat");
                info.Cell(1, 1).Value = "Texnika Reyestri"; info.Cell(1, 1).Style.Font.Bold = true;
                info.Cell(2, 1).Value = "İxrac edən"; info.Cell(2, 2).Value = $"{Session.AdSoyad} ({Session.QurumAd})";
                info.Cell(3, 1).Value = "Tarix"; info.Cell(3, 2).Value = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                info.Cell(4, 1).Value = "Filtr"; info.Cell(4, 2).Value = $"Zavod: {s.Filtr.Zavod ?? "Hamısı"}, Status: {s.Filtr.Status ?? "Hamısı"}{(s.Filtr.Gecikenler ? ", yalnız gecikənlər" : "")}{(string.IsNullOrEmpty(s.Filtr.Axtar) ? "" : ", axtarış: " + s.Filtr.Axtar)}";
                info.Cell(5, 1).Value = "Sətir sayı"; info.Cell(5, 2).Value = n2;
                info.Cell(6, 1).Value = "Proqram versiyası"; info.Cell(6, 2).Value = typeof(ExcelService).Assembly.GetName().Version?.ToString(3);
                info.Columns().AdjustToContents();
                p?.Report(95); wb.SaveAs(yol); p?.Report(100);
                return n2;
            }, ct);
        }

        /// <summary>Xəta hesabatı: yalnız xətalı sətirlər, səbəbi ilə.</summary>
        public static void XetaHesabati(string yol, IEnumerable<IdxalSetri> xetalar)
        {
            using var wb = new XLWorkbook(); var ws = wb.AddWorksheet("Xətalar");
            ws.Cell(1, 1).Value = "Excel sətri"; ws.Cell(1, 2).Value = "Səbəb"; ws.Cell(1, 3).Value = "Zavod nömrəsi"; ws.Cell(1, 4).Value = "Marka";
            ws.Range(1, 1, 1, 4).Style.Font.Bold = true; int r = 2;
            foreach (var x in xetalar)
            {
                ws.Cell(r, 1).Value = x.Setir; ws.Cell(r, 2).Value = x.Qeyd; ws.Cell(r, 2).Style.Font.FontColor = XLColor.FromHtml("#C00000");
                ws.Cell(r, 3).Value = x.Deyerler.GetValueOrDefault("ZavodNo")?.ToString(); ws.Cell(r, 4).Value = x.Deyerler.GetValueOrDefault("Marka")?.ToString(); r++;
            }
            ws.Columns().AdjustToContents(); wb.SaveAs(yol);
        }
    }
}
