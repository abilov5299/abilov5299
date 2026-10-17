using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Forms;

namespace TexnikaReyestri.Pages
{
    /// <summary>Sorğu əlavə et / redaktə: məktub məlumatları, əlaqəli texnikalar, ehtiyat hissələri. Təminat nəticəsini istifadəçi seçir.</summary>
    public class SorguEditPage : PageBase
    {
        private Sorgu _s = new();
        private readonly Label lblTitle = new() { Text = "Sorğu əlavə et", Tag = "title", AutoSize = true, Font = new Font("Bahnschrift SemiBold", 18f), Location = new Point(0, 4) };
        private readonly FlatButton btnSave = new() { Text = "Yadda saxla", Glyph = Icons.Save, Size = new Size(140, 34) };
        private readonly FlatButton btnCancel = new() { Text = "Ləğv et", Kind = ButtonKind.Secondary, Size = new Size(110, 34) };
        private readonly FlatButton btnDel = new() { Text = "Sil", Glyph = Icons.Delete, Kind = ButtonKind.Danger, Size = new Size(90, 34) };
        private readonly Dictionary<string, Control> _c = new();
        private readonly ModernGrid gridTex = new() { Dock = DockStyle.Fill }, gridHis = new() { Dock = DockStyle.Fill, ReadOnly = false };
        private readonly RoundTextBox txtTex = new() { PlaceholderText = "Zavod nömrəsi, marka və ya ad (boş: siyahı)", Font = new Font("Segoe UI", 10f), Size = new Size(320, 28), Location = new Point(14, 44) };
        private readonly FlatButton btnTexAxtar = new() { Text = "Reyestrdən texnika əlavə et", Glyph = Icons.Add, Kind = ButtonKind.Secondary, Size = new Size(230, 32), Location = new Point(344, 42) };
        private readonly FlatButton btnHisAdd = new() { Text = "Sətir əlavə et", Glyph = Icons.Add, Kind = ButtonKind.Secondary, Size = new Size(150, 32), Location = new Point(14, 42) };
        private DataGridViewComboBoxColumn colTex;

        public bool YeniRejim => _s.Id == 0;

        public SorguEditPage()
        {
            var head = new Panel { Dock = DockStyle.Top, Height = 48 };
            head.Controls.AddRange(new Control[] { lblTitle, btnDel, btnCancel, btnSave });
            head.Resize += (_, _) => { btnSave.Left = head.Width - btnSave.Width; btnCancel.Left = btnSave.Left - btnCancel.Width - 8; btnDel.Left = btnCancel.Left - btnDel.Width - 8; btnSave.Top = btnCancel.Top = btnDel.Top = 4; };
            btnSave.Click += async (_, _) => await SaxlaAsync();
            btnCancel.Click += async (_, _) => await MainForm.Current.AcAsync("tech");
            btnDel.Click += async (_, _) => await SilAsync();

            var scroll = new Panel { Dock = DockStyle.Fill, AutoScroll = true };

            // --- Məktub məlumatları ---
            RoundComboBox Cmb(string[] items) { var c = new RoundComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10f), Dock = DockStyle.Fill, Margin = new Padding(0, 0, 12, 0) }; c.Items.AddRange(items); c.SelectedIndex = 0; return c; }
            RoundTextBox Txt() => new() { Font = new Font("Segoe UI", 10f), Dock = DockStyle.Fill, Margin = new Padding(0, 0, 12, 0) };
            RoundDatePicker Dt(bool bos) => new() { ShowCheckBox = bos, Dock = DockStyle.Fill, Margin = new Padding(0, 0, 12, 0) };
            Label L(string s) => new() { Text = s, AutoSize = false, Dock = DockStyle.Fill, Tag = "muted", Font = new Font("Segoe UI Semibold", 9f), TextAlign = ContentAlignment.BottomLeft, Margin = new Padding(0, 0, 12, 2) };
            (string kod, string ad, Control c)[] sahe =
            {
                ("MektubNo", "Məktub nömrəsi *", Txt()), ("QeydTarixi", "Qeydiyyat tarixi *", Dt(false)), ("Zavod", "Zavod *", Cmb(Secimler.Zavodlar)), ("IcraMuddeti", "Məktubun icra müddəti", Dt(true)),
                ("TeminEden", "Təmin edən qurum", Txt()), ("TeminIcraci", "Təmin edən qurum üzrə icraçı", Txt()), ("ZavodMesul", "Zavod üzrə məsul şəxs", Txt()), ("CavabVez", "Cavab vəziyyəti", Cmb(SorguRepo.CavabVez)),
                ("CavabTarixi", "Cavab tarixi", Dt(true)), ("Netice", "Təminat nəticəsi", Cmb(SorguRepo.Neticeler)), ("Qeyd", "Qeyd", Txt()),
            };
            var tl = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 6, BackColor = Color.Transparent };
            for (int i = 0; i < 4; i++) tl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            for (int i = 0; i < 3; i++) { tl.RowStyles.Add(new RowStyle(SizeType.Absolute, 24)); tl.RowStyles.Add(new RowStyle(SizeType.Absolute, 36)); }
            for (int i = 0; i < sahe.Length; i++)
            {
                var (kod, ad, c) = sahe[i]; _c[kod] = c;
                tl.Controls.Add(L(ad), i % 4, i / 4 * 2); tl.Controls.Add(c, i % 4, i / 4 * 2 + 1);
                if (kod == "Qeyd") tl.SetColumnSpan(c, 2);
            }
            var rp1 = Bolme("Məktub məlumatları", 230); rp1.Controls.Add(tl); tl.BringToFront();

            // --- Əlaqəli texnikalar ---
            gridTex.Columns.Add("Marka", "Marka"); gridTex.Columns.Add("ZavodNo", "Zavod nömrəsi"); gridTex.Columns.Add("Status", "Status");
            gridTex.Columns.Add(new DataGridViewButtonColumn { Name = "Sil", HeaderText = "", Text = "Çıxar", UseColumnTextForButtonValue = true, Width = 90 });
            foreach (var c in new[] { 0, 1, 2 }) gridTex.Columns[c].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gridTex.CellContentClick += (_, e) => { if (e.RowIndex >= 0 && e.ColumnIndex == 3) { _s.Tex.RemoveAt(e.RowIndex); TexGoster(); } };
            btnTexAxtar.Click += async (_, _) => await TexnikaElaveAsync();
            txtTex.KeyDown += async (_, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; await TexnikaElaveAsync(); } };
            var rp2 = Bolme("Əlaqəli texnikalar", 230);
            var g2 = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, 42, 0, 0) }; g2.Controls.Add(gridTex);
            rp2.Controls.Add(g2); rp2.Controls.Add(txtTex); rp2.Controls.Add(btnTexAxtar); g2.BringToFront(); txtTex.BringToFront(); btnTexAxtar.BringToFront();

            // --- Ehtiyat hissələri ---
            gridHis.Columns.Add("Ad", "Ehtiyat hissəsi");
            gridHis.Columns.Add(colTex = new DataGridViewComboBoxColumn { Name = "Tex", HeaderText = "Texnika", FlatStyle = FlatStyle.Flat });
            var colV = new DataGridViewComboBoxColumn { Name = "Vahid", HeaderText = "Ölçü vahidi", FlatStyle = FlatStyle.Flat }; colV.Items.AddRange(SorguRepo.Vahidler); gridHis.Columns.Add(colV);
            gridHis.Columns.Add("Teleb", "Tələb olunan"); gridHis.Columns.Add("Qebul", "Qəbul edilmiş"); gridHis.Columns.Add("Qaliq", "Qalıq");
            gridHis.Columns.Add(new DataGridViewButtonColumn { Name = "Sil", HeaderText = "", Text = "Sil", UseColumnTextForButtonValue = true, Width = 70 });
            for (int c = 0; c < 6; c++) gridHis.Columns[c].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gridHis.Columns["Qaliq"].ReadOnly = true;
            gridHis.EditMode = DataGridViewEditMode.EditOnEnter;
            gridHis.DataError += (_, e) => e.ThrowException = false;
            gridHis.CellValueChanged += (_, e) => { if (e.RowIndex >= 0 && e.ColumnIndex is 3 or 4) Qaliq(gridHis.Rows[e.RowIndex]); };
            gridHis.CellContentClick += (_, e) => { if (e.RowIndex >= 0 && e.ColumnIndex == 6) gridHis.Rows.RemoveAt(e.RowIndex); };
            btnHisAdd.Click += (_, _) => { int i = gridHis.Rows.Add("", null, SorguRepo.Vahidler[0], 1, 0, 1); gridHis.CurrentCell = gridHis.Rows[i].Cells[0]; };
            var rp3 = Bolme("Ehtiyat hissələri", 260);
            var g3 = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, 42, 0, 0) }; g3.Controls.Add(gridHis);
            rp3.Controls.Add(g3); rp3.Controls.Add(btnHisAdd); g3.BringToFront(); btnHisAdd.BringToFront();

            foreach (var rp in new[] { rp3, rp2, rp1 }) { scroll.Controls.Add(rp); scroll.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 10 }); }
            Controls.Add(scroll); Controls.Add(head);
        }

        private static RoundPanel Bolme(string ad, int h)
        {
            var rp = new RoundPanel { Dock = DockStyle.Top, Height = h, Padding = new Padding(14, 8, 14, 10) };
            rp.Controls.Add(new Label { Text = ad, Dock = DockStyle.Top, Height = 30, Font = new Font("Segoe UI Semibold", 11f) });
            return rp;
        }

        private static void Qaliq(DataGridViewRow r)
        {
            double.TryParse(Convert.ToString(r.Cells["Teleb"].Value), out var t); double.TryParse(Convert.ToString(r.Cells["Qebul"].Value), out var q);
            r.Cells["Qaliq"].Value = Math.Max(0, t - q);
        }

        /// <summary>id = 0: yeni sorğu.</summary>
        public async Task YukleAsync(long id)
        {
            _s = id == 0 ? new Sorgu() : await SorguRepo.GetAsync(id) ?? new Sorgu();
            lblTitle.Text = Lang.T(YeniRejim ? "Sorğu əlavə et" : "Sorğunu redaktə et");
            btnDel.Visible = !YeniRejim;
            string V(string k) => _s.V.TryGetValue(k, out var v) ? v : null;
            foreach (var (k, c) in _c)
                switch (c)
                {
                    case RoundTextBox t: t.Text = V(k) ?? ""; break;
                    case RoundComboBox cb: cb.SelectedIndex = Math.Max(0, cb.Items.IndexOf(V(k) ?? (k == "Zavod" ? Session.ZavodFiltri : null) ?? "")); break;
                    case RoundDatePicker d:
                        bool var_ = DateTime.TryParse(V(k), out var dt);
                        d.Value = var_ ? dt : DateTime.Today; if (d.ShowCheckBox) d.Checked = var_; break;
                }
            _c["Zavod"].Enabled = Session.ZavodFiltri == null;
            gridHis.Rows.Clear();
            TexGoster();
            foreach (var h in _s.His) { int i = gridHis.Rows.Add(h.Ad, h.Texnika, h.Vahid, h.Teleb, h.Qebul, 0); Qaliq(gridHis.Rows[i]); }
            txtTex.Text = "";
        }

        public override Task OnShownAsync() => Task.CompletedTask;

        private void TexGoster()
        {
            gridTex.Rows.Clear();
            foreach (var t in _s.Tex) gridTex.Rows.Add(t.Marka, t.ZavodNo, t.Status);
            var ad = _s.Tex.Select(TexAd).ToList();
            foreach (var v in _s.His.Select(h => h.Texnika)) if (v != null && !ad.Contains(v)) ad.Add(v);   // çıxarılmış texnikanın adı sətirdə qalsın   // silinmiş texnikanın adı sətirdə qalsın
            colTex.Items.Clear(); colTex.Items.AddRange(ad.ToArray());
        }
        private static string TexAd(SorguTexnika t) => $"{t.Marka} ({t.ZavodNo})";

        private async Task TexnikaElaveAsync()
        {
            string zavod = Session.ZavodFiltri ?? ((RoundComboBox)_c["Zavod"]).SelectedItem as string;
            var l = await SorguRepo.TexnikaAxtarAsync(txtTex.Text, zavod);
            l.RemoveAll(x => _s.Tex.Any(t => t.Guid == x.Guid));
            if (l.Count == 0) { Toast.Show(this, "Bu zavodda uyğun texnika tapılmadı", ToastKind.Warn); return; }
            void Elave(SorguTexnika t) { _s.Tex.Add(t); TexGoster(); txtTex.Text = ""; }
            if (l.Count == 1 && txtTex.Text.Trim().Length > 0) { Elave(l[0]); return; }
            var m = new ContextMenuStrip();
            foreach (var t in l) m.Items.Add($"{t.Marka} · {t.ZavodNo} · {Secimler.Ekran(t.Status)}", null, (_, _) => Elave(t));
            m.Show(btnTexAxtar, new Point(0, btnTexAxtar.Height));
        }

        private async Task SaxlaAsync()
        {
            gridHis.EndEdit();
            foreach (var (k, c) in _c)
                _s.V[k] = c switch
                {
                    RoundTextBox t => t.Text, RoundComboBox cb => cb.SelectedItem as string,
                    RoundDatePicker d => !d.ShowCheckBox || d.Checked ? Vaxt.Iso(d.Value.Date) : null, _ => null
                };
            if (string.IsNullOrWhiteSpace(_s.V["MektubNo"])) { Toast.Show(this, "Məktub nömrəsini yazın", ToastKind.Warn); _c["MektubNo"].Focus(); return; }
            if (Session.ZavodFiltri != null) _s.V["Zavod"] = Session.ZavodFiltri;
            _s.His = gridHis.Rows.Cast<DataGridViewRow>().Where(r => !string.IsNullOrWhiteSpace(Convert.ToString(r.Cells["Ad"].Value))).Select(r =>
            {
                double.TryParse(Convert.ToString(r.Cells["Teleb"].Value), out var t); double.TryParse(Convert.ToString(r.Cells["Qebul"].Value), out var q);
                return new SorguHisse { Ad = Convert.ToString(r.Cells["Ad"].Value).Trim(), Texnika = r.Cells["Tex"].Value as string, Vahid = r.Cells["Vahid"].Value as string, Teleb = t, Qebul = q };
            }).ToList();
            bool yeni = YeniRejim;
            await SorguRepo.SaxlaAsync(_s);
            await SistemRepo.LogAsync("Sorğu", $"{Session.AdSoyad}: {_s.V["MektubNo"]} nömrəli sorğu {(yeni ? "əlavə edildi" : "dəyişdirildi")}");
            Toast.Show(this, yeni ? "Sorğu əlavə edildi" : "Yadda saxlanıldı");
            await YukleAsync(0);
            await MainForm.Current.AcAsync("tech");
        }

        private async Task SilAsync()
        {
            if (YeniRejim || !Ui.Tesdiq(this, "Sorğunu silmək", $"{_s.V["MektubNo"]} nömrəli sorğu silinsin?", "Sil", true)) return;
            await SorguRepo.SilAsync(_s.Id);
            await SistemRepo.LogAsync("Sorğu", $"{Session.AdSoyad}: {_s.V["MektubNo"]} nömrəli sorğu silindi");
            await YukleAsync(0);
            await MainForm.Current.AcAsync("tech");
        }
    }
}
