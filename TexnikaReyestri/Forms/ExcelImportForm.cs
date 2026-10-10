using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Services;

namespace TexnikaReyestri.Forms
{
    /// <summary>Excel idxalı: 1) fayl 2) sütunların uyğunlaşdırılması 3) yoxlama 4) nəticə.</summary>
    public partial class ExcelImportForm : Form
    {
        private int _addim; private ExcelService.Fayl _f; private List<IdxalSetri> _s; private int _yeni, _yen;
        private Panel[] Addimlar => new[] { step1, step2, step3, step4 };

        public ExcelImportForm()
        {
            InitializeComponent();
            pnlTop.MouseDown += (_, e) => { if (e.Button == MouseButtons.Left) Win32.DragWindow(this); };
            gridMap.RowTemplate.Height = 32; gridCheck.RowTemplate.Height = 30;
            foreach (DataGridViewColumn c in gridMap.Columns) c.ReadOnly = c.Name != "colField";
        }

        private void ExcelImportForm_Load(object sender, EventArgs e)
        {
            Ui.Hazirla(this); BackColor = Theme.Line;
            colField.Items.Add("— ötür —"); foreach (var s in Saheler.Hamisi) colField.Items.Add(s.Ad);
            Goster(0);
        }

        private void Goster(int a)
        {
            _addim = a; for (int i = 0; i < 4; i++) Addimlar[i].Visible = i == a;
            stepper.CurrentStep = a == 3 ? 4 : a;
            btnBack.Visible = a is 1 or 2; btnNext.Enabled = a != 0 || _f != null;
            btnNext.Text = Lang.T(a == 2 ? "İdxal et" : a == 3 ? "Bağla" : "Növbəti"); btnNext.Glyph = a == 2 ? Icons.Upload : "";
        }

        // ---------- Addım 1 ----------
        private void btnPick_Click(object sender, EventArgs e)
        {
            using var d = new OpenFileDialog { Filter = "Excel|*.xlsx;*.xlsm" };
            if (d.ShowDialog(this) == DialogResult.OK) _ = AcAsync(d.FileName);
        }
        private void pnlDrop_DragEnter(object sender, DragEventArgs e) { if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy; }
        private void pnlDrop_DragDrop(object sender, DragEventArgs e) { if (e.Data.GetData(DataFormats.FileDrop) is string[] f && f.Length > 0) _ = AcAsync(f[0]); }

        private async Task AcAsync(string yol, string vereq = null)
        {
            await Ui.Is(pnlHost, "Excel faylı oxunur...", async _ =>
            {
                _f = await Task.Run(() => ExcelService.Ac(yol, vereq));
                lblFile.Text = $"📄 {Path.GetFileName(yol)} · {new FileInfo(yol).Length / 1024} KB";
                cmbSheet.SelectedIndexChanged -= cmbSheet_SelectedIndexChanged;
                cmbSheet.Items.Clear(); cmbSheet.Items.AddRange(_f.Vereqler.ToArray()); cmbSheet.SelectedItem = _f.Vereq;
                cmbSheet.SelectedIndexChanged += cmbSheet_SelectedIndexChanged;
                vHeader.Text = $"{_f.BaslikSetri}"; vData.Text = $"{_f.MelumatBaslayir}";
                int ok = _f.Sutunlar.Count(s => s.Avtomatik);
                lblAuto.Text = $"✓ {Lang.T("Başlıq sətri avtomatik tapıldı")}. {ok} {Lang.T("sütun avtomatik uyğunlaşdırıldı")}.";
                Goster(0);
            });
        }

        private async void cmbSheet_SelectedIndexChanged(object sender, EventArgs e) { if (_f != null && cmbSheet.SelectedItem is string v && v != _f.Vereq) await AcAsync(_f.Yol, v); }

        // ---------- Addım 2 ----------
        private void Uygun()
        {
            gridMap.Rows.Clear();
            foreach (var s in _f.Sutunlar)
            {
                string ad = s.SaheKod == null ? "— ötür —" : Saheler.Hamisi.First(x => x.Kod == s.SaheKod).Ad;
                gridMap.Rows.Add($"{ColName(s.Sutun)} · {s.ExcelAd}", s.Numune, ad, s.SaheKod == null ? "Rədd edildi" : s.Shubheli ? "Hissə gözləyir" : "Təhvil verilib");
            }
            Xulase();
        }
        private void Xulase()
        {
            int ok = _f.Sutunlar.Count(s => s.SaheKod != null && !s.Shubheli), sh = _f.Sutunlar.Count(s => s.Shubheli && s.SaheKod != null), no = _f.Sutunlar.Count(s => s.SaheKod == null);
            lblMapSum.Text = $"✓ {ok} {Lang.T("uyğun")} · ? {sh} {Lang.T("yoxlanmalıdır")} · ✕ {no} {Lang.T("ötürüləcək")}";
        }
        private static string ColName(int c) { string s = ""; for (; c > 0; c = (c - 1) / 26) s = (char)('A' + (c - 1) % 26) + s; return s; }

        private void gridMap_CurrentCellDirtyStateChanged(object sender, EventArgs e) { if (gridMap.IsCurrentCellDirty) gridMap.CommitEdit(DataGridViewDataErrorContexts.Commit); }
        private void gridMap_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || gridMap.Columns[e.ColumnIndex].Name != "colField" || _f == null) return;
            string v = gridMap.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as string;
            var s = _f.Sutunlar[e.RowIndex]; s.SaheKod = Saheler.Hamisi.FirstOrDefault(x => x.Ad == v)?.Kod; s.Shubheli = false;
            gridMap.Rows[e.RowIndex].Cells["colMapState"].Value = s.SaheKod == null ? "Rədd edildi" : "Təhvil verilib";
            Xulase();
        }

        // ---------- Addım 3 ----------
        private async Task YoxlaAsync()
        {
            await Ui.Is(pnlHost, "Sətirlər yoxlanılır...", async ov => _s = await ExcelService.YoxlaAsync(_f, new Progress<int>(p => ov.Report(p)), default));
            if (_s == null) return;
            cReady.Value = _s.Count(x => x.Veziyyet is SetirVeziyyet.Yeni or SetirVeziyyet.Yenilenecek).ToString(); cWarn.Value = _s.Count(x => x.Veziyyet == SetirVeziyyet.Xeberdarliq).ToString();
            cErr.Value = _s.Count(x => x.Veziyyet == SetirVeziyyet.Xeta).ToString(); cEmpty.Value = _s.Count(x => x.Veziyyet == SetirVeziyyet.Bos).ToString();
            gridCheck.Rows.Clear();
            foreach (var x in _s.Where(x => x.Veziyyet != SetirVeziyyet.Bos).Take(2000))
            {
                string res = x.Veziyyet switch { SetirVeziyyet.Yeni => "✓ " + Lang.T("Yeni"), SetirVeziyyet.Yenilenecek => "! " + Lang.T(x.Qeyd), SetirVeziyyet.Xeberdarliq => "! " + x.Qeyd, _ => "✕ " + x.Qeyd };
                int i = gridCheck.Rows.Add(x.Setir, Lang.T(x.Deyerler.GetValueOrDefault("Nov") as string ?? ""), x.Deyerler.GetValueOrDefault("Marka"), x.Deyerler.GetValueOrDefault("ZavodNo"), res);
                gridCheck.Rows[i].Tag = x.Veziyyet;
            }
            lblStatus.Text = $"{_s.Count(x => x.Veziyyet != SetirVeziyyet.Bos):#,0} {Lang.T("sətir oxundu")}";
            Goster(2);
        }

        private void gridCheck_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || gridCheck.Rows[e.RowIndex].Tag is not SetirVeziyyet v || e.ColumnIndex != 4) return;
            e.CellStyle.ForeColor = v switch { SetirVeziyyet.Xeta => Theme.Bad, SetirVeziyyet.Xeberdarliq or SetirVeziyyet.Yenilenecek => Theme.Warn, _ => Theme.Ok };
            e.CellStyle.Font = new Font(gridCheck.Font, FontStyle.Bold);
        }

        // ---------- Addım 4 ----------
        private async Task IdxalAsync()
        {
            var xeta = _s.Where(x => x.Veziyyet == SetirVeziyyet.Xeta).ToList();
            if (xeta.Count > 0 && radErrStop.Checked) { Toast.Show(this, $"{xeta.Count} {Lang.T("xətalı sətir var. Heç nə idxal edilmədi.")}", ToastKind.Error); return; }
            var yaz = _s.Where(x => x.Veziyyet is SetirVeziyyet.Yeni or SetirVeziyyet.Xeberdarliq || x.Veziyyet == SetirVeziyyet.Yenilenecek && radDupUpdate.Checked).ToList();
            _yeni = yaz.Count(x => !x.Deyerler.ContainsKey("Guid")); _yen = yaz.Count - _yeni;
            var sw = System.Diagnostics.Stopwatch.StartNew();
            bool ok = await Ui.Is(pnlHost, "İdxal olunur...", async ov =>
            {
                ov.Report(0, Lang.T("Ehtiyat nüsxə alınır...")); await BackupService.AlAsync(false);
                await TexnikaRepo.TopluYazAsync(yaz.Select(x => x.Deyerler), new Progress<int>(i => ov.Report(i * 100 / Math.Max(1, yaz.Count), $"{i:#,0} / {yaz.Count:#,0}")), default);
            });
            if (!ok) return;
            await SistemRepo.LogAsync("Əlavə", $"Excel idxalı: {Path.GetFileName(_f.Yol)} — {_yeni} yeni, {_yen} yeniləndi, {xeta.Count} xəta");
            dNew.Value = _yeni.ToString(); dUpd.Value = _yen.ToString(); dSkip.Value = xeta.Count.ToString(); dEmpty.Value = _s.Count(x => x.Veziyyet == SetirVeziyyet.Bos).ToString();
            lblDoneInfo.Text = $"{_s.Count:#,0} {Lang.T("sətir")} {sw.Elapsed.TotalSeconds:0.0} {Lang.T("saniyədə işləndi")}";
            btnErrReport.Visible = xeta.Count > 0;
            Goster(3);
        }

        private void btnErrReport_Click(object sender, EventArgs e)
        {
            using var d = new SaveFileDialog { Filter = "Excel|*.xlsx", FileName = "Idxal_xetalari.xlsx" };
            if (d.ShowDialog(this) != DialogResult.OK) return;
            ExcelService.XetaHesabati(d.FileName, _s.Where(x => x.Veziyyet == SetirVeziyyet.Xeta));
            Toast.Show(this, "Xəta hesabatı saxlanıldı");
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            switch (_addim)
            {
                case 0: Uygun(); Goster(1); break;
                case 1:
                    if (!_f.Sutunlar.Any(s => s.SaheKod == "ZavodNo")) { Toast.Show(this, "\"Zavod nömrəsi\" sütunu seçilməlidir.", ToastKind.Error); return; }
                    await YoxlaAsync(); break;
                case 2: await IdxalAsync(); break;
                default: DialogResult = DialogResult.OK; break;
            }
        }
        private void btnBack_Click(object sender, EventArgs e) => Goster(Math.Max(0, _addim - 1));
        private void btnCancel_Click(object sender, EventArgs e) => DialogResult = _addim == 3 ? DialogResult.OK : DialogResult.Cancel;

        protected override CreateParams CreateParams { get { var cp = base.CreateParams; cp.ClassStyle |= Win32.CS_DROPSHADOW; return cp; } }
        protected override void OnHandleCreated(EventArgs e) { base.OnHandleCreated(e); Win32.Yumru(this); }
    }
}
