using System.Security.Cryptography;
using System.Text;
using TexnikaReyestri.Data;

namespace TexnikaReyestri.Services
{
    public enum GirisNetice { Ugurlu, Yanlis, Bloklanib, MuveqqetiParol }

    public static class AuthService
    {
        private const int Iter = 210_000;   // PBKDF2-SHA256 (OWASP tövsiyəsi)

        public static (byte[] hash, byte[] salt) Hash(string parol)
        {
            var salt = RandomNumberGenerator.GetBytes(16);
            return (Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(parol), salt, Iter, HashAlgorithmName.SHA256, 32), salt);
        }

        private static bool Yoxla(string parol, byte[] hash, byte[] salt) =>
            CryptographicOperations.FixedTimeEquals(Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(parol), salt, Iter, HashAlgorithmName.SHA256, 32), hash);

        public static async Task<(GirisNetice netice, int qalan)> GirisAsync(string login, string parol)
        {
            int limit = await SistemRepo.ParamAsync("Blok", 5);
            return await Task.Run(() => LocalDb.YazAsync(async (con, tr) =>
            {
                using var cmd = con.CreateCommand(); cmd.Transaction = tr;
                cmd.CommandText = "SELECT i.AdSoyad, i.Rol, i.Hash, i.Duz, i.Aktiv, i.YanlisCehd, i.Muveqqeti, q.Kod, q.Ad, q.Tip FROM Istifadeciler i JOIN Qurumlar q ON q.Kod=i.QurumKod WHERE i.Login=$l;";
                cmd.Parameters.AddWithValue("$l", login.Trim());
                using var r = await cmd.ExecuteReaderAsync();
                if (!await r.ReadAsync()) return (GirisNetice.Yanlis, limit);
                string ad = r.GetString(0), rol = r.GetString(1); var h = (byte[])r[2]; var s = (byte[])r[3];
                bool aktiv = r.GetInt32(4) == 1; int cehd = r.GetInt32(5); bool tmp = r.GetInt32(6) == 1;
                string qk = r.GetString(7), qa = r.GetString(8); var tip = (QurumTip)r.GetInt32(9);
                await r.CloseAsync();
                if (!aktiv) return (GirisNetice.Bloklanib, 0);
                using var u = con.CreateCommand(); u.Transaction = tr; u.Parameters.AddWithValue("$l", login.Trim());
                if (!Yoxla(parol, h, s))
                {
                    cehd++;
                    u.CommandText = cehd >= limit ? "UPDATE Istifadeciler SET YanlisCehd=$c, Aktiv=0 WHERE Login=$l;" : "UPDATE Istifadeciler SET YanlisCehd=$c WHERE Login=$l;";
                    u.Parameters.AddWithValue("$c", cehd); await u.ExecuteNonQueryAsync();
                    return cehd >= limit ? (GirisNetice.Bloklanib, 0) : (GirisNetice.Yanlis, limit - cehd);
                }
                u.CommandText = "UPDATE Istifadeciler SET YanlisCehd=0, SonGiris=$t WHERE Login=$l;"; u.Parameters.AddWithValue("$t", Vaxt.Indi);
                await u.ExecuteNonQueryAsync();
                Session.Login = login.Trim(); Session.AdSoyad = ad; Session.Rol = rol; Session.QurumKod = qk; Session.QurumAd = qa; Session.Tip = tip;
                return (tmp ? GirisNetice.MuveqqetiParol : GirisNetice.Ugurlu, 0);
            }));
        }

        public static Task YaratAsync(string login, string ad, string qurumKod, string rol, string parol, bool muveqqeti) => Task.Run(() =>
        {
            var (h, s) = Hash(parol);
            return LocalDb.YazAsync(async (con, tr) =>
            {
                using var cmd = con.CreateCommand(); cmd.Transaction = tr;
                cmd.CommandText = "INSERT INTO Istifadeciler (Login, AdSoyad, QurumKod, Rol, Hash, Duz, Muveqqeti, ParolTarixi) VALUES ($l,$a,$q,$r,$h,$s,$m,$t);";
                cmd.Parameters.AddWithValue("$l", login.Trim()); cmd.Parameters.AddWithValue("$a", ad.Trim()); cmd.Parameters.AddWithValue("$q", qurumKod);
                cmd.Parameters.AddWithValue("$r", rol); cmd.Parameters.AddWithValue("$h", h); cmd.Parameters.AddWithValue("$s", s);
                cmd.Parameters.AddWithValue("$m", muveqqeti ? 1 : 0); cmd.Parameters.AddWithValue("$t", Vaxt.Indi);
                return await cmd.ExecuteNonQueryAsync();
            });
        });

        public static Task ParolDeyisAsync(string login, string parol, bool muveqqeti) => Task.Run(() =>
        {
            var (h, s) = Hash(parol);
            return LocalDb.YazAsync(async (con, tr) =>
            {
                using var cmd = con.CreateCommand(); cmd.Transaction = tr;
                cmd.CommandText = "UPDATE Istifadeciler SET Hash=$h, Duz=$s, Muveqqeti=$m, YanlisCehd=0, ParolTarixi=$t WHERE Login=$l;";
                cmd.Parameters.AddWithValue("$l", login); cmd.Parameters.AddWithValue("$h", h); cmd.Parameters.AddWithValue("$s", s);
                cmd.Parameters.AddWithValue("$m", muveqqeti ? 1 : 0); cmd.Parameters.AddWithValue("$t", Vaxt.Indi);
                return await cmd.ExecuteNonQueryAsync();
            });
        });

        public static Task YenileAsync(string login, string ad, string qurumKod, string rol, bool? aktiv = null) => LocalDb.YazAsync(async (con, tr) =>
        {
            using var cmd = con.CreateCommand(); cmd.Transaction = tr;
            cmd.CommandText = "UPDATE Istifadeciler SET AdSoyad=$a, QurumKod=$q, Rol=$r, Aktiv=IFNULL($k, Aktiv), YanlisCehd=CASE WHEN $k=1 THEN 0 ELSE YanlisCehd END WHERE Login=$l;";
            cmd.Parameters.AddWithValue("$l", login); cmd.Parameters.AddWithValue("$a", ad); cmd.Parameters.AddWithValue("$q", qurumKod); cmd.Parameters.AddWithValue("$r", rol);
            cmd.Parameters.AddWithValue("$k", aktiv is null ? DBNull.Value : aktiv.Value ? 1 : 0);
            return await cmd.ExecuteNonQueryAsync();
        });

        public static string ParolQaydasi(string p, int min)
        {
            if (p.Length < min) return $"Parol ən azı {min} simvol olmalıdır.";
            if (!p.Any(char.IsUpper) || !p.Any(char.IsLower) || !p.Any(char.IsDigit)) return "Parolda böyük hərf, kiçik hərf və rəqəm olmalıdır.";
            return null;
        }

        public static string MuveqqetiParol() => "Tmp-" + Convert.ToHexString(RandomNumberGenerator.GetBytes(3)) + "a1";

        // ---------- Admin parolu (ayrıca, ProgramData-da hash kimi) ----------
        public static async Task<bool> AdminVarAsync() => await SistemRepo.ParamStrAsync("AdminHash") != null;

        public static async Task AdminTeyinAsync(string parol)
        {
            var (h, s) = await Task.Run(() => Hash(parol));
            await SistemRepo.ParamYazAsync("AdminHash", Convert.ToBase64String(h));
            await SistemRepo.ParamYazAsync("AdminSalt", Convert.ToBase64String(s));
        }

        public static async Task<bool> AdminYoxlaAsync(string parol)
        {
            var h = await SistemRepo.ParamStrAsync("AdminHash"); var s = await SistemRepo.ParamStrAsync("AdminSalt");
            if (h == null || s == null) return false;
            return await Task.Run(() => Yoxla(parol, Convert.FromBase64String(h), Convert.FromBase64String(s)));
        }
    }
}
