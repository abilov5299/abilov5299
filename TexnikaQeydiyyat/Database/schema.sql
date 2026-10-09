-- SQL Server Express. Serverdə bir dəfə icra edin.
IF DB_ID('TexnikaDb') IS NULL CREATE DATABASE TexnikaDb;
GO
USE TexnikaDb;
GO
-- Tip: 1 = Zavod, 2 = Üst qurum, 3 = Ən üst qurum
CREATE TABLE Qurumlar (
    Id          INT IDENTITY PRIMARY KEY,
    Ad          NVARCHAR(100) NOT NULL,
    Tip         TINYINT NOT NULL,
    UstQurumId  INT NULL REFERENCES Qurumlar(Id)
);
CREATE TABLE Istifadeciler (
    Id          INT IDENTITY PRIMARY KEY,
    Login       NVARCHAR(50) NOT NULL UNIQUE,
    ParolHash   VARBINARY(32) NOT NULL,
    ParolDuz    VARBINARY(16) NOT NULL,
    AdSoyad     NVARCHAR(100) NOT NULL,
    QurumId     INT NOT NULL REFERENCES Qurumlar(Id),
    Aktiv       BIT NOT NULL DEFAULT 1
);
CREATE TABLE Texnikalar (
    Id              INT IDENTITY PRIMARY KEY,
    ZavodId         INT NOT NULL REFERENCES Qurumlar(Id),
    Ad              NVARCHAR(150) NOT NULL,
    Model           NVARCHAR(100) NULL,
    SeriyaNo        NVARCHAR(100) NULL,
    QebulTarixi     DATE NOT NULL,
    Qeyd            NVARCHAR(500) NULL,
    YaradanId       INT NOT NULL REFERENCES Istifadeciler(Id),
    YaradilmaTarixi DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    Silinib         BIT NOT NULL DEFAULT 0
);
CREATE INDEX IX_Texnikalar_Zavod ON Texnikalar(ZavodId, Silinib);
-- Parametrlər açar-dəyər şəklindədir: şablon dəyişəndə cədvəl dəyişmir.
CREATE TABLE TexnikaParametrleri (
    Id        INT IDENTITY PRIMARY KEY,
    TexnikaId INT NOT NULL REFERENCES Texnikalar(Id) ON DELETE CASCADE,
    Ad        NVARCHAR(100) NOT NULL,
    Deyer     NVARCHAR(200) NULL
);
CREATE INDEX IX_Param_Texnika ON TexnikaParametrleri(TexnikaId);
CREATE TABLE Loglar (
    Id           BIGINT IDENTITY PRIMARY KEY,
    Tarix        DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    IstifadeciId INT NULL,
    ZavodId      INT NULL,
    Nov          NVARCHAR(30) NOT NULL,
    Mesaj        NVARCHAR(1000) NOT NULL
);
CREATE INDEX IX_Loglar_Tarix ON Loglar(Tarix DESC);
GO
INSERT INTO Qurumlar (Ad, Tip) VALUES (N'Ən üst qurum', 3);
INSERT INTO Qurumlar (Ad, Tip, UstQurumId) VALUES (N'Üst qurum', 2, 1);
INSERT INTO Qurumlar (Ad, Tip, UstQurumId) VALUES
 (N'Zavod 1',1,2),(N'Zavod 2',1,2),(N'Zavod 3',1,2),(N'Zavod 4',1,2),
 (N'Zavod 5',1,2),(N'Zavod 6',1,2),(N'Zavod 7',1,2);
-- İstifadəçilər proqramın ilk açılışında avtomatik yaradılır (DbSeeder).
