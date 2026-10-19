using System.Data;
using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Forms;
using TexnikaReyestri.Services;

namespace TexnikaReyestri.Pages
{
    /// <summary>Təchizat sorğuları: 5 kart, filtr, sorğular cədvəli, seçilmiş sorğunun ehtiyat hissələri.</summary>
    public class TechizatPage : PageBase
    {
        private readonly Label lblTitle = new() { Text = "Təchizat sorğuları", Tag = "title", AutoSize = true, Font = new Font("Bahnschrift SemiBold", 18f), Location = new Point(0, 4) };
        private readonly FlatButton btnAdd = new() { Text = "Sorğu əlavə et", Glyph = Icons.Add, Size = new Size(160, 34), Anchor = AnchorStyles.Top | AnchorStyles.Right };
        private readonly FlatButton btnExcel = new() { Text = "Excel hesabatı", Glyph = Icons.Excel, Kind = ButtonKind.Secondary, Size = new Size(150, 34), Anchor = AnchorStyles.Top | AnchorStyles.Right };
        private readonly StatCard[] _k = new StatCard[5];
        private RoundComboBox cmbZavod, cmbNetice, cmbIcra, cmbTarix; private RoundDatePicker dtDan, dtDek; private RoundTextBox txtMektub; private Label lblZavod;
        private readonly ModernGrid grid = new() { Dock = DockStyle.Fill }, gridHis = new() { Dock = DockStyle.Fill };
        private readonly Label lblDet = new() { AutoSize = true, Font = new Font("Segoe UI Semibold", 11f), Location = new Point(4, 8) };
        private readonly Label lblMesul = new() { Dock = DockStyle.Bottom, Height = 24, Tag = "muted", TextAlign = ContentAlignment.MiddleLeft };
        private readonly FlatButton btnQebul = new() { Text = "Ehtiyat hissələrinin qəbulunu qeyd et", Size = new Size(270, 32), Anchor = AnchorStyles.Top | AnchorStyles.Right };
        private readonly FlatButton btnPdf = new() { Text = "PDF sənəd", Glyph = Icons.Print, Kind = ButtonKind.Secondary, Size = new Size(130, 32), Anchor = AnchorStyles.Top | AnchorStyles.Right };
        private readonly FlatButton btnTex = new() { Text = "Sorğuya texnika əlavə et", Kind = ButtonKind.Secondary, Size = new Size(200, 32), Anchor = AnchorStyles.Top | AnchorStyles.Right };
        private readonly System.Windows.Forms.Timer _axtar = new() { Interval = 300 };
        private DataTable _dt; private bool _hazir;

        private static readonly string[] IcraKod = { null, "davam", "cavab", "late" };
        private static readonly string[] TarixKod = { "QeydTarixi", "IcraMuddeti", "CavabTarixi" };

        public TechizatPage()
        {
            // --- Başlıq ---
            var head = new Panel { Dock = DockStyle.Top, Height = 44 };
            head.Controls.AddRange(new Control[] { lblTitle, btnExcel, btnAdd });
            head.Resize += (_, _) => { btnExcel.Left = head.Width - btnExcel.Width; btnAdd.Left = btnExcel.Left - btnAdd.Width - 8; btnAdd.Top = btnExcel.Top = 4; };
            btnAdd.Click += async (_, _) => await MainForm.Current.SorguAcAsync(0);
            btnExcel.Click += (_, _) => Excel();

            // --- Kartlar ---
            var kart = new TableLayoutPanel { Dock = DockStyle.Top, Height = 96, ColumnCount = 5, Padding = new Padding(0, 4, 0, 8) };
            (string ad, string g, AccentKind a, string netice, string icra)[] t =
            {
                ("Cəmi sorğu", Icons.List, AccentKind.Nav, null, null), ("İcrası davam edən", Icons.Repair, AccentKind.Info, null, "davam"),
                ("Qismən təmin edilmiş", Icons.Clock, AccentKind.Warn, "Qismən təmin edilib", null), ("Tam təmin edilmiş", Icons.Check, AccentKind.Ok, "Tam təmin edilib", null),
                ("Təmin edilməmiş", Icons.Cancel, AccentKind.Bad, "Təmin edilməyib", null)
            };
            for (int i = 0; i < 5; i++)
            {
                kart.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f)); var x = t[i];
                var c = _k[i] = new StatCard { Title = x.ad, Glyph = x.g, Accent = x.a, Value = "0", Dock = DockStyle.Fill, Margin = new Padding(0, 0, i < 4 ? 10 : 0, 0) };
                c.Click += async (_, _) => { _hazir = false; cmbNetice.SelectedIndex = x.netice == null ? 0 : Array.IndexOf(SorguRepo.Neticeler, x.netice) + 1; cmbIcra.SelectedIndex = Math.Max(0, Array.IndexOf(IcraKod, x.icra)); _hazir = true; await YukleAsync(); };
                kart.Controls.Add(c, i, 0);
            }

            // --- Filtr ---
            RoundComboBox Cmb(params string[] items) { var c = new RoundComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10f), Dock = DockStyle.Fill, Margin = new Padding(0, 0, 12, 0) }; c.Items.AddRange(items); c.SelectedIndex = 0; c.SelectedIndexChanged += filtr_Changed; return c; }
            RoundDatePicker Dt() { var d = new RoundDatePicker { ShowCheckBox = true, Checked = false, Dock = DockStyle.Fill, Margin = new Padding(0, 0, 12, 0) }; d.ValueChanged += filtr_Changed; return d; }
            Label L(string s) => new() { Text = s, AutoSize = false, Dock = DockStyle.Fill, Tag = "muted", Font = new Font("Segoe UI Semibold", 9f), TextAlign = ContentAlignment.BottomLeft, Margin = new Padding(0, 0, 12, 2) };
            cmbZavod = Cmb(new[] { "Hamısı" }.Concat(Secimler.Zavodlar).ToArray());
            cmbNetice = Cmb(new[] { "Hamısı" }.Concat(SorguRepo.Neticeler).ToArray());
            cmbIcra = Cmb("Hamısı", "İcrası davam edir", "Cavab verilib", "Məktubun icra müddəti keçib");
            cmbTarix = Cmb("Məktubun qeydiyyat tarixi", "Məktubun icra müddəti", "Cavab tarixi");
            dtDan = Dt(); dtDek = Dt();
            txtMektub = new RoundTextBox { PlaceholderText = "Məktubun nömrəsi", Font = new Font("Segoe UI", 10f), Dock = DockStyle.Fill, Margin = new Padding(0, 0, 12, 0) };
            txtMektub.TextChanged += (_, _) => { if (_hazir) { _axtar.Stop(); _axtar.Start(); } };
            _axtar.Tick += async (_, _) => { _axtar.Stop(); await YukleAsync(); };
            var tl = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 4, BackColor = Color.Transparent };
            for (int i = 0; i < 4; i++) tl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            foreach (var h in new[] { 22, 36, 26, 36 }) tl.RowStyles.Add(new RowStyle(SizeType.Absolute, h));
            tl.Controls.Add(lblZavod = L("Zavod:"), 0, 0); tl.Controls.Add(L("Təminat nəticəsi:"), 1, 0); tl.Controls.Add(L("Məktubun icra vəziyyəti:"), 2, 0); tl.Controls.Add(L("Məktub nömrəsi:"), 3, 0);
            tl.Controls.Add(cmbZavod, 0, 1); tl.Controls.Add(cmbNetice, 1, 1); tl.Controls.Add(cmbIcra, 2, 1); tl.Controls.Add(txtMektub, 3, 1);
            tl.Controls.Add(L("Tarix növü:"), 0, 2); tl.Controls.Add(L("Başlanğıc:"), 1, 2); tl.Controls.Add(L("Son:"), 2, 2);
            tl.Controls.Add(cmbTarix, 0, 3); tl.Controls.Add(dtDan, 1, 3); tl.Controls.Add(dtDek, 2, 3);
            var rpF = new RoundPanel { Dock = DockStyle.Top, Height = 150, Padding = new Padding(14, 8, 2, 10) }; rpF.Controls.Add(tl);

            // --- Sorğular cədvəli ---
            grid.Columns.Add("Mektub", "Məktub / qeydiyyat tarixi"); grid.Columns.Add("Zavod", "Zavod / əlaqəli texnikalar");
            grid.Columns.Add("Temin", "Təmin edən qurum / icraçı"); grid.Columns.Add("Icra", "Məktubun icra müddəti");
            grid.Columns.Add("Cavab", "Cavab vəziyyəti"); grid.Columns.Add("Status", "Təminat nəticəsi");
            foreach (var (c, w) in new[] { (0, 170), (1, 320), (2, 220), (3, 190), (4, 200), (5, 180) }) grid.Columns[c].Width = w;
            grid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grid.SelectionChanged += (_, _) => Detal();
            grid.CellDoubleClick += async (_, e) => { if (e.RowIndex >= 0) await MainForm.Current.SorguAcAsync((long)grid.Rows[e.RowIndex].Tag); };
            grid.MouseWheel += (_, e) => grid.ScrollRows(e.Delta);
            var rpGrid = new RoundPanel { Dock = DockStyle.Fill, Padding = new Padding(6) }; rpGrid.Controls.Add(grid);

            // --- Seçilmiş sorğunun ehtiyat hissələri ---
            gridHis.Columns.Add("Ad", "Ehtiyat hissəsi"); gridHis.Columns.Add("Tex", "Texnika"); gridHis.Columns.Add("Vahid", "Ölçü vahidi");
            gridHis.Columns.Add("Teleb", "Tələb olunan"); gridHis.Columns.Add("Qebul", "Qəbul edilmiş"); gridHis.Columns.Add("Qaliq", "Qalıq");
            foreach (DataGridViewColumn c in gridHis.Columns) c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            var dh = new Panel { Dock = DockStyle.Top, Height = 42 };
            dh.Controls.AddRange(new Control[] { lblDet, btnPdf, btnTex, btnQebul });
            dh.Resize += (_, _) => { btnQebul.Left = dh.Width - btnQebul.Width - 4; btnTex.Left = btnQebul.Left - btnTex.Width - 8; btnPdf.Left = (Session.YazaBiler ? btnTex.Left : dh.Width + 4) - btnPdf.Width - 8; btnQebul.Top = btnTex.Top = btnPdf.Top = 4; };
            btnQebul.Click += async (_, _) => { if (SecilmisId is long id) await MainForm.Current.SorguAcAsync(id); };
            btnPdf.Click += async (_, _) => await PdfAsync();
            btnTex.Click += async (_, _) => { if (SecilmisId is long id) await MainForm.Current.SorguAcAsync(id); };
            var rpDet = new RoundPanel { Dock = DockStyle.Bottom, Height = 250, Padding = new Padding(14, 8, 14, 8) };
            rpDet.Controls.Add(gridHis); rpDet.Controls.Add(dh); rpDet.Controls.Add(lblMesul);
            var ara = new Panel { Dock = DockStyle.Bottom, Height = 10 };

            Controls.Add(rpGrid); Controls.Add(ara); Controls.Add(rpDet); Controls.Add(new Panel { Dock = DockStyle.Top, Height = 8 }); Controls.Add(rpF); Controls.Add(kart); Controls.Add(head);
            _hazir = true;
        }

        private long? SecilmisId => grid.CurrentRow?.Tag as long?;

        public override async Task OnShownAsync()
        {
            cmbZavod.Visible = lblZavod.Visible = Session.ZavodFiltri == null;
            btnAdd.Visible = btnQebul.Visible = btnTex.Visible = Session.YazaBiler;
            await YukleAsync();
        }

        private async void filtr_Changed(object sender, EventArgs e) { if (_hazir) await YukleAsync(); }

        private SorguFiltr Filtr() => new()
        {
            Zavod = Session.ZavodFiltri ?? (cmbZavod.SelectedIndex > 0 ? Secimler.Zavodlar[cmbZavod.SelectedIndex - 1] : null),
            Netice = cmbNetice.SelectedIndex > 0 ? SorguRepo.Neticeler[cmbNetice.SelectedIndex - 1] : null,
            Icra = IcraKod[Math.Max(0, cmbIcra.SelectedIndex)], Mektub = txtMektub.Text,
            TarixSahe = TarixKod[Math.Max(0, cmbTarix.SelectedIndex)],
            TarixDan = dtDan.Checked ? Vaxt.Iso(dtDan.Value.Date) : null, TarixDek = dtDek.Checked ? Vaxt.Iso(dtDek.Value.Date) : null,
        };

        private async Task YukleAsync()
        {
            var f = Filtr();
            long? sec = SecilmisId;
            _dt = await Task.Run(() => SorguRepo.SiyahiAsync(f));
            var st = await SorguRepo.StatAsync(f.Zavod);
            long[] v = { st.Cemi, st.Davam, st.Qismen, st.Tam, st.Edilmeyib };
            for (int i = 0; i < 5; i++) _k[i].Value = v[i].ToString("#,0").Replace(",", " ");
            string bugun = Vaxt.Iso(DateTime.Today);
            grid.Rows.Clear();
            foreach (DataRow r in _dt.Rows)
            {
                string S(string k) => r[k] as string;
                var tex = SorguRepo.TexnikaJson(r["Texnikalar"]);
                bool davam = (S("CavabVez") ?? "İcrası davam edir") == "İcrası davam edir";
                string icra = Vaxt.Tarix(S("IcraMuddeti"));
                if (davam && S("IcraMuddeti") is string im && DateTime.TryParse(im, out var d))
                { int g = (d - DateTime.Today).Days; icra += g >= 0 ? $" · {g} {Lang.T("gün qalıb")}" : $" · {-g} {Lang.T("gün keçib")}"; }
                string cavab = davam ? Lang.T("İcrası davam edir") : Lang.T("Cavab verilib") + (S("CavabTarixi") is string ct ? " · " + Vaxt.Tarix(ct) : "");
                int i = grid.Rows.Add($"{S("MektubNo")} · {Vaxt.Tarix(S("QeydTarixi"))}",
                    Lang.T(S("Zavod")) + (tex.Count > 0 ? " · " + string.Join(", ", tex.Select(x => x.Marka)) : ""),
                    string.Join(" · ", new[] { S("TeminEden"), S("TeminIcraci") }.Where(x => !string.IsNullOrEmpty(x))),
                    icra, cavab, S("Netice") ?? "Təyin edilməyib");
                grid.Rows[i].Tag = Convert.ToInt64(r["Id"]);
                if (davam && string.CompareOrdinal(S("IcraMuddeti") ?? "9", bugun) < 0) grid.Rows[i].Cells[3].Style.ForeColor = Theme.Bad;
                if (sec == (long)grid.Rows[i].Tag) grid.CurrentCell = grid.Rows[i].Cells[0];
            }
            Detal();
        }

        private async void Detal()
        {
            gridHis.Rows.Clear();
            if (SecilmisId is not long id) { lblDet.Text = Lang.T("Sorğu seçilməyib"); lblMesul.Text = ""; btnQebul.Enabled = btnTex.Enabled = btnPdf.Enabled = false; return; }
            btnQebul.Enabled = btnTex.Enabled = btnPdf.Enabled = true;
            var s = await SorguRepo.GetAsync(id); if (s == null || SecilmisId != id) return;
            lblDet.Text = $"{s.V["MektubNo"]} {Lang.T("nömrəli məktub üzrə ehtiyat hissələri")}";
            foreach (var h in s.His) gridHis.Rows.Add(h.Ad, h.Texnika, h.Vahid, h.Teleb, h.Qebul, Math.Max(0, h.Teleb - h.Qebul));
            lblMesul.Text = $"{Lang.T("Məsul şəxslər")}: {Lang.T("Zavod üzrə")} — {s.V["ZavodMesul"] ?? "—"}   •   {Lang.T("Təmin edən qurum üzrə")} — {s.V["TeminIcraci"] ?? "—"}";
        }

        private async Task PdfAsync()
        {
            if (SecilmisId is not long id || await SorguRepo.GetAsync(id) is not { } s) return;
            using var d = new SaveFileDialog { Filter = "PDF (*.pdf)|*.pdf", FileName = $"Sorgu_{(s.V["MektubNo"] ?? "").Replace("/", "-")}.pdf" };
            if (d.ShowDialog(this) != DialogResult.OK) return;
            try { PdfService.Sorgu(d.FileName, s); System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(d.FileName) { UseShellExecute = true }); }
            catch (Exception ex) { Toast.Show(this, ex.Message, ToastKind.Error); }
        }

        private void Excel()
        {
            if (grid.Rows.Count == 0) { Toast.Show(this, "Siyahı boşdur", ToastKind.Warn); return; }
            using var d = new SaveFileDialog { Filter = "Excel (*.xlsx)|*.xlsx", FileName = $"Techizat_sorgulari_{DateTime.Today:yyyyMMdd}.xlsx" };
            if (d.ShowDialog(this) != DialogResult.OK) return;
            try
            {
                using var wb = new ClosedXML.Excel.XLWorkbook(); var ws = wb.AddWorksheet("Sorğular");
                for (int c = 0; c < grid.Columns.Count; c++) ws.Cell(1, c + 1).Value = grid.Columns[c].HeaderText;
                for (int r = 0; r < grid.Rows.Count; r++) for (int c = 0; c < grid.Columns.Count; c++) ws.Cell(r + 2, c + 1).Value = Convert.ToString(grid.Rows[r].Cells[c].Value);
                ws.Row(1).Style.Font.Bold = true; ws.Columns().AdjustToContents(); wb.SaveAs(d.FileName);
                Toast.Show(this, "Excel hesabatı yaradıldı");
            }
            catch (Exception ex) { Toast.Show(this, ex.Message, ToastKind.Error); }
        }
    }
}
