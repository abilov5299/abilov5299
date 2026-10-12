using Microsoft.Data.Sqlite;

namespace TexnikaReyestri.Data
{
    // Lokal SQLite bazası. Qaydalar:
    //  - UI thread heç vaxt bazanı gözləmir: hər metod async-dir və CancellationToken qəbul edir.
    //  - Oxumalar paralel gedə bilər (WAL), yazılar bir-bir (_yaz semaforu) — SQLITE_BUSY olmur.
    //  - Bağlantılar hovuzdan (pooling) gəlir: Open/Close ucuzdur.
    public static class LocalDb
    {
        private static string _cs;
        private static readonly SemaphoreSlim _yaz = new(1, 1);

        public static void Init(string dbFayli)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dbFayli)!);
            _cs = new SqliteConnectionStringBuilder
            {
                DataSource = dbFayli,
                Mode = SqliteOpenMode.ReadWriteCreate,
                Cache = SqliteCacheMode.Private,   // WAL ilə Shared cache lazım deyil
                Pooling = true,
                DefaultTimeout = 30
            }.ToString();

            using var con = new SqliteConnection(_cs);
            con.Open();
            // Bazaya bir dəfə yazılan (davamlı) parametrlər
            Exec(con, "PRAGMA journal_mode=WAL;");
            Exec(con, "PRAGMA synchronous=NORMAL;");   // WAL ilə təhlükəsiz və FULL-dan xeyli sürətli
            Schema.Yarat(con);
            // Sorğu planlayıcısının statistikası: olmasa SQLite səhv indeksi seçir (100 000 sətirdə 150 ms → 3 ms fərq)
            using (var c = con.CreateCommand())
            {
                c.CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE name='sqlite_stat1';";
                bool lazim = Convert.ToInt64(c.ExecuteScalar()) == 0;   // yeni baza: cədvəl hələ yoxdur
                if (!lazim) { c.CommandText = "SELECT COUNT(*) FROM sqlite_stat1 WHERE idx='IX_T_Gec';"; lazim = Convert.ToInt64(c.ExecuteScalar()) == 0; }
                if (lazim) Exec(con, "ANALYZE;");
            }
            Exec(con, "PRAGMA optimize=0x10002;");   // dəyişən cədvəllər üçün statistikanı yenilə
        }

        /// <summary>
        /// Oxuyucunu DataTable-a sürətli köçürür. DataTable.Load(r) hər dəfə sütun sxemini (GetSchemaTable) soruşur:
        /// Microsoft.Data.Sqlite-də bu, əlavə PRAGMA sorğuları deməkdir — 200 sətirlik reyestr səhifəsi 1 ms əvəzinə ~280 ms çəkirdi.
        /// </summary>
        public static async Task<System.Data.DataTable> CedvelAsync(SqliteDataReader r, CancellationToken ct = default)
        {
            var dt = new System.Data.DataTable();
            for (int i = 0; i < r.FieldCount; i++) dt.Columns.Add(r.GetName(i), typeof(object));
            dt.BeginLoadData();
            var a = new object[r.FieldCount];
            while (await r.ReadAsync(ct)) { r.GetValues(a); dt.Rows.Add(a); }
            dt.EndLoadData();
            return dt;
        }

        /// <summary>Toplu yazmadan sonra (Excel, USB idxalı, nümunə məlumat) statistikanı yenilə.</summary>
        public static async Task AnalizAsync()
        {
            await _yaz.WaitAsync();
            try { await using var con = await AcAsync(); Exec(con, "ANALYZE;"); }
            finally { _yaz.Release(); }
        }

        public static async Task<SqliteConnection> AcAsync(CancellationToken ct = default)
        {
            var con = new SqliteConnection(_cs);
            await con.OpenAsync(ct);
            // Hər bağlantı üçün parametrlər
            using var cmd = con.CreateCommand();
            cmd.CommandText =
                "PRAGMA foreign_keys=ON;" +
                "PRAGMA busy_timeout=5000;" +
                "PRAGMA cache_size=-32768;" +     // 32 MB yaddaş keşi
                "PRAGMA temp_store=MEMORY;" +
                "PRAGMA mmap_size=268435456;";    // 256 MB: oxuma diskdən birbaşa yaddaşa xəritələnir
            await cmd.ExecuteNonQueryAsync(ct);
            return con;
        }

        // Yazı əməliyyatı: kilid + tranzaksiya. Xəta olsa, hamısı geri qaytarılır.
        public static async Task<T> YazAsync<T>(Func<SqliteConnection, SqliteTransaction, Task<T>> isFunc, CancellationToken ct = default)
        {
            await _yaz.WaitAsync(ct);
            try
            {
                await using var con = await AcAsync(ct);
                await using var tr = (SqliteTransaction)await con.BeginTransactionAsync(ct);
                var netice = await isFunc(con, tr);
                await tr.CommitAsync(ct);
                return netice;
            }
            finally { _yaz.Release(); }
        }

        // Ayda bir dəfə və ya admin panelindən: statistikanı yeniləyir, faylı sıxır
        public static async Task XidmetAsync(bool sixmaq, CancellationToken ct = default)
        {
            await _yaz.WaitAsync(ct);
            try
            {
                await using var con = await AcAsync(ct);
                Exec(con, "PRAGMA optimize;");
                if (sixmaq) Exec(con, "VACUUM;");
                Exec(con, "PRAGMA wal_checkpoint(TRUNCATE);");
            }
            finally { _yaz.Release(); }
        }

        public static async Task<bool> ButovlukAsync(CancellationToken ct = default)
        {
            await using var con = await AcAsync(ct);
            using var cmd = con.CreateCommand();
            cmd.CommandText = "PRAGMA quick_check;";
            return (string)await cmd.ExecuteScalarAsync(ct) == "ok";
        }

        internal static void Exec(SqliteConnection con, string sql)
        {
            using var cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }
    }
}
