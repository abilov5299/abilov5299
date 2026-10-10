namespace TexnikaQeydiyyat.Data.Local
{
    public enum SaheNov { Metn, Reqem, Tarix, Secim }

    public sealed record Sahe(string Kod, string Ad, SaheNov Nov, string Qrup, bool Mecburi = false);

    // Reyestrin bütün sütunları. Cədvəl, forma və Excel uyğunlaşdırması bu siyahıdan qurulur.
    // Yeni sütun lazım olanda bura əlavə edilir: baza açılışda sütunu özü əlavə edir (köhnə məlumat itmir).
    public static class Saheler
    {
        public static readonly Sahe[] Hamisi =
        {
            new("Nov", "Texnikanın növü", SaheNov.Secim, "Pasport", true),
            new("Marka", "Texnikanın markası", SaheNov.Metn, "Pasport", true),
            new("ZavodNo", "Zavod nömrəsi", SaheNov.Metn, "Pasport", true),
            new("IstehsalIli", "İstehsal ili", SaheNov.Reqem, "Pasport"),
            new("IstehsalciZavod", "İstehsalçı zavod", SaheNov.Metn, "Pasport"),
            new("Derece", "Dərəcəsi", SaheNov.Secim, "Pasport"),
            new("OrtaTemir", "Orta təmir (bitmə tarixi)", SaheNov.Metn, "Təmir tarixçəsi"),
            new("EsasliTemir", "Əsaslı təmir (bitmə tarixi)", SaheNov.Metn, "Təmir tarixçəsi"),
            new("HerbiHisse", "İstismarda olduğu h/h", SaheNov.Metn, "İstismar göstəriciləri"),
            new("Km", "Maşının iş uçotu (km)", SaheNov.Reqem, "İstismar göstəriciləri"),
            new("Motosaat", "Mühərrikin iş uçotu (motosaat)", SaheNov.Reqem, "İstismar göstəriciləri"),
            new("Atislar", "Atışların uçotu (say)", SaheNov.Reqem, "İstismar göstəriciləri"),
            new("TopCapi", "Topun çapı (mm)", SaheNov.Reqem, "Silah sistemi"),
            new("TopMarka", "Topun markası", SaheNov.Metn, "Silah sistemi"),
            new("LuleNo", "Topun / lülənin nömrəsi", SaheNov.Metn, "Silah sistemi"),
            new("VMuherrik", "Mühərrikin vəziyyəti", SaheNov.Metn, "Texniki vəziyyət"),
            new("VSassi", "Şassinin vəziyyəti (transmissiya, təkər/tırtıl)", SaheNov.Metn, "Texniki vəziyyət"),
            new("VLule", "Lülə və avtomatik doldurma sistemi", SaheNov.Metn, "Texniki vəziyyət"),
            new("VAis", "Atəşi idarəetmə sistemi və stabilizator", SaheNov.Metn, "Texniki vəziyyət"),
            new("VOptik", "Optik və termal cihazlar", SaheNov.Metn, "Texniki vəziyyət"),
            new("VDiger", "Digər nasazlıqlar", SaheNov.Metn, "Texniki vəziyyət"),
            new("Qeyd", "Qeyd (qənimət və s.)", SaheNov.Metn, "Texniki vəziyyət"),
            new("Status", "Statusu", SaheNov.Secim, "Cari təmir", true),
            new("TemirNovu", "Təmir növü", SaheNov.Secim, "Cari təmir"),
            new("KritikHisse", "Kritik ehtiyat hissələrin mövcudluğu", SaheNov.Secim, "Cari təmir"),
            new("BaslamaTarixi", "Təmirin başlama tarixi", SaheNov.Tarix, "Cari təmir"),
            new("GozlenilenBitme", "Təmirin gözlənilən bitmə tarixi", SaheNov.Tarix, "Cari təmir"),
            new("GecikmeSebebi", "Gecikmə səbəbi", SaheNov.Metn, "Cari təmir"),
            new("Merhele", "Təmirin icra mərhələsi", SaheNov.Secim, "Cari təmir"),
            new("YararsizSebeb", "Təmirə yararlı deyilsə, səbəbi", SaheNov.Metn, "Cari təmir"),
            new("TemirQeyd", "Təmirə aid digər vacib qeydlər", SaheNov.Metn, "Cari təmir"),
            new("TemirZavodu", "Təmiri aparan zavod", SaheNov.Secim, "Qəbul və icra", true),
            new("DaxilolmaTarixi", "Zavoda daxilolma tarixi", SaheNov.Tarix, "Qəbul və icra"),
            new("GonderenQurum", "Göndərən qurum", SaheNov.Metn, "Qəbul və icra"),
            new("QebulSenedi", "Qəbul sənədinin nömrəsi və tarixi", SaheNov.Metn, "Qəbul və icra"),
            new("IcraMuddeti", "İcra müddəti (gün)", SaheNov.Reqem, "Qəbul və icra"),
            new("PlanDeyisikliyi", "Plan dəyişikliyinin səbəbi və əsası", SaheNov.Metn, "Qəbul və icra"),
            new("MesulIcraci", "Zavodda məsul icraçı", SaheNov.Metn, "Qəbul və icra"),
            new("TelebHisseler", "Tələb olunan hissələr (ad / kod, miqdar, vahid)", SaheNov.Metn, "Ehtiyat hissələr və yazışma"),
            new("Mektublar", "Əlaqəli məktubların nömrələri və tarixləri", SaheNov.Metn, "Ehtiyat hissələr və yazışma"),
            new("TeminEden", "Təmin edən idarə və icraçı", SaheNov.Metn, "Ehtiyat hissələr və yazışma"),
            new("SorguStatus", "Sorğuların icra statusları", SaheNov.Secim, "Ehtiyat hissələr və yazışma"),
            new("HisseDaxilolma", "Hissələr üzrə daxilolma tarixləri və miqdarları", SaheNov.Metn, "Ehtiyat hissələr və yazışma"),
            new("Senedler", "Əlaqəli sənədlər (nömrə, tarix, fayl)", SaheNov.Metn, "Ehtiyat hissələr və yazışma"),
            new("FaktikiBitme", "Təmirin faktiki bitmə tarixi", SaheNov.Tarix, "Təhvil"),
            new("TehvilSenedi", "Təhvil sənədinin nömrəsi və tarixi", SaheNov.Metn, "Təhvil"),
        };

        // Reyestrdə göstərilən 5 sütun
        public static readonly string[] Reyestr = { "Nov", "Marka", "ZavodNo", "Status", "GozlenilenBitme" };

        public static string SqlTip(SaheNov n) => n switch
        {
            SaheNov.Reqem => "REAL",
            _ => "TEXT"   // Tarix ISO formatında (yyyy-MM-dd) saxlanılır: sıralama və müqayisə düzgün işləyir
        };
    }
}
