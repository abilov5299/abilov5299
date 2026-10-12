using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Forms;

namespace TexnikaReyestri.Pages
{
    /// <summary>Ana səhifə: statistika kartları, qəbul/təhvil diaqramı, status bölgüsü, növlər, orta müddət, gecikənlər, son hadisələr.</summary>
    public partial class HomePage : PageBase
    {
        private static readonly int[] Aylar = { 1, 3, 6, 12 };
        private EvStat _st;

        public HomePage()
        {
            InitializeComponent();
            gridLate.RowTemplate.Height = 30; gridEvents.RowTemplate.Height = 30;
            gridLate.Columns["colLateDays"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public override async Task OnShownAsync()
        {
            lblTitle.Text = Lang.T("Ana səhifə") + " · " + (Session.ZavodFiltri ?? Lang.T("bütün zavodlar"));
            lblAvgTitle.Text = Session.ZavodFiltri != null ? "Təmir növü üzrə orta müddət (gün)" : "Zavodlar üzrə orta təmir müddəti (gün)";
            lblLegA.ForeColor = Theme.Chart1; lblLegB.ForeColor = Theme.Chart2;
            await Ui.Is(this, "Məlumatlar yüklənir...", async _ =>
            {
                _st = await Task.Run(() => TexnikaRepo.EvStatAsync(Session.ZavodFiltri, Aylar[segPeriod.SelectedIndex]));
                var loglar = await SistemRepo.LoglarAsync(DateTime.Today.AddDays(-30), DateTime.Today, null, 3);
                Goster(loglar);
            });
            Lang.Apply(this);
        }

        private void Goster(System.Data.DataTable loglar)
        {
            var s = _st;
            cardTotal.Value = s.Cemi.ToString("#,0").Replace(",", " ");
            cardRepair.Value = s.Temirde.ToString("#,0").Replace(",", " "); cardRepair.SubText = $"+ {s.HisseGozleyir} hissə gözləyir";
            cardLate.Value = s.Geciken.ToString("#,0").Replace(",", " ");
            // Gecikən = aktiv statuslardan (Qəbul edilib + Təmirdə + Hissə gözləyir); faiz də eyni əsasdan
            long aktiv = s.Statuslar.Where(x => Secimler.AktivStatus.Contains(x.Item1)).Sum(x => x.Item2);
            cardLate.SubText = aktiv > 0 ? $"{Math.Round(s.Geciken * 100.0 / aktiv)}% aktiv təmirlərin" : "";
            cardAvg.Value = $"{s.OrtaGun} gün";
            double d = s.OrtaGunEvvel > 0 ? s.OrtaGun - s.OrtaGunEvvel : 0;
            cardAvg.SubText = d == 0 ? "" : $"{(d < 0 ? "▼" : "▲")} {Math.Abs(d)} {Lang.T("gün əvvəlki dövrə görə")}";
            cardAvg.SubColor = d < 0 ? Theme.Ok : Theme.Bad;
            chartMonthly.SetData(s.Aylar, s.Qebul, s.Tehvil);
            brkStatus.SetItems(s.Statuslar);
            barTypes.SetItems(s.Novler);
            barAvg.SetItems(s.OrtaUzre);
            gridLate.Rows.Clear();
            foreach (var g in s.Gecikenler) { int i = gridLate.Rows.Add(g.ad + (Session.ZavodFiltri == null ? $" · {g.zavod}" : ""), $"{g.gun} gün"); gridLate.Rows[i].Tag = g.id; gridLate.Rows[i].Cells[1].Style.ForeColor = Theme.Bad; }
            if (s.Gecikenler.Length == 0) gridLate.Rows.Add(Lang.T("Gecikən təmir yoxdur."), "");
            gridEvents.Rows.Clear();
            foreach (System.Data.DataRow r in loglar.Rows) gridEvents.Rows.Add(Vaxt.Yaz((long)r["Tarix"]), r["Nov"], r["Mesaj"]);
        }

        private async void segPeriod_SelectedIndexChanged(object sender, EventArgs e) => await OnShownAsync();

        private async void card_Click(object sender, EventArgs e)
        {
            var c = (StatCard)sender; var f = new ReyestrFiltr { Zavod = Session.ZavodFiltri };
            if (c.FilterKey == "late") f.Gecikenler = true; else if (c.FilterKey.Length > 0) f.Status = c.FilterKey;
            await MainForm.Current.ReyestrAcAsync(f);
        }

        private async void brkStatus_StatusClicked(object sender, string status) => await MainForm.Current.ReyestrAcAsync(new ReyestrFiltr { Zavod = Session.ZavodFiltri, Status = status });

        private async void barAvg_ItemClicked(object sender, string ad)
        {
            if (Session.ZavodFiltri == null && Secimler.Zavodlar.Contains(ad)) await MainForm.Current.ReyestrAcAsync(new ReyestrFiltr { Zavod = ad });
        }

        private async void lnkLate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => await MainForm.Current.ReyestrAcAsync(new ReyestrFiltr { Zavod = Session.ZavodFiltri, Gecikenler = true });
        private async void lnkLogs_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => await MainForm.Current.AcAsync("log");

        private async void gridLate_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && gridLate.Rows[e.RowIndex].Tag is long id) await MainForm.Current.DetalAcAsync(id);
        }
    }
}
