using System.Data;
using System.Text.Json;
using Microsoft.Data.SqlClient;

namespace TexnikaQeydiyyat.Data
{
    // Bütün sorğular async-dir: UI thread bloklanmır, forma donmur.
    public static class Db
    {
        private static readonly string _cs = LoadConnectionString();

        private static string LoadConnectionString()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            using var doc = JsonDocument.Parse(File.ReadAllText(path));
            return doc.RootElement.GetProperty("ConnectionString").GetString();
        }

        public static async Task<SqlConnection> OpenAsync()
        {
            var con = new SqlConnection(_cs);
            await con.OpenAsync();
            return con;
        }

        public static async Task<DataTable> QueryAsync(string sql, params SqlParameter[] p)
        {
            await using var con = await OpenAsync();
            await using var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddRange(p);
            await using var rdr = await cmd.ExecuteReaderAsync();
            var dt = new DataTable();
            dt.Load(rdr);
            return dt;
        }

        public static async Task<object> ScalarAsync(string sql, params SqlParameter[] p)
        {
            await using var con = await OpenAsync();
            await using var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddRange(p);
            return await cmd.ExecuteScalarAsync();
        }

        public static async Task<int> ExecAsync(string sql, params SqlParameter[] p)
        {
            await using var con = await OpenAsync();
            await using var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddRange(p);
            return await cmd.ExecuteNonQueryAsync();
        }

        public static SqlParameter P(string name, object value) => new SqlParameter(name, value ?? DBNull.Value);
    }
}
