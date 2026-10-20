using System.Globalization;
using System.Text.Json;

namespace TexnikaReyestri.Data
{
    /// <summary>İstismar göstəricisi (Texnikalar.Gostericiler JSON siyahısı).</summary>
    public sealed class Gosterici { public string Nov { get; set; } public double? Miqdar { get; set; } }

    /// <summary>Ehtiyat hissəsinin bir qəbulu.</summary>
    public sealed class HisseQebul { public string Tarix { get; set; } public double Miqdar { get; set; } }

    /// <summary>Texnikaya tələb olunan ehtiyat hissəsi (Texnikalar.Hisseler JSON siyahısı).</summary>
    public sealed class Hisse
    {
        public string Ad { get; set; } public string Kod { get; set; } public string Vahid { get; set; } public double Teleb { get; set; }
        public List<HisseQebul> Qebullar { get; set; } = new();
        public double Qebul => Qebullar.Sum(q => q.Miqdar);
        public double Qaliq => Math.Max(0, Teleb - Qebul);
    }

    /// <summary>Texnikanın JSON sahələri: oxuma, yazma, təminatın hesablanması və mətnə çevirmə.</summary>
    public static class TexnikaJson
    {
        public static List<Gosterici> Gostericiler(object v) => Oxu<Gosterici>(v);
        public static List<Hisse> Hisseler(object v) => Oxu<Hisse>(v);

        private static List<T> Oxu<T>(object v)
        {
            if (v is not string s || s.Length == 0) return new();
            try { return JsonSerializer.Deserialize<List<T>>(s) ?? new(); } catch (JsonException) { return new(); }
        }

        /// <summary>Boş siyahı bazaya NULL kimi yazılır.</summary>
        public static string Yaz<T>(List<T> l) => l == null || l.Count == 0 ? null : JsonSerializer.Serialize(l);

        /// <summary>Hissə yoxdursa null; heç biri qəbul olunmayıbsa "Təminat gözlənilir"; hamısı tam qəbul olunubsa "Tam təmin edilib"; qalan hallarda "Qismən təmin edilib".</summary>
        public static string Teminat(List<Hisse> l)
        {
            if (l == null || l.Count == 0) return null;
            if (l.All(h => h.Qebul <= 0)) return "Təminat gözlənilir";
            return l.All(h => h.Qebul >= h.Teleb) ? "Tam təmin edilib" : "Qismən təmin edilib";
        }

        private static string R(double d) => d.ToString("0.##", CultureInfo.InvariantCulture);

        /// <summary>Excel, PDF və "Geniş bax" üçün oxunaqlı mətn.</summary>
        public static string Metn(string kod, object v) => kod switch
        {
            "Gostericiler" => string.Join("; ", Gostericiler(v).Select(g => g.Miqdar is double m ? $"{g.Nov}: {R(m)}" : g.Nov)),
            "Hisseler" => string.Join("; ", Hisseler(v).Select(h => $"{h.Ad}{(string.IsNullOrEmpty(h.Kod) ? "" : " (" + h.Kod + ")")} — {R(h.Teleb)} {h.Vahid}, qəbul {R(h.Qebul)}, qalıq {R(h.Qaliq)}")),
            _ => v as string
        };
    }
}
