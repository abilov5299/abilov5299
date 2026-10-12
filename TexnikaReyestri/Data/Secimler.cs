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

        public static readonly string[] Status = { "Qəbul edilib", "Təmirdə", "Hissə gözləyir", "Təhvil verilib", "Təmirə yararsız" };
        public static readonly string[] AktivStatus = { "Qəbul edilib", "Təmirdə", "Hissə gözləyir" };
        public static readonly string[] Merhele = { "Qəbul", "Defekt aşkarlama", "Hissə təminatı", "Təmir", "Sınaq", "Təhvil" };

        public static readonly Dictionary<string, string[]> Sahe = new()
        {
            ["Nov"] = new[] { "Əsas döyüş tankı", "Piyadaların döyüş maşını", "Zirehli transportyor", "Özüyeriyən artilleriya qurğusu", "Təkərli texnika" },
            ["Derece"] = new[] { "I", "II", "III", "IV", "V" },
            ["Status"] = Status,
            ["TemirNovu"] = new[] { "Cari", "Orta", "Əsaslı" },
            ["KritikHisse"] = new[] { "Var", "Qismən", "Yoxdur" },
            ["Merhele"] = Merhele,
            ["TemirZavodu"] = Zavodlar,
            ["SorguStatus"] = new[] { "Göndərilib", "Cavab gözlənilir", "İcra olunub", "İmtina" },
        };

        public static readonly string[] Rollar = { "Zavod istifadəçisi", "Zavod rəhbəri", "Yalnız baxış" };

        /// <summary>Uzun mətn sahələri: formada çoxsətirli xana və bütün en boyu.</summary>
        public static readonly HashSet<string> UzunMetn = new()
        { "VMuherrik", "VSassi", "VLule", "VAis", "VOptik", "VDiger", "Qeyd", "GecikmeSebebi", "YararsizSebeb", "TemirQeyd", "PlanDeyisikliyi",
          "TelebHisseler", "Mektublar", "HisseDaxilolma", "Senedler" };
    }
}
