namespace TexnikaQeydiyyat.Forms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblLogin = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.lblParol = new System.Windows.Forms.Label();
            this.txtParol = new System.Windows.Forms.TextBox();
            this.btnDaxil = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // lblLogin
            //
            this.lblLogin.AutoSize = true;
            this.lblLogin.Location = new System.Drawing.Point(30, 30);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Text = "İstifadəçi adı";
            //
            // txtLogin
            //
            this.txtLogin.Location = new System.Drawing.Point(30, 50);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(260, 23);
            this.txtLogin.TabIndex = 0;
            //
            // lblParol
            //
            this.lblParol.AutoSize = true;
            this.lblParol.Location = new System.Drawing.Point(30, 85);
            this.lblParol.Name = "lblParol";
            this.lblParol.Text = "Parol";
            //
            // txtParol
            //
            this.txtParol.Location = new System.Drawing.Point(30, 105);
            this.txtParol.Name = "txtParol";
            this.txtParol.Size = new System.Drawing.Size(260, 23);
            this.txtParol.TabIndex = 1;
            this.txtParol.UseSystemPasswordChar = true;
            //
            // btnDaxil
            //
            this.btnDaxil.Location = new System.Drawing.Point(30, 145);
            this.btnDaxil.Name = "btnDaxil";
            this.btnDaxil.Size = new System.Drawing.Size(260, 32);
            this.btnDaxil.TabIndex = 2;
            this.btnDaxil.Text = "Daxil ol";
            this.btnDaxil.Click += new System.EventHandler(this.btnDaxil_Click);
            //
            // lblStatus
            //
            this.lblStatus.Location = new System.Drawing.Point(30, 185);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(260, 40);
            this.lblStatus.ForeColor = System.Drawing.Color.Firebrick;
            //
            // LoginForm
            //
            this.AcceptButton = this.btnDaxil;
            this.ClientSize = new System.Drawing.Size(320, 240);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.lblParol);
            this.Controls.Add(this.txtParol);
            this.Controls.Add(this.btnDaxil);
            this.Controls.Add(this.lblStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistemə giriş";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label lblParol;
        private System.Windows.Forms.TextBox txtParol;
        private System.Windows.Forms.Button btnDaxil;
        private System.Windows.Forms.Label lblStatus;
    }
}
