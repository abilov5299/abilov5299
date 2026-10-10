using System.IO.Compression;
using Microsoft.Data.Sqlite;
using TexnikaReyestri.Data;

namespace TexnikaReyestri.Services
{
    /// <summary>Ehtiyat nüsxə: SQLite-ın onlayn backup API-si (proqram işləyərkən də düzgün nüsxə) + GZip sıxma.</summary>
    public static class BackupService
    {
        public sealed record Nusxe(string Fayl, DateTime Tarix, long Olcu, bool Avtomatik);

        public static async Task<Nusxe> AlAsync(bool avtomatik, IProgress<int> p = null, CancellationToken ct = default)
        {
            Directory.CreateDirectory(AppPaths.Backup);
            string tmp = Path.Combine(AppPaths.Backup, "temp.db"), ad = $"{(avtomatik ? "auto" : "manual")}_{DateTime.Now:yyyyMMdd_HHmmss}.db.gz";
            string hedef = Path.Combine(AppPaths.Backup, ad);
            await Task.Run(async () =>
            {
                await using (var src = await LocalDb.AcAsync(ct))
                await using (var dst = new SqliteConnection($"Data Source={tmp};Pooling=False"))
                { dst.Open(); src.BackupDatabase(dst); }
                p?.Report(60);
                await using (var fi = File.OpenRead(tmp))
                await using (var fo = File.Create(hedef))
                await using (var gz = new GZipStream(fo, CompressionLevel.Fastest)) await fi.CopyToAsync(gz, ct);
                File.Delete(tmp);
            }, ct);
            p?.Report(100);
            await TemizleAsync();
            return new Nusxe(hedef, DateTime.Now, new FileInfo(hedef).Length, avtomatik);
        }

        /// <summary>Gün ərzində ilk açılışda bir dəfə.</summary>
        public static async Task GundelikAsync()
        {
            if (Siyahi().Any(n => n.Avtomatik && n.Tarix.Date == DateTime.Today)) return;
            await AlAsync(true);
        }

        public static List<Nusxe> Siyahi()
        {
            if (!Directory.Exists(AppPaths.Backup)) return new();
            return Directory.GetFiles(AppPaths.Backup, "*.db.gz").Select(f => new FileInfo(f))
                .Select(f => new Nusxe(f.FullName, f.LastWriteTime, f.Length, f.Name.StartsWith("auto"))).OrderByDescending(n => n.Tarix).ToList();
        }

        private static async Task TemizleAsync()
        {
            int say = await SistemRepo.ParamAsync("NusxeSay", 30);
            foreach (var n in Siyahi().Where(n => n.Avtomatik).Skip(say)) try { File.Delete(n.Fayl); } catch { }
        }

        /// <summary>Bərpa: əvvəlcə indiki baza ayrıca saxlanılır, sonra nüsxə bazanın yerinə açılır. Proqram yenidən başladılmalıdır.</summary>
        public static async Task BerpaAsync(Nusxe n)
        {
            await AlAsync(false);
            SqliteConnection.ClearAllPools();
            string tmp = AppPaths.Db + ".restore";
            await using (var fi = File.OpenRead(n.Fayl))
            await using (var gz = new GZipStream(fi, CompressionMode.Decompress))
            await using (var fo = File.Create(tmp)) await gz.CopyToAsync(fo);
            foreach (var ext in new[] { "-wal", "-shm" }) if (File.Exists(AppPaths.Db + ext)) File.Delete(AppPaths.Db + ext);
            File.Copy(tmp, AppPaths.Db, true); File.Delete(tmp);
        }
    }
}
