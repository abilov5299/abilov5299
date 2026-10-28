using System.Globalization;
using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Forms;
using static TexnikaReyestri.Pages.EditPage;

namespace TexnikaReyestri.Pages
{
    /// <summary>
    /// "Sorğu yarat": 1) məktub yaradılır və ya qeydiyyatdakı məktub seçilir; 2) məktub texnikalara bağlanır;
    /// 3) seçilmiş texnikanın ehtiyat hissələri və qəbulu; 4) məktubun icrası üzrə qeydlər. Hər addım dərhal yazılır.
    /// </summary>
    public class SorguYaratPage : PageBase
    {
        private Sorgu _s;                          // cari məktub (null — hələ yaradılmayıb/seçilməyib)
        private bool _redakte;                     // panel mövcud məktubu dəyişir
        private long? _texId; private string _texGuid;
        private List<Hisse> _hisseler = new();

        private readonly Dictionary<string, Control> _mk = new();
        private readonly Dictionary<ComboBox, List<string>> _orig = new();
        private RoundPanel pnlMektub, pnlHisse, pnlQebul, pnlQeyd;
        private Label lblMektub, lblHisse;
        private ModernGrid gridTex, gridHis, gridQeyd;
        private RoundTextBox hAd, hKod, hVahid, hTeleb, qMiqdar, iNetice; private RoundComboBox qHisse; private RoundDatePicker qTarix, iTarix, iGozl;
        private StatusChip chipTeminat;
        private TableLayoutPanel kTex, kHis, kQeyd;

        public SorguYaratPage()
        {
            var s = new Panel { Dock = DockStyle.Fill, AutoScroll = true, Padding = new Padding(0, 0, 8, 8) };
            Controls.Add(s);
            var head = new Panel { Dock = DockStyle.Top, Height = 44 };
            head.Controls.Add(new Label { Text = "Sorğu yarat", Tag = "title", AutoSize = true, Font = new Font("Bahnschrift SemiBold", 18f), Location = new Point(0, 4) });
            Controls.Add(head);

            // --- Addım 1: məktub ---
            var t = Kart(s, "Addım 1 · Təchizat məktubu", 4, "Əvvəlcə yeni məktub yaradın və ya qeydiyyatdakı məktubu seçin.");
            var bYeni = Btn("Yeni məktub qeyd et", Icons.Add); bYeni.Click += (_, _) => MektubPaneli(true);
            var bSec = Btn("Qeydiyyatdakı məktubu seç", Icons.List, ButtonKind.Secondary); bSec.Click += async (_, _) => await MektubSecAsync();
            var bDey = Btn("Məktubu dəyiş", null, ButtonKind.Ghost); bDey.Click += (_, _) => { if (_s != null) MektubPaneli(false); };
            var fl = new FlowLayoutPanel { AutoSize = true, BackColor = Color.Transparent, Margin = new Padding(0) }; fl.Controls.AddRange(new Control[] { bYeni, bSec, bDey });
            t.Controls.Add(fl, 0, 0); t.SetColumnSpan(fl, 4);
            lblMektub = new Label { AutoSize = true, Font = F(10f, true), Margin = new Padding(0, 4, 0, 6), Text = Lang.T("Məktub seçilməyib") };
            t.Controls.Add(lblMektub, 0, 1); t.SetColumnSpan(lblMektub, 4);

            pnlMektub = new RoundPanel { Fill = PanelFill.Alt, Dock = DockStyle.Fill, Margin = new Padding(0, 4, 18, 8), Padding = new Padding(14, 10, 14, 8), Visible = false };
            var mt = new TableLayoutPanel { Dock = DockStyle.Top, ColumnCount = 4, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, BackColor = Color.Transparent };
            for (int i = 0; i < 4; i++) mt.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            mt.Controls.Add(M("MektubNo", "Məktubun nömrəsi", Txt()), 0, 0);
            mt.Controls.Add(M("QeydTarixi", "Məktubun tarixi", Dt()), 1, 0);
            mt.Controls.Add(M("TeminEden", "Təmin edən qurum", Txt()), 2, 0);
            mt.Controls.Add(M("TeminIcraci", "Təmin edən qurum üzrə məsul icraçı", Txt()), 3, 0);
            mt.Controls.Add(M("Telefon", "Əlaqə nömrəsi", Txt()), 0, 1);
            mt.Controls.Add(M("IcraMuddeti", "İcra üçün son tarix", Dt()), 1, 1);
            mt.Controls.Add(M("CavabVez", "Məktubun icra vəziyyəti", Cmb(SorguRepo.CavabVez)), 2, 1);
            mt.Controls.Add(M("Zavod", "Təmir zavodu", Cmb(Secimler.Zavodlar)), 3, 1);
            var mb = new FlowLayoutPanel { AutoSize = true, BackColor = Color.Transparent, Margin = new Padding(0, 4, 0, 0) };
            var bMs = Btn("Məktubu yadda saxla", Icons.Save); bMs.Click += async (_, _) => await MektubSaxlaAsync();
            var bMl = Btn("Bağla", null, ButtonKind.Ghost); bMl.Click += (_, _) => pnlMektub.Visible = false;
            mb.Controls.AddRange(new Control[] { bMs, bMl }); mt.Controls.Add(mb, 0, 2); mt.SetColumnSpan(mb, 4);
            pnlMektub.Controls.Add(mt);
            mt.SizeChanged += (_, _) => pnlMektub.Height = mt.Height + pnlMektub.Padding.Vertical + 2;
            t.Controls.Add(pnlMektub, 0, 2); t.SetColumnSpan(pnlMektub, 4);

            // --- Addım 2: texnikalar ---
            t = kTex = Kart(s, "Addım 2 · Texnikaya bağla", 4, "Məktub yadda saxlanandan sonra açılır. Bir məktub bir neçə texnikaya bağlana bilər.");
            var bTex = Btn("Texnika seç və bağla", Icons.Add); bTex.Click += async (_, _) => await TexnikaBaglaAsync();
            t.Controls.Add(bTex, 0, 0);
            gridTex = Grid(("Marka", "Marka", 220), ("ZavodNo", "Zavod nömrəsi", 140), ("Status", "Status", 180));
            gridTex.Columns["Marka"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gridTex.SelectionChanged += async (_, _) => await TexnikaSecildiAsync();
            var gt = GridQab(gridTex, 140); t.Controls.Add(gt, 0, 1); t.SetColumnSpan(gt, 4);
            var it = Ipucu("Ehtiyat hissələri cədvəldə seçilmiş texnika üçün göstərilir. Yadda saxlanmış bağlantını ayırmaq olmaz.");
            t.Controls.Add(it, 0, 2); t.SetColumnSpan(it, 4);

            // --- Addım 3: ehtiyat hissələri ---
            chipTeminat = new StatusChip { Text = "—", Size = new Size(200, 26) };
            var sag = new Panel { Size = new Size(420, 30), BackColor = Color.Transparent };
            sag.Controls.Add(new Label { Text = "Təminat vəziyyəti:", AutoSize = true, Font = F(10f, true), Location = new Point(0, 4) });
            sag.Controls.Add(chipTeminat); chipTeminat.Location = new Point(150, 2);
            t = kHis = Kart(s, "Addım 3 · Ehtiyat hissələri", 4, null, sag);
            lblHisse = new Label { AutoSize = true, Font = F(9.5f, true), Tag = "muted", Margin = new Padding(0, 0, 0, 4) };
            t.Controls.Add(lblHisse, 0, 0); t.SetColumnSpan(lblHisse, 4);
            gridHis = Grid(("Ad", "Adı", 220), ("Kod", "Kodu", 120), ("Vahid", "Ölçü vahidi", 110), ("Teleb", "Tələb olunan", 110), ("Qebul", "Qəbul edilmiş", 120), ("Qaliq", "Qalıq", 90));
            gridHis.Columns["Ad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            var gh = GridQab(gridHis, 170); t.Controls.Add(gh, 0, 1); t.SetColumnSpan(gh, 4);
            var bH = Btn("Ehtiyat hissəsi əlavə et", Icons.Add); bH.Click += (_, _) => { if (TexnikaYoxla()) { pnlQebul.Visible = false; pnlHisse.Visible = !pnlHisse.Visible; } };
            var bQ = Btn("Ehtiyat hissəsinin qəbulunu qeyd et", null, ButtonKind.Secondary); bQ.Click += (_, _) => { if (TexnikaYoxla()) { pnlHisse.Visible = false; QebulPaneli(); } };
            var fh = new FlowLayoutPanel { AutoSize = true, BackColor = Color.Transparent, Margin = new Padding(0, 6, 0, 0) }; fh.Controls.AddRange(new Control[] { bH, bQ });
            t.Controls.Add(fh, 0, 2); t.SetColumnSpan(fh, 4);
            var ih = Ipucu("Qəbul edilmiş miqdar, qalıq və təminat vəziyyəti avtomatik hesablanır."); t.Controls.Add(ih, 0, 3); t.SetColumnSpan(ih, 4);

            pnlHisse = AltPanel(t, 4, "Ehtiyat hissəsi", out var ht);
            ht.Controls.Add(Sahe("Adı", hAd = Txt(), true), 0, 0);
            ht.Controls.Add(Sahe("Kodu", hKod = Txt()), 1, 0);
            ht.Controls.Add(Sahe("Ölçü vahidi", hVahid = Txt(), true), 2, 0);
            ht.Controls.Add(Sahe("Tələb olunan", hTeleb = Txt(), true), 3, 0); Ui.Reqemli(hTeleb, true);
            var bHs = Btn("Yadda saxla", Icons.Save); bHs.Click += async (_, _) => await HisseElaveAsync(); ht.Controls.Add(bHs, 3, 1);

            pnlQebul = AltPanel(t, 5, "Ehtiyat hissəsinin qəbulu", out var qt);
            qt.Controls.Add(Sahe("Ehtiyat hissəsi", qHisse = Cmb(Array.Empty<string>()), true), 0, 0);
            qt.Controls.Add(Sahe("Qəbul tarixi", qTarix = Dt(), true), 1, 0);
            qt.Controls.Add(Sahe("Miqdar", qMiqdar = Txt(), true), 2, 0); Ui.Reqemli(qMiqdar, true);
            var bQs = Btn("Yadda saxla", Icons.Save); bQs.Click += async (_, _) => await QebulElaveAsync(); qt.Controls.Add(bQs, 3, 0);

            // --- Addım 4: icra qeydləri ---
            t = kQeyd = Kart(s, "Addım 4 · Məktubun icrası üzrə qeydlər", 4, "Yeni qeyd əvvəlkini silmir. Son qeyd yuxarıda göstərilir.");
            var bI = Btn("İcra qeydi əlavə et", Icons.Add); bI.Click += (_, _) => pnlQeyd.Visible = !pnlQeyd.Visible;
            t.Controls.Add(bI, 0, 0);
            pnlQeyd = AltPanel(t, 1, "İcra qeydi", out var qq);
            qq.Controls.Add(Sahe("Əlaqə tarixi", iTarix = Dt(), true), 0, 0);
            var ne = Sahe("Danışığın nəticəsi və ya alınmış məlumat", iNetice = Txt(true), true, 56);
            qq.Controls.Add(ne, 1, 0); qq.SetColumnSpan(ne, 2);
            qq.Controls.Add(Sahe("Bildirildiyi üzrə gözlənilən təminat tarixi", iGozl = Dt()), 3, 0);
            var bIs = Btn("Qeydi yadda saxla", Icons.Save); bIs.Click += async (_, _) => await QeydElaveAsync(); qq.Controls.Add(bIs, 3, 1);
            gridQeyd = Grid(("Tarix", "Əlaqə tarixi", 120), ("Netice", "Nəticə", 400), ("Gozl", "Gözlənilən təminat", 160));
            gridQeyd.Columns["Netice"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            var gq = GridQab(gridQeyd, 140); t.Controls.Add(gq, 0, 2); t.SetColumnSpan(gq, 4);

            Goster();
        }

        // ===================================================== Köməkçilər
        private Panel Sahe(string ad, Control c, bool mecburi = false, int hundur = 0)
        {
            var p = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 18, 6), BackColor = Color.Transparent };
            var l = new Label { Text = ad + (mecburi ? " *" : ""), Dock = DockStyle.Top, Height = 22, Font = F(9f), Tag = "muted" };
            c.Dock = DockStyle.Top; c.Font = F(10f);
            if (hundur > 0) c.Height = hundur;
            if (c is RoundTextBox { Multiline: false } t0) t0.Height = 32;
            p.Controls.Add(c); p.Controls.Add(l);
            p.Height = l.Height + c.Height + 8;
            return p;
        }
        private Panel M(string kod, string ad, Control c) { _mk[kod] = c; return Sahe(ad, c, kod is not ("Telefon" or "Zavod")); }
        private RoundComboBox Cmb(IEnumerable<string> items)
        {
            var c = new RoundComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Height = 30 };
            var o = items.ToList(); c.Items.AddRange(o.Cast<object>().ToArray()); _orig[c] = o;
            if (c.Items.Count > 0) c.SelectedIndex = 0; return c;
        }
        private string Deyer(ComboBox c) => _orig.TryGetValue(c, out var o) && c.SelectedIndex >= 0 && c.SelectedIndex < o.Count ? o[c.SelectedIndex] : null;
        private void Sec(ComboBox c, string v) { int i = _orig[c].IndexOf(v ?? ""); if (i < 0 && !string.IsNullOrEmpty(v)) { _orig[c].Add(v); c.Items.Add(v); i = c.Items.Count - 1; } c.SelectedIndex = Math.Max(0, i); }

        /// <summary>Addım 2–4 yalnız məktub olanda aktivdir.</summary>
        private void Goster()
        {
            kTex.Enabled = kHis.Enabled = kQeyd.Enabled = _s != null;
            lblMektub.Text = _s == null ? Lang.T("Məktub seçilməyib")
                : $"{Lang.T("Məktub")}: {_s.V.GetValueOrDefault("MektubNo")} · {Vaxt.Tarix(_s.V.GetValueOrDefault("QeydTarixi"))} · {_s.V.GetValueOrDefault("TeminEden")} · {Lang.T(_s.V.GetValueOrDefault("CavabVez") ?? "İcrası davam edir")}";
        }

        // ===================================================== Yükləmə
        public override async Task OnShownAsync() { if (_s != null) await YukleAsync(_s.Id); }

        /// <summary>Məktubu açır (null — boş səhifə). texGuid verilərsə həmin texnika seçilir.</summary>
        public async Task YukleAsync(long? sorguId, string texGuid = null)
        {
            pnlMektub.Visible = pnlHisse.Visible = pnlQebul.Visible = pnlQeyd.Visible = false;
            _s = sorguId is long id ? await SorguRepo.GetAsync(id) : null;
            string evvel = texGuid ?? _texGuid;
            gridTex.Rows.Clear();
            if (_s != null) foreach (var x in _s.Tex) gridTex.Rows.Add(x.Marka, x.ZavodNo, Lang.T(x.Status));
            int sec = _s?.Tex.FindIndex(x => x.Guid == evvel) ?? -1;
            if (gridTex.Rows.Count > 0) { gridTex.ClearSelection(); gridTex.Rows[Math.Max(0, sec)].Selected = true; gridTex.CurrentCell = gridTex.Rows[Math.Max(0, sec)].Cells[0]; }
            await TexnikaSecildiAsync();
            QeydleriGoster();
            Goster();
        }

        private async Task TexnikaSecildiAsync()
        {
            int i = gridTex.CurrentRow?.Index ?? -1;
            var x = _s != null && i >= 0 && i < _s.Tex.Count ? _s.Tex[i] : null;
            _texGuid = x?.Guid; _texId = x == null ? null : await TexnikaRepo.IdAsync(x.Guid);
            var r = _texId is long tid ? await TexnikaRepo.TapAsync(tid) : null;
            _hisseler = TexnikaJson.Hisseler(r?.GetValueOrDefault("Hisseler"));
            lblHisse.Text = x == null ? Lang.T("Texnika seçilməyib") : $"{Lang.T("Texnika")}: {x.Marka} ({x.ZavodNo})";
            HisseleriGoster();
        }

        private bool TexnikaYoxla()
        {
            if (_texId != null) return true;
            Toast.Show(this, Lang.T("Əvvəlcə məktubu texnikaya bağlayın"), ToastKind.Warn); return false;
        }

        // ===================================================== Məktub
        private void MektubPaneli(bool yeni)
        {
            _redakte = !yeni;
            foreach (var (k, c) in _mk)
            {
                string v = yeni ? null : _s?.V.GetValueOrDefault(k);
                if (yeni && k == "Zavod") v = Session.ZavodFiltri;
                switch (c)
                {
                    case DateTimePicker d: if (DateTime.TryParse(v, out var x)) { d.Value = x; d.Checked = true; } else { d.Value = DateTime.Today; d.Checked = k == "QeydTarixi"; } break;
                    case ComboBox cb: Sec(cb, v); break;
                    case TextBox t: t.Text = v ?? ""; break;
                }
            }
            _mk["Zavod"].Enabled = Session.ZavodFiltri == null && (yeni || _s?.Tex.Count == 0);
            pnlMektub.Visible = true;
        }

        private async Task MektubSaxlaAsync()
        {
            var x = new List<string>();
            var s = _redakte && _s != null ? _s : new Sorgu();
            foreach (var (k, c) in _mk)
            {
                string v = c switch { DateTimePicker { Checked: true } d => Vaxt.Iso(d.Value.Date), DateTimePicker => null, ComboBox cb => Deyer(cb), _ => c.Text.Trim() };
                if (string.IsNullOrEmpty(v) && k != "Telefon") x.Add(((Label)c.Parent.Controls.OfType<Label>().First()).Text.TrimEnd(' ', '*'));
                s.V[k] = v;
            }
            if (x.Count > 0) { Toast.Show(this, $"{Lang.T("Doldurun")}: {string.Join(", ", x.Take(3))}", ToastKind.Error); return; }
            bool yeni = s.Id == 0;
            long id = await SorguRepo.SaxlaAsync(s);
            await SistemRepo.LogAsync(yeni ? "Əlavə" : "Redaktə", $"{Lang.T("Məktub")}: {s.V["MektubNo"]}");
            _texGuid = null;
            await YukleAsync(id);
            Toast.Show(this, Lang.T("Məktub yadda saxlanıldı"));
        }

        private async Task MektubSecAsync()
        {
            using var f = new Form { Text = Lang.T("Qeydiyyatdakı məktubu seç"), Size = new Size(560, 460), StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedDialog, MinimizeBox = false, MaximizeBox = false, BackColor = Theme.Card, Font = F(10f) };
            var axt = new RoundTextBox { Dock = DockStyle.Top, Height = 32, PlaceholderText = Lang.T("Məktubun nömrəsi") };
            var lb = new ListBox { Dock = DockStyle.Fill, BorderStyle = BorderStyle.None, BackColor = Theme.Card, ForeColor = Theme.Ink, IntegralHeight = false, ItemHeight = 24 };
            var ok = new FlatButton { Text = Lang.T("Seç"), Glyph = Icons.Check, Dock = DockStyle.Bottom, Height = 38 };
            f.Controls.Add(lb); f.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 8 }); f.Controls.Add(axt); f.Controls.Add(ok);
            f.Padding = new Padding(12);
            List<(long id, string ad)> l = new();
            async Task Doldur() { l = await SorguRepo.MektublarAsync(Session.ZavodFiltri, axt.Text); lb.Items.Clear(); lb.Items.AddRange(l.Select(x => (object)x.ad).ToArray()); }
            axt.TextChanged += async (_, _) => await Doldur();
            ok.Click += (_, _) => { if (lb.SelectedIndex >= 0) f.DialogResult = DialogResult.OK; };
            lb.DoubleClick += (_, _) => { if (lb.SelectedIndex >= 0) f.DialogResult = DialogResult.OK; };
            await Doldur();
            if (f.ShowDialog(FindForm()) != DialogResult.OK || lb.SelectedIndex < 0) return;
            _texGuid = null;
            await YukleAsync(l[lb.SelectedIndex].id);
        }

        // ===================================================== Texnika
        private async Task TexnikaBaglaAsync()
        {
            if (_s == null) return;
            string zavod = _s.V.GetValueOrDefault("Zavod") ?? Session.ZavodFiltri;
            using var f = new Form { Text = Lang.T("Texnika seç"), Size = new Size(560, 460), StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedDialog, MinimizeBox = false, MaximizeBox = false, BackColor = Theme.Card, Font = F(10f) };
            var axt = new RoundTextBox { Dock = DockStyle.Top, Height = 32, PlaceholderText = Lang.T("Zavod nömrəsi, naryad nömrəsi və ya marka") };
            var lb = new ListBox { Dock = DockStyle.Fill, BorderStyle = BorderStyle.None, BackColor = Theme.Card, ForeColor = Theme.Ink, IntegralHeight = false, ItemHeight = 24 };
            var ok = new FlatButton { Text = Lang.T("Bağla"), Glyph = Icons.Check, Dock = DockStyle.Bottom, Height = 38 };
            f.Controls.Add(lb); f.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 8 }); f.Controls.Add(axt); f.Controls.Add(ok);
            f.Padding = new Padding(12);
            List<SorguTexnika> l = new();
            async Task Doldur() { l = await SorguRepo.TexnikaAxtarAsync(axt.Text, zavod); lb.Items.Clear(); lb.Items.AddRange(l.Select(x => (object)$"{x.Marka} · {x.ZavodNo} · {Lang.T(x.Status)}").ToArray()); }
            axt.TextChanged += async (_, _) => await Doldur();
            ok.Click += (_, _) => { if (lb.SelectedIndex >= 0) f.DialogResult = DialogResult.OK; };
            lb.DoubleClick += (_, _) => { if (lb.SelectedIndex >= 0) f.DialogResult = DialogResult.OK; };
            await Doldur();
            if (f.ShowDialog(FindForm()) != DialogResult.OK || lb.SelectedIndex < 0) return;
            await TexnikaBaglaAsync(l[lb.SelectedIndex]);
        }

        internal async Task TexnikaBaglaAsync(SorguTexnika t)
        {
            if (_s.Tex.Any(x => x.Guid == t.Guid)) { Toast.Show(this, Lang.T("Bu texnika artıq bağlıdır"), ToastKind.Warn); return; }
            await SorguRepo.BaglaAsync(_s.Id, t.Guid, new() { ["Marka"] = t.Marka, ["ZavodNo"] = t.ZavodNo, ["Status"] = t.Status }, true);
            await SistemRepo.LogAsync("Redaktə", $"{Lang.T("Məktub")}: {_s.V["MektubNo"]} → {t.Marka} ({t.ZavodNo})");
            await YukleAsync(_s.Id, t.Guid);
            Toast.Show(this, Lang.T("Məktub texnikaya bağlandı"));
        }

        // ===================================================== Ehtiyat hissələri
        private void HisseleriGoster()
        {
            gridHis.Rows.Clear();
            string R(double d) => d.ToString("0.##", CultureInfo.InvariantCulture);
            foreach (var h in _hisseler) gridHis.Rows.Add(h.Ad, string.IsNullOrEmpty(h.Kod) ? "—" : h.Kod, h.Vahid, R(h.Teleb), R(h.Qebul), R(h.Qaliq));
            chipTeminat.Text = TexnikaJson.Teminat(_hisseler) ?? "—";
            qHisse.Items.Clear(); _orig[qHisse].Clear();
            foreach (var h in _hisseler) { string a = h.Ad + (string.IsNullOrEmpty(h.Kod) ? "" : " · " + h.Kod); qHisse.Items.Add(a); _orig[qHisse].Add(a); }
            if (qHisse.Items.Count > 0) qHisse.SelectedIndex = 0;
        }

        /// <summary>Texnikanın hissələri dərhal yazılır; məktubun hissə siyahısı və nəticəsi TexnikaRepo-da yenilənir.</summary>
        private async Task HisseYazAsync()
        {
            if (_texId is not long id) return;
            await TexnikaRepo.SaxlaAsync(id, new() { ["Hisseler"] = TexnikaJson.Yaz(_hisseler), ["Teminat"] = TexnikaJson.Teminat(_hisseler) });
            await YukleAsync(_s.Id);
            Toast.Show(this, Lang.T("Ehtiyat hissələri yadda saxlanıldı"));
        }

        private async Task HisseElaveAsync()
        {
            if (string.IsNullOrWhiteSpace(hAd.Text) || string.IsNullOrWhiteSpace(hVahid.Text) || !Reqem(hTeleb.Text, out var t) || t <= 0)
            { Toast.Show(this, Lang.T("Adı, ölçü vahidini və tələb olunan miqdarı (rəqəm) yazın"), ToastKind.Error); return; }
            _hisseler.Add(new Hisse { Ad = hAd.Text.Trim(), Kod = string.IsNullOrWhiteSpace(hKod.Text) ? null : hKod.Text.Trim(), Vahid = hVahid.Text.Trim(), Teleb = t });
            hAd.Text = hKod.Text = hVahid.Text = hTeleb.Text = "";
            await HisseYazAsync();
        }

        private void QebulPaneli()
        {
            if (_hisseler.Count == 0) { Toast.Show(this, Lang.T("Əvvəlcə ehtiyat hissəsini əlavə edin"), ToastKind.Warn); return; }
            qTarix.Value = DateTime.Today; qTarix.Checked = true; qMiqdar.Text = "";
            pnlQebul.Visible = !pnlQebul.Visible;
        }

        private async Task QebulElaveAsync()
        {
            if (qHisse.SelectedIndex < 0 || !qTarix.Checked || !Reqem(qMiqdar.Text, out var m) || m <= 0)
            { Toast.Show(this, Lang.T("Ehtiyat hissəsini, qəbul tarixini və miqdarı (rəqəm) yazın"), ToastKind.Error); return; }
            var h = _hisseler[qHisse.SelectedIndex];
            if (h.Qebul + m > h.Teleb && !Ui.Tesdiq(FindForm(), Lang.T("Qəbul"), Lang.T("Qəbul edilən miqdar tələb olunandan çoxdur. Davam edilsin?"))) return;
            h.Qebullar.Add(new HisseQebul { Tarix = Vaxt.Iso(qTarix.Value.Date), Miqdar = m });
            await HisseYazAsync();
        }

        // ===================================================== İcra qeydləri
        private void QeydleriGoster()
        {
            gridQeyd.Rows.Clear();
            if (_s == null) return;
            foreach (var q in SorguRepo.QeydJson(_s.V.GetValueOrDefault("Qeydler")).OrderByDescending(q => q.Tarix))
                gridQeyd.Rows.Add(Vaxt.Tarix(q.Tarix), q.Netice, Vaxt.Tarix(q.Gozlenilen) is { Length: > 0 } g ? g : "—");
        }

        private async Task QeydElaveAsync()
        {
            if (_s == null) return;
            if (!iTarix.Checked || string.IsNullOrWhiteSpace(iNetice.Text)) { Toast.Show(this, Lang.T("Əlaqə tarixini və nəticəni yazın"), ToastKind.Error); return; }
            var l = SorguRepo.QeydJson(_s.V.GetValueOrDefault("Qeydler"));
            l.Add(new SorguQeyd { Tarix = Vaxt.Iso(iTarix.Value.Date), Netice = iNetice.Text.Trim(), Gozlenilen = iGozl.Checked ? Vaxt.Iso(iGozl.Value.Date) : null });
            _s.V["Qeydler"] = System.Text.Json.JsonSerializer.Serialize(l);
            await SorguRepo.SaxlaAsync(_s);
            iNetice.Text = ""; iGozl.Checked = false;
            await YukleAsync(_s.Id);
            Toast.Show(this, Lang.T("İcra qeydi yadda saxlanıldı"));
        }
    }
}
