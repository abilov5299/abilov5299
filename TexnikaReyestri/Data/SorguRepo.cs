using System.Data;
using System.Text.Json;
using Microsoft.Data.Sqlite;

namespace TexnikaReyestri.Data
{
    public sealed record SorguTexnika(string Guid, string Marka, string ZavodNo, string Status);
    public sealed class SorguHisse { public string Ad { get; set; } public string Texnika { get; set; } public string Vahid { get; set; } public double Teleb { get; set; } public double Qebul { get; set; } }
    public readonly record struct SorguStat(long Cemi, long Edilmeyib, long Qismen, long Tam, long Davam);

    public sealed class Sorgu
    {
        public long Id; public string Guid; public long Versiya;
        public Dictionary<string, string> V = new();
        public List<SorguTexnika> Tex = new();
        public List<SorguHisse> His = new();
    }

    public sealed class SorguFiltr
    {
        public string Zavod, Netice, Icra, Mektub, TarixSahe = "QeydTarixi", TarixDan, TarixDek;
    }

    /// <summary>Təchizat sorğuları (məktub + əlaqəli texnikalar + ehtiyat hissələri). Texnikalar və hissələr JSON sütunlarındadır.</summary>
    public static class SorguRepo
    {
        /// <summary>Mətn sahələri (sütun adı = açar).</summary>
        public static readonly string[] Sahe = { "MektubNo", "QeydTarixi", "Zavod", "IcraMuddeti", "TeminEden", "TeminIcraci", "ZavodMesul", "CavabVez", "CavabTarixi", "Netice", "Qeyd" };
        public static readonly string[] Neticeler = { "Təyin edilməyib", "Təmin edilməyib", "Qismən təmin edilib", "Tam təmin edilib" };
        public static readonly string[] CavabVez = { "İcrası davam edir", "Cavab verilib" };
        public static readonly string[] Vahidler = { "ədəd", "dəst", "litr", "kq", "metr" };
        private static readonly HashSet<string> TarixSahe = new() { "QeydTarixi", "IcraMuddeti", "CavabTarixi" };

        public static void Yarat(SqliteConnection con) => LocalDb.Exec(con, $@"
CREATE TABLE IF NOT EXISTS Sorgular (
  Id INTEGER PRIMARY KEY, Guid TEXT NOT NULL UNIQUE, Versiya INTEGER NOT NULL DEFAULT 1, Deyisdi INTEGER NOT NULL, Silinib INTEGER NOT NULL DEFAULT 0,
  {string.Join(", ", Sahe.Select(s => s + " TEXT"))}, Texnikalar TEXT, Hisseler TEXT);
CREATE INDEX IF NOT EXISTS IX_S_Zavod ON Sorgular(Silinib, Zavod, QeydTarixi);
CREATE INDEX IF NOT EXISTS IX_S_Deyisdi ON Sorgular(Deyisdi);");

        private static string Sert(SorguFiltr f, SqliteCommand c)
        {
            var w = new List<string> { "Silinib=0" };
            void P(string n, object v) => c.Parameters.AddWithValue(n, v);
            if (f.Zavod != null) { w.Add("Zavod=$z"); P("$z", f.Zavod); }
            if (f.Netice != null) { w.Add("IFNULL(Netice,'Təyin edilməyib')=$n"); P("$n", f.Netice); }
            if (f.Icra == "davam") w.Add("IFNULL(CavabVez,'İcrası davam edir')='İcrası davam edir'");
            else if (f.Icra == "cavab") w.Add("CavabVez='Cavab verilib'");
            else if (f.Icra == "late") { w.Add("IFNULL(CavabVez,'İcrası davam edir')='İcrası davam edir' AND IcraMuddeti<$b"); P("$b", Vaxt.Iso(DateTime.Today)); }
            if (!string.IsNullOrWhiteSpace(f.Mektub)) { w.Add("MektubNo LIKE $m"); P("$m", "%" + f.Mektub.Trim() + "%"); }
            string ts = TarixSahe.Contains(f.TarixSahe) ? f.TarixSahe : "QeydTarixi";
            if (f.TarixDan != null) { w.Add($"{ts}>=$td"); P("$td", f.TarixDan); }
            if (f.TarixDek != null) { w.Add($"{ts}<=$tk"); P("$tk", f.TarixDek); }
            return string.Join(" AND ", w);
        }

        public static async Task<DataTable> SiyahiAsync(SorguFiltr f)
        {
            await using var con = await LocalDb.AcAsync();
            using var c = con.CreateCommand();
            c.CommandText = $"SELECT Id, {string.Join(",", Sahe)}, Texnikalar FROM Sorgular WHERE {Sert(f, c)} ORDER BY QeydTarixi DESC, Id DESC LIMIT 5000;";
            await using var r = await c.ExecuteReaderAsync();
            return await LocalDb.CedvelAsync(r);
        }

        public static async Task<SorguStat> StatAsync(string zavod)
        {
            await using var con = await LocalDb.AcAsync();
            using var c = con.CreateCommand();
            c.CommandText = @"SELECT COUNT(*),
  IFNULL(SUM(Netice='Təmin edilməyib'),0), IFNULL(SUM(Netice='Qismən təmin edilib'),0), IFNULL(SUM(Netice='Tam təmin edilib'),0),
  IFNULL(SUM(IFNULL(CavabVez,'İcrası davam edir')='İcrası davam edir'),0)
FROM Sorgular WHERE Silinib=0 AND ($z IS NULL OR Zavod=$z);";
            c.Parameters.AddWithValue("$z", (object)zavod ?? DBNull.Value);
            await using var r = await c.ExecuteReaderAsync(); await r.ReadAsync();
            return new SorguStat(r.GetInt64(0), r.GetInt64(1), r.GetInt64(2), r.GetInt64(3), r.GetInt64(4));
        }

        public static async Task<Sorgu> GetAsync(long id)
        {
            await using var con = await LocalDb.AcAsync();
            using var c = con.CreateCommand();
            c.CommandText = $"SELECT Id, Guid, Versiya, {string.Join(",", Sahe)}, Texnikalar, Hisseler FROM Sorgular WHERE Id=$i;";
            c.Parameters.AddWithValue("$i", id);
            await using var r = await c.ExecuteReaderAsync();
            if (!await r.ReadAsync()) return null;
            var s = new Sorgu { Id = r.GetInt64(0), Guid = r.GetString(1), Versiya = r.GetInt64(2) };
            for (int i = 0; i < Sahe.Length; i++) s.V[Sahe[i]] = r.IsDBNull(3 + i) ? null : r.GetString(3 + i);
            int k = 3 + Sahe.Length;
            if (!r.IsDBNull(k)) s.Tex = JsonSerializer.Deserialize<List<SorguTexnika>>(r.GetString(k)) ?? new();
            if (!r.IsDBNull(k + 1)) s.His = JsonSerializer.Deserialize<List<SorguHisse>>(r.GetString(k + 1)) ?? new();
            return s;
        }

        public static List<SorguTexnika> TexnikaJson(object v) => v is string s ? JsonSerializer.Deserialize<List<SorguTexnika>>(s) ?? new() : new();
        public static List<SorguHisse> HisseJson(object v) => v is string s ? JsonSerializer.Deserialize<List<SorguHisse>>(s) ?? new() : new();

        public static Task<long> SaxlaAsync(Sorgu s) => LocalDb.YazAsync(async (con, tr) =>
        {
            using var c = con.CreateCommand(); c.Transaction = tr;
            if (s.Id == 0) { s.Guid = System.Guid.NewGuid().ToString("N"); s.Versiya = 1; } else s.Versiya++;
            c.CommandText = s.Id == 0
                ? $"INSERT INTO Sorgular (Guid, Versiya, Deyisdi, {string.Join(",", Sahe)}, Texnikalar, Hisseler) VALUES ($g,$v,$d,{string.Join(",", Sahe.Select(x => "$" + x))},$tx,$hs); SELECT last_insert_rowid();"
                : $"UPDATE Sorgular SET Versiya=$v, Deyisdi=$d, {string.Join(",", Sahe.Select(x => $"{x}=${x}"))}, Texnikalar=$tx, Hisseler=$hs WHERE Id=$i; SELECT $i;";
            c.Parameters.AddWithValue("$g", s.Guid); c.Parameters.AddWithValue("$v", s.Versiya); c.Parameters.AddWithValue("$d", Vaxt.Indi); c.Parameters.AddWithValue("$i", s.Id);
            foreach (var x in Sahe) c.Parameters.AddWithValue("$" + x, s.V.TryGetValue(x, out var v) && !string.IsNullOrWhiteSpace(v) ? v.Trim() : DBNull.Value);
            c.Parameters.AddWithValue("$tx", JsonSerializer.Serialize(s.Tex)); c.Parameters.AddWithValue("$hs", JsonSerializer.Serialize(s.His));
            return s.Id = Convert.ToInt64(await c.ExecuteScalarAsync());
        });

        public static Task SilAsync(long id) => LocalDb.YazAsync(async (con, tr) =>
        {
            using var c = con.CreateCommand(); c.Transaction = tr;
            c.CommandText = "UPDATE Sorgular SET Silinib=1, Versiya=Versiya+1, Deyisdi=$d WHERE Id=$i;";
            c.Parameters.AddWithValue("$d", Vaxt.Indi); c.Parameters.AddWithValue("$i", id);
            return await c.ExecuteNonQueryAsync();
        });

        /// <summary>Sorğuya əlavə etmək üçün texnika axtarışı: zavod nömrəsi, lülə nömrəsi və ya marka (ən çox 10).</summary>
        public static async Task<List<SorguTexnika>> TexnikaAxtarAsync(string metn, string zavod)
        {
            await using var con = await LocalDb.AcAsync();
            using var c = con.CreateCommand();
            c.CommandText = "SELECT Guid, IFNULL(Marka,''), IFNULL(ZavodNo,''), IFNULL(Status,'') FROM Texnikalar WHERE Silinib=0 AND ($z IS NULL OR TemirZavodu=$z) " +
                            "AND (ZavodNo=$t OR LuleNo=$t OR Marka LIKE $l) ORDER BY ZavodNo=$t DESC LIMIT 10;";
            c.Parameters.AddWithValue("$z", (object)zavod ?? DBNull.Value); c.Parameters.AddWithValue("$t", metn.Trim()); c.Parameters.AddWithValue("$l", metn.Trim() + "%");
            var l = new List<SorguTexnika>();
            await using var r = await c.ExecuteReaderAsync();
            while (await r.ReadAsync()) l.Add(new SorguTexnika(r.GetString(0), r.GetString(1), r.GetString(2), r.GetString(3)));
            return l;
        }

        // ---------------- USB sinxronizasiya ----------------
        public static async Task<List<Dictionary<string, object>>> DeltaAsync(long since, string zavod)
        {
            await using var con = await LocalDb.AcAsync();
            using var c = con.CreateCommand();
            c.CommandText = $"SELECT Guid, Versiya, Silinib, {string.Join(",", Sahe)}, Texnikalar, Hisseler FROM Sorgular WHERE Deyisdi>$s AND ($z IS NULL OR Zavod=$z);";
            c.Parameters.AddWithValue("$s", since); c.Parameters.AddWithValue("$z", (object)zavod ?? DBNull.Value);
            var l = new List<Dictionary<string, object>>();
            await using var r = await c.ExecuteReaderAsync();
            while (await r.ReadAsync())
            {
                var d = new Dictionary<string, object>();
                for (int i = 0; i < r.FieldCount; i++) if (!r.IsDBNull(i)) d[r.GetName(i)] = r.GetValue(i);
                l.Add(d);
            }
            return l;
        }

        /// <summary>Paketdən gələn sorğular. Versiya bizdəkindən böyükdürsə yazılır; zavod yalnız öz sorğusunu göndərə bilər.</summary>
        public static async Task<(int yeni, int dey, int red)> TetbiqAsync(SqliteConnection con, SqliteTransaction tr, IEnumerable<Dictionary<string, JsonElement>> qeydler, string yalnizZavod, CancellationToken ct)
        {
            int yeni = 0, dey = 0, red = 0; var all = Sahe.Concat(new[] { "Texnikalar", "Hisseler" }).ToArray();
            using var sel = con.CreateCommand(); sel.Transaction = tr; sel.CommandText = "SELECT Versiya, IFNULL(Zavod,'') FROM Sorgular WHERE Guid=$g;";
            var pg = sel.Parameters.Add("$g", SqliteType.Text);
            using var up = con.CreateCommand(); up.Transaction = tr;
            up.CommandText = $"INSERT INTO Sorgular (Guid, Versiya, Deyisdi, Silinib, {string.Join(",", all)}) VALUES ($g,$v,$d,$s,{string.Join(",", all.Select(k => "$" + k))}) " +
                             $"ON CONFLICT(Guid) DO UPDATE SET Versiya=excluded.Versiya, Deyisdi=excluded.Deyisdi, Silinib=excluded.Silinib, {string.Join(",", all.Select(k => $"{k}=excluded.{k}"))};";
            var p = new[] { "$g", "$v", "$d", "$s" }.Concat(all.Select(k => "$" + k)).ToDictionary(n => n, n => up.Parameters.Add(n, SqliteType.Text));
            long indi = Vaxt.Indi;
            foreach (var q in qeydler)
            {
                ct.ThrowIfCancellationRequested();
                string guid = q["Guid"].GetString(); long ver = q["Versiya"].GetInt64();
                string S(string k) => q.TryGetValue(k, out var v) && v.ValueKind == JsonValueKind.String ? v.GetString() : null;
                pg.Value = guid; long? mv = null; string mz = null;
                await using (var rd = await sel.ExecuteReaderAsync(ct)) if (await rd.ReadAsync(ct)) { mv = rd.GetInt64(0); mz = rd.GetString(1); }
                if (yalnizZavod != null && (S("Zavod") != yalnizZavod || (mz != null && mz != yalnizZavod))) { red++; continue; }
                if (mv >= ver) continue;
                p["$g"].Value = guid; p["$v"].Value = ver; p["$d"].Value = indi;
                p["$s"].Value = q.TryGetValue("Silinib", out var sv) && sv.ValueKind == JsonValueKind.Number ? sv.GetInt64() : 0;
                foreach (var k in all) p["$" + k].Value = (object)S(k) ?? DBNull.Value;
                await up.ExecuteNonQueryAsync(ct);
                if (mv is null) yeni++; else dey++;
            }
            return (yeni, dey, red);
        }
    }
}
