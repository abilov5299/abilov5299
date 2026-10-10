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

        private static readonly Dictionary<float, Font> _cache = new();
        /// <summary>Windows 11-də "Segoe Fluent Icons", yoxdursa "Segoe MDL2 Assets" (eyni kodlar).</summary>
        public static readonly string Family = new System.Drawing.Text.InstalledFontCollection().Families.Any(f => f.Name == "Segoe Fluent Icons") ? "Segoe Fluent Icons" : FontName;
        public static Font Font(float size)
        {
            if (!_cache.TryGetValue(size, out var f)) _cache[size] = f = new Font(Family, size, GraphicsUnit.Point);
            return f;
        }
    }
}
