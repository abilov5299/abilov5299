# ModernMessageBox — istifadə qaydası

Standart `MessageBox`-un müasir görünüşlü əvəzi. Tək fayldır (`ModernMessageBox.cs`), heç bir əlavə kitabxana tələb etmir.
Bütün `MessageBox.Show` imzaları, düymələri, nişanları və seçimləri eyni işləyir.

## 1. Quraşdırma
1. `ModernMessageBox.cs` faylını layihənizin qovluğuna kopyalayın. Visual Studio-da faylı layihəyə əlavə edin: **Add → Existing Item**.
2. Layihə WinForms olmalıdır (.NET 6 və ya 8, `<UseWindowsForms>true</UseWindowsForms>`).
3. İstifadə etdiyiniz faylın yuxarısına bu sətri yazın:
   ```csharp
   using ModernUI;
   ```

## 2. Köhnə kodu dəyişmək
`MessageBox.Show(` yazılan yerləri `ModernMessageBox.Show(` ilə əvəz edin, parametrlər eyni qalır.
Visual Studio-da hamısını birdən dəyişmək üçün **Ctrl+Shift+H** basın:
- **Find:** `MessageBox.Show(`
- **Replace:** `ModernMessageBox.Show(`
- **Look in:** Entire solution

## 3. Nümunələr
```csharp
// Sadə məlumat
ModernMessageBox.Show("Yadda saxlanıldı");
ModernMessageBox.Show("Yadda saxlanıldı", "Məlumat");

// Nişan ilə
ModernMessageBox.Show("Fayl tapılmadı.", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);

// Bəli / Xeyr sualı
if (ModernMessageBox.Show("Silinsin?", "Təsdiq", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
{
    // sil
}

// Üç seçim
switch (ModernMessageBox.Show(this, "Dəyişikliklər saxlansın?", "Diqqət", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
{
    case DialogResult.Yes: /* saxla */ break;
    case DialogResult.No: /* saxlama */ break;
    case DialogResult.Cancel: /* geri qayıt */ break;
}

// Defolt düymə: Enter "Xeyr"-i basır (təhlükəli əməliyyatlar üçün)
ModernMessageBox.Show("Bütün qeydlər silinsin?", "Silmə", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

// Kömək düyməsi (forma-nın HelpRequested hadisəsini çağırır)
ModernMessageBox.Show(this, "Ətraflı?", "Kömək", MessageBoxButtons.OK, MessageBoxIcon.Information,
                      MessageBoxDefaultButton.Button1, 0, true);

// Kömək faylı (.chm)
ModernMessageBox.Show(this, "Ətraflı?", "Kömək", MessageBoxButtons.OK, MessageBoxIcon.Information,
                      MessageBoxDefaultButton.Button1, 0, "help.chm", HelpNavigator.Topic, "giris.htm");
```

`this` (owner) verilsə, pəncərə həmin formanın ortasında açılır. Verilməsə, aktiv formanın üstündə, aktiv forma yoxdursa ekranın ortasında açılır.

## 4. Parametrlər

| Parametr | Dəyərlər | Nə edir |
|---|---|---|
| `buttons` | `OK`, `OKCancel`, `YesNo`, `YesNoCancel`, `RetryCancel`, `AbortRetryIgnore`, `CancelTryContinue` | Düymə dəsti |
| `icon` | `None`, `Information` (=`Asterisk`), `Question`, `Warning` (=`Exclamation`), `Error` (=`Hand`, `Stop`) | Nişan və Windows səsi. Warning/Error olanda və birdən çox düymə varsa, defolt düymə qırmızı olur |
| `defaultButton` | `Button1` … `Button4` | Enter basılanda seçilən düymə. Əsas rəngdə göstərilir. `Button4` Kömək düyməsidir |
| `options` | `RightAlign` | Mətn sağa düzlənir |
| | `RtlReading` | Sağdan sola: nişan sağda, düymələr tərsinə |
| | `DefaultDesktopOnly`, `ServiceNotification` | Pəncərə bütün pəncərələrin üstündə açılır. Bu seçimlərlə `owner` vermək olmaz (standartdakı kimi xəta verir) |
| `displayHelpButton` | `true` | "Kömək" düyməsi; basılanda (və ya F1) owner formanın `HelpRequested` hadisəsi işə düşür |
| `helpFilePath`, `keyword`, `navigator`, `param` | | "Kömək" düyməsi `Help.ShowHelp` ilə kömək faylını açır |

**Qaytarılan nəticə** (`DialogResult`): `OK`, `Cancel`, `Yes`, `No`, `Retry`, `Abort`, `Ignore`, `TryAgain`, `Continue`.

## 5. Klaviatura

| Düymə | Nə edir |
|---|---|
| Enter / Boşluq | Seçilmiş düymə |
| Esc, Alt+F4 | `Cancel` (varsa). Yalnız OK varsa OK. YesNo və AbortRetryIgnore-da pəncərə bağlanmır (standart kimi) |
| ← → , Tab | Düymələr arasında keçid |
| Ctrl+C | Başlıq, mətn və düymələr standart formatda buferə kopyalanır |
| F1 | Kömək (Kömək düyməsi varsa) |

Pəncərəni başlığından tutub sürüşdürmək olar. Çox uzun mətndə sürüşdürmə zolağı çıxır.

## 6. Görünüşü dəyişmək
Proqram açılanda bir dəfə yazın (məsələn, `Program.cs`-də `Application.Run`-dan əvvəl):
```csharp
ModernMessageBox.Primary = Color.FromArgb(0, 120, 215);   // əsas düymə rəngi
ModernMessageBox.Danger = Color.Firebrick;                 // təhlükəli təsdiq
ModernMessageBox.TitleFont = "Segoe UI Semibold";
ModernMessageBox.Width = 480;                              // pəncərənin eni
ModernMessageBox.PlaySound = false;                        // səssiz

// Düymə adları (başqa dil üçün)
ModernMessageBox.ButtonText[DialogResult.Yes] = "Yes";
ModernMessageBox.ButtonText[DialogResult.No] = "No";
ModernMessageBox.ButtonText[DialogResult.Cancel] = "Cancel";
ModernMessageBox.HelpText = "Help";
```
Digər rənglər: `Secondary`, `SecondaryText`, `Card` (fon), `Border` (çərçivə), `TitleColor`, `TextColor`, `TextFont`.

## 7. Qeydlər
- Windows 11-də künclər yumru olur (sistem özü çəkir), Windows 10-da forma yumru kəsilir.
- `[STAThread]` olmayan proqramda da Ctrl+C işləyir.
- Fərqli axından (background thread) çağırmayın, standart `MessageBox` kimi UI axınından çağırın. Lazım olsa `Invoke(() => ModernMessageBox.Show(...))` yazın.
