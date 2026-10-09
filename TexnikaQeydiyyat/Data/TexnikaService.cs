using System.Data;
using Microsoft.Data.SqlClient;

namespace TexnikaQeydiyyat.Data
{
    public static class TexnikaService
    {
        // Müvəqqəti şablon: yeni texnika formasında bu parametrlər hazır gəlir.
        public static readonly string[] ParametrSablonu =
        {
            "Buraxılış ili", "Mühərrik gücü (kVt)", "Yanacaq növü", "Motor saatı", "Vəziyyət"
        };

        public static Task<DataTable> ZavodlarAsync() =>
            Db.QueryAsync("SELECT Id, Ad FROM Qurumlar WHERE Tip = 1 ORDER BY Id");

        // Parametrlər bir sütunda "Ad: Dəyər; ..." şəklində birləşdirilir.
        public static Task<DataTable> SiyahiAsync(int? zavodId) => Db.QueryAsync(@"
            SELECT t.Id, q.Ad Zavod, t.Ad, t.Model, t.SeriyaNo, t.QebulTarixi,
                   STUFF((SELECT '; ' + p.Ad + ': ' + ISNULL(p.Deyer, '')
                          FROM TexnikaParametrleri p WHERE p.TexnikaId = t.Id
                          FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') Parametrler,
                   t.Qeyd
            FROM Texnikalar t JOIN Qurumlar q ON q.Id = t.ZavodId
            WHERE t.Silinib = 0 AND (@z IS NULL OR t.ZavodId = @z)
            ORDER BY t.Id DESC", Db.P("@z", zavodId));

        public static async Task ElaveEtAsync(string ad, string model, string seriya, DateTime qebul, string qeyd,
                                              IEnumerable<(string ad, string deyer)> parametrler)
        {
            await using var con = await Db.OpenAsync();
            await using var tr = (SqlTransaction)await con.BeginTransactionAsync();

            var cmd = new SqlCommand(@"INSERT INTO Texnikalar (ZavodId, Ad, Model, SeriyaNo, QebulTarixi, Qeyd, YaradanId)
                                       OUTPUT INSERTED.Id VALUES (@z, @a, @m, @s, @t, @q, @y)", con, tr);
            cmd.Parameters.AddRange(new[] {
                Db.P("@z", Session.QurumId), Db.P("@a", ad), Db.P("@m", model), Db.P("@s", seriya),
                Db.P("@t", qebul.Date), Db.P("@q", qeyd), Db.P("@y", Session.IstifadeciId) });
            int id = (int)await cmd.ExecuteScalarAsync();

            foreach (var (pAd, pDeyer) in parametrler)
            {
                var pc = new SqlCommand("INSERT INTO TexnikaParametrleri (TexnikaId, Ad, Deyer) VALUES (@t, @a, @d)", con, tr);
                pc.Parameters.AddRange(new[] { Db.P("@t", id), Db.P("@a", pAd), Db.P("@d", pDeyer) });
                await pc.ExecuteNonQueryAsync();
            }
            await tr.CommitAsync();
            await LogService.YazAsync("TexnikaElave", $"Texnika əlavə edildi: #{id} {ad} ({seriya})");
        }

        public static async Task SilAsync(int id, string ad)
        {
            await Db.ExecAsync("UPDATE Texnikalar SET Silinib = 1 WHERE Id = @i AND ZavodId = @z",
                Db.P("@i", id), Db.P("@z", Session.QurumId));
            await LogService.YazAsync("TexnikaSil", $"Texnika silindi: #{id} {ad}");
        }
    }
}
