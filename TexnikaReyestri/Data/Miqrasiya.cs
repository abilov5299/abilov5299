using System.Text.Json;
using Microsoft.Data.Sqlite;

namespace TexnikaReyestri.Data
{
    /// <summary>
    /// Köhnə quruluşdan (pasport, silah sistemi, texniki vəziyyət...) yeni 5 bölməli quruluşa bir dəfəlik keçid.
    /// Əvvəlcə bazanın tam nüsxəsi götürülür, sonra məlumat yeni sahələrə köçürülür və artıq sütunlar silinir.
    /// </summary>
    internal static class Miqrasiya
    {
        /// <summary>Köhnə sütun hələ varsa keçidi edir. Schema.Yarat yeni sütunları əlavə etdikdən sonra çağırılır.</summary>
        public static void BesBolme(SqliteConnection con, Func<string> cedvelSql)
        {
            if (!SutunVar(con, "Texnikalar", "LuleNo")) return;

            // 1. Ehtiyat nüsxə (bazanın yanında)
            string db = con.DataSource;
            if (!string.IsNullOrEmpty(db) && File.Exists(db))
            {
                string bak = Path.Combine(Path.GetDirectoryName(db)!, $"{Path.GetFileNameWithoutExtension(db)}_kohne_{DateTime.Now:yyyyMMdd_HHmmss}.db");
                LocalDb.Exec(con, $"VACUUM INTO '{bak.Replace("'", "''")}';");
            }

            using var tr = con.BeginTransaction();
            void X(string sql) { using var c = con.CreateCommand(); c.Transaction = tr; c.CommandText = sql; c.ExecuteNonQuery(); }

            // 2. Köhnə sahələrdən yeni sahələrə
            X(@"UPDATE Texnikalar SET
  Nov = CASE WHEN Nov IN ('Əsas döyüş tankı','Piyadaların döyüş maşını','Zirehli transportyor','Özüyeriyən artilleriya qurğusu') THEN 'Zirehli texnika'
             WHEN Nov = 'Təkərli texnika' THEN 'Nəqliyyat vasitəsi' WHEN Nov IN ('Nəqliyyat vasitəsi','Zirehli texnika','Digər texnika') THEN Nov
             WHEN Nov IS NULL THEN NULL ELSE 'Digər texnika' END,
  Kateqoriya = IFNULL(Kateqoriya, Derece),
  NaryadNo = IFNULL(NaryadNo, QebulSenedi),
  GonderenQurum = IFNULL(GonderenQurum, HerbiHisse),
  Gostericiler = IFNULL(Gostericiler, (SELECT CASE WHEN COUNT(*) > 0 THEN json_group_array(json_object('Nov', n, 'Miqdar', m)) END FROM (
      SELECT 'Gediş, km' AS n, Km AS m WHERE Km IS NOT NULL UNION ALL SELECT 'Motosaat', Motosaat WHERE Motosaat IS NOT NULL
      UNION ALL SELECT 'Atəş sayı', Atislar WHERE Atislar IS NOT NULL))),
  SonTemirNovu = IFNULL(SonTemirNovu, CASE WHEN IFNULL(EsasliTemir,'') <> '' THEN 'Əsaslı təmir' WHEN IFNULL(OrtaTemir,'') <> '' THEN 'Orta təmir' END),
  SonTemirTarixi = IFNULL(SonTemirTarixi, CASE WHEN IFNULL(EsasliTemir,'') <> '' THEN CASE WHEN EsasliTemir GLOB '[12][0-9][0-9][0-9]-[01][0-9]-[0-3][0-9]' THEN EsasliTemir END
                                               WHEN OrtaTemir GLOB '[12][0-9][0-9][0-9]-[01][0-9]-[0-3][0-9]' THEN OrtaTemir END),
  SonTemirQeyd = IFNULL(SonTemirQeyd, NULLIF(TRIM(IFNULL('Orta təmir: ' || OrtaTemir || char(10), '') || IFNULL('Əsaslı təmir: ' || EsasliTemir, '')), '')),
  Nasazliqlar = IFNULL(Nasazliqlar, NULLIF(TRIM(
      IFNULL('Mühərrik: ' || VMuherrik || char(10), '') || IFNULL('Şassi: ' || VSassi || char(10), '') || IFNULL('Lülə: ' || VLule || char(10), '') ||
      IFNULL('Atəşi idarəetmə: ' || VAis || char(10), '') || IFNULL('Optika: ' || VOptik || char(10), '') || IFNULL('Digər: ' || VDiger || char(10), '') ||
      IFNULL('Qeyd: ' || Qeyd || char(10), '') || IFNULL('Yararsızlıq səbəbi: ' || YararsizSebeb || char(10), '') || IFNULL('Təmir qeydi: ' || TemirQeyd, '')), '')),
  TemirNovu = CASE TemirNovu WHEN 'Cari' THEN 'Cari təmir' WHEN 'Orta' THEN 'Orta təmir' WHEN 'Əsaslı' THEN 'Əsaslı təmir' ELSE TemirNovu END,
  MuddetVahidi = IFNULL(MuddetVahidi, CASE WHEN IcraMuddeti IS NOT NULL THEN 'Gün' END),
  GecikmeSebebi = NULLIF(TRIM(IFNULL(GecikmeSebebi || char(10), '') || IFNULL(PlanDeyisikliyi, '')), ''),
  TehvilSenedNo = IFNULL(TehvilSenedNo, TehvilSenedi),
  Status = CASE WHEN Status IN ('Hissə gözləyir','Qismən təmin edilib','Ehtiyat hissələrinin təminatı gözlənilir') THEN 'Təmirdə' ELSE Status END;");
            X("UPDATE Texnikalar SET Status='Təmir tamamlanıb, təhvil gözlənilir' WHERE IFNULL(FaktikiBitme,'')<>'' AND IFNULL(Status,'') NOT IN ('Təhvil verilib','Təmirə yararsız');");
            X("UPDATE Texnikalar SET TehvilTarixi = FaktikiBitme WHERE Status='Təhvil verilib' AND TehvilTarixi IS NULL;");

            // 3. Sorğulardakı ehtiyat hissələri texnikanın öz siyahısına köçür (sorğuda "Marka (ZavodNo)" kimi yazılıb); məktubun nəticəsi yenidən hesablanır
            foreach (var g in SorguHisseleri(con, tr)) SorguRepo.TexnikaUzreYenileAsync(con, tr, g).GetAwaiter().GetResult();
            X("UPDATE Sorgular SET CavabVez='İcra başa çatıb' WHERE CavabVez='Cavab verilib';");

            // 4. Cədvəli yalnız yeni sütunlarla yenidən qur (artıq sütunlar silinir); axtarış cədvəli də yenilənir
            var kodlar = string.Join(", ", new[] { "Id", "Guid", "Versiya", "Deyisdi", "Silinib" }.Concat(Saheler.Hamisi.Select(s => s.Kod)));
            X("DROP TABLE IF EXISTS TexnikaAxtar;");
            X(cedvelSql().Replace("CREATE TABLE IF NOT EXISTS Texnikalar (", "CREATE TABLE Texnikalar_yeni ("));
            X($"INSERT INTO Texnikalar_yeni ({kodlar}) SELECT {kodlar} FROM Texnikalar;");
            X("DROP TABLE Texnikalar;");
            X("ALTER TABLE Texnikalar_yeni RENAME TO Texnikalar;");
            X(@"DELETE FROM Icmal;
INSERT INTO Icmal SELECT IFNULL(TemirZavodu,''), IFNULL(Status,''), COUNT(*) FROM Texnikalar WHERE Silinib=0 GROUP BY 1, 2;");
            tr.Commit();
            LocalDb.Exec(con, "VACUUM;");   // köhnə cədvəlin yeri boşalsın
        }

        private static IEnumerable<string> SorguHisseleri(SqliteConnection con, SqliteTransaction tr)
        {
            var yeni = new Dictionary<string, List<Hisse>>();   // texnika Guid → hissələr
            using (var c = con.CreateCommand())
            {
                c.Transaction = tr;
                c.CommandText = "SELECT Texnikalar, Hisseler, IFNULL(CavabTarixi, QeydTarixi) FROM Sorgular WHERE Silinib=0 AND Hisseler IS NOT NULL AND Hisseler<>'[]';";
                using var r = c.ExecuteReader();
                while (r.Read())
                {
                    var tex = SorguRepo.TexnikaJson(r.IsDBNull(0) ? null : r.GetString(0));
                    var his = SorguRepo.HisseJson(r.GetString(1));
                    string tarix = r.IsDBNull(2) ? Vaxt.Iso(DateTime.Today) : r.GetString(2);
                    foreach (var h in his)
                    {
                        var t = tex.FirstOrDefault(x => $"{x.Marka} ({x.ZavodNo})" == h.Texnika) ?? (tex.Count == 1 ? tex[0] : null);
                        if (t == null || string.IsNullOrWhiteSpace(h.Ad)) continue;
                        if (!yeni.TryGetValue(t.Guid, out var l)) yeni[t.Guid] = l = new();
                        var y = new Hisse { Ad = h.Ad, Vahid = h.Vahid, Teleb = h.Teleb };
                        if (h.Qebul > 0) y.Qebullar.Add(new HisseQebul { Tarix = tarix, Miqdar = h.Qebul });
                        l.Add(y);
                    }
                }
            }
            using var u = con.CreateCommand(); u.Transaction = tr;
            u.CommandText = "UPDATE Texnikalar SET Hisseler=$h, Teminat=$t WHERE Guid=$g AND Hisseler IS NULL;";
            var ph = u.Parameters.Add("$h", SqliteType.Text); var pt = u.Parameters.Add("$t", SqliteType.Text); var pg = u.Parameters.Add("$g", SqliteType.Text);
            foreach (var (g, l) in yeni)
            {
                ph.Value = JsonSerializer.Serialize(l); pt.Value = (object)TexnikaJson.Teminat(l) ?? DBNull.Value; pg.Value = g;
                u.ExecuteNonQuery();
            }
            return yeni.Keys.ToList();
        }

        public static bool SutunVar(SqliteConnection con, string cedvel, string sutun)
        {
            using var c = con.CreateCommand(); c.CommandText = $"SELECT COUNT(*) FROM pragma_table_info('{cedvel}') WHERE name=$n;";
            c.Parameters.AddWithValue("$n", sutun);
            return Convert.ToInt64(c.ExecuteScalar()) > 0;
        }
    }
}
