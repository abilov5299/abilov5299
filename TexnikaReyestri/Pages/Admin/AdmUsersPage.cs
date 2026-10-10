using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;
using TexnikaReyestri.Forms;
using TexnikaReyestri.Services;

namespace TexnikaReyestri.Pages.Admin
{
    public partial class AdmUsersPage : PageBase
    {
        private List<Istifadeci> _l = new(); private List<Qurum> _q = new();
        public AdmUsersPage() { InitializeComponent(); grid.RowTemplate.Height = 36; txtFilter.TextChanged += (_, _) => Goster(); }

        public override async Task OnShownAsync()
        {
            _q = await SistemRepo.QurumlarAsync();
            if (cmbOrg.Items.Count == 0) { cmbOrg.Items.Add("Qurum: Hamısı"); foreach (var q in _q) cmbOrg.Items.Add(Lang.T(q.Ad)); cmbOrg.SelectedIndex = 0; }
            _l = await SistemRepo.IstifadecilerAsync(); Goster();
        }

        private List<Istifadeci> _v = new();
        private void Goster()
        {
            string f = txtFilter.Text.Trim().ToLowerInvariant(); string qk = cmbOrg.SelectedIndex > 0 ? _q[cmbOrg.SelectedIndex - 1].Kod : null;
            _v = _l.Where(u => (qk == null || u.QurumKod == qk) && (f.Length == 0 || (u.Login + " " + u.AdSoyad).ToLowerInvariant().Contains(f))).ToList();
            grid.Rows.Clear();
            foreach (var u in _v)
            {
                int i = grid.Rows.Add(u.Login, u.AdSoyad, Lang.T(_q.FirstOrDefault(q => q.Kod == u.QurumKod)?.Ad ?? u.QurumKod), Lang.T(u.Rol), Vaxt.Yaz(u.SonGiris), u.Aktiv ? "Aktiv" : "Bloklanıb");
                grid.Rows[i].Cells["colBlock"].Value = Lang.T(u.Aktiv ? "Blokla" : "Aktiv et");
            }
        }

        private async void filtr_Changed(object sender, EventArgs e) { if (_l.Count > 0) Goster(); await Task.CompletedTask; }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using var f = new UserEditForm(); if (f.ShowDialog(this) == DialogResult.OK) { Toast.Show(this, "Yadda saxlanıldı"); await OnShownAsync(); }
        }

        private async void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _v.Count) return;
            var u = _v[e.RowIndex]; string col = grid.Columns[e.ColumnIndex].Name;
            if (col == "colEdit") { using var f = new UserEditForm(u); if (f.ShowDialog(this) == DialogResult.OK) await OnShownAsync(); }
            else if (col == "colReset")
            {
                if (!Ui.Tesdiq(this, "Parolu sıfırla", $"\"{u.Login}\": {Lang.T("yeni müvəqqəti parol yaradılsın?")}")) return;
                string t = AuthService.MuveqqetiParol(); await AuthService.ParolDeyisAsync(u.Login, t, true);
                await SistemRepo.AdminLogAsync($"\"{u.Login}\" parolu sıfırlandı");
                Ui.Melumat(this, "Müvəqqəti parol", $"\"{u.Login}\": {t}\n\n{Lang.T("İstifadəçi ilk girişdə öz parolunu təyin etməlidir. Bu parol yalnız indi göstərilir.")}");
            }
            else if (col == "colBlock")
            {
                await AuthService.YenileAsync(u.Login, u.AdSoyad, u.QurumKod, u.Rol, !u.Aktiv);
                await SistemRepo.AdminLogAsync($"\"{u.Login}\" hesabı {(u.Aktiv ? "bloklandı" : "aktiv edildi")}"); await OnShownAsync();
            }
        }
    }
}
