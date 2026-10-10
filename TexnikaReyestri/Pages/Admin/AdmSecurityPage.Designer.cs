namespace TexnikaReyestri.Pages.Admin
{
    partial class AdmSecurityPage
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
            this.pnlHead = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnSave = new TexnikaReyestri.Controls.FlatButton();
            this.tlp = new System.Windows.Forms.TableLayoutPanel();
            this.rpPw = new TexnikaReyestri.Controls.RoundPanel();
            this.lblPwTitle = new System.Windows.Forms.Label();
            this.knumMin = new System.Windows.Forms.Label();
            this.dnumMin = new System.Windows.Forms.Label();
            this.numMin = new System.Windows.Forms.NumericUpDown();
            this.unumMin = new System.Windows.Forms.Label();
            this.knumExp = new System.Windows.Forms.Label();
            this.dnumExp = new System.Windows.Forms.Label();
            this.numExp = new System.Windows.Forms.NumericUpDown();
            this.unumExp = new System.Windows.Forms.Label();
            this.rpLogin = new TexnikaReyestri.Controls.RoundPanel();
            this.lblLoginTitle = new System.Windows.Forms.Label();
            this.knumLock = new System.Windows.Forms.Label();
            this.dnumLock = new System.Windows.Forms.Label();
            this.numLock = new System.Windows.Forms.NumericUpDown();
            this.unumLock = new System.Windows.Forms.Label();
            this.knumIdle = new System.Windows.Forms.Label();
            this.dnumIdle = new System.Windows.Forms.Label();
            this.numIdle = new System.Windows.Forms.NumericUpDown();
            this.unumIdle = new System.Windows.Forms.Label();
            this.lblAdminPw = new System.Windows.Forms.Label();
            this.lNew = new System.Windows.Forms.Label();
            this.txtNew = new TexnikaReyestri.Controls.RoundTextBox();
            this.lNew2 = new System.Windows.Forms.Label();
            this.txtNew2 = new TexnikaReyestri.Controls.RoundTextBox();
            this.btnAdminPw = new TexnikaReyestri.Controls.FlatButton();
            ((System.ComponentModel.ISupportInitialize)(this.numMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIdle)).BeginInit();
            this.pnlHead.SuspendLayout();
            this.tlp.SuspendLayout();
            this.rpPw.SuspendLayout();
            this.rpLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHead
            // 
            this.pnlHead.Controls.Add(this.btnSave);
            this.pnlHead.Controls.Add(this.lblTitle);
            this.pnlHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHead.Height = 44;
            this.pnlHead.Name = "pnlHead";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 18.0F);
            this.lblTitle.Location = new System.Drawing.Point(0, 4);
            this.lblTitle.Text = "Təhlükəsizlik";
            this.lblTitle.Tag = "title";
            this.lblTitle.Name = "lblTitle";
            // 
            // btnSave
            // 
            this.btnSave.Kind = TexnikaReyestri.Controls.ButtonKind.Primary;
            this.btnSave.Location = new System.Drawing.Point(828, 4);
            this.btnSave.Size = new System.Drawing.Size(130, 34);
            this.btnSave.Text = "Yadda saxla";
            this.btnSave.Glyph = "\uE74E";
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tlp
            // 
            this.tlp.ColumnCount = 2;
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0F));
            this.tlp.RowCount = 1;
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0F));
            this.tlp.Controls.Add(this.rpPw, 0, 0);
            this.tlp.Controls.Add(this.rpLogin, 1, 0);
            this.tlp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp.Name = "tlp";
            // 
            // rpPw
            // 
            this.rpPw.Controls.Add(this.unumExp);
            this.rpPw.Controls.Add(this.numExp);
            this.rpPw.Controls.Add(this.dnumExp);
            this.rpPw.Controls.Add(this.knumExp);
            this.rpPw.Controls.Add(this.unumMin);
            this.rpPw.Controls.Add(this.numMin);
            this.rpPw.Controls.Add(this.dnumMin);
            this.rpPw.Controls.Add(this.knumMin);
            this.rpPw.Controls.Add(this.lblPwTitle);
            this.rpPw.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpPw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpPw.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.rpPw.Name = "rpPw";
            // 
            // lblPwTitle
            // 
            this.lblPwTitle.AutoSize = false;
            this.lblPwTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblPwTitle.Location = new System.Drawing.Point(0, 0);
            this.lblPwTitle.Text = "Parol qaydaları";
            this.lblPwTitle.Tag = "muted";
            this.lblPwTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPwTitle.Height = 26;
            this.lblPwTitle.Name = "lblPwTitle";
            // 
            // knumMin
            // 
            this.knumMin.AutoSize = true;
            this.knumMin.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.knumMin.Location = new System.Drawing.Point(14, 44);
            this.knumMin.Text = "Parolun minimal uzunluğu";
            this.knumMin.Name = "knumMin";
            // 
            // dnumMin
            // 
            this.dnumMin.AutoSize = true;
            this.dnumMin.Font = new System.Drawing.Font("Segoe UI", 8.0F);
            this.dnumMin.Location = new System.Drawing.Point(14, 62);
            this.dnumMin.Text = "Böyük hərf, kiçik hərf və rəqəm tələb olunur";
            this.dnumMin.Tag = "muted";
            this.dnumMin.Name = "dnumMin";
            // 
            // numMin
            // 
            this.numMin.Location = new System.Drawing.Point(330, 48);
            this.numMin.Size = new System.Drawing.Size(70, 25);
            this.numMin.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.numMin.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            this.numMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numMin.Name = "numMin";
            // 
            // unumMin
            // 
            this.unumMin.AutoSize = true;
            this.unumMin.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.unumMin.Location = new System.Drawing.Point(406, 52);
            this.unumMin.Text = "simvol";
            this.unumMin.Tag = "muted";
            this.unumMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.unumMin.Name = "unumMin";
            // 
            // knumExp
            // 
            this.knumExp.AutoSize = true;
            this.knumExp.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.knumExp.Location = new System.Drawing.Point(14, 100);
            this.knumExp.Text = "Parolun etibarlılıq müddəti";
            this.knumExp.Name = "knumExp";
            // 
            // dnumExp
            // 
            this.dnumExp.AutoSize = true;
            this.dnumExp.Font = new System.Drawing.Font("Segoe UI", 8.0F);
            this.dnumExp.Location = new System.Drawing.Point(14, 118);
            this.dnumExp.Text = "Bitəndə istifadəçi yeni parol təyin edir (0 = müddətsiz)";
            this.dnumExp.Tag = "muted";
            this.dnumExp.Name = "dnumExp";
            // 
            // numExp
            // 
            this.numExp.Location = new System.Drawing.Point(330, 104);
            this.numExp.Size = new System.Drawing.Size(70, 25);
            this.numExp.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.numExp.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            this.numExp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numExp.Name = "numExp";
            // 
            // unumExp
            // 
            this.unumExp.AutoSize = true;
            this.unumExp.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.unumExp.Location = new System.Drawing.Point(406, 108);
            this.unumExp.Text = "gün";
            this.unumExp.Tag = "muted";
            this.unumExp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.unumExp.Name = "unumExp";
            // 
            // rpLogin
            // 
            this.rpLogin.Controls.Add(this.btnAdminPw);
            this.rpLogin.Controls.Add(this.txtNew2);
            this.rpLogin.Controls.Add(this.lNew2);
            this.rpLogin.Controls.Add(this.txtNew);
            this.rpLogin.Controls.Add(this.lNew);
            this.rpLogin.Controls.Add(this.lblAdminPw);
            this.rpLogin.Controls.Add(this.unumIdle);
            this.rpLogin.Controls.Add(this.numIdle);
            this.rpLogin.Controls.Add(this.dnumIdle);
            this.rpLogin.Controls.Add(this.knumIdle);
            this.rpLogin.Controls.Add(this.unumLock);
            this.rpLogin.Controls.Add(this.numLock);
            this.rpLogin.Controls.Add(this.dnumLock);
            this.rpLogin.Controls.Add(this.knumLock);
            this.rpLogin.Controls.Add(this.lblLoginTitle);
            this.rpLogin.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpLogin.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.rpLogin.Name = "rpLogin";
            // 
            // lblLoginTitle
            // 
            this.lblLoginTitle.AutoSize = false;
            this.lblLoginTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblLoginTitle.Location = new System.Drawing.Point(0, 0);
            this.lblLoginTitle.Text = "Giriş qaydaları";
            this.lblLoginTitle.Tag = "muted";
            this.lblLoginTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLoginTitle.Height = 26;
            this.lblLoginTitle.Name = "lblLoginTitle";
            // 
            // knumLock
            // 
            this.knumLock.AutoSize = true;
            this.knumLock.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.knumLock.Location = new System.Drawing.Point(14, 44);
            this.knumLock.Text = "Yanlış cəhdlərdən sonra blok";
            this.knumLock.Name = "knumLock";
            // 
            // dnumLock
            // 
            this.dnumLock.AutoSize = true;
            this.dnumLock.Font = new System.Drawing.Font("Segoe UI", 8.0F);
            this.dnumLock.Location = new System.Drawing.Point(14, 62);
            this.dnumLock.Text = "Hesab bloklanır, admin açır";
            this.dnumLock.Tag = "muted";
            this.dnumLock.Name = "dnumLock";
            // 
            // numLock
            // 
            this.numLock.Location = new System.Drawing.Point(330, 48);
            this.numLock.Size = new System.Drawing.Size(70, 25);
            this.numLock.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.numLock.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            this.numLock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numLock.Name = "numLock";
            // 
            // unumLock
            // 
            this.unumLock.AutoSize = true;
            this.unumLock.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.unumLock.Location = new System.Drawing.Point(406, 52);
            this.unumLock.Text = "cəhd";
            this.unumLock.Tag = "muted";
            this.unumLock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.unumLock.Name = "unumLock";
            // 
            // knumIdle
            // 
            this.knumIdle.AutoSize = true;
            this.knumIdle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.knumIdle.Location = new System.Drawing.Point(14, 100);
            this.knumIdle.Text = "Fəaliyyətsizlikdə avtomatik çıxış";
            this.knumIdle.Name = "knumIdle";
            // 
            // dnumIdle
            // 
            this.dnumIdle.AutoSize = true;
            this.dnumIdle.Font = new System.Drawing.Font("Segoe UI", 8.0F);
            this.dnumIdle.Location = new System.Drawing.Point(14, 118);
            this.dnumIdle.Text = "Kompüter başında olmayanda (0 = söndürülüb)";
            this.dnumIdle.Tag = "muted";
            this.dnumIdle.Name = "dnumIdle";
            // 
            // numIdle
            // 
            this.numIdle.Location = new System.Drawing.Point(330, 104);
            this.numIdle.Size = new System.Drawing.Size(70, 25);
            this.numIdle.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.numIdle.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            this.numIdle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numIdle.Name = "numIdle";
            // 
            // unumIdle
            // 
            this.unumIdle.AutoSize = true;
            this.unumIdle.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.unumIdle.Location = new System.Drawing.Point(406, 108);
            this.unumIdle.Text = "dəq";
            this.unumIdle.Tag = "muted";
            this.unumIdle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.unumIdle.Name = "unumIdle";
            // 
            // lblAdminPw
            // 
            this.lblAdminPw.AutoSize = true;
            this.lblAdminPw.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblAdminPw.Location = new System.Drawing.Point(14, 170);
            this.lblAdminPw.Text = "Admin parolunu dəyiş";
            this.lblAdminPw.Tag = "muted";
            this.lblAdminPw.Name = "lblAdminPw";
            // 
            // lNew
            // 
            this.lNew.AutoSize = true;
            this.lNew.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lNew.Location = new System.Drawing.Point(14, 200);
            this.lNew.Text = "Yeni admin parolu";
            this.lNew.Tag = "muted";
            this.lNew.Name = "lNew";
            // 
            // txtNew
            // 
            this.txtNew.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.txtNew.Location = new System.Drawing.Point(14, 220);
            this.txtNew.Size = new System.Drawing.Size(200, 25);
            this.txtNew.UseSystemPasswordChar = true;
            this.txtNew.Name = "txtNew";
            // 
            // lNew2
            // 
            this.lNew2.AutoSize = true;
            this.lNew2.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lNew2.Location = new System.Drawing.Point(222, 200);
            this.lNew2.Text = "Təkrar";
            this.lNew2.Tag = "muted";
            this.lNew2.Name = "lNew2";
            // 
            // txtNew2
            // 
            this.txtNew2.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.txtNew2.Location = new System.Drawing.Point(222, 220);
            this.txtNew2.Size = new System.Drawing.Size(200, 25);
            this.txtNew2.UseSystemPasswordChar = true;
            this.txtNew2.Name = "txtNew2";
            // 
            // btnAdminPw
            // 
            this.btnAdminPw.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            this.btnAdminPw.Location = new System.Drawing.Point(14, 258);
            this.btnAdminPw.Size = new System.Drawing.Size(210, 34);
            this.btnAdminPw.Text = "Admin parolunu dəyiş";
            this.btnAdminPw.Glyph = "\uE72E";
            this.btnAdminPw.Name = "btnAdminPw";
            this.btnAdminPw.Click += new System.EventHandler(this.btnAdminPw_Click);
            // 
            // AdmSecurityPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(958, 698);
            this.Controls.Add(this.tlp);
            this.Controls.Add(this.pnlHead);
            this.Name = "AdmSecurityPage";
            ((System.ComponentModel.ISupportInitialize)(this.numMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIdle)).EndInit();
            this.rpLogin.ResumeLayout(false);
            this.rpLogin.PerformLayout();
            this.rpPw.ResumeLayout(false);
            this.rpPw.PerformLayout();
            this.tlp.ResumeLayout(false);
            this.tlp.PerformLayout();
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Label lblTitle;
        private TexnikaReyestri.Controls.FlatButton btnSave;
        private System.Windows.Forms.TableLayoutPanel tlp;
        private TexnikaReyestri.Controls.RoundPanel rpPw;
        private System.Windows.Forms.Label lblPwTitle;
        private System.Windows.Forms.Label knumMin;
        private System.Windows.Forms.Label dnumMin;
        private System.Windows.Forms.NumericUpDown numMin;
        private System.Windows.Forms.Label unumMin;
        private System.Windows.Forms.Label knumExp;
        private System.Windows.Forms.Label dnumExp;
        private System.Windows.Forms.NumericUpDown numExp;
        private System.Windows.Forms.Label unumExp;
        private TexnikaReyestri.Controls.RoundPanel rpLogin;
        private System.Windows.Forms.Label lblLoginTitle;
        private System.Windows.Forms.Label knumLock;
        private System.Windows.Forms.Label dnumLock;
        private System.Windows.Forms.NumericUpDown numLock;
        private System.Windows.Forms.Label unumLock;
        private System.Windows.Forms.Label knumIdle;
        private System.Windows.Forms.Label dnumIdle;
        private System.Windows.Forms.NumericUpDown numIdle;
        private System.Windows.Forms.Label unumIdle;
        private System.Windows.Forms.Label lblAdminPw;
        private System.Windows.Forms.Label lNew;
        private TexnikaReyestri.Controls.RoundTextBox txtNew;
        private System.Windows.Forms.Label lNew2;
        private TexnikaReyestri.Controls.RoundTextBox txtNew2;
        private TexnikaReyestri.Controls.FlatButton btnAdminPw;
    }
}
