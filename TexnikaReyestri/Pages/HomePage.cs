using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Forms;
using TexnikaReyestri.Services;

namespace TexnikaReyestri.Pages
{
    /// <summary>Ana səhifə: 6 statistika kartı, qəbul/təmir/təhvil diaqramı; üst qurumda zavodlar üzrə icra vəziyyəti, zavodda təhvil və mübadilə zolaqları.</summary>
    public partial class HomePage : PageBase
    {
        private static readonly int[] Aylar = { 1, 3, 6, 12 };
        private readonly Dictionary<string, long> _paket = new();   // zavod → son idxal edilmiş paketin vaxtı
        private static readonly string[] _ayAd = { "Yanvar", "Fevral", "Mart", "Aprel", "May", "İyun", "İyul", "Avqust", "Sentyabr", "Oktyabr", "Noyabr", "Dekabr" };
        private EvStat _st; private Dictionary<string, KartStat> _ks; private int _n;

        public HomePage()
        {
            InitializeComponent();
            lblLegC = new Label { Name = "lblLegC", AutoSize = true, Font = lblLegB.Font, Tag = "muted", Text = "■ Təhvil verilib", Top = lblLegB.Top };
            lblLegA.Text = "■ Qəbul edilib"; lblLegB.Text = "■ Təmir tamamlanıb"; rpMonthly.Controls.Add(lblLegC);
            Ui.Saga(lblLegC); Ui.Saga(lblLegB, lblLegC); Ui.Saga(lblLegA, lblLegB);   // sağ kənara bərkidilmiş başlıq yazıları
            foreach (var l in new[] { lblLegA, lblLegB, lblLegC }) l.Visible = false;   // əfsanə diaqramın içində çəkilir
            chartMonthly.Genis = true;
            lblMonthlyTitle.Font = lblAvgTitle.Font = new Font("Bahnschrift SemiBold", 15f); lblMonthlyTitle.Height = lblAvgTitle.Height = 40; lblMonthlyTitle.Tag = lblAvgTitle.Tag = null;
            pnlHead.Controls.Remove(segPeriod); rpMonthly.Controls.Add(segPeriod); segPeriod.BringToFront(); segPeriod.Top = 12; Ui.Saga(segPeriod);
            gridZav.Font = new Font("Segoe UI", 12f); gridZav.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 11f); gridZav.ColumnHeadersHeight = 58; gridZav.RowTemplate.Height = 40;
            foreach (DataGridViewColumn c in gridZav.Columns) if (c.Index > 0) c.Width = (int)(c.Width * 1.25);
            gridZav.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            gridZav.Columns.Add(new DataGridViewTextBoxColumn { Name = "colZavGec", HeaderText = "İcra müddəti keçib", Width = 130 });
            gridZav.Columns.Add(new DataGridViewTextBoxColumn { Name = "colZavSon", HeaderText = "Son məlumat tarixi", Width = 150 });
            Duzum();
            gridZav.RowTemplate.Height = 40;
            foreach (DataGridViewColumn c in gridZav.Columns) if (c.Index > 0) { c.DefaultCellStyle.Alignment = c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter; }
        }

        public override async Task OnShownAsync()
        {
            lblTitle.Text = Lang.T("Ana səhifə") + " · " + (Session.ZavodFiltri ?? Lang.T("bütün zavodlar"));
            lblLegA.ForeColor = Theme.Chart1; lblLegB.ForeColor = Theme.Chart2; lblLegC.ForeColor = Theme.Chart3; lblLegC.Text = Lang.T("■ Təhvil verilib"); lblLegC.BringToFront();
            await Ui.Is(this, "Məlumatlar yüklənir...", async _ =>
            {
                _st = await Task.Run(() => TexnikaRepo.EvStatAsync(Session.ZavodFiltri, Aylar[segPeriod.SelectedIndex]));
                _n = await KartPaneli.YaxinGunAsync();
                _ks = await Task.Run(() => TexnikaRepo.KartStatAsync(Session.ZavodFiltri, _n));
                _paket.Clear();
                if (Session.ZavodFiltri == null)
                    foreach (System.Data.DataRow r in (await TexnikaRepo.ZavodIcmalAsync()).Rows) if (r["SonPaket"] is long t) _paket[(string)r["Zavod"]] = t;
                chartMonthly.Note = $"{Lang.T(_ayAd[DateTime.Today.Month - 1])}: {string.Format(Lang.T("{0}-dək"), DateTime.Today.ToString("dd.MM.yyyy"))}";
                Goster();
                if (Session.ZavodFiltri != null) await ZolaqlarAsync();
            });
            Lang.Apply(this);
        }

        private Label lblXeber, lblLegC, lblTehvilInfo, lblSyncSon, lblSyncDey;

        /// <summary>
        /// Zavod: diaqram (tam en) + "Təhvil gözlənilən texnikalar" və "Məlumat mübadiləsi" zolaqları.
        /// Xidmət/İdarə: "Qəbul və təhvil" diaqramı + "Zavodlar üzrə icra vəziyyəti".
        /// </summary>
        private void Duzum()
        {
            tlpBody.SuspendLayout();
            foreach (var c in new Control[] { rpMonthly, rpAvg }) tlpBody.Controls.Remove(c);
            tlpBody.ColumnStyles.Clear(); tlpBody.RowStyles.Clear();
            rpMonthly.Margin = rpAvg.Margin = new Padding(0, 0, 0, 10);
            if (Session.ZavodFiltri != null)
            {
                lblMonthlyTitle.Text = "Aylar üzrə qəbul, təmir və təhvil";
                tlpBody.ColumnCount = 1; tlpBody.RowCount = 3; tlpBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                tlpBody.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); tlpBody.RowStyles.Add(new RowStyle(SizeType.Absolute, 70)); tlpBody.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
                tlpBody.Controls.Add(rpMonthly, 0, 0); tlpBody.SetColumnSpan(rpMonthly, 1);
                tlpBody.Controls.Add(Zolaq("Təhvil gözlənilən texnikalar", "Siyahını aç →", async () => await MainForm.Current.ReyestrAcAsync(new ReyestrFiltr { Zavod = Session.ZavodFiltri, Qrup = "gozl" }), out lblTehvilInfo, out _), 0, 1);
                tlpBody.Controls.Add(Zolaq("Məlumat mübadiləsi", "Məlumatları köçürməyə →", async () => await MainForm.Current.AcAsync("sync"), out lblSyncSon, out lblSyncDey), 0, 2);
            }
            else
            {
                lblMonthlyTitle.Text = "Aylar üzrə qəbul, təmir və təhvil";
                tlpBody.ColumnCount = 2; tlpBody.RowCount = 1;
                tlpBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52)); tlpBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48));
                tlpBody.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                rpAvg.Margin = new Padding(0, 0, 12, 10);
                tlpBody.Controls.Add(rpAvg, 0, 0); tlpBody.Controls.Add(rpMonthly, 1, 0);
                lblXeber = new Label { Dock = DockStyle.Bottom, Height = 0, TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(10, 0, 0, 0), Font = new Font("Segoe UI", 10f) };
                rpAvg.Controls.Add(lblXeber);
                tlpBody.SetColumnSpan(rpMonthly, 1); tlpBody.SetRowSpan(rpMonthly, 1);   // Designer-dəki 2 sütunluq en qalmasın
            }
            tlpBody.ResumeLayout(true);
        }

        /// <summary>Başlıq + məlumat yazıları + sağda düymə olan üfüqi zolaq.</summary>
        private static RoundPanel Zolaq(string basliq, string duyme, Func<Task> klik, out Label info, out Label info2)
        {
            var rp = new RoundPanel { Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, 10), Padding = new Padding(16, 6, 16, 6) };
            var t = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 1, BackColor = Color.Transparent };
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300)); t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50)); t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50)); t.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 240));
            t.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            t.Controls.Add(new Label { Text = basliq, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, Font = new Font("Bahnschrift SemiBold", 15f) }, 0, 0);
            info = new Label { Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, Font = new Font("Segoe UI", 11f) }; t.Controls.Add(info, 1, 0);
            info2 = new Label { Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, Font = new Font("Segoe UI", 11f) }; t.Controls.Add(info2, 2, 0);
            var b = new FlatButton { Text = duyme, Size = new Size(230, 40), Anchor = AnchorStyles.Right, Font = new Font("Segoe UI Semibold", 10f) };
            b.Click += async (_, _) => await klik(); t.Controls.Add(b, 3, 0);
            rp.Controls.Add(t); return rp;
        }

        private async Task ZolaqlarAsync()
        {
            var (say, gun) = await TexnikaRepo.TehvilGozlAsync(Session.ZavodFiltri);
            lblTehvilInfo.Text = $"{say} {Lang.T("texnika")} • {Lang.T("Ən uzun gözləmə")}: {gun} {Lang.T("gün")}";
            var (t, _, dey) = await SyncService.IxracHazirlikAsync();
            lblSyncSon.Text = Lang.T("Son köçürmə faylı hazırlanıb") + "\n" + (t == 0 ? "—" : DateTimeOffset.FromUnixTimeMilliseconds(t).LocalDateTime.ToString("dd.MM.yyyy HH:mm"));
            lblSyncDey.Text = Lang.T("Son ixracdan sonra dəyişiklik") + "\n" + (dey > 0 ? $"{Lang.T("Var")} • {dey} {Lang.T("qeyd")}" : Lang.T("Yoxdur"));
            lblSyncDey.ForeColor = dey > 0 ? Theme.Warn : Theme.Ok;
        }

        private void Goster()
        {
            var s = _st;
            tlpTiles.Goster(KartStat.Cem(_ks.Values), _n);
            chartMonthly.SeriesBName = "təmir tamamlanıb";
            chartMonthly.SetData(s.Aylar, s.Qebul, s.Tehvil, s.Verilib, s.TemirAy);
            // Zavodlar üzrə icra vəziyyəti: sətir = zavod, sonda cəmi
            gridZav.Rows.Clear();
            string F(long v) => v.ToString("#,0").Replace(",", " ");
            foreach (var z in Secimler.Zavodlar.Where(z => Session.ZavodFiltri == null || z == Session.ZavodFiltri))
            {
                var k = _ks.GetValueOrDefault(z);
                string son = "—"; bool kohne = false;
                if (_paket.TryGetValue(z, out var t)) { var d = DateTimeOffset.FromUnixTimeMilliseconds(t).LocalDateTime; son = d.ToString("dd.MM.yyyy"); kohne = (DateTime.Now - d).TotalDays >= 7; }
                else kohne = Session.ZavodFiltri == null;
                int i = gridZav.Rows.Add(Lang.T(z), F(k.Davam), F(k.Gozlenilir), F(k.Tehvil), F(k.Cemi), F(k.Kecmis), son); gridZav.Rows[i].Tag = z;
                var gc = gridZav.Rows[i].Cells["colZavGec"]; gc.Style.ForeColor = Theme.C("#6A3FC8"); gc.Style.Font = new Font(gridZav.Font, FontStyle.Bold | FontStyle.Underline);
                if (kohne) { var sc = gridZav.Rows[i].Cells["colZavSon"]; sc.Style.BackColor = sc.Style.SelectionBackColor = Theme.Mix(Theme.Warn, Theme.Card, 0.75f); sc.Style.ForeColor = sc.Style.SelectionForeColor = Theme.Bad; }
            }
            var kz = Secimler.Zavodlar.Where(z => !_paket.TryGetValue(z, out var t) || (DateTime.Now - DateTimeOffset.FromUnixTimeMilliseconds(t).LocalDateTime).TotalDays >= 7).ToList();
            if (lblXeber != null)
            {
                lblXeber.Text = kz.Count == 0 ? "" : string.Join(", ", kz.Select(Lang.T)) + " " + Lang.T("üzrə məlumatın yenilənməsi gözlənilir.");
                lblXeber.Height = kz.Count == 0 ? 0 : 36; lblXeber.BackColor = Theme.Mix(Theme.Warn, Theme.Card, 0.82f); lblXeber.ForeColor = Theme.Ink;
            }
            if (Session.ZavodFiltri == null)
            {
                var c = KartStat.Cem(_ks.Values);
                int i = gridZav.Rows.Add(Lang.T("Cəmi"), F(c.Davam), F(c.Gozlenilir), F(c.Tehvil), F(c.Cemi), F(c.Kecmis), "—");
                gridZav.Rows[i].DefaultCellStyle.Font = new Font(gridZav.Font, FontStyle.Bold);
            }
        }

        private async void segPeriod_SelectedIndexChanged(object sender, EventArgs e) => await OnShownAsync();

        private async void kart_Clicked(object sender, string qrup) => await MainForm.Current.ReyestrAcAsync(new ReyestrFiltr { Zavod = Session.ZavodFiltri, Qrup = qrup });

        private async void gridZav_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string z = gridZav.Rows[e.RowIndex].Tag as string;   // Cəmi sətrində null = bütün zavodlar
            if (gridZav.Columns[e.ColumnIndex].Name == "colZavGec") await MainForm.Current.ReyestrAcAsync(new ReyestrFiltr { Zavod = z, Qrup = "late" });
            else if (z != null) await MainForm.Current.ReyestrAcAsync(new ReyestrFiltr { Zavod = z, Qrup = "cemi" });
        }
    }
}
