using System.Data;
using System.Text.Json;
using Microsoft.Data.Sqlite;

namespace TexnikaReyestri.Data
{
    public sealed record SorguTexnika(string Guid, string Marka, string ZavodNo, string Status);
    public sealed class SorguQeyd { public string Tarix { get; set; } public string Netice { get; set; } public string Gozlenilen { get; set; } }
    /// <summary>
    /// Məktubun ehtiyat hissəsi / materialı. Texnika — "hansı texnika üçün" sifariş olunub ("Marka (ZavodNo)", yalnız məlumat; TexGuid ilə).
    /// Qəbullar zavodun anbarına düşür; Qebul — qəbulların cəmi (köhnə paketlər üçün ayrıca da saxlanır).
    /// </summary>
    public sealed class SorguHisse
    {
        public string Ad { get; set; } public string Kod { get; set; } public string Texnika { get; set; } public string TexGuid { get; set; }
        public string Vahid { get; set; } public double Teleb { get; set; } public double Qebul { get; set; }
        public List<HisseQebul> Qebullar { get; set; } = new();
        public double Qaliq => Math.Max(0, Teleb - Qebul);
    }
    /// <summary>"Sorğular üzrə təminat" cədvəlinin bir sətri: məktubun bir hissəsi, ona düşən sərf və anbar qalığı.</summary>
    public sealed record TeminatSetir(long SorguId, string MektubNo, string MektubTarix, string TeminEden, string Texnika, string Ad, string Kod, string Vahid,
        double Teleb, double Qebul, double Serf, string Netice, string IlkQebul)
    {
        public double Anbar => Math.Max(0, Qebul - Serf);
    }
    /// <summary>Anbardakı material (zavod üzrə; ad + kod + vahid üzrə cəmlənir).</summary>
    public sealed record AnbarMal(string Ad, string Kod, string Vahid, double Qebul, double Serf)
    {
        public double Qaliq => Math.Max(0, Qebul - Serf);
    }
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
        public static readonly string[] Sahe = { "MektubNo", "QeydTarixi", "Zavod", "IcraMuddeti", "TeminEden", "TeminIcraci", "ZavodMesul", "CavabVez", "CavabTarixi", "Netice", "Qeyd", "Telefon", "Qeydler" };
        public static readonly string[] Neticeler = { "Təyin edilməyib", "Təmin edilməyib", "Qismən təmin edilib", "Tam təmin edilib" };
        public static readonly string[] CavabVez = { "İcrası davam edir", "İcra başa çatıb" };
        public static readonly string[] Vahidler = { "ədəd", "dəst", "litr", "kq", "metr" };
        private static readonly HashSet<string> TarixSahe = new() { "QeydTarixi", "IcraMuddeti", "CavabTarixi" };

        public static void Yarat(SqliteConnection con)
        {
            LocalDb.Exec(con, $@"
CREATE TABLE IF NOT EXISTS Sorgular (
  Id INTEGER PRIMARY KEY, Guid TEXT NOT NULL UNIQUE, Versiya INTEGER NOT NULL DEFAULT 1, Deyisdi INTEGER NOT NULL, Silinib INTEGER NOT NULL DEFAULT 0,
  {string.Join(", ", Sahe.Select(s => s + " TEXT"))}, Texnikalar TEXT, Hisseler TEXT);
CREATE INDEX IF NOT EXISTS IX_S_Zavod ON Sorgular(Silinib, Zavod, QeydTarixi);
CREATE INDEX IF NOT EXISTS IX_S_Deyisdi ON Sorgular(Deyisdi);");
            foreach (var x in Sahe.Where(x => !Miqrasiya.SutunVar(con, "Sorgular", x))) LocalDb.Exec(con, $"ALTER TABLE Sorgular ADD COLUMN {x} TEXT;");
        }

        private static string Sert(SorguFiltr f, SqliteCommand c)
        {
            var w = new List<string> { "Silinib=0" };
            void P(string n, object v) => c.Parameters.AddWithValue(n, v);
            if (f.Zavod != null) { w.Add("Zavod=$z"); P("$z", f.Zavod); }
            if (f.Netice != null) { w.Add("IFNULL(Netice,'Təyin edilməyib')=$n"); P("$n", f.Netice); }
            if (f.Icra == "davam") w.Add("IFNULL(CavabVez,'İcrası davam edir')='İcrası davam edir'");
            else if (f.Icra == "cavab") w.Add("CavabVez='İcra başa çatıb'");
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

        /// <summary>Hissə yoxdur — "Təyin edilməyib"; heç nə qəbul olunmayıb — "Təmin edilməyib"; hamısı tam — "Tam təmin edilib"; qalanı — "Qismən təmin edilib".</summary>
        public static string Netice(List<SorguHisse> his) =>
            his.Count == 0 ? "Təyin edilməyib" : his.All(h => h.Qebul <= 0) ? "Təmin edilməyib" : his.All(h => h.Qebul >= h.Teleb) ? "Tam təmin edilib" : "Qismən təmin edilib";

        private static string Acar(string ad, string kod, string vahid) => $"{(ad ?? "").Trim().ToLowerInvariant()}|{(kod ?? "").Trim().ToLowerInvariant()}|{(vahid ?? "").Trim().ToLowerInvariant()}";

        /// <summary>Zavodun anbarı: məktublarda qəbul edilmiş hissələr − texnikalara anbardan sərf olunanlar.</summary>
        public static async Task<List<AnbarMal>> AnbarAsync(string zavod)
        {
            var (setir, serf) = await TeminatMelumatAsync(zavod);
            var l = setir.GroupBy(s => Acar(s.Ad, s.Kod, s.Vahid)).Select(g => { var f = g.First(); return new AnbarMal(f.Ad, f.Kod, f.Vahid, g.Sum(x => x.Qebul), serf.GetValueOrDefault(g.Key)); }).ToList();
            return l.OrderBy(x => x.Ad).ToList();
        }

        /// <summary>
        /// Məktubların bütün hissələri (zavod üzrə) və hər birinə düşən sərf. Anbar zavod üzrə ümumidir: eyni ad, kod və vahidli
        /// hissələrin sərfi ilk qəbul tarixinə görə köhnədən yeniyə paylanır (hər sətrə ən çox onun qəbulu qədər).
        /// </summary>
        public static async Task<List<TeminatSetir>> TeminatAsync(string zavod) => (await TeminatMelumatAsync(zavod)).setir;

        private static async Task<(List<TeminatSetir> setir, Dictionary<string, double> serf)> TeminatMelumatAsync(string zavod)
        {
            var xam = new List<(TeminatSetir s, string acar)>();
            var serf = new Dictionary<string, double>();
            await using var con = await LocalDb.AcAsync();
            using (var c = con.CreateCommand())
            {
                c.CommandText = "SELECT Id, MektubNo, QeydTarixi, TeminEden, Hisseler FROM Sorgular WHERE Silinib=0 AND ($z IS NULL OR Zavod=$z) ORDER BY QeydTarixi, Id;";
                c.Parameters.AddWithValue("$z", (object)zavod ?? DBNull.Value);
                await using var r = await c.ExecuteReaderAsync();
                while (await r.ReadAsync())
                    foreach (var h in HisseJson(r.IsDBNull(4) ? null : r.GetString(4)))
                    {
                        string ilk = h.Qebullar.Count > 0 ? h.Qebullar.Min(q => q.Tarix) : null;
                        xam.Add((new TeminatSetir(r.GetInt64(0), r.IsDBNull(1) ? "" : r.GetString(1), r.IsDBNull(2) ? null : r.GetString(2), r.IsDBNull(3) ? "" : r.GetString(3),
                            h.Texnika, h.Ad, h.Kod, h.Vahid, h.Teleb, h.Qebul, 0, h.Qebul <= 0 ? "Təmin edilməyib" : h.Qebul >= h.Teleb ? "Tam təmin edilib" : "Qismən təmin edilib", ilk), Acar(h.Ad, h.Kod, h.Vahid)));
                    }
            }
            using (var c = con.CreateCommand())
            {
                c.CommandText = "SELECT Serfiyyat FROM Texnikalar WHERE Silinib=0 AND ($z IS NULL OR TemirZavodu=$z) AND IFNULL(Serfiyyat,'')<>'';";
                c.Parameters.AddWithValue("$z", (object)zavod ?? DBNull.Value);
                await using var r = await c.ExecuteReaderAsync();
                while (await r.ReadAsync())
                    foreach (var x in Data.TexnikaJson.Serfler(r.GetString(0)).Where(x => x.Anbar))
                    { string k = Acar(x.Ad, x.Kod, x.Vahid); serf[k] = serf.GetValueOrDefault(k) + x.Miqdar; }
            }
            var qalan = new Dictionary<string, double>(serf);
            var setir = new List<TeminatSetir>();
            foreach (var (s, k) in xam.OrderBy(x => x.s.IlkQebul ?? "9999").ThenBy(x => x.s.SorguId))
            {
                double pay = Math.Min(s.Qebul, qalan.GetValueOrDefault(k));
                if (pay > 0) qalan[k] -= pay;
                setir.Add(s with { Serf = pay });
            }
            return (setir, serf);
        }

        public static List<SorguQeyd> QeydJson(string v) => string.IsNullOrEmpty(v) ? new() : JsonSerializer.Deserialize<List<SorguQeyd>>(v) ?? new();

        private const string TexnikaSert = "EXISTS (SELECT 1 FROM json_each(Sorgular.Texnikalar) j WHERE json_extract(j.value,'$.Guid')=$tg)";

        /// <summary>Texnikanın bağlı olduğu məktublar (yeni əvvəldə).</summary>
        public static async Task<List<Sorgu>> TexnikaUzreAsync(string texGuid)
        {
            var idler = new List<long>();
            await using (var con = await LocalDb.AcAsync())
            using (var c = con.CreateCommand())
            {
                c.CommandText = $"SELECT Id FROM Sorgular WHERE Silinib=0 AND {TexnikaSert} ORDER BY QeydTarixi DESC, Id DESC;";
                c.Parameters.AddWithValue("$tg", texGuid ?? "");
                await using var r = await c.ExecuteReaderAsync();
                while (await r.ReadAsync()) idler.Add(r.GetInt64(0));
            }
            var l = new List<Sorgu>(); foreach (var id in idler) l.Add(await GetAsync(id));
            return l;
        }

        /// <summary>Qeydiyyatdakı məktublar (texnikaya bağlamaq üçün): nömrəyə görə axtarış, zavod üzrə. Ən çox 50.</summary>
        public static async Task<List<(long id, string ad)>> MektublarAsync(string zavod, string metn)
        {
            await using var con = await LocalDb.AcAsync();
            using var c = con.CreateCommand();
            c.CommandText = "SELECT Id, IFNULL(MektubNo,'') || ' · ' || IFNULL(QeydTarixi,'') || ' · ' || IFNULL(TeminEden,'') FROM Sorgular WHERE Silinib=0 AND ($z IS NULL OR Zavod=$z) AND ($t='' OR MektubNo LIKE $l) ORDER BY QeydTarixi DESC, Id DESC LIMIT 50;";
            c.Parameters.AddWithValue("$z", (object)zavod ?? DBNull.Value); c.Parameters.AddWithValue("$t", (metn ?? "").Trim()); c.Parameters.AddWithValue("$l", "%" + (metn ?? "").Trim() + "%");
            var l = new List<(long, string)>(); await using var r = await c.ExecuteReaderAsync();
            while (await r.ReadAsync()) l.Add((r.GetInt64(0), r.GetString(1)));
            return l;
        }

        /// <summary>
        /// Texnika yadda saxlananda: ona bağlı məktubların ehtiyat hissələri (texnikaların siyahısından toplanır) və təminat nəticəsi yenilənir.
        /// Nəticə: hissə yoxdur — "Təyin edilməyib"; heç nə qəbul olunmayıb — "Təmin edilməyib"; hamısı — "Tam"; qalanı — "Qismən".
        /// </summary>
        internal static async Task TexnikaUzreYenileAsync(SqliteConnection con, SqliteTransaction tr, string texGuid)
        {
            var sorgular = new List<(long id, List<SorguTexnika> tex)>();
            using (var c = con.CreateCommand())
            {
                c.Transaction = tr; c.CommandText = $"SELECT Id, Texnikalar FROM Sorgular WHERE Silinib=0 AND {TexnikaSert};";
                c.Parameters.AddWithValue("$tg", texGuid);
                await using var r = await c.ExecuteReaderAsync();
                while (await r.ReadAsync()) sorgular.Add((r.GetInt64(0), TexnikaJson(r.IsDBNull(1) ? null : r.GetString(1))));
            }
            foreach (var (id, tex) in sorgular)
            {
                var his = new List<SorguHisse>();
                foreach (var t in tex)
                {
                    using var c = con.CreateCommand(); c.Transaction = tr;
                    c.CommandText = "SELECT Hisseler FROM Texnikalar WHERE Guid=$g AND Silinib=0;"; c.Parameters.AddWithValue("$g", t.Guid);
                    foreach (var h in Data.TexnikaJson.Hisseler(await c.ExecuteScalarAsync()))
                        his.Add(new SorguHisse { Ad = h.Ad, Texnika = $"{t.Marka} ({t.ZavodNo})", Vahid = h.Vahid, Teleb = h.Teleb, Qebul = h.Qebul });
                }
                string netice = his.Count == 0 ? "Təyin edilməyib" : his.All(h => h.Qebul <= 0) ? "Təmin edilməyib" : his.All(h => h.Qebul >= h.Teleb) ? "Tam təmin edilib" : "Qismən təmin edilib";
                using var u = con.CreateCommand(); u.Transaction = tr;
                u.CommandText = "UPDATE Sorgular SET Hisseler=$h, Netice=$n, Versiya=Versiya+1, Deyisdi=$d WHERE Id=$i AND (IFNULL(Hisseler,'')<>$h OR IFNULL(Netice,'')<>$n);";
                u.Parameters.AddWithValue("$h", JsonSerializer.Serialize(his)); u.Parameters.AddWithValue("$n", netice); u.Parameters.AddWithValue("$d", Vaxt.Indi); u.Parameters.AddWithValue("$i", id);
                await u.ExecuteNonQueryAsync();
            }
            // Hissəsi yazılmamış texnika məktuba bağlıdırsa — "Təminat gözlənilir"; məktubu da yoxdursa — boş
            using (var t = con.CreateCommand())
            {
                t.Transaction = tr;
                t.CommandText = @"UPDATE Texnikalar SET Teminat=$tm, Versiya=Versiya+1, Deyisdi=$d
 WHERE Guid=$tg AND IFNULL(Hisseler,'[]')='[]' AND IFNULL(Teminat,'')<>IFNULL($tm,'');";
                t.Parameters.AddWithValue("$tm", sorgular.Count > 0 ? "Təminat gözlənilir" : DBNull.Value);
                t.Parameters.AddWithValue("$tg", texGuid); t.Parameters.AddWithValue("$d", Vaxt.Indi);
                await t.ExecuteNonQueryAsync();
            }
        }

        /// <summary>Texnikanı məktubun texnika siyahısına əlavə edir (artıq varsa, dəyişmir).</summary>
        internal static async Task TexnikaBaglaAsync(SqliteConnection con, SqliteTransaction tr, long sorguId, string texGuid, Dictionary<string, object> t)
        {
            using var c = con.CreateCommand(); c.Transaction = tr; c.CommandText = "SELECT Texnikalar FROM Sorgular WHERE Id=$i;"; c.Parameters.AddWithValue("$i", sorguId);
            var l = TexnikaJson(await c.ExecuteScalarAsync());
            if (l.Any(x => x.Guid == texGuid)) return;
            l.Add(new SorguTexnika(texGuid, t.GetValueOrDefault("Marka") as string ?? "", t.GetValueOrDefault("ZavodNo") as string ?? "", t.GetValueOrDefault("Status") as string ?? ""));
            using var u = con.CreateCommand(); u.Transaction = tr;
            u.CommandText = "UPDATE Sorgular SET Texnikalar=$t, Versiya=Versiya+1, Deyisdi=$d WHERE Id=$i;";
            u.Parameters.AddWithValue("$t", JsonSerializer.Serialize(l)); u.Parameters.AddWithValue("$d", Vaxt.Indi); u.Parameters.AddWithValue("$i", sorguId);
            await u.ExecuteNonQueryAsync();
        }

        /// <summary>Mövcud texnikanı məktuba bağlayır (bağlı > true) və ya ayırır; sonra hissələr və nəticə yenilənir.</summary>
        public static Task BaglaAsync(long sorguId, string texGuid, Dictionary<string, object> t, bool bagli) => LocalDb.YazAsync(async (con, tr) =>
        {
            if (bagli) await TexnikaBaglaAsync(con, tr, sorguId, texGuid, t);
            else
            {
                using var c = con.CreateCommand(); c.Transaction = tr; c.CommandText = "SELECT Texnikalar FROM Sorgular WHERE Id=$i;"; c.Parameters.AddWithValue("$i", sorguId);
                var l = TexnikaJson(await c.ExecuteScalarAsync()); l.RemoveAll(x => x.Guid == texGuid);
                using var u = con.CreateCommand(); u.Transaction = tr;
                u.CommandText = "UPDATE Sorgular SET Texnikalar=$t, Versiya=Versiya+1, Deyisdi=$d WHERE Id=$i;";
                u.Parameters.AddWithValue("$t", JsonSerializer.Serialize(l)); u.Parameters.AddWithValue("$d", Vaxt.Indi); u.Parameters.AddWithValue("$i", sorguId);
                await u.ExecuteNonQueryAsync();
                // Ayrılan məktubun hissə siyahısı qalan texnikalardan yenidən toplanır
                foreach (var x in l.Take(1)) await TexnikaUzreYenileAsync(con, tr, x.Guid);
                if (l.Count == 0) { using var z = con.CreateCommand(); z.Transaction = tr; z.CommandText = "UPDATE Sorgular SET Hisseler='[]', Netice='Təyin edilməyib' WHERE Id=$i;"; z.Parameters.AddWithValue("$i", sorguId); await z.ExecuteNonQueryAsync(); }
            }
            await TexnikaUzreYenileAsync(con, tr, texGuid);
            return 0;
        });

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
            foreach (var h in s.His) if (h.Qebullar.Count > 0) h.Qebul = h.Qebullar.Sum(q => q.Miqdar);
            c.Parameters.AddWithValue("$tx", JsonSerializer.Serialize(s.Tex)); c.Parameters.AddWithValue("$hs", JsonSerializer.Serialize(s.His));
            c.Parameters["$Netice"].Value = Netice(s.His);   // məktubun nəticəsi öz hissələrindən hesablanır
            return s.Id = Convert.ToInt64(await c.ExecuteScalarAsync());
        });

        public static Task SilAsync(long id) => LocalDb.YazAsync(async (con, tr) =>
        {
            using var c = con.CreateCommand(); c.Transaction = tr;
            c.CommandText = "UPDATE Sorgular SET Silinib=1, Versiya=Versiya+1, Deyisdi=$d WHERE Id=$i;";
            c.Parameters.AddWithValue("$d", Vaxt.Indi); c.Parameters.AddWithValue("$i", id);
            return await c.ExecuteNonQueryAsync();
        });

        /// <summary>Sorğuya əlavə etmək üçün texnika axtarışı: zavod nömrəsi, naryad nömrəsi, marka və ya növ (mətn içində). Boş mətn: zavodun texnikaları. Ən çox <paramref name="limit"/> (0 — hamısı).</summary>
        public static async Task<List<SorguTexnika>> TexnikaAxtarAsync(string metn, string zavod, int limit = 30)
        {
            await using var con = await LocalDb.AcAsync();
            using var c = con.CreateCommand();
            c.CommandText = "SELECT Guid, IFNULL(Marka,''), IFNULL(ZavodNo,''), IFNULL(Status,'') FROM Texnikalar WHERE Silinib=0 AND ($z IS NULL OR TemirZavodu=$z) " +
                            "AND ($t='' OR ZavodNo LIKE $l OR NaryadNo LIKE $l OR Marka LIKE $l OR Nov LIKE $l) ORDER BY ZavodNo=$t DESC, Marka" + (limit > 0 ? $" LIMIT {limit};" : ";");
            c.Parameters.AddWithValue("$z", (object)zavod ?? DBNull.Value); c.Parameters.AddWithValue("$t", metn.Trim()); c.Parameters.AddWithValue("$l", "%" + metn.Trim() + "%");
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
