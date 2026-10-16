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
        private EvStat _st; private Dictionary<string, KartStat> _ks; private int _n;

        public HomePage()
        {
            InitializeComponent();
            lblLegC = new Label { AutoSize = true, Font = lblLegB.Font, Tag = "muted", Text = "■ Təhvil verilib", Top = lblLegB.Top };
            lblLegA.Text = "■ Qəbul edilib"; lblLegB.Text = "■ Təmir tamamlanıb"; rpMonthly.Controls.Add(lblLegC);
            Ui.Saga(lnkLogs); Ui.Saga(lnkLate); Ui.Saga(lblLegC); Ui.Saga(lblLegB, lblLegC); Ui.Saga(lblLegA, lblLegB);   // sağ kənara bərkidilmiş başlıq yazıları
            Duzum();
            gridLate.RowTemplate.Height = 30; gridEvents.RowTemplate.Height = 30;
            gridLate.Columns["colLateDays"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridZav.RowTemplate.Height = 22;
            foreach (DataGridViewColumn c in gridZav.Columns) if (c.Index > 0) { c.DefaultCellStyle.Alignment = c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight; }
        }

        public override async Task OnShownAsync()
        {
            lblTitle.Text = Lang.T("Ana səhifə") + " · " + (Session.ZavodFiltri ?? Lang.T("bütün zavodlar"));
            lblLegA.ForeColor = Theme.Chart1; lblLegB.ForeColor = Theme.Chart2; lblLegC.ForeColor = Theme.Chart3;
            await Ui.Is(this, "Məlumatlar yüklənir...", async _ =>
            {
                _st = await Task.Run(() => TexnikaRepo.EvStatAsync(Session.ZavodFiltri, Aylar[segPeriod.SelectedIndex]));
                _n = await KartPaneli.YaxinGunAsync();
                _ks = await Task.Run(() => TexnikaRepo.KartStatAsync(Session.ZavodFiltri, _n));
                Goster();
                if (Session.ZavodFiltri != null) await ZolaqlarAsync();
            });
            Lang.Apply(this);
        }

        private Label lblLegC, lblTehvilInfo, lblSyncSon, lblSyncDey;

        /// <summary>
        /// Zavod: diaqram (tam en) + "Təhvil gözlənilən texnikalar" və "Məlumat mübadiləsi" zolaqları.
        /// Xidmət/İdarə: "Qəbul və təhvil" diaqramı + "Zavodlar üzrə icra vəziyyəti".
        /// </summary>
        private void Duzum()
        {
            tlpBody.SuspendLayout();
            foreach (var c in new Control[] { rpStatus, rpTypes, rpLate, rpEvents, rpMonthly, rpAvg }) tlpBody.Controls.Remove(c);
            lblFoot.Visible = false;
            tlpBody.ColumnStyles.Clear(); tlpBody.RowStyles.Clear();
            rpMonthly.Margin = rpAvg.Margin = new Padding(0, 0, 0, 10);
            if (Session.ZavodFiltri != null)
            {
                lblMonthlyTitle.Text = "Aylar üzrə qəbul, təmir və təhvil";
                tlpBody.ColumnCount = 1; tlpBody.RowCount = 3; tlpBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                tlpBody.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); tlpBody.RowStyles.Add(new RowStyle(SizeType.Absolute, 70)); tlpBody.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
                tlpBody.Controls.Add(rpMonthly, 0, 0);
                tlpBody.Controls.Add(Zolaq("Təhvil gözlənilən texnikalar", "Siyahını aç →", async () => await MainForm.Current.ReyestrAcAsync(new ReyestrFiltr { Zavod = Session.ZavodFiltri, Qrup = "gozl" }), out lblTehvilInfo, out _), 0, 1);
                tlpBody.Controls.Add(Zolaq("Məlumat mübadiləsi", "Məlumatları köçürməyə →", async () => await MainForm.Current.AcAsync("sync"), out lblSyncSon, out lblSyncDey), 0, 2);
            }
            else
            {
                lblMonthlyTitle.Text = "Qəbul və təhvil";
                tlpBody.ColumnCount = 2; tlpBody.RowCount = 1;
                tlpBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58)); tlpBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42));
                tlpBody.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                rpMonthly.Margin = new Padding(0, 0, 10, 10);
                tlpBody.Controls.Add(rpMonthly, 0, 0); tlpBody.Controls.Add(rpAvg, 1, 0);
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
            t.Controls.Add(new Label { Text = basliq, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, Font = new Font("Bahnschrift SemiBold", 12f) }, 0, 0);
            info = new Label { Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, Font = new Font("Segoe UI", 9.5f) }; t.Controls.Add(info, 1, 0);
            info2 = new Label { Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, Font = new Font("Segoe UI", 9.5f) }; t.Controls.Add(info2, 2, 0);
            var b = new FlatButton { Text = duyme, Size = new Size(220, 34), Anchor = AnchorStyles.Right };
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
            chartMonthly.SetData(s.Aylar, s.Qebul, s.Tehvil, s.Verilib);
            brkStatus.SetItems(s.Statuslar);
            barTypes.SetItems(s.Novler);
            // Zavodlar üzrə icra vəziyyəti: sətir = zavod, sonda cəmi
            gridZav.Rows.Clear();
            string F(long v) => v.ToString("#,0").Replace(",", " ");
            foreach (var z in Secimler.Zavodlar.Where(z => Session.ZavodFiltri == null || z == Session.ZavodFiltri))
            {
                var k = _ks.GetValueOrDefault(z);
                int i = gridZav.Rows.Add(Lang.T(z), F(k.Davam), F(k.Gozlenilir), F(k.Tehvil), F(k.Cemi)); gridZav.Rows[i].Tag = z;
            }
            if (Session.ZavodFiltri == null)
            {
                var c = KartStat.Cem(_ks.Values);
                int i = gridZav.Rows.Add(Lang.T("Cəmi"), F(c.Davam), F(c.Gozlenilir), F(c.Tehvil), F(c.Cemi));
                gridZav.Rows[i].DefaultCellStyle.Font = new Font(gridZav.Font, FontStyle.Bold);
            }
            gridLate.Rows.Clear();
            foreach (var g in s.Gecikenler) { int i = gridLate.Rows.Add(g.ad + (Session.ZavodFiltri == null ? $" · {g.zavod}" : ""), $"{g.gun} gün"); gridLate.Rows[i].Tag = g.id; gridLate.Rows[i].Cells[1].Style.ForeColor = Theme.Bad; }
            if (s.Gecikenler.Length == 0) gridLate.Rows.Add(Lang.T("Gecikən təmir yoxdur."), "");
        }

        private async void segPeriod_SelectedIndexChanged(object sender, EventArgs e) => await OnShownAsync();

        private async void kart_Clicked(object sender, string qrup) => await MainForm.Current.ReyestrAcAsync(new ReyestrFiltr { Zavod = Session.ZavodFiltri, Qrup = qrup });

        private async void brkStatus_StatusClicked(object sender, string status) => await MainForm.Current.ReyestrAcAsync(new ReyestrFiltr { Zavod = Session.ZavodFiltri, Status = status });

        private async void gridZav_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && gridZav.Rows[e.RowIndex].Tag is string z) await MainForm.Current.ReyestrAcAsync(new ReyestrFiltr { Zavod = z, Qrup = "cemi" });
        }

        private async void lnkLate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => await MainForm.Current.ReyestrAcAsync(new ReyestrFiltr { Zavod = Session.ZavodFiltri, Qrup = "late" });
        private async void lnkLogs_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => await MainForm.Current.AcAsync("log");

        private async void gridLate_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && gridLate.Rows[e.RowIndex].Tag is long id) await MainForm.Current.DetalAcAsync(id);
        }
    }
}
