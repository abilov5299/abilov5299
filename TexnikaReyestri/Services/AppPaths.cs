namespace TexnikaReyestri.Services
{
    /// <summary>Faylların yeri. Hamısı bir qovluqdadır: C:\ProgramData\TexnikaReyestri</summary>
    public static class AppPaths
    {
        /// <summary>Dəyişdirilə bilər (məs. portativ rejim və ya sınaq üçün).</summary>
        public static string Root { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TexnikaReyestri");
        public static string Db => Path.Combine(Root, "Data", "reyestr.db");
        public static string Backup => Path.Combine(Root, "Backup");
        public static string Keys => Path.Combine(Root, "Keys");
        public static string Settings => Path.Combine(Root, "ayarlar.json");
    }
}
