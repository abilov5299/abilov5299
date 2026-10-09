using System.Data;

namespace TexnikaQeydiyyat.Data
{
    public static class LogService
    {
        // Log yazılmasa belə proqram dayanmamalıdır.
        public static async Task YazAsync(string nov, string mesaj)
        {
            try
            {
                await Db.ExecAsync("INSERT INTO Loglar (IstifadeciId, ZavodId, Nov, Mesaj) VALUES (@i, @z, @n, @m)",
                    Db.P("@i", Session.IstifadeciId == 0 ? null : Session.IstifadeciId),
                    Db.P("@z", Session.ZavodId), Db.P("@n", nov), Db.P("@m", mesaj));
            }
            catch { }
        }

        // Zavod yalnız öz loglarını, üst qurumlar hamısını görür. Son 500 qeyd.
        public static Task<DataTable> SiyahiAsync(int? zavodId) => Db.QueryAsync(@"
            SELECT TOP 500 l.Tarix, ISNULL(i.AdSoyad, '-') Istifadeci, ISNULL(q.Ad, '-') Zavod, l.Nov, l.Mesaj
            FROM Loglar l
            LEFT JOIN Istifadeciler i ON i.Id = l.IstifadeciId
            LEFT JOIN Qurumlar q ON q.Id = l.ZavodId
            WHERE @z IS NULL OR l.ZavodId = @z
            ORDER BY l.Id DESC", Db.P("@z", zavodId));
    }
}
