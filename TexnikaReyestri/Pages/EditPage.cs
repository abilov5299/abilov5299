using System.Globalization;
using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Forms;

namespace TexnikaReyestri.Pages
{
    /// <summary>
    /// "Texnika əlavə et" / redaktə: 5 bölmə (Qəbul və sənədlər, İstismar göstəriciləri və son təmir, Təmirin icrası,
    /// Faktiki sərfiyyat, Tamamlanma və təhvil). Məktub və ehtiyat hissələri ayrıca "Sorğu yarat" səhifəsindədir. Sahə kontrolları "in_SaheKodu" adlıdır və kodla qurulur.
    /// Statuslar "Təmir tamamlanıb..." və "Təhvil verilib", plan bitmə tarixi və təminat vəziyyəti avtomatik hesablanır.
    /// </summary>
    public partial class EditPage : PageBase
    {
        private long? _id; private string _guid;
        private readonly Dictionary<string, Control> _in = new();
        private readonly Dictionary<string, Label> _lbl = new();

        // Bölmə 2: göstəricilər
        private TableLayoutPanel tlpGost;
        // Bölmə 3: avtomatik xanalar
        private RoundTextBox txtPlan, txtQalan; private CheckBox chkTeminat;
        // Bölmə 4: faktiki sərfiyyat (anbar — zavodun qəbul edilmiş hissələri)
        private List<Serf> _serfler = new(); private List<AnbarMal> _anbar = new();
        private ModernGrid gridSerf; private RoundPanel pnlSerf;
        private RoundComboBox sMal; private RoundTextBox sAd, sKod, sVahid, sMiqdar; private RoundDatePicker sTarix; private FlatButton bYeniAd; private bool _yeniAd;
        // Bölmə 5
        private RoundComboBox cmbTehvilAlan; private RoundTextBox txtDigerQurum; private RoundTextBox txtGozleme;
        private Label lblQebulInfo;

        public EditPage()
        {
            InitializeComponent();
            Bolme1(); Bolme2(); Bolme3(); Bolme4(); Bolme5();
            pnlHead.Resize += (_, _) => { btnSave.Left = pnlHead.Width - btnSave.Width; btnNextG.Left = btnSave.Left - btnNextG.Width - 8; btnPrevG.Left = btnNextG.Left - btnPrevG.Width - 8; };
        }

        private void EditPage_Load(object sender, EventArgs e)
        {
            foreach (TabPage t in tabs.TabPages) t.BackColor = Theme.Bg;
        }

        public bool YeniRejim { get; private set; }

        // ===================================================== Qurma köməkçiləri
        internal static Font F(float s, bool b = false) => new(b ? "Segoe UI Semibold" : "Segoe UI", s);

        internal static Panel Scroll(TabPage t)
        {
            var p = new Panel { Dock = DockStyle.Fill, AutoScroll = true, Padding = new Padding(0, 0, 8, 8) };
            t.Controls.Add(p); return p;
        }

        /// <summary>Başlıqlı kart: içində N sütunlu cədvəl. Kartın hündürlüyü məzmuna görə dəyişir.</summary>
        internal static TableLayoutPanel Kart(Panel host, string basliq, int sutun, string alt = null, Control sag = null)
        {
            var card = new RoundPanel { Dock = DockStyle.Top, Padding = new Padding(18, 12, 18, 12) };
            var tl = new TableLayoutPanel { Dock = DockStyle.Top, ColumnCount = sutun, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, BackColor = Color.Transparent };
            for (int i = 0; i < sutun; i++) tl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / sutun));
            var head = new Panel { Dock = DockStyle.Top, Height = alt == null ? 36 : 56, BackColor = Color.Transparent };
            head.Controls.Add(new Label { Text = basliq, AutoSize = true, Font = new Font("Bahnschrift SemiBold", 15f), Tag = "title", Location = new Point(0, 0) });
            if (alt != null) head.Controls.Add(new Label { Text = alt, AutoSize = true, Font = F(9f), Tag = "muted", Location = new Point(1, 32) });
            if (sag != null) { sag.Anchor = AnchorStyles.Top | AnchorStyles.Right; head.Controls.Add(sag); head.Resize += (_, _) => sag.Left = head.Width - sag.Width; }
            card.Controls.Add(tl); card.Controls.Add(head);
            void Olc() => card.Height = head.Height + tl.Height + card.Padding.Vertical + 4;
            tl.SizeChanged += (_, _) => Olc(); Olc();
            // Dock=Top: ön plandakı (indeksi kiçik) ən sonda yerləşir — kartlar yazılış sırası ilə yuxarıdan aşağı düzülsün
            var ara = new Panel { Dock = DockStyle.Top, Height = 12, BackColor = Color.Transparent };
            host.Controls.Add(card); card.BringToFront(); host.Controls.Add(ara); ara.BringToFront();
            return tl;
        }

        internal static Label Ipucu(string t) => new() { Text = t, AutoSize = true, Font = F(8.5f), Tag = "muted", Margin = new Padding(0, 2, 18, 6), MaximumSize = new Size(900, 0) };

        /// <summary>Sahə: başlıq + kontrol (+ ipucu). Kod "in_Kod" adı ilə qeyd olunur.</summary>
        private Panel Sahe(string kod, string ad, Control c, string ipucu = null, bool mecburi = false, int hundur = 0)
        {
            var p = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 18, 6), BackColor = Color.Transparent };
            var l = new Label { Text = ad + (mecburi ? " *" : ""), Dock = DockStyle.Top, Height = 22, Font = F(9f), Tag = "muted" };
            c.Dock = DockStyle.Top; c.Font = F(10f);
            if (hundur > 0) c.Height = hundur;
            if (c is RoundTextBox { Multiline: false } t0) t0.Height = 32;
            p.Controls.Add(c); p.Controls.Add(l);
            Label h = null;
            if (ipucu != null) { h = new Label { Text = ipucu, Dock = DockStyle.Top, AutoSize = false, Height = 20, Font = F(8.5f), Tag = "muted" }; p.Controls.Add(h); h.BringToFront(); }
            p.Height = l.Height + c.Height + (h?.Height ?? 0) + 8;
            if (kod != null) { c.Name = "in_" + kod; _in[kod] = c; _lbl[kod] = l; }
            return p;
        }

        internal static RoundTextBox Txt(bool coxSetir = false, bool ro = false) => new() { Multiline = coxSetir, ReadOnly = ro, ScrollBars = coxSetir ? ScrollBars.Vertical : ScrollBars.None };
        // Açılan siyahının bazaya yazılan dəyərləri (ekranda tərcümə və ya ekran adı göstərilə bilər — Lang.Apply, Secimler.Ekran)
        private readonly Dictionary<ComboBox, List<string>> _orig = new();
        private RoundComboBox Cmb(IEnumerable<string> items, bool bos = true, Func<string, string> ekran = null)
        {
            var c = new RoundComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Height = 30 };
            var o = (bos ? new[] { "" } : Array.Empty<string>()).Concat(items).ToList();
            c.Items.AddRange(o.Select(x => (object)(ekran?.Invoke(x) ?? x)).ToArray()); _orig[c] = o;
            if (c.Items.Count > 0) c.SelectedIndex = 0; return c;
        }
        private string Deyer(ComboBox c) => (_orig.TryGetValue(c, out var o) && c.SelectedIndex >= 0 && c.SelectedIndex < o.Count ? o[c.SelectedIndex] : c.SelectedItem as string) is { Length: > 0 } v ? v : null;
        private void Sec(ComboBox c, string v)
        {
            if (!_orig.TryGetValue(c, out var o)) { c.SelectedItem = v; return; }
            int i = o.IndexOf(v ?? "");
            if (i < 0 && !string.IsNullOrEmpty(v)) { o.Add(v); c.Items.Add(v); i = o.Count - 1; }
            c.SelectedIndex = Math.Max(0, i);
        }
        internal static RoundDatePicker Dt() => new() { ShowCheckBox = true, Checked = false, Height = 30 };
        internal static FlatButton Btn(string t, string glyph = null, ButtonKind k = ButtonKind.Primary, int w = 0)
            => new() { Text = t, Glyph = glyph ?? "", Kind = k, Size = new Size(w > 0 ? w : TextRenderer.MeasureText(t, F(9.5f, true)).Width + (glyph == null ? 36 : 58), 36), Margin = new Padding(0, 4, 12, 6) };

        private Control In(string kod) => _in.GetValueOrDefault(kod);

        // ===================================================== 1. Qəbul və sənədlər
        private void Bolme1()
        {
            var s = Scroll(tab1);
            var t = Kart(s, "Texnika haqqında məlumat", 4);
            t.Controls.Add(Sahe("Nov", "Texnikanın növü", Cmb(Secimler.Sahe["Nov"]), mecburi: true), 0, 0);
            t.Controls.Add(Sahe("Marka", "Texnikanın markası", Txt(), mecburi: true), 1, 0);
            t.Controls.Add(Sahe("ZavodNo", "Zavod nömrəsi", Txt(), mecburi: true), 2, 0);
            t.Controls.Add(Sahe("Kateqoriya", "Kateqoriyası — faktiki", Cmb(Secimler.Sahe["Kateqoriya"]), "Aktda göstərildiyi kimi"), 3, 0);
            ((RoundTextBox)In("ZavodNo")).PlaceholderText = "Nömrəni daxil edin";

            t = Kart(s, "Naryad və qəbul məlumatları", 4);
            t.Controls.Add(Sahe("NaryadNo", "Naryadın nömrəsi", Txt(), "Bir naryadla bir neçə texnika gələ bilər", true), 0, 0);
            t.Controls.Add(Sahe("NaryadTarixi", "Sənədin tarixi (naryadın tarixi)", Dt(), mecburi: true), 1, 0);
            t.Controls.Add(Sahe("DaxilolmaTarixi", "Zavoda faktiki daxilolma tarixi", Dt(), mecburi: true), 2, 0);
            t.Controls.Add(Sahe("GonderenQurum", "Təhvil verən (göndərən)", Txt(), mecburi: true), 3, 0);

            t = Kart(s, "Texniki vəziyyət aktı", 3);
            t.Controls.Add(Sahe("AktForma", "Aktın forması", Cmb(Secimler.Sahe["AktForma"])), 0, 0);
            t.Controls.Add(Sahe("AktNo", "Aktın nömrəsi", Txt()), 1, 0);
            t.Controls.Add(Sahe("AktTarixi", "Aktın tarixi", Dt()), 2, 0);
            t.Controls.Add(Sahe("MesulIcraci", "Zavodda məsul icraçı", Txt()), 0, 1);
            var zv = Sahe("TemirZavodu", "Qəbul edən təmir müəssisəsi", Cmb(Secimler.Zavodlar), mecburi: true);
            t.Controls.Add(zv, 1, 1);
            lblQebulInfo = new Label { AutoSize = false, Height = 34, Dock = DockStyle.Fill, Font = F(9.5f, true), Tag = "title", TextAlign = ContentAlignment.MiddleLeft, Margin = new Padding(0, 8, 18, 4) };
            t.Controls.Add(lblQebulInfo, 0, 2); t.SetColumnSpan(lblQebulInfo, 3);
        }

        // ===================================================== 2. İstismar göstəriciləri və son təmir
        private void Bolme2()
        {
            var s = Scroll(tab2);
            var t = Kart(s, "Texniki-istismar göstəriciləri", 4);
            t.Controls.Add(Sahe("IstehsalIli", "İstehsal ili", Txt()), 0, 0);
            t.Controls.Add(Sahe("IstismarTarixi", "İstismara verilib (tarix)", Dt()), 1, 0);
            var alt = new Label { Text = "İstismarın əvvəlindən işləyib", AutoSize = true, Font = F(11f, true), Tag = "title", Margin = new Padding(0, 8, 0, 0) };
            t.Controls.Add(alt, 0, 1); t.SetColumnSpan(alt, 4);
            var ip = Ipucu("İlk istismara verildiyi vaxtdan ümumi göstərici. \"Tətbiq edilmir\" və \"Məlumat yoxdur\" seçildikdə miqdar tələb olunmur.");
            t.Controls.Add(ip, 0, 2); t.SetColumnSpan(ip, 4);
            tlpGost = new TableLayoutPanel { ColumnCount = 3, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, BackColor = Color.Transparent, Margin = new Padding(0, 4, 0, 4) };
            tlpGost.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300)); tlpGost.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220)); tlpGost.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));
            t.Controls.Add(tlpGost, 0, 3); t.SetColumnSpan(tlpGost, 4);
            var add = Btn("Digər istismar göstəricisini əlavə et", Icons.Add);
            add.Click += (_, _) => GostericiSetri(null, null);
            t.Controls.Add(add, 0, 4); t.SetColumnSpan(add, 4);

            t = Kart(s, "Zavoda daxilolmadan əvvəlki son təmir", 3, "Yalnız ən son aparılmış təmir qeyd olunur.");
            t.Controls.Add(Sahe("SonTemirNovu", "Təmirin növü", Cmb(Secimler.Sahe["SonTemirNovu"])), 0, 0);
            t.Controls.Add(Sahe("SonTemirTarixi", "Tamamlanma tarixi", Dt()), 1, 0);
            t.Controls.Add(Sahe("SonTemirIcraci", "Təmiri aparmış hərbi hissə və ya müəssisə", Txt()), 2, 0);
            var q = Sahe("SonTemirQeyd", "Yerinə yetirilmiş işlər haqqında qeyd", Txt(true), hundur: 70);
            t.Controls.Add(q, 0, 1); t.SetColumnSpan(q, 3);
        }

        private void GostericiSetri(string nov, double? miqdar)
        {
            var c = new RoundComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Font = F(10f), Width = 284, Margin = new Padding(0, 2, 16, 4) };
            c.Items.AddRange(Secimler.Gosterici); c.SelectedIndex = Math.Max(0, Array.IndexOf(Secimler.Gosterici, nov));
            var m = new RoundTextBox { Font = F(10f), Width = 204, Height = 32, PlaceholderText = "Miqdar", Margin = new Padding(0, 2, 16, 4), Text = miqdar?.ToString("0.##", CultureInfo.InvariantCulture) ?? "" };
            var x = new FlatButton { Text = "", Glyph = Icons.Delete, Kind = ButtonKind.Ghost, Size = new Size(36, 32), Margin = new Padding(0, 2, 0, 4) };
            Ui.Reqemli(m, true);
            void Yoxla() { bool yox = Secimler.MiqdarsizGosterici(c.SelectedItem as string); m.Enabled = !yox; if (yox) m.Text = ""; }
            c.SelectedIndexChanged += (_, _) => Yoxla(); Yoxla();
            int r = tlpGost.RowCount++;
            tlpGost.Controls.Add(c, 0, r); tlpGost.Controls.Add(m, 1, r); tlpGost.Controls.Add(x, 2, r);
            x.Click += (_, _) => { tlpGost.Controls.Remove(c); tlpGost.Controls.Remove(m); tlpGost.Controls.Remove(x); };
            Theme.Apply(tlpGost);
        }

        private List<Gosterici> Gostericiler(List<string> xeta)
        {
            var l = new List<Gosterici>();
            foreach (var c in tlpGost.Controls.OfType<RoundComboBox>().OrderBy(c => tlpGost.GetRow(c)))
            {
                var m = (RoundTextBox)tlpGost.GetControlFromPosition(1, tlpGost.GetRow(c));
                string nov = c.SelectedItem as string; double? q = null;
                if (!Secimler.MiqdarsizGosterici(nov))
                {
                    if (string.IsNullOrWhiteSpace(m.Text)) { if (tlpGost.Controls.OfType<RoundComboBox>().Count() == 1) continue; xeta.Add($"{Lang.T(nov)}: {Lang.T("miqdarı yazın")}"); continue; }
                    if (!double.TryParse(m.Text.Replace(" ", "").Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out var d)) { xeta.Add($"{Lang.T(nov)}: {Lang.T("rəqəm olmalıdır")}"); continue; }
                    q = d;
                }
                l.Add(new Gosterici { Nov = nov, Miqdar = q });
            }
            return l;
        }

        // ===================================================== 3. Təmirin icrası
        private void Bolme3()
        {
            var s = Scroll(tab3);
            var t = Kart(s, "Texniki vəziyyəti", 4);
            var n = Sahe("Nasazliqlar", "Aşkar edilmiş nasazlıqların qısa təsviri", Txt(true), mecburi: true, hundur: 70);
            ((RoundTextBox)In("Nasazliqlar")).PlaceholderText = "Nasazlıqların qısa təsvirini daxil edin...";
            t.Controls.Add(n, 0, 0); t.SetColumnSpan(n, 3);
            t.Controls.Add(Sahe("BaslamaTarixi", "Diaqnostika tarixi", Dt(), mecburi: true), 3, 0);

            t = Kart(s, "Təmir məlumatları", 3);
            t.Controls.Add(Sahe("TemirNovu", "Təmirin / işin növü", Cmb(Secimler.Sahe["TemirNovu"]), "Hazırkı qəbul üzrə görüləcək iş seçilir.", true), 0, 0);
            t.Controls.Add(Sahe("Status", "İcra statusu", Cmb(Secimler.Status, false, Secimler.Ekran), "Tamamlanma və təhvil statusları tarixlərə əsasən yenilənir.", true), 1, 0);
            // Ehtiyat hissəsi gözləyirsə işarələnir (status dəyişmir: Qəbul edilib / Təmirdə qalır); reyestrdə "Ehtiyat hissələrinin təminatı" sütununda görünür
            chkTeminat = new CheckBox { Text = "Təminat gözləyir", AutoSize = true, Font = F(10f, true), Margin = new Padding(0, 30, 18, 6), Cursor = Cursors.Hand };
            ((ComboBox)In("Status")).SelectedIndexChanged += (_, _) => TeminatYoxla();
            t.Controls.Add(chkTeminat, 2, 0);

            t = Kart(s, "İcra olunma müddəti", 4);
            t.Controls.Add(Sahe("IcraMuddeti", "Müddət", Txt(), "Hesablama diaqnostika tarixindən aparılır.", true), 0, 0);
            t.Controls.Add(Sahe("MuddetVahidi", "Vahid", Cmb(Secimler.Sahe["MuddetVahidi"]), mecburi: true), 1, 0);
            t.Controls.Add(Sahe(null, "Plan üzrə bitmə tarixi", txtPlan = Txt(ro: true), "Avtomatik hesablanır"), 2, 0);
            t.Controls.Add(Sahe(null, "Qalan / gecikən gün", txtQalan = Txt(ro: true), "Avtomatik"), 3, 0);
            var mud = (RoundTextBox)In("IcraMuddeti"); mud.PlaceholderText = "Məsələn: 10";
            Ui.Reqemli(mud);

            t = Kart(s, "Gecikmə və plan dəyişikliyi", 4);
            var g = Sahe("GecikmeSebebi", "Gecikmənin və ya plan dəyişikliyinin səbəbi", Txt(true), "Yalnız dəyişiklik olduqda doldurulur. İlkin plan tarixçədə saxlanılır.", hundur: 60);
            t.Controls.Add(g, 0, 0); t.SetColumnSpan(g, 2);
            t.Controls.Add(Sahe("ProqnozBitme", "Gözlənilən bitmə tarixi", Dt()), 2, 0);
            t.Controls.Add(Sahe("YeniPlanBitme", "Yeni plan üzrə bitmə tarixi", Dt(), "Dəyişiklik yadda saxlandıqda plan bu tarixə yenilənir."), 3, 0);
            var b = Btn("Dəyişikliyi yadda saxla", Icons.Save); b.Click += btnSave_Click;
            t.Controls.Add(b, 3, 1);

            foreach (var k in new[] { "BaslamaTarixi", "IcraMuddeti", "MuddetVahidi", "YeniPlanBitme" })
                switch (In(k)) { case DateTimePicker d: d.ValueChanged += (_, _) => Hesabla(); break; case ComboBox c: c.SelectedIndexChanged += (_, _) => Hesabla(); break; case TextBox x: x.TextChanged += (_, _) => Hesabla(); break; }
        }

        private static DateTime? Tarix(Control c) => c is DateTimePicker { Checked: true } d ? d.Value.Date : null;

        /// <summary>Plan = diaqnostika + müddət (gün/ay); yeni plan yazılıbsa o. Qalan/gecikən gün bu günə görə.</summary>
        private DateTime? Plan()
        {
            if (Tarix(In("YeniPlanBitme")) is DateTime y) return y;
            if (Tarix(In("BaslamaTarixi")) is not DateTime d || !int.TryParse(In("IcraMuddeti").Text.Trim(), out int m) || m < 0) return null;
            return Deyer((ComboBox)In("MuddetVahidi")) == "Ay" ? d.AddMonths(m) : d.AddDays(m);
        }

        private void Hesabla()
        {
            var p = Plan();
            txtPlan.Text = p?.ToString("dd.MM.yyyy") ?? "";
            if (p is DateTime pd && Tarix(In("FaktikiBitme")) == null)
            {
                int g = (pd - DateTime.Today).Days;
                txtQalan.Text = g >= 0 ? $"{g} {Lang.T("gün qalıb")}" : $"{-g} {Lang.T("gün gecikir")}";
            }
            else txtQalan.Text = "";
            // Təhvil gözləmə: tamamlanmadan faktiki təhvilədək; təhvil verilməyibsə bu günədək
            if (Tarix(In("FaktikiBitme")) is DateTime f)
            {
                var son = Tarix(In("TehvilTarixi")) ?? DateTime.Today;
                txtGozleme.Text = $"{Math.Max(0, (son - f).Days)} {Lang.T("gün")}";
            }
            else txtGozleme.Text = Lang.T("Təmir hələ tamamlanmayıb");
        }

        // ===================================================== 4. Faktiki sərfiyyat
        private void Bolme4()
        {
            var s = Scroll(tab4);
            var t = Kart(s, "Faktiki sərfiyyat", 4, "Faktiki istifadə bu bölmədə qeyd edilir. Hesabatda məlumat daxil edilmir.");
            var bE = Btn("Sərfiyyat əlavə et", Icons.Add); bE.Click += (_, _) => { if (!pnlSerf.Visible) SerfPaneli(); else pnlSerf.Visible = false; };
            t.Controls.Add(bE, 0, 0);
            pnlSerf = AltPanel(t, 1, "Sərfiyyat", out var st);
            sMal = new RoundComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Height = 30 };
            sAd = new RoundTextBox { PlaceholderText = Lang.T("Yeni materialın adı"), Visible = false };
            var pm = Sahe(null, "Material / ehtiyat hissəsi", sMal, mecburi: true);
            pm.Controls.Add(sAd); sAd.Dock = DockStyle.Top; sAd.Height = 32; sAd.Font = F(10f); sAd.BringToFront();
            st.Controls.Add(pm, 0, 0);
            st.Controls.Add(Sahe(null, "Kodu", sKod = Txt(ro: true)), 1, 0);
            st.Controls.Add(Sahe(null, "Ölçü vahidi", sVahid = Txt(ro: true)), 2, 0);
            bYeniAd = Btn("Yeni ad əlavə et", Icons.Add, ButtonKind.Secondary); bYeniAd.Margin = new Padding(0, 26, 12, 6);
            bYeniAd.Click += (_, _) => YeniAdRejimi(!_yeniAd);
            st.Controls.Add(bYeniAd, 3, 0);
            st.Controls.Add(Sahe(null, "Sərf edilmiş miqdar", sMiqdar = Txt(), mecburi: true), 0, 1); Ui.Reqemli(sMiqdar, true);
            st.Controls.Add(Sahe(null, "Sərf tarixi", sTarix = Dt(), mecburi: true), 1, 1);
            var bS = Btn("Yadda saxla", Icons.Save); bS.Margin = new Padding(0, 26, 12, 6); bS.Click += async (_, _) => await SerfElaveAsync();
            st.Controls.Add(bS, 2, 1);
            sMal.SelectedIndexChanged += (_, _) => MalSecildi();
            var ip = Ipucu("Anbardan seçiləndə qalıqdan çox yazmaq olmaz. \"Yeni ad\" ilə yazılan material anbardan çıxılmır. Yadda saxlanmış qeyd silinmir.");
            st.Controls.Add(ip, 0, 2); st.SetColumnSpan(ip, 4);
            gridSerf = Grid(("Ad", "Materialın / ehtiyat hissəsinin adı", 260), ("Kod", "Kodu", 120), ("Vahid", "Ölçü vahidi", 110), ("Miqdar", "Sərf edilmiş miqdar", 150), ("Tarix", "Sərf tarixi", 120), ("Menbe", "Mənbə", 140));
            gridSerf.Columns["Ad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            var gs = GridQab(gridSerf, 220); t.Controls.Add(gs, 0, 2); t.SetColumnSpan(gs, 4);
        }

        private static string Rq(double d) => d.ToString("0.##", CultureInfo.InvariantCulture);

        /// <summary>Anbar qalığı: bazadakı qalıq − bu formada hələ yazılmamış (yeni texnika) sərflər.</summary>
        private double Qaliq(AnbarMal m) => m.Qaliq - (_id == null ? _serfler.Where(x => x.Anbar && Eyni(x, m)).Sum(x => x.Miqdar) : 0);
        private static bool Eyni(Serf x, AnbarMal m) => string.Equals((x.Ad ?? "").Trim(), (m.Ad ?? "").Trim(), StringComparison.OrdinalIgnoreCase)
            && string.Equals((x.Kod ?? "").Trim(), (m.Kod ?? "").Trim(), StringComparison.OrdinalIgnoreCase) && string.Equals((x.Vahid ?? "").Trim(), (m.Vahid ?? "").Trim(), StringComparison.OrdinalIgnoreCase);
        private List<AnbarMal> AnbarSiyahi() => _anbar.Where(m => Qaliq(m) > 0).ToList();

        private void SerfPaneli()
        {
            var l = AnbarSiyahi();
            sMal.Items.Clear();
            sMal.Items.AddRange(l.Select(m => (object)$"{m.Ad}{(string.IsNullOrEmpty(m.Kod) ? "" : " · " + m.Kod)} — {Lang.T("qalıq")}: {Rq(Qaliq(m))} {m.Vahid}").ToArray());
            YeniAdRejimi(l.Count == 0);
            if (sMal.Items.Count > 0) sMal.SelectedIndex = 0;
            sMiqdar.Text = ""; sTarix.Value = DateTime.Today; sTarix.Checked = true;
            pnlSerf.Visible = true;
        }

        private void YeniAdRejimi(bool yeni)
        {
            _yeniAd = yeni;
            sMal.Visible = !yeni; sAd.Visible = yeni; sKod.ReadOnly = sVahid.ReadOnly = !yeni;
            bYeniAd.Text = Lang.T(yeni ? "Anbardan seç" : "Yeni ad əlavə et");
            if (yeni) { sAd.Text = sKod.Text = sVahid.Text = ""; } else MalSecildi();
        }

        private void MalSecildi()
        {
            if (_yeniAd) return;
            var l = AnbarSiyahi();
            var m = sMal.SelectedIndex >= 0 && sMal.SelectedIndex < l.Count ? l[sMal.SelectedIndex] : null;
            sKod.Text = m?.Kod ?? ""; sVahid.Text = m?.Vahid ?? "";
        }

        private void SerfleriGoster()
        {
            gridSerf.Rows.Clear();
            foreach (var x in _serfler.OrderByDescending(x => x.Tarix))
                gridSerf.Rows.Add(x.Ad, string.IsNullOrEmpty(x.Kod) ? "—" : x.Kod, x.Vahid, Rq(x.Miqdar), Vaxt.Tarix(x.Tarix), Lang.T(x.Anbar ? "Anbar" : "Yeni ad"));
        }

        private async Task AnbarYukleAsync()
        {
            string zavod = Deyer((ComboBox)In("TemirZavodu")) ?? Session.ZavodFiltri;
            _anbar = zavod == null ? new() : await SorguRepo.AnbarAsync(zavod);
        }

        private async Task SerfElaveAsync()
        {
            if (!Reqem(sMiqdar.Text, out var m) || m <= 0 || !sTarix.Checked)
            { Toast.Show(this, Lang.T("Sərf edilmiş miqdarı (rəqəm) və sərf tarixini yazın"), ToastKind.Error); return; }
            Serf x;
            if (_yeniAd)
            {
                if (string.IsNullOrWhiteSpace(sAd.Text) || string.IsNullOrWhiteSpace(sVahid.Text)) { Toast.Show(this, Lang.T("Materialın adını və ölçü vahidini yazın"), ToastKind.Error); return; }
                x = new Serf { Ad = sAd.Text.Trim(), Kod = string.IsNullOrWhiteSpace(sKod.Text) ? null : sKod.Text.Trim(), Vahid = sVahid.Text.Trim(), Anbar = false };
            }
            else
            {
                var l = AnbarSiyahi();
                if (sMal.SelectedIndex < 0 || sMal.SelectedIndex >= l.Count) { Toast.Show(this, Lang.T("Materialı seçin"), ToastKind.Error); return; }
                var a = l[sMal.SelectedIndex];
                if (m > Qaliq(a) + 1e-9) { Toast.Show(this, $"{Lang.T("Anbarda qalıq")}: {Rq(Qaliq(a))} {a.Vahid}", ToastKind.Error); return; }
                x = new Serf { Ad = a.Ad, Kod = a.Kod, Vahid = a.Vahid, Anbar = true };
            }
            x.Miqdar = m; x.Tarix = Vaxt.Iso(sTarix.Value.Date);
            _serfler.Add(x);
            pnlSerf.Visible = false;
            if (_id is long id)
            {
                await TexnikaRepo.SaxlaAsync(id, new() { ["Serfiyyat"] = TexnikaJson.Yaz(_serfler) });
                await AnbarYukleAsync();
                Toast.Show(this, Lang.T("Sərfiyyat yadda saxlanıldı"));
            }
            else Toast.Show(this, Lang.T("Texnikanı yadda saxlayanda sərfiyyat da yazılacaq"), ToastKind.Info);
            SerfleriGoster();
        }

        internal static ModernGrid Grid(params (string ad, string basliq, int en)[] s)
        {
            var g = new ModernGrid { Dock = DockStyle.Fill };
            foreach (var (ad, b, en) in s) { g.Columns.Add(ad, b); g.Columns[ad].Width = en; }
            g.MouseWheel += (_, e) => g.ScrollRows(e.Delta);
            return g;
        }
        internal static Panel GridQab(ModernGrid g, int h) { var p = new Panel { Dock = DockStyle.Fill, Height = h, Margin = new Padding(0, 4, 18, 4), BackColor = Color.Transparent }; p.Controls.Add(g); return p; }

        internal static RoundPanel AltPanel(TableLayoutPanel host, int row, string basliq, out TableLayoutPanel t)
        {
            var p = new RoundPanel { Fill = PanelFill.Alt, Dock = DockStyle.Fill, Margin = new Padding(0, 4, 18, 8), Padding = new Padding(14, 8, 14, 8), Visible = false };
            t = new TableLayoutPanel { Dock = DockStyle.Top, ColumnCount = 4, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, BackColor = Color.Transparent };
            for (int i = 0; i < 4; i++) t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            var l = new Label { Text = basliq, Dock = DockStyle.Top, Height = 26, Font = F(11f, true), Tag = "title" };
            p.Controls.Add(t); p.Controls.Add(l);
            var tt = t; tt.SizeChanged += (_, _) => p.Height = l.Height + tt.Height + p.Padding.Vertical + 2;
            host.Controls.Add(p, 0, row); host.SetColumnSpan(p, 4);
            return p;
        }

        // ===================================================== 5. Tamamlanma və təhvil
        private void Bolme5()
        {
            var s = Scroll(tab5);
            var t = Kart(s, "Təmirin tamamlanması", 3);
            t.Controls.Add(Sahe("FaktikiBitme", "Təmirin faktiki tamamlanma tarixi", Dt(), "Saxlananda status: Təmir tamamlanıb, təhvil gözlənilir."), 0, 0);
            var g = Sahe("GorulenIsler", "Yerinə yetirilmiş işlər haqqında qeyd", Txt(true), "Tamamlanma tarixi yazılıbsa, məcburidir.", mecburi: true, hundur: 70);
            t.Controls.Add(g, 1, 0); t.SetColumnSpan(g, 2);

            t = Kart(s, "Texnikanın təhvili", 4, "Faktiki təhvil tarixi yazılıbsa, bütün təhvil sahələri məcburidir. Saxlananda status: Təhvil verilib.");
            t.Controls.Add(Sahe("TehvilTarixi", "Faktiki təhvil tarixi", Dt()), 0, 0);
            t.Controls.Add(Sahe("TehvilSenedNo", "Təhvil sənədinin nömrəsi", Txt()), 1, 0);
            t.Controls.Add(Sahe("TehvilSenedTarixi", "Təhvil sənədinin tarixi", Dt()), 2, 0);
            cmbTehvilAlan = Cmb(new[] { "Göndərən qurum", "Digər qurum" }, false);
            var pa = Sahe(null, "Təhvil alan qurum", cmbTehvilAlan, "Birinci bölmədəki göndərən qurum təklif edilir; dəyişmək mümkündür.");
            txtDigerQurum = new RoundTextBox { Dock = DockStyle.Top, Height = 32, Font = F(10f), PlaceholderText = "Qurumun adını yazın", Visible = false };
            pa.Controls.Add(txtDigerQurum); txtDigerQurum.BringToFront();
            cmbTehvilAlan.SelectedIndexChanged += (_, _) => { txtDigerQurum.Visible = cmbTehvilAlan.SelectedIndex == 1; pa.Height = 22 + 30 + 20 + 8 + (txtDigerQurum.Visible ? 36 : 0); };
            t.Controls.Add(pa, 3, 0);
            // Birinci seçim göndərən qurumun öz adıdır (1-ci bölmədən)
            var gq = In("GonderenQurum");
            void GqAdi() { int i = cmbTehvilAlan.SelectedIndex; cmbTehvilAlan.Items[0] = gq.Text.Trim() is { Length: > 0 } a ? a : Lang.T("Göndərən qurum"); cmbTehvilAlan.SelectedIndex = i; }
            gq.TextChanged += (_, _) => GqAdi(); GqAdi();

            t = Kart(s, "Təhvil gözləmə müddəti", 1, "Tamamlanmadan faktiki təhvilədək; təhvil verilməyibsə, bu günədək.");
            t.Controls.Add(Sahe(null, "Avtomatik hesablanır", txtGozleme = Txt(ro: true)), 0, 0);
            foreach (var k in new[] { "FaktikiBitme", "TehvilTarixi" }) ((DateTimePicker)In(k)).ValueChanged += (_, _) => { Hesabla(); StatusYoxla(); };
        }

        // ===================================================== Yükləmə
        public Task YukleAsync(long? id) => YukleAsync(id, 0);

        public async Task YukleAsync(long? id, int bolme)
        {
            _id = id; YeniRejim = id == null; tabs.SelectedIndex = Math.Clamp(bolme, 0, tabs.TabCount - 1);
            lblTitle.Text = id == null ? Lang.T("Texnika əlavə et") : Lang.T("Redaktə") + "…";
            Dictionary<string, object> r = id == null ? new() : await TexnikaRepo.TapAsync(id.Value) ?? new();
            _guid = r.GetValueOrDefault("Guid") as string;
            if (id != null) lblTitle.Text = $"{Lang.T("Redaktə")}: {r.GetValueOrDefault("Marka")} · {r.GetValueOrDefault("ZavodNo")}";
            if (id == null)
            {
                r["TemirZavodu"] = Session.ZavodFiltri; r["Status"] = "Qəbul edilib"; r["MuddetVahidi"] = "Gün";
                r["DaxilolmaTarixi"] = r["BaslamaTarixi"] = Vaxt.Iso(DateTime.Today); r["MesulIcraci"] = Session.AdSoyad;   // məsul icraçı: daxil edən istifadəçi (dəyişdirilə bilər)
            }
            foreach (var s in Saheler.Hamisi)
            {
                var v = r.GetValueOrDefault(s.Kod); string str = v is null or DBNull ? "" : Convert.ToString(v, CultureInfo.InvariantCulture);
                switch (In(s.Kod))
                {
                    case ComboBox cb: Sec(cb, str); break;
                    case DateTimePicker dp: if (DateTime.TryParse(str, out var d)) { dp.Value = d; dp.Checked = true; } else { dp.Value = DateTime.Today; dp.Checked = false; } break;
                    case TextBox tb: tb.Text = tb.Multiline ? str.Replace("\r\n", "\n").Replace("\n", "\r\n") : str; break;   // çoxsətirli xana yalnız CRLF-i sətir kimi göstərir
                }
            }
            if (In("TemirZavodu") is ComboBox z) z.Enabled = Session.ZavodFiltri == null;
            chkTeminat.Checked = r.GetValueOrDefault("Teminat") as string == Secimler.StTeminat;
            // Göstəricilər
            tlpGost.SuspendLayout(); tlpGost.Controls.Clear(); tlpGost.RowCount = 0;
            var gl = TexnikaJson.Gostericiler(r.GetValueOrDefault("Gostericiler"));
            if (gl.Count == 0) GostericiSetri("Gediş, km", null); else foreach (var g in gl) GostericiSetri(g.Nov, g.Miqdar);
            tlpGost.ResumeLayout();
            // Təhvil alan qurum
            string alan = r.GetValueOrDefault("TehvilAlan") as string;
            bool diger = !string.IsNullOrEmpty(alan) && alan != r.GetValueOrDefault("GonderenQurum") as string;
            cmbTehvilAlan.SelectedIndex = diger ? 1 : 0; txtDigerQurum.Text = diger ? alan : "";
            // Faktiki sərfiyyat
            pnlSerf.Visible = false;
            _serfler = TexnikaJson.Serfler(r.GetValueOrDefault("Serfiyyat"));
            SerfleriGoster();
            await AnbarYukleAsync();
            Ui.Hazirla(this);
            QebulInfo(); Hesabla(); StatusYoxla(); Duymeler();
            _ilk = Iz();
        }

        // ===================================================== Yadda saxlanmamış dəyişiklik
        private string _ilk;

        /// <summary>Formanın cari məzmunu (müqayisə üçün). Mövcud texnikada sərfiyyat dərhal yazıldığı üçün nəzərə alınmır.</summary>
        private string Iz()
        {
            var d = Topla(out _);
            if (_id != null) d.Remove("Serfiyyat");
            return System.Text.Json.JsonSerializer.Serialize(d);
        }

        public override async Task<bool> AyrilAsync()
        {
            if (_ilk == null || Iz() == _ilk) return true;
            switch (Ui.Secim(FindForm(), Lang.T("Yadda saxlanmamış dəyişiklik"), Lang.T("Formada yadda saxlanmamış məlumat var. Yadda saxlansın?"), "Yadda saxla", "Saxlama"))
            {
                case DialogResult.OK: return await YazAsync() != null;
                case DialogResult.No: if (_id == null) await YukleAsync(null); else _ilk = null; return true;   // yeni forma təmizlənir
                default: return false;
            }
        }

        public override Task OnShownAsync() => Task.CompletedTask;

        private void QebulInfo()
        {
            string zavod = Deyer((ComboBox)In("TemirZavodu"));
            lblQebulInfo.Text = $"{Lang.T("Qəbul edən təmir müəssisəsi")}: {Lang.T(zavod ?? "—")}   •   {Lang.T("İlkin status")}: {Lang.T("Qəbul edilib")}";
        }

        /// <summary>"Təminat gözləyir" yalnız təmiri davam edən texnikada (Qəbul edilib / Təmirdə) qoyula bilər.</summary>
        private void TeminatYoxla()
        {
            bool olar = Deyer((ComboBox)In("Status")) is "Qəbul edilib" or "Təmirdə";
            chkTeminat.Enabled = olar; if (!olar) chkTeminat.Checked = false;
        }

        /// <summary>Tamamlanma/təhvil tarixi yazılıbsa status avtomatikdir və seçilə bilməz.</summary>
        private void StatusYoxla()
        {
            var cb = (ComboBox)In("Status");
            string avto = Tarix(In("TehvilTarixi")) != null ? Secimler.StTehvil : Tarix(In("FaktikiBitme")) != null ? Secimler.StTamamlanib : null;
            if (avto != null) { Sec(cb, avto); cb.Enabled = false; }
            else
            {
                cb.Enabled = true;
                if (Deyer(cb) is Secimler.StTehvil or Secimler.StTamamlanib) Sec(cb, "Təmirdə");
            }
        }

        internal static bool Reqem(string s, out double d) => double.TryParse((s ?? "").Replace(" ", "").Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out d);

        // ===================================================== Yadda saxla
        private Dictionary<string, object> Topla(out List<(string kod, string mesaj)> xetalar)
        {
            xetalar = new(); var d = new Dictionary<string, object>();
            var xx = xetalar;
            void X(string kod, string m) => xx.Add((kod, m));
            foreach (var s in Saheler.Hamisi)
            {
                if (In(s.Kod) is not { } c) continue;
                object v = c switch
                {
                    ComboBox cb => Deyer(cb),
                    DateTimePicker dp => dp.Checked ? Vaxt.Iso(dp.Value) : null,
                    TextBox tb => tb.Text.Trim() is { Length: > 0 } t ? t : null,
                    _ => null
                };
                if (v is string str && s.Nov == SaheNov.Reqem)
                {
                    if (Reqem(str, out var n)) v = n; else X(s.Kod, $"{Lang.T(s.Ad)}: {Lang.T("rəqəm olmalıdır")}");
                }
                if (s.Mecburi && v == null) X(s.Kod, $"{Lang.T(s.Ad)}: {Lang.T("Məcburi sahə boşdur")}");
                d[s.Kod] = v;
            }
            // Göstəricilər (ehtiyat hissələri "Sorğu yarat" səhifəsində dərhal yazılır)
            var gx = new List<string>();
            d["Gostericiler"] = TexnikaJson.Yaz(Gostericiler(gx));
            foreach (var m in gx) X("Gostericiler", m);
            d["Serfiyyat"] = TexnikaJson.Yaz(_serfler);
            d["Teminat"] = chkTeminat.Checked && chkTeminat.Enabled ? Secimler.StTeminat : null;
            // Tamamlanma və təhvil
            if (d["FaktikiBitme"] != null && d["GorulenIsler"] == null) X("GorulenIsler", $"{Lang.T("Yerinə yetirilmiş işlər haqqında qeyd")}: {Lang.T("Məcburi sahə boşdur")}");
            string alan = cmbTehvilAlan.SelectedIndex == 1 ? (txtDigerQurum.Text.Trim() is { Length: > 0 } q ? q : null) : d["GonderenQurum"] as string;
            bool tehvil = d["TehvilTarixi"] != null || d["TehvilSenedNo"] != null || d["TehvilSenedTarixi"] != null || cmbTehvilAlan.SelectedIndex == 1;
            d["TehvilAlan"] = tehvil ? alan : null;
            if (tehvil)
            {
                foreach (var k in new[] { "TehvilTarixi", "TehvilSenedNo", "TehvilSenedTarixi", "TehvilAlan" })
                    if (d[k] == null) X(k, $"{Lang.T(Saheler.Hamisi.First(s => s.Kod == k).Ad)}: {Lang.T("Məcburi sahə boşdur")}");
                if (d["FaktikiBitme"] == null) X("FaktikiBitme", $"{Lang.T("Təmirin faktiki tamamlanma tarixi")}: {Lang.T("Məcburi sahə boşdur")}");
            }
            // Status və plan tarixi (avtomatik)
            if (d["TehvilTarixi"] != null) d["Status"] = Secimler.StTehvil;
            else if (d["FaktikiBitme"] != null) d["Status"] = Secimler.StTamamlanib;
            else if (d["Status"] is Secimler.StTamamlanib or Secimler.StTehvil) X("FaktikiBitme", $"{Lang.T("Təmirin faktiki tamamlanma tarixi")}: {Lang.T("Məcburi sahə boşdur")}");
            d["GozlenilenBitme"] = Plan() is DateTime p ? Vaxt.Iso(p) : null;
            return d;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (await YazAsync() is long id) await MainForm.Current.DetalAcAsync(id);
        }

        /// <summary>Yoxlayır və yadda saxlayır; uğursuz olsa null.</summary>
        private async Task<long?> YazAsync()
        {
            var d = Topla(out var x);
            if (x.Count > 0)
            {
                // İlk xətalı sahənin bölməsinə keç və onu fokusla
                Control c = x[0].kod == "Gostericiler" ? tlpGost : x[0].kod == "TehvilAlan" ? cmbTehvilAlan : In(x[0].kod);
                if (c != null) { var tp = c.Parent; while (tp is not TabPage && tp != null) tp = tp.Parent; if (tp is TabPage t) tabs.SelectedTab = t; c.Focus(); }
                Toast.Show(this, string.Join("\n", x.Take(3).Select(q => q.mesaj)), ToastKind.Error); return null;
            }
            btnSave.Enabled = false;
            try
            {
                bool yeni = _id == null;
                long id = await TexnikaRepo.SaxlaAsync(_id, d);
                _id = id; YeniRejim = false;   // yadda saxlandı: menyudan "əlavə et" basanda forma təmiz açılsın, təkrar basanda dublikat yaranmasın
                await SistemRepo.LogAsync(yeni ? "Əlavə" : "Redaktə", $"{(yeni ? "Yeni texnika" : "Dəyişdirildi")}: {d["Marka"]} ({d["ZavodNo"]})");
                if (yeni) await SistemRepo.BildirisAsync($"new:{id}", "new", "Yeni texnika qəbul edildi", $"{d["Marka"]} ({d["ZavodNo"]}) {d["TemirZavodu"]}-ə daxil oldu", d["TemirZavodu"] as string, id);
                Toast.Show(this, yeni ? "Texnika reyestrə əlavə edildi" : "Dəyişikliklər yadda saxlanıldı");
                await MainForm.Current.BadgeYenileAsync();
                _ilk = Iz();
                return id;
            }
            catch (Exception ex) { Toast.Show(this, ex.Message.Contains("UNIQUE") ? Lang.T("Bu zavod nömrəsi artıq bazada var.") : ex.Message, ToastKind.Error); return null; }
            finally { btnSave.Enabled = true; }
        }

        private void btnPrevG_Click(object sender, EventArgs e) { if (tabs.SelectedIndex > 0) tabs.SelectedIndex--; }
        private void btnNextG_Click(object sender, EventArgs e) { if (tabs.SelectedIndex < tabs.TabCount - 1) tabs.SelectedIndex++; }
        private void tabs_SelectedIndexChanged(object sender, EventArgs e) => Duymeler();
        private void Duymeler() { btnPrevG.Enabled = tabs.SelectedIndex > 0; btnNextG.Enabled = tabs.SelectedIndex < tabs.TabCount - 1; }
    }
}
