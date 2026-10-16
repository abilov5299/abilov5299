using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Forms;

namespace TexnikaReyestri.Pages
{
    /// <summary>Ana səhifə: 6 statistika kartı, qəbul/təhvil diaqramı, status bölgüsü, növlər, zavodlar üzrə icra vəziyyəti, gecikənlər, son hadisələr.</summary>
    public partial class HomePage : PageBase
    {
        private static readonly int[] Aylar = { 1, 3, 6, 12 };
        private EvStat _st; private Dictionary<string, KartStat> _ks; private int _n;

        public HomePage()
        {
            InitializeComponent();
            Ui.Saga(lnkLogs); Ui.Saga(lnkLate); Ui.Saga(lblLegB); Ui.Saga(lblLegA, lblLegB);   // sağ kənara bərkidilmiş başlıq yazıları
            gridLate.RowTemplate.Height = 30; gridEvents.RowTemplate.Height = 30;
            gridLate.Columns["colLateDays"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridZav.RowTemplate.Height = 22;
            foreach (DataGridViewColumn c in gridZav.Columns) if (c.Index > 0) { c.DefaultCellStyle.Alignment = c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight; }
        }

        public override async Task OnShownAsync()
        {
            lblTitle.Text = Lang.T("Ana səhifə") + " · " + (Session.ZavodFiltri ?? Lang.T("bütün zavodlar"));
            lblLegA.ForeColor = Theme.Chart1; lblLegB.ForeColor = Theme.Chart2;
            await Ui.Is(this, "Məlumatlar yüklənir...", async _ =>
            {
                _st = await Task.Run(() => TexnikaRepo.EvStatAsync(Session.ZavodFiltri, Aylar[segPeriod.SelectedIndex]));
                _n = await KartPaneli.YaxinGunAsync();
                _ks = await Task.Run(() => TexnikaRepo.KartStatAsync(Session.ZavodFiltri, _n));
                var loglar = await SistemRepo.LoglarAsync(DateTime.Today.AddDays(-30), DateTime.Today, null, 3);
                Goster(loglar);
            });
            Lang.Apply(this);
        }

        private void Goster(System.Data.DataTable loglar)
        {
            var s = _st;
            tlpTiles.Goster(KartStat.Cem(_ks.Values), _n);
            chartMonthly.SetData(s.Aylar, s.Qebul, s.Tehvil);
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
            gridEvents.Rows.Clear();
            foreach (System.Data.DataRow r in loglar.Rows) gridEvents.Rows.Add(Vaxt.Yaz((long)r["Tarix"]), r["Nov"], r["Mesaj"]);
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
