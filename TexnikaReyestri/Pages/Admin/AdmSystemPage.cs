using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Data;

namespace TexnikaReyestri.Pages.Admin
{
    public partial class AdmSystemPage : PageBase
    {
        private static readonly (string ad, int susma)[] P = { ("BildirisYaxin", 3), ("BildirisGec", 0), ("BildirisSorgu", 30), ("BildirisPaket", 7), ("NusxeSay", 30), ("IcraSari", 20) };
        private CheckBox chkGirisAnim, chkSes, chkLoqo, chkXat; private NumericUpDown numGirisMs;
        public AdmSystemPage()
        {
            InitializeComponent();
            // Giriş pəncərəsi parametrləri (chkAnim-in altında)
            CheckBox C(string t, int y) { var c = new CheckBox { Text = t, AutoSize = true, Font = chkAnim.Font, Location = new Point(chkAnim.Left, y) }; rpGen.Controls.Add(c); return c; }
            int y0 = chkAnim.Bottom + 24;
            rpGen.Controls.Add(new Label { Text = "Giriş pəncərəsi", AutoSize = true, Font = new Font("Segoe UI Semibold", 10f), Location = new Point(chkAnim.Left, y0) });
            chkGirisAnim = C("Kölgədən çıxma animasiyası", y0 + 30);
            rpGen.Controls.Add(new Label { Text = "Müddət (ms):", AutoSize = true, Tag = "muted", Location = new Point(chkAnim.Left + 230, y0 + 32) });
            numGirisMs = new NumericUpDown { Minimum = 200, Maximum = 3000, Increment = 100, Value = 900, Width = 80, Location = new Point(chkAnim.Left + 320, y0 + 28) }; rpGen.Controls.Add(numGirisMs);
            chkSes = C("Açılışda Windows səsi", y0 + 62);
            chkXat = C("\"Bu kompüterdə məni xatırla\" (yalnız istifadəçi adı)", y0 + 94);
            chkLoqo = C("Proqramçı loqosu (giriş pəncərəsində)", y0 + 126);
        }
        private NumericUpDown[] N => new[] { numSoon, numLate, numReq, numPkg, numKeep, numSari };

        public override async Task OnShownAsync()
        {
            for (int i = 0; i < P.Length; i++) N[i].Value = await SistemRepo.ParamAsync(P[i].ad, P[i].susma);
            chkAnim.Checked = await SistemRepo.ParamAsync("Animasiya", 1) == 1;
            int ms = await SistemRepo.ParamAsync("GirisAnimasiya", 900); chkGirisAnim.Checked = ms > 0; numGirisMs.Value = Math.Clamp(ms > 0 ? ms : 900, 200, 3000);
            chkSes.Checked = await SistemRepo.ParamAsync("GirisSes", 1) == 1; chkXat.Checked = await SistemRepo.ParamAsync("GirisXatirla", 1) == 1; chkLoqo.Checked = await SistemRepo.ParamAsync("ProqramciLoqo", 1) == 1;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < P.Length; i++) await SistemRepo.ParamYazAsync(P[i].ad, ((int)N[i].Value).ToString());
            await SistemRepo.ParamYazAsync("Animasiya", chkAnim.Checked ? "1" : "0");
            await SistemRepo.ParamYazAsync("GirisAnimasiya", chkGirisAnim.Checked ? ((int)numGirisMs.Value).ToString() : "0");
            await SistemRepo.ParamYazAsync("GirisSes", chkSes.Checked ? "1" : "0"); await SistemRepo.ParamYazAsync("ProqramciLoqo", chkLoqo.Checked ? "1" : "0");
            await SistemRepo.ParamYazAsync("GirisXatirla", chkXat.Checked ? "1" : "0"); if (!chkXat.Checked) await SistemRepo.ParamYazAsync("XatirlananLogin", ""); Animator.Aktiv = chkAnim.Checked; ReyestrFiltr.Yaxin = (int)numSoon.Value; ReyestrFiltr.Sari = (int)numSari.Value;
            await SistemRepo.AdminLogAsync("Sistem parametrləri dəyişdi"); Toast.Show(this, "Yadda saxlanıldı");
        }
    }
}
