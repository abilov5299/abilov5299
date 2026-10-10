namespace TexnikaReyestri.Forms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlBrand = new TexnikaReyestri.Controls.BrandPanel();
            this.logo = new TexnikaReyestri.Controls.LogoMark();
            this.lblBrand = new System.Windows.Forms.Label();
            this.lblBrandSub = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.pnlForm = new System.Windows.Forms.Panel();
            this.btnClose = new TexnikaReyestri.Controls.FlatButton();
            this.btnTheme = new TexnikaReyestri.Controls.FlatButton();
            this.cmbLang = new TexnikaReyestri.Controls.RoundComboBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtUser = new TexnikaReyestri.Controls.RoundTextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.txtPass = new TexnikaReyestri.Controls.RoundTextBox();
            this.btnLogin = new TexnikaReyestri.Controls.FlatButton();
            this.lblError = new System.Windows.Forms.Label();
            this.pnlBrand.SuspendLayout();
            this.pnlForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBrand
            // 
            this.pnlBrand.Controls.Add(this.lblVersion);
            this.pnlBrand.Controls.Add(this.lblBrandSub);
            this.pnlBrand.Controls.Add(this.lblBrand);
            this.pnlBrand.Controls.Add(this.logo);
            this.pnlBrand.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlBrand.Size = new System.Drawing.Size(420, 540);
            this.pnlBrand.Padding = new System.Windows.Forms.Padding(40);
            this.pnlBrand.Name = "pnlBrand";
            // 
            // logo
            // 
            this.logo.Location = new System.Drawing.Point(40, 40);
            this.logo.Size = new System.Drawing.Size(132, 132);
            this.logo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logo.Name = "logo";
            this.logo.Click += new System.EventHandler(this.logo_Click);
            // 
            // lblBrand
            // 
            this.lblBrand.AutoSize = true;
            this.lblBrand.Font = new System.Drawing.Font("Bahnschrift SemiBold", 30.0F);
            this.lblBrand.Location = new System.Drawing.Point(36, 196);
            this.lblBrand.Text = "TEXNİKA\r\nREYESTRİ";
            this.lblBrand.Tag = "white";
            this.lblBrand.Name = "lblBrand";
            // 
            // lblBrandSub
            // 
            this.lblBrandSub.AutoSize = true;
            this.lblBrandSub.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.lblBrandSub.Location = new System.Drawing.Point(40, 306);
            this.lblBrandSub.Text = "Təmir zavodları üzrə texnikanın qəbulu, təmiri və təhvilinin uçotu";
            this.lblBrandSub.Tag = "navtext";
            this.lblBrandSub.MaximumSize = new System.Drawing.Size(330, 0);
            this.lblBrandSub.Name = "lblBrandSub";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 8.0F);
            this.lblVersion.Location = new System.Drawing.Point(40, 500);
            this.lblVersion.Text = "v1.0 · Lokal baza · 7 zavod";
            this.lblVersion.Tag = "navtext";
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblVersion.Name = "lblVersion";
            // 
            // pnlForm
            // 
            this.pnlForm.Controls.Add(this.lblError);
            this.pnlForm.Controls.Add(this.btnLogin);
            this.pnlForm.Controls.Add(this.txtPass);
            this.pnlForm.Controls.Add(this.lblPass);
            this.pnlForm.Controls.Add(this.txtUser);
            this.pnlForm.Controls.Add(this.lblUser);
            this.pnlForm.Controls.Add(this.lblHeader);
            this.pnlForm.Controls.Add(this.cmbLang);
            this.pnlForm.Controls.Add(this.btnTheme);
            this.pnlForm.Controls.Add(this.btnClose);
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlForm.Size = new System.Drawing.Size(540, 540);
            this.pnlForm.Tag = "card";
            this.pnlForm.Name = "pnlForm";
            // 
            // btnClose
            // 
            this.btnClose.Kind = TexnikaReyestri.Controls.ButtonKind.Ghost;
            this.btnClose.Location = new System.Drawing.Point(492, 8);
            this.btnClose.Size = new System.Drawing.Size(40, 34);
            this.btnClose.Text = "";
            this.btnClose.Glyph = "\uE8BB";
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnTheme
            // 
            this.btnTheme.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            this.btnTheme.Location = new System.Drawing.Point(384, 8);
            this.btnTheme.Size = new System.Drawing.Size(40, 34);
            this.btnTheme.Text = "";
            this.btnTheme.Glyph = "\uE708";
            this.btnTheme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTheme.Name = "btnTheme";
            this.btnTheme.Click += new System.EventHandler(this.btnTheme_Click);
            // 
            // cmbLang
            // 
            this.cmbLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLang.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F);
            this.cmbLang.Location = new System.Drawing.Point(430, 11);
            this.cmbLang.Size = new System.Drawing.Size(56, 25);
            this.cmbLang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLang.Name = "cmbLang";
            this.cmbLang.Items.AddRange(new object[] {
            "AZ",
            "EN",
            "RU"});
            this.cmbLang.SelectedIndexChanged += new System.EventHandler(this.cmbLang_SelectedIndexChanged);
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Bahnschrift SemiBold", 20.0F);
            this.lblHeader.Location = new System.Drawing.Point(118, 148);
            this.lblHeader.Text = "Sistemə giriş";
            this.lblHeader.Tag = "title";
            this.lblHeader.Name = "lblHeader";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblUser.Location = new System.Drawing.Point(120, 204);
            this.lblUser.Text = "İstifadəçi adı";
            this.lblUser.Tag = "muted";
            this.lblUser.Name = "lblUser";
            // 
            // txtUser
            // 
            this.txtUser.Font = new System.Drawing.Font("Segoe UI", 11.0F);
            this.txtUser.Location = new System.Drawing.Point(120, 224);
            this.txtUser.Size = new System.Drawing.Size(300, 27);
            this.txtUser.Name = "txtUser";
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblPass.Location = new System.Drawing.Point(120, 266);
            this.lblPass.Text = "Parol";
            this.lblPass.Tag = "muted";
            this.lblPass.Name = "lblPass";
            // 
            // txtPass
            // 
            this.txtPass.Font = new System.Drawing.Font("Segoe UI", 11.0F);
            this.txtPass.Location = new System.Drawing.Point(120, 286);
            this.txtPass.Size = new System.Drawing.Size(300, 27);
            this.txtPass.UseSystemPasswordChar = true;
            this.txtPass.Name = "txtPass";
            // 
            // btnLogin
            // 
            this.btnLogin.Kind = TexnikaReyestri.Controls.ButtonKind.Primary;
            this.btnLogin.Location = new System.Drawing.Point(120, 340);
            this.btnLogin.Size = new System.Drawing.Size(300, 42);
            this.btnLogin.Text = "DAXİL OL";
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 10.0F);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblError
            // 
            this.lblError.AutoSize = false;
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblError.Location = new System.Drawing.Point(120, 394);
            this.lblError.Text = "";
            this.lblError.Tag = "danger";
            this.lblError.Size = new System.Drawing.Size(300, 44);
            this.lblError.Name = "lblError";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(960, 540);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Texnika Reyestri";
            this.AcceptButton = this.btnLogin;
            this.KeyPreview = true;
            this.Controls.Add(this.pnlForm);
            this.Controls.Add(this.pnlBrand);
            this.Name = "LoginForm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LoginForm_KeyDown);
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            this.pnlBrand.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private TexnikaReyestri.Controls.BrandPanel pnlBrand;
        private TexnikaReyestri.Controls.LogoMark logo;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.Label lblBrandSub;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Panel pnlForm;
        private TexnikaReyestri.Controls.FlatButton btnClose;
        private TexnikaReyestri.Controls.FlatButton btnTheme;
        private TexnikaReyestri.Controls.RoundComboBox cmbLang;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblUser;
        private TexnikaReyestri.Controls.RoundTextBox txtUser;
        private System.Windows.Forms.Label lblPass;
        private TexnikaReyestri.Controls.RoundTextBox txtPass;
        private TexnikaReyestri.Controls.FlatButton btnLogin;
        private System.Windows.Forms.Label lblError;
    }
}
