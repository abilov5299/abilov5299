using Microsoft.Data.Sqlite;

namespace TexnikaReyestri.Data
{
    /// <summary>İstifadəçilər, qurumlar, bildirişlər, sinxronizasiya və parametrlər.</summary>
    internal static class SistemSchema
    {
        public static void Yarat(SqliteConnection con)
        {
            LocalDb.Exec(con, @"
-- Tip: 1 = Zavod, 2 = Üst qurum, 3 = Ən üst qurum
CREATE TABLE IF NOT EXISTS Qurumlar (
  Kod TEXT PRIMARY KEY, Ad TEXT NOT NULL, Tip INTEGER NOT NULL, UstKod TEXT);
INSERT OR IGNORE INTO Qurumlar VALUES ('ENUST','Ən üst qurum',3,NULL),('UST','Üst qurum',2,'ENUST'),
 ('Z1','Zavod 1',1,'UST'),('Z2','Zavod 2',1,'UST'),('Z3','Zavod 3',1,'UST'),('Z4','Zavod 4',1,'UST'),
 ('Z5','Zavod 5',1,'UST'),('Z6','Zavod 6',1,'UST'),('Z7','Zavod 7',1,'UST');

-- Rol: 'Zavod istifadəçisi' | 'Zavod rəhbəri' | 'Yalnız baxış'
CREATE TABLE IF NOT EXISTS Istifadeciler (
  Login TEXT PRIMARY KEY COLLATE NOCASE, AdSoyad TEXT NOT NULL, QurumKod TEXT NOT NULL REFERENCES Qurumlar(Kod),
  Rol TEXT NOT NULL, Hash BLOB NOT NULL, Duz BLOB NOT NULL, Aktiv INTEGER NOT NULL DEFAULT 1,
  YanlisCehd INTEGER NOT NULL DEFAULT 0, Muveqqeti INTEGER NOT NULL DEFAULT 0, ParolTarixi INTEGER, SonGiris INTEGER);

CREATE TABLE IF NOT EXISTS Bildirisler (
  Id INTEGER PRIMARY KEY, Acar TEXT UNIQUE, Nov TEXT NOT NULL, Basliq TEXT NOT NULL, Metn TEXT NOT NULL,
  Zavod TEXT, TexnikaId INTEGER, Tarix INTEGER NOT NULL, Oxunub INTEGER NOT NULL DEFAULT 0);
CREATE INDEX IF NOT EXISTS IX_Bildiris ON Bildirisler(Oxunub, Tarix DESC);

-- Hər paket: ixrac və ya idxal; qurum üzrə son ardıcıllıq nömrəsi təkrar idxalı bloklayır
CREATE TABLE IF NOT EXISTS SyncJurnal (
  Id INTEGER PRIMARY KEY, Tarix INTEGER NOT NULL, Emeliyyat TEXT NOT NULL, Qurum TEXT NOT NULL, Seq INTEGER NOT NULL,
  Fayl TEXT, Yeni INTEGER, Deyisen INTEGER, Silinen INTEGER, Netice TEXT NOT NULL, Qeyd TEXT);
CREATE INDEX IF NOT EXISTS IX_Sync ON SyncJurnal(Qurum, Emeliyyat, Seq);

-- Etibar edilən digər qurumların açıq açarları
CREATE TABLE IF NOT EXISTS Acarlar (
  QurumKod TEXT PRIMARY KEY, ImzaAcari BLOB NOT NULL, SifreAcari BLOB NOT NULL, BarmaqIzi TEXT NOT NULL, Elave INTEGER NOT NULL);

CREATE TABLE IF NOT EXISTS Parametrler (Ad TEXT PRIMARY KEY, Deyer TEXT);
INSERT OR IGNORE INTO Parametrler VALUES ('ParolMin','8'),('ParolMuddet','90'),('Blok','5'),('Fealiyyetsiz','15'),
 ('BildirisYaxin','3'),('BildirisGec','0'),('BildirisSorgu','30'),('BildirisPaket','7'),('NusxeSay','30'),('Animasiya','1');

CREATE TABLE IF NOT EXISTS AdminJurnal (Id INTEGER PRIMARY KEY, Tarix INTEGER NOT NULL, Kim TEXT NOT NULL, Emeliyyat TEXT NOT NULL);
");
        }
    }
}
