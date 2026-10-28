using System.Data;
using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Forms;

namespace TexnikaReyestri.Pages
{
    /// <summary>
    /// Reyestr: VirtualMode cədvəl. Sətirlər 200-lük hissələrlə (keyset) yüklənir; aşağı sürüşdükcə növbəti hissə gəlir.
    /// 1 000 000 sətirdə də ilk ekran dərhal açılır, UI donmur.
    /// </summary>
    public partial class RegistryPage : PageBase
    {
        private ReyestrFiltr _f = new();
        private readonly List<object[]> _rows = new();
        private string _bazaVersiya;
        private long _total; private bool _loading, _bitdi; private int _versiya;
        private readonly System.Windows.Forms.Timer _axtar = new() { Interval = 300 };   // yazarkən hər hərfdə sorğu getməsin
        private bool _hazir;

        public RegistryPage()
        {
            InitializeComponent();
            grid.BasliqSrift = new Font("Segoe UI", 11f, FontStyle.Bold); grid.ColumnHeadersHeight = 44; grid.ApplyColors();
            colBitme.DisplayIndex = colStatus.DisplayIndex;   // "Ehtiyat hissələrinin təminatı" — İcra statusu ilə Məsul icraçı arasında
            _axtar.Tick += async (_, _) => { _axtar.Stop(); await YenidenAsync(); };
            txtSearch.TextChanged += (_, _) => { if (_hazir) { _axtar.Stop(); _axtar.Start(); } };
            cmbZavod.Items.Add("Hamısı"); cmbZavod.Items.AddRange(Secimler.Sahe["TemirZavodu"]);
            cmbStatus.Items.Add("Hamısı"); cmbStatus.Items.AddRange(StatusSecim.Select(x => x.ad).ToArray());
            cmbZavod.SelectedIndex = 0; cmbStatus.SelectedIndex = 0;
            FiltrPaneliQur();
            grid.WheelScrolled += (_, _) => ElaveYukle();
            grid.KeyDown += (_, e) => { if (e.KeyCode is Keys.Down or Keys.PageDown or Keys.End) ElaveYukle(); if (e.KeyCode == Keys.Enter && grid.CurrentRow != null) { e.Handled = true; _ = DetalAc(grid.CurrentRow.Index); } };
            _hazir = true;
        }

        public override async Task OnShownAsync()
        {
            cmbZavod.Visible = lblZavod.Visible = Session.ZavodFiltri == null;
            await IcraVeMesulYenileAsync();
            btnImport.Visible = Session.YazaBiler;
            // Bazada dəyişiklik olubsa (yeni texnika, redaktə, idxal) siyahını yenidən yüklə, yoxsa yalnız kartları yenilə
            if (_rows.Count == 0 || await TexnikaRepo.SonDeyisikAsync() != _bazaVersiya) await YenidenAsync(); else await KartlarAsync();
        }

        /// <summary>Başqa səhifədən filtrlə açmaq (ana səhifə kartları, bildirişlər).</summary>
        public async Task FiltrTetbiqAsync(ReyestrFiltr f)
        {
            _hazir = false;
            cmbZavod.SelectedIndex = f.Zavod == null || Session.ZavodFiltri != null ? 0 : Math.Max(0, cmbZavod.Items.IndexOf(f.Zavod));
            // Kart qrupu uyğun açılan siyahıya köçürülür; "davam" və "cemi" üçün siyahıda yer yoxdur — gizli qalır
            int si = f.Status == null ? 0 : Array.FindIndex(StatusSecim, x => x.status == f.Status) + 1;
            if (f.Qrup is "gozl" or "tehvil") si = Array.FindIndex(StatusSecim, x => x.qrup == f.Qrup || x.status == "Təhvil verilib" && f.Qrup == "tehvil") + 1;
            cmbStatus.SelectedIndex = Math.Max(0, si);
            cmbIcra.SelectedIndex = Math.Max(0, Array.IndexOf(IcraKod, f.Qrup is "yaxin" or "late" ? f.Qrup : f.Icra));
            cmbMesul.SelectedIndex = f.Mesul == null ? 0 : Math.Max(0, cmbMesul.Items.IndexOf(f.Mesul));
            chkTeminat.Checked = f.TeminatGozleyir;
            dtDan.Checked = dtDek.Checked = false;
            txtSearch.Text = f.Axtar ?? "";
            _qrup = f.Qrup is "davam" ? f.Qrup : null; _hazir = true;
            await YenidenAsync();
        }
        private string _qrup;   // kartdan gələn, siyahılarda olmayan qrup ("davam")

        // Status siyahısı: bazadakı dəyər və ya kart qrupu; ad ekranda Secimler.Ekran ilə göstərilir
        private static readonly (string ad, string status, string qrup)[] StatusSecim =
        {
            ("Qəbul edilib", "Qəbul edilib", null), ("Təmirdədir", "Təmirdə", null), ("Təminat gözləyir", "Təminat gözləyir", null),
            ("Təmir tamamlanıb, təhvil gözlənilir", null, "gozl"), ("Təhvil verilib", "Təhvil verilib", null), ("Təmirə yararsız", "Təmirə yararsız", null)
        };
        private static readonly string[] IcraKod = { null, "sari", "yaxin", "late", "bos" };
        private static readonly string[] TarixKod = { "GozlenilenBitme", "FaktikiBitme", "DaxilolmaTarixi", "BaslamaTarixi", "TehvilTarixi" };
        private RoundComboBox cmbIcra, cmbMesul, cmbTarix; private CheckBox chkTeminat; private RoundDatePicker dtDan, dtDek; private Label lblZavod;

        /// <summary>Filtr paneli (kartların altında): 2 sətir — Zavod, Status, İcra müddəti, Məsul icraçı / Axtarış, Tarix növü, Tarixdən, Tarixədək.</summary>
        private void FiltrPaneliQur()
        {
            RoundComboBox Cmb(params string[] items) { var c = new RoundComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10f), Dock = DockStyle.Fill, Margin = new Padding(0, 0, 12, 0) }; c.Items.AddRange(items); c.SelectedIndex = 0; c.SelectedIndexChanged += filtr_Changed; return c; }
            RoundDatePicker Dt() { var d = new RoundDatePicker { ShowCheckBox = true, Checked = false, Dock = DockStyle.Fill, Margin = new Padding(0, 0, 12, 0) }; d.ValueChanged += filtr_Changed; return d; }
            Label L(string t) => new() { Text = t, AutoSize = false, Dock = DockStyle.Fill, Tag = "muted", Font = new Font("Segoe UI Semibold", 9f), TextAlign = ContentAlignment.BottomLeft, Margin = new Padding(0, 0, 12, 2) };
            cmbIcra = Cmb("Hamısı", "", "", "İcra müddəti keçib", "İcra müddəti müəyyən edilməyib");
            cmbMesul = Cmb("Hamısı");
            cmbTarix = Cmb("Plan üzrə bitmə tarixi", "Faktiki tamamlanma tarixi", "Zavoda daxilolma tarixi", "Diaqnostika tarixi", "Faktiki təhvil tarixi");
            chkTeminat = new CheckBox { Text = "Təminat gözləyir", AutoSize = true, Font = new Font("Segoe UI Semibold", 9.5f), Anchor = AnchorStyles.Left, Margin = new Padding(0, 14, 12, 0), Cursor = Cursors.Hand };
            chkTeminat.CheckedChanged += filtr_Changed;
            dtDan = Dt(); dtDek = Dt();
            foreach (var c in new Control[] { cmbZavod, cmbStatus, txtSearch }) { c.Anchor = AnchorStyles.Left | AnchorStyles.Top; c.Dock = DockStyle.Fill; c.Margin = new Padding(0, 0, 12, 0); }
            txtSearch.PlaceholderText = "Texnikanın adı, inventar kodu və ya məktub nömrəsi";
            var t = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 5, BackColor = Color.Transparent };
            for (int i = 0; i < 4; i++) t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            foreach (var h in new[] { 22, 36, 26, 36, 46 }) t.RowStyles.Add(new RowStyle(SizeType.Absolute, h));
            t.Controls.Add(lblZavod = L("Zavod:"), 0, 0); t.Controls.Add(L("Status:"), 1, 0); t.Controls.Add(L("İcra müddəti:"), 2, 0); t.Controls.Add(L("Məsul icraçı:"), 3, 0);
            t.Controls.Add(cmbZavod, 0, 1); t.Controls.Add(cmbStatus, 1, 1); t.Controls.Add(cmbIcra, 2, 1); t.Controls.Add(cmbMesul, 3, 1);
            t.Controls.Add(L("Tarix növü:"), 1, 2); t.Controls.Add(L("Tarixdən:"), 2, 2); t.Controls.Add(L("Tarixədək:"), 3, 2);
            t.Controls.Add(txtSearch, 0, 3); t.Controls.Add(cmbTarix, 1, 3); t.Controls.Add(dtDan, 2, 3); t.Controls.Add(dtDek, 3, 3);
            // Ehtiyat hissəsi gözləyən texnikalar: "Təminat gözləyir" statusu
            t.Controls.Add(chkTeminat, 0, 4); t.SetColumnSpan(chkTeminat, 2);
            var rp = new RoundPanel { Dock = DockStyle.Top, Height = 196, Padding = new Padding(14, 8, 2, 10), Margin = new Padding(0, 0, 0, 8) };
            rp.Controls.Add(t); Controls.Add(rp);
            // Dok sırası: başlıq → kartlar → filtr → cədvəl
            rp.BringToFront(); rpGrid.BringToFront();
        }

        /// <summary>İcra müddəti siyahısının adları Admin hədlərindən; məsul icraçılar bazadan.</summary>
        private async Task IcraVeMesulYenileAsync()
        {
            int n = await KartPaneli.YaxinGunAsync(), m = Math.Max(n + 1, ReyestrFiltr.Sari);
            bool h = _hazir; _hazir = false;
            cmbIcra.Items[1] = $"{n + 1}–{m} gün qalıb"; cmbIcra.Items[2] = $"0–{n} gün qalıb";
            string sec = cmbMesul.SelectedIndex > 0 ? (string)cmbMesul.SelectedItem : null;
            var l = await TexnikaRepo.MesullarAsync(Session.ZavodFiltri);
            cmbMesul.BeginUpdate(); while (cmbMesul.Items.Count > 1) cmbMesul.Items.RemoveAt(1); cmbMesul.Items.AddRange(l.ToArray()); cmbMesul.EndUpdate();
            cmbMesul.SelectedIndex = sec == null ? 0 : Math.Max(0, cmbMesul.Items.IndexOf(sec));
            _hazir = h;
        }

        public void AxtarisaFokus() { txtSearch.Focus(); txtSearch.SelectAll(); }

        private ReyestrFiltr Filtr() => new()
        {
            Zavod = Session.ZavodFiltri ?? (cmbZavod.SelectedIndex > 0 ? (string)Secimler.Sahe["TemirZavodu"][cmbZavod.SelectedIndex - 1] : null),
            Status = cmbStatus.SelectedIndex > 0 ? StatusSecim[cmbStatus.SelectedIndex - 1].status : null,
            Qrup = cmbStatus.SelectedIndex > 0 && StatusSecim[cmbStatus.SelectedIndex - 1].qrup is { } q ? q : _qrup,
            Icra = IcraKod[Math.Max(0, cmbIcra.SelectedIndex)],
            Mesul = cmbMesul.SelectedIndex > 0 ? (string)cmbMesul.SelectedItem : null,
            TeminatGozleyir = chkTeminat.Checked,
            TarixSahe = TarixKod[Math.Max(0, cmbTarix.SelectedIndex)],
            TarixDan = dtDan.Checked ? Vaxt.Iso(dtDan.Value.Date) : null, TarixDek = dtDek.Checked ? Vaxt.Iso(dtDek.Value.Date) : null,
            Axtar = txtSearch.Text.Trim()
        };

        private async void filtr_Changed(object sender, EventArgs e) { if (_hazir) { _qrup = null; await YenidenAsync(); } }

        private async Task YenidenAsync()
        {
            int v = ++_versiya; _f = Filtr();
            lblTitle.Text = Lang.T("Təmir reyestri") + " · " + (_f.Zavod ?? Lang.T("bütün zavodlar"));   // hansı zavoda baxıldığı görünsün
            _bazaVersiya = await TexnikaRepo.SonDeyisikAsync();
            _rows.Clear(); _bitdi = false; grid.RowCount = 0;
            await Ui.Is(rpGrid, "Reyestr yüklənir...", async _ =>
            {
                _total = await Task.Run(() => TexnikaRepo.SayAsync(_f));
                if (v != _versiya) return;
                await NovbetiAsync(v);
            });
            await KartlarAsync();
            Sayac();
        }

        private async Task NovbetiAsync(int v)
        {
            if (_loading || _bitdi) return;
            _loading = true;
            try
            {
                Kursor? k = _rows.Count == 0 ? null : new Kursor(_rows[^1][6] as string, (long)_rows[^1][0]);
                var dt = await Task.Run(() => TexnikaRepo.SehifeAsync(_f, k));
                if (v != _versiya) return;
                foreach (DataRow r in dt.Rows) _rows.Add(new object[] { r["Id"], _rows.Count + 1, r["Nov"], r["Marka"], r["ZavodNo"], r["Status"], r["GozlenilenBitme"], r["TemirZavodu"], r["MesulIcraci"], r["Teminat"] });
                if (dt.Rows.Count < TexnikaRepo.SehifeOlcusu) _bitdi = true;
                grid.RowCount = _rows.Count; grid.Invalidate();
            }
            finally { _loading = false; Sayac(); }
        }

        // Ekranın sonuna 40 sətir qalanda növbəti hissəni gətir
        private void ElaveYukle()
        {
            if (grid.RowCount == 0) return;
            int son = grid.FirstDisplayedScrollingRowIndex + grid.DisplayedRowCount(true);
            if (son >= _rows.Count - 40) _ = NovbetiAsync(_versiya);
        }
        private void grid_Scroll(object sender, ScrollEventArgs e) => ElaveYukle();

        private void Sayac()
        {
            lblCount.Text = $"{Lang.T("Göstərilir")}: {_rows.Count:#,0} / {_total:#,0}".Replace(",", " ")
                + (_qrup == "davam" ? " · " + Lang.T("təmiri davam edənlər") : "");
        }

        private async Task KartlarAsync()
        {
            int n = await KartPaneli.YaxinGunAsync(); string z = _f.Zavod;
            var d = await Task.Run(() => TexnikaRepo.KartStatAsync(z, n));
            tlpTiles.Goster(KartStat.Cem(d.Values), n);
        }

        private void grid_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex >= _rows.Count) return;
            var r = _rows[e.RowIndex];
            e.Value = e.ColumnIndex switch
            {
                // Görünən sütunlar: Texnika / inventar kodu, Zavod, İcra statusu, Məsul icraçı
                1 => string.Join(" · ", new[] { r[3], r[4] }.OfType<string>().Where(x => x != "")), 2 => r[7] is string z ? Lang.T(z) : "",
                3 => r[5] is DBNull ? "" : r[5],   // nişan kimi çəkilir (ModernGrid, ad "Status")
                4 => r[8] is DBNull ? "" : r[8],
                5 => r[5] as string == Secimler.StTeminat ? Secimler.StTeminat : "—",   // təminat: nişan kimi çəkilir; köhnə avtomatik təminat sahəsi göstərilmir
                6 => Lang.T("Geniş bax →"), _ => null
            };
        }

        private static bool Gecikir(object[] r) => r[6] is string s && string.CompareOrdinal(s, Vaxt.Iso(DateTime.Today)) < 0 && r[5] is string st && Secimler.AktivStatus.Contains(st);

        private void grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        private async void grid_CellContentClick(object sender, DataGridViewCellEventArgs e) { if (e.RowIndex >= 0 && e.ColumnIndex == 6) await DetalAc(e.RowIndex); }
        private async void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { if (e.RowIndex >= 0) await DetalAc(e.RowIndex); }

        private Task DetalAc(int i) => MainForm.Current.DetalAcAsync((long)_rows[i][0], _rows.Select(r => (long)r[0]).ToList());

        private async void kart_Clicked(object sender, string qrup) => await FiltrTetbiqAsync(new ReyestrFiltr { Zavod = _f.Zavod, Qrup = qrup });

        private async void btnImport_Click(object sender, EventArgs e)
        {
            using var f = new ExcelImportForm();
            if (f.ShowDialog(this) == DialogResult.OK) await YenidenAsync();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            long? sel = grid.CurrentRow != null && grid.CurrentRow.Index < _rows.Count ? (long)_rows[grid.CurrentRow.Index][0] : null;
            using var f = new ExcelExportForm(_f, _total, sel);
            f.ShowDialog(this);
        }

        public void Yenile() => _ = YenidenAsync();
    }
}
