using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Services;

namespace TexnikaReyestri.Pages
{
    /// <summary>Hesabatlar: növ, zavod, hesabat tarixi; solda hesabat siyahısı, sağda cədvəl; Excel və PDF; altda dövr.</summary>
    public class HesabatPage : PageBase
    {
        private readonly RoundComboBox cmbNov = new() { DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10f), Dock = DockStyle.Fill, Margin = new Padding(0, 0, 12, 0) };
        private readonly RoundComboBox cmbZavod = new() { DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10f), Dock = DockStyle.Fill, Margin = new Padding(0, 0, 12, 0) };
        private readonly RoundDatePicker dtTarix = new() { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 12, 0) };
        private readonly RoundDatePicker dtBas = new() { Width = 150 }, dtSon = new() { Width = 150 };
        private readonly FlatButton btnGoster = new() { Text = "Hesabatı göstər", Size = new Size(160, 34) };
        private readonly FlatButton btnExcel = new() { Text = "Excel-ə çıxar", Glyph = Icons.Excel, Kind = ButtonKind.Secondary, Size = new Size(150, 34) };
        private readonly FlatButton btnPdf = new() { Text = "Çap et (PDF)", Glyph = Icons.Print, Kind = ButtonKind.Secondary, Size = new Size(150, 34) };
        private readonly FlowLayoutPanel lst = new() { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false, BackColor = Color.Transparent };
        private readonly ModernGrid grid = new() { Dock = DockStyle.Fill };
        private readonly Label lblAd = new() { Dock = DockStyle.Top, Height = 30, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Bahnschrift SemiBold", 14f) };
        private readonly Label lblAlt = new() { Dock = DockStyle.Top, Height = 22, TextAlign = ContentAlignment.MiddleCenter, Tag = "muted" };
        private readonly Label lblQeyd = new() { Dock = DockStyle.Bottom, Height = 22, Tag = "muted", TextAlign = ContentAlignment.MiddleLeft };
        private readonly Label lblXeber = new() { Dock = DockStyle.Bottom, Height = 0, TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(8, 0, 0, 0) };
        // "Ehtiyat hissələri üzrə təminat": iki keçid (ilkin — təmin edilməmiş)
        private readonly FlatButton btnTeminsiz = new() { Text = "Təmin edilməmiş ehtiyat hissələri", Size = new Size(260, 36), Margin = new Padding(0, 0, 6, 0) };
        private readonly FlatButton btnTam = new() { Text = "Tam təmin edilmiş ehtiyat hissələri", Size = new Size(260, 36), Kind = ButtonKind.Ghost, Margin = new Padding(0) };
        private readonly Panel pnlKecid = new() { Dock = DockStyle.Top, Height = 46, Visible = false };
        private bool _tam;
        private Label lblZavod; private Hesabat _h; private List<string> _xeber = new(); private bool _hazir;

        public HesabatPage()
        {
            var title = new Label { Text = "Hesabatlar", Tag = "title", AutoSize = true, Font = new Font("Bahnschrift SemiBold", 18f), Location = new Point(0, 4) };
            var head = new Panel { Dock = DockStyle.Top, Height = 44 }; head.Controls.Add(title);

            cmbNov.Items.AddRange(TexnikaRepo.HesabatAdlari); cmbNov.SelectedIndex = 0;
            cmbZavod.Items.Add("Hamısı"); cmbZavod.Items.AddRange(Secimler.Zavodlar); cmbZavod.SelectedIndex = 0;
            dtTarix.Value = DateTime.Today; dtSon.Value = DateTime.Today; dtBas.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            Label L(string s) => new() { Text = s, AutoSize = false, Dock = DockStyle.Fill, Tag = "muted", Font = new Font("Segoe UI Semibold", 9f), TextAlign = ContentAlignment.BottomLeft, Margin = new Padding(0, 0, 12, 2) };
            var tl = new TableLayoutPanel { Dock = DockStyle.Top, Height = 60, ColumnCount = 3, RowCount = 2, BackColor = Color.Transparent };
            tl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45)); tl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35)); tl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            tl.RowStyles.Add(new RowStyle(SizeType.Absolute, 22)); tl.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
            tl.Controls.Add(L("Hesabat növü:"), 0, 0); tl.Controls.Add(lblZavod = L("Zavod:"), 1, 0); tl.Controls.Add(L("Hesabat tarixi:"), 2, 0);
            tl.Controls.Add(cmbNov, 0, 1); tl.Controls.Add(cmbZavod, 1, 1); tl.Controls.Add(dtTarix, 2, 1);
            var bt = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 46, Padding = new Padding(0, 8, 0, 0), BackColor = Color.Transparent };
            foreach (var b in new[] { btnGoster, btnExcel, btnPdf }) { b.Margin = new Padding(0, 0, 10, 0); bt.Controls.Add(b); }
            var rpF = new RoundPanel { Dock = DockStyle.Top, Height = 130, Padding = new Padding(14, 8, 14, 8) };
            rpF.Controls.Add(bt); rpF.Controls.Add(tl);

            // Sol: hesabat siyahısı
            for (int i = 0; i < TexnikaRepo.HesabatNovleri.Length; i++)
            {
                int k = i;
                var b = new FlatButton { Text = TexnikaRepo.HesabatNovleri[i], Kind = ButtonKind.Ghost, Size = new Size(220, 40), TextAlign = ContentAlignment.MiddleLeft, Margin = new Padding(0, 0, 0, 4) };
                b.Click += async (_, _) => { _tam = false; cmbNov.SelectedIndex = k; await GosterAsync(); };
                lst.Controls.Add(b);
            }
            var rpL = new RoundPanel { Dock = DockStyle.Left, Width = 250, Padding = new Padding(12) }; rpL.Controls.Add(lst);

            // Sağ: hesabat
            grid.MouseWheel += (_, e) => grid.ScrollRows(e.Delta);
            var rpR = new RoundPanel { Dock = DockStyle.Fill, Padding = new Padding(16, 10, 16, 10) };
            var fk = new FlowLayoutPanel { AutoSize = true, WrapContents = false, BackColor = Color.Transparent, Top = 6 };
            fk.Controls.Add(btnTeminsiz); fk.Controls.Add(btnTam); pnlKecid.Controls.Add(fk);
            pnlKecid.Resize += (_, _) => fk.Left = Math.Max(0, (pnlKecid.Width - fk.Width) / 2);
            btnTeminsiz.Click += async (_, _) => { _tam = false; await GosterAsync(); };
            btnTam.Click += async (_, _) => { _tam = true; await GosterAsync(); };
            rpR.Controls.Add(grid); rpR.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 8 }); rpR.Controls.Add(pnlKecid); rpR.Controls.Add(lblAlt); rpR.Controls.Add(lblAd);
            rpR.Controls.Add(lblQeyd); rpR.Controls.Add(lblXeber); grid.BringToFront();
            var orta = new Panel { Dock = DockStyle.Fill };
            orta.Controls.Add(rpR); orta.Controls.Add(new Panel { Dock = DockStyle.Left, Width = 12 }); orta.Controls.Add(rpL);

            // Alt: dövr
            var rpD = new RoundPanel { Dock = DockStyle.Bottom, Height = 64, Padding = new Padding(14, 8, 14, 8) };
            var fd = new FlowLayoutPanel { Dock = DockStyle.Fill, BackColor = Color.Transparent, WrapContents = false };
            Label LL(string s, string tag = null) => new() { Text = s, AutoSize = true, Tag = tag, Margin = new Padding(0, 12, 8, 0), Font = new Font("Segoe UI Semibold", 9.5f) };
            fd.Controls.Add(LL("Dövr üzrə hesabatlar")); fd.Controls.Add(new Label { Width = 20 });
            fd.Controls.Add(LL("Başlanğıc tarixi:")); dtBas.Margin = new Padding(0, 6, 16, 0); fd.Controls.Add(dtBas);
            fd.Controls.Add(LL("Son tarix:")); dtSon.Margin = new Padding(0, 6, 16, 0); fd.Controls.Add(dtSon);
            fd.Controls.Add(LL("Qəbul, tamamlanma, təhvil və məktublar üzrə tarix aralığı seçilir.", "muted"));
            rpD.Controls.Add(fd);

            Controls.Add(orta); Controls.Add(new Panel { Dock = DockStyle.Bottom, Height = 10 }); Controls.Add(rpD);
            Controls.Add(new Panel { Dock = DockStyle.Top, Height = 10 }); Controls.Add(rpF); Controls.Add(head);

            btnGoster.Click += async (_, _) => await GosterAsync();
            btnExcel.Click += (_, _) => Excel();
            btnPdf.Click += (_, _) => Pdf();
            cmbNov.SelectedIndexChanged += async (_, _) => { if (_hazir) await GosterAsync(); };
            cmbZavod.SelectedIndexChanged += async (_, _) => { if (_hazir) await GosterAsync(); };
            foreach (var d in new[] { dtTarix, dtBas, dtSon }) d.ValueChanged += async (_, _) => { if (_hazir) await GosterAsync(); };
            _hazir = true;
        }

        public override async Task OnShownAsync()
        {
            cmbZavod.Visible = lblZavod.Visible = Session.ZavodFiltri == null;
            await KartPaneli.YaxinGunAsync();   // icra müddəti hədləri
            await GosterAsync();
        }

        private string Zavod => Session.ZavodFiltri ?? (cmbZavod.SelectedIndex > 0 ? Secimler.Zavodlar[cmbZavod.SelectedIndex - 1] : null);

        private string AltBasliq => (Zavod ?? $"{Secimler.Zavodlar.Length} {Lang.T("zavod üzrə")}") + " • " + string.Format(Lang.T("{0} tarixinə"), dtTarix.Value.ToString("dd.MM.yyyy"));

        private async Task GosterAsync()
        {
            int nov = Math.Max(0, cmbNov.SelectedIndex);
            for (int i = 0; i < lst.Controls.Count; i++) ((FlatButton)lst.Controls[i]).Kind = i == nov ? ButtonKind.Secondary : ButtonKind.Ghost;
            DateTime d1 = dtBas.Value.Date, d2 = dtSon.Value.Date; if (d2 < d1) (d1, d2) = (d2, d1);
            string z = Zavod; DateTime t = dtTarix.Value.Date;
            pnlKecid.Visible = nov == 4;
            btnTeminsiz.Kind = _tam ? ButtonKind.Ghost : ButtonKind.Primary; btnTam.Kind = _tam ? ButtonKind.Primary : ButtonKind.Ghost;
            bool tam = _tam;
            _h = await Task.Run(() => TexnikaRepo.HesabatAsync(nov, z, t, d1, d2, tam));
            _xeber = await TexnikaRepo.KohneMelumatAsync(z);
            lblAd.Text = Lang.T(_h.Ad).ToUpper(); lblAlt.Text = AltBasliq; lblQeyd.Text = Lang.T(_h.Qeyd);
            lblXeber.Text = _xeber.Count == 0 ? "" : "⚠  " + string.Join("   ", _xeber);
            lblXeber.Height = _xeber.Count == 0 ? 0 : 30; lblXeber.BackColor = Theme.Mix(Theme.Warn, Theme.Card, 0.82f); lblXeber.ForeColor = Theme.Ink;
            grid.Rows.Clear(); grid.Columns.Clear();
            for (int i = 0; i < _h.Sutun.Length; i++)
            {
                int c = grid.Columns.Add("c" + i, Lang.T(_h.Sutun[i]));
                grid.Columns[c].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; grid.Columns[c].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i > 0) grid.Columns[c].DefaultCellStyle.Alignment = grid.Columns[c].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            grid.Columns[0].FillWeight = 160;
            if (nov == 4) grid.Columns[1].FillWeight = grid.Columns[2].FillWeight = 170;   // texnika və hissənin adı uzun olur
            foreach (var r in _h.Setir) grid.Rows.Add(r.Select(x => (object)Lang.T(x)).ToArray());
            if (_h.Cem != null) { int i = grid.Rows.Add(_h.Cem.Select(x => (object)Lang.T(x)).ToArray()); grid.Rows[i].DefaultCellStyle.Font = new Font(grid.Font, FontStyle.Bold); }
        }

        /// <summary>Çap və Excel üçün: ehtiyat hissələri hesabatında hansı siyahı olduğu başlıqda yazılır.</summary>
        private Hesabat Cap => cmbNov.SelectedIndex == 4 ? _h with { Ad = $"{Lang.T(_h.Ad)} — {Lang.T(_tam ? "Tam təmin edilmiş ehtiyat hissələri" : "Təmin edilməmiş ehtiyat hissələri")}" } : _h;

        private string FaylAdi(string ext) => $"Hesabat_{TexnikaRepo.HesabatNovleri[cmbNov.SelectedIndex].Replace(" ", "_").Replace(",", "")}{(cmbNov.SelectedIndex == 4 ? (_tam ? "_tam" : "_temin_edilmemis") : "")}_{dtTarix.Value:yyyyMMdd}.{ext}";

        private void Excel()
        {
            if (_h == null) return;
            using var d = new SaveFileDialog { Filter = "Excel (*.xlsx)|*.xlsx", FileName = FaylAdi("xlsx") };
            if (d.ShowDialog(this) != DialogResult.OK) return;
            try
            {
                using var wb = new ClosedXML.Excel.XLWorkbook(); var ws = wb.AddWorksheet("Hesabat");
                int n = _h.Sutun.Length;
                ws.Cell(1, 1).Value = "TƏMİR VƏ TƏMİNAT ÜZRƏ HESABAT"; ws.Range(1, 1, 1, n).Merge().Style.Font.SetBold().Font.SetFontSize(14).Alignment.SetHorizontal(ClosedXML.Excel.XLAlignmentHorizontalValues.Center);
                ws.Cell(2, 1).Value = Cap.Ad; ws.Range(2, 1, 2, n).Merge().Style.Font.SetBold().Alignment.SetHorizontal(ClosedXML.Excel.XLAlignmentHorizontalValues.Center);
                ws.Cell(3, 1).Value = AltBasliq; ws.Range(3, 1, 3, n).Merge().Style.Alignment.SetHorizontal(ClosedXML.Excel.XLAlignmentHorizontalValues.Center);
                int y = 5;
                void Yaz(string[] v, bool bold) { for (int i = 0; i < v.Length; i++) { var c = ws.Cell(y, i + 1); if (i > 0 && double.TryParse(v[i].Replace(" ", ""), out var x)) c.Value = x; else c.Value = v[i]; c.Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin; c.Style.Font.Bold = bold; } y++; }
                Yaz(_h.Sutun, true); foreach (var r in _h.Setir) Yaz(r, false); if (_h.Cem != null) Yaz(_h.Cem, true);
                y++; foreach (var q in new[] { _h.Qeyd }.Concat(_xeber)) ws.Cell(y++, 1).Value = q;
                ws.Columns().AdjustToContents(); wb.SaveAs(d.FileName);
                Toast.Show(this, "Excel hesabatı yaradıldı");
            }
            catch (Exception ex) { Toast.Show(this, ex.Message, ToastKind.Error); }
        }

        private void Pdf()
        {
            if (_h == null) return;
            using var d = new SaveFileDialog { Filter = "PDF (*.pdf)|*.pdf", FileName = FaylAdi("pdf") };
            if (d.ShowDialog(this) != DialogResult.OK) return;
            try
            {
                PdfService.Hesabat(d.FileName, Cap, AltBasliq, _xeber);
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(d.FileName) { UseShellExecute = true });   // çap üçün PDF oxuyucuda açılır
            }
            catch (Exception ex) { Toast.Show(this, ex.Message, ToastKind.Error); }
        }
    }
}
