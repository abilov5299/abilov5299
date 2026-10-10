namespace TexnikaReyestri.Forms
{
    partial class ChangePasswordForm
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
            this.ltxtNew = new System.Windows.Forms.Label();
            this.txtNew = new TexnikaReyestri.Controls.RoundTextBox();
            this.ltxtNew2 = new System.Windows.Forms.Label();
            this.txtNew2 = new TexnikaReyestri.Controls.RoundTextBox();
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
            this.pnlBody.Controls.Add(this.txtNew2);
            this.pnlBody.Controls.Add(this.ltxtNew2);
            this.pnlBody.Controls.Add(this.txtNew);
            this.pnlBody.Controls.Add(this.ltxtNew);
            this.pnlBody.Controls.Add(this.lblInfo);
            this.pnlBody.Controls.Add(this.lblTitle);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Size = new System.Drawing.Size(348, 288);
            this.pnlBody.Tag = "card";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(24, 18, 24, 16);
            this.pnlBody.Name = "pnlBody";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 15.0F);
            this.lblTitle.Location = new System.Drawing.Point(24, 18);
            this.lblTitle.Text = "Parolu dəyiş";
            this.lblTitle.Tag = "title";
            this.lblTitle.Name = "lblTitle";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblInfo.Location = new System.Drawing.Point(24, 52);
            this.lblInfo.Text = "Müvəqqəti parolla daxil oldunuz. Davam etmək üçün yeni parol təyin edin.";
            this.lblInfo.Tag = "muted";
            this.lblInfo.MaximumSize = new System.Drawing.Size(320, 0);
            this.lblInfo.Name = "lblInfo";
            // 
            // ltxtNew
            // 
            this.ltxtNew.AutoSize = true;
            this.ltxtNew.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.ltxtNew.Location = new System.Drawing.Point(24, 96);
            this.ltxtNew.Text = "Yeni parol";
            this.ltxtNew.Tag = "muted";
            this.ltxtNew.Name = "ltxtNew";
            // 
            // txtNew
            // 
            this.txtNew.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.txtNew.Location = new System.Drawing.Point(24, 116);
            this.txtNew.Size = new System.Drawing.Size(300, 25);
            this.txtNew.UseSystemPasswordChar = true;
            this.txtNew.Name = "txtNew";
            // 
            // ltxtNew2
            // 
            this.ltxtNew2.AutoSize = true;
            this.ltxtNew2.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.ltxtNew2.Location = new System.Drawing.Point(24, 146);
            this.ltxtNew2.Text = "Parolu təkrar yazın";
            this.ltxtNew2.Tag = "muted";
            this.ltxtNew2.Name = "ltxtNew2";
            // 
            // txtNew2
            // 
            this.txtNew2.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.txtNew2.Location = new System.Drawing.Point(24, 166);
            this.txtNew2.Size = new System.Drawing.Size(300, 25);
            this.txtNew2.UseSystemPasswordChar = true;
            this.txtNew2.Name = "txtNew2";
            // 
            // lblErr
            // 
            this.lblErr.AutoSize = true;
            this.lblErr.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblErr.Location = new System.Drawing.Point(24, 198);
            this.lblErr.Text = "";
            this.lblErr.Tag = "danger";
            this.lblErr.MaximumSize = new System.Drawing.Size(320, 0);
            this.lblErr.Name = "lblErr";
            // 
            // btnOk
            // 
            this.btnOk.Kind = TexnikaReyestri.Controls.ButtonKind.Primary;
            this.btnOk.Location = new System.Drawing.Point(214, 236);
            this.btnOk.Size = new System.Drawing.Size(110, 36);
            this.btnOk.Text = "Yadda saxla";
            this.btnOk.Name = "btnOk";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            this.btnCancel.Location = new System.Drawing.Point(98, 236);
            this.btnCancel.Size = new System.Drawing.Size(110, 36);
            this.btnCancel.Text = "Ləğv et";
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ChangePasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(350, 290);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ShowInTaskbar = false;
            this.Padding = new System.Windows.Forms.Padding(1);
            this.AcceptButton = this.btnOk;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.pnlBody);
            this.Name = "ChangePasswordForm";
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label ltxtNew;
        private TexnikaReyestri.Controls.RoundTextBox txtNew;
        private System.Windows.Forms.Label ltxtNew2;
        private TexnikaReyestri.Controls.RoundTextBox txtNew2;
        private System.Windows.Forms.Label lblErr;
        private TexnikaReyestri.Controls.FlatButton btnOk;
        private TexnikaReyestri.Controls.FlatButton btnCancel;
    }
}
