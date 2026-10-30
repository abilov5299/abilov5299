using System.Globalization;
using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Forms;
using TexnikaReyestri.Services;

namespace TexnikaReyestri.Pages
{
    /// <summary>
    /// "Sorğular üzrə təminat": təchizat sorğuları əsasında qəbul edilmiş ehtiyat hissələri və materiallar. Hər sətir məktubun bir hissəsidir:
    /// tələb, qəbul, texnikaya sərf və anbar qalığı. Filtr: zavod, məktub, təminat vəziyyəti, məktubun tarixi, təmin edən qurum, axtarış.
    /// </summary>
    public class TeminatPage : PageBase
    {
        private readonly RoundComboBox cmbZavod = Cmb(), cmbMektub = Cmb(), cmbNetice = Cmb(), cmbTemin = Cmb();
        private readonly RoundDatePicker dtDan = new() { ShowCheckBox = true, Checked = false, Dock = DockStyle.Fill, Margin = new Padding(0, 0, 12, 0) };
        private readonly RoundDatePicker dtDek = new() { ShowCheckBox = true, Checked = false, Dock = DockStyle.Fill, Margin = new Padding(0, 0, 12, 0) };
        private readonly RoundTextBox txtAxtar = new() { PlaceholderText = "Hissə, kod və ya texnika", Font = new Font("Segoe UI", 10f), Dock = DockStyle.Fill, Margin = new Padding(0, 0, 12, 0) };
        private readonly ModernGrid grid = new() { Dock = DockStyle.Fill };
        // Məktub: kliklə axtarış pəncərəsi açılır (siyahı uzun olur); seçim cmbMektub-da saxlanır (ekranda görünmür)
        private readonly RoundTextBox txtMektub = new() { ReadOnly = true, Cursor = Cursors.Hand, Font = new Font("Segoe UI", 10f), Dock = DockStyle.Fill, Margin = new Padding(0, 0, 12, 0) };
        private readonly Label lblZavod, lblSay = new() { Dock = DockStyle.Bottom, Height = 24, Tag = "muted", TextAlign = ContentAlignment.MiddleLeft };
        private readonly FlatButton btnExcel = new() { Text = "Excel", Glyph = Icons.Excel, Kind = ButtonKind.Secondary, Size = new Size(110, 34) };
        private readonly FlatButton btnPdf = new() { Text = "Çap et (PDF)", Glyph = Icons.Print, Kind = ButtonKind.Secondary, Size = new Size(140, 34) };
        private readonly FlatButton btnMektub = new() { Text = "Məktub sənədi (PDF)", Glyph = Icons.Print, Kind = ButtonKind.Ghost, Size = new Size(190, 34) };
        private readonly System.Windows.Forms.Timer _axtar = new() { Interval = 300 };
        private List<TeminatSetir> _hamisi = new(), _gorunen = new();
        private List<(long id, string ad)> _mektublar = new();
        private bool _hazir;

        private static readonly string[] Netice = { "Təmin edilməyib", "Qismən təmin edilib", "Tam təmin edilib" };
        private static RoundComboBox Cmb() => new() { DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10f), Dock = DockStyle.Fill, Margin = new Padding(0, 0, 12, 0) };

        public TeminatPage()
        {
            var head = new Panel { Dock = DockStyle.Top, Height = 64 };
            head.Controls.Add(new Label { Text = "Sorğular üzrə təminat", Tag = "title", AutoSize = true, Font = new Font("Bahnschrift SemiBold", 18f), Location = new Point(0, 4) });
            head.Controls.Add(new Label { Text = "Təchizat sorğuları əsasında qəbul edilmiş ehtiyat hissələri və materiallar", Tag = "muted", AutoSize = true, Font = new Font("Segoe UI", 9.5f), Location = new Point(2, 40) });

            // --- Filtr ---
            Label L(string s) => new() { Text = s, AutoSize = false, Dock = DockStyle.Fill, Tag = "muted", Font = new Font("Segoe UI Semibold", 9f), TextAlign = ContentAlignment.BottomLeft, Margin = new Padding(0, 0, 12, 2) };
            cmbZavod.Items.Add("Hamısı"); cmbZavod.Items.AddRange(Secimler.Zavodlar); cmbZavod.SelectedIndex = 0;
            cmbNetice.Items.Add("Hamısı"); cmbNetice.Items.AddRange(Netice); cmbNetice.SelectedIndex = 0;
            cmbMektub.Items.Add("Hamısı"); cmbMektub.SelectedIndex = 0;
            cmbTemin.Items.Add("Hamısı"); cmbTemin.SelectedIndex = 0;
            var tl = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 4, BackColor = Color.Transparent };
            for (int i = 0; i < 4; i++) tl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            foreach (var h in new[] { 22, 36, 26, 36 }) tl.RowStyles.Add(new RowStyle(SizeType.Absolute, h));
            tl.Controls.Add(lblZavod = L("Zavod:"), 0, 0); tl.Controls.Add(L("Məktub:"), 1, 0); tl.Controls.Add(L("Təminat vəziyyəti:"), 2, 0); tl.Controls.Add(L("Təmin edən qurum:"), 3, 0);
            tl.Controls.Add(cmbZavod, 0, 1); tl.Controls.Add(txtMektub, 1, 1); tl.Controls.Add(cmbNetice, 2, 1); tl.Controls.Add(cmbTemin, 3, 1);
            tl.Controls.Add(L("Məktubun tarixi (başlanğıc):"), 0, 2); tl.Controls.Add(L("Son:"), 1, 2); tl.Controls.Add(L("Axtarış:"), 2, 2);
            tl.Controls.Add(dtDan, 0, 3); tl.Controls.Add(dtDek, 1, 3); tl.Controls.Add(txtAxtar, 2, 3);
            var bt = new FlowLayoutPanel { Dock = DockStyle.Fill, BackColor = Color.Transparent, WrapContents = false, Margin = new Padding(0) };
            foreach (var b in new[] { btnExcel, btnPdf }) { b.Margin = new Padding(0, 0, 8, 0); bt.Controls.Add(b); }
            tl.Controls.Add(bt, 3, 3);
            var rpF = new RoundPanel { Dock = DockStyle.Top, Height = 150, Padding = new Padding(14, 8, 2, 10) }; rpF.Controls.Add(tl);

            // --- Cədvəl ---
            foreach (var (k, b, w) in new[] { ("Mektub", "Məktub №", 90), ("MTarix", "Məktubun tarixi", 100), ("Tex", "Hansı texnika üçün", 200), ("Ad", "Adı", 200), ("Kod", "Kodu", 110), ("Vahid", "Ölçü vahidi", 90),
                                               ("Teleb", "Tələb olunan", 100), ("Qebul", "Qəbul edilmiş", 105), ("Serf", "Texnikaya sərf olunub", 150), ("Anbar", "Anbarda qalıq", 110), ("Netice", "Vəziyyət", 160) })
            { int c = grid.Columns.Add(k, b); grid.Columns[c].Width = w; }
            grid.Columns["Ad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            foreach (var k in new[] { "Teleb", "Qebul", "Serf", "Anbar" }) grid.Columns[k].DefaultCellStyle.Alignment = grid.Columns[k].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;   // başlıq rəqəmlərin üstündə
            grid.CellDoubleClick += async (_, e) => { if (e.RowIndex >= 0 && Session.YazaBiler && grid.Rows[e.RowIndex].Tag is long id) await MainForm.Current.SorguAcAsync(id); };
            grid.MouseWheel += (_, e) => grid.ScrollRows(e.Delta);
            var alt = new Panel { Dock = DockStyle.Bottom, Height = 40 };
            alt.Controls.Add(btnMektub); alt.Resize += (_, _) => { btnMektub.Left = alt.Width - btnMektub.Width; btnMektub.Top = 4; };
            var ipucu = new Label { Dock = DockStyle.Bottom, Height = 22, Tag = "muted", Font = new Font("Segoe UI", 8.5f), TextAlign = ContentAlignment.MiddleLeft,
                Text = "Anbarda qalıq = qəbul edilmiş − texnikaya sərf olunan. Anbar zavod üzrə ümumidir. Sətrə iki dəfə klikləyin — məktub \"Sorğu yarat\"da açılır." };
            var rpGrid = new RoundPanel { Dock = DockStyle.Fill, Padding = new Padding(10, 6, 10, 6) };
            rpGrid.Controls.Add(grid); rpGrid.Controls.Add(lblSay); rpGrid.Controls.Add(ipucu); rpGrid.Controls.Add(alt); grid.BringToFront();

            Controls.Add(rpGrid); Controls.Add(new Panel { Dock = DockStyle.Top, Height = 10 }); Controls.Add(rpF); Controls.Add(head);

            void MektubAc()
            {
                var l = cmbMektub.Items.Cast<object>().Select(x => x.ToString()).ToList();
                int i = Ui.SiyahidanSec(FindForm(), "Məktub", "Nömrə, tarix və ya qurum üzrə axtarış", l);
                if (i >= 0) cmbMektub.SelectedIndex = i;
            }
            txtMektub.Click += (_, _) => MektubAc();
            foreach (Control c in txtMektub.Controls) c.Click += (_, _) => MektubAc();   // içindəki mətn xanası
            txtMektub.KeyDown += (_, e) => { if (e.KeyCode is Keys.Enter or Keys.Space or Keys.F4) { MektubAc(); e.Handled = true; } };
            cmbMektub.SelectedIndexChanged += (_, _) => txtMektub.Text = cmbMektub.SelectedItem as string ?? "";
            foreach (var c in new[] { cmbMektub, cmbNetice, cmbTemin }) c.SelectedIndexChanged += (_, _) => { if (_hazir) Goster(); };
            cmbZavod.SelectedIndexChanged += async (_, _) => { if (_hazir) await YukleAsync(); };
            foreach (var d in new[] { dtDan, dtDek }) d.ValueChanged += (_, _) => { if (_hazir) Goster(); };
            txtAxtar.TextChanged += (_, _) => { if (_hazir) { _axtar.Stop(); _axtar.Start(); } };
            _axtar.Tick += (_, _) => { _axtar.Stop(); Goster(); };
            btnExcel.Click += (_, _) => Excel();
            btnPdf.Click += (_, _) => Pdf();
            btnMektub.Click += async (_, _) => await MektubPdfAsync();
            _hazir = true;
        }

        private string Zavod => Session.ZavodFiltri ?? (cmbZavod.SelectedIndex > 0 ? Secimler.Zavodlar[cmbZavod.SelectedIndex - 1] : null);

        public override async Task OnShownAsync()
        {
            cmbZavod.Visible = lblZavod.Visible = Session.ZavodFiltri == null;
            await YukleAsync();
        }

        private async Task YukleAsync()
        {
            string z = Zavod;
            _hamisi = await Task.Run(() => SorguRepo.TeminatAsync(z));
            _mektublar = await SorguRepo.MektublarAsync(z, "");
            _hazir = false;
            string m = cmbMektub.SelectedIndex > 0 ? cmbMektub.SelectedItem as string : null, t = cmbTemin.SelectedIndex > 0 ? cmbTemin.SelectedItem as string : null;
            cmbMektub.Items.Clear(); cmbMektub.Items.Add(Lang.T("Hamısı")); cmbMektub.Items.AddRange(_mektublar.Select(x => (object)x.ad).ToArray());
            cmbMektub.SelectedIndex = Math.Max(0, m == null ? 0 : cmbMektub.Items.IndexOf(m));
            cmbTemin.Items.Clear(); cmbTemin.Items.Add(Lang.T("Hamısı"));
            cmbTemin.Items.AddRange(_hamisi.Select(x => x.TeminEden).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().OrderBy(x => x).Cast<object>().ToArray());
            cmbTemin.SelectedIndex = Math.Max(0, t == null ? 0 : cmbTemin.Items.IndexOf(t));
            _hazir = true;
            Goster();
        }

        private static string R(double d) => d.ToString("#,0.##", CultureInfo.InvariantCulture).Replace(",", " ");

        /// <summary>Filtrlər yaddaşdakı siyahıya tətbiq olunur (bazaya təkrar müraciət yoxdur).</summary>
        private void Goster()
        {
            long? mid = cmbMektub.SelectedIndex > 0 && cmbMektub.SelectedIndex - 1 < _mektublar.Count ? _mektublar[cmbMektub.SelectedIndex - 1].id : null;
            string net = cmbNetice.SelectedIndex > 0 ? Netice[cmbNetice.SelectedIndex - 1] : null;
            string tem = cmbTemin.SelectedIndex > 0 ? cmbTemin.SelectedItem as string : null;
            string dan = dtDan.Checked ? Vaxt.Iso(dtDan.Value.Date) : null, dek = dtDek.Checked ? Vaxt.Iso(dtDek.Value.Date) : null;
            string q = txtAxtar.Text.Trim();
            bool Var(string s) => s != null && s.Contains(q, StringComparison.CurrentCultureIgnoreCase);
            _gorunen = _hamisi.Where(x => (mid == null || x.SorguId == mid) && (net == null || x.Netice == net) && (tem == null || x.TeminEden == tem)
                                         && (dan == null || string.CompareOrdinal(x.MektubTarix ?? "", dan) >= 0) && (dek == null || string.CompareOrdinal(x.MektubTarix ?? "", dek) <= 0)
                                         && (q.Length == 0 || Var(x.Ad) || Var(x.Kod) || Var(x.Texnika)))
                              .OrderByDescending(x => x.MektubTarix).ThenByDescending(x => x.SorguId).ToList();
            grid.Rows.Clear();
            foreach (var x in _gorunen)
            {
                int i = grid.Rows.Add(Setir(x));
                grid.Rows[i].Tag = x.SorguId;
                grid.Rows[i].Cells["Netice"].Style.ForeColor = x.Netice switch { "Tam təmin edilib" => Theme.Ok, "Qismən təmin edilib" => Theme.Warn, _ => Theme.Bad };
            }
            lblSay.Text = $"{Lang.T("Sətir sayı")}: {_gorunen.Count}";
        }

        private string[] Setir(TeminatSetir x) => new[]
        {
            x.MektubNo, Vaxt.Tarix(x.MektubTarix), string.IsNullOrEmpty(x.Texnika) ? "—" : x.Texnika, x.Ad, string.IsNullOrEmpty(x.Kod) ? "—" : x.Kod, x.Vahid,
            R(x.Teleb), R(x.Qebul), R(x.Serf), R(x.Anbar), Lang.T(x.Netice)
        };

        private Hesabat Cedvel() => new("Sorğular üzrə təminat", grid.Columns.Cast<DataGridViewColumn>().Select(c => c.HeaderText).ToArray(), _gorunen.Select(Setir).ToList(), null,
            "Təchizat sorğuları əsasında qəbul edilmiş ehtiyat hissələri və materiallar.");

        private string AltBasliq => (Zavod ?? $"{Secimler.Zavodlar.Length} {Lang.T("zavod üzrə")}") + " • " + string.Format(Lang.T("{0} tarixinə"), DateTime.Today.ToString("dd.MM.yyyy"));

        private void Excel()
        {
            if (_gorunen.Count == 0) { Toast.Show(this, "Siyahı boşdur", ToastKind.Warn); return; }
            using var d = new SaveFileDialog { Filter = "Excel (*.xlsx)|*.xlsx", FileName = $"Sorgular_uzre_teminat_{DateTime.Today:yyyyMMdd}.xlsx" };
            if (d.ShowDialog(this) != DialogResult.OK) return;
            try
            {
                var h = Cedvel(); ExcelService.Hesabat(d.FileName, h, AltBasliq, new[] { h.Qeyd });
                Toast.Show(this, "Excel hesabatı yaradıldı");
            }
            catch (Exception ex) { Toast.Show(this, ex.Message, ToastKind.Error); }
        }

        private void Pdf()
        {
            if (_gorunen.Count == 0) { Toast.Show(this, "Siyahı boşdur", ToastKind.Warn); return; }
            var (h, alt) = (Cedvel(), AltBasliq);
            CapService.Menyu(btnPdf, $"Sorgular_uzre_teminat_{DateTime.Today:yyyyMMdd}.pdf", yol => Task.Run(() => PdfService.Hesabat(yol, h, alt, null)));
        }

        /// <summary>Seçilmiş sətrin məktubu üçün təchizat sorğusu sənədi.</summary>
        private async Task MektubPdfAsync()
        {
            if (grid.CurrentRow?.Tag is not long id || await SorguRepo.GetAsync(id) is not { } s) { Toast.Show(this, Lang.T("Sətir seçin"), ToastKind.Warn); return; }
            CapService.Menyu(btnMektub, $"Sorgu_{(s.V["MektubNo"] ?? "").Replace("/", "-")}.pdf", yol => Task.Run(() => PdfService.Sorgu(yol, s)));
        }
    }
}
