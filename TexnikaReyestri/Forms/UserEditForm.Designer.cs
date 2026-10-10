namespace TexnikaReyestri.Forms
{
    partial class UserEditForm
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
            this.ltxtLogin = new System.Windows.Forms.Label();
            this.txtLogin = new TexnikaReyestri.Controls.RoundTextBox();
            this.ltxtName = new System.Windows.Forms.Label();
            this.txtName = new TexnikaReyestri.Controls.RoundTextBox();
            this.lQurum = new System.Windows.Forms.Label();
            this.cmbQurum = new TexnikaReyestri.Controls.RoundComboBox();
            this.lRol = new System.Windows.Forms.Label();
            this.cmbRol = new TexnikaReyestri.Controls.RoundComboBox();
            this.ltxtPass = new System.Windows.Forms.Label();
            this.txtPass = new TexnikaReyestri.Controls.RoundTextBox();
            this.chkTemp = new System.Windows.Forms.CheckBox();
            this.lblRoleInfo = new System.Windows.Forms.Label();
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
            this.pnlBody.Controls.Add(this.lblRoleInfo);
            this.pnlBody.Controls.Add(this.chkTemp);
            this.pnlBody.Controls.Add(this.txtPass);
            this.pnlBody.Controls.Add(this.ltxtPass);
            this.pnlBody.Controls.Add(this.cmbRol);
            this.pnlBody.Controls.Add(this.lRol);
            this.pnlBody.Controls.Add(this.cmbQurum);
            this.pnlBody.Controls.Add(this.lQurum);
            this.pnlBody.Controls.Add(this.txtName);
            this.pnlBody.Controls.Add(this.ltxtName);
            this.pnlBody.Controls.Add(this.txtLogin);
            this.pnlBody.Controls.Add(this.ltxtLogin);
            this.pnlBody.Controls.Add(this.lblTitle);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Size = new System.Drawing.Size(610, 324);
            this.pnlBody.Tag = "card";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(24, 18, 24, 16);
            this.pnlBody.Name = "pnlBody";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 15.0F);
            this.lblTitle.Location = new System.Drawing.Point(24, 18);
            this.lblTitle.Text = "Yeni istifadəçi";
            this.lblTitle.Tag = "title";
            this.lblTitle.Name = "lblTitle";
            // 
            // ltxtLogin
            // 
            this.ltxtLogin.AutoSize = true;
            this.ltxtLogin.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.ltxtLogin.Location = new System.Drawing.Point(24, 56);
            this.ltxtLogin.Text = "Login *";
            this.ltxtLogin.Tag = "muted";
            this.ltxtLogin.Name = "ltxtLogin";
            // 
            // txtLogin
            // 
            this.txtLogin.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.txtLogin.Location = new System.Drawing.Point(24, 76);
            this.txtLogin.Size = new System.Drawing.Size(270, 25);
            this.txtLogin.Name = "txtLogin";
            // 
            // ltxtName
            // 
            this.ltxtName.AutoSize = true;
            this.ltxtName.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.ltxtName.Location = new System.Drawing.Point(316, 56);
            this.ltxtName.Text = "Ad Soyad *";
            this.ltxtName.Tag = "muted";
            this.ltxtName.Name = "ltxtName";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.txtName.Location = new System.Drawing.Point(316, 76);
            this.txtName.Size = new System.Drawing.Size(270, 25);
            this.txtName.Name = "txtName";
            // 
            // lQurum
            // 
            this.lQurum.AutoSize = true;
            this.lQurum.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lQurum.Location = new System.Drawing.Point(24, 106);
            this.lQurum.Text = "Qurum *";
            this.lQurum.Tag = "muted";
            this.lQurum.Name = "lQurum";
            // 
            // cmbQurum
            // 
            this.cmbQurum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQurum.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.cmbQurum.Location = new System.Drawing.Point(24, 126);
            this.cmbQurum.Size = new System.Drawing.Size(270, 25);
            this.cmbQurum.Name = "cmbQurum";
            this.cmbQurum.SelectedIndexChanged += new System.EventHandler(this.cmbQurum_SelectedIndexChanged);
            // 
            // lRol
            // 
            this.lRol.AutoSize = true;
            this.lRol.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lRol.Location = new System.Drawing.Point(316, 106);
            this.lRol.Text = "Rol";
            this.lRol.Tag = "muted";
            this.lRol.Name = "lRol";
            // 
            // cmbRol
            // 
            this.cmbRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRol.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.cmbRol.Location = new System.Drawing.Point(316, 126);
            this.cmbRol.Size = new System.Drawing.Size(270, 25);
            this.cmbRol.Name = "cmbRol";
            this.cmbRol.SelectedIndexChanged += new System.EventHandler(this.cmbQurum_SelectedIndexChanged);
            // 
            // ltxtPass
            // 
            this.ltxtPass.AutoSize = true;
            this.ltxtPass.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.ltxtPass.Location = new System.Drawing.Point(24, 156);
            this.ltxtPass.Text = "Parol *";
            this.ltxtPass.Tag = "muted";
            this.ltxtPass.Name = "ltxtPass";
            // 
            // txtPass
            // 
            this.txtPass.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.txtPass.Location = new System.Drawing.Point(24, 176);
            this.txtPass.Size = new System.Drawing.Size(270, 25);
            this.txtPass.Name = "txtPass";
            // 
            // chkTemp
            // 
            this.chkTemp.AutoSize = true;
            this.chkTemp.Text = "İlk girişdə parolu dəyişsin";
            this.chkTemp.Location = new System.Drawing.Point(316, 180);
            this.chkTemp.Checked = true;
            this.chkTemp.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.chkTemp.Name = "chkTemp";
            // 
            // lblRoleInfo
            // 
            this.lblRoleInfo.AutoSize = true;
            this.lblRoleInfo.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblRoleInfo.Location = new System.Drawing.Point(24, 214);
            this.lblRoleInfo.Text = "";
            this.lblRoleInfo.Tag = "muted";
            this.lblRoleInfo.MaximumSize = new System.Drawing.Size(560, 0);
            this.lblRoleInfo.Name = "lblRoleInfo";
            // 
            // lblErr
            // 
            this.lblErr.AutoSize = true;
            this.lblErr.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblErr.Location = new System.Drawing.Point(24, 246);
            this.lblErr.Text = "";
            this.lblErr.Tag = "danger";
            this.lblErr.Name = "lblErr";
            // 
            // btnOk
            // 
            this.btnOk.Kind = TexnikaReyestri.Controls.ButtonKind.Primary;
            this.btnOk.Location = new System.Drawing.Point(456, 272);
            this.btnOk.Size = new System.Drawing.Size(130, 36);
            this.btnOk.Text = "Yadda saxla";
            this.btnOk.Name = "btnOk";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            this.btnCancel.Location = new System.Drawing.Point(316, 272);
            this.btnCancel.Size = new System.Drawing.Size(130, 36);
            this.btnCancel.Text = "Ləğv et";
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // UserEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(612, 326);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ShowInTaskbar = false;
            this.Padding = new System.Windows.Forms.Padding(1);
            this.AcceptButton = this.btnOk;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.pnlBody);
            this.Name = "UserEditForm";
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label ltxtLogin;
        private TexnikaReyestri.Controls.RoundTextBox txtLogin;
        private System.Windows.Forms.Label ltxtName;
        private TexnikaReyestri.Controls.RoundTextBox txtName;
        private System.Windows.Forms.Label lQurum;
        private TexnikaReyestri.Controls.RoundComboBox cmbQurum;
        private System.Windows.Forms.Label lRol;
        private TexnikaReyestri.Controls.RoundComboBox cmbRol;
        private System.Windows.Forms.Label ltxtPass;
        private TexnikaReyestri.Controls.RoundTextBox txtPass;
        private System.Windows.Forms.CheckBox chkTemp;
        private System.Windows.Forms.Label lblRoleInfo;
        private System.Windows.Forms.Label lblErr;
        private TexnikaReyestri.Controls.FlatButton btnOk;
        private TexnikaReyestri.Controls.FlatButton btnCancel;
    }
}
