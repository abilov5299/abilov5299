using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using TexnikaReyestri.Data;

namespace TexnikaReyestri.Services
{
    /// <summary>Texnikanın təmir pasportu (A4, bütün sahələr qruplar üzrə, imza yeri ilə).</summary>
    public static class PdfService
    {
        static PdfService() { GlobalFontSettings.FontResolver ??= new WinFonts(); }

        /// <summary>Şriftləri Windows-un Fonts qovluğundan oxuyur (Times New Roman; Azərbaycan hərfləri daxildir).</summary>
        private sealed class WinFonts : IFontResolver
        {
            private static readonly string Dir = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
            public FontResolverInfo ResolveTypeface(string familyName, bool bold, bool italic) =>
                new((bold, italic) switch { (true, true) => "timesbi", (true, false) => "timesbd", (false, true) => "timesi", _ => "times" });
            public byte[] GetFont(string faceName)
            {
                string f = Path.Combine(Dir, faceName + ".ttf");
                if (!File.Exists(f)) f = Path.Combine(Dir, faceName.Contains("bd") ? "arialbd.ttf" : "arial.ttf");   // Times yoxdursa Arial
                return File.ReadAllBytes(f);
            }
        }

        public static void Pasport(string yol, Dictionary<string, object> t)
        {
            var doc = new PdfDocument(); doc.Info.Title = $"Pasport {t.GetValueOrDefault("ZavodNo")}";
            var fH = new XFont("Times New Roman", 15, XFontStyleEx.Bold); var fG = new XFont("Times New Roman", 10.5, XFontStyleEx.Bold);
            var fN = new XFont("Times New Roman", 10); var fS = new XFont("Times New Roman", 9);
            PdfPage page = null; XGraphics g = null; double y = 0; const double L = 50, W = 495, row = 18;
            void Yeni()
            {
                g?.Dispose(); page = doc.AddPage(); page.Size = PdfSharp.PageSize.A4; g = XGraphics.FromPdfPage(page); y = 50;
                g.DrawString("Təmir və Texniki Təminat Xidməti · Məlumat bazası · Təmir zavodları üzrə texnikanın qəbulu, təmiri və təhvilinin uçotu", fS, XBrushes.Gray, new XRect(L, page.Height.Point - 35, W, 12), XStringFormats.TopLeft);
            }
            Yeni();
            g.DrawString("TEXNİKANIN TƏMİR PASPORTU", fH, XBrushes.Black, new XRect(L, y, W, 20), XStringFormats.TopCenter); y += 24;
            g.DrawString($"{t.GetValueOrDefault("TemirZavodu")} · Çap tarixi: {DateTime.Now:dd.MM.yyyy}", fN, XBrushes.Black, new XRect(L, y, W, 14), XStringFormats.TopCenter); y += 26;
            var pen = new XPen(XColor.FromArgb(150, 150, 150), 0.6);
            foreach (var grp in Saheler.Hamisi.GroupBy(s => s.Qrup))
            {
                var dolu = grp.Where(s => t.GetValueOrDefault(s.Kod) is not null and not DBNull).ToList();
                if (dolu.Count == 0) continue;
                if (y + row * 2 > page.Height.Point - 60) Yeni();
                g.DrawRectangle(pen, new XSolidBrush(XColor.FromArgb(235, 235, 235)), L, y, W, row);
                g.DrawString(grp.Key, fG, XBrushes.Black, new XRect(L + 6, y + 3, W, row), XStringFormats.TopLeft); y += row;
                foreach (var s in dolu)
                {
                    string v = s.Nov == SaheNov.Tarix ? Vaxt.Tarix(t[s.Kod]) : Saheler.Json.Contains(s.Kod) ? TexnikaJson.Metn(s.Kod, t[s.Kod])
                             : s.Kod == "Status" ? Secimler.Ekran(t[s.Kod] as string) : Convert.ToString(t[s.Kod], System.Globalization.CultureInfo.InvariantCulture);
                    var lines = Wrap(g, v, fN, W * 0.55 - 10); double h = Math.Max(row, lines.Count * 13 + 5);
                    if (y + h > page.Height.Point - 60) Yeni();
                    g.DrawRectangle(pen, L, y, W * 0.45, h); g.DrawRectangle(pen, L + W * 0.45, y, W * 0.55, h);
                    g.DrawString(s.Ad, fN, XBrushes.DimGray, new XRect(L + 6, y + 3, W * 0.45 - 10, h), XStringFormats.TopLeft);
                    for (int i = 0; i < lines.Count; i++) g.DrawString(lines[i], fN, XBrushes.Black, new XRect(L + W * 0.45 + 6, y + 3 + i * 13, W * 0.55 - 10, 13), XStringFormats.TopLeft);
                    y += h;
                }
                y += 8;
            }
            if (y + 60 > page.Height.Point - 60) Yeni();
            y += 30;
            g.DrawString("Məsul icraçı: ______________________", fN, XBrushes.Black, new XRect(L, y, W / 2, 14), XStringFormats.TopLeft);
            g.DrawString("İmza: ______________", fN, XBrushes.Black, new XRect(L + W / 2 + 40, y, W / 2, 14), XStringFormats.TopLeft);
            g.Dispose(); doc.Save(yol);
        }

        /// <summary>Hesabat (A4): "TƏMİR VƏ TƏMİNAT ÜZRƏ HESABAT", altında hesabatın adı, alt başlıq, cədvəl, cəmi, qeydlər.</summary>
        public static void Hesabat(string yol, Hesabat h, string altBasliq, IEnumerable<string> qeydler)
        {
            var doc = new PdfDocument(); doc.Info.Title = h.Ad;
            var fH = new XFont("Times New Roman", 14, XFontStyleEx.Bold); var fA = new XFont("Times New Roman", 12, XFontStyleEx.Bold);
            var fB = new XFont("Times New Roman", 9.5, XFontStyleEx.Bold); var fN = new XFont("Times New Roman", 9.5); var fS = new XFont("Times New Roman", 9);
            bool genis = h.Sutun.Length > 5;
            PdfPage page = null; XGraphics g = null; double y = 0, L = 40;
            double W() => page.Width.Point - 2 * L;
            var pen = new XPen(XColor.FromArgb(150, 150, 150), 0.6); var boz = new XSolidBrush(XColor.FromArgb(232, 236, 236));
            double[] en = null;
            void Yeni()
            {
                g?.Dispose(); page = doc.AddPage(); page.Size = PdfSharp.PageSize.A4; if (genis) page.Orientation = PdfSharp.PageOrientation.Landscape;
                g = XGraphics.FromPdfPage(page); y = 40;
                double w = W();
                // 8 sütunlu (ehtiyat hissələri) hesabatda texnika və hissə adı uzundur — onlara geniş yer
                double[] ag = h.Sutun.Length == 8 ? new[] { 1.2, 1.7, 1.6, 0.7, 0.8, 0.8, 0.7, 1.1 } : null;
                en = ag != null ? ag.Select(a => w * a / ag.Sum()).ToArray() : h.Sutun.Select((_, i) => i == 0 ? w * 0.28 : w * 0.72 / (h.Sutun.Length - 1)).ToArray();
                g.DrawString($"Təmir və Texniki Təminat Xidməti · Məlumat bazası · Çap tarixi: {DateTime.Now:dd.MM.yyyy HH:mm}", fS, XBrushes.Gray, new XRect(L, page.Height.Point - 30, w, 12), XStringFormats.TopLeft);
            }
            void Setir(string[] v, XFont f, XBrush fon)
            {
                var lines = v.Select((x, i) => Wrap(g, x ?? "", f, en[i] - 8)).ToArray();
                double rh = Math.Max(18, lines.Max(l => l.Count) * 12 + 6);
                if (y + rh > page.Height.Point - 50) { Yeni(); Setir(h.Sutun, fB, boz); }
                double x = L;
                for (int i = 0; i < v.Length; i++)
                {
                    if (fon != null) g.DrawRectangle(pen, fon, x, y, en[i], rh); else g.DrawRectangle(pen, x, y, en[i], rh);
                    for (int k = 0; k < lines[i].Count; k++)
                        g.DrawString(lines[i][k], f, XBrushes.Black, new XRect(x + 4, y + 3 + k * 12, en[i] - 8, 12), i == 0 ? XStringFormats.TopLeft : XStringFormats.TopCenter);
                    x += en[i];
                }
                y += rh;
            }
            Yeni();
            g.DrawString("TƏMİR VƏ TƏMİNAT ÜZRƏ HESABAT", fH, XBrushes.Black, new XRect(L, y, W(), 18), XStringFormats.TopCenter); y += 20;
            g.DrawString(h.Ad, fA, XBrushes.Black, new XRect(L, y, W(), 16), XStringFormats.TopCenter); y += 18;
            g.DrawString(altBasliq, fN, XBrushes.DimGray, new XRect(L, y, W(), 14), XStringFormats.TopCenter); y += 22;
            Setir(h.Sutun, fB, boz);
            foreach (var r in h.Setir) Setir(r, fN, null);
            if (h.Cem != null) Setir(h.Cem, fB, boz);
            y += 8;
            foreach (var q in new[] { h.Qeyd }.Concat(qeydler ?? Enumerable.Empty<string>()).Where(x => !string.IsNullOrEmpty(x)))
            { if (y + 14 > page.Height.Point - 50) Yeni(); g.DrawString(q, fS, XBrushes.DimGray, new XRect(L, y, W(), 12), XStringFormats.TopLeft); y += 14; }
            y += 30; if (y + 20 > page.Height.Point - 50) Yeni();
            g.DrawString("Məsul şəxs: ______________________", fN, XBrushes.Black, new XRect(L, y, W() / 2, 14), XStringFormats.TopLeft);
            g.DrawString("İmza: ______________", fN, XBrushes.Black, new XRect(L + W() / 2 + 40, y, W() / 2, 14), XStringFormats.TopLeft);
            g.Dispose(); doc.Save(yol);
        }

        /// <summary>Təchizat sorğusu sənədi (A4): məktub məlumatları, əlaqəli texnikalar, ehtiyat hissələri, imza yerləri.</summary>
        public static void Sorgu(string yol, Sorgu s)
        {
            string V(string k) => s.V.TryGetValue(k, out var v) && !string.IsNullOrEmpty(v) ? v : "—";
            string T(string k) => V(k) == "—" ? "—" : Vaxt.Tarix(V(k));
            var doc = new PdfDocument(); doc.Info.Title = $"Təchizat sorğusu {V("MektubNo")}";
            var fH = new XFont("Times New Roman", 15, XFontStyleEx.Bold); var fG = new XFont("Times New Roman", 10.5, XFontStyleEx.Bold);
            var fB = new XFont("Times New Roman", 9.5, XFontStyleEx.Bold); var fN = new XFont("Times New Roman", 10); var fS = new XFont("Times New Roman", 9);
            var pen = new XPen(XColor.FromArgb(150, 150, 150), 0.6); var boz = new XSolidBrush(XColor.FromArgb(235, 235, 235));
            PdfPage page = null; XGraphics g = null; double y = 0; const double L = 50, W = 495;
            void Yeni()
            {
                g?.Dispose(); page = doc.AddPage(); page.Size = PdfSharp.PageSize.A4; g = XGraphics.FromPdfPage(page); y = 50;
                g.DrawString($"Təmir və Texniki Təminat Xidməti · Məlumat bazası · Çap tarixi: {DateTime.Now:dd.MM.yyyy HH:mm}", fS, XBrushes.Gray, new XRect(L, page.Height.Point - 35, W, 12), XStringFormats.TopLeft);
            }
            void Bolme(string ad) { if (y + 40 > page.Height.Point - 60) Yeni(); g.DrawString(ad, fG, XBrushes.Black, new XRect(L, y, W, 14), XStringFormats.TopLeft); y += 18; }
            void Cedvel(double[] en, string[] bas, IEnumerable<string[]> rows)
            {
                void Setir(string[] v, XFont f, XBrush fon)
                {
                    var lines = v.Select((x, i) => Wrap(g, x ?? "", f, en[i] * W - 8)).ToArray(); double rh = Math.Max(18, lines.Max(l => l.Count) * 12 + 6);
                    if (y + rh > page.Height.Point - 60) { Yeni(); if (f != fB) Setir(bas, fB, boz); }
                    double x = L;
                    for (int i = 0; i < v.Length; i++)
                    {
                        double w = en[i] * W; if (fon != null) g.DrawRectangle(pen, fon, x, y, w, rh); else g.DrawRectangle(pen, x, y, w, rh);
                        for (int k = 0; k < lines[i].Count; k++) g.DrawString(lines[i][k], f, XBrushes.Black, new XRect(x + 4, y + 3 + k * 12, w - 8, 12), XStringFormats.TopLeft);
                        x += w;
                    }
                    y += rh;
                }
                Setir(bas, fB, boz); foreach (var r in rows) Setir(r, fN, null); y += 12;
            }
            Yeni();
            g.DrawString("TƏMİR VƏ TƏMİNAT ÜZRƏ", fS, XBrushes.DimGray, new XRect(L, y, W, 12), XStringFormats.TopCenter); y += 14;
            g.DrawString("TƏCHİZAT SORĞUSU", fH, XBrushes.Black, new XRect(L, y, W, 20), XStringFormats.TopCenter); y += 22;
            g.DrawString($"Məktub № {V("MektubNo")} · {T("QeydTarixi")} · {V("Zavod")}", fN, XBrushes.Black, new XRect(L, y, W, 14), XStringFormats.TopCenter); y += 26;
            Bolme("Məktub məlumatları");
            Cedvel(new[] { 0.4, 0.6 }, new[] { "Sahə", "Dəyər" }, new[]
            {
                new[] { "Məktub nömrəsi", V("MektubNo") }, new[] { "Qeydiyyat tarixi", T("QeydTarixi") }, new[] { "Zavod", V("Zavod") },
                new[] { "İcra üçün son tarix", T("IcraMuddeti") }, new[] { "Təmin edən qurum", V("TeminEden") }, new[] { "Təmin edən qurum üzrə məsul icraçı", V("TeminIcraci") },
                new[] { "Əlaqə nömrəsi", V("Telefon") }, new[] { "Məktubun icra vəziyyəti", V("CavabVez") }, new[] { "Təminat nəticəsi", V("Netice") },
            });
            Bolme("Əlaqəli texnikalar");
            Cedvel(new[] { 0.08, 0.42, 0.25, 0.25 }, new[] { "№", "Marka", "Zavod nömrəsi", "Status" }, s.Tex.Select((t, i) => new[] { (i + 1).ToString(), t.Marka, t.ZavodNo, Secimler.Ekran(t.Status) }));
            Bolme("Ehtiyat hissələri");
            string F(double v) => v.ToString("0.##");
            Cedvel(new[] { 0.06, 0.28, 0.24, 0.1, 0.11, 0.11, 0.1 }, new[] { "№", "Ehtiyat hissəsi", "Texnika", "Ölçü vahidi", "Tələb olunan", "Qəbul edilmiş", "Qalıq" },
                s.His.Select((h, i) => new[] { (i + 1).ToString(), h.Ad, h.Texnika, h.Vahid, F(h.Teleb), F(h.Qebul), F(Math.Max(0, h.Teleb - h.Qebul)) })
                     .Append(new[] { "", "Cəmi", "", "", F(s.His.Sum(h => h.Teleb)), F(s.His.Sum(h => h.Qebul)), F(s.His.Sum(h => Math.Max(0, h.Teleb - h.Qebul))) }));
            if (y + 70 > page.Height.Point - 60) Yeni();
            y += 24;
            g.DrawString($"Təmin edən qurum üzrə icraçı: {V("TeminIcraci")}   ______________", fN, XBrushes.Black, new XRect(L, y, W, 14), XStringFormats.TopLeft);
            g.Dispose(); doc.Save(yol);
        }

        private static List<string> Wrap(XGraphics g, string s, XFont f, double w)
        {
            var l = new List<string>(); var cur = "";
            foreach (var word in (s ?? "").Split(' '))
            {
                var t = cur.Length == 0 ? word : cur + " " + word;
                if (g.MeasureString(t, f).Width > w && cur.Length > 0) { l.Add(cur); cur = word; } else cur = t;
            }
            l.Add(cur); return l;
        }
    }
}
