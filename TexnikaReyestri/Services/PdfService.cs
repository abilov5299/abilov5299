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
                g.DrawString("Texnika Reyestri · Təmir zavodları üzrə texnikanın qəbulu, təmiri və təhvilinin uçotu", fS, XBrushes.Gray, new XRect(L, page.Height.Point - 35, W, 12), XStringFormats.TopLeft);
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
                    string v = s.Nov == SaheNov.Tarix ? Vaxt.Tarix(t[s.Kod]) : Convert.ToString(t[s.Kod], System.Globalization.CultureInfo.InvariantCulture);
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
