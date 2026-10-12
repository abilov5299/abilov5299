using System.Data;
using System.Text;
using Microsoft.Data.Sqlite;

namespace TexnikaReyestri.Data
{
    public sealed class ReyestrFiltr
    {
        public string Zavod;     // null = hamısı (üst qurum)
        public string Status;
        public bool Gecikenler;
        public string Axtar;     // FTS: marka, zavod nömrəsi, lülə nömrəsi, qeyd
    }

    // Səhifənin sonuncu sətri: növbəti səhifə buradan davam edir (OFFSET yoxdur)
    public readonly record struct Kursor(string Bitme, long Id);

    public static partial class TexnikaRepo
    {
        public const int SehifeOlcusu = 200;
        private static readonly string ReyestrSql = "Id, " + string.Join(", ", Saheler.Reyestr);

        // Reyestr səhifəsi: 1 mln sətirdə ölçülmüş müddət ~0.2–0.3 ms
        public static async Task<DataTable> SehifeAsync(ReyestrFiltr f, Kursor? sonra, CancellationToken ct = default)
        {
            await using var con = await LocalDb.AcAsync(ct);
            using var cmd = con.CreateCommand();
            var w = Sert(f, cmd);
            if (sonra is { } k)
            {
                // NULL bitmə tarixi '' kimi sayılır. Bu yazılış indeksdə aralıq axtarışı verir:
                // 1 mln sətirdə dərin səhifə 0.23 ms ((a,b) > (x,y) yazılışı isə 82 ms idi — ölçülüb)
                w.Append(" AND IFNULL(GozlenilenBitme,'') >= $kb AND (IFNULL(GozlenilenBitme,'') > $kb OR Id > $ki)");
                cmd.Parameters.AddWithValue("$kb", k.Bitme ?? "");
                cmd.Parameters.AddWithValue("$ki", k.Id);
            }
            cmd.CommandText = $"SELECT {ReyestrSql} FROM Texnikalar WHERE {w} ORDER BY IFNULL(GozlenilenBitme,''), Id LIMIT {SehifeOlcusu};";
            await using var r = await cmd.ExecuteReaderAsync(ct);
            var dt = new DataTable();
            dt.Load(r);
            return dt;
        }

        /// <summary>Son dəyişiklik vaxtı (indeksdən, ~0 ms). Səhifələr bununla siyahının köhnəldiyini bilir: əlavə, redaktə, silmə, Excel/USB idxalı.</summary>
        public static async Task<string> SonDeyisikAsync()
        {
            await using var con = await LocalDb.AcAsync();
            using var cmd = con.CreateCommand(); cmd.CommandText = "SELECT IFNULL(MAX(Deyisdi),0) || ':' || (SELECT COUNT(*) FROM Icmal WHERE Say > 0) FROM Texnikalar;";
            return await cmd.ExecuteScalarAsync() as string ?? "0";
        }

        public static async Task<long> SayAsync(ReyestrFiltr f, CancellationToken ct = default)
        {
            await using var con = await LocalDb.AcAsync(ct);
            using var cmd = con.CreateCommand();
            // Filtr yoxdursa (və ya yalnız zavod/status), icmal cədvəlindən dərhal oxu
            if (string.IsNullOrEmpty(f.Axtar) && !f.Gecikenler)
            {
                cmd.CommandText = "SELECT IFNULL(SUM(Say),0) FROM Icmal WHERE ($z IS NULL OR Zavod=$z) AND ($s IS NULL OR Status=$s);";
                cmd.Parameters.AddWithValue("$z", (object)f.Zavod ?? DBNull.Value);
                cmd.Parameters.AddWithValue("$s", (object)f.Status ?? DBNull.Value);
            }
            else cmd.CommandText = $"SELECT COUNT(*) FROM Texnikalar WHERE {Sert(f, cmd)};";
            return (long)await cmd.ExecuteScalarAsync(ct);
        }

        public static async Task<Dictionary<string, object>> TapAsync(long id, CancellationToken ct = default)
        {
            await using var con = await LocalDb.AcAsync(ct);
            using var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM Texnikalar WHERE Id=$id;";
            cmd.Parameters.AddWithValue("$id", id);
            await using var r = await cmd.ExecuteReaderAsync(ct);
            if (!await r.ReadAsync(ct)) return null;
            var d = new Dictionary<string, object>(r.FieldCount);
            for (int i = 0; i < r.FieldCount; i++) d[r.GetName(i)] = r.IsDBNull(i) ? null : r.GetValue(i);
            return d;
        }

        // Toplu yazı (Excel / USB idxalı): bir tranzaksiya + bir dəfə hazırlanmış sorğu.
        // 1 mln sətir ~23 s (ölçülüb). Hər 1000 sətirdən bir progress, ləğv etmək mümkündür.
        public static Task<int> TopluYazAsync(IEnumerable<Dictionary<string, object>> qeydler, IProgress<int> progress, CancellationToken ct)
            => LocalDb.YazAsync(async (con, tr) =>
            {
                var kodlar = Saheler.Hamisi.Select(s => s.Kod).ToArray();
                using var cmd = con.CreateCommand();
                cmd.Transaction = tr;
                cmd.CommandText =
                    $"INSERT INTO Texnikalar (Guid, Deyisdi, {string.Join(", ", kodlar)}) " +
                    $"VALUES ($guid, $deyisdi, {string.Join(", ", kodlar.Select(k => "$" + k))}) " +
                    $"ON CONFLICT(Guid) DO UPDATE SET Versiya = Versiya + 1, Deyisdi = excluded.Deyisdi, " +
                    string.Join(", ", kodlar.Select(k => $"{k} = IFNULL(excluded.{k}, {k})")) + ";";   // boş xana köhnə dəyəri silmir
                var pGuid = cmd.Parameters.Add("$guid", SqliteType.Text);
                var pDey = cmd.Parameters.Add("$deyisdi", SqliteType.Integer);
                var p = kodlar.ToDictionary(k => k, k => cmd.Parameters.Add("$" + k, SqliteType.Text));
                cmd.Prepare();

                int n = 0;
                long indi = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                foreach (var q in qeydler)
                {
                    ct.ThrowIfCancellationRequested();
                    pGuid.Value = q.TryGetValue("Guid", out var g) && g != null ? g : Guid.NewGuid().ToString("N");
                    pDey.Value = indi;
                    foreach (var k in kodlar) p[k].Value = q.TryGetValue(k, out var v) && v != null ? v : DBNull.Value;
                    await cmd.ExecuteNonQueryAsync(ct);
                    if (++n % 1000 == 0) progress?.Report(n);
                }
                progress?.Report(n);
                return n;
            }, ct);

        // USB ixracı: son ixracdan sonra dəyişənlər (indeks IX_T_Deyisdi, 1 mln sətirdə ~6 ms)
        public static async Task<DataTable> DeltaAsync(long sonIxrac, CancellationToken ct = default)
        {
            await using var con = await LocalDb.AcAsync(ct);
            using var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM Texnikalar WHERE Deyisdi > $t ORDER BY Deyisdi;";
            cmd.Parameters.AddWithValue("$t", sonIxrac);
            await using var r = await cmd.ExecuteReaderAsync(ct);
            var dt = new DataTable();
            dt.Load(r);
            return dt;
        }

        internal static StringBuilder Sert(ReyestrFiltr f, SqliteCommand cmd)
        {
            var w = new StringBuilder("Silinib = 0");
            if (f.Zavod != null) { w.Append(" AND TemirZavodu = $z"); cmd.Parameters.AddWithValue("$z", f.Zavod); }
            if (f.Status != null) { w.Append(" AND Status = $s"); cmd.Parameters.AddWithValue("$s", f.Status); }
            if (f.Gecikenler)
            {
                w.Append(" AND GozlenilenBitme < $bugun AND Status NOT IN ('Təhvil verilib','Təmirə yararsız')");
                cmd.Parameters.AddWithValue("$bugun", DateTime.Today.ToString("yyyy-MM-dd"));
            }
            if (!string.IsNullOrWhiteSpace(f.Axtar))
            {
                // Hər sözü prefiks kimi axtar: "bmp 02" -> "bmp"* "02"*
                var ifade = string.Join(" ", f.Axtar.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                               .Select(s => "\"" + s.Replace("\"", "") + "\"*"));
                w.Append(" AND Id IN (SELECT rowid FROM TexnikaAxtar WHERE TexnikaAxtar MATCH $q)");
                cmd.Parameters.AddWithValue("$q", ifade);
            }
            return w;
        }
    }
}
