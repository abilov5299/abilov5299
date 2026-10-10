using System.Security.Cryptography;
using System.Text.Json;
using TexnikaReyestri.Data;

namespace TexnikaReyestri.Services
{
    /// <summary>
    /// Bu kompüterin açarları: ECDSA P-384 (imza) və ECDH P-384 (şifrə açarı mübadiləsi).
    /// Gizli açarlar DPAPI ilə (LocalMachine) şifrələnib diskdə saxlanılır və kompüterdən çıxmır.
    /// Açıq açar faylı (.trkey) USB ilə üst quruma verilir.
    /// </summary>
    public static class KeyStore
    {
        private static string ImzaFayl => Path.Combine(AppPaths.Keys, "imza.key");
        private static string SifreFayl => Path.Combine(AppPaths.Keys, "sifre.key");

        public static bool Var => File.Exists(ImzaFayl) && File.Exists(SifreFayl);

        public static void Yarat()
        {
            Directory.CreateDirectory(AppPaths.Keys);
            using var e = ECDsa.Create(ECCurve.NamedCurves.nistP384);
            using var d = ECDiffieHellman.Create(ECCurve.NamedCurves.nistP384);
            File.WriteAllBytes(ImzaFayl, Qoru(e.ExportPkcs8PrivateKey()));
            File.WriteAllBytes(SifreFayl, Qoru(d.ExportPkcs8PrivateKey()));
        }

        public static ECDsa Imza() { var k = ECDsa.Create(); k.ImportPkcs8PrivateKey(Ac(File.ReadAllBytes(ImzaFayl)), out _); return k; }
        public static ECDiffieHellman Sifre() { var k = ECDiffieHellman.Create(); k.ImportPkcs8PrivateKey(Ac(File.ReadAllBytes(SifreFayl)), out _); return k; }

        public static (byte[] imzaPub, byte[] sifrePub) AciqAcarlar()
        {
            using var e = Imza(); using var d = Sifre();
            return (e.ExportSubjectPublicKeyInfo(), d.PublicKey.ExportSubjectPublicKeyInfo());
        }

        /// <summary>Barmaq izi: açıq açarların SHA-256-sının ilk 8 baytı (telefonla yoxlamaq üçün).</summary>
        public static string BarmaqIzi(byte[] imzaPub, byte[] sifrePub)
        {
            var h = SHA256.HashData(imzaPub.Concat(sifrePub).ToArray());
            return string.Join("·", Enumerable.Range(0, 4).Select(i => Convert.ToHexString(h, i * 2, 2)));
        }

        public sealed record AcarFayli(string Qurum, string QurumAd, string Imza, string Sifre, string BarmaqIzi);

        public static void AciqAcariYaz(string fayl)
        {
            var (i, s) = AciqAcarlar();
            File.WriteAllText(fayl, JsonSerializer.Serialize(new AcarFayli(Session.QurumKod, Session.QurumAd, Convert.ToBase64String(i), Convert.ToBase64String(s), BarmaqIzi(i, s))));
        }

        public static AcarFayli Oxu(string fayl)
        {
            var a = JsonSerializer.Deserialize<AcarFayli>(File.ReadAllText(fayl));
            if (BarmaqIzi(Convert.FromBase64String(a.Imza), Convert.FromBase64String(a.Sifre)) != a.BarmaqIzi) throw new InvalidDataException("Açar faylı zədələnib.");
            return a;
        }

        public static Task EtibarEtAsync(AcarFayli a) => LocalDb.YazAsync(async (con, tr) =>
        {
            using var cmd = con.CreateCommand(); cmd.Transaction = tr;
            cmd.CommandText = "INSERT INTO Acarlar VALUES ($q,$i,$s,$b,$t) ON CONFLICT(QurumKod) DO UPDATE SET ImzaAcari=excluded.ImzaAcari, SifreAcari=excluded.SifreAcari, BarmaqIzi=excluded.BarmaqIzi, Elave=excluded.Elave;";
            cmd.Parameters.AddWithValue("$q", a.Qurum); cmd.Parameters.AddWithValue("$i", Convert.FromBase64String(a.Imza));
            cmd.Parameters.AddWithValue("$s", Convert.FromBase64String(a.Sifre)); cmd.Parameters.AddWithValue("$b", a.BarmaqIzi); cmd.Parameters.AddWithValue("$t", Vaxt.Indi);
            return await cmd.ExecuteNonQueryAsync();
        });

        public static async Task<List<(string qurum, string iz, long tarix)>> EtibarlilarAsync()
        {
            await using var con = await LocalDb.AcAsync();
            using var cmd = con.CreateCommand(); cmd.CommandText = "SELECT QurumKod, BarmaqIzi, Elave FROM Acarlar ORDER BY QurumKod;";
            await using var r = await cmd.ExecuteReaderAsync(); var l = new List<(string, string, long)>();
            while (await r.ReadAsync()) l.Add((r.GetString(0), r.GetString(1), r.GetInt64(2)));
            return l;
        }

        public static async Task<(byte[] imza, byte[] sifre)?> EtibarliAsync(string qurum)
        {
            await using var con = await LocalDb.AcAsync();
            using var cmd = con.CreateCommand(); cmd.CommandText = "SELECT ImzaAcari, SifreAcari FROM Acarlar WHERE QurumKod=$q;"; cmd.Parameters.AddWithValue("$q", qurum);
            await using var r = await cmd.ExecuteReaderAsync();
            return await r.ReadAsync() ? ((byte[])r[0], (byte[])r[1]) : null;
        }

        private static byte[] Qoru(byte[] b) => OperatingSystem.IsWindows() ? ProtectedData.Protect(b, Entropy, DataProtectionScope.LocalMachine) : b;
        private static byte[] Ac(byte[] b) => OperatingSystem.IsWindows() ? ProtectedData.Unprotect(b, Entropy, DataProtectionScope.LocalMachine) : b;
        private static readonly byte[] Entropy = "TexnikaReyestri.v1"u8.ToArray();
    }
}
