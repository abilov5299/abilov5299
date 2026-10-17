# Təmir Reyestri — layihə qeydləri

> Layihəni davam etdirmək üçün əsas sənəd (həm sizin, həm Claude üçün). Son yenilənmə: 17.10.2026.
> Claude Code bu faylı `CLAUDE.md` vasitəsilə avtomatik tapır.

## 1. Qısa məlumat

| | |
|---|---|
| Ad | **Təmir Reyestri** — təmir zavodlarına daxil olan texnikanın (texnika, minik maşını, bloklar) qəbulu, təmiri və təhvilinin uçotu |
| Platforma | C# WinForms, .NET 8 (`net8.0-windows`), Visual Studio 2022 17.13+ |
| Baza | SQLite, hər kompüterdə ayrıca (`C:\ProgramData\TexnikaReyestri\Data\reyestr.db`) |
| Məlumat ötürmə | Şifrəli USB paketi (`.tqx`): zavod → Xidmət (üst qurum) → İdarə (ən üst qurum) |
| Qurumlar | 6 zavod (939, 328, 329, 114, 412, 339 nömrəli hərbi hissə), "Təmir və Texniki Təminat xidməti", "Müdafiə Sənayesi İdarəsi" |
| Dil / tema | AZ / EN / RU; işıqlı / qaranlıq |
| Repo | `abilov5299/abilov5299`, branch `claude/trusting-gates-twb5gz` |
| Qovluq / namespace | Kodda köhnə ad qalır: `TexnikaReyestri` (yalnız ekranda "Təmir Reyestri") |

## 2. İş qaydası (istifadəçi ilə razılaşma)

1. **Əvvəlcə görünüş, sonra kod:** yeni ekran/dizayn dəyişikliyi əvvəl HTML nümunə ilə göstərilir, "okey"-dən sonra koda keçir. Sadə xəta düzəlişləri birbaşa edilir.
2. Cavablar qısa, cədvəl və nömrəli siyahı ilə; dəqiq bilinməyən məlumat uydurulmur.
3. Mövcud dizayn (kartlar, başlıqlar, rənglər) istifadəçi istəmədən **dəyişdirilmir**.
4. Kod: yalnız lazım olan hissə dəyişir, ətrafdakı stilə uyğun (Azərbaycanca adlar və şərhlər).
5. Hər dəyişiklikdən sonra build yoxlanılır, commit + push edilir.

## 3. Qəbul edilmiş qərarlar

| № | Qərar |
|---|---|
| 1 | 46 sahə kodda sabitdir (`Data/Saheler.cs`), seçimlər `Data/Secimler.cs`-də |
| 2 | Üst qurumlar texnika daxil etmir, yalnız baxır; İcmal yalnız üst qurumlarda |
| 3 | USB paketi: ECDH P-384 + AES-256-GCM + ECDSA imza + ardıcıllıq nömrəsi; zavod yalnız öz məlumatını göndərə bilər |
| 4 | **Status adları yalnız ekranda dəyişir** (`Secimler.Ekran/Baza`): bazada "Hissə gözləyir" → ekranda "Ehtiyat hissələrinin təminatı gözlənilir", "Təmirdə" → "Təmirdədir". `Lang.T`-yə qoyulmur (EditPage seçimi bazaya yazır) |
| 5 | Aktiv (təmiri davam edən) statuslar: Qəbul edilib, Təmirdə, Hissə gözləyir, **Qismən təmin edilib** (`Secimler.AktivStatus`, SQL-də `DavamSql`) |
| 6 | "Təmir tamamlanıb, təhvil gözlənilir" = Faktiki bitmə yazılıb, status hələ "Təhvil verilib" deyil (ayrıca status yoxdur) |
| 7 | İcra müddəti hədləri Admin → Sistem: qırmızı `BildirisYaxin` (0–N gün), sarı `IcraSari` ((N+1)–M gün) |
| 8 | Təhvil tarixi ayrıca sahə deyil: statusun "Təhvil verilib"ə dəyişdiyi an (Tarixce), yoxdursa Faktiki bitmə |
| 9 | Təchizat sorğusunun "Təminat nəticəsi" istifadəçi tərəfindən seçilir (avtomatik hesablanmır) |
| 10 | "Məni xatırla" yalnız istifadəçi adını saxlayır, parolu yox |
| 11 | Yan menyu həmişə bağlı açılır; loqoya klik ilə açılır (məzmunun üstündə, animasiya ilə) |
| 12 | Admin paneli gizlidir: giriş ekranında loqoya 3 s-də 5 klik və ya Ctrl+Shift+F12 |
| 13 | Proqramçı loqosu: giriş ekranının brend panelində sağ aşağı küncdə, solğun, yazısız (`ProqramciLoqo`) |

## 4. Bölmələr (sol menyu, `MainForm`)

| PageKey | Səhifə | Qeyd |
|---|---|---|
| `home` | HomePage | 6 kart (`KartPaneli`) + diaqram (qəbul / təmir tamamlanıb / təhvil). Zavod: diaqram + "Təhvil gözlənilən" və "Məlumat mübadiləsi" zolaqları. Üst qurum: "Zavodlar üzrə icra vəziyyəti" cədvəli + diaqram |
| `sum` | SummaryPage | İcmal (yalnız üst qurum) |
| `list` | RegistryPage | Təmir reyestri: 6 kart + filtr paneli (Zavod, Status, İcra müddəti, Məsul icraçı, Axtarış, Tarix növü/aralığı) |
| `add` | EditPage | Texnika daxil et; məsul icraçı avtomatik = istifadəçinin adı |
| `tech` | TechizatPage | Təchizat sorğuları: 5 kart, filtr, cədvəl, ehtiyat hissələri, Excel, PDF sənəd |
| `sadd` | SorguEditPage | Sorğu əlavə et / redaktə |
| `rep` | HesabatPage | 6 hesabat, Excel, PDF ("Təmir və təminat üzrə hesabat" + hesabatın adı) |
| `noti` | NotificationsPage | Bildirişlər |
| `sync` | SyncPage | USB ixrac / idxal |
| `set` | TenzimlemePage | Ehtiyat nüsxə + Loglar (nişanlarla) |
| `det` | DetailPage | Texnikanın detalları |

## 5. Qovluqlar

```
TexnikaReyestri.slnx
CLAUDE.md                  Claude Code üçün qısa giriş (bu fayla yönləndirir)
docs/LAYIHE.md             Bu fayl
TexnikaReyestri/
  Program.cs               Baza → ilk quraşdırma → giriş → əsas pəncərə
  Core/                    Theme, Lang (+Lang.Dict tərcümələr), Icons, Ui, Animator, Win32
  Controls/                Toolbox kontrolları: RoundPanel, FlatButton, NavButton, StatCard, KartPaneli, ModernGrid,
                           ColumnChart (Genis rejimi), StatusChip, Round* inputlar, Brand (LogoMark, BrandPanel, ProqramciLoqo, PageBase)
  Data/                    LocalDb, Schema, SistemSchema, Saheler, Secimler, Session,
                           TexnikaRepo (+.Saxla, +.Hesabat), SorguRepo, SistemRepo
  Services/                AuthService, KeyStore, SyncService, BackupService, ExcelService, PdfService, NotificationService, DemoData
  Forms/                   LoginForm, MainForm, FirstRunForm, AdminForm, ... 
  Pages/                   (bölmə 4) + Admin/ (Users, Orgs, Pc, Security, Keys, System, Journal)
  Assets/                  ikon faylları
```

Konvensiyalar: Label rəngi `Tag` ilə (`title`, `muted`, `danger`, `white`, `navtext`); forma `Load`-da `Ui.Hazirla(this)`; bazaya yazı `LocalDb.YazAsync`; uzun iş `Ui.Is(host, "mesaj", async ov => ...)`. Yeni səhifələr (Techizat, SorguEdit, Hesabat, Tenzimleme) Designer-siz, kodla qurulub.

## 6. Baza

- `Texnikalar` (46 sahə + Guid/Versiya/Deyisdi/Silinib), `TexnikaAxtar` (FTS5), `Icmal` (trigger), `Tarixce`, `Loglar`.
- `Sorgular`: məktub sahələri + `Texnikalar` (JSON: Guid, Marka, ZavodNo, Status) + `Hisseler` (JSON: Ad, Texnika, Vahid, Teleb, Qebul). USB paketində `"_Sorgu"` nişanı ilə gedir.
- Sistem: `Qurumlar`, `Istifadeciler`, `Bildirisler`, `SyncJurnal`, `Acarlar`, `Parametrler`, `AdminJurnal`.
- Miqrasiya `Schema.cs`-də: yeni sütunlar avtomatik `ALTER TABLE`; `IX_T_Gec` aktiv status siyahısı dəyişəndə yenidən yaradılır.

### Parametrlər (`Parametrler`, Admin → Sistem)

| Ad | Mənası | Standart |
|---|---|---|
| `BildirisYaxin` / `IcraSari` | İcra müddəti: qırmızı / sarı hədd (gün) | 3 / 20 |
| `BildirisGec`, `BildirisSorgu`, `BildirisPaket`, `NusxeSay` | Bildirişlər, ehtiyat nüsxə sayı | — |
| `Animasiya` | Səhifə keçid animasiyası | 1 |
| `GirisAnimasiya` | Giriş pəncərəsinin animasiya müddəti, ms (0 = yox) | 900 |
| `GirisSes` | Açılışda Windows səsi | 1 |
| `GirisXatirla` / `XatirlananLogin` | "Məni xatırla" icazəsi / saxlanmış ad | 1 / — |
| `ProqramciLoqo` | Proqramçı loqosunu göstər | 1 |
| `ParolMin`, `ParolMuddet`, `Blok`, `Fealiyyetsiz` | Təhlükəsizlik | 8, 90, 5, 15 |

## 7. Build və yoxlama

- Windows: `TexnikaReyestri.slnx` → Build → F5. Komanda sətri: `dotnet build TexnikaReyestri/TexnikaReyestri.csproj`.
- Sınaq məlumatı: Admin → Kompüter → "Nümunə məlumat yarat" və "Nümunə təchizat sorğuları yarat".
- Bulud mühitində yalnız kompilyasiya yoxlanılırdı (Linux); **görünüş Windows-da yoxlanılmalıdır.**

## 8. Son vəziyyət (harada qaldıq)

Son edilənlər (köhnədən yeniyə): 6 kart hər üç səhifədə → Reyestr filtr paneli → Təchizat sorğuları + Sorğu əlavə et → Hesabatlar → yeni Ana səhifə (diaqram 3 sütun, zolaqlar, zavodlar cədvəli) → ad "Təmir Reyestri" → Tənzimləmə bölməsi → giriş ekranı (animasiya, səs, məni xatırla, parol gözü, proqramçı loqosu) → sorğu PDF sənədi.

### Windows-da yoxlanılmalı (istifadəçi hələ təsdiqləməyib)

- [ ] Yan menyunun hamar açılması və yazıların kəsilməməsi (`NavButton` ResizeRedraw düzəlişi)
- [ ] Giriş: "Unut" linkinin yeri, parol gözü xananın içində, animasiyanın hiss olunması
- [ ] Ana səhifə: zavodlar cədvəli (böyük şrift) + diaqram yan-yana; "1 il" seçiləndə ay adları sıxlaşırmı
- [ ] Sorğuya texnika əlavəsi (boş axtarış → siyahı)
- [ ] Hesabatlar və sorğu PDF-inin görünüşü

### Açıq təkliflər (istifadəçi qərar verməyib)

- [ ] Ayrıca "Təhvil tarixi" sahəsi (diaqram və hesabatda dəqiq təhvil sayı üçün)
- [ ] "Sorğuya texnika əlavə et" və "Hissələrin qəbulunu qeyd et" üçün ayrıca pəncərələr (indi hər ikisi redaktə formasını açır)
- [ ] Girişi kompüterin öz qurumu ilə məhdudlaşdırmaq; bir zavodda bir neçə kompüter
- [ ] Excel idxal pəncərəsinin dizaynı
- [ ] İstifadə təlimatı (köhnə .docx silindi, yeni ekranlarla yenidən hazırlanmalıdır)
- [ ] Quraşdırma faylı (`dotnet publish -r win-x64 --self-contained` + Inno Setup)

### Texniki borc (kiçik)

- `HomePage.Designer.cs`-də köhnə panellər (rpStatus, rpTypes, rpLate, rpEvents) qalır, kodda gizlədilir — Designer-dən silmək olar.
- `navBackup` düyməsi MainForm Designer-də qalır, gizlidir (Tənzimləmə əvəz edir).
- Kod qovluğu/namespace `TexnikaReyestri` adındadır; ekrandakı ad dəyişib.
