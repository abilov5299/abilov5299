using System.Text;
using Microsoft.Data.Sqlite;

namespace TexnikaReyestri.Data
{
    internal static class Schema
    {
        public static void Yarat(SqliteConnection con)
        {
            var cols = new StringBuilder();
            foreach (var s in Saheler.Hamisi) cols.Append($",\n  {s.Kod} {Saheler.SqlTip(s.Nov)}");

            string cedvel = $@"
CREATE TABLE IF NOT EXISTS Texnikalar (
  Id        INTEGER PRIMARY KEY,           -- lokal açar (sürətli)
  Guid      TEXT NOT NULL UNIQUE,          -- USB sinxronizasiya açarı (bütün kompüterlərdə eyni)
  Versiya   INTEGER NOT NULL DEFAULT 1,
  Deyisdi   INTEGER NOT NULL,              -- unix ms, delta ixracı üçün
  Silinib   INTEGER NOT NULL DEFAULT 0{cols}
);";
            LocalDb.Exec(con, cedvel);
            // Köhnə bazada olmayan yeni sütunları əlavə et (miqrasiya)
            var movcud = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "PRAGMA table_info(Texnikalar);";
                using var r = cmd.ExecuteReader();
                while (r.Read()) movcud.Add(r.GetString(1));
            }
            foreach (var s in Saheler.Hamisi.Where(s => !movcud.Contains(s.Kod)))
                LocalDb.Exec(con, $"ALTER TABLE Texnikalar ADD COLUMN {s.Kod} {Saheler.SqlTip(s.Nov)};");
            LocalDb.Exec(con, "CREATE TABLE IF NOT EXISTS Icmal (Zavod TEXT NOT NULL, Status TEXT NOT NULL, Say INTEGER NOT NULL, PRIMARY KEY (Zavod, Status)) WITHOUT ROWID;");
            SorguRepo.Yarat(con);

            // Köhnə quruluşdan yeni 5 bölməyə keçid (bir dəfə; əvvəlcə ehtiyat nüsxə)
            Miqrasiya.BesBolme(con, () => cedvel);
            // Ehtiyat hissələri texnikadan məktuba (bir dəfə)
            Miqrasiya.HisseMektuba(con);
            // "Təminat gözləyir" status deyil, ayrıca işarədir (Teminat sahəsi); status kimi yazılıbsa düzəlir
            LocalDb.Exec(con, "UPDATE Texnikalar SET Status='Təmirdə', Teminat='Təminat gözləyir' WHERE Status='Təminat gözləyir';");
            // Köhnə statuslar: təminat artıq ayrıca sahədədir (USB ilə köhnə paket gələ bilər)
            using (var k = con.CreateCommand())
            {
                k.CommandText = "SELECT COUNT(*) FROM Icmal WHERE Say>0 AND Status IN ('Hissə gözləyir','Qismən təmin edilib','Ehtiyat hissələrinin təminatı gözlənilir');";
                if ((long)k.ExecuteScalar() > 0) LocalDb.Exec(con, "UPDATE Texnikalar SET Status='Təmirdə' WHERE Status IN ('Hissə gözləyir','Qismən təmin edilib','Ehtiyat hissələrinin təminatı gözlənilir');");
            }
            using (var k = con.CreateCommand())
            {
                // İndeksdə köhnə statuslar (və ya bir müddət status olmuş "Təminat gözləyir") qalıbsa yenidən qurulur
                k.CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE name='IX_T_Gec' AND (sql LIKE '%Hissə gözləyir%' OR sql LIKE '%Təminat gözləyir%');";
                if ((long)k.ExecuteScalar() > 0) LocalDb.Exec(con, "DROP INDEX IX_T_Gec;");
            }

            LocalDb.Exec(con, @"
-- Reyestrin filtr + sıralama indeksləri (keyset səhifələmə üçün sonda Id var)
CREATE INDEX IF NOT EXISTS IX_T_Siyahi  ON Texnikalar(Silinib, TemirZavodu, Status, IFNULL(GozlenilenBitme,''), Id);
CREATE INDEX IF NOT EXISTS IX_T_Bitme   ON Texnikalar(Silinib, IFNULL(GozlenilenBitme,''), Id);
CREATE INDEX IF NOT EXISTS IX_T_ZavodNo ON Texnikalar(ZavodNo);
CREATE INDEX IF NOT EXISTS IX_T_Teminat ON Texnikalar(Teminat) WHERE Silinib=0;
CREATE INDEX IF NOT EXISTS IX_T_Deyisdi ON Texnikalar(Deyisdi);
CREATE INDEX IF NOT EXISTS IX_T_Daxil   ON Texnikalar(TemirZavodu, DaxilolmaTarixi);
CREATE INDEX IF NOT EXISTS IX_T_Fakt    ON Texnikalar(TemirZavodu, FaktikiBitme);
-- Ana səhifə statistikası bütün zavodlar üzrə (üst qurumlar): cədvəli tam oxumamaq üçün örtən indekslər
CREATE INDEX IF NOT EXISTS IX_T_DaxilG  ON Texnikalar(DaxilolmaTarixi) WHERE Silinib=0;
CREATE INDEX IF NOT EXISTS IX_T_FaktG   ON Texnikalar(FaktikiBitme, BaslamaTarixi, TemirZavodu, TemirNovu) WHERE Silinib=0;
CREATE INDEX IF NOT EXISTS IX_T_Nov     ON Texnikalar(Nov) WHERE Silinib=0;
CREATE INDEX IF NOT EXISTS IX_T_Gec     ON Texnikalar(GozlenilenBitme, TemirZavodu) WHERE Silinib=0 AND Status IN ('Qəbul edilib','Təmirdə');

-- Mətn axtarışı: LIKE '%..%' əvəzinə (1 mln sətirdə ~6 ms)
CREATE VIRTUAL TABLE IF NOT EXISTS TexnikaAxtar USING fts5(
  Marka, ZavodNo, NaryadNo, Nasazliqlar, content='Texnikalar', content_rowid='Id', tokenize='unicode61 remove_diacritics 2');
CREATE TRIGGER IF NOT EXISTS TR_Axtar_I AFTER INSERT ON Texnikalar BEGIN
  INSERT INTO TexnikaAxtar(rowid, Marka, ZavodNo, NaryadNo, Nasazliqlar) VALUES (new.Id, new.Marka, new.ZavodNo, new.NaryadNo, new.Nasazliqlar); END;
CREATE TRIGGER IF NOT EXISTS TR_Axtar_D AFTER DELETE ON Texnikalar BEGIN
  INSERT INTO TexnikaAxtar(TexnikaAxtar, rowid, Marka, ZavodNo, NaryadNo, Nasazliqlar) VALUES ('delete', old.Id, old.Marka, old.ZavodNo, old.NaryadNo, old.Nasazliqlar); END;
CREATE TRIGGER IF NOT EXISTS TR_Axtar_U AFTER UPDATE OF Marka, ZavodNo, NaryadNo, Nasazliqlar ON Texnikalar BEGIN
  INSERT INTO TexnikaAxtar(TexnikaAxtar, rowid, Marka, ZavodNo, NaryadNo, Nasazliqlar) VALUES ('delete', old.Id, old.Marka, old.ZavodNo, old.NaryadNo, old.Nasazliqlar);
  INSERT INTO TexnikaAxtar(rowid, Marka, ZavodNo, NaryadNo, Nasazliqlar) VALUES (new.Id, new.Marka, new.ZavodNo, new.NaryadNo, new.Nasazliqlar); END;

-- İcmal cədvəli: ana səhifə və 'Cəmi' sayı GROUP BY etmədən buradan oxunur (~1 ms)
CREATE TABLE IF NOT EXISTS Icmal (Zavod TEXT NOT NULL, Status TEXT NOT NULL, Say INTEGER NOT NULL, PRIMARY KEY (Zavod, Status)) WITHOUT ROWID;
CREATE TRIGGER IF NOT EXISTS TR_Icmal_I AFTER INSERT ON Texnikalar WHEN new.Silinib = 0 BEGIN
  INSERT INTO Icmal VALUES (IFNULL(new.TemirZavodu,''), IFNULL(new.Status,''), 1) ON CONFLICT DO UPDATE SET Say = Say + 1; END;
CREATE TRIGGER IF NOT EXISTS TR_Icmal_U AFTER UPDATE OF TemirZavodu, Status, Silinib ON Texnikalar BEGIN
  UPDATE Icmal SET Say = Say - 1 WHERE old.Silinib = 0 AND Zavod = IFNULL(old.TemirZavodu,'') AND Status = IFNULL(old.Status,'');
  INSERT INTO Icmal SELECT IFNULL(new.TemirZavodu,''), IFNULL(new.Status,''), 1 WHERE new.Silinib = 0 ON CONFLICT DO UPDATE SET Say = Say + 1; END;

-- Dəyişiklik tarixçəsi (hər sahə dəyişikliyi bir sətir)
CREATE TABLE IF NOT EXISTS Tarixce (
  Id INTEGER PRIMARY KEY, TexnikaId INTEGER NOT NULL, Tarix INTEGER NOT NULL, Istifadeci TEXT NOT NULL,
  Sahe TEXT NOT NULL, Kohne TEXT, Yeni TEXT);
CREATE INDEX IF NOT EXISTS IX_Tarixce ON Tarixce(TexnikaId, Tarix DESC);

CREATE TABLE IF NOT EXISTS Loglar (
  Id INTEGER PRIMARY KEY, Tarix INTEGER NOT NULL, Istifadeci TEXT, Zavod TEXT, Nov TEXT NOT NULL, Mesaj TEXT NOT NULL);
CREATE INDEX IF NOT EXISTS IX_Loglar ON Loglar(Tarix DESC);
");
            // Axtarış cədvəli yenidən qurulubsa (keçid), mövcud sətirləri indeksə yaz
            using (var k = con.CreateCommand())
            {
                k.CommandText = "SELECT (SELECT COUNT(*) FROM TexnikaAxtar_docsize) = 0 AND EXISTS (SELECT 1 FROM Texnikalar);";
                if (Convert.ToInt64(k.ExecuteScalar()) == 1) LocalDb.Exec(con, "INSERT INTO TexnikaAxtar(TexnikaAxtar) VALUES('rebuild');");
            }
            SistemSchema.Yarat(con);
        }
    }
}
