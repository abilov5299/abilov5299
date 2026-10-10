namespace TexnikaReyestri.Data
{
    public enum QurumTip { Zavod = 1, UstQurum = 2, EnUstQurum = 3 }

    /// <summary>Daxil olmuş istifadəçi və bu kompüterin qurumu.</summary>
    public static class Session
    {
        public static string Login, AdSoyad, Rol, QurumKod, QurumAd;
        public static QurumTip Tip;
        /// <summary>Zavod istifadəçisi: texnika daxil edə və redaktə edə bilər. Üst qurumlar yalnız baxır.</summary>
        public static bool YazaBiler => Tip == QurumTip.Zavod && Rol != "Yalnız baxış";
        /// <summary>Zavod istifadəçisi yalnız öz zavodunu görür (null = hamısı).</summary>
        public static string ZavodFiltri => Tip == QurumTip.Zavod ? QurumAd : null;
        public static string RolMetni => Tip == QurumTip.Zavod ? Rol : "Yalnız baxış";
        public static void Bitir() { Login = AdSoyad = Rol = QurumKod = QurumAd = null; }
    }

    public static class Vaxt
    {
        public static long Indi => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        public static string Yaz(long? ms) => ms is null ? "—" : DateTimeOffset.FromUnixTimeMilliseconds(ms.Value).ToLocalTime().ToString("dd.MM.yyyy HH:mm");
        /// <summary>Bazada tarix ISO formatındadır (yyyy-MM-dd); ekranda dd.MM.yyyy.</summary>
        public static string Tarix(object iso) => iso is string s && DateTime.TryParse(s, out var d) ? d.ToString("dd.MM.yyyy") : iso?.ToString() ?? "";
        public static string Iso(DateTime d) => d.ToString("yyyy-MM-dd");
    }
}
