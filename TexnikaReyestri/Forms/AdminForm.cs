using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;
using TexnikaReyestri.Pages.Admin;

namespace TexnikaReyestri.Forms
{
    /// <summary>Gizli admin paneli: istifadəçilər, qurumlar, bu kompüter, təhlükəsizlik, USB açarları, sistem, jurnal.</summary>
    public partial class AdminForm : Form
    {
        private readonly Dictionary<RadioButton, PageBase> _p = new();
        public AdminForm() { InitializeComponent(); pnlAside.MouseDown += (_, e) => { if (e.Button == MouseButtons.Left) Win32.DragWindow(this); }; }

        private async void AdminForm_Load(object sender, EventArgs e)
        {
            Ui.Hazirla(this); BackColor = Theme.Line; pnlAside.BackColor = Theme.C("#1B2B2A");   // tünd yan menyu (tema onu şəffaf etməsin)
            foreach (var r in new[] { radUsers, radOrgs, radPc, radSec, radKeys, radSys, radLog }) { r.FlatAppearance.BorderSize = 0; r.FlatAppearance.CheckedBackColor = Theme.C("#26393A"); r.FlatAppearance.MouseOverBackColor = Theme.C("#26393A"); }
            await AcAsync(radUsers);
        }

        private async void tab_CheckedChanged(object sender, EventArgs e) { if (sender is RadioButton { Checked: true } r) await AcAsync(r); }

        private async Task AcAsync(RadioButton r)
        {
            if (!_p.TryGetValue(r, out var p))
            {
                p = r == radUsers ? new AdmUsersPage() : r == radOrgs ? new AdmOrgsPage() : r == radPc ? new AdmPcPage() : r == radSec ? new AdmSecurityPage()
                  : r == radKeys ? new AdmKeysPage() : r == radSys ? new AdmSystemPage() : new AdmJournalPage();
                p.Dock = DockStyle.Fill; pnlContent.Controls.Add(p); Ui.Hazirla(p); _p[r] = p;
            }
            foreach (var x in _p.Values) x.Visible = x == p;
            foreach (var b in _p.Keys) b.ForeColor = b.Checked ? Color.White : Theme.C("#CFE0DE");
            p.BringToFront(); await p.OnShownAsync();
        }

        private void btnExit_Click(object sender, EventArgs e) => Close();
        protected override void OnPaint(PaintEventArgs e) { base.OnPaint(e); Ui.Kenar(this, e); }

        protected override CreateParams CreateParams { get { var cp = base.CreateParams; cp.ClassStyle |= Win32.CS_DROPSHADOW; return cp; } }
        protected override void OnHandleCreated(EventArgs e) { base.OnHandleCreated(e); Win32.Yumru(this); }
    }
}
