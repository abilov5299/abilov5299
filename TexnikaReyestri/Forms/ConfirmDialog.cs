using TexnikaReyestri.Controls;
using TexnikaReyestri.Core;

namespace TexnikaReyestri.Forms
{
    /// <summary>Təsdiq və məlumat pəncərəsi (MessageBox əvəzinə, temaya uyğun).</summary>
    public partial class ConfirmDialog : Form
    {
        public ConfirmDialog() { InitializeComponent(); }

        public ConfirmDialog(string basliq, string mesaj, string ok, bool tehlukeli, bool yalnizOk = false, string xeyr = null) : this()
        {
            lblTitle.Text = basliq; lblMessage.Text = mesaj; btnOk.Text = ok;
            if (tehlukeli) btnOk.Kind = ButtonKind.Danger;
            if (yalnizOk) btnCancel.Visible = false;
            if (xeyr != null)
            {
                // Üçüncü düymə (məs. "Saxlama") → DialogResult.No
                var b = new FlatButton { Kind = ButtonKind.Secondary, Text = xeyr, Size = btnCancel.Size, Location = new Point(btnCancel.Left - btnCancel.Width - 10, btnCancel.Top), Anchor = btnCancel.Anchor };
                b.Click += (_, _) => DialogResult = DialogResult.No;
                pnlBody.Controls.Add(b);
            }
            using var g = CreateGraphics();
            var h = TextRenderer.MeasureText(g, Lang.T(mesaj), lblMessage.Font, new Size(lblMessage.Width, 1000), TextFormatFlags.WordBreak).Height;
            if (h > lblMessage.Height) Height += h - lblMessage.Height;
        }

        protected override void OnLoad(EventArgs e) { Ui.Hazirla(this); BackColor = Theme.Line; base.OnLoad(e); }
        private void btnOk_Click(object sender, EventArgs e) { DialogResult = DialogResult.OK; }
        private void btnCancel_Click(object sender, EventArgs e) { DialogResult = DialogResult.Cancel; }

        protected override CreateParams CreateParams { get { var cp = base.CreateParams; cp.ClassStyle |= Win32.CS_DROPSHADOW; return cp; } }
        protected override void OnHandleCreated(EventArgs e) { base.OnHandleCreated(e); Win32.Yumru(this); }
    }
}
