using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Forms;
using TexnikaReyestri.Services;

namespace TexnikaReyestri.Pages
{
    /// <summary>Zavodda: USB-yə ixrac. Üst qurumlarda: USB-dən idxal (və öz üst qurumuna ixrac).</summary>
    public partial class SyncPage : PageBase
    {
        public SyncPage() { InitializeComponent(); gridHist.RowTemplate.Height = 32; }

        private bool Ixrac => Session.Tip != QurumTip.EnUstQurum;   // ən üst qurumun göndərəcəyi yer yoxdur
        private bool Idxal => Session.Tip != QurumTip.Zavod;

        // Üst qurum həm idxal, həm ixrac edir: solda İxrac, sağda yuxarıda İdxal, altında Proses; aşağıda tarixçə
        private bool _ikili;
        private void IkiliDuzulus()
        {
            if (_ikili) return; _ikili = true;
            using var _ = Ui.Dondur(this);
            tlp.SuspendLayout();
            tlp.Controls.Remove(rpProc); pnlLeft.Controls.Remove(rpImport);
            var sag = new Panel { Dock = DockStyle.Fill, Margin = rpProc.Margin, BackColor = Color.Transparent };
            rpProc.Margin = Padding.Empty; rpProc.Dock = DockStyle.Fill;
            rpImport.Visible = true; rpImport.Dock = DockStyle.Top; rpImport.Height = 214;
            lstPackages.Height = 110; btnPick.Top = btnImport.Top = lstPackages.Bottom + 10;
            var ara = new Panel { Dock = DockStyle.Top, Height = 10, BackColor = Color.Transparent };
            sag.Controls.Add(rpProc); sag.Controls.Add(ara); sag.Controls.Add(rpImport);   // sonuncu əlavə olunan ən yuxarıda
            tlp.Controls.Add(sag, 1, 0);
            rpExport.Dock = DockStyle.Fill;
            tlp.RowStyles[0] = new RowStyle(SizeType.Absolute, 420); tlp.RowStyles[1] = new RowStyle(SizeType.Percent, 100);
            tlp.ResumeLayout(true);
        }

        public override async Task OnShownAsync()
        {
            lblTitle.Text = Lang.T(Ixrac && Idxal ? "İxrac / İdxal" : Ixrac ? "İxrac (USB-yə)" : "İdxal (USB-dən)");
            rpExport.Visible = Ixrac; rpImport.Visible = Idxal && !Ixrac;
            if (Ixrac && Idxal) IkiliDuzulus();
            Diskler();
            var (t, seq) = await SyncService.SonIxracAsync();
            int say = (await TexnikaRepo.DeltaAsync(t)).Rows.Count;
            vPkg.Text = $"TQX-{Session.QurumKod}-{seq + 1:000000}"; vCnt.Text = say.ToString("#,0").Replace(",", " ");
            var q = (await SistemRepo.QurumlarAsync()).FirstOrDefault(x => x.Kod == Session.QurumKod);
            vRec.Text = q?.UstKod == null ? "—" : Lang.T((await SistemRepo.QurumlarAsync()).First(x => x.Kod == q.UstKod).Ad);
            vLast.Text = t == 0 ? Lang.T("hələ olmayıb") : Vaxt.Yaz(t);
            double gun = t == 0 ? 999 : (DateTime.Now - DateTimeOffset.FromUnixTimeMilliseconds(t).LocalDateTime).TotalDays;
            lblWarn.Visible = Ixrac && gun >= 3;
            lblWarn.Text = "⚠ " + (t == 0 ? Lang.T("Hələ heç vaxt ixrac edilməyib.") : $"{Lang.T("Son ixrac")} {(int)gun} {Lang.T("gün əvvəl olub")} ({Vaxt.Yaz(t)}). {Lang.T("Üst qurum bu müddətin dəyişikliklərini görmür.")}");
            steps.Items = Ixrac && !Idxal ? SyncService.IxracAddimlari : SyncService.IdxalAddimlari; steps.Current = -1; lblResult.Text = "";
            Paketler();
            await TarixceAsync();
        }

        private void Diskler()
        {
            cmbDrive.Items.Clear();
            foreach (var d in DriveInfo.GetDrives().Where(d => d.IsReady && d.DriveType == DriveType.Removable))
                cmbDrive.Items.Add($"{d.Name} ({(string.IsNullOrEmpty(d.VolumeLabel) ? "USB" : d.VolumeLabel)}) · {d.AvailableFreeSpace / 1_073_741_824.0:0.0} GB");
            cmbDrive.Items.Add(Lang.T("Başqa qovluq seç..."));
            cmbDrive.SelectedIndex = 0;
            btnExport.Enabled = true;
        }

        private string SecilmisQovluq()
        {
            string s = cmbDrive.SelectedItem as string ?? "";
            if (s.Length > 2 && s[1] == ':') return Path.Combine(s[..3], "TexnikaReyestri");
            using var d = new FolderBrowserDialog { Description = Lang.T("Paketin yazılacağı qovluq") };
            return d.ShowDialog(this) == DialogResult.OK ? d.SelectedPath : null;
        }

        private void Paketler()
        {
            lstPackages.Items.Clear();
            foreach (var d in DriveInfo.GetDrives().Where(d => d.IsReady && d.DriveType == DriveType.Removable))
                try { foreach (var f in Directory.EnumerateFiles(d.RootDirectory.FullName, "*.tqx", SearchOption.AllDirectories).Take(200)) lstPackages.Items.Add(f); } catch { }
            btnImport.Enabled = lstPackages.SelectedIndex >= 0;
        }

        private async Task TarixceAsync()
        {
            var dt = await SyncService.JurnalAsync(); gridHist.Rows.Clear();
            foreach (System.Data.DataRow r in dt.Rows)
                gridHist.Rows.Add(Vaxt.Yaz((long)r["Tarix"]), Lang.T((string)r["Emeliyyat"]), r["Fayl"], r["Yeni"], r["Deyisen"], r["Silinen"], r["Netice"], r["Emeliyyat"] as string == "İxrac" ? "" : r["Qeyd"]);
        }

        private void btnDrives_Click(object sender, EventArgs e) { Diskler(); Paketler(); }
        private void lstPackages_SelectedIndexChanged(object sender, EventArgs e) => btnImport.Enabled = lstPackages.SelectedIndex >= 0;

        private void btnPick_Click(object sender, EventArgs e)
        {
            using var d = new OpenFileDialog { Filter = "Texnika Reyestri paketi|*.tqx" };
            if (d.ShowDialog(this) != DialogResult.OK) return;
            lstPackages.Items.Insert(0, d.FileName); lstPackages.SelectedIndex = 0;
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            string q = SecilmisQovluq(); if (q == null) return;
            steps.Items = SyncService.IxracAddimlari; btnExport.Enabled = false; lblResult.Text = "";
            var p = new Progress<int>(i => steps.Current = i);
            try
            {
                var n = await Task.Run(() => SyncService.IxracAsync(q, p));
                steps.Current = steps.Items.Length;
                lblResult.ForeColor = Theme.Ok; lblResult.Text = $"✓ {Path.GetFileName(n.Fayl)} {Lang.T("hazırdır")}: {n.Yeni} {Lang.T("qeyd")}. {Lang.T("USB-ni üst quruma çatdırın.")}";
                await SistemRepo.LogAsync("İxrac", $"Paket {Path.GetFileName(n.Fayl)} yazıldı ({n.Yeni} qeyd)");
                Toast.Show(this, "İxrac tamamlandı");
                await OnShownAsync(); lblResult.Text = $"✓ {Path.GetFileName(n.Fayl)}: {n.Yeni} {Lang.T("qeyd")}";
            }
            catch (Exception ex) { steps.Fail(); lblResult.ForeColor = Theme.Bad; lblResult.Text = "✕ " + ex.Message; await SistemRepo.LogAsync("Xəta", "İxrac: " + ex.Message); }
            finally { btnExport.Enabled = true; }
        }

        private async void btnImport_Click(object sender, EventArgs e)
        {
            if (lstPackages.SelectedItem is not string f) return;
            steps.Items = SyncService.IdxalAddimlari; btnImport.Enabled = false; lblResult.Text = "";
            var p = new Progress<int>(i => steps.Current = i);
            try
            {
                await BackupService.AlAsync(false);   // idxaldan əvvəl ehtiyat nüsxə
                var n = await Task.Run(() => SyncService.IdxalAsync(f, p));
                steps.Current = steps.Items.Length;
                lblResult.ForeColor = Theme.Ok; lblResult.Text = $"✓ {n.Yeni} {Lang.T("yeni")}, {n.Deyisen} {Lang.T("dəyişiklik")}, {n.Silinen} {Lang.T("silinmə")}";
                await SistemRepo.LogAsync("İdxal", $"{Path.GetFileName(f)}: {n.Yeni} yeni, {n.Deyisen} dəyişiklik, {n.Silinen} silinmə");
                Toast.Show(this, "İdxal tamamlandı"); await MainForm.Current.BadgeYenileAsync(); await TarixceAsync();
            }
            catch (Exception ex)
            {
                steps.Fail(); lblResult.ForeColor = Theme.Bad; lblResult.Text = "✕ " + ex.Message;
                await SyncService.RedJurnalAsync(f, ex.Message); await SistemRepo.LogAsync("Xəta", $"{Path.GetFileName(f)} rədd edildi: {ex.Message}"); await TarixceAsync();
            }
            finally { btnImport.Enabled = true; }
        }
    }
}
