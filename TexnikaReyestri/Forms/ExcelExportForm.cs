using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Services;

namespace TexnikaReyestri.Forms
{
    public partial class ExcelExportForm : Form
    {
        private readonly ReyestrFiltr _f; private readonly long _say; private readonly long? _secilmis;
        public ExcelExportForm() { InitializeComponent(); }
        public ExcelExportForm(ReyestrFiltr f, long say, long? secilmis) : this() { _f = f; _say = say; _secilmis = secilmis; }

        private async void ExcelExportForm_Load(object sender, EventArgs e)
        {
            Ui.Hazirla(this); BackColor = Theme.Line;
            long hamisi = await TexnikaRepo.SayAsync(new ReyestrFiltr { Zavod = Session.ZavodFiltri });
            radFiltered.Text = $"{Lang.T("Ekrandakı filtrlənmiş siyahı")} ({_say:#,0})"; radAll.Text = $"{Lang.T("Bütün reyestr")} ({hamisi:#,0})";
            radSelected.Enabled = _secilmis != null; if (_say == 0) { radAll.Checked = true; radFiltered.Enabled = false; }
            lblSize.Text = $"{Lang.T("Təxmini ölçü")}: {Math.Max(0.1, Math.Max(_say, hamisi) * 0.6 / 1024):0.0} MB";
        }

        private async void btnOk_Click(object sender, EventArgs e)
        {
            var s = new ExcelService.IxracSecim
            {
                Filtr = radAll.Checked ? new ReyestrFiltr { Zavod = Session.ZavodFiltri } : _f ?? new ReyestrFiltr(),
                YalnizIdler = radSelected.Checked && _secilmis is long id ? new[] { id } : null, BesSutun = radFive.Checked,
                QrupBasliqlari = chkGroups.Checked && !radFive.Checked, StatusReng = chkStatus.Checked, GecikenQirmizi = chkLate.Checked, DondurVeFiltr = chkFreeze.Checked, IcmalVereqi = chkSummary.Checked
            };
            using var d = new SaveFileDialog { Filter = "Excel|*.xlsx", FileName = $"Reyestr_{(Session.ZavodFiltri ?? "hamisi").Replace(" ", "")}_{DateTime.Now:dd-MM-yyyy}.xlsx" };
            if (d.ShowDialog(this) != DialogResult.OK) return;
            int n = 0;
            if (await Ui.Is(this, "Excel faylı hazırlanır...", async ov => n = await ExcelService.IxracAsync(d.FileName, s, new Progress<int>(p => ov.Report(p)), default)))
            {
                await SistemRepo.LogAsync("İxrac", $"Excel: {Path.GetFileName(d.FileName)} ({n} sətir)");
                if (chkOpen.Checked) try { System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(d.FileName) { UseShellExecute = true }); } catch { }
                Toast.Show(Owner ?? this, $"{Path.GetFileName(d.FileName)} {Lang.T("saxlanıldı")} ({n} {Lang.T("sətir")})");
                DialogResult = DialogResult.OK;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;
    }
}
