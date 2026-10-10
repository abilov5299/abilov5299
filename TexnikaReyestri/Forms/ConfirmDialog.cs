using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;

namespace TexnikaReyestri.Forms
{
    /// <summary>Təsdiq və məlumat pəncərəsi (MessageBox əvəzinə, temaya uyğun).</summary>
    public partial class ConfirmDialog : Form
    {
        public ConfirmDialog() { InitializeComponent(); }

        public ConfirmDialog(string basliq, string mesaj, string ok, bool tehlukeli, bool yalnizOk = false) : this()
        {
            lblTitle.Text = basliq; lblMessage.Text = mesaj; btnOk.Text = ok;
            if (tehlukeli) btnOk.Kind = ButtonKind.Danger;
            if (yalnizOk) btnCancel.Visible = false;
            using var g = CreateGraphics();
            var h = TextRenderer.MeasureText(g, Lang.T(mesaj), lblMessage.Font, new Size(lblMessage.Width, 1000), TextFormatFlags.WordBreak).Height;
            if (h > lblMessage.Height) Height += h - lblMessage.Height;
        }

        protected override void OnLoad(EventArgs e) { Ui.Hazirla(this); BackColor = Theme.Line; base.OnLoad(e); }
        private void btnOk_Click(object sender, EventArgs e) { DialogResult = DialogResult.OK; }
        private void btnCancel_Click(object sender, EventArgs e) { DialogResult = DialogResult.Cancel; }
    }
}
