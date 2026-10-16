using System.Data;
using System.Globalization;
using Microsoft.Data.Sqlite;

namespace TexnikaReyestri.Data
{
    public sealed record TarixceQeyd(long Tarix, string Istifadeci, string Sahe, string Kohne, string Yeni);
    public sealed record EvStat(long Cemi, long Temirde, long HisseGozleyir, long Geciken, double OrtaGun, double OrtaGunEvvel,
        (string, long)[] Statuslar, string[] Aylar, double[] Qebul, double[] Tehvil, (string, double)[] Novler, (string, double)[] OrtaUzre,
        (string ad, int gun, string zavod, long id)[] Gecikenler, double[] Verilib = null);

    public static partial class TexnikaRepo
    {
        /// <summary>Yeni texnika (id = null) və ya redaktə. Dəyişən hər sahə tarixçəyə yazılır.</summary>
        public static Task<long> SaxlaAsync(long? id, Dictionary<string, object> deyerler) => LocalDb.YazAsync(async (con, tr) =>
        {
            var kodlar = Saheler.Hamisi.Select(s => s.Kod).ToArray();
            long indi = Vaxt.Indi;
            if (id is null)
            {
                using var ins = con.CreateCommand(); ins.Transaction = tr;
                ins.CommandText = $"INSERT INTO Texnikalar (Guid, Deyisdi, {string.Join(",", kodlar)}) VALUES ($g, $d, {string.Join(",", kodlar.Select(k => "$" + k))}); SELECT last_insert_rowid();";
                ins.Parameters.AddWithValue("$g", Guid.NewGuid().ToString("N")); ins.Parameters.AddWithValue("$d", indi);
                foreach (var k in kodlar) ins.Parameters.AddWithValue("$" + k, Bos(deyerler.GetValueOrDefault(k)));
                long yeni = (long)await ins.ExecuteScalarAsync();
                await Tarixce(con, tr, yeni, "Qeyd yaradıldı", null, "Texnika reyestrə daxil edildi");
                return yeni;
            }
            var kohne = new Dictionary<string, object>();
            using (var sel = con.CreateCommand())
            {
                sel.Transaction = tr; sel.CommandText = "SELECT * FROM Texnikalar WHERE Id=$i;"; sel.Parameters.AddWithValue("$i", id);
                using var r = await sel.ExecuteReaderAsync();
                if (await r.ReadAsync()) for (int i = 0; i < r.FieldCount; i++) kohne[r.GetName(i)] = r.IsDBNull(i) ? null : r.GetValue(i);
            }
            var deyisen = Saheler.Hamisi.Where(s => deyerler.ContainsKey(s.Kod) && Str(kohne.GetValueOrDefault(s.Kod)) != Str(deyerler[s.Kod])).ToList();
            if (deyisen.Count == 0) return id.Value;
            using (var upd = con.CreateCommand())
            {
                upd.Transaction = tr;
                upd.CommandText = $"UPDATE Texnikalar SET Versiya=Versiya+1, Deyisdi=$d, {string.Join(",", deyisen.Select(s => $"{s.Kod}=${s.Kod}"))} WHERE Id=$i;";
                upd.Parameters.AddWithValue("$d", indi); upd.Parameters.AddWithValue("$i", id);
                foreach (var s in deyisen) upd.Parameters.AddWithValue("$" + s.Kod, Bos(deyerler[s.Kod]));
                await upd.ExecuteNonQueryAsync();
            }
            foreach (var s in deyisen) await Tarixce(con, tr, id.Value, s.Ad, Str(kohne.GetValueOrDefault(s.Kod)), Str(deyerler[s.Kod]));
            return id.Value;
        });

        public static Task SilAsync(long id) => LocalDb.YazAsync(async (con, tr) =>
        {
            using var cmd = con.CreateCommand(); cmd.Transaction = tr;
            cmd.CommandText = "UPDATE Texnikalar SET Silinib=1, Versiya=Versiya+1, Deyisdi=$d WHERE Id=$i;";
            cmd.Parameters.AddWithValue("$d", Vaxt.Indi); cmd.Parameters.AddWithValue("$i", id);
            await cmd.ExecuteNonQueryAsync();
            await Tarixce(con, tr, id, "Silindi", null, "Qeyd silindi");
            return 0;
        });

        public static async Task<List<TarixceQeyd>> TarixceAsync(long id)
        {
            await using var con = await LocalDb.AcAsync();
            using var cmd = con.CreateCommand(); cmd.CommandText = "SELECT Tarix, Istifadeci, Sahe, Kohne, Yeni FROM Tarixce WHERE TexnikaId=$i ORDER BY Tarix DESC, Id DESC LIMIT 200;";
            cmd.Parameters.AddWithValue("$i", id);
            await using var r = await cmd.ExecuteReaderAsync();
            var l = new List<TarixceQeyd>();
            while (await r.ReadAsync()) l.Add(new(r.GetInt64(0), r.GetString(1), r.GetString(2), r.IsDBNull(3) ? null : r.GetString(3), r.IsDBNull(4) ? null : r.GetString(4)));
            return l;
        }

        /// <summary>Zavod nömrəsi bazada varsa, onun Guid-i (Excel idxalında dublikat yoxlaması üçün).</summary>
        public static async Task<Dictionary<string, string>> ZavodNoGuidAsync()
        {
            await using var con = await LocalDb.AcAsync();
            using var cmd = con.CreateCommand(); cmd.CommandText = "SELECT ZavodNo, Guid FROM Texnikalar WHERE ZavodNo IS NOT NULL;";
            await using var r = await cmd.ExecuteReaderAsync();
            var d = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            while (await r.ReadAsync()) d[r.GetString(0)] = r.GetString(1);
            return d;
        }

        /// <summary>Ana səhifənin bütün statistikası bir bağlantıda. ayGeri: 1, 3, 6, 12.</summary>
        public static async Task<EvStat> EvStatAsync(string zavod, int ayGeri, CancellationToken ct = default)
        {
            await using var con = await LocalDb.AcAsync(ct);
            string zw = zavod == null ? "" : " AND TemirZavodu=$z";
            async Task<List<object[]>> Q(string sql)
            {
                using var c = con.CreateCommand(); c.CommandText = sql; c.Parameters.AddWithValue("$z", (object)zavod ?? DBNull.Value);
                c.Parameters.AddWithValue("$bugun", Vaxt.Iso(DateTime.Today));
                var start = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-(ayGeri - 1));
                c.Parameters.AddWithValue("$bas", Vaxt.Iso(start)); c.Parameters.AddWithValue("$evvel", Vaxt.Iso(start.AddMonths(-ayGeri)));
                using var r = await c.ExecuteReaderAsync(ct); var l = new List<object[]>();
                while (await r.ReadAsync(ct)) { var a = new object[r.FieldCount]; r.GetValues(a); l.Add(a); }
                return l;
            }
            var st = (await Q("SELECT Status, SUM(Say) FROM Icmal WHERE ($z IS NULL OR Zavod=$z) AND Status<>'' GROUP BY Status;"))
                     .ToDictionary(x => (string)x[0], x => Convert.ToInt64(x[1]));
            string[] sira = { "Qəbul edilib", "Təmirdə", "Hissə gözləyir", "Qismən təmin edilib", "Təhvil verilib", "Təmirə yararsız" };
            long G(string s) => st.GetValueOrDefault(s);
            long gec = Convert.ToInt64((await Q($"SELECT COUNT(*) FROM Texnikalar WHERE Silinib=0{zw} AND GozlenilenBitme<$bugun AND Status IN ('Qəbul edilib','Təmirdə','Hissə gözləyir','Qismən təmin edilib') AND IFNULL(FaktikiBitme,'')='';"))[0][0]);
            var ayQ = (await Q($"SELECT substr(DaxilolmaTarixi,1,7), COUNT(*) FROM Texnikalar WHERE Silinib=0{zw} AND DaxilolmaTarixi>=$bas GROUP BY 1;")).ToDictionary(x => (string)x[0], x => Convert.ToDouble(x[1]));
            var ayT = (await Q($"SELECT substr(FaktikiBitme,1,7), COUNT(*) FROM Texnikalar WHERE Silinib=0{zw} AND FaktikiBitme>=$bas GROUP BY 1;")).ToDictionary(x => (string)x[0], x => Convert.ToDouble(x[1]));
            // Təhvil verilib: statusun "Təhvil verilib"ə dəyişdiyi ay (tarixçədən); tarixçəsi olmayanlar (Excel, ilk qeyd) — faktiki bitmə ayı
            string tw = zavod == null ? "" : " AND t.TemirZavodu=$z";
            var ayV = (await Q($@"SELECT ay, COUNT(*) FROM (
  SELECT t.Id, strftime('%Y-%m', MAX(h.Tarix)/1000, 'unixepoch', 'localtime') AS ay FROM Tarixce h JOIN Texnikalar t ON t.Id=h.TexnikaId
   WHERE t.Silinib=0{tw} AND t.Status='Təhvil verilib' AND h.Sahe='Statusu' AND h.Yeni='Təhvil verilib' GROUP BY t.Id
  UNION ALL
  SELECT t.Id, substr(t.FaktikiBitme,1,7) FROM Texnikalar t WHERE t.Silinib=0{tw} AND t.Status='Təhvil verilib' AND t.FaktikiBitme>=$bas
   AND NOT EXISTS (SELECT 1 FROM Tarixce h WHERE h.TexnikaId=t.Id AND h.Sahe='Statusu' AND h.Yeni='Təhvil verilib'))
 WHERE ay>=substr($bas,1,7) GROUP BY ay;")).Where(x => x[0] is string).ToDictionary(x => (string)x[0], x => Convert.ToDouble(x[1]));
            var aylar = Enumerable.Range(0, ayGeri).Select(i => new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-(ayGeri - 1) + i)).ToArray();
            string[] adlar = { "Yanvar", "Fevral", "Mart", "Aprel", "May", "İyun", "İyul", "Avqust", "Sentyabr", "Oktyabr", "Noyabr", "Dekabr" };
            var nov = (await Q($"SELECT Nov, COUNT(*) FROM Texnikalar WHERE Silinib=0{zw} AND Nov IS NOT NULL GROUP BY Nov ORDER BY 2 DESC LIMIT 6;")).Select(x => ((string)x[0], Convert.ToDouble(x[1]))).ToArray();
            string orta = "AVG(julianday(FaktikiBitme)-julianday(BaslamaTarixi))";
            double Avg(List<object[]> l) => l.Count == 0 || l[0][0] is DBNull ? 0 : Math.Round(Convert.ToDouble(l[0][0]));
            double o1 = Avg(await Q($"SELECT {orta} FROM Texnikalar WHERE Silinib=0{zw} AND FaktikiBitme>=$bas AND BaslamaTarixi IS NOT NULL;"));
            double o0 = Avg(await Q($"SELECT {orta} FROM Texnikalar WHERE Silinib=0{zw} AND FaktikiBitme>=$evvel AND FaktikiBitme<$bas AND BaslamaTarixi IS NOT NULL;"));
            var uzre = zavod != null
                ? (await Q($"SELECT TemirNovu, ROUND({orta}) FROM Texnikalar WHERE Silinib=0{zw} AND TemirNovu IS NOT NULL AND FaktikiBitme IS NOT NULL AND BaslamaTarixi IS NOT NULL GROUP BY TemirNovu ORDER BY 2;"))
                : (await Q($"SELECT TemirZavodu, ROUND({orta}) FROM Texnikalar WHERE Silinib=0 AND FaktikiBitme > '' AND BaslamaTarixi IS NOT NULL GROUP BY TemirZavodu ORDER BY 2 DESC;"));
            var gecList = (await Q($"SELECT Marka||' · '||IFNULL(ZavodNo,''), CAST(julianday($bugun)-julianday(GozlenilenBitme) AS INTEGER), TemirZavodu, Id FROM Texnikalar " +
                                   $"WHERE Silinib=0{zw} AND GozlenilenBitme<$bugun AND Status IN ('Qəbul edilib','Təmirdə','Hissə gözləyir','Qismən təmin edilib') AND IFNULL(FaktikiBitme,'')='' ORDER BY GozlenilenBitme LIMIT 5;"))
                          .Select(x => ((string)x[0], Convert.ToInt32(x[1]), x[2] as string, Convert.ToInt64(x[3]))).ToArray();
            return new EvStat(st.Values.Sum(), G("Təmirdə"), G("Hissə gözləyir"), gec, o1, o0,
                sira.Select(s => (s, G(s))).ToArray(), aylar.Select(d => adlar[d.Month - 1]).ToArray(),
                aylar.Select(d => ayQ.GetValueOrDefault(d.ToString("yyyy-MM"))).ToArray(), aylar.Select(d => ayT.GetValueOrDefault(d.ToString("yyyy-MM"))).ToArray(),
                nov, uzre.Where(x => x[0] is string).Select(x => ((string)x[0], x[1] is DBNull ? 0 : Convert.ToDouble(x[1]))).ToArray(), gecList,
                aylar.Select(d => ayV.GetValueOrDefault(d.ToString("yyyy-MM"))).ToArray());
        }

        /// <summary>Təmiri tamamlanıb, təhvil gözlənilən texnikalar: say və ən uzun gözləmə (gün, faktiki bitmədən bu günə).</summary>
        public static async Task<(long say, int maxGun)> TehvilGozlAsync(string zavod)
        {
            await using var con = await LocalDb.AcAsync();
            using var c = con.CreateCommand();
            c.CommandText = "SELECT COUNT(*), IFNULL(MAX(CAST(julianday($b)-julianday(FaktikiBitme) AS INTEGER)),0) FROM Texnikalar " +
                            "WHERE Silinib=0 AND ($z IS NULL OR TemirZavodu=$z) AND IFNULL(FaktikiBitme,'')<>'' AND IFNULL(Status,'') NOT IN ('Təhvil verilib','Təmirə yararsız');";
            c.Parameters.AddWithValue("$z", (object)zavod ?? DBNull.Value); c.Parameters.AddWithValue("$b", Vaxt.Iso(DateTime.Today));
            await using var r = await c.ExecuteReaderAsync(); await r.ReadAsync();
            return (r.GetInt64(0), Math.Max(0, r.GetInt32(1)));
        }

        /// <summary>Üst qurum üçün zavodlar üzrə icmal (son paket tarixi daxil).</summary>
        public static async Task<DataTable> ZavodIcmalAsync()
        {
            await using var con = await LocalDb.AcAsync();
            using var cmd = con.CreateCommand();
            cmd.CommandText = @"
SELECT q.Ad AS Zavod,
  IFNULL((SELECT SUM(Say) FROM Icmal i WHERE i.Zavod=q.Ad),0) AS Cemi,
  IFNULL((SELECT Say FROM Icmal i WHERE i.Zavod=q.Ad AND i.Status='Təmirdə'),0) AS Temirde,
  IFNULL((SELECT Say FROM Icmal i WHERE i.Zavod=q.Ad AND i.Status='Hissə gözləyir'),0) AS HisseGozleyir,
  (SELECT COUNT(*) FROM Texnikalar t WHERE t.Silinib=0 AND t.TemirZavodu=q.Ad AND t.GozlenilenBitme<$b AND t.Status IN ('Qəbul edilib','Təmirdə','Hissə gözləyir','Qismən təmin edilib')) AS Geciken,
  (SELECT ROUND(AVG(julianday(FaktikiBitme)-julianday(BaslamaTarixi))) FROM Texnikalar t WHERE t.Silinib=0 AND t.TemirZavodu=q.Ad AND FaktikiBitme IS NOT NULL AND BaslamaTarixi IS NOT NULL) AS OrtaGun,
  (SELECT MAX(Tarix) FROM SyncJurnal s WHERE s.Qurum=q.Kod AND s.Emeliyyat='İdxal' AND s.Netice='Uğurlu') AS SonPaket
FROM Qurumlar q WHERE q.Tip=1 ORDER BY q.Kod;";
            cmd.Parameters.AddWithValue("$b", Vaxt.Iso(DateTime.Today));
            // Hesablanan sütunların tipi SQLite-da elan olunmur, ona görə cədvəl əl ilə qurulur
            var dt = new DataTable();
            dt.Columns.Add("Zavod", typeof(string)); foreach (var c in new[] { "Cemi", "Temirde", "HisseGozleyir", "Geciken" }) dt.Columns.Add(c, typeof(long));
            dt.Columns.Add("OrtaGun", typeof(double)); dt.Columns.Add("SonPaket", typeof(long));
            await using var r = await cmd.ExecuteReaderAsync();
            while (await r.ReadAsync())
                dt.Rows.Add(r.GetString(0), r.GetInt64(1), r.GetInt64(2), r.GetInt64(3), r.GetInt64(4), r.IsDBNull(5) ? DBNull.Value : r.GetDouble(5), r.IsDBNull(6) ? DBNull.Value : r.GetInt64(6));
            return dt;
        }

        private static async Task Tarixce(SqliteConnection con, SqliteTransaction tr, long id, string sahe, string kohne, string yeni)
        {
            using var c = con.CreateCommand(); c.Transaction = tr;
            c.CommandText = "INSERT INTO Tarixce (TexnikaId, Tarix, Istifadeci, Sahe, Kohne, Yeni) VALUES ($i,$t,$u,$s,$k,$y);";
            c.Parameters.AddWithValue("$i", id); c.Parameters.AddWithValue("$t", Vaxt.Indi); c.Parameters.AddWithValue("$u", Session.AdSoyad ?? "Sistem");
            c.Parameters.AddWithValue("$s", sahe); c.Parameters.AddWithValue("$k", (object)kohne ?? DBNull.Value); c.Parameters.AddWithValue("$y", (object)yeni ?? DBNull.Value);
            await c.ExecuteNonQueryAsync();
        }

        private static object Bos(object v) => v is null || v is string s && s.Trim().Length == 0 ? DBNull.Value : v is string t ? t.Trim() : v;
        private static string Str(object v) => v switch { null or DBNull => "", double d => d.ToString(CultureInfo.InvariantCulture), _ => v.ToString().Trim() };
    }
}
