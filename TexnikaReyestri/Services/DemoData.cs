using TexnikaReyestri.Data;

namespace TexnikaReyestri.Services
{
    /// <summary>Sürət sınağı üçün nümunə məlumat (Admin → Sistem). Zavod nömrələri "NÜMUNƏ-" ilə başlayır.</summary>
    public static class DemoData
    {
        private static readonly string[] Nov = { "Əsas döyüş tankı", "Piyadaların döyüş maşını", "Zirehli transportyor", "Təkərli texnika", "Özüyeriyən artilleriya qurğusu" };
        private static readonly string[] Marka = { "T-72 B", "T-72 M-1", "BMP-2", "BMP-1", "BTR-80", "BTR-70", "2S1", "KamAZ-4310" };
        private static readonly string[] St = { "Qəbul edilib", "Təmirdə", "Təmirdə", "Hissə gözləyir", "Təhvil verilib", "Təhvil verilib", "Təmirə yararsız" };
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
                        ["TemirZavodu"] = zavod ?? $"Zavod {rnd.Next(1, 8)}", ["DaxilolmaTarixi"] = Vaxt.Iso(dax), ["BaslamaTarixi"] = Vaxt.Iso(bas),
                        ["GozlenilenBitme"] = Vaxt.Iso(bas.AddDays(rnd.Next(20, 160))),
                        ["FaktikiBitme"] = s == "Təhvil verilib" ? Vaxt.Iso(bas.AddDays(rnd.Next(20, 150))) : null,
                        ["Qeyd"] = "Nümunə məlumat (sürət sınağı)"
                    };
                }
            }
            return TexnikaRepo.TopluYazAsync(Qeydler(), p, ct);
        }
    }
}
