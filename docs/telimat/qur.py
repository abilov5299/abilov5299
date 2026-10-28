# -*- coding: utf-8 -*-
"""
Təmir və Texniki Təminat Xidməti · Məlumat bazası — istifadəçi təlimatlarının qurulması.
Bir mənbədən üç təlimat yaranır: Zavod, Xidmət, İdarə. Şəkillər HTML-in içinə yerləşdirilir (tək fayl).
İşə salmaq:  python docs/telimat/qur.py
Nəticə:      TexnikaReyestri/Telimat/Zavod.html, Xidmet.html, Idare.html
Ekran şəkilləri: docs/telimat/sekil/<rol>/*.png — tools/TelimatSekil aləti ilə çəkilir (dotnet run --project tools/TelimatSekil).
Düymələrin kiçik şəkilləri: sekil/ikon/*.png; şəkillərdəki elementlərin yerləri: sekil/yer.json.
"""
import base64, os, html, json, re

KOK = os.path.dirname(os.path.abspath(__file__))
CIX = os.path.normpath(os.path.join(KOK, '..', '..', 'TexnikaReyestri', 'Telimat'))
VERSIYA = '1.0'

ROLLAR = {
    'zavod':  dict(fayl='Zavod.html',  ad='Təmir zavodu',  tam='Təmir zavodunun istifadəçisi üçün',
                   qurum='939 nömrəli hərbi hissə', sekil='zavod'),
    'xidmet': dict(fayl='Xidmet.html', ad='Xidmət', tam='Təmir və Texniki Təminat xidmətinin istifadəçisi üçün',
                   qurum='Təmir və Texniki Təminat xidməti', sekil='xidmet'),
    'idare':  dict(fayl='Idare.html',  ad='İdarə', tam='Müdafiə Sənayesi İdarəsinin istifadəçisi üçün',
                   qurum='Müdafiə Sənayesi İdarəsi', sekil='idare'),
}

_sekil_say = [0]
YER = json.load(open(os.path.join(KOK, 'sekil', 'yer.json'), encoding='utf-8-sig'))
MENYU = {'Ana səhifə': 'home', 'Təmir reyestri': 'list', 'Texnika əlavə et': 'add', 'Sorğu yarat': 'sorgu', 'Sorğular üzrə təminat': 'tech',
         'Hesabatlar': 'rep', 'Bildirişlər': 'noti', 'İxrac / İdxal': 'sync', 'Tənzimləmə': 'set', 'Təlimat': 'help'}


def slug(t):
    """"Yadda saxla" → "yadda_saxla" (tools/TelimatSekil-dəki Slug ilə eyni qayda)."""
    t = t.replace('İ', 'i').lower()
    for a, b in (('ə', 'e'), ('ı', 'i'), ('ö', 'o'), ('ü', 'u'), ('ş', 's'), ('ç', 'c'), ('ğ', 'g'), ('i̇', 'i')):
        t = t.replace(a, b)
    return re.sub('_+', '_', re.sub('[^a-z0-9]', '_', t)).strip('_')


_ik_kes = {}
def ik(ad, sinif='ik'):
    """Düymənin / menyunun kiçik şəkli (yoxdursa boş)."""
    yol = os.path.join(KOK, 'sekil', 'ikon', ad + '.png')
    if not os.path.exists(yol): return ''
    if ad not in _ik_kes: _ik_kes[ad] = b64('ikon/' + ad + '.png')
    return f'<img class="{sinif}" src="{_ik_kes[ad]}" alt="">'


def Y(fayl, *adlar):
    """Şəkildəki elementlərin yerləri: [(nömrə, x%, y%)] — yer.json-dan (element mətni ilə)."""
    d = YER.get(fayl, {})
    return [(k, *d[a]) for k, a in enumerate(adlar, 1) if a in d]
_bolme = [0]


def b64(yol):
    with open(os.path.join(KOK, 'sekil', yol), 'rb') as f:
        return 'data:image/png;base64,' + base64.b64encode(f.read()).decode()


def sekil(yol, imza, nisanlar=None, izah=None, en=None):
    """Ekran şəkli; nisanlar: [(nömrə, x%, y%)], izah: [(nömrə, ad, mətn)]."""
    _sekil_say[0] += 1
    n = f'{_bolme[0]}.{_sekil_say[0]}'
    ni = ''.join(f'<span class="nis" style="left:{x}%;top:{y}%">{k}</span>' for k, x, y in (nisanlar or []))
    st = f' style="max-width:{en}px"' if en else ''
    s = f'<figure><div class="sek"{st}><img src="{b64(yol)}" alt="">{ni}</div><figcaption>Şəkil {n}. {imza}</figcaption></figure>'
    if izah:
        s += '<table class="izah"><thead><tr><th>№</th><th>Element</th><th>Təyinatı</th></tr></thead><tbody>'
        s += ''.join(f'<tr><td><span class="nis s">{k}</span></td><td>{a}</td><td>{m}</td></tr>' for k, a, m in izah)
        s += '</tbody></table>'
    return s


def cedvel(basliq, setirler, sinif=''):
    s = f'<table class="ced {sinif}"><thead><tr>' + ''.join(f'<th>{b}</th>' for b in basliq) + '</tr></thead><tbody>'
    for r in setirler:
        s += '<tr>' + ''.join(f'<td>{x}</td>' for x in r) + '</tr>'
    return s + '</tbody></table>'


def qeyd(m, nov='qeyd'):
    ad = {'qeyd': 'Qeyd', 'diqqet': 'Diqqət', 'meslehet': 'Məsləhət'}[nov]
    return f'<div class="qt {nov}"><b>{ad}.</b> {m}</div>'


def addimlar(*l):
    return '<ol class="add">' + ''.join(f'<li>{x}</li>' for x in l) + '</ol>'


def btn(t):
    i = ik(slug(t))
    return f'<span class="btnad">{t}</span>{i}' if i else f'<span class="btn">{t}</span>'


def menyu(t):
    return f'<span class="mn">{t}</span>' + (ik('menyu_' + MENYU[t], 'ik m') if t in MENYU else '')


# ============================================================ Bölmələr
def fesiller(r):
    Z, X, I = r == 'zavod', r == 'xidmet', r == 'idare'
    sq = ROLLAR[r]['sekil']
    F = []

    def fesil(id_, ad, mezmun):
        _bolme[0] += 1; _sekil_say[0] = 0
        F.append((id_, f'{_bolme[0]}. {ad}', mezmun()))

    # ---------------------------------------------------- 1
    def umumi():
        s = '<p><b>Təmir və Texniki Təminat Xidməti · Məlumat bazası</b> proqramı təmir zavodlarına daxil olan texnikanın qəbulunu, təmirini, ehtiyat hissələri ilə təminatını və təhvilini vahid qaydada uçota almaq üçün nəzərdə tutulub. Proqram hər kompüterdə öz lokal bazası ilə işləyir; qurumlar arasında məlumat <b>USB yaddaş vasitəsilə şifrələnmiş paketlərlə</b> ötürülür. Şəbəkə və internet bağlantısı tələb olunmur.</p>'
        s += '<h3>Məlumatın hərəkəti</h3><div class="axin"><div><b>Təmir zavodları</b><small>texnikanı qeyd edir, təmiri aparır</small></div><i>USB →</i><div><b>Təmir və Texniki Təminat xidməti</b><small>bütün zavodlara nəzarət edir</small></div><i>USB →</i><div><b>Müdafiə Sənayesi İdarəsi</b><small>ümumi vəziyyəti izləyir</small></div></div>'
        s += '<p>Texnika barədə məlumatı yalnız təmir zavodu daxil edir və dəyişdirir. Xidmət və İdarə məlumatı USB paketlərindən alır və <b>yalnız baxış</b> rejimində işləyir: siyahılara, hesabatlara, bildirişlərə baxır, Excel və PDF sənədləri hazırlayır.</p>'
        s += '<h3>Rollar və imkanlar</h3>' + cedvel(['İmkan', 'Zavod istifadəçisi', 'Zavod (yalnız baxış)', 'Xidmət', 'İdarə'], [
            ['Texnikanın əlavə edilməsi və redaktəsi', '✔', '—', '—', '—'],
            ['Sorğu yarat: məktub, ehtiyat hissələri, qəbul, icra qeydləri', '✔', '—', '—', '—'],
            ['Faktiki sərfiyyat (texnikaya sərf olunan material)', '✔', '—', '—', '—'],
            ['Reyestr, geniş baxış, hesabatlar', 'öz zavodu', 'öz zavodu', 'bütün zavodlar', 'bütün zavodlar'],
            ['Excel-dən idxal', '✔', '—', '—', '—'],
            ['Excel-ə və PDF-ə çıxarış', '✔', '✔', '✔', '✔'],
            ['USB-yə ixrac (paket göndərmək)', '✔ (Xidmətə)', '✔ (Xidmətə)', '✔ (İdarəyə)', '—'],
            ['USB-dən idxal (paket qəbul etmək)', '—', '—', '✔ (zavodlardan)', '✔ (Xidmətdən)'],
            ['Ehtiyat nüsxə və loglar', '✔', '✔', '✔', '✔'],
        ])
        s += f'<p>Bu təlimat <b>{ROLLAR[r]["tam"].lower()}</b> nəzərdə tutulub. Təlimatdakı şəkillər nümunə məlumatla hazırlanıb; sizin ekranınızda rəqəmlər və adlar fərqli olacaq.</p>'
        s += '<h3>Təlimatda istifadə olunan işarələr</h3>' + cedvel(['İşarə', 'Mənası'], [
            [btn('Yadda saxla'), 'Ekrandakı düymə'], [menyu('Təmir reyestri'), 'Sol menyudakı bölmə'],
            ['<span class="nis s">3</span>', 'Şəkildəki element; izahı şəklin altındakı cədvəldədir'],
            ['<b>*</b>', 'Məcburi sahə — doldurulmadan qeyd yadda saxlanılmır']])
        return s
    fesil('umumi', 'Ümumi məlumat', umumi)

    # ---------------------------------------------------- 2
    def giris():
        s = '<p>Proqram masaüstündəki <b>Təmir və Texniki Təminat Xidməti</b> nişanı ilə açılır. Açılışda giriş pəncərəsi göstərilir.</p>'
        s += sekil('forms/00_giris.png', 'Giriş pəncərəsi', [(1, 54.5, 44.5), (2, 54.5, 55.5), (3, 87.5, 55.5), (4, 54.5, 61), (5, 54.5, 68.6), (6, 86, 4.5), (7, 91.5, 4.5), (8, 97.2, 4.5)], [
            (1, 'İstifadəçi adı', 'Administratorun sizə verdiyi login. Enter düyməsi kursoru parol xanasına keçirir.'),
            (2, 'Parol', 'Şəxsi parolunuz. Simvollar gizli göstərilir.'),
            (3, 'Göz nişanı', 'Basılı saxladıqda yazdığınız parol görünür — səhvi yoxlamaq üçün.'),
            (4, 'Bu kompüterdə məni xatırla', 'İşarələnsə, növbəti açılışda istifadəçi adı avtomatik yazılır. Parol heç vaxt yadda saxlanılmır. Yanında çıxan <b>Unut</b> linki yadda saxlanılmış adı silir.'),
            (5, 'DAXİL OL', 'Girişi təsdiqləyir (Enter düyməsi də eyni işi görür).'),
            (6, 'Tema', 'İşıqlı və tünd görünüş arasında keçid.'),
            (7, 'Dil', 'Proqramın dili: Azərbaycan (AZ), ingilis (EN), rus (RU).'),
            (8, 'Bağla', 'Proqramdan çıxış.')])
        s += '<h3>Giriş qaydaları</h3><ul>'
        s += '<li>Parol bir neçə dəfə (standart olaraq 5 dəfə) ardıcıl səhv yazılarsa, hesab <b>bloklanır</b>. Bloku yalnız administrator aça bilər.</li>'
        s += '<li>Administrator sizə <b>müvəqqəti parol</b> veribsə, ilk girişdən sonra yeni parol təyin etməyiniz tələb olunur (şəkil ' + f'{_bolme[0]}.2' + '). Parol ən azı 8 simvoldan ibarət olmalı, böyük hərf, kiçik hərf və rəqəm saxlamalıdır.</li>'
        s += '<li>Parolun etibarlılıq müddəti bitdikdə (standart 90 gün) proqram yeni parol tələb edir.</li>'
        s += '<li>Proqramda müəyyən müddət (standart 15 dəqiqə) heç bir əməliyyat edilməzsə, sessiya avtomatik bağlanır və yenidən giriş tələb olunur. Yadda saxlanılmamış dəyişikliklər itir.</li></ul>'
        s += sekil('forms/00_parol.png', 'Parolun dəyişdirilməsi', en=520)
        s += '<h3>Proqramdan çıxış</h3><p>Sol menyunun ən aşağısındakı çıxış nişanı sessiyanı bağlayır və giriş pəncərəsinə qaytarır. Kompüterdən ayrılarkən həmişə çıxış edin.</p>'
        s += qeyd('Proqram kompüterdə ilk dəfə quraşdırılanda <b>İlk quraşdırma</b> pəncərəsi açılır: kompüterin qurumu seçilir, administrator parolu və ilk istifadəçi yaradılır. Bu əməliyyatı administrator yerinə yetirir (Əlavə A).')
        return s
    fesil('giris', 'Sistemə giriş və çıxış', giris)

    # ---------------------------------------------------- 3
    def pencere():
        s = '<p>Girişdən sonra əsas pəncərə açılır. Pəncərə üç hissədən ibarətdir: yuxarıda <b>başlıq zolağı</b>, solda <b>menyu</b>, ortada seçilmiş bölmənin məzmunu.</p>'
        s += sekil(f'{sq}/01_ana.png', 'Əsas pəncərə', [(1, 9, 3), (2, 26, 3), (3, 77.8, 3), (4, 81.4, 3), (5, 85.4, 3), (6, 92.8, 3), (7, 2.3, 3.6), (8, 2.3, 96.3)], [
            (1, 'Qurumun adı', 'Bu kompüterin aid olduğu qurum.'),
            (2, 'İstifadəçi və rol', 'Daxil olmuş istifadəçinin adı və səlahiyyəti.'),
            (3, 'Tema', 'İşıqlı / tünd görünüş.'),
            (4, 'Dil', 'AZ / EN / RU. Dəyişiklik dərhal tətbiq olunur. Bazadakı məlumat (adlar, qeydlər) tərcümə olunmur.'),
            (5, 'Bildirişlər', 'Oxunmamış bildirişlərin sayı qırmızı dairədə göstərilir. Klik bildiriş pəncərəsini açır.'),
            (6, 'Pəncərə düymələri', 'Kiçilt, böyüt, bağla.'),
            (7, 'Loqo', 'Klikləndikdə sol menyu genişlənir və bölmələrin adları görünür; təkrar klik menyunu yığır.'),
            (8, 'Çıxış', 'Sessiyanı bağlayır.')])
        s += sekil(f'{sq}/02_menyu.png', 'Genişləndirilmiş menyu')
        rows = [['Ana səhifə', 'Ümumi göstəricilər, aylar üzrə diaqram, diqqət tələb edən məsələlər.'],
                ['Təmir reyestri', 'Bütün texnikanın siyahısı, filtrlər, axtarış, Excel ilə iş.']]
        if Z: rows += [['Texnika əlavə et', 'Zavoda daxil olan yeni texnikanın qeydiyyatı (5 bölməli forma).'],
                       ['Sorğu yarat', 'Təchizat məktubu, ehtiyat hissələri və materiallar, onların qəbulu və icra qeydləri.']]
        rows += [['Sorğular üzrə təminat', 'Təchizat sorğuları əsasında qəbul edilmiş ehtiyat hissələri və materiallar: tələb, qəbul, sərf, anbar qalığı.'],
                 ['Hesabatlar', 'Yeddi növ hazır hesabat; ekranda, Excel və PDF formatında.'],
                 ['Bildirişlər', 'Gecikmə, son tarix, ehtiyat hissəsi, paket və yeni qəbul xəbərdarlıqları.'],
                 ['İxrac / İdxal', 'USB yaddaş ilə məlumat mübadiləsi.'],
                 ['Tənzimləmə', 'Ehtiyat nüsxə və loglar (əməliyyat jurnalı).']]
        s += '<h3>Menyu bölmələri</h3>' + cedvel(['Bölmə', 'Təyinatı'], rows)
        s += '<h3>Klaviatura qısayolları</h3>' + cedvel(['Düymə', 'Əməliyyat'], ([['Ctrl + N', 'Yeni texnika əlavə et']] if Z else []) + [
            ['Ctrl + F', 'Reyestri açır və kursoru axtarış xanasına qoyur'], ['F5', 'Açıq bölməni yeniləyir'],
            ['Enter (reyestrdə)', 'Seçilmiş texnikanın geniş baxışını açır']])
        return s
    fesil('pencere', 'Əsas pəncərə və menyu', pencere)

    # ---------------------------------------------------- 4
    def ana():
        s = f'<p>{menyu("Ana səhifə")} proqram açılanda ilk görünən bölmədir. {"Göstəricilər yalnız sizin zavod üzrə hesablanır." if Z else "Göstəricilər bütün zavodlar üzrə hesablanır; aşağıda zavodlar üzrə cədvəl də göstərilir."}</p>'
        s += sekil(f'{sq}/01_ana.png', 'Ana səhifə', [(1, 13.5, 17), (2, 29, 17), (3, 44.5, 17), (4, 60, 17), (5, 75.5, 17), (6, 91, 17), (7, 50, 45)] + ([(8, 50, 85), (9, 50, 93.3)] if Z else []))
        s += '<h3>Göstərici kartları</h3>' + cedvel(['№', 'Kart', 'Nəyi göstərir'], [
            ['1', 'Cəmi texnika', 'Təmiri davam edən + təhvil gözlənilən + təhvil verilmiş texnika. "Təmirə yararsız" statusu daxil deyil.'],
            ['2', 'Təmiri davam edən', 'Statusu "Qəbul edilib" və ya "Təmirdədir" olan, tamamlanmamış texnika.'],
            ['3', 'Təmir tamamlanıb, təhvil gözlənilir', 'Təmiri bitmiş, lakin hələ təhvil verilməmiş texnika.'],
            ['4', 'Təhvil verilmiş', 'Sahibinə təhvil verilmiş texnika.'],
            ['5', 'İcra müddətinə 0–3 gün qalıb', 'Plan üzrə bitmə tarixinə az qalmış texnika (hədd administrator tərəfindən dəyişdirilə bilər).'],
            ['6', 'İcra müddəti keçmiş', 'Plan tarixi keçmiş, təmiri hələ davam edən texnika.']])
        s += '<p>Hər hansı karta klik etdikdə <b>Təmir reyestri</b> həmin şərtlə süzülmüş halda açılır.</p>'
        s += '<h3>Aylar üzrə diaqram (7)</h3><p>Son aylar üzrə qəbul edilmiş, təmiri tamamlanmış və təhvil verilmiş texnikanın sayı. Sütunun üzərindəki rəqəm həmin ay üzrə saydır.</p>'
        if Z:
            s += '<h3>Diqqət zolaqları</h3><p><b>Təhvil gözlənilən texnikalar (8)</b> — təmiri bitib təhvil verilməyən texnikanın sayı və ən uzun gözləmə müddəti; <b>Siyahını aç</b> düyməsi bu texnikaları reyestrdə göstərir. <b>Məlumat mübadiləsi (9)</b> — son USB ixracının tarixi və ondan sonra baş vermiş dəyişikliklərin sayı; <b>Məlumatları köçürməyə</b> düyməsi İxrac bölməsini açır.</p>'
        else:
            s += '<h3>Zavodlar üzrə cədvəl</h3><p>Hər zavod üzrə təmiri davam edən, təhvil gözlənilən, təhvil verilmiş texnika və cəmi. Cədvəldəki məlumat zavodlardan son qəbul edilmiş USB paketinə əsaslanır.</p>'
        return s
    fesil('ana', 'Ana səhifə', ana)

    # ---------------------------------------------------- 5
    def reyestr():
        s = f'<p>{menyu("Təmir reyestri")} — texnikanın əsas siyahısıdır. Siyahı hissə-hissə yüklənir: aşağı sürüşdükcə növbəti sətirlər gəlir, buna görə yüz minlərlə qeyd olsa belə ekran dərhal açılır.</p>'
        s += sekil(f'{sq}/03_reyestr.png', 'Təmir reyestri', [(1, 21, 9.5)] + ([(2, 83.5, 9.5)] if Z else []) + [(3, 93.5, 9.5), (4, 50, 17), (5, 40.5, 27.8), (6, 63, 27.8), (7, 86, 27.8), (8, 40.5, 34.7), (9, 75, 34.7), (10, *YER[f'{sq}/03_reyestr.png'].get('Təminat gözləyir', [35, 39.7])), (11, 45, 47.8), (12, 95, 52), (13, 9, 97)], [
            (1, 'Başlıq', 'Hansı zavoda baxıldığı göstərilir.'),
            *([(2, 'Excel-dən idxal', 'Mövcud Excel reyestrini proqrama köçürmək (bölmə 5.4).')] if Z else []),
            (3, 'Excel-ə çıxar', 'Siyahını Excel faylına çıxarmaq (bölmə 5.3).'),
            (4, 'Kartlar', 'Ana səhifədəki kimi; klik siyahını həmin şərtlə süzür.'),
            (5, 'Status', 'İcra statusuna görə süzgəc.'),
            (6, 'İcra müddəti', 'Plan tarixinə qalan günə görə: sarı hədd, 0–3 gün qalıb, müddəti keçib, müddəti müəyyən edilməyib.'),
            (7, 'Məsul icraçı', 'Zavodda məsul şəxsə görə süzgəc.'),
            (8, 'Tarix növü', 'Tarix aralığı hansı tarixə tətbiq olunsun: plan üzrə bitmə, faktiki tamamlanma, zavoda daxilolma, diaqnostika, faktiki təhvil.'),
            (9, 'Tarixdən / Tarixədək', 'Xananın solundakı işarə qoyulduqda tarix aralığı tətbiq olunur.'),
            (10, 'Təminat gözləyir', 'İşarələndikdə yalnız ehtiyat hissəsi gözləyən texnika göstərilir.'),
            (11, 'Cədvəl', 'Texnika / inventar kodu, zavod, icra statusu, ehtiyat hissələrinin təminatı (“Təminat gözləyir” işarəsi), məsul icraçı.'),
            (12, 'Geniş bax', 'Texnikanın bütün məlumatlarını açır (bölmə 6). Sətrə iki dəfə klik də eyni işi görür.'),
            (13, 'Sayğac', 'Göstərilən / filtrə uyğun qeydlərin sayı.')])
        if not Z:
            s += qeyd('Xidmət və İdarə istifadəçisində filtrlərin əvvəlində <b>Zavod</b> seçimi də var: bütün zavodlara və ya bir zavoda baxmaq mümkündür.')
        s += '<h3>5.1. Axtarış</h3><p>Axtarış xanasına texnikanın adı (markası), inventar (zavod) nömrəsi, naryad nömrəsi, nasazlıq təsvirindən söz və ya məktub nömrəsi yazılır. Bir neçə söz yazmaq olar: <code>bmp 52</code>. Nəticə yazmağı dayandırdıqdan dərhal sonra göstərilir.</p>'
        s += '<h3>5.2. Statuslar</h3>' + cedvel(['Status', 'Mənası', 'Necə qoyulur'], [
            ['<span class="cip g">Qəbul edilib</span>', 'Texnika zavoda daxil olub, təmirə başlanmayıb', 'Yeni texnikada avtomatik'],
            ['<span class="cip b">Təmirdədir</span>', 'Təmir işləri aparılır', 'İstifadəçi seçir'],
            ['<span class="cip o">Təmir tamamlanıb, təhvil gözlənilir</span>', 'Təmir bitib, texnika təhvil verilməyib', 'Faktiki tamamlanma tarixi yazılanda avtomatik'],
            ['<span class="cip y">Təhvil verilib</span>', 'Texnika təhvil verilib', 'Faktiki təhvil tarixi yazılanda avtomatik'],
            ['<span class="cip r">Təmirə yararsız</span>', 'Texnika bərpa oluna bilmir', 'İstifadəçi seçir']])
        s += qeyd('<span class="cip o">Təminat gözləyir</span> status deyil, ayrıca işarədir: texnika “Qəbul edilib” və ya “Təmirdədir” statusunda qalır, işarə isə “Ehtiyat hissələrinin təminatı” sütununda görünür. Formada bölmə 3-dəki çek qutusu ilə qoyulur.')
        s += '<h3>5.3. Excel-ə çıxarış</h3>' + sekil('forms/13_excel_ixrac.png', 'Excel-ə ixrac pəncərəsi', en=730) + cedvel(['Seçim', 'Təyinatı'], [
            ['Ekrandakı filtrlənmiş siyahı', 'Yalnız hazırda filtrə uyğun texnika'], ['Bütün reyestr', 'Bütün texnika (zavod istifadəçisində — öz zavodu)'],
            ['Yalnız seçilmiş sətir', 'Cədvəldə seçilmiş bir texnika'], ['Yalnız reyestr sütunları / Bütün sütunlar', 'Qısa siyahı və ya bütün sahələr'],
            ['Bölmə başlıqları', 'Sütunların üstündə bölmə adları (Qəbul və sənədlər, Təmirin icrası...)'],
            ['Statusa görə rəngli xanalar', 'Status və təminat xanaları ekrandakı rənglərlə'], ['Gecikən tarixləri qırmızı göstər', 'Plan tarixi keçmiş texnikanın tarixi qırmızı'],
            ['Başlığı dondur və filtr əlavə et', 'Excel-də başlıq sətri sabit qalır və süzgəc düymələri əlavə olunur'],
            ['İkinci vərəq: zavodlar üzrə icmal', 'Ayrı vərəqdə zavodlar üzrə saylar'], ['Hazır olanda faylı aç', 'Fayl yaradılandan sonra Excel-də açılır']])
        if Z:
            s += '<h3>5.4. Excel-dən idxal</h3><p>Əvvəllər Excel-də aparılmış reyestri proqrama köçürmək üçündür. İdxal dörd addımda aparılır və <b>yoxlama mərhələsində bazaya heç nə yazılmır</b>.</p>'
            s += sekil('forms/14_excel_idxal.png', 'Excel-dən idxal — 1-ci addım', en=930)
            s += addimlar('<b>Fayl seç.</b> Faylı pəncərəyə sürükləyin və ya klikləyib seçin. Bir neçə vərəq varsa, lazım olanı seçin. Proqram başlıq sətrini özü tapır.',
                          '<b>Sütunları uyğunlaşdır.</b> Hər Excel sütunu üçün proqramdakı sahə avtomatik təklif olunur. Sarı ilə işarələnənləri yoxlayın; lazım olmayan sütun üçün <i>— ötür —</i> seçin.',
                          '<b>Yoxla.</b> Hər sətir üçün nəticə göstərilir: <i>Yeni</i>, <i>Yenilənəcək</i> (eyni zavod nömrəsi bazada var), <i>Xəbərdarlıq</i>, <i>Xəta</i>. Xətalı sətirlər idxal olunmur; onların siyahısını Excel faylı kimi saxlamaq olar.',
                          '<b>Nəticə.</b> İdxal edilən, yenilənən və ötürülən sətirlərin sayı.')
            s += qeyd('Excel-də boş qalan xana bazadakı mövcud dəyəri silmir. Köhnə formatdakı adlar (məs. "Cari", "Hissə gözləyir") yeni adlara avtomatik uyğunlaşdırılır.', 'meslehet')
        return s
    fesil('reyestr', 'Təmir reyestri', reyestr)

    # ---------------------------------------------------- 6
    def genis():
        s = '<p>Reyestrdə <b>Geniş bax</b> düyməsi və ya sətrə iki dəfə klik texnikanın tam kartını açır.</p>'
        s += sekil(f'{sq}/04_genis_bax.png', 'Geniş baxış', [(1, 9, 9.3), (2, 30, 7.5), (3, 72, 9.3), (4, 79.5, 9.3), (5, 87.5, 9.3)] + ([(6, 96, 9.3)] if Z else []) + [(7, 50, 17.5)], [
            (1, 'Reyestrə qayıt', 'Siyahıya qayıdır; filtrlər saxlanılır.'),
            (2, 'Ad və status', 'Texnikanın markası, zavod nömrəsi, statusu. Plan tarixi keçibsə, qırmızı <b>Gecikir</b> nişanı görünür.'),
            (3, 'Əvvəlki', 'Reyestrdəki əvvəlki texnikaya keçid.'),
            (4, 'Növbəti', 'Növbəti texnikaya keçid.'),
            (5, 'PDF pasport', 'Texnikanın bütün məlumatlarını çap üçün PDF faylına çıxarır.'),
            *([(6, 'Redaktə', 'Texnikanın məlumatlarını dəyişmək (bölmə 9).')] if Z else []),
            (7, 'Mərhələ xətti', 'Qəbul edilib → Təmirdədir → Təmir tamamlanıb → Təhvil verilib.')])
        s += '<p>Aşağıdakı kartlar formanın bölmələrinə uyğundur: Qəbul və sənədlər, İstismar göstəriciləri və son təmir, Təmirin icrası (o cümlədən “Təminat gözləyir” işarəsi), Faktiki sərfiyyat, Təmirin tamamlanması və təhvil. Boş sahələrdə “—” işarəsi göstərilir.</p>'
        s += '<h3>Dəyişiklik tarixçəsi</h3><p>Sol yuxarı kartda texnika üzrə edilmiş hər dəyişiklik göstərilir: tarix və vaxt, istifadəçi, sahənin adı, köhnə və yeni dəyər (“köhnə → yeni”). Tarixçə silinmir və redaktə edilmir; USB paketi ilə üst qurumlara da ötürülür.</p>'
        return s
    fesil('genis', 'Texnikanın geniş baxışı', genis)

    # ---------------------------------------------------- 7–10 (yalnız zavod)
    if Z:
        def elave():
            s = f'<p>{menyu("Texnika əlavə et")} (və ya <b>Ctrl + N</b>) zavoda daxil olan texnikanın qeydiyyat formasını açır. Forma 5 bölmədən ibarətdir. Bölmələr arasında yuxarıdakı nişanlarla və ya {btn("Əvvəlki bölmə")} / {btn("Növbəti bölmə")} düymələri ilə keçid edilir. {btn("Yadda saxla")} bütün bölmələri birlikdə saxlayır.</p>'
            s += qeyd('Ulduzla (*) işarələnmiş sahələr məcburidir. Boş məcburi sahə qalarsa, proqram yadda saxlamır, həmin sahənin olduğu bölməni açır və kursoru oraya qoyur. Tarix xanasının solundakı işarə götürülərsə, tarix boş sayılır.', 'diqqet')
            s += '<h3>7.1. Bölmə 1 — Qəbul və sənədlər</h3>' + sekil('zavod/05_elave_1.png', 'Qəbul və sənədlər')
            s += cedvel(['Sahə', 'Nə yazılır', 'Nümunə'], [
                ['Texnikanın növü *', 'Siyahıdan: Nəqliyyat vasitəsi, Zirehli texnika, Digər texnika', 'Zirehli texnika'],
                ['Texnikanın markası *', 'Marka və modifikasiya', 'BMP-2'],
                ['Zavod nömrəsi *', 'İstehsalçı zavod nömrəsi (inventar kodu). Bazada təkrarlana bilməz', 'KD-688774'],
                ['Kateqoriyası — faktiki', 'Texniki vəziyyət aktında göstərildiyi kimi (I–V)', 'III'],
                ['Naryadın nömrəsi *', 'Texnikanın göndərildiyi naryad. Bir naryadla bir neçə texnika gələ bilər — eyni nömrə təkrar yazıla bilər', '818/26'],
                ['Sənədin tarixi (naryadın tarixi) *', 'Naryadın tarixi', '20.10.2026'],
                ['Zavoda faktiki daxilolma tarixi *', 'Texnikanın zavoda faktiki gəldiyi gün (standart — bu gün)', '23.10.2026'],
                ['Təhvil verən (göndərən) *', 'Texnikanı göndərən hərbi hissə və ya qurum', '444 nömrəli hərbi hissə'],
                ['Aktın forması / nömrəsi / tarixi', 'Texniki vəziyyət aktı: Forma 12 və ya Forma 12 (RAS)', 'Forma 12, A-125, 23.10.2026'],
                ['Zavodda məsul icraçı', 'Texnikaya cavabdeh şəxs (standart — daxil edən istifadəçi)', 'kapitan Məmmədov R.'],
                ['Qəbul edən təmir müəssisəsi', 'Sizin zavod — avtomatik, dəyişdirilmir', '939 nömrəli hərbi hissə']])
            s += '<p>Yeni texnikanın ilkin statusu avtomatik olaraq <b>Qəbul edilib</b> olur.</p>'
            s += '<h3>7.2. Bölmə 2 — İstismar göstəriciləri və son təmir</h3>' + sekil('zavod/05_elave_2.png', 'İstismar göstəriciləri və son təmir')
            s += cedvel(['Sahə', 'Nə yazılır', 'Nümunə'], [
                ['İstehsal ili', 'Dörd rəqəmli il', '1991'], ['İstismara verilib (tarix)', 'İlk istismara verildiyi tarix', '01.06.1992'],
                ['Göstəricinin növü və miqdarı', 'İstismarın əvvəlindən ümumi göstərici: Gediş (km), İşləmə (saat), Motosaat, Dövr, Atəş sayı. "Tətbiq edilmir" və "Məlumat yoxdur" seçildikdə miqdar tələb olunmur', 'Gediş, km — 12 480'],
                ['Son təmirin növü və tarixi', 'Zavoda daxil olmadan əvvəlki <b>yalnız ən son</b> təmir: Cari, Orta, Əsaslı', 'Orta təmir, 15.03.2018'],
                ['Təmiri aparmış hissə / müəssisə', 'Son təmiri aparan qurum', '296 nömrəli hərbi hissə'],
                ['Yerinə yetirilmiş işlər haqqında qeyd', 'Son təmirdə görülən işlər', 'Mühərrik dəyişdirilib']])
            s += f'<p>Bir neçə göstərici yazmaq üçün {btn("+ Digər istismar göstəricisini əlavə et")} düyməsindən istifadə edin. Sətrin yanındakı səbət nişanı sətri silir.</p>'
            s += '<h3>7.3. Bölmə 3 — Təmirin icrası</h3>' + sekil('zavod/05_elave_3.png', 'Təmirin icrası')
            s += cedvel(['Sahə', 'Nə yazılır', 'Nümunə'], [
                ['Aşkar edilmiş nasazlıqların qısa təsviri *', 'Diaqnostika nəticəsində aşkar edilən nasazlıqlar', 'Transmissiyada kənar səs, sürət qutusu nasazdır'],
                ['Diaqnostika tarixi *', 'Diaqnostikanın aparıldığı gün. İcra müddəti bu tarixdən hesablanır', '24.10.2026'],
                ['Təmirin / işin növü *', 'Cari, Orta, Əsaslı təmir və ya Nasazlıqların aradan qaldırılması', 'Orta təmir'],
                ['İcra statusu *', 'Qəbul edilib, Təmirdədir, Təmirə yararsız. Tamamlanma və təhvil statusları tarixlərə görə avtomatik qoyulur', 'Təmirdədir'],
                ['Təminat gözləyir (çek qutusu)', 'Texnika ehtiyat hissəsi gözləyirsə işarələnir. Status dəyişmir; yalnız “Qəbul edilib” və “Təmirdədir” statusunda qoyula bilər', '✔'],
                ['Müddət * və Vahid *', 'Təmirin icra müddəti; vahid — Gün və ya Ay', '45, Gün'],
                ['Plan üzrə bitmə tarixi', 'Avtomatik: diaqnostika tarixi + müddət', '08.12.2026'],
                ['Qalan / gecikən gün', 'Avtomatik: plan tarixinə qalan və ya keçən gün', '43 gün qalıb'],
                ['Gecikmənin və ya plan dəyişikliyinin səbəbi', 'Yalnız gecikmə və ya plan dəyişikliyi olduqda', 'Ehtiyat hissələri gecikir'],
                ['Gözlənilən bitmə tarixi', 'Təmirin real olaraq nə vaxt bitəcəyi barədə proqnoz. Planı dəyişmir', '20.12.2026'],
                ['Yeni plan üzrə bitmə tarixi', 'Planın rəsmi dəyişdirilmiş tarixi. Yazıldıqda plan bu tarixə keçir; ilkin plan tarixçədə qalır', '25.12.2026']])
            s += sekil('zavod/05_elave_3_teminat.png', '“Təminat gözləyir” işarəsi (mövcud texnikanın redaktəsi)', Y('zavod/05_elave_3_teminat.png', 'İcra statusu *', 'Təminat gözləyir'), [
                (1, 'İcra statusu', 'Qəbul edilib / Təmirdədir / Təmirə yararsız. “Təminat gözləyir” statusu deyil.'),
                (2, 'Təminat gözləyir', 'Texnika ehtiyat hissəsi gözləyirsə işarələyin. Reyestrdə “Ehtiyat hissələrinin təminatı” sütununda görünür.')])
            s += qeyd('“Gözlənilən bitmə” yalnız məlumat xarakterlidir; gecikmə hesablaması, kartlar və bildirişlər “Plan üzrə bitmə tarixi”nə əsasən aparılır.')
            s += '<h3>7.4. Bölmə 4 — Faktiki sərfiyyat</h3><p>Texnikanın təmirinə faktiki sərf olunan material və ehtiyat hissələri burada qeyd edilir. Hesabatlarda bu məlumat daxil edilmir — oradan avtomatik götürülür.</p>'
            s += addimlar(f'{btn("+ Sərfiyyat əlavə et")} basın.',
                          '<b>Material / ehtiyat hissəsi</b> xanasına klikləyin — axtarış pəncərəsi açılır. Adın və ya kodun bir hissəsini yazın, siyahıdan seçin. Siyahıda zavodun anbarındakı (sorğular üzrə qəbul edilmiş) materiallar qalığı ilə göstərilir; kodu və ölçü vahidi avtomatik gəlir.',
                          f'Material anbarda yoxdursa, {btn("Yeni ad əlavə et")} basın və adı, kodu, ölçü vahidini əl ilə yazın.',
                          'Sərf edilmiş miqdarı və sərf tarixini yazın, ' + btn('Yadda saxla') + ' basın.')
            F4 = 'zavod/05_elave_4_serf.png'
            s += sekil(F4, 'Faktiki sərfiyyat', Y(F4, 'Sərfiyyat əlavə et', 'Material / ehtiyat hissəsi *', 'Kodu', 'Yeni ad əlavə et', 'Sərf edilmiş miqdar *', 'Sərf tarixi *'), [
                (1, 'Sərfiyyat əlavə et', 'Sərfiyyat panelini açır.'),
                (2, 'Material / ehtiyat hissəsi', 'Klik — axtarış pəncərəsi açılır. Anbardakı materiallar qalığı ilə göstərilir.'),
                (3, 'Kodu, Ölçü vahidi', 'Anbardan seçiləndə avtomatik dolur.'),
                (4, 'Yeni ad əlavə et', 'Anbarda olmayan materialı əl ilə yazmaq üçün. Basıldıqda “Anbardan seç” olur — geri qayıtmaq üçün.'),
                (5, 'Sərf edilmiş miqdar', 'Faktiki istifadə olunan miqdar. Anbardan seçiləndə qalıqdan çox ola bilməz.'),
                (6, 'Sərf tarixi', 'Materialın sərf olunduğu gün.')])
            s += sekil('zavod/05_material_axtar.png', 'Material axtarışı: “Mühərrik” yazılıb', en=620)
            s += '<p>Axtarış pəncərəsində ox düymələri ilə siyahıda gəzmək, Enter ilə seçmək, Esc ilə bağlamaq olar. Başlıqda tapılanların sayı göstərilir.</p>'
            s += qeyd('Anbardan seçilən materialda qalıqdan çox miqdar yazmaq olmaz. “Yeni ad” ilə yazılan material anbardan çıxılmır. Yadda saxlanmış sərfiyyat qeydi silinmir. Mövcud texnikada qeyd dərhal yazılır, yeni texnikada — texnika yadda saxlananda.', 'diqqet')
            s += '<h3>7.5. Bölmə 5 — Təmirin tamamlanması və təhvil</h3>' + sekil('zavod/05_elave_5.png', 'Tamamlanma və təhvil')
            s += cedvel(['Sahə', 'Nə yazılır', 'Nəticə'], [
                ['Təmirin faktiki tamamlanma tarixi', 'Təmirin faktiki bitdiyi gün', 'Status: <b>Təmir tamamlanıb, təhvil gözlənilir</b>'],
                ['Yerinə yetirilmiş işlər haqqında qeyd', 'Görülən işlər. Tamamlanma tarixi yazılıbsa məcburidir', '—'],
                ['Faktiki təhvil tarixi', 'Texnikanın təhvil verildiyi gün', 'Status: <b>Təhvil verilib</b>'],
                ['Təhvil sənədinin nömrəsi və tarixi', 'Təhvil-qəbul sənədi. Təhvil tarixi yazılıbsa məcburidir', '—'],
                ['Təhvil alan qurum', '<i>Göndərən qurum</i> (bölmə 1-dəki qurum) və ya <i>Digər qurum</i> — seçildikdə qurumun adı əl ilə yazılır', '—'],
                ['Təhvil gözləmə müddəti', 'Avtomatik: tamamlanmadan təhvilədək (təhvil olmayıbsa — bu günədək)', '12 gün']])
            s += qeyd('Təhvil tarixi faktiki tamamlanma tarixi olmadan yazıla bilməz. Statusu əl ilə “Təmir tamamlanıb” və ya “Təhvil verilib” seçmək olmaz — müvafiq tarix yazılmalıdır.', 'diqqet')
            s += f'<h3>7.6. Yadda saxlama</h3><p>{btn("Yadda saxla")} basıldıqdan sonra texnika reyestrə əlavə olunur, “Texnika reyestrə əlavə edildi” məlumatı çıxır və texnikanın geniş baxışı açılır. Menyuda {menyu("Texnika əlavə et")} yenidən basıldıqda forma boş açılır.</p>'
            return s
        fesil('elave', 'Texnikanın əlavə edilməsi', elave)

        def hisse():
            s = f'<p>{menyu("Sorğu yarat")} — ehtiyat hissələri və materiallar üçün təchizat sorğusunun aparıldığı səhifədir. İş üç addımdan ibarətdir; hər addım dərhal yazılır.</p>'
            F = 'zavod/06_sorgu.png'
            s += sekil(F, 'Sorğu yarat', Y(F, 'Yeni məktub qeyd et', 'Qeydiyyatdakı məktubu seç', 'Məktubu dəyiş', 'Təminat vəziyyəti:', 'Ehtiyat hissəsi əlavə et', 'Ehtiyat hissəsinin qəbulunu qeyd et', 'İcra qeydi əlavə et'), [
                (1, 'Yeni məktub qeyd et', 'Yeni məktubun sahələrini açır.'), (2, 'Qeydiyyatdakı məktubu seç', 'Artıq qeyd edilmiş məktubu axtarıb açır.'),
                (3, 'Məktubu dəyiş', 'Açıq məktubun məlumatını dəyişmək.'), (4, 'Təminat vəziyyəti', 'Məktubun hissələrinə görə avtomatik.'),
                (5, 'Ehtiyat hissəsi əlavə et', 'Yeni hissə və ya material (addım 2).'), (6, 'Qəbulunu qeyd et', 'Gələn miqdarın qeydi (bölmə 8.3).'),
                (7, 'İcra qeydi əlavə et', 'Təmin edən qurumla əlaqənin nəticəsi (addım 3).')])
            s += '<h3>8.1. Addım 1 — Təchizat məktubu</h3>'
            s += addimlar(f'{btn("+ Yeni məktub qeyd et")} basın və ya {btn("Qeydiyyatdakı məktubu seç")} ilə artıq qeyd edilmiş məktubu açın.',
                          'Məktubun sahələrini doldurun və ' + btn('Məktubu yadda saxla') + ' basın. Seçilmiş məktub yuxarıda göstərilir; ' + btn('Məktubu dəyiş') + ' ilə onu redaktə etmək olar.')
            s += cedvel(['Sahə', 'Nümunə'], [['Məktubun nömrəsi *', '190/26'], ['Məktubun tarixi *', '24.09.2026'], ['Təmin edən qurum *', 'Təchizat idarəsi'],
                ['Təmin edən qurum üzrə məsul icraçı *', 'mayor Səfərov N.'], ['Əlaqə nömrəsi', '(012) 498-67-35'], ['İcra üçün son tarix *', '04.11.2026'],
                ['Məktubun icra vəziyyəti *', 'İcrası davam edir / İcra başa çatıb']])
            s += sekil('zavod/06_sorgu_mektub.png', 'Məktubun sahələri')
            s += '<p>Addım 2 və 3 yalnız məktub yaradıldıqdan və ya seçildikdən sonra açılır.</p>'
            s += '<h3>8.2. Addım 2 — Ehtiyat hissələri və materiallar</h3>'
            s += addimlar(f'{btn("+ Ehtiyat hissəsi əlavə et")} basın.',
                          'Adı, kodu (varsa), ölçü vahidini və tələb olunan miqdarı yazın.',
                          f'{btn("Hansı texnika üçün")} ilə hissənin hansı texnika üçün sifariş edildiyini seçin (məcburi deyil).',
                          btn('Yadda saxla') + ' basın.')
            s += sekil('zavod/06_sorgu_hisse.png', 'Yeni ehtiyat hissəsi: “Hava filtri”, hansı texnika üçün — 2S1')
            s += qeyd('“Hansı texnika üçün” yalnız məlumatdır: texnika məktuba bağlanmır, onun statusu və təminat işarəsi dəyişmir. Texnika hissə gözləyirsə, “Təminat gözləyir” işarəsini texnikanın formasında (bölmə 3) özünüz qoyun.')
            s += '<h3>8.3. Qəbul</h3>'
            s += addimlar(f'{btn("Ehtiyat hissəsinin qəbulunu qeyd et")} basın.', 'Siyahıdan hissəni seçin, qəbul tarixini və gələn miqdarı yazın.',
                          btn('Yadda saxla') + ' basın. Qəbul edilmiş miqdar, qalıq və məktubun təminat vəziyyəti avtomatik yenilənir.')
            s += sekil('zavod/06_sorgu_qebul.png', 'Ehtiyat hissəsinin qəbulu')
            s += '<p>Hissə bir neçə dəfəyə gələ bilər — hər gəlişi ayrıca qeyd edin. Tələb olunandan çox miqdar yazıldıqda proqram təsdiq istəyir. Qəbul edilən hissələr <b>zavodun anbarına</b> düşür və texnikanın “Faktiki sərfiyyat” bölməsində seçilə bilər.</p>'
            s += '<h3>8.4. Məktubun təminat vəziyyəti</h3>' + cedvel(['Vəziyyət', 'Göstərilən'], [
                ['Hissə yazılmayıb', 'Təyin edilməyib'], ['Heç bir hissə qəbul olunmayıb', '<span class="cip o">Təmin edilməyib</span>'],
                ['Hissələrin bir qismi qəbul olunub', '<span class="cip p">Qismən təmin edilib</span>'], ['Bütün hissələr tam qəbul olunub', '<span class="cip y">Tam təmin edilib</span>']])
            s += '<h3>8.5. Addım 3 — Məktubun icrası üzrə qeydlər</h3>'
            s += '<p>Təmin edən qurumla hər əlaqə (zəng, yazışma) qeyd olunur: ' + btn('+ İcra qeydi əlavə et') + ' basın, əlaqə tarixini, danışığın nəticəsini və bildirilmiş gözlənilən təminat tarixini yazın. Yeni qeyd əvvəlkini silmir; son qeyd yuxarıda göstərilir.</p>'
            s += sekil('zavod/06_sorgu_icra.png', 'Məktubun icrası üzrə qeydlər')
            s += '<div class="ornek"><b>Nümunə qeyd:</b> 03.10.2026 — “Hissələr anbardadır, 10 gün ərzində göndəriləcək”, gözlənilən təminat tarixi 13.10.2026.</div>'
            s += '<h3>8.6. Anbar</h3><p>Anbar zavod üzrə ümumidir: eyni ad, kod və ölçü vahidli hissələr bir yerdə cəmlənir. <b>Anbarda qalıq = qəbul edilmiş − texnikalara sərf olunan.</b> Sərf əvvəlcə ən tez qəbul edilmiş məktubun hissələrindən çıxılır.</p>'
            return s
        fesil('hisse', 'Sorğu yarat — təchizat məktubu və ehtiyat hissələri', hisse)

        def redakte():
            s = f'<p>Texnikanın məlumatını dəyişmək üçün geniş baxışda {btn("Redaktə")} basın. Redaktə ayrıca səhifədə açılır (menyuda “Təmir reyestri” seçili qalır) və əlavə etmə formasına təsir etmir. Dəyişikliyi edib {btn("Yadda saxla")} basın.</p>'
            s += '<ul><li>Hər dəyişən sahə tarixçəyə “köhnə → yeni” kimi yazılır.</li><li>Plan tarixi dəyişəndə ilkin plan tarixçədə qalır.</li><li>Faktiki sərfiyyat qeydləri öz düyməsi ilə dərhal yazılır. Məktublar, hissələr və qəbullar “Sorğu yarat” səhifəsində aparılır.</li></ul>'
            s += qeyd('Üst qurumlar dəyişikliyi yalnız növbəti USB ixracından sonra görür.', 'meslehet')
            return s
        fesil('redakte', 'Məlumatın dəyişdirilməsi', redakte)

    # ---------------------------------------------------- Təchizat
    def techizat():
        F = f'{sq}/07_teminat.png'
        ad = (['Zavod:'] if not Z else []) + ['Məktub:', 'Təminat vəziyyəti:', 'Təmin edən qurum:', 'Məktubun tarixi (başlanğıc):', 'Axtarış:', 'Excel', 'Çap et (PDF)', 'Məktub sənədi (PDF)']
        izah = {'Zavod:': 'Bir zavod və ya hamısı.', 'Məktub:': 'Bir məktub və ya hamısı.', 'Təminat vəziyyəti:': 'Təmin edilməyib / Qismən / Tam.', 'Təmin edən qurum:': 'Təchizat edən qurum.',
                'Məktubun tarixi (başlanğıc):': 'Məktubun tarixinə görə aralıq (işarə qoyulanda tətbiq olunur).', 'Axtarış:': 'Hissənin adı, kodu və ya texnika.',
                'Excel': 'Siyahını Excel faylına çıxarır.', 'Çap et (PDF)': 'Siyahını PDF kimi çap edir.', 'Məktub sənədi (PDF)': 'Seçilmiş sətrin məktubu üzrə rəsmi sənəd.'}
        s = sekil(F, 'Sorğular üzrə təminat', Y(F, *ad), [(k, a.rstrip(':'), izah[a]) for k, a in enumerate(ad, 1)])
        s += f'<p>{menyu("Sorğular üzrə təminat")} — təchizat sorğuları əsasında qəbul edilmiş ehtiyat hissələri və materialların siyahısıdır. {"Məktublar və hissələr “Sorğu yarat” səhifəsində yaradılır; bu səhifə nəzarət üçündür." if Z else "Məktublar zavodlarda yaradılır və USB paketi ilə gəlir."} Hər sətir məktubun bir hissəsidir.</p>'
        s += '<h3>Cədvəlin sütunları</h3>' + cedvel(['Sütun', 'Mənası'], [['Məktub №', 'Məktubun nömrəsi və tarixi'], ['Hansı texnika üçün', 'Hissənin sifariş edildiyi texnika (məlumat üçün)'],
            ['Adı, Kodu, Ölçü vahidi', 'Ehtiyat hissəsi və ya material'], ['Tələb olunan', 'Məktubla tələb olunan miqdar'], ['Qəbul edilmiş', 'Qəbulların cəmi'],
            ['Texnikaya sərf olunub', 'Bu qəbuldan texnikalara faktiki sərf olunan miqdar'], ['Anbarda qalıq', 'Qəbul edilmiş − texnikaya sərf olunan'],
            ['Vəziyyət', 'Təmin edilməyib / Qismən təmin edilib / Tam təmin edilib']])
        s += '<p>Filtrlər: ' + ('' if Z else 'zavod, ') + 'məktub (və ya hamısı), təminat vəziyyəti, məktubun tarixi (başlanğıc – son), təmin edən qurum və axtarış (hissə, kod, texnika).</p>'
        s += f'<p>{btn("Excel")} və {btn("Çap et (PDF)")} göstərilən siyahını çıxarır. {btn("Məktub sənədi (PDF)")} seçilmiş sətrin məktubu üzrə rəsmi sənəd hazırlayır.' + (' Sətrə iki dəfə klik məktubu “Sorğu yarat” səhifəsində açır.' if Z else '') + '</p>'
        return s
    fesil('techizat', 'Sorğular üzrə təminat', techizat)

    # ---------------------------------------------------- Hesabat
    def hesabat():
        s = f'<p>{menyu("Hesabatlar")} hazır formada yeddi hesabat verir.</p>' + sekil(f'{sq}/08_hesabat.png', 'Hesabatlar', [(1, 27, 17), (2, 12.5, 22.5), (3, 25, 22.5), (4, 35, 22.5), (5, 14.5, 45), (6, 66, 45), (7, 50, 94)], [
            (1, 'Hesabat növü', 'Hesabatın seçimi (soldakı siyahı ilə eyni).'), (2, 'Hesabatı göstər', 'Ekranda qurur.'),
            (3, 'Excel-ə çıxar', 'Excel faylı.'), (4, 'Çap et (PDF)', 'Rəsmi formada A4 PDF.'),
            (5, 'Hesabatlar siyahısı', 'Tez keçid.'), (6, 'Hesabat', 'Başlıq, qurum, tarix, cədvəl, cəmi və izah.'),
            (7, 'Dövr', 'Dövr üzrə hesabatlar üçün başlanğıc və son tarix.')])
        s += cedvel(['Hesabat', 'Məzmunu'], [
            ['Zavodlar üzrə ümumi icra vəziyyəti', 'Seçilmiş tarixə təmiri davam edən, təhvil gözlənilən, təhvil verilmiş texnika'],
            ['Dövr ərzində qəbul, təmir və təhvil', 'Dövrdə qəbul edilmiş, təmiri tamamlanmış, təhvil verilmiş texnika'],
            ['İcra müddəti üzrə nəzarət', 'Plan tarixinə qalan günə görə bölgü, müddəti keçmiş və müəyyən edilməmiş'],
            ['Təmiri tamamlanıb, təhvil gözlənilən texnikalar', 'Siyahı və gözləmə günləri'],
            ['Ehtiyat hissələri üzrə təminat', 'Məktubların hər ehtiyat hissəsi üzrə: tələb olunan, qəbul edilmiş, qalıq. İki keçid: “Təmin edilməmiş” (qalığı olanlar) və “Tam təmin edilmiş”'],
            ['Texnika üzrə faktiki material sərfi', 'Texnikalara sərf olunan material (sərf tarixi — dövr üzrə). ' + btn('Texnika seç') + ' ilə bir texnika seçildikdə hesabat yalnız onun üçün qurulur: material, məktub №, vahid, miqdar və sərf tarixi'],
            ['Məktubların icra vəziyyəti', 'Məktubların sayı, icrası davam edən, başa çatmış, müddəti keçmiş']])
        if not Z: s += '<p>Zavod seçimi ilə hesabat bir zavod və ya bütün zavodlar üzrə qurulur.</p>'
        FT = f'{sq}/08_hesabat_teminat.png'
        s += '<h3>Ehtiyat hissələri üzrə təminat</h3>' + sekil(FT, 'Ehtiyat hissələri üzrə təminat', Y(FT, 'Təmin edilməmiş ehtiyat hissələri', 'Tam təmin edilmiş ehtiyat hissələri'), [
            (1, 'Təmin edilməmiş ehtiyat hissələri', 'Qalığı olan hissələr (heç gəlməyən və qismən gələnlər). Açılanda bu seçilir.'),
            (2, 'Tam təmin edilmiş ehtiyat hissələri', 'Tələb olunan miqdarı tam gəlmiş hissələr. Qalıq sıfır olanda hissə avtomatik bu siyahıya keçir.')])
        s += '<p>Çap və Excel faylının başlığında hansı siyahının olduğu yazılır.</p>'
        FS = f'{sq}/08_hesabat_serf_tex.png' if Z else f'{sq}/08_hesabat_serf.png'
        s += '<h3>Texnika üzrə faktiki material sərfi</h3>' + sekil(FS, 'Texnika üzrə faktiki material sərfi' + (' — bir texnika seçilib' if Z else ''), Y(FS, 'Texnika seç', 'Bütün texnikalar', 'Başlanğıc tarixi:'), [
            (1, 'Texnika seç', 'Zavodun texnikaları arasında axtarış; seçiləndə hesabat yalnız həmin texnika üçün qurulur və başlıqda onun adı yazılır.'),
            (2, 'Bütün texnikalar', 'Yenidən bütün texnikalar üzrə ümumi hesabat (texnika seçiləndə görünür).'),
            (3, 'Dövr', 'Sərf tarixi üzrə başlanğıc və son tarix.')])
        s += cedvel(['Görünüş', 'Sütunlar'], [['Bütün texnikalar', 'Texnika, material, məktub №, ölçü vahidi, miqdar (texnika və material üzrə cəm)'],
                                              ['Bir texnika', 'Material, məktub №, ölçü vahidi, miqdar, sərf tarixi (hər sərf ayrıca sətir)']])
        s += qeyd('“Məktub №” — sərf olunan materialın hansı məktubun qəbulundan çıxdığı. Anbar qaydası ilə hesablanır: ilk qəbul edilən ilk sərf olunur. “Yeni ad” ilə yazılan materialda “—” göstərilir.')
        return s
    fesil('hesabat', 'Hesabatlar', hesabat)

    # ---------------------------------------------------- Bildiriş
    def bildiris():
        s = f'<p>Proqram açılanda və hər saat bildiriş qaydalarını yoxlayır. Yeni bildirişlərin sayı başlıq zolağında zəng nişanının üstündə görünür; {menyu("Bildirişlər")} bölməsində hamısı siyahı şəklindədir.</p>'
        s += sekil(f'{sq}/09_bildiris.png', 'Bildirişlər')
        rows = [['Gecikmə', 'Plan tarixi keçmiş, təmiri davam edən texnika'], ['Son tarix', 'Plan tarixinə 3 gün və az qalıb'],
                ['Hissə', 'Texnika “Təminat gözləyir” işarəlidir və diaqnostikadan bəri uzun müddət (standart 30 gün) keçib'],
                ['Yeni qəbul', 'Yeni texnika qeydə alınıb']]
        if not Z: rows.append(['Paket', 'Zavod müəyyən müddət (standart 7 gün) USB paketi göndərməyib'])
        s += cedvel(['Növ', 'Nə vaxt yaranır'], rows)
        s += f'<p>Bildirişə klik — texnikanın geniş baxışını açır və bildirişi oxunmuş edir. {btn("Hamısını oxunmuş et")} sayğacı sıfırlayır. Eyni bildiriş gündə bir dəfə yaranır; çox olduqda bir ümumi bildirişlə birləşdirilir.</p>'
        return s
    fesil('bildiris', 'Bildirişlər', bildiris)

    # ---------------------------------------------------- USB
    def usb():
        s = '<p>Qurumlar arasında məlumat USB yaddaşla ötürülür. Paket <b>sıxılır, AES-256 ilə şifrələnir və göndərən kompüterin rəqəmsal imzası ilə imzalanır</b>. Paketi yalnız alıcı qurum aça bilər; yolda dəyişdirilmiş və ya saxta paket qəbul olunmur.</p>'
        s += sekil(f'{sq}/10_usb.png', 'İxrac / İdxal')
        if not I:
            alici = 'Təmir və Texniki Təminat xidməti' if Z else 'Müdafiə Sənayesi İdarəsi'
            s += f'<h3>USB-yə ixrac (paket göndərmək)</h3><p>Paketə son ixracdan bəri dəyişən bütün qeydlər (texnika, tarixçə, məktublar) daxil olur. Alıcı: <b>{alici}</b>.</p>'
            s += addimlar('USB yaddaşı kompüterə taxın.', f'{menyu("İxrac / İdxal")} bölməsini açın. <b>USB yaddaş</b> siyahısından diski seçin (görünmürsə, yeniləmə düyməsini basın və ya “Başqa qovluq seç...”).',
                          '<b>Paket nömrəsi</b> və <b>Dəyişiklik</b> sayını yoxlayın.', f'{btn("USB-yə ixrac et")} basın. Sağdakı “Proses” hissəsində 5 addım ardıcıl yerinə yetirilir: dəyişikliklər toplanır, paket sıxılır, şifrələnir, imzalanır, USB-yə yazılıb yoxlanılır.',
                          '“✓ ... hazırdır” yazısından sonra USB-ni çıxarın və alıcı quruma çatdırın.')
            s += qeyd('Paket nömrələri ardıcıldır (məs. TQX-Z1-000001, 000002...). Alıcı paketləri sıra ilə qəbul edir; ötürülmüş paketi silməyin. Ixracı mütəmadi — ən azı həftədə bir dəfə — aparın; uzun müddət ixrac edilmədikdə səhifədə xəbərdarlıq çıxır və üst quruma bildiriş gedir.', 'diqqet')
        if not Z:
            mənbə = 'təmir zavodlarından' if X else 'Təmir və Texniki Təminat xidmətindən'
            s += f'<h3>USB-dən idxal (paket qəbul etmək)</h3><p>Paketlər {mənbə} gəlir.</p>'
            s += addimlar('USB yaddaşı taxın. <b>USB-dəki paketlər</b> siyahısında paketlər görünəcək (görünmürsə, ' + btn('Fayl seç...') + ' ilə paketi seçin).',
                          'Paketi seçin və ' + btn('Seçilmiş paketi idxal et') + ' basın.',
                          '“Proses” hissəsində yoxlamalar aparılır: paket oxunur, imza yoxlanılır, şifrə açılır, ardıcıllıq nömrəsi yoxlanılır, dəyişikliklər bazaya tətbiq olunur.',
                          'Nəticədə yeni, dəyişən və silinən qeydlərin sayı göstərilir və <b>Sinxronizasiya tarixçəsi</b>nə yazılır.')
            s += qeyd('Paket rədd edilirsə: imza tanınmırsa — göndərənin açarı bu kompüterə idxal edilməyib (administrator, Əlavə A); ardıcıllıq pozulubsa — əvvəlki paket hələ qəbul edilməyib, əvvəlcə onu idxal edin; zavod başqa zavodun məlumatını göndərə bilməz.', 'diqqet')
        s += '<h3>Sinxronizasiya tarixçəsi</h3><p>Bütün ixrac və idxallar: tarix, əməliyyat, paket, yeni / dəyişiklik / silinmə sayı, nəticə və qeyd.</p>'
        return s
    fesil('usb', 'USB ilə məlumat mübadiləsi', usb)

    # ---------------------------------------------------- Ehtiyat
    def ehtiyat():
        s = f'<p>{menyu("Tənzimləmə")} bölməsində iki nişan var: <b>Ehtiyat nüsxə</b> və <b>Loglar</b>.</p>'
        s += sekil(f'{sq}/11_ehtiyat.png', 'Ehtiyat nüsxə')
        s += '<h3>Avtomatik nüsxə</h3><p>Proqram hər gün ilk açılışda bazanın sıxılmış nüsxəsini götürür. Son nüsxələr saxlanılır (standart — 30), köhnələri avtomatik silinir. Nüsxələr <code>C:\\ProgramData\\TexnikaReyestri\\Backup</code> qovluğunda saxlanılır.</p>'
        s += '<h3>Əl ilə nüsxə</h3><p>' + btn('İndi nüsxə al') + ' — mühüm işdən (məs. böyük Excel idxalından) əvvəl istifadə edin.</p>'
        s += '<h3>Bərpa</h3>' + addimlar('“Nüsxələr” siyahısında lazımi tarixi tapın.', btn('Bərpa et') + ' basın və təsdiqləyin.', 'Proqram bazanı həmin nüsxə ilə əvəz edir və yenidən açılır.')
        s += qeyd('Bərpadan sonra nüsxə tarixindən sonra edilmiş bütün dəyişikliklər itir. Bərpa yalnız zərurət olduqda və rəhbərliyin razılığı ilə aparılır.', 'diqqet')
        s += qeyd(btn('Qovluğu aç') + ' ilə nüsxə qovluğunu açıb vaxtaşırı başqa diskə və ya xarici yaddaşa köçürün — kompüterin diski sıradan çıxsa, nüsxələr də itməsin.', 'meslehet')
        s += '<h3>Loglar</h3>' + sekil(f'{sq}/12_loglar.png', 'Loglar') + '<p>Bütün əməliyyatların jurnalı: giriş və çıxış, əlavə, redaktə, silmə, ixrac, idxal, ehtiyat nüsxə, xətalar. Tarix aralığı, növ və mətnə görə axtarış aparılır. Son 1000 qeyd göstərilir. Zavod istifadəçisi yalnız öz zavodunun loglarını görür.</p>'
        return s
    fesil('ehtiyat', 'Ehtiyat nüsxə və loglar', ehtiyat)

    # ---------------------------------------------------- Ssenari
    def ssenari():
        if Z:
            s = '<h3>Texnikanın qəbulundan təhvilinə qədər</h3>' + addimlar(
                '<b>Qəbul.</b> Texnika naryadla gəlir → ' + menyu('Texnika əlavə et') + ' → bölmə 1 (növ, marka, zavod nömrəsi, naryad, daxilolma tarixi, göndərən) və bölmə 2 (göstəricilər) doldurulur → bölmə 3-də diaqnostika tarixi, nasazlıqlar, təmir növü, müddət yazılır → ' + btn('Yadda saxla') + '.',
                '<b>Təmirə başlanması.</b> Redaktədə icra statusu “Təmirdədir” seçilir.',
                '<b>Ehtiyat hissələri.</b> ' + menyu('Sorğu yarat') + ' → məktub qeyd olunur (və ya qeydiyyatdakı seçilir) → lazımi hissələr “hansı texnika üçün” göstərilərək əlavə edilir. Texnikanın formasında “Təminat gözləyir” işarələnir. Hər zəng və cavab icra qeydi kimi yazılır.',
                '<b>Hissələrin gəlməsi.</b> Hər gəliş “Sorğu yarat”da “Ehtiyat hissəsinin qəbulu” ilə qeyd olunur — hissə anbara düşür. Hamısı gəldikdə “Təminat gözləyir” işarəsi götürülür.',
                '<b>Sərf.</b> Texnikanın formasında bölmə 4 “Faktiki sərfiyyat”da anbardan götürülən material və miqdarı yazılır.',
                '<b>Gecikmə.</b> Plan pozulursa, bölmə 3-də səbəb və yeni plan tarixi yazılır.',
                '<b>Tamamlanma.</b> Bölmə 5-də faktiki tamamlanma tarixi və görülən işlər yazılır — status avtomatik “Təmir tamamlanıb, təhvil gözlənilir”.',
                '<b>Təhvil.</b> Faktiki təhvil tarixi, təhvil sənədi və təhvil alan qurum yazılır — status “Təhvil verilib”.',
                '<b>Hesabat.</b> Həftəlik ' + menyu('İxrac / İdxal') + ' bölməsində USB-yə ixrac edilir və paket Xidmətə çatdırılır.')
        else:
            s = '<h3>Həftəlik nəzarət</h3>' + addimlar('Gələn USB paketlərini ' + menyu('İxrac / İdxal') + ' bölməsində sıra ilə idxal edin.',
                ('Paketi göndərməyən zavodları <b>Bildirişlər</b>də (“Paket”) və ana səhifədə yoxlayın.'),
                'Ana səhifədə “İcra müddəti keçmiş” kartına klik edin, gecikənləri zavodlar üzrə nəzərdən keçirin.',
                'Ehtiyat hissələri üzrə vəziyyəti reyestrdə “Təminat gözləyir” filtri və ' + menyu('Sorğular üzrə təminat') + ' bölməsi ilə yoxlayın.',
                'Lazımi hesabatı ' + menyu('Hesabatlar') + ' bölməsində qurub PDF və ya Excel kimi saxlayın.' + (' Sonra USB-yə ixrac edib paketi İdarəyə çatdırın.' if X else ''))
        return s
    fesil('ssenari', 'İş ardıcıllığı — nümunə', ssenari)

    # ---------------------------------------------------- FAQ
    def faq():
        l = [('Parolu unutmuşam və ya hesabım bloklanıb.', 'Administratora müraciət edin: o, parolu sıfırlayıb müvəqqəti parol verəcək. İlk girişdə yeni parol təyin edəcəksiniz.'),
             ('Proqram özü bağlandı və giriş istədi.', 'Uzun müddət əməliyyat olmadıqda sessiya təhlükəsizlik üçün bağlanır. Yenidən daxil olun.'),
             ('Texnikanı reyestrdə tapa bilmirəm.', 'Filtrləri “Hamısı” vəziyyətinə qaytarın, tarix aralığının işarəsini götürün və axtarış xanasını təmizləyin.')]
        if Z:
            l += [('“Bu zavod nömrəsi artıq bazada var” xətası çıxır.', 'Bu nömrə ilə texnika artıq qeydə alınıb. Axtarışla tapın və redaktə edin.'),
                  ('Yadda saxlamır, bölmə dəyişir.', 'Məcburi (*) sahə boş qalıb. Açılan bölmədə kursorun olduğu sahəni doldurun; xəta mətni ekranın aşağısında göstərilir.'),
                  ('Statusu “Təhvil verilib” seçə bilmirəm.', 'Bu status avtomatikdir — bölmə 5-də faktiki təhvil tarixini yazın.'),
                  ('Sərfiyyatda material siyahıda yoxdur.', 'Material hələ heç bir sorğu üzrə qəbul edilməyib və ya anbarda qalığı bitib. Qəbulu “Sorğu yarat”da qeyd edin, ya da “Yeni ad əlavə et” ilə yazın.')]
        if not Z:
            l += [('Zavodun son dəyişikliklərini görmürəm.', 'Məlumat yalnız USB paketi idxal edildikdən sonra görünür. Zavoddan yeni paket tələb edin.'),
                  ('Paket “imza tanınmır” xətası ilə rədd edildi.', 'Göndərən kompüterin açıq açarı bu kompüterə idxal edilməyib. Administratora müraciət edin.')]
        l += [('Səhvən məlumat silinib və ya pozulub.', 'Ehtiyat nüsxədən bərpa mümkündür (bölmə “Ehtiyat nüsxə və loglar”). Bərpadan əvvəl administratorla məsləhətləşin.')]
        return ''.join(f'<details class="faq"><summary>{q}</summary><p>{c}</p></details>' for q, c in l)
    fesil('faq', 'Tez-tez verilən suallar', faq)
    return F


def elaveler(r):
    A = []
    _bolme[0] = 0
    def admin():
        _sekil_say[0] = 0; _bolme[0] = 'A'
        s = '<p>Administrator paneli istifadəçilərin, qurumların, təhlükəsizlik qaydalarının və şifrələmə açarlarının idarə olunması üçündür. Panel administrator parolu ilə açılır; giriş qaydası administratora ayrıca bildirilir.</p>'
        s += sekil('forms/21_admin.png', 'Administrator paneli — İstifadəçilər', en=1000)
        s += cedvel(['Bölmə', 'Təyinatı'], [
            ['İstifadəçilər', 'Yeni istifadəçi yaratmaq (login, ad soyad, qurum, rol, parol), redaktə, parolu sıfırlamaq, bloklamaq. İstifadəçi silinmir, bloklanır — onun etdiyi dəyişikliklər tarixçədə adı ilə qalır.'],
            ['Qurumlar', 'Zavodların və üst qurumların siyahısı və strukturu. Kod dəyişmir, ad dəyişə bilər.'],
            ['Bu kompüter', 'Kompüterin qurumu, baza faylı, ölçüsü, texnika sayı, versiya; bazanın bütövlüyünü yoxlamaq və sıxmaq.'],
            ['Təhlükəsizlik', 'Parolun minimal uzunluğu və etibarlılıq müddəti, səhv cəhd limiti, fəaliyyətsizlikdə avtomatik çıxış, admin parolunun dəyişdirilməsi.'],
            ['USB açarları', 'Bu kompüterin açarı və barmaq izi; açıq açarın USB-yə yazılması; digər qurumların açarlarının idxalı. Yeni açarın barmaq izini telefonla və ya şəxsən yoxlayın.'],
            ['Sistem', 'Bildiriş həddləri (neçə gün qalmış, neçə gün sonra, hissə və paket müddətləri), saxlanılan nüsxə sayı, keçid animasiyaları.'],
            ['Admin jurnalı', 'Administratorun bütün əməliyyatları.']])
        s += sekil('forms/28_istifadeci_yeni.png', 'Yeni istifadəçi', en=560)
        s += '<h3>USB mübadiləsinin ilkin qurulması</h3>' + addimlar('Hər kompüterdə <b>USB açarları</b> bölməsində ' + btn('Açıq açarı USB-yə yaz') + ' basılır.',
            'Zavodun açıq açarı Xidmətin, Xidmətin açıq açarı İdarənin kompüterində ' + btn('Açar faylını idxal et') + ' ilə idxal olunur.',
            'Barmaq izi göndərən tərəflə ayrıca rabitə vasitəsi ilə tutuşdurulur.')
        return s
    def lugat():
        return cedvel(['Termin', 'İzah'], [
            ['Naryad', 'Texnikanın təmirə göndərilməsi barədə sənəd'], ['Zavod nömrəsi (inventar kodu)', 'Texnikanın unikal nömrəsi; reyestrdə təkrarlana bilməz'],
            ['Diaqnostika tarixi', 'İcra müddətinin başlanğıcı'], ['Plan üzrə bitmə tarixi', 'Diaqnostika tarixi + icra müddəti, və ya yeni plan tarixi'],
            ['Gözlənilən bitmə tarixi', 'Real bitmə proqnozu; planı dəyişmir'], ['Təminat gözləyir', 'Texnikanın ehtiyat hissəsi gözlədiyini bildirən işarə; status deyil'],
            ['Məktubun təminat vəziyyəti', 'Məktubun hissələrinin qəbul vəziyyəti — avtomatik'], ['Anbar', 'Zavodda sorğular üzrə qəbul edilmiş, hələ sərf olunmamış hissələr'],
            ['Faktiki sərfiyyat', 'Texnikanın təmirinə faktiki sərf olunan material və ehtiyat hissələri'],
            ['Təchizat məktubu', 'Ehtiyat hissələrinin tələbi üzrə rəsmi məktub'], ['İcra qeydi', 'Məktub üzrə təmin edən qurumla əlaqənin nəticəsi'],
            ['Paket', 'USB ilə ötürülən şifrələnmiş və imzalanmış məlumat faylı'], ['Ehtiyat nüsxə', 'Bazanın sıxılmış surəti']])
    _bolme[0] = 'A'; a = admin()
    A.append(('admin', 'Əlavə A. Administrator paneli', a))
    _bolme[0] = 'B'; A.append(('lugat', 'Əlavə B. Terminlər', lugat()))
    return A


CSS = """
:root{--t:#0F4C4A;--t2:#1C6B68;--ac:#E08A00;--ink:#1B2B2A;--mut:#5D7472;--line:#D4E2E0;--alt:#F4F8F7;--bg:#fff}
*{box-sizing:border-box}html{scroll-behavior:smooth}
body{margin:0;font:15px/1.6 "Segoe UI",Arial,sans-serif;color:var(--ink);background:var(--bg)}
aside{position:fixed;top:0;left:0;bottom:0;width:290px;overflow:auto;background:var(--alt);color:var(--ink);padding:22px 16px;border-right:1px solid var(--line);border-left:4px solid var(--ac)}
aside h1{font:600 17px "Bahnschrift","Segoe UI";margin:0 0 2px;color:var(--t);letter-spacing:.3px;line-height:1.25}
aside .alt{font-size:12px;color:var(--ac);font-weight:600;margin-bottom:18px}
aside a{display:block;color:var(--ink);text-decoration:none;padding:6px 10px;border-radius:6px;font-size:14px;border-left:3px solid transparent}
aside a:hover{background:#E3EEEC;color:var(--t);border-left-color:var(--t)}
aside input{width:100%;padding:8px 10px;border-radius:6px;border:1px solid var(--line);background:#fff;margin:0 0 14px;font:inherit}
main{margin-left:290px;padding:34px 48px 80px;max-width:1240px}
.uz{border-bottom:3px solid var(--t);padding-bottom:18px;margin-bottom:26px}
.uz .k{font:600 13px "Segoe UI";color:var(--ac);letter-spacing:1px;text-transform:uppercase}
.uz h1{font:700 34px "Bahnschrift","Segoe UI";color:var(--t);margin:4px 0}
.uz p{margin:0;color:var(--mut)}
details.fs{border:1px solid var(--line);border-radius:10px;margin:0 0 14px;background:#fff}
details.fs>summary{cursor:pointer;list-style:none;padding:14px 20px;font:600 20px "Bahnschrift","Segoe UI";color:var(--t);display:flex;align-items:center}
details.fs>summary::-webkit-details-marker{display:none}
details.fs>summary:before{content:"›";display:inline-block;width:22px;transition:transform .15s;font-size:24px}
details.fs[open]>summary:before{transform:rotate(90deg)}
details.fs>.ic{padding:0 26px 22px}
h3{color:var(--t);font:600 17px "Segoe UI";margin:26px 0 8px}
figure{margin:18px 0 8px}
.sek{position:relative;display:inline-block;max-width:100%;border:1px solid var(--line);border-radius:8px;overflow:hidden;box-shadow:0 2px 10px rgba(15,76,74,.08)}
.sek img{display:block;max-width:100%}
figcaption{font-size:13px;color:var(--mut);margin-top:6px;font-style:italic}
.nis{position:absolute;transform:translate(-50%,-50%);width:24px;height:24px;border-radius:50%;background:var(--ac);color:#fff;font:700 12px/24px "Segoe UI";text-align:center;box-shadow:0 0 0 2px #fff,0 2px 6px rgba(0,0,0,.35)}
.nis.s{position:static;transform:none;display:inline-block;box-shadow:none}
table{border-collapse:collapse;width:100%;margin:10px 0 14px;font-size:14px}
th{background:var(--alt);color:var(--t);text-align:left;font-weight:600}
th,td{border:1px solid var(--line);padding:7px 10px;vertical-align:top}
table.izah td:first-child{width:46px;text-align:center}
.btn{display:inline-block;background:var(--t);color:#fff;border-radius:5px;padding:0 8px;font-size:13px;white-space:nowrap}
.btnad{font-weight:600;color:var(--t)}
.ik{height:26px;vertical-align:middle;margin:0 4px;border-radius:6px;box-shadow:0 1px 4px rgba(15,76,74,.25)}
.ik.m{height:30px;border-radius:8px}
.mn{font-weight:600;color:var(--t);border-bottom:2px solid var(--ac)}
.qt{border-left:4px solid var(--t2);background:var(--alt);padding:10px 14px;margin:12px 0;border-radius:0 6px 6px 0}
.qt.diqqet{border-color:#C94A44;background:#FBEFEE}.qt.meslehet{border-color:var(--ac);background:#FFF6E6}
ol.add{counter-reset:a;list-style:none;padding:0}
ol.add li{counter-increment:a;position:relative;padding:4px 0 6px 38px}
ol.add li:before{content:counter(a);position:absolute;left:0;top:4px;width:26px;height:26px;border-radius:50%;background:var(--t);color:#fff;text-align:center;font:600 13px/26px "Segoe UI"}
.axin{display:flex;align-items:center;gap:10px;flex-wrap:wrap;margin:10px 0}
.axin div{flex:1;min-width:200px;border:1px solid var(--line);border-radius:8px;padding:12px;background:var(--alt)}
.axin small{display:block;color:var(--mut)}.axin i{color:var(--ac);font-style:normal;font-weight:700}
.cip{display:inline-block;border-radius:10px;padding:1px 9px;font-size:12px;white-space:nowrap}
.cip.g{background:#E7ECEB;color:#4A5A59}.cip.b{background:#DCEBF6;color:#1F5F8B}.cip.o{background:#FCE7D2;color:#A65A10}
.cip.y{background:#DDF1E3;color:#23703E}.cip.r{background:#F8DCDB;color:#A3302C}.cip.p{background:#ECE3FA;color:#5B3A99}
.ornek{background:#F7FBFA;border:1px dashed var(--t2);border-radius:6px;padding:10px 14px}
details.faq{border-bottom:1px solid var(--line);padding:8px 0}details.faq summary{cursor:pointer;font-weight:600;color:var(--t)}
code{background:var(--alt);padding:1px 5px;border-radius:4px}
.ust{display:flex;gap:8px;margin-bottom:12px}.ust button{font:inherit;font-size:13px;border:1px solid var(--line);background:#fff;border-radius:6px;padding:5px 12px;cursor:pointer;color:var(--t)}
footer{margin-top:40px;color:var(--mut);font-size:12px;border-top:1px solid var(--line);padding-top:10px}
@media print{aside,.ust{display:none}main{margin:0;padding:0;max-width:none}details.fs{border:0;break-inside:auto}details.fs>summary:before{content:""}}
@media(max-width:900px){aside{position:static;width:auto}main{margin:0;padding:20px}}
"""

JS = """
const d=[...document.querySelectorAll('details.fs')];
function ac(v){d.forEach(x=>x.open=v)}
document.querySelectorAll('aside a').forEach(a=>a.onclick=()=>{const t=document.querySelector(a.getAttribute('href'));if(t)t.open=true});
window.onbeforeprint=()=>ac(true);
document.getElementById('axt').oninput=e=>{const q=e.target.value.toLowerCase();document.querySelectorAll('aside a').forEach(a=>{const t=document.querySelector(a.getAttribute('href'));a.style.display=!q||(t&&t.textContent.toLowerCase().includes(q))?'':'none'})};
"""


def qur(r):
    _bolme[0] = 0
    R = ROLLAR[r]
    F = fesiller(r) + elaveler(r)
    nav = ''.join(f'<a href="#{i}">{a}</a>' for i, a, _ in F)
    body = ''.join(f'<details class="fs" id="{i}"{" open" if k == 0 else ""}><summary>{a}</summary><div class="ic">{m}</div></details>' for k, (i, a, m) in enumerate(F))
    return f"""<!doctype html><html lang="az"><head><meta charset="utf-8"><meta name="viewport" content="width=device-width,initial-scale=1">
<title>Təmir və Texniki Təminat Xidməti — İstifadəçi təlimatı ({R['ad']})</title><style>{CSS}</style></head><body>
<aside><h1>TƏMİR VƏ TEXNİKİ TƏMİNAT XİDMƏTİ</h1><div class="alt">İstifadəçi təlimatı · {R['ad']}</div><input id="axt" placeholder="Təlimatda axtar...">{nav}</aside>
<main><div class="uz"><div class="k">İstifadəçi təlimatı · versiya {VERSIYA}</div><h1>Təmir və Texniki Təminat Xidməti</h1>
<p>Məlumat bazası — təmir zavodları üzrə texnikanın qəbulu, təmiri, ehtiyat hissələri ilə təminatı və təhvilinin uçotu. {R['tam']}.</p></div>
<div class="ust"><button onclick="ac(true)">Hamısını aç</button><button onclick="ac(false)">Hamısını yığ</button><button onclick="ac(true);print()">Çap et</button></div>
{body}<footer>Təmir və Texniki Təminat Xidməti · Məlumat bazası · İstifadəçi təlimatı ({R['ad']}) · versiya {VERSIYA}</footer></main><script>{JS}</script></body></html>"""


if __name__ == '__main__':
    os.makedirs(CIX, exist_ok=True)
    for r, R in ROLLAR.items():
        with open(os.path.join(CIX, R['fayl']), 'w', encoding='utf-8') as f:
            f.write(qur(r))
        print(R['fayl'], os.path.getsize(os.path.join(CIX, R['fayl'])) // 1024, 'KB')
