namespace TexnikaReyestri.Data
{
    /// <summary>Açılan siyahıların (ComboBox) variantları. Yeni variant əlavə etmək üçün buranı redaktə edin.</summary>
    public static class Secimler
    {
        /// <summary>Zavodların adları (Qurumlar cədvəlində Z1…Z6 ilə eyni sırada). Adı burada və SistemSchema.cs-də birlikdə dəyişin.</summary>
        public static readonly string[] Zavodlar =
        {
            "939 nömrəli hərbi hissə", "328 nömrəli hərbi hissə", "329 nömrəli hərbi hissə",
            "114 nömrəli hərbi hissə", "412 nömrəli hərbi hissə", "339 nömrəli hərbi hissə"
        };
        public const string UstAd = "Təmir və Texniki Təminat xidməti", EnUstAd = "Müdafiə Sənayesi İdarəsi";

        public static readonly string[] Status = { "Qəbul edilib", "Təmirdə", "Təmir tamamlanıb, təhvil gözlənilir", "Təhvil verilib", "Təmirə yararsız" };
        /// <summary>Statusların ekranda göstərilən adı (bazada köhnə dəyər qalır: USB paketləri və Excel pozulmasın).</summary>
        private static readonly Dictionary<string, string> EkranAd = new() { ["Təmirdə"] = "Təmirdədir" };
        public static string Ekran(string s) => s != null && EkranAd.TryGetValue(s, out var e) ? e : s;
        /// <summary>Ekran adı → bazadakı dəyər (Excel-də ekran adı yazılıbsa).</summary>
        public static string Baza(string s) => EkranAd.FirstOrDefault(x => x.Value == s).Key ?? s;
        /// <summary>Təmiri davam edən statuslar (icra müddəti, gecikmə yalnız bunlar üçün).</summary>
        public static readonly string[] AktivStatus = { "Qəbul edilib", "Təmirdə" };
        /// <summary>Formada istifadəçinin seçə bildiyi statuslar; qalanları tarixlərə görə avtomatik qoyulur.</summary>
        public static readonly string[] SecilenStatus = { "Qəbul edilib", "Təmirdə", "Təmirə yararsız" };
        /// <summary>Teminat sahəsinin dəyəri: texnika (Qəbul edilib / Təmirdə) ehtiyat hissəsi gözləyir. Formada çek qutusu ilə qoyulur; status deyil.</summary>
        public const string StTeminat = "Təminat gözləyir";
        public const string StTamamlanib = "Təmir tamamlanıb, təhvil gözlənilir", StTehvil = "Təhvil verilib";

        /// <summary>Ehtiyat hissələrinin təminatı (avtomatik hesablanır; hissə yoxdursa boş).</summary>
        public static readonly string[] Teminat = { "Təminat gözlənilir", "Qismən təmin edilib", "Tam təmin edilib" };

        /// <summary>İstismar göstəricisinin növü; son ikisində miqdar tələb olunmur.</summary>
        public static readonly string[] Gosterici = { "Gediş, km", "İşləmə, saat", "Motosaat", "Dövr", "Atəş sayı", "Tətbiq edilmir", "Məlumat yoxdur" };
        public static bool MiqdarsizGosterici(string n) => n is "Tətbiq edilmir" or "Məlumat yoxdur";

        public static readonly string[] Vahidler = { "ədəd", "dəst", "litr", "kq", "metr" };

        public static readonly Dictionary<string, string[]> Sahe = new()
        {
            ["Nov"] = new[] { "Nəqliyyat vasitəsi", "Zirehli texnika", "Digər texnika" },
            ["Kateqoriya"] = new[] { "I", "II", "III", "IV", "V" },
            ["AktForma"] = new[] { "Forma 12", "Forma 12 (RAS)" },
            ["SonTemirNovu"] = new[] { "Cari təmir", "Orta təmir", "Əsaslı təmir" },
            ["TemirNovu"] = new[] { "Cari təmir", "Orta təmir", "Əsaslı təmir", "Nasazlıqların aradan qaldırılması" },
            ["Status"] = Status,
            ["MuddetVahidi"] = new[] { "Gün", "Ay" },
            ["Teminat"] = Teminat,
            ["TemirZavodu"] = Zavodlar,
        };

        public static readonly string[] Rollar = { "Zavod istifadəçisi", "Zavod rəhbəri", "Yalnız baxış" };

        /// <summary>Uzun mətn sahələri: çoxsətirli xana.</summary>
        public static readonly HashSet<string> UzunMetn = new() { "Nasazliqlar", "GecikmeSebebi", "SonTemirQeyd", "GorulenIsler" };
    }
}
