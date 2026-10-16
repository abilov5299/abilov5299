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
            pnlBrand = new TexnikaReyestri.Controls.BrandPanel();
            lblVersion = new Label();
            lblBrandSub = new Label();
            lblBrand = new Label();
            logo = new TexnikaReyestri.Controls.LogoMark();
            pnlForm = new Panel();
            lblError = new Label();
            btnLogin = new TexnikaReyestri.Controls.FlatButton();
            txtPass = new TexnikaReyestri.Controls.RoundTextBox();
            lblPass = new Label();
            txtUser = new TexnikaReyestri.Controls.RoundTextBox();
            lblUser = new Label();
            lblHeader = new Label();
            cmbLang = new TexnikaReyestri.Controls.RoundComboBox();
            btnTheme = new TexnikaReyestri.Controls.FlatButton();
            btnClose = new TexnikaReyestri.Controls.FlatButton();
            pnlBrand.SuspendLayout();
            pnlForm.SuspendLayout();
            SuspendLayout();
            // 
            // pnlBrand
            // 
            pnlBrand.BackColor = Color.FromArgb(15, 76, 74);
            pnlBrand.Controls.Add(lblVersion);
            pnlBrand.Controls.Add(lblBrandSub);
            pnlBrand.Controls.Add(lblBrand);
            pnlBrand.Controls.Add(logo);
            pnlBrand.Dock = DockStyle.Left;
            pnlBrand.Location = new Point(0, 0);
            pnlBrand.Name = "pnlBrand";
            pnlBrand.Padding = new Padding(40);
            pnlBrand.Size = new Size(420, 540);
            pnlBrand.TabIndex = 1;
            // 
            // lblVersion
            // 
            lblVersion.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblVersion.AutoSize = true;
            lblVersion.Font = new Font("Segoe UI", 8F);
            lblVersion.Location = new Point(41, 500);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(138, 13);
            lblVersion.TabIndex = 0;
            lblVersion.Tag = "navtext";
            lblVersion.Text = "v1.0 · Lokal baza · 6 zavod";
            // 
            // lblBrandSub
            // 
            lblBrandSub.AutoSize = true;
            lblBrandSub.Font = new Font("Segoe UI", 10F);
            lblBrandSub.Location = new Point(40, 306);
            lblBrandSub.MaximumSize = new Size(330, 0);
            lblBrandSub.Name = "lblBrandSub";
            lblBrandSub.Size = new Size(307, 38);
            lblBrandSub.TabIndex = 1;
            lblBrandSub.Tag = "navtext";
            lblBrandSub.Text = "Təmir zavodları üzrə texnikanın qəbulu, təmiri və təhvilinin uçotu";
            // 
            // lblBrand
            // 
            lblBrand.AutoSize = true;
            lblBrand.Font = new Font("Bahnschrift SemiBold", 30F);
            lblBrand.Location = new Point(36, 196);
            lblBrand.Name = "lblBrand";
            lblBrand.Size = new Size(196, 96);
            lblBrand.TabIndex = 2;
            lblBrand.Tag = "white";
            lblBrand.Text = "TEXNİKA\r\nREYESTRİ";
            // 
            // logo
            // 
            logo.BackColor = Color.Transparent;
            logo.Cursor = Cursors.Hand;
            logo.Location = new Point(40, 40);
            logo.Name = "logo";
            logo.Size = new Size(132, 132);
            logo.TabIndex = 3;
            logo.Click += logo_Click;
            // 
            // pnlForm
            // 
            pnlForm.Controls.Add(lblError);
            pnlForm.Controls.Add(btnLogin);
            pnlForm.Controls.Add(txtPass);
            pnlForm.Controls.Add(lblPass);
            pnlForm.Controls.Add(txtUser);
            pnlForm.Controls.Add(lblUser);
            pnlForm.Controls.Add(lblHeader);
            pnlForm.Controls.Add(cmbLang);
            pnlForm.Controls.Add(btnTheme);
            pnlForm.Controls.Add(btnClose);
            pnlForm.Dock = DockStyle.Fill;
            pnlForm.Location = new Point(420, 0);
            pnlForm.Name = "pnlForm";
            pnlForm.Size = new Size(540, 540);
            pnlForm.TabIndex = 0;
            pnlForm.Tag = "card";
            // 
            // lblError
            // 
            lblError.Font = new Font("Segoe UI", 9F);
            lblError.Location = new Point(120, 394);
            lblError.Name = "lblError";
            lblError.Size = new Size(300, 44);
            lblError.TabIndex = 0;
            lblError.Tag = "danger";
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.Transparent;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI Semibold", 10F);
            btnLogin.Location = new Point(120, 340);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(300, 42);
            btnLogin.TabIndex = 1;
            btnLogin.Text = "DAXİL OL";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // txtPass
            // 
            txtPass.Font = new Font("Segoe UI", 11F);
            txtPass.Location = new Point(120, 286);
            txtPass.Name = "txtPass";
            txtPass.Size = new Size(300, 27);
            txtPass.TabIndex = 2;
            txtPass.UseSystemPasswordChar = true;
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.Font = new Font("Segoe UI", 9F);
            lblPass.Location = new Point(120, 266);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(34, 15);
            lblPass.TabIndex = 3;
            lblPass.Tag = "muted";
            lblPass.Text = "Parol";
            // 
            // txtUser
            // 
            txtUser.Font = new Font("Segoe UI", 11F);
            txtUser.Location = new Point(120, 224);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(300, 27);
            txtUser.TabIndex = 4;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Font = new Font("Segoe UI", 9F);
            lblUser.Location = new Point(120, 204);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(73, 15);
            lblUser.TabIndex = 5;
            lblUser.Tag = "muted";
            lblUser.Text = "İstifadəçi adı";
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Bahnschrift SemiBold", 20F);
            lblHeader.Location = new Point(118, 148);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(175, 33);
            lblHeader.TabIndex = 6;
            lblHeader.Tag = "title";
            lblHeader.Text = "Sistemə giriş";
            // 
            // cmbLang
            // 
            cmbLang.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbLang.DrawMode = DrawMode.OwnerDrawFixed;
            cmbLang.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLang.FlatStyle = FlatStyle.Flat;
            cmbLang.Font = new Font("Segoe UI Semibold", 9.5F);
            cmbLang.IntegralHeight = false;
            cmbLang.ItemHeight = 22;
            cmbLang.Items.AddRange(new object[] { "AZ", "EN", "RU" });
            cmbLang.Location = new Point(430, 11);
            cmbLang.Name = "cmbLang";
            cmbLang.Size = new Size(56, 28);
            cmbLang.TabIndex = 7;
            cmbLang.SelectedIndexChanged += cmbLang_SelectedIndexChanged;
            // 
            // btnTheme
            // 
            btnTheme.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTheme.BackColor = Color.Transparent;
            btnTheme.FlatStyle = FlatStyle.Flat;
            btnTheme.Font = new Font("Segoe UI Semibold", 9F);
            btnTheme.Glyph = "";
            btnTheme.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            btnTheme.Location = new Point(384, 8);
            btnTheme.Name = "btnTheme";
            btnTheme.Size = new Size(40, 34);
            btnTheme.TabIndex = 8;
            btnTheme.UseVisualStyleBackColor = false;
            btnTheme.Click += btnTheme_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.BackColor = Color.Transparent;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI Semibold", 9F);
            btnClose.Glyph = "";
            btnClose.Kind = TexnikaReyestri.Controls.ButtonKind.Ghost;
            btnClose.Location = new Point(492, 8);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(40, 34);
            btnClose.TabIndex = 9;
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // LoginForm
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(960, 540);
            Controls.Add(pnlForm);
            Controls.Add(pnlBrand);
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Texnika Reyestri";
            Load += LoginForm_Load;
            KeyDown += LoginForm_KeyDown;
            pnlBrand.ResumeLayout(false);
            pnlBrand.PerformLayout();
            pnlForm.ResumeLayout(false);
            pnlForm.PerformLayout();
            ResumeLayout(false);
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
