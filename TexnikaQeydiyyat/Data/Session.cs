namespace TexnikaQeydiyyat.Data
{
    public enum QurumTip : byte { Zavod = 1, UstQurum = 2, EnUstQurum = 3 }

    // Daxil olmuş istifadəçi
    public static class Session
    {
        public static int IstifadeciId;
        public static string AdSoyad;
        public static int QurumId;
        public static string QurumAd;
        public static QurumTip Tip;

        public static bool YazaBiler => Tip == QurumTip.Zavod;
        public static int? ZavodId => Tip == QurumTip.Zavod ? QurumId : null;
    }
}
