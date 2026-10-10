namespace TexnikaReyestri.Forms
{
    partial class AdminLoginForm
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
            this.ltxtPass = new System.Windows.Forms.Label();
            this.txtPass = new TexnikaReyestri.Controls.RoundTextBox();
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
            this.pnlBody.Controls.Add(this.lblInfo);
            this.pnlBody.Controls.Add(this.lblTitle);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Size = new System.Drawing.Size(348, 218);
            this.pnlBody.Tag = "card";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(24, 18, 24, 16);
            this.pnlBody.Name = "pnlBody";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 15.0F);
            this.lblTitle.Location = new System.Drawing.Point(24, 18);
            this.lblTitle.Text = "🔒 Administrator";
            this.lblTitle.Tag = "title";
            this.lblTitle.Name = "lblTitle";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblInfo.Location = new System.Drawing.Point(24, 52);
            this.lblInfo.Text = "Bu bölmə yalnız sistem administratoru üçündür.";
            this.lblInfo.Tag = "muted";
            this.lblInfo.Name = "lblInfo";
            // 
            // ltxtPass
            // 
            this.ltxtPass.AutoSize = true;
            this.ltxtPass.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.ltxtPass.Location = new System.Drawing.Point(24, 82);
            this.ltxtPass.Text = "Admin parolu";
            this.ltxtPass.Tag = "muted";
            this.ltxtPass.Name = "ltxtPass";
            // 
            // txtPass
            // 
            this.txtPass.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.txtPass.Location = new System.Drawing.Point(24, 102);
            this.txtPass.Size = new System.Drawing.Size(300, 25);
            this.txtPass.UseSystemPasswordChar = true;
            this.txtPass.Name = "txtPass";
            // 
            // lblErr
            // 
            this.lblErr.AutoSize = true;
            this.lblErr.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblErr.Location = new System.Drawing.Point(24, 134);
            this.lblErr.Text = "";
            this.lblErr.Tag = "danger";
            this.lblErr.Name = "lblErr";
            // 
            // btnOk
            // 
            this.btnOk.Kind = TexnikaReyestri.Controls.ButtonKind.Primary;
            this.btnOk.Location = new System.Drawing.Point(214, 166);
            this.btnOk.Size = new System.Drawing.Size(110, 36);
            this.btnOk.Text = "Daxil ol";
            this.btnOk.Name = "btnOk";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            this.btnCancel.Location = new System.Drawing.Point(98, 166);
            this.btnCancel.Size = new System.Drawing.Size(110, 36);
            this.btnCancel.Text = "Ləğv et";
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AdminLoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(350, 220);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ShowInTaskbar = false;
            this.Padding = new System.Windows.Forms.Padding(1);
            this.AcceptButton = this.btnOk;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.pnlBody);
            this.Name = "AdminLoginForm";
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label ltxtPass;
        private TexnikaReyestri.Controls.RoundTextBox txtPass;
        private System.Windows.Forms.Label lblErr;
        private TexnikaReyestri.Controls.FlatButton btnOk;
        private TexnikaReyestri.Controls.FlatButton btnCancel;
    }
}
