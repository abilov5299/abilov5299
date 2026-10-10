using System.Data;
using Microsoft.Data.Sqlite;

namespace TexnikaReyestri.Data
{
    public sealed record Qurum(string Kod, string Ad, QurumTip Tip, string UstKod);
    public sealed record Istifadeci(string Login, string AdSoyad, string QurumKod, string Rol, bool Aktiv, int YanlisCehd, bool Muveqqeti, long? SonGiris);
    public sealed record Bildiris(long Id, string Nov, string Basliq, string Metn, string Zavod, long? TexnikaId, long Tarix, bool Oxunub);

    /// <summary>Kiçik sistem cədvəlləri üçün sorğular (qurumlar, istifadəçilər, parametrlər, loglar, bildirişlər).</summary>
    public static class SistemRepo
    {
        // ---------- Qurumlar ----------
        public static async Task<List<Qurum>> QurumlarAsync()
        {
            await using var con = await LocalDb.AcAsync();
            using var cmd = con.CreateCommand(); cmd.CommandText = "SELECT Kod, Ad, Tip, UstKod FROM Qurumlar ORDER BY Tip DESC, Kod;";
            await using var r = await cmd.ExecuteReaderAsync();
            var l = new List<Qurum>();
            while (await r.ReadAsync()) l.Add(new(r.GetString(0), r.GetString(1), (QurumTip)r.GetInt32(2), r.IsDBNull(3) ? null : r.GetString(3)));
            return l;
        }

        public static Task QurumSaxlaAsync(Qurum q) => LocalDb.YazAsync(async (con, tr) =>
        {
            using var cmd = con.CreateCommand(); cmd.Transaction = tr;
            cmd.CommandText = "INSERT INTO Qurumlar VALUES ($k,$a,$t,$u) ON CONFLICT(Kod) DO UPDATE SET Ad=excluded.Ad;";
            cmd.Parameters.AddWithValue("$k", q.Kod); cmd.Parameters.AddWithValue("$a", q.Ad);
            cmd.Parameters.AddWithValue("$t", (int)q.Tip); cmd.Parameters.AddWithValue("$u", (object)q.UstKod ?? DBNull.Value);
            return await cmd.ExecuteNonQueryAsync();
        });

        // ---------- İstifadəçilər ----------
        public static async Task<List<Istifadeci>> IstifadecilerAsync()
        {
            await using var con = await LocalDb.AcAsync();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT Login, AdSoyad, QurumKod, Rol, Aktiv, YanlisCehd, Muveqqeti, SonGiris FROM Istifadeciler ORDER BY QurumKod, Login;";
            await using var r = await cmd.ExecuteReaderAsync();
            var l = new List<Istifadeci>();
            while (await r.ReadAsync()) l.Add(new(r.GetString(0), r.GetString(1), r.GetString(2), r.GetString(3), r.GetInt32(4) == 1, r.GetInt32(5), r.GetInt32(6) == 1, r.IsDBNull(7) ? null : r.GetInt64(7)));
            return l;
        }

        // ---------- Parametrlər ----------
        public static async Task<int> ParamAsync(string ad, int susma)
        {
            await using var con = await LocalDb.AcAsync();
            using var cmd = con.CreateCommand(); cmd.CommandText = "SELECT Deyer FROM Parametrler WHERE Ad=$a;"; cmd.Parameters.AddWithValue("$a", ad);
            return int.TryParse(await cmd.ExecuteScalarAsync() as string, out var v) ? v : susma;
        }

        public static async Task<string> ParamStrAsync(string ad)
        {
            await using var con = await LocalDb.AcAsync();
            using var cmd = con.CreateCommand(); cmd.CommandText = "SELECT Deyer FROM Parametrler WHERE Ad=$a;"; cmd.Parameters.AddWithValue("$a", ad);
            return await cmd.ExecuteScalarAsync() as string;
        }

        public static Task ParamYazAsync(string ad, string deyer) => LocalDb.YazAsync(async (con, tr) =>
        {
            using var cmd = con.CreateCommand(); cmd.Transaction = tr;
            cmd.CommandText = "INSERT INTO Parametrler VALUES ($a,$d) ON CONFLICT(Ad) DO UPDATE SET Deyer=excluded.Deyer;";
            cmd.Parameters.AddWithValue("$a", ad); cmd.Parameters.AddWithValue("$d", (object)deyer ?? DBNull.Value);
            return await cmd.ExecuteNonQueryAsync();
        });

        // ---------- Loglar ----------
        /// <summary>Log yazılmasa belə proqram dayanmamalıdır.</summary>
        public static async Task LogAsync(string nov, string mesaj)
        {
            try
            {
                await LocalDb.YazAsync(async (con, tr) =>
                {
                    using var cmd = con.CreateCommand(); cmd.Transaction = tr;
                    cmd.CommandText = "INSERT INTO Loglar (Tarix, Istifadeci, Zavod, Nov, Mesaj) VALUES ($t,$i,$z,$n,$m);";
                    cmd.Parameters.AddWithValue("$t", Vaxt.Indi); cmd.Parameters.AddWithValue("$i", (object)Session.AdSoyad ?? "Sistem");
                    cmd.Parameters.AddWithValue("$z", (object)Session.QurumAd ?? DBNull.Value); cmd.Parameters.AddWithValue("$n", nov); cmd.Parameters.AddWithValue("$m", mesaj);
                    return await cmd.ExecuteNonQueryAsync();
                });
            }
            catch { }
        }

        public static async Task<DataTable> LoglarAsync(DateTime bas, DateTime son, string nov, int limit = 1000)
        {
            await using var con = await LocalDb.AcAsync();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT Tarix, Istifadeci, Zavod, Nov, Mesaj FROM Loglar WHERE Tarix BETWEEN $b AND $s AND ($n IS NULL OR Nov=$n) " +
                              "AND ($z IS NULL OR Zavod=$z OR Zavod IS NULL) ORDER BY Tarix DESC LIMIT $l;";
            cmd.Parameters.AddWithValue("$b", new DateTimeOffset(bas.Date).ToUnixTimeMilliseconds());
            cmd.Parameters.AddWithValue("$s", new DateTimeOffset(son.Date.AddDays(1)).ToUnixTimeMilliseconds());
            cmd.Parameters.AddWithValue("$n", (object)nov ?? DBNull.Value); cmd.Parameters.AddWithValue("$z", (object)Session.ZavodFiltri ?? DBNull.Value);
            cmd.Parameters.AddWithValue("$l", limit);
            await using var r = await cmd.ExecuteReaderAsync();
            var dt = new DataTable(); dt.Load(r); return dt;
        }

        public static Task AdminLogAsync(string emeliyyat) => LocalDb.YazAsync(async (con, tr) =>
        {
            using var cmd = con.CreateCommand(); cmd.Transaction = tr;
            cmd.CommandText = "INSERT INTO AdminJurnal (Tarix, Kim, Emeliyyat) VALUES ($t, 'Admin', $e);";
            cmd.Parameters.AddWithValue("$t", Vaxt.Indi); cmd.Parameters.AddWithValue("$e", emeliyyat);
            return await cmd.ExecuteNonQueryAsync();
        });

        public static async Task<DataTable> AdminJurnalAsync()
        {
            await using var con = await LocalDb.AcAsync();
            using var cmd = con.CreateCommand(); cmd.CommandText = "SELECT Tarix, Kim, Emeliyyat FROM AdminJurnal ORDER BY Id DESC LIMIT 1000;";
            await using var r = await cmd.ExecuteReaderAsync(); var dt = new DataTable(); dt.Load(r); return dt;
        }

        // ---------- Bildirişlər ----------
        /// <summary>Acar eyni olan bildiriş təkrar yaradılmır (məs. "late:123:2026-10-09").</summary>
        public static Task BildirisAsync(string acar, string nov, string basliq, string metn, string zavod, long? texnikaId) => LocalDb.YazAsync(async (con, tr) =>
        {
            using var cmd = con.CreateCommand(); cmd.Transaction = tr;
            cmd.CommandText = "INSERT OR IGNORE INTO Bildirisler (Acar, Nov, Basliq, Metn, Zavod, TexnikaId, Tarix) VALUES ($a,$n,$b,$m,$z,$t,$v);";
            cmd.Parameters.AddWithValue("$a", (object)acar ?? DBNull.Value); cmd.Parameters.AddWithValue("$n", nov); cmd.Parameters.AddWithValue("$b", basliq);
            cmd.Parameters.AddWithValue("$m", metn); cmd.Parameters.AddWithValue("$z", (object)zavod ?? DBNull.Value);
            cmd.Parameters.AddWithValue("$t", (object)texnikaId ?? DBNull.Value); cmd.Parameters.AddWithValue("$v", Vaxt.Indi);
            return await cmd.ExecuteNonQueryAsync();
        });

        public static async Task<List<Bildiris>> BildirislerAsync(string nov = null, bool yalnizOxunmamis = false, int limit = 300)
        {
            await using var con = await LocalDb.AcAsync();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT Id, Nov, Basliq, Metn, Zavod, TexnikaId, Tarix, Oxunub FROM Bildirisler " +
                              "WHERE ($z IS NULL OR Zavod=$z) AND ($n IS NULL OR Nov=$n) AND ($u=0 OR Oxunub=0) ORDER BY Tarix DESC LIMIT $l;";
            cmd.Parameters.AddWithValue("$z", (object)Session.ZavodFiltri ?? DBNull.Value); cmd.Parameters.AddWithValue("$n", (object)nov ?? DBNull.Value);
            cmd.Parameters.AddWithValue("$u", yalnizOxunmamis ? 1 : 0); cmd.Parameters.AddWithValue("$l", limit);
            await using var r = await cmd.ExecuteReaderAsync();
            var l = new List<Bildiris>();
            while (await r.ReadAsync()) l.Add(new(r.GetInt64(0), r.GetString(1), r.GetString(2), r.GetString(3), r.IsDBNull(4) ? null : r.GetString(4), r.IsDBNull(5) ? null : r.GetInt64(5), r.GetInt64(6), r.GetInt32(7) == 1));
            return l;
        }

        public static async Task<int> OxunmamisSayAsync()
        {
            await using var con = await LocalDb.AcAsync();
            using var cmd = con.CreateCommand(); cmd.CommandText = "SELECT COUNT(*) FROM Bildirisler WHERE Oxunub=0 AND ($z IS NULL OR Zavod=$z);";
            cmd.Parameters.AddWithValue("$z", (object)Session.ZavodFiltri ?? DBNull.Value);
            return Convert.ToInt32(await cmd.ExecuteScalarAsync());
        }

        public static Task OxunduAsync(long? id) => LocalDb.YazAsync(async (con, tr) =>
        {
            using var cmd = con.CreateCommand(); cmd.Transaction = tr;
            cmd.CommandText = id is null ? "UPDATE Bildirisler SET Oxunub=1 WHERE $z IS NULL OR Zavod=$z;" : "UPDATE Bildirisler SET Oxunub=1 WHERE Id=$i;";
            cmd.Parameters.AddWithValue("$z", (object)Session.ZavodFiltri ?? DBNull.Value); cmd.Parameters.AddWithValue("$i", (object)id ?? DBNull.Value);
            return await cmd.ExecuteNonQueryAsync();
        });

        internal static SqliteParameter P(this SqliteCommand c, string n, object v) => c.Parameters.AddWithValue(n, v ?? DBNull.Value);
    }
}
