using TexnikaQeydiyyat.Data;

namespace TexnikaQeydiyyat.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            Text = $"Texnika qeydiyyatı — {Session.QurumAd} — {Session.AdSoyad}";

            if (Session.YazaBiler)
            {
                cmbZavod.Visible = lblZavod.Visible = false;
                ParametrSablonunuDoldur();
            }
            else
            {
                // Üst qurumlar yalnız baxır
                tabControl.TabPages.Remove(tabDaxilEt);
                btnSil.Visible = false;
                await ZavodlariYukle();
            }
            await SiyahiniYukle();
        }

        private int? SecilmisZavod()
        {
            if (Session.ZavodId.HasValue) return Session.ZavodId;
            return cmbZavod.SelectedValue is int id && id > 0 ? id : null;
        }

        private async Task ZavodlariYukle()
        {
            var dt = await TexnikaService.ZavodlarAsync();
            var row = dt.NewRow(); row["Id"] = 0; row["Ad"] = "Hamısı";
            dt.Rows.InsertAt(row, 0);
            cmbZavod.SelectedIndexChanged -= cmbZavod_SelectedIndexChanged;
            cmbZavod.DisplayMember = "Ad";
            cmbZavod.ValueMember = "Id";
            cmbZavod.DataSource = dt;
            cmbZavod.SelectedIndexChanged += cmbZavod_SelectedIndexChanged;
        }

        private async Task SiyahiniYukle()
        {
            btnYenile.Enabled = false;
            try { dgvTexnika.DataSource = await TexnikaService.SiyahiAsync(SecilmisZavod()); }
            catch (Exception ex) { Xeta(ex); }
            finally { btnYenile.Enabled = true; }
        }

        private async Task LoglariYukle()
        {
            btnLogYenile.Enabled = false;
            try { dgvLog.DataSource = await LogService.SiyahiAsync(SecilmisZavod()); }
            catch (Exception ex) { Xeta(ex); }
            finally { btnLogYenile.Enabled = true; }
        }

        private void ParametrSablonunuDoldur()
        {
            dgvParam.Rows.Clear();
            foreach (var p in TexnikaService.ParametrSablonu) dgvParam.Rows.Add(p, "");
        }

        private async void btnYenile_Click(object sender, EventArgs e) => await SiyahiniYukle();
        private async void btnLogYenile_Click(object sender, EventArgs e) => await LoglariYukle();
        private async void cmbZavod_SelectedIndexChanged(object sender, EventArgs e) => await SiyahiniYukle();

        private async void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabLog) await LoglariYukle();
        }

        private async void btnSil_Click(object sender, EventArgs e)
        {
            if (dgvTexnika.CurrentRow == null) return;
            int id = (int)dgvTexnika.CurrentRow.Cells["Id"].Value;
            string ad = dgvTexnika.CurrentRow.Cells["Ad"].Value?.ToString();
            if (MessageBox.Show($"\"{ad}\" silinsin?", "Təsdiq", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try { await TexnikaService.SilAsync(id, ad); await SiyahiniYukle(); }
            catch (Exception ex) { Xeta(ex); }
        }

        private async void btnYaddaSaxla_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAd.Text))
            {
                MessageBox.Show("Texnikanın adını daxil edin.");
                return;
            }
            var parametrler = dgvParam.Rows.Cast<DataGridViewRow>()
                .Where(r => !r.IsNewRow && !string.IsNullOrWhiteSpace(r.Cells[0].Value?.ToString()))
                .Select(r => (r.Cells[0].Value.ToString().Trim(), r.Cells[1].Value?.ToString()?.Trim()))
                .ToList();

            btnYaddaSaxla.Enabled = false;
            try
            {
                await TexnikaService.ElaveEtAsync(txtAd.Text.Trim(), txtModel.Text.Trim(), txtSeriya.Text.Trim(),
                                                  dtpQebul.Value, txtQeyd.Text.Trim(), parametrler);
                txtAd.Clear(); txtModel.Clear(); txtSeriya.Clear(); txtQeyd.Clear();
                ParametrSablonunuDoldur();
                await SiyahiniYukle();
                tabControl.SelectedTab = tabSiyahi;
            }
            catch (Exception ex) { Xeta(ex); }
            finally { btnYaddaSaxla.Enabled = true; }
        }

        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e) =>
            await LogService.YazAsync("Cixis", $"{Session.AdSoyad} çıxış etdi");

        private static void Xeta(Exception ex)
        {
            _ = LogService.YazAsync("Xeta", ex.Message);
            MessageBox.Show(ex.Message, "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
