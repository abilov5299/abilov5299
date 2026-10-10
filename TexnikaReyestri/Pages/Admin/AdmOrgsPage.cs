using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;

namespace TexnikaReyestri.Pages.Admin
{
    public partial class AdmOrgsPage : PageBase
    {
        private List<Qurum> _q = new();
        public AdmOrgsPage()
        {
            InitializeComponent(); grid.RowTemplate.Height = 34;
            foreach (DataGridViewColumn c in grid.Columns) c.ReadOnly = c.Name != "colName";
        }

        public override async Task OnShownAsync()
        {
            _q = await SistemRepo.QurumlarAsync(); var u = await SistemRepo.IstifadecilerAsync();
            int Say(string k) => u.Count(x => x.QurumKod == k);
            tree.Nodes.Clear(); tree.BackColor = Theme.Card; tree.ForeColor = Theme.Ink;
            foreach (var top in _q.Where(q => q.UstKod == null))
            {
                var n = tree.Nodes.Add($"🏛 {Lang.T(top.Ad)} · {Say(top.Kod)}");
                foreach (var mid in _q.Where(q => q.UstKod == top.Kod))
                {
                    var m = n.Nodes.Add($"🏢 {Lang.T(mid.Ad)} · {Say(mid.Kod)}");
                    foreach (var z in _q.Where(q => q.UstKod == mid.Kod)) m.Nodes.Add($"🏭 {Lang.T(z.Ad)} · {Say(z.Kod)}");
                }
            }
            tree.ExpandAll();
            grid.Rows.Clear();
            foreach (var z in _q.Where(q => q.Tip == QurumTip.Zavod)) grid.Rows.Add(z.Kod, z.Ad, Lang.T(_q.First(x => x.Kod == z.UstKod).Ad), Say(z.Kod));
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            grid.EndEdit();
            foreach (DataGridViewRow r in grid.Rows)
            {
                var q = _q.First(x => x.Kod == (string)r.Cells["colCode"].Value); string ad = (r.Cells["colName"].Value as string ?? "").Trim();
                if (ad.Length > 0 && ad != q.Ad) { await SistemRepo.QurumSaxlaAsync(q with { Ad = ad }); await SistemRepo.AdminLogAsync($"Qurumun adı dəyişdi: \"{q.Ad}\" → \"{ad}\""); }
            }
            Toast.Show(this, "Yadda saxlanıldı"); await OnShownAsync();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            int n = _q.Count(q => q.Tip == QurumTip.Zavod) + 1;
            while (_q.Any(q => q.Kod == $"Z{n}")) n++;
            await SistemRepo.QurumSaxlaAsync(new Qurum($"Z{n}", $"Zavod {n}", QurumTip.Zavod, "UST"));
            await SistemRepo.AdminLogAsync($"Yeni zavod: \"Zavod {n}\""); Toast.Show(this, $"Zavod {n} əlavə edildi"); await OnShownAsync();
        }
    }
}
