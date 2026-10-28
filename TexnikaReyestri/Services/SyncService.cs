using System.Buffers.Binary;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using TexnikaReyestri.Data;

namespace TexnikaReyestri.Services
{
    public sealed record PaketMelumat(string Fayl, string Gonderen, string Alan, long Seq, long Tarix);
    public sealed record SyncNetice(int Yeni, int Deyisen, int Silinen, string Fayl);

    /// <summary>
    /// USB paketi (.tqx). Quruluş:
    ///   "TRX1" | başlıq uzunluğu (4 bayt) | başlıq (JSON, açıq) | şifrəli məzmun | GCM etiketi (16) | imza uzunluğu (2) | imza
    /// Şifrə: hər paket üçün yeni ECDH P-384 açarı → SHA-256 → AES-256-GCM. Başlıq GCM-in AAD-sidir (dəyişdirilə bilməz).
    /// İmza: göndərən zavodun ECDSA P-384 açarı ilə bütün fayl (imzadan əvvəlki hissə). Ardıcıllıq nömrəsi təkrar idxalı bloklayır.
    /// </summary>
    public static class SyncService
    {
        private static readonly byte[] Magic = "TRX1"u8.ToArray();
        private sealed record Baslik(int V, string From, string To, long Seq, long Created, string Eph, string Nonce);

        public static readonly string[] IxracAddimlari = { "Dəyişikliklər toplanır", "Paket sıxılır", "AES-256-GCM ilə şifrələnir", "Zavod imzası (ECDSA) əlavə olunur", "USB-yə yazılır və yoxlanılır" };
        public static readonly string[] IdxalAddimlari = { "Paket oxunur", "İmza yoxlanılır", "Şifrə açılır", "Ardıcıllıq nömrəsi yoxlanılır", "Dəyişikliklər bazaya tətbiq olunur" };

        public static async Task<(long sonTarix, long seq, int say)> IxracHazirlikAsync()
        {
            var (t, s) = await SonIxracAsync();
            return (t, s, (int)await TexnikaRepo.DeltaSayAsync(t, Session.ZavodFiltri) + (await SorguRepo.DeltaAsync(t, Session.ZavodFiltri)).Count);
        }

        public static async Task<SyncNetice> IxracAsync(string qovluq, IProgress<int> addim, CancellationToken ct = default)
        {
            var qurumlar = await SistemRepo.QurumlarAsync();
            string alan = qurumlar.First(q => q.Kod == Session.QurumKod).UstKod ?? throw new InvalidOperationException("Bu qurumun üst qurumu yoxdur.");
            var alanAcar = await KeyStore.EtibarliAsync(alan) ?? throw new InvalidOperationException(
                $"Paketi alacaq qurumun ({qurumlar.First(q => q.Kod == alan).Ad}) açıq açarı bu kompüterə idxal edilməyib.\n\n" +
                "Həll: həmin qurumun .trkey faylını alın → Admin paneli → USB açarları → Açar idxal et → barmaq izini telefonla yoxlayıb Etibar et.");

            addim?.Report(0);
            var (sonTarix, sonSeq) = await SonIxracAsync();
            long indi = Vaxt.Indi;
            var dt = await TexnikaRepo.DeltaAsync(sonTarix, ct);
            var menim = qurumlar.First(q => q.Kod == Session.QurumKod);
            var qeydler = dt.Rows.Cast<System.Data.DataRow>().Where(r => menim.Tip != QurumTip.Zavod || Convert.ToString(r["TemirZavodu"]) == menim.Ad).Select(r => dt.Columns.Cast<System.Data.DataColumn>()
                .Where(c => c.ColumnName != "Id" && c.ColumnName != "Deyisdi" && r[c] != DBNull.Value).ToDictionary(c => c.ColumnName, c => r[c])).ToList();
            int sil = qeydler.Count(q => Convert.ToInt64(q.GetValueOrDefault("Silinib", 0L)) == 1);
            // Dəyişiklik tarixçəsi də paketə düşür (hər qeydin "_Tarixce" sahəsi: [vaxt, istifadəçi, sahə, köhnə, yeni])
            var tarixce = await TexnikaRepo.TarixceDeltaAsync(sonTarix, ct);
            foreach (var q in qeydler) if (q["Guid"] is string g && tarixce.TryGetValue(g, out var tl)) q["_Tarixce"] = tl;
            // Təchizat sorğuları eyni paketdə ("_Sorgu" nişanı ilə)
            var sorgular = await SorguRepo.DeltaAsync(sonTarix, menim.Tip == QurumTip.Zavod ? menim.Ad : null);
            foreach (var q in sorgular) q["_Sorgu"] = 1;
            int sorguSay = sorgular.Count; qeydler.AddRange(sorgular);

            addim?.Report(1);
            byte[] json = JsonSerializer.SerializeToUtf8Bytes(qeydler);
            byte[] sixilmis;
            using (var ms = new MemoryStream()) { using (var gz = new GZipStream(ms, CompressionLevel.Optimal)) gz.Write(json); sixilmis = ms.ToArray(); }

            addim?.Report(2);
            using var eph = ECDiffieHellman.Create(ECCurve.NamedCurves.nistP384);
            using var alanPub = ECDiffieHellman.Create(); alanPub.ImportSubjectPublicKeyInfo(alanAcar.sifre, out _);
            byte[] key = eph.DeriveKeyFromHash(alanPub.PublicKey, HashAlgorithmName.SHA256, Magic, null);
            byte[] nonce = RandomNumberGenerator.GetBytes(12);
            var b = new Baslik(1, Session.QurumKod, alan, sonSeq + 1, indi, Convert.ToBase64String(eph.PublicKey.ExportSubjectPublicKeyInfo()), Convert.ToBase64String(nonce));
            byte[] bas = JsonSerializer.SerializeToUtf8Bytes(b);
            byte[] cipher = new byte[sixilmis.Length], tag = new byte[16];
            using (var gcm = new AesGcm(key, 16)) gcm.Encrypt(nonce, sixilmis, cipher, tag, bas);
            CryptographicOperations.ZeroMemory(key);

            addim?.Report(3);
            using var ms2 = new MemoryStream();
            ms2.Write(Magic); var len = new byte[4]; BinaryPrimitives.WriteInt32LittleEndian(len, bas.Length); ms2.Write(len);
            ms2.Write(bas); ms2.Write(cipher); ms2.Write(tag);
            byte[] imzalanan = ms2.ToArray();
            byte[] imza; using (var e = KeyStore.Imza()) imza = e.SignData(imzalanan, HashAlgorithmName.SHA384);
            var il = new byte[2]; BinaryPrimitives.WriteUInt16LittleEndian(il, (ushort)imza.Length); ms2.Write(il); ms2.Write(imza);

            addim?.Report(4);
            Directory.CreateDirectory(qovluq);
            string fayl = Path.Combine(qovluq, $"TQX-{Session.QurumKod}-{b.Seq:000000}.tqx");
            byte[] tam = ms2.ToArray();
            await File.WriteAllBytesAsync(fayl, tam, ct);
            if (!(await File.ReadAllBytesAsync(fayl, ct)).AsSpan().SequenceEqual(tam)) throw new IOException("USB-yə yazılan fayl yoxlamadan keçmədi.");
            await Jurnal("İxrac", Session.QurumKod, b.Seq, Path.GetFileName(fayl), qeydler.Count - sorguSay - sil, 0, sil, "Uğurlu", indi.ToString());
            addim?.Report(5);
            return new SyncNetice(qeydler.Count - sorguSay - sil, 0, sil, fayl);
        }

        public static PaketMelumat Bax(string fayl)
        {
            var (b, _, _, _, _) = Parcala(File.ReadAllBytes(fayl));
            return new PaketMelumat(fayl, b.From, b.To, b.Seq, b.Created);
        }

        public static async Task<SyncNetice> IdxalAsync(string fayl, IProgress<int> addim, CancellationToken ct = default)
        {
            addim?.Report(0);
            byte[] data = await File.ReadAllBytesAsync(fayl, ct);
            var (b, bas, cipher, tag, imzaHissesi) = Parcala(data);

            addim?.Report(1);
            var gonderen = await KeyStore.EtibarliAsync(b.From) ?? throw new CryptographicException($"\"{b.From}\" qurumunun açarı etibar edilənlər siyahısında yoxdur.");
            int imzaLen = BinaryPrimitives.ReadUInt16LittleEndian(data.AsSpan(imzaHissesi, 2));
            using (var e = ECDsa.Create())
            {
                e.ImportSubjectPublicKeyInfo(gonderen.imza, out _);
                if (!e.VerifyData(data.AsSpan(0, imzaHissesi), data.AsSpan(imzaHissesi + 2, imzaLen), HashAlgorithmName.SHA384))
                    throw new CryptographicException("İmza yanlışdır: paket dəyişdirilib və ya saxtadır. Heç bir məlumat tətbiq olunmadı.");
            }
            if (b.To != Session.QurumKod) throw new CryptographicException($"Bu paket \"{b.To}\" üçündür, bu kompüter üçün deyil.");

            addim?.Report(2);
            byte[] acilmis = new byte[cipher.Length];
            using (var my = KeyStore.Sifre())
            using (var ephPub = ECDiffieHellman.Create())
            {
                ephPub.ImportSubjectPublicKeyInfo(Convert.FromBase64String(b.Eph), out _);
                byte[] key = my.DeriveKeyFromHash(ephPub.PublicKey, HashAlgorithmName.SHA256, Magic, null);
                using var gcm = new AesGcm(key, 16);
                gcm.Decrypt(Convert.FromBase64String(b.Nonce), cipher, tag, acilmis, bas);
                CryptographicOperations.ZeroMemory(key);
            }

            addim?.Report(3);
            long sonSeq = await SonIdxalSeqAsync(b.From);
            if (b.Seq <= sonSeq) throw new InvalidOperationException($"Bu paket artıq idxal olunub (paket №{b.Seq}, son idxal №{sonSeq}). Təkrar idxal qadağandır.");

            addim?.Report(4);
            List<Dictionary<string, JsonElement>> qeydler;
            using (var gz = new GZipStream(new MemoryStream(acilmis), CompressionMode.Decompress))
                qeydler = await JsonSerializer.DeserializeAsync<List<Dictionary<string, JsonElement>>>(gz, cancellationToken: ct);
            // Zavod yalnız öz texnikasını göndərə bilər: başqa zavoda aid qeydlər (yeni və ya mövcud) qəbul edilmir
            var gq = (await SistemRepo.QurumlarAsync()).FirstOrDefault(q => q.Kod == b.From);
            string yalniz = gq?.Tip == QurumTip.Zavod ? gq.Ad : null;
            var (yeni, dey, sil, red) = await TetbiqAsync(qeydler, yalniz, ct);
            if (yeni + dey >= 1000) await LocalDb.AnalizAsync();
            await Jurnal("İdxal", b.From, b.Seq, Path.GetFileName(fayl), yeni, dey, sil, "Uğurlu",
                red > 0 ? $"{red} qeyd rədd edildi: {gq?.Ad} başqa zavodun texnikasını göndərə bilməz" : null);
            if (red > 0) await SistemRepo.LogAsync("Xəta", $"{Path.GetFileName(fayl)}: {red} qeyd başqa zavoda aid olduğu üçün qəbul edilmədi");
            await SistemRepo.BildirisAsync($"sync:{b.From}:{b.Seq}", "new", "İdxal tamamlandı", $"{b.From}: {yeni} yeni, {dey} dəyişiklik, {sil} silinmə", null, null);
            addim?.Report(5);
            return new SyncNetice(yeni, dey, sil, fayl);
        }

        public static Task RedJurnalAsync(string fayl, string sebeb)
        {
            string q = "?"; long seq = 0;
            try { var m = Bax(fayl); q = m.Gonderen; seq = m.Seq; } catch { }
            return Jurnal("İdxal", q, seq, Path.GetFileName(fayl), 0, 0, 0, "Rədd edildi", sebeb);
        }

        private static (Baslik b, byte[] bas, byte[] cipher, byte[] tag, int imzaHissesi) Parcala(byte[] d)
        {
            if (d.Length < 30 || !d.AsSpan(0, 4).SequenceEqual(Magic)) throw new InvalidDataException("Bu fayl proqramın paketi deyil.");
            int bl = BinaryPrimitives.ReadInt32LittleEndian(d.AsSpan(4, 4));
            if (bl <= 0 || bl > 4096 || 8 + bl + 18 > d.Length) throw new InvalidDataException("Paketin başlığı zədələnib.");
            byte[] bas = d.AsSpan(8, bl).ToArray();
            var b = JsonSerializer.Deserialize<Baslik>(bas);
            int imzaLen = BinaryPrimitives.ReadUInt16LittleEndian(d.AsSpan(d.Length - 2 - FindSig(d), 2));
            int imzaHissesi = d.Length - 2 - imzaLen;
            int cs = 8 + bl, cl = imzaHissesi - 16 - cs;
            if (cl < 0) throw new InvalidDataException("Paket zədələnib.");
            return (b, bas, d.AsSpan(cs, cl).ToArray(), d.AsSpan(cs + cl, 16).ToArray(), imzaHissesi);
        }

        // İmza sonda: [uzunluq 2 bayt][imza]. ECDSA P-384 imzası .NET-də sabit 96 baytdır (IEEE P1363).
        private static int FindSig(byte[] d) => 96;

        private static async Task<(int, int, int, int)> TetbiqAsync(List<Dictionary<string, JsonElement>> qeydler, string yalnizZavod, CancellationToken ct)
        {
            var kodlar = Saheler.Hamisi.Select(s => s.Kod).ToArray();
            return await LocalDb.YazAsync(async (con, tr) =>
            {
                int yeni = 0, dey = 0, sil = 0, red = 0; long indi = Vaxt.Indi;
                using var sel = con.CreateCommand(); sel.Transaction = tr; sel.CommandText = "SELECT Versiya, IFNULL(TemirZavodu,'') FROM Texnikalar WHERE Guid=$g;";
                var pg = sel.Parameters.Add("$g", Microsoft.Data.Sqlite.SqliteType.Text); sel.Prepare();
                using var up = con.CreateCommand(); up.Transaction = tr;
                up.CommandText = $"INSERT INTO Texnikalar (Guid, Versiya, Deyisdi, Silinib, {string.Join(",", kodlar)}) VALUES ($g,$v,$d,$s,{string.Join(",", kodlar.Select(k => "$" + k))}) " +
                                 $"ON CONFLICT(Guid) DO UPDATE SET Versiya=excluded.Versiya, Deyisdi=excluded.Deyisdi, Silinib=excluded.Silinib, {string.Join(",", kodlar.Select(k => $"{k}=excluded.{k}"))};";
                var p = new Dictionary<string, Microsoft.Data.Sqlite.SqliteParameter>();
                foreach (var n in new[] { "$g", "$v", "$d", "$s" }.Concat(kodlar.Select(k => "$" + k))) p[n] = up.Parameters.Add(n, Microsoft.Data.Sqlite.SqliteType.Text);
                up.Prepare();
                using var idc = con.CreateCommand(); idc.Transaction = tr; idc.CommandText = "SELECT Id FROM Texnikalar WHERE Guid=$g;";
                var ig = idc.Parameters.Add("$g", Microsoft.Data.Sqlite.SqliteType.Text); idc.Prepare();
                using var hc = con.CreateCommand(); hc.Transaction = tr;
                hc.CommandText = "INSERT INTO Tarixce (TexnikaId, Tarix, Istifadeci, Sahe, Kohne, Yeni) SELECT $i,$t,$u,$s,$k,$y " +
                                 "WHERE NOT EXISTS (SELECT 1 FROM Tarixce WHERE TexnikaId=$i AND Tarix=$t AND Sahe=$s);";   // təkrar yazılmasın
                var hp = new[] { "$i", "$t", "$u", "$s", "$k", "$y" }.ToDictionary(n => n, n => hc.Parameters.Add(n, Microsoft.Data.Sqlite.SqliteType.Text)); hc.Prepare();
                var sorgular = qeydler.Where(q => q.ContainsKey("_Sorgu")).ToList();
                foreach (var q in qeydler)
                {
                    ct.ThrowIfCancellationRequested();
                    if (q.ContainsKey("_Sorgu")) continue;
                    string guid = q["Guid"].GetString(); long ver = q["Versiya"].GetInt64(); bool silinib = q.TryGetValue("Silinib", out var sv) && sv.GetInt64() == 1;
                    pg.Value = guid; object movcud = null; string movcudZavod = null;
                    await using (var rd = await sel.ExecuteReaderAsync(ct)) if (await rd.ReadAsync(ct)) { movcud = rd.GetInt64(0); movcudZavod = rd.GetString(1); }
                    if (yalnizZavod != null)
                    {
                        string gelen = q.TryGetValue("TemirZavodu", out var tz) && tz.ValueKind == JsonValueKind.String ? tz.GetString() : null;
                        if (gelen != yalnizZavod || (movcudZavod != null && movcudZavod != yalnizZavod)) { red++; continue; }
                    }
                    if (movcud is long mv && mv >= ver) continue;   // bizdəki versiya eyni və ya daha yenidir
                    p["$g"].Value = guid; p["$v"].Value = ver; p["$d"].Value = indi; p["$s"].Value = silinib ? 1 : 0;
                    foreach (var k in kodlar)
                        p["$" + k].Value = q.TryGetValue(k, out var v) ? v.ValueKind switch
                        { JsonValueKind.Number => v.GetDouble(), JsonValueKind.String => v.GetString(), _ => DBNull.Value } : DBNull.Value;
                    await up.ExecuteNonQueryAsync(ct);
                    if (q.TryGetValue("_Tarixce", out var th) && th.ValueKind == JsonValueKind.Array)
                    {
                        ig.Value = guid; long tid = (long)await idc.ExecuteScalarAsync(ct);
                        foreach (var h in th.EnumerateArray())
                        {
                            hp["$i"].Value = tid; hp["$t"].Value = h[0].GetInt64(); hp["$u"].Value = h[1].GetString(); hp["$s"].Value = h[2].GetString();
                            hp["$k"].Value = h[3].ValueKind == JsonValueKind.Null ? DBNull.Value : h[3].GetString();
                            hp["$y"].Value = h[4].ValueKind == JsonValueKind.Null ? DBNull.Value : h[4].GetString();
                            await hc.ExecuteNonQueryAsync(ct);
                        }
                    }
                    if (silinib) sil++; else if (movcud is null) yeni++; else dey++;
                }
                using (var nc = con.CreateCommand()) { nc.Transaction = tr; nc.CommandText = "UPDATE Texnikalar SET Status='Hissə gözləyir' WHERE Status='Ehtiyat hissələrinin təminatı gözlənilir';"; await nc.ExecuteNonQueryAsync(ct); }   // ekran adı ilə gəlmiş paket
                var (sy, sd, sr) = await SorguRepo.TetbiqAsync(con, tr, sorgular, yalnizZavod, ct);
                return (yeni + sy, dey + sd, sil, red + sr);
            }, ct);
        }

        public static async Task<(long tarix, long seq)> SonIxracAsync()
        {
            await using var con = await LocalDb.AcAsync();
            using var cmd = con.CreateCommand(); cmd.CommandText = "SELECT Qeyd, Seq FROM SyncJurnal WHERE Emeliyyat='İxrac' AND Netice='Uğurlu' ORDER BY Seq DESC LIMIT 1;";
            await using var r = await cmd.ExecuteReaderAsync();
            return await r.ReadAsync() ? (long.Parse(r.GetString(0)), r.GetInt64(1)) : (0, 0);
        }

        private static async Task<long> SonIdxalSeqAsync(string qurum)
        {
            await using var con = await LocalDb.AcAsync();
            using var cmd = con.CreateCommand(); cmd.CommandText = "SELECT IFNULL(MAX(Seq),0) FROM SyncJurnal WHERE Emeliyyat='İdxal' AND Netice='Uğurlu' AND Qurum=$q;";
            cmd.Parameters.AddWithValue("$q", qurum);
            return Convert.ToInt64(await cmd.ExecuteScalarAsync());
        }

        public static async Task<System.Data.DataTable> JurnalAsync()
        {
            await using var con = await LocalDb.AcAsync();
            using var cmd = con.CreateCommand(); cmd.CommandText = "SELECT Tarix, Emeliyyat, Fayl, Yeni, Deyisen, Silinen, Netice, Qeyd FROM SyncJurnal ORDER BY Id DESC LIMIT 200;";
            await using var r = await cmd.ExecuteReaderAsync(); var dt = await LocalDb.CedvelAsync(r); return dt;
        }

        private static Task Jurnal(string em, string qurum, long seq, string fayl, int yeni, int dey, int sil, string netice, string qeyd) => LocalDb.YazAsync(async (con, tr) =>
        {
            using var cmd = con.CreateCommand(); cmd.Transaction = tr;
            cmd.CommandText = "INSERT INTO SyncJurnal (Tarix, Emeliyyat, Qurum, Seq, Fayl, Yeni, Deyisen, Silinen, Netice, Qeyd) VALUES ($t,$e,$q,$s,$f,$y,$d,$x,$n,$k);";
            cmd.Parameters.AddWithValue("$t", Vaxt.Indi); cmd.Parameters.AddWithValue("$e", em); cmd.Parameters.AddWithValue("$q", qurum); cmd.Parameters.AddWithValue("$s", seq);
            cmd.Parameters.AddWithValue("$f", fayl); cmd.Parameters.AddWithValue("$y", yeni); cmd.Parameters.AddWithValue("$d", dey); cmd.Parameters.AddWithValue("$x", sil);
            cmd.Parameters.AddWithValue("$n", netice); cmd.Parameters.AddWithValue("$k", (object)qeyd ?? DBNull.Value);
            return await cmd.ExecuteNonQueryAsync();
        });
    }
}
