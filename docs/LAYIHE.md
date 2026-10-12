# Texnika Reyestri — layihə qeydləri

> Bu fayl layihəni davam etdirmək üçündür: həm sizin üçün, həm də gələcək söhbətlərdə Claude üçün.
> Yeni söhbətə başlayanda yazın: **"docs/LAYIHE.md faylını oxu və layihəni davam etdirək"**.

## 1. Qısa məlumat

| | |
|---|---|
| Ad | **Texnika Reyestri** — təmir zavodları üzrə texnikanın qəbulu, təmiri və təhvilinin uçotu |
| Platforma | C# WinForms, .NET 8 (`net8.0-windows`), Visual Studio 2022 17.13+ |
| Baza | SQLite, hər kompüterdə ayrıca (`C:\ProgramData\TexnikaReyestri\Data\reyestr.db`) |
| Məlumat ötürmə | Şifrəli USB paketi (`.tqx`): zavod → üst qurum → ən üst qurum |
| Dil / tema | AZ / EN / RU; işıqlı / qaranlıq |
| Repo | `abilov5299/abilov5299`, branch `claude/trusting-gates-twb5gz` |

## 2. Qəbul edilmiş qərarlar (dəyişməzdən əvvəl bunları nəzərə alın)

| № | Qərar | Səbəb |
|---|---|---|
| 1 | 46 sütun **kodda sabitdir** (`Data/Saheler.cs`), istifadəçi sahə əlavə etmir | 7 zavodun cədvəli eyni qalsın, hesabatlar qarışmasın |
| 2 | Seçim siyahıları kodda (`Data/Secimler.cs`) | Eyni səbəb |
| 3 | Üst qurumlar **yalnız baxır** | Tələb |
| 4 | Hər kompüterin öz bazası, şəbəkə yoxdur | Tələb; təhlükə USB-dəki məlumatdır, kompüter yox |
| 5 | USB paketi: ECDH P-384 + AES-256-GCM + ECDSA P-384 imza + ardıcıllıq nömrəsi | Paket itsə açılmasın, dəyişdirilə və təkrar idxal edilə bilməsin |
| 6 | Əlavə qorumalar (idxal parolu, BitLocker, padding) **edilmədi** | İstifadəçinin qərarı: mövcud qoruma kifayətdir |
| 7 | Sənəd/skan əlavəsi **edilmədi** | İstifadəçinin qərarı |
| 8 | "Sahələr" menyusu **yoxdur** | İstifadəçinin qərarı |
| 9 | Dizayn: V4 (dar ikon menyu + statistika kartları), loqo L2 (qalxan + T) | İstifadəçinin seçimi |
| 10 | Giriş ekranında mətn: "TEXNİKA REYESTRİ" + "Təmir zavodları üzrə texnikanın qəbulu, təmiri və təhvilinin uçotu" | İstifadəçinin seçimi |
| 11 | Admin paneli gizlidir: loqoya 3 s-də 5 klik və ya Ctrl+Shift+F12 + admin parolu | Tələb |
| 12 | Skrol zolaqları gizlidir (təkərlə sürüşür) | Tələb |
| 13 | UI Designer-də dəyişdirilə bilən olmalıdır (Toolbox) | Tələb |

## 3. Qovluqlar

```
TexnikaReyestri.slnx
TexnikaReyestri/
  Program.cs              Başlanğıc: baza → ilk quraşdırma → giriş → əsas pəncərə (çıxışdan sonra yenə giriş)
  Core/                   Theme (rənglər), Lang (+Lang.Dict tərcümələr), Icons, Ui (köməkçilər), Animator, WheelRouter, Win32
  Controls/               Xüsusi Toolbox kontrolları (aşağıda)
  Data/                   LocalDb, Schema, SistemSchema, Saheler, Secimler, Session, TexnikaRepo(+.Saxla), SistemRepo
  Services/               AuthService, KeyStore, SyncService, BackupService, ExcelService, PdfService, NotificationService, DemoData, AppPaths
  Forms/                  LoginForm, MainForm, FirstRunForm, AdminLoginForm, AdminForm, ChangePasswordForm, ConfirmDialog,
                          UserEditForm, NotificationPopup, ExcelImportForm, ExcelExportForm
  Pages/                  HomePage, RegistryPage, DetailPage, EditPage, NotificationsPage, SyncPage, SummaryPage, BackupPage, LogsPage
  Pages/Admin/            AdmUsersPage, AdmOrgsPage, AdmPcPage, AdmSecurityPage, AdmKeysPage, AdmSystemPage, AdmJournalPage
  Assets/                 TexnikaReyestri.ico / .svg / _256.png
docs/
  Istifade_Telimati.docx  İstifadə təlimatı (şəkilli)
  prototip.html           Brauzerdə açılan klikləməli prototip (dizaynın mənbəyi)
  excel-idxal-ixrac.html, loqo-variantlari.html
  LAYIHE.md               Bu fayl
```

### Xüsusi kontrollar (Toolbox → "TexnikaReyestri Components")

| Kontrol | Əsas xassələr |
|---|---|
| `RoundPanel` | CornerRadius, ShadowVisible, BorderVisible, Fill (Card/Alt/Nav) |
| `FlatButton` | Kind (Primary/Secondary/Danger/Ghost), Glyph, CornerRadius |
| `NavButton` | Glyph, Active, Badge, ToolTipText, PageKey, DangerStyle |
| `StatCard` | Title, Value, SubText, Glyph, Accent, FilterKey |
| `StatusChip` | Text (rəng mətnə görə avtomatik) |
| `StepperControl` | Steps, CurrentStep |
| `StepList` | Items, Current, Fail() |
| `BarList`, `ColumnChart`, `StatusBreakdown` | SetItems / SetData (kodla) |
| `ModernGrid` | DataGridView: gizli skrol, "Status" adlı sütunda nişan, düymə sütunu |
| `SegmentControl` | Items, SelectedIndex |
| `ScrollPanel` | zolaqsız sürüşən panel |
| `LogoMark`, `BrandPanel` | loqo və giriş ekranının sol paneli |
| `PageBase` | bütün səhifələrin əsas sinfi (fon kölgəsi + su nişanı), `OnShownAsync()` |

### Konvensiyalar
- Adlar Azərbaycan dilindədir (`Saxla`, `Yukle`, `Session.ZavodFiltri`).
- Label rəngi `Tag` ilə: `title`, `muted`, `danger`, `white`, `navtext`; panel fonu: `card`, `alt`, `nav`.
- Designer-də mətni **Azərbaycan dilində** yazın; tərcüməni `Core/Lang.Dict.cs`-ə əlavə edin.
- Hər forma `Load`-da `Ui.Hazirla(this)` çağırır (tema + dil).
- Bazaya hər yazı `LocalDb.YazAsync` ilə (kilid + tranzaksiya). UI thread-də uzun iş yoxdur: `Ui.Is(host, "mesaj", async ov => ...)`.

## 4. Baza

- `Texnikalar`: `Id`, `Guid` (sinxronizasiya açarı), `Versiya`, `Deyisdi` (unix ms), `Silinib` + 46 sahə sütunu.
- İndekslər: siyahı (`Silinib, TemirZavodu, Status, IFNULL(GozlenilenBitme,''), Id`), bitmə tarixi, zavod nömrəsi, dəyişmə vaxtı, daxilolma, faktiki bitmə.
- `TexnikaAxtar` (FTS5): marka, zavod nömrəsi, lülə nömrəsi, qeyd — trigger-lərlə avtomatik.
- `Icmal` (zavod × status sayı) — trigger-lərlə avtomatik; ana səhifə və "Cəmi" buradan oxuyur.
- `Tarixce`, `Loglar`, `Qurumlar`, `Istifadeciler`, `Bildirisler`, `SyncJurnal`, `Acarlar`, `Parametrler`, `AdminJurnal`.
- Yeni sütun: `Saheler.cs`-ə əlavə olunanda baza açılışda `ALTER TABLE ADD COLUMN` edir (köhnə məlumat qalır).

### Ölçülmüş sürət (1 000 000 sətir, server kompüteri; ofis kompüterində 2–5 dəfə yavaş ola bilər)

| Əməliyyat | Müddət |
|---|---|
| Reyestrin səhifəsi (200 sətir), dərin səhifə daxil | 0.2–0.6 ms |
| Mətn axtarışı (FTS5) | ~6 ms |
| 20 000 sətrin yazılması | ~2.4 s |
| 20 000 sətirlik USB ixracı / idxalı | ~1.6 s / ~2.9 s |

## 5. Necə edilir

### Yeni sahə (sütun) əlavə etmək
1. `Data/Saheler.cs` → `Hamisi` siyahısına sətir: `new("Kod", "Ad", SaheNov.Metn, "Qrup")`.
2. `Pages/EditPage` Designer → uyğun tabda yeni Panel; içində Label (`lbl_Kod`) və kontrol (`in_Kod`: TextBox / ComboBox / DateTimePicker).
3. `Pages/DetailPage` Designer → uyğun kartın TableLayoutPanel-ində yeni sətir: `cap_Kod` və `val_Kod` Label-ları.
4. Seçim sahəsidirsə: `Data/Secimler.cs` → `Sahe["Kod"] = new[] { ... }`. Tərcümə: `Core/Lang.Dict.cs`.

### Yeni səhifə
1. Pages qovluğunda Add → User Control; sinfi `: PageBase` edin; `OnShownAsync()`-u override edin.
2. `MainForm` Designer → `pnlNav`-a `NavButton` (PageKey = "acar"), `MainForm.AcAsync`-dakı `switch`-ə əlavə edin.

### Rəngləri dəyişmək
`Core/Theme.cs` → `Set()`: işıqlı və qaranlıq rənglər.

## 6. Yoxlanılanlar və yoxlanılmayanlar

| Yoxlanılıb | Necə |
|---|---|
| Bütün C# kodu kompilyasiya olunur (0 xəta) | Linux-da WinForms reference assembly-ləri ilə |
| Giriş, blok, keyset səhifələmə, axtarış, statistika, tarixçə, bildirişlər, ehtiyat nüsxə | Avtomatik sınaq (22 yoxlama, hamısı keçdi) |
| USB: şifrələmə, dəyişdirilmiş paketin rəddi, təkrar idxalın bloku, delta ixrac | Eyni sınaq, iki "kompüter" ilə |
| Excel idxalı istifadəçinin real faylı ilə (46/46 sütun avtomatik), ixrac → yenidən idxal | Eyni sınaq |
| PDF pasport (Azərbaycan və kiril hərfləri) | Eyni sınaq |

| **Yoxlanılmayıb** (Windows tələb edir) | Nə etməli |
|---|---|
| Proqramın Windows-da işə düşməsi və görünüşü | F5 ilə açıb hər səhifəyə baxın; problem olsa ekran şəklini göndərin |
| Formaların Visual Studio Designer-də açılması | Hər formanı bir dəfə açın |
| Yüksək DPI (125%, 150%) ekranlarda görünüş | Windows ekran miqyasını dəyişib baxın |
| DPAPI ilə açar qorunması, USB disklərin tanınması | Real USB ilə ixrac/idxal edin |

## 7. Açıq məsələlər / sonrakı addımlar

- [ ] Windows-da ilk işə salma və ekranların prototiplə müqayisəsi (görünüş düzəlişləri).
- [ ] İstifadə təlimatındakı şəkilləri real proqramın ekran şəkilləri ilə əvəz etmək.
- [ ] Quraşdırma faylı (installer) — məsələn, `dotnet publish -r win-x64 --self-contained` + Inno Setup.
- [ ] Parolun təkrar istifadəsinə qadağa (keçmiş parollar) — hələ yoxdur.
- [ ] Çox böyük Excel fayllarının (500 000+ sətir) idxalında yaddaş istifadəsinin yoxlanması.

## Dəyişikliklər — UI düzəlişləri (Windows sınağından sonra)
- **Yumru pəncərə + kölgə:** bütün çərçivəsiz formalarda `Win32.Yumru` (Win11 DWM, Win10 Region) + `CS_DROPSHADOW`.
- **CaptionButton** (Toolbox): kiçilt / böyüt⇄bərpa / bağla — xətlərlə çəkilir, şriftdən asılı deyil. MainForm-da istifadə olunur.
- **Səhifə keçidi:** `Animator` artıq canlı kontrolları sürüşdürmür; yeni səhifənin şəkli sürüşür (≈140 ms). `PageBase` fonu bitmap-də keşlənir, `RoundPanel` valideyni yenidən çəkmir (`Gfx.ParentBack`).
- **EditPage:** `PageTabs` (başlıqsız TabControl, Designer-də başlıq görünür) + `ChipTabBar` (bütün 9 qrup sətirlərə bölünərək görünür).
- **RoundTextBox / RoundComboBox / RoundDatePicker** (Toolbox): bütün Designer fayllarında adi TextBox/ComboBox/DateTimePicker əvəzinə.
- **10 nümunə texnika:** baza boşdursa ilk girişdə avtomatik (`Parametrler.Numune10`), həmçinin Admin → PC → say = 10.
- Qeyd: `.Designer.cs` faylları artıq əl ilə/VS Designer-də düzəldilir; köhnə Python generatoru bu dəyişiklikləri bilmir — yenidən işə salmayın.

## Qurum adları
- Zavodların və üst qurumların adları `Data/Secimler.cs` → `Zavodlar`, `UstAd`, `EnUstAd`. Kodlar: Z1…Z6, UST, ENUST.
- Proqram açılanda `SistemSchema.Qurumlar` standart köhnə adları ("Zavod N", "Üst qurum", "Ən üst qurum") yeni adlara keçirir və texnikaların `TemirZavodu` sahəsini yeniləyir. Admin əl ilə verdiyi adlara toxunulmur.
- Texnikada zavodun **adı** saxlanılır: adı sonradan dəyişmək üçün 9 kompüterin hamısında eyni versiya olmalıdır.

## Qurum adları
- Zavodların və üst qurumların adları `Data/Secimler.cs` → `Zavodlar`, `UstAd`, `EnUstAd`. Kodlar: Z1…Z6, UST, ENUST.
- Proqram açılanda `SistemSchema.Qurumlar` standart köhnə adları ("Zavod N", "Üst qurum", "Ən üst qurum") yeni adlara keçirir və texnikaların `TemirZavodu` sahəsini yeniləyir. Admin əl ilə verdiyi adlara toxunulmur.
- Texnikada zavodun **adı** saxlanılır: adı sonradan dəyişmək üçün 9 kompüterin hamısında eyni versiya olmalıdır.
