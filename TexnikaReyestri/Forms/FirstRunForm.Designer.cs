namespace TexnikaReyestri.Forms
{
    partial class FirstRunForm
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
            this.pnlBody = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lQurum = new System.Windows.Forms.Label();
            this.cmbQurum = new System.Windows.Forms.ComboBox();
            this.lSecAdmin = new System.Windows.Forms.Label();
            this.lSecUser = new System.Windows.Forms.Label();
            this.ltxtAdmin = new System.Windows.Forms.Label();
            this.txtAdmin = new System.Windows.Forms.TextBox();
            this.ltxtAdmin2 = new System.Windows.Forms.Label();
            this.txtAdmin2 = new System.Windows.Forms.TextBox();
            this.ltxtLogin = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.ltxtName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.ltxtPass = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.lblErr = new System.Windows.Forms.Label();
            this.btnOk = new TexnikaReyestri.Controls.FlatButton();
            this.btnCancel = new TexnikaReyestri.Controls.FlatButton();
            this.pnlBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.btnCancel);
            this.pnlBody.Controls.Add(this.btnOk);
            this.pnlBody.Controls.Add(this.lblErr);
            this.pnlBody.Controls.Add(this.txtPass);
            this.pnlBody.Controls.Add(this.ltxtPass);
            this.pnlBody.Controls.Add(this.txtName);
            this.pnlBody.Controls.Add(this.ltxtName);
            this.pnlBody.Controls.Add(this.txtLogin);
            this.pnlBody.Controls.Add(this.ltxtLogin);
            this.pnlBody.Controls.Add(this.txtAdmin2);
            this.pnlBody.Controls.Add(this.ltxtAdmin2);
            this.pnlBody.Controls.Add(this.txtAdmin);
            this.pnlBody.Controls.Add(this.ltxtAdmin);
            this.pnlBody.Controls.Add(this.lSecUser);
            this.pnlBody.Controls.Add(this.lSecAdmin);
            this.pnlBody.Controls.Add(this.cmbQurum);
            this.pnlBody.Controls.Add(this.lQurum);
            this.pnlBody.Controls.Add(this.lblInfo);
            this.pnlBody.Controls.Add(this.lblTitle);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Tag = "card";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(24, 18, 24, 16);
            this.pnlBody.Name = "pnlBody";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 15.0F);
            this.lblTitle.Location = new System.Drawing.Point(24, 18);
            this.lblTitle.Text = "İlk quraşdırma";
            this.lblTitle.Tag = "title";
            this.lblTitle.Name = "lblTitle";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblInfo.Location = new System.Drawing.Point(24, 52);
            this.lblInfo.Text = "Proqram bu kompüterdə ilk dəfə açılır. Kompüterin qurumunu seçin, admin parolunu və ilk istifadəçini yaradın. Qurumu sonradan dəyişmək bazanı sıfırlayır.";
            this.lblInfo.Tag = "muted";
            this.lblInfo.MaximumSize = new System.Drawing.Size(560, 0);
            this.lblInfo.Name = "lblInfo";
            // 
            // lQurum
            // 
            this.lQurum.AutoSize = true;
            this.lQurum.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lQurum.Location = new System.Drawing.Point(24, 102);
            this.lQurum.Text = "Bu kompüterin qurumu";
            this.lQurum.Tag = "muted";
            this.lQurum.Name = "lQurum";
            // 
            // cmbQurum
            // 
            this.cmbQurum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQurum.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.cmbQurum.Location = new System.Drawing.Point(24, 122);
            this.cmbQurum.Size = new System.Drawing.Size(270, 25);
            this.cmbQurum.Name = "cmbQurum";
            // 
            // lSecAdmin
            // 
            this.lSecAdmin.AutoSize = true;
            this.lSecAdmin.Font = new System.Drawing.Font("Bahnschrift SemiBold", 11.0F);
            this.lSecAdmin.Location = new System.Drawing.Point(24, 164);
            this.lSecAdmin.Text = "Administrator";
            this.lSecAdmin.Tag = "title";
            this.lSecAdmin.Name = "lSecAdmin";
            // 
            // lSecUser
            // 
            this.lSecUser.AutoSize = true;
            this.lSecUser.Font = new System.Drawing.Font("Bahnschrift SemiBold", 11.0F);
            this.lSecUser.Location = new System.Drawing.Point(316, 164);
            this.lSecUser.Text = "İlk istifadəçi";
            this.lSecUser.Tag = "title";
            this.lSecUser.Name = "lSecUser";
            // 
            // ltxtAdmin
            // 
            this.ltxtAdmin.AutoSize = true;
            this.ltxtAdmin.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.ltxtAdmin.Location = new System.Drawing.Point(24, 192);
            this.ltxtAdmin.Text = "Admin parolu (ən azı 12 simvol)";
            this.ltxtAdmin.Tag = "muted";
            this.ltxtAdmin.Name = "ltxtAdmin";
            // 
            // txtAdmin
            // 
            this.txtAdmin.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.txtAdmin.Location = new System.Drawing.Point(24, 212);
            this.txtAdmin.Size = new System.Drawing.Size(270, 25);
            this.txtAdmin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdmin.UseSystemPasswordChar = true;
            this.txtAdmin.Name = "txtAdmin";
            // 
            // ltxtAdmin2
            // 
            this.ltxtAdmin2.AutoSize = true;
            this.ltxtAdmin2.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.ltxtAdmin2.Location = new System.Drawing.Point(24, 242);
            this.ltxtAdmin2.Text = "Admin parolunu təkrar yazın";
            this.ltxtAdmin2.Tag = "muted";
            this.ltxtAdmin2.Name = "ltxtAdmin2";
            // 
            // txtAdmin2
            // 
            this.txtAdmin2.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.txtAdmin2.Location = new System.Drawing.Point(24, 262);
            this.txtAdmin2.Size = new System.Drawing.Size(270, 25);
            this.txtAdmin2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdmin2.UseSystemPasswordChar = true;
            this.txtAdmin2.Name = "txtAdmin2";
            // 
            // ltxtLogin
            // 
            this.ltxtLogin.AutoSize = true;
            this.ltxtLogin.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.ltxtLogin.Location = new System.Drawing.Point(316, 192);
            this.ltxtLogin.Text = "İstifadəçi adı (login)";
            this.ltxtLogin.Tag = "muted";
            this.ltxtLogin.Name = "ltxtLogin";
            // 
            // txtLogin
            // 
            this.txtLogin.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.txtLogin.Location = new System.Drawing.Point(316, 212);
            this.txtLogin.Size = new System.Drawing.Size(270, 25);
            this.txtLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLogin.Name = "txtLogin";
            // 
            // ltxtName
            // 
            this.ltxtName.AutoSize = true;
            this.ltxtName.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.ltxtName.Location = new System.Drawing.Point(316, 242);
            this.ltxtName.Text = "Ad Soyad";
            this.ltxtName.Tag = "muted";
            this.ltxtName.Name = "ltxtName";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.txtName.Location = new System.Drawing.Point(316, 262);
            this.txtName.Size = new System.Drawing.Size(270, 25);
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Name = "txtName";
            // 
            // ltxtPass
            // 
            this.ltxtPass.AutoSize = true;
            this.ltxtPass.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.ltxtPass.Location = new System.Drawing.Point(316, 292);
            this.ltxtPass.Text = "Parol";
            this.ltxtPass.Tag = "muted";
            this.ltxtPass.Name = "ltxtPass";
            // 
            // txtPass
            // 
            this.txtPass.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.txtPass.Location = new System.Drawing.Point(316, 312);
            this.txtPass.Size = new System.Drawing.Size(270, 25);
            this.txtPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPass.UseSystemPasswordChar = true;
            this.txtPass.Name = "txtPass";
            // 
            // lblErr
            // 
            this.lblErr.AutoSize = true;
            this.lblErr.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblErr.Location = new System.Drawing.Point(24, 350);
            this.lblErr.Text = "";
            this.lblErr.Tag = "danger";
            this.lblErr.MaximumSize = new System.Drawing.Size(560, 0);
            this.lblErr.Name = "lblErr";
            // 
            // btnOk
            // 
            this.btnOk.Kind = TexnikaReyestri.Controls.ButtonKind.Primary;
            this.btnOk.Location = new System.Drawing.Point(456, 384);
            this.btnOk.Size = new System.Drawing.Size(130, 38);
            this.btnOk.Text = "Quraşdır";
            this.btnOk.Name = "btnOk";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            this.btnCancel.Location = new System.Drawing.Point(316, 384);
            this.btnCancel.Size = new System.Drawing.Size(130, 38);
            this.btnCancel.Text = "Ləğv et";
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FirstRunForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(612, 440);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ShowInTaskbar = true;
            this.Padding = new System.Windows.Forms.Padding(1);
            this.AcceptButton = this.btnOk;
            this.Text = "Texnika Reyestri";
            this.Controls.Add(this.pnlBody);
            this.Name = "FirstRunForm";
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lQurum;
        private System.Windows.Forms.ComboBox cmbQurum;
        private System.Windows.Forms.Label lSecAdmin;
        private System.Windows.Forms.Label lSecUser;
        private System.Windows.Forms.Label ltxtAdmin;
        private System.Windows.Forms.TextBox txtAdmin;
        private System.Windows.Forms.Label ltxtAdmin2;
        private System.Windows.Forms.TextBox txtAdmin2;
        private System.Windows.Forms.Label ltxtLogin;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label ltxtName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label ltxtPass;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label lblErr;
        private TexnikaReyestri.Controls.FlatButton btnOk;
        private TexnikaReyestri.Controls.FlatButton btnCancel;
    }
}
