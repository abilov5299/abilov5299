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
            _axtar.Tick += async (_, _) => { _axtar.Stop(); await YenidenAsync(); };
            txtSearch.TextChanged += (_, _) => { if (_hazir) { _axtar.Stop(); _axtar.Start(); } };
            cmbZavod.Items.Add("Zavod: Hamısı"); cmbZavod.Items.AddRange(Secimler.Sahe["TemirZavodu"]);
            cmbStatus.Items.Add("Status: Hamısı"); cmbStatus.Items.AddRange(Secimler.Status);
            cmbZavod.SelectedIndex = 0; cmbStatus.SelectedIndex = 0;
            grid.WheelScrolled += (_, _) => ElaveYukle();
            grid.KeyDown += (_, e) => { if (e.KeyCode is Keys.Down or Keys.PageDown or Keys.End) ElaveYukle(); if (e.KeyCode == Keys.Enter && grid.CurrentRow != null) { e.Handled = true; _ = DetalAc(grid.CurrentRow.Index); } };
            _hazir = true;
        }

        public override async Task OnShownAsync()
        {
            cmbZavod.Visible = Session.ZavodFiltri == null;
            btnImport.Visible = Session.YazaBiler;
            // Bazada dəyişiklik olubsa (yeni texnika, redaktə, idxal) siyahını yenidən yüklə, yoxsa yalnız kartları yenilə
            if (_rows.Count == 0 || await TexnikaRepo.SonDeyisikAsync() != _bazaVersiya) await YenidenAsync(); else await KartlarAsync();
        }

        /// <summary>Başqa səhifədən filtrlə açmaq (ana səhifə kartları, bildirişlər).</summary>
        public async Task FiltrTetbiqAsync(ReyestrFiltr f)
        {
            _hazir = false;
            cmbZavod.SelectedIndex = f.Zavod == null || Session.ZavodFiltri != null ? 0 : Math.Max(0, cmbZavod.Items.IndexOf(f.Zavod));
            cmbStatus.SelectedIndex = f.Status == null ? 0 : Math.Max(0, cmbStatus.Items.IndexOf(f.Status));
            txtSearch.Text = f.Axtar ?? "";
            _gecikenler = f.Gecikenler; _hazir = true;
            await YenidenAsync();
        }
        private bool _gecikenler;

        public void AxtarisaFokus() { txtSearch.Focus(); txtSearch.SelectAll(); }

        private ReyestrFiltr Filtr() => new()
        {
            Zavod = Session.ZavodFiltri ?? (cmbZavod.SelectedIndex > 0 ? (string)Secimler.Sahe["TemirZavodu"][cmbZavod.SelectedIndex - 1] : null),
            Status = cmbStatus.SelectedIndex > 0 ? Secimler.Status[cmbStatus.SelectedIndex - 1] : null,
            Axtar = txtSearch.Text.Trim(), Gecikenler = _gecikenler
        };

        private async void filtr_Changed(object sender, EventArgs e) { if (_hazir) { _gecikenler = false; await YenidenAsync(); } }

        private async Task YenidenAsync()
        {
            int v = ++_versiya; _f = Filtr();
            lblTitle.Text = Lang.T("Reyestr") + " · " + (_f.Zavod ?? Lang.T("bütün zavodlar"));   // hansı zavoda baxıldığı görünsün
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
                foreach (DataRow r in dt.Rows) _rows.Add(new object[] { r["Id"], _rows.Count + 1, r["Nov"], r["Marka"], r["ZavodNo"], r["Status"], r["GozlenilenBitme"] });
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
                + (_gecikenler ? " · " + Lang.T("yalnız gecikənlər") : "");
        }

        private async Task KartlarAsync()
        {
            var b = new ReyestrFiltr { Zavod = _f.Zavod };
            cardAll.Value = (await TexnikaRepo.SayAsync(b)).ToString("#,0").Replace(",", " ");
            cardRepair.Value = (await TexnikaRepo.SayAsync(new() { Zavod = b.Zavod, Status = "Təmirdə" })).ToString("#,0").Replace(",", " ");
            cardLate.Value = (await Task.Run(() => TexnikaRepo.SayAsync(new() { Zavod = b.Zavod, Gecikenler = true }))).ToString("#,0").Replace(",", " ");
            cardDone.Value = (await TexnikaRepo.SayAsync(new() { Zavod = b.Zavod, Status = "Təhvil verilib" })).ToString("#,0").Replace(",", " ");
        }

        private void grid_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex >= _rows.Count) return;
            var r = _rows[e.RowIndex];
            e.Value = e.ColumnIndex switch
            {
                0 => r[1], 1 => r[2] is DBNull ? "" : Lang.T((string)r[2]), 2 => r[3], 3 => r[4], 4 => r[5] is DBNull ? "" : r[5],
                5 => r[6] is string s ? Vaxt.Tarix(s) + (Gecikir(r) ? $" · {(DateTime.Today - DateTime.Parse(s)).Days} {Lang.T("gün gecikir")}" : "") : "",
                6 => Lang.T("Geniş bax →"), _ => null
            };
        }

        private static bool Gecikir(object[] r) => r[6] is string s && string.CompareOrdinal(s, Vaxt.Iso(DateTime.Today)) < 0 && r[5] is string st && Secimler.AktivStatus.Contains(st);

        private void grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex < _rows.Count && Gecikir(_rows[e.RowIndex])) { e.CellStyle.ForeColor = Theme.Bad; e.CellStyle.Font = new Font(grid.Font, FontStyle.Bold); }
        }

        private async void grid_CellContentClick(object sender, DataGridViewCellEventArgs e) { if (e.RowIndex >= 0 && e.ColumnIndex == 6) await DetalAc(e.RowIndex); }
        private async void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { if (e.RowIndex >= 0) await DetalAc(e.RowIndex); }

        private Task DetalAc(int i) => MainForm.Current.DetalAcAsync((long)_rows[i][0], _rows.Select(r => (long)r[0]).ToList());

        private async void card_Click(object sender, EventArgs e)
        {
            var c = (StatCard)sender;
            await FiltrTetbiqAsync(new ReyestrFiltr { Zavod = _f.Zavod, Status = c.FilterKey is "" or "late" ? null : c.FilterKey, Gecikenler = c.FilterKey == "late" });
        }

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
