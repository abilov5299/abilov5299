# Yeni "Texnika əlavə et" forması — razılaşdırılmış tələblər

Mənbə: istifadəçinin 6 ekran maketi (2026-10-20). Kod hələ dəyişməyib.

## Forma: 5 bölmə (əlavə və düzəliş üçün eyni forma)
1. **Qəbul və sənədlər** — növ (Nəqliyyat vasitəsi / Zirehli texnika / Digər texnika), marka, zavod nömrəsi, kateqoriya (faktiki); naryad nömrəsi + tarixi, zavoda daxilolma tarixi, təhvil verən (göndərən qurum); akt forması (Forma 12 / Forma 12 (RAS)), nömrəsi, tarixi. Zavod və ilkin status ("Qəbul edilib") avtomatik.
   - Naryad təkrar ola bilər (bir naryadla bir neçə texnika gəlir) — xəbərdarlıq yoxdur.
2. **İstismar göstəriciləri və son təmir** — istehsal ili, istismara verilmə tarixi; göstəricilər siyahısı (Gediş km, İşləmə saat, Motosaat, Dövr, Atəş sayı, Tətbiq edilmir, Məlumat yoxdur) + miqdar, "Digər göstərici əlavə et"; son təmir: növ (Cari/Orta/Əsaslı), tamamlanma tarixi, aparan hissə, qeyd.
3. **Təmirin icrası** — nasazlıqların təsviri, diaqnostika tarixi; işin növü (Cari / Orta / Əsaslı / Nasazlıqların aradan qaldırılması); icra statusu; müddət + vahid (gün/ay); plan bitmə = diaqnostika + müddət (avtomatik); qalan/gecikən gün (avtomatik); gecikmə səbəbi, gözlənilən bitmə, yeni plan üzrə bitmə (ayrı-ayrı sahələr), ilkin plan tarixçədə qalır. Təminat vəziyyəti Təchizatdan avtomatik.
4. **Ehtiyat hissələri və məktublar** — mövcud məktubu seç və ya yeni məktub (nömrə, tarix, təmin edən qurum, məsul icraçı, əlaqə nömrəsi, son tarix, icra vəziyyəti: İcrası davam edir / İcra başa çatıb); bir məktub bir neçə texnikaya aid ola bilər. Ehtiyat hissələri cədvəli (ad, kod, vahid, tələb olunan, qəbul edilmiş, qalıq); qəbul (tarix, miqdar). İcra qeydləri (əlaqə tarixi, nəticə, gözlənilən təminat tarixi), son qeyd yuxarıda.
5. **Tamamlanma və təhvil** — faktiki tamamlanma tarixi + görülən işlər (status → "Təmir tamamlanıb, təhvil gözlənilir"); faktiki təhvil tarixi (status → "Təhvil verilib"), təhvil sənədi nömrəsi, tarixi, təhvil alan qurum (Göndərən qurum / Digər qurum → ad əl ilə yazılır); təhvil gözləmə müddəti avtomatik.

- Ulduzlu (*) sahələr boş olarsa saxlamaq olmaz; hansı sahə olduğu bildirilir.

## Statuslar (5)
Qəbul edilib · Təmirdədir · Təmir tamamlanıb, təhvil gözlənilir · Təhvil verilib · Təmirə yararsız.
Köçürmə: "Hissə gözləyir" və "Qismən təmin edilib" → Təmirdədir.

## Ehtiyat hissələrinin təminatı (avtomatik)
| Vəziyyət | Göstərilən |
|---|---|
| Hissə yazılmayıb | — |
| Heç biri qəbul olunmayıb | Təminat gözlənilir |
| Bir qismi qəbul olunub | Qismən təmin edilib |
| Hamısı qəbul olunub | Tam təmin edilib |

## Təmir reyestri
- Yeni sütun "Ehtiyat hissələrinin təminatı" (İcra statusu ilə Məsul icraçı arasında, çip).
- Yeni filtr: Hamısı / Təminat gözlənilir / Qismən təmin edilib / Tam təmin edilib.
- Excel çıxarışına da əlavə olunur.

## Digər
- "Sorğu yarat" bölməsi ləğv edilir (4-cü bölmə əvəz edir). Təchizat sorğuları siyahısı və kartları qalır, yeni quruluşa uyğunlaşdırılır.

## Baza
- Baza yeni quruluşa tam uyğunlaşır: artıq sahələr (silah sistemi, texniki vəziyyət xanaları, istehsalçı zavod, dərəcə, kritik hissə, mərhələ və s.) silinir, yenilər əlavə olunur.
- Mövcud məlumat yeni sahələrə köçürülür (test üçün).
