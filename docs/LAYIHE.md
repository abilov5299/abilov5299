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
| 2 | Üst qurumlar texnika daxil etmir, yalnız baxır (İcmal bölməsi 19.10-da ləğv edildi: Reyestr eyni işi görür) |
| 3 | USB paketi: ECDH P-384 + AES-256-GCM + ECDSA imza + ardıcıllıq nömrəsi; zavod yalnız öz məlumatını göndərə bilər |
| 4 | **Status adları yalnız ekranda dəyişir** (`Secimler.Ekran/Baza`): bazada "Hissə gözləyir" → ekranda "Ehtiyat hissələrinin təminatı gözlənilir", "Təmirdə" → "Təmirdədir". `Lang.T`-yə qoyulmur (EditPage seçimi bazaya yazır) |
| 5 | Aktiv (təmiri davam edən) statuslar: Qəbul edilib, Təmirdə, Hissə gözləyir, **Qismən təmin edilib** (`Secimler.AktivStatus`, SQL-də `DavamSql`) |
| 6 | "Təmir tamamlanıb, təhvil gözlənilir" = Faktiki bitmə yazılıb, status hələ "Təhvil verilib" deyil (ayrıca status yoxdur) |
| 7 | İcra müddəti hədləri Admin → Sistem: qırmızı `BildirisYaxin` (0–N gün), sarı `IcraSari` ((N+1)–M gün) |
| 8 | Təhvil tarixi ayrıca sahə deyil: statusun "Təhvil verilib"ə dəyişdiyi an (Tarixce), yoxdursa Faktiki bitmə |
| 9 | Təchizat sorğusunun "Təminat nəticəsi" istifadəçi tərəfindən seçilir (avtomatik hesablanmır) |
| 10 | "Məni xatırla" yalnız istifadəçi adını saxlayır, parolu yox |
| 11 | Yan menyu həmişə bağlı açılır; loqoya klik ilə açılır. Açıq menyu səhifəni sıxır (altında qalmır); animasiya üstdə gedir, səhifə yalnız sonda bir dəfə düzülür |
| 12 | Admin paneli gizlidir: giriş ekranında loqoya 3 s-də 5 klik və ya Ctrl+Shift+F12 |
| 13 | Proqramçı loqosu: giriş ekranının brend panelində sağ aşağı küncdə, solğun, yazısız (`ProqramciLoqo`) |

## 4. Bölmələr (sol menyu, `MainForm`)

| PageKey | Səhifə | Qeyd |
|---|---|---|
| `home` | HomePage | 6 kart (`KartPaneli`) + diaqram (qəbul / təmir tamamlanıb / təhvil). Zavod: diaqram + "Təhvil gözlənilən" və "Məlumat mübadiləsi" zolaqları. Üst qurum: "Zavodlar üzrə icra vəziyyəti" cədvəli + diaqram |
| `list` | RegistryPage | Təmir reyestri: 6 kart + filtr paneli (Zavod, Status, İcra müddəti, Məsul icraçı, Axtarış, Tarix növü/aralığı) |
| `add` | EditPage | Texnika daxil et; məsul icraçı avtomatik = istifadəçinin adı |
| `tech` | TechizatPage | Təchizat sorğuları: 5 kart, filtr, cədvəl, ehtiyat hissələri, Excel, PDF sənəd |
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

Konvensiyalar: Label rəngi `Tag` ilə (`title`, `muted`, `danger`, `white`, `navtext`); forma `Load`-da `Ui.Hazirla(this)`; bazaya yazı `LocalDb.YazAsync`; uzun iş `Ui.Is(host, "mesaj", async ov => ...)`. Yeni səhifələr (Techizat, Hesabat, Tenzimleme) və EditPage-in 5 bölməsi Designer-siz, kodla qurulub.

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

Son edilənlər (köhnədən yeniyə): 6 kart hər üç səhifədə → Reyestr filtr paneli → Təchizat sorğuları + Sorğu əlavə et → Hesabatlar → yeni Ana səhifə (diaqram 3 sütun, zolaqlar, zavodlar cədvəli) → ad "Təmir Reyestri" → Tənzimləmə bölməsi → giriş ekranı (animasiya, səs, məni xatırla, parol gözü, proqramçı loqosu) → sorğu PDF sənədi → tam dil tərcüməsi → İcmal bölməsi ləğv edildi (19.10) → Təchizat kartlarının sırası və adları dəyişdi → Reyestr cədvəli 4 sütun: Texnika / inventar kodu, Zavod, İcra statusu, Məsul icraçı (19.10). → **Yeni 5 bölməli "Texnika əlavə et" forması və baza quruluşu (20.10)** — tələblər: `docs/YENI_FORMA.md`, maket: `docs/maket/texnika_elave.html`.

**28.10:** Proqramın adı "Təmir və Texniki Təminat Xidməti · Məlumat bazası". "Texnika əlavə et" formasının 4-cü bölməsi çıxarıldı (indi 4 bölmə). Menyuda yeni **"Sorğu yarat"** səhifəsi (`Pages/SorguYaratPage.cs`): 1) məktub yaradılır/seçilir → 2) texnikaya bağlanır → 3) seçilmiş texnikanın ehtiyat hissələri və qəbulu → 4) icra qeydləri; hər addım dərhal yazılır. Təchizat sorğularındakı "Sorğunu aç" düyməsi bu səhifəni açır. Nümunə: `docs/numune-sorgu-yarat.html`. "Texnika seç" pəncərəsi zavodun bütün texnikalarını göstərir (əvvəl 30 ilə məhdud idi), sayı başlıqdadır.

**28.10 (təminat və sərfiyyat — yeni məntiq):** Nümunə: `docs/numune-teminat-serfiyyat.html`.
- **Sorğu yarat:** məktub → hissələr **məktuba** yazılır (`Sorgular.Hisseler`: ad, kod, vahid, tələb, qəbullar, "hansı texnika üçün" — yalnız məlumat, texnika bağlanmır, statusu/təminatı dəyişmir) → qəbul (zavodun anbarına düşür) → icra qeydləri. Məktubun nəticəsi öz hissələrindən hesablanır.
- **Sorğular üzrə təminat** (`Pages/TeminatPage.cs`, köhnə "Təchizat sorğuları" səhifəsini əvəz edir): hər sətir məktubun hissəsi — tələb, qəbul, texnikaya sərf, anbarda qalıq. Filtr: zavod, məktub, vəziyyət, tarix, təmin edən, axtarış; Excel/PDF, məktub sənədi.
- **Anbar** zavod üzrə ümumidir (ad+kod+vahid); sərf ilk qəbul tarixinə görə paylanır (`SorguRepo.TeminatAsync/AnbarAsync`).
- **Texnika forması 5 bölmə:** "4. Faktiki sərfiyyat" (`Texnikalar.Serfiyyat` JSON): anbardan seçim (qalıqdan çox olmaz) və ya "Yeni ad" (anbardan çıxılmır); yadda saxlanmış qeyd silinmir. Geniş baxışda "Faktiki sərfiyyat" kartı.
- **Status "Təminat gözləyir"** (yeni bazadakı dəyər, əllə seçilir): aktiv statuslar siyahısına daxildir; ana səhifədə "hissə gözləyən" sayı bu statusdan.
- **Reyestr:** köhnə "Ehtiyat hissələrinin təminatı" siyahısı əvəzinə "Təminat gözləyir" çek qutusu (Status="Təminat gözləyir"); cədvəlin təminat sütunu da bu statusu göstərir.
- **Hesabat "Texnika üzrə faktiki material sərfi"** (nov 5): texnika/material axtarışı, sərf tarixi = dövr. "Ehtiyat hissələri üzrə təminat" məktubun hissələrindən.
- **Köçürmə** (`Miqrasiya.HisseMektuba`, bir dəfə, `Miqrasiyalar` cədvəli): texnikadakı köhnə hissələr ona bağlı ən son məktuba keçir; texnikadakı siyahı silinmir.

**28.10 (hesabat):** "Ehtiyat hissələri üzrə təminat" hesabatı hər ehtiyat hissəsi üzrə sətirlərdir (məktub, texnika, ad/kod, vahid, tələb, qəbul, qalıq, təmin edən). İki keçid: "Təmin edilməmiş" (ilkin; qalığı > 0) və "Tam təmin edilmiş" (qalıq 0 olanda avtomatik keçir). Çap və Excel başlığında seçilmiş siyahının adı yazılır; PDF-də bu hesabatın sütun enləri genişdir.

**27.10:** Mövcud texnikada yadda saxlanmış məktubu ayırmaq və ehtiyat hissəsini silmək olmaz (xəbərdarlıq çıxır); yeni texnikada saxlanana qədər mümkündür.

**20.10 dəyişikliyi qısaca:**
- `Saheler.cs` yeni sahələrlə; köhnə sahələr (pasport, silah sistemi, texniki vəziyyət, mərhələ...) bazadan silinir. Bir dəfəlik keçid `Data/Miqrasiya.cs`: əvvəlcə bazanın yanına `reyestr_kohne_<tarix>.db` nüsxəsi, sonra köçürmə (km/motosaat/atış → göstəricilər, V* sahələri → nasazlıqlar, köhnə sorğu hissələri → texnikanın hissələri), cədvəl yenidən qurulur, VACUUM. 85 804 sətirlik bazanın nüsxəsində ~9 s, `integrity_check` = ok.
- Statuslar: Qəbul edilib · Təmirdə (ekranda "Təmirdədir") · Təmir tamamlanıb, təhvil gözlənilir · Təhvil verilib · Təmirə yararsız. "Hissə gözləyir"/"Qismən təmin edilib" → Təmirdə. Tamamlanma/təhvil statusu tarixlərdən avtomatik.
- Göstəricilər və ehtiyat hissələri texnikada JSON (`Gostericiler`, `Hisseler`, `TexnikaJson.cs`); `Teminat` avtomatik (Təminat gözlənilir / Qismən / Tam). Plan bitmə = diaqnostika + müddət (gün/ay), yeni plan yazılıbsa o.
- Məktub = `Sorgular` sətri (texnikalar JSON-da); yeni sahələr `Telefon`, `Qeydler` (icra qeydləri). Məktubun hissələri və nəticəsi texnika yadda saxlananda texnikalardan yığılır. "Sorğu əlavə et" bölməsi ləğv edildi; Təchizat siyahısından texnika 4-cü bölmədə açılır.
- Reyestr: "Ehtiyat hissələrinin təminatı" sütunu (çip) və filtri; Excel ixracında da var.
- Redaktə ayrıca səhifədə açılır (`red`, menyuda Reyestr seçili qalır); "Texnika əlavə et" həmişə öz boş formasıdır (23.10).
- Ekran şəkilləri ilə yoxlandı (nüsxə bazada): forma 5 bölmə, "Geniş bax", reyestr. **İstifadəçi testi gözlənilir.**

### Windows-da yoxlanılmalı (istifadəçi hələ təsdiqləməyib)

- [x] Yan menyu: açılış və bağlanma normal (17.10, təsdiqləndi); bağlanma üçün bağlanarkən altdakı səhifənin şəkli "pərdə" kimi göstərilir (`PerdeGoster`)
- [x] Yan menyu açılanda səhifə sıxılır (17.10, `MezmunSol`, təsdiqləndi)
- [ ] Giriş: "Unut" linkinin yeri, parol gözü xananın içində, animasiyanın hiss olunması
- [ ] Ana səhifə: "1 il" seçiləndə ay adları sıxlaşırmı
- [x] Ana səhifə: zavodlar cədvəli + diaqram yan-yana (təsdiqləndi)
- [x] Sorğuya texnika əlavəsi (təsdiqləndi)
- [x] Hesabatlar və sorğu PDF-i (hələlik normal)

- [x] Dil (17.10): lüğətə ~390 mətn (EN/RU); placeholder, cədvəl xanaları (`ModernGrid.OnCellFormatting`, yalnız oxunan xanalar), dil dəyişəndə aktiv səhifə yenidən yüklənir. EN/RU ekranda yoxlandı. Tərcümə olunmur: PDF/Excel sənədləri, jurnal/log mətnləri (bazada Az)
- [x] Yan menyu: bölmə seçiləndə açıq qalır, yalnız loqo bağlayır
- [ ] Yan menyu animasiyası hələ hamar deyil: səhifə sürüşəndə addım ~100 ms (səbəb: şəffaf panellərin yenidən çəkilməsi). Yeni yanaşma lazımdır

### Açıq təkliflər (istifadəçi qərar verməyib)

- [x] Ayrıca "Təhvil tarixi" sahəsi (20.10, `TehvilTarixi`)
- [ ] Girişi kompüterin öz qurumu ilə məhdudlaşdırmaq; bir zavodda bir neçə kompüter
- [ ] Excel idxal pəncərəsinin dizaynı
- [x] İstifadə təlimatı (26.10): 3 təlimat (Zavod, Xidmət, İdarə) — `TexnikaReyestri/Telimat/*.html`, mənbə `docs/telimat/qur.py`, şəkillər `docs/telimat/sekil/`. Proqramda menyu → "Təlimat" (`TelimatPage`): istifadəçinin öz rolunun təlimatı proqramın içində açılır (WebView2; mühit yoxdursa brauzerdə). Ekran dəyişəndə şəkillər yenidən çəkilib `python docs/telimat/qur.py` işə salınır
- [ ] Quraşdırma faylı (`dotnet publish -r win-x64 --self-contained` + Inno Setup)

### Texniki borc (kiçik)

- Kod qovluğu/namespace `TexnikaReyestri` adındadır; ekrandakı ad dəyişib (istifadəçi qərarı: toxunulmur).
- 17.10: HomePage-dəki köhnə panellər (rpStatus, rpTypes, rpLate, rpEvents, lblFoot) və `navBackup` silindi.
- `EvStat` hələ də Statuslar/Novler/Gecikenler hesablayır, ana səhifədə istifadə olunmur.
