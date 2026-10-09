using System.Security.Cryptography;
using System.Text;

namespace TexnikaQeydiyyat.Data
{
    public static class AuthService
    {
        private const int Iterasiya = 100_000;

        public static (byte[] hash, byte[] salt) HashParol(string parol)
        {
            var salt = RandomNumberGenerator.GetBytes(16);
            return (Hash(parol, salt), salt);
        }

        private static byte[] Hash(string parol, byte[] salt) =>
            Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(parol), salt, Iterasiya, HashAlgorithmName.SHA256, 32);

        public static async Task<bool> LoginAsync(string login, string parol)
        {
            var dt = await Db.QueryAsync(@"
                SELECT i.Id, i.AdSoyad, i.ParolHash, i.ParolDuz, q.Id QurumId, q.Ad QurumAd, q.Tip
                FROM Istifadeciler i JOIN Qurumlar q ON q.Id = i.QurumId
                WHERE i.Login = @l AND i.Aktiv = 1", Db.P("@l", login));

            if (dt.Rows.Count == 0)
            {
                await LogService.YazAsync("GirisXeta", $"Uğursuz giriş: {login}");
                return false;
            }
            var r = dt.Rows[0];
            var hash = Hash(parol, (byte[])r["ParolDuz"]);
            if (!CryptographicOperations.FixedTimeEquals(hash, (byte[])r["ParolHash"]))
            {
                await LogService.YazAsync("GirisXeta", $"Yanlış parol: {login}");
                return false;
            }

            Session.IstifadeciId = (int)r["Id"];
            Session.AdSoyad = (string)r["AdSoyad"];
            Session.QurumId = (int)r["QurumId"];
            Session.QurumAd = (string)r["QurumAd"];
            Session.Tip = (QurumTip)(byte)r["Tip"];
            await LogService.YazAsync("Giris", $"{Session.AdSoyad} daxil oldu");
            return true;
        }

        // Cədvəl boşdursa hər qurum üçün ilkin istifadəçi yaradır. Parolları dərhal dəyişin.
        public static async Task SeedAsync()
        {
            if ((int)await Db.ScalarAsync("SELECT COUNT(*) FROM Istifadeciler") > 0) return;

            var qurumlar = await Db.QueryAsync("SELECT Id, Tip FROM Qurumlar ORDER BY Id");
            int zavodNo = 0;
            foreach (System.Data.DataRow q in qurumlar.Rows)
            {
                var tip = (QurumTip)(byte)q["Tip"];
                string login = tip switch
                {
                    QurumTip.EnUstQurum => "enustqurum",
                    QurumTip.UstQurum => "ustqurum",
                    _ => $"zavod{++zavodNo}"
                };
                var (hash, salt) = HashParol("Parol123!");
                await Db.ExecAsync(@"INSERT INTO Istifadeciler (Login, ParolHash, ParolDuz, AdSoyad, QurumId)
                                     VALUES (@l, @h, @s, @a, @q)",
                    Db.P("@l", login), Db.P("@h", hash), Db.P("@s", salt), Db.P("@a", login), Db.P("@q", q["Id"]));
            }
        }
    }
}
