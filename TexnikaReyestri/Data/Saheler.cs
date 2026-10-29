namespace TexnikaReyestri.Data
{
    public enum SaheNov { Metn, Reqem, Tarix, Secim }

    public sealed record Sahe(string Kod, string Ad, SaheNov Nov, string Qrup, bool Mecburi = false);

    // Texnikanın bütün sahələri ("Texnika əlavə et" formasının 5 bölməsi). Cədvəl, forma və Excel uyğunlaşdırması bu siyahıdan qurulur.
    // Yeni sütun lazım olanda bura əlavə edilir: baza açılışda sütunu özü əlavə edir (köhnə məlumat itmir).
    public static class Saheler
    {
        public static readonly Sahe[] Hamisi =
        {
            // 1. Qəbul və sənədlər
            new("Nov", "Texnikanın növü", SaheNov.Secim, "Qəbul və sənədlər", true),
            new("Marka", "Texnikanın markası", SaheNov.Metn, "Qəbul və sənədlər", true),
            new("ZavodNo", "Zavod nömrəsi", SaheNov.Metn, "Qəbul və sənədlər", true),
            new("Kateqoriya", "Kateqoriyası — faktiki", SaheNov.Secim, "Qəbul və sənədlər"),
            new("NaryadNo", "Naryadın nömrəsi", SaheNov.Metn, "Qəbul və sənədlər", true),
            new("NaryadTarixi", "Sənədin tarixi (naryadın tarixi)", SaheNov.Tarix, "Qəbul və sənədlər", true),
            new("DaxilolmaTarixi", "Zavoda faktiki daxilolma tarixi", SaheNov.Tarix, "Qəbul və sənədlər", true),
            new("GonderenQurum", "Təhvil verən (göndərən)", SaheNov.Metn, "Qəbul və sənədlər", true),
            new("AktForma", "Aktın forması", SaheNov.Secim, "Qəbul və sənədlər", true),
            new("AktNo", "Aktın nömrəsi", SaheNov.Metn, "Qəbul və sənədlər", true),
            new("AktTarixi", "Aktın tarixi", SaheNov.Tarix, "Qəbul və sənədlər", true),
            new("TemirZavodu", "Qəbul edən təmir müəssisəsi", SaheNov.Secim, "Qəbul və sənədlər", true),
            new("MesulIcraci", "Zavodda məsul icraçı", SaheNov.Metn, "Qəbul və sənədlər"),
            // 2. İstismar göstəriciləri və son təmir
            new("IstehsalIli", "İstehsal ili", SaheNov.Reqem, "İstismar göstəriciləri və son təmir"),
            new("IstismarTarixi", "İstismara verilib (tarix)", SaheNov.Tarix, "İstismar göstəriciləri və son təmir"),
            new("Gostericiler", "İstismarın əvvəlindən işləyib", SaheNov.Metn, "İstismar göstəriciləri və son təmir"),
            new("SonTemirNovu", "Son təmirin növü", SaheNov.Secim, "İstismar göstəriciləri və son təmir"),
            new("SonTemirTarixi", "Son təmirin tamamlanma tarixi", SaheNov.Tarix, "İstismar göstəriciləri və son təmir"),
            new("SonTemirIcraci", "Təmiri aparmış hərbi hissə və ya müəssisə", SaheNov.Metn, "İstismar göstəriciləri və son təmir"),
            new("SonTemirQeyd", "Son təmirdə yerinə yetirilmiş işlər", SaheNov.Metn, "İstismar göstəriciləri və son təmir"),
            // 3. Təmirin icrası
            new("Nasazliqlar", "Aşkar edilmiş nasazlıqların qısa təsviri", SaheNov.Metn, "Təmirin icrası", true),
            new("BaslamaTarixi", "Diaqnostika tarixi", SaheNov.Tarix, "Təmirin icrası", true),
            new("TemirNovu", "Təmirin / işin növü", SaheNov.Secim, "Təmirin icrası", true),
            new("Status", "İcra statusu", SaheNov.Secim, "Təmirin icrası", true),
            new("IcraMuddeti", "İcra müddəti", SaheNov.Reqem, "Təmirin icrası", true),
            new("MuddetVahidi", "Müddətin vahidi", SaheNov.Secim, "Təmirin icrası", true),
            new("GozlenilenBitme", "Plan üzrə bitmə tarixi", SaheNov.Tarix, "Təmirin icrası"),
            new("GecikmeSebebi", "Gecikmənin və ya plan dəyişikliyinin səbəbi", SaheNov.Metn, "Təmirin icrası"),
            new("Teminat", "Ehtiyat hissələrinin təminatı", SaheNov.Secim, "Təmirin icrası"),   // "Təminat gözləyir" işarəsi (formada çek qutusu)
            new("ProqnozBitme", "Gözlənilən bitmə tarixi", SaheNov.Tarix, "Təmirin icrası"),
            new("YeniPlanBitme", "Yeni plan üzrə bitmə tarixi", SaheNov.Tarix, "Təmirin icrası"),
            // Köhnə: texnikanın ehtiyat hissələri (indi hissələr məktubdadır — Sorgular.Hisseler; formada göstərilmir)
            new("Hisseler", "Ehtiyat hissələri", SaheNov.Metn, "Ehtiyat hissələri və məktublar"),
            
            // Faktiki sərfiyyat (formada 4-cü bölmə)
            new("Serfiyyat", "Faktiki sərfiyyat", SaheNov.Metn, "Faktiki sərfiyyat"),
            // 5. Təmirin tamamlanması və təhvil
            new("FaktikiBitme", "Təmirin faktiki tamamlanma tarixi", SaheNov.Tarix, "Təmirin tamamlanması və təhvil"),
            new("GorulenIsler", "Yerinə yetirilmiş işlər haqqında qeyd", SaheNov.Metn, "Təmirin tamamlanması və təhvil"),
            new("TehvilTarixi", "Faktiki təhvil tarixi", SaheNov.Tarix, "Təmirin tamamlanması və təhvil"),
            new("TehvilSenedNo", "Təhvil sənədinin nömrəsi", SaheNov.Metn, "Təmirin tamamlanması və təhvil"),
            new("TehvilSenedTarixi", "Təhvil sənədinin tarixi", SaheNov.Tarix, "Təmirin tamamlanması və təhvil"),
            new("TehvilAlan", "Təhvil alan qurum", SaheNov.Metn, "Təmirin tamamlanması və təhvil"),
        };

        /// <summary>JSON siyahı saxlanılan sahələr (göstəricilər, ehtiyat hissələri): Excel idxalında seçilmir, ixracda mətnə çevrilir.</summary>
        public static readonly HashSet<string> Json = new() { "Gostericiler", "Hisseler", "Serfiyyat" };

        // Reyestrin Excel ixracında ("yalnız reyestr sütunları") olan sahələr
        public static readonly string[] Reyestr = { "Nov", "Marka", "ZavodNo", "TemirZavodu", "Status", "Teminat", "MesulIcraci", "GozlenilenBitme" };

        public static string SqlTip(SaheNov n) => n switch
        {
            SaheNov.Reqem => "REAL",
            _ => "TEXT"   // Tarix ISO formatında (yyyy-MM-dd) saxlanılır: sıralama və müqayisə düzgün işləyir
        };
    }
}
