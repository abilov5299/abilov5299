namespace TexnikaReyestri.Core
{
    /// <summary>Windows-un daxili ikon şrifti (Segoe MDL2 Assets, Windows 10/11). Kontrolun Glyph xassəsinə bu simvollar yazılır.</summary>
    public static class Icons
    {
        public const string FontName = "Segoe MDL2 Assets";
        public const string Home = "", List = "", Add = "", Bell = "", Sync = "", Logs = "",
            Backup = "", Overview = "", SignOut = "", Close = "", Minimize = "", Maximize = "",
            Sun = "", Moon = "", Search = "", Download = "", Upload = "", Print = "", Edit = "",
            Back = "", Forward = "", Save = "", Delete = "", Warning = "", Check = "", Clock = "",
            Repair = "", Filter = "", Lock = "", Usb = "", User = "", Org = "", Pc = "",
            Shield = "", Key = "", Settings = "", Journal = "", Refresh = "", Excel = "", Cancel = "";

        private static Font _cache; private static float _size;
        public static Font Font(float size)
        {
            if (_cache == null || _size != size) { _cache = new Font(FontName, size); _size = size; }
            return _cache;
        }
    }
}
