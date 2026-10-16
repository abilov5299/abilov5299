namespace TexnikaReyestri.Pages.Admin
{
    partial class AdmSystemPage
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
            this.rpNoti = new TexnikaReyestri.Controls.RoundPanel();
            this.lblNotiTitle = new System.Windows.Forms.Label();
            this.knumSoon = new System.Windows.Forms.Label();
            this.dnumSoon = new System.Windows.Forms.Label();
            this.numSoon = new System.Windows.Forms.NumericUpDown();
            this.unumSoon = new System.Windows.Forms.Label();
            this.knumLate = new System.Windows.Forms.Label();
            this.dnumLate = new System.Windows.Forms.Label();
            this.numLate = new System.Windows.Forms.NumericUpDown();
            this.unumLate = new System.Windows.Forms.Label();
            this.knumReq = new System.Windows.Forms.Label();
            this.dnumReq = new System.Windows.Forms.Label();
            this.numReq = new System.Windows.Forms.NumericUpDown();
            this.unumReq = new System.Windows.Forms.Label();
            this.knumPkg = new System.Windows.Forms.Label();
            this.dnumPkg = new System.Windows.Forms.Label();
            this.numPkg = new System.Windows.Forms.NumericUpDown();
            this.unumPkg = new System.Windows.Forms.Label();
            this.rpGen = new TexnikaReyestri.Controls.RoundPanel();
            this.lblGenTitle = new System.Windows.Forms.Label();
            this.knumKeep = new System.Windows.Forms.Label();
            this.dnumKeep = new System.Windows.Forms.Label();
            this.numKeep = new System.Windows.Forms.NumericUpDown();
            this.unumKeep = new System.Windows.Forms.Label();
            this.chkAnim = new System.Windows.Forms.CheckBox();
            this.dAnim = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numSoon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPkg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKeep)).BeginInit();
            this.pnlHead.SuspendLayout();
            this.tlp.SuspendLayout();
            this.rpNoti.SuspendLayout();
            this.rpGen.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHead
            // 
            this.pnlHead.Controls.Add(this.btnSave);
            this.pnlHead.Controls.Add(this.lblTitle);
            this.pnlHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHead.Size = new System.Drawing.Size(914, 44);
            this.pnlHead.Name = "pnlHead";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 18.0F);
            this.lblTitle.Location = new System.Drawing.Point(0, 4);
            this.lblTitle.Text = "Sistem";
            this.lblTitle.Tag = "title";
            this.lblTitle.Name = "lblTitle";
            // 
            // btnSave
            // 
            this.btnSave.Kind = TexnikaReyestri.Controls.ButtonKind.Primary;
            this.btnSave.Location = new System.Drawing.Point(778, 4);
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
            this.tlp.Controls.Add(this.rpNoti, 0, 0);
            this.tlp.Controls.Add(this.rpGen, 1, 0);
            this.tlp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp.Size = new System.Drawing.Size(914, 624);
            this.tlp.Name = "tlp";
            // 
            // rpNoti
            // 
            this.rpNoti.Controls.Add(this.unumPkg);
            this.rpNoti.Controls.Add(this.numPkg);
            this.rpNoti.Controls.Add(this.dnumPkg);
            this.rpNoti.Controls.Add(this.knumPkg);
            this.rpNoti.Controls.Add(this.unumReq);
            this.rpNoti.Controls.Add(this.numReq);
            this.rpNoti.Controls.Add(this.dnumReq);
            this.rpNoti.Controls.Add(this.knumReq);
            this.rpNoti.Controls.Add(this.unumLate);
            this.rpNoti.Controls.Add(this.numLate);
            this.rpNoti.Controls.Add(this.dnumLate);
            this.rpNoti.Controls.Add(this.knumLate);
            this.rpNoti.Controls.Add(this.unumSoon);
            this.rpNoti.Controls.Add(this.numSoon);
            this.rpNoti.Controls.Add(this.dnumSoon);
            this.rpNoti.Controls.Add(this.knumSoon);
            this.rpNoti.Controls.Add(this.lblNotiTitle);
            this.rpNoti.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpNoti.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpNoti.Size = new System.Drawing.Size(447, 614);
            this.rpNoti.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.rpNoti.Name = "rpNoti";
            // 
            // lblNotiTitle
            // 
            this.lblNotiTitle.AutoSize = false;
            this.lblNotiTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblNotiTitle.Location = new System.Drawing.Point(0, 0);
            this.lblNotiTitle.Text = "Bildiriş qaydaları";
            this.lblNotiTitle.Tag = "muted";
            this.lblNotiTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNotiTitle.Size = new System.Drawing.Size(419, 26);
            this.lblNotiTitle.Name = "lblNotiTitle";
            // 
            // knumSoon
            // 
            this.knumSoon.AutoSize = true;
            this.knumSoon.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.knumSoon.Location = new System.Drawing.Point(14, 44);
            this.knumSoon.Text = "İcra müddətinə xəbərdarlıq";
            this.knumSoon.Name = "knumSoon";
            // 
            // dnumSoon
            // 
            this.dnumSoon.AutoSize = true;
            this.dnumSoon.Font = new System.Drawing.Font("Segoe UI", 8.0F);
            this.dnumSoon.Location = new System.Drawing.Point(14, 62);
            this.dnumSoon.Text = "Bitməyə neçə gün qalmış (kart və bildiriş)";
            this.dnumSoon.Tag = "muted";
            this.dnumSoon.Name = "dnumSoon";
            // 
            // numSoon
            // 
            this.numSoon.Location = new System.Drawing.Point(317, 48);
            this.numSoon.Size = new System.Drawing.Size(70, 25);
            this.numSoon.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.numSoon.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            this.numSoon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numSoon.Name = "numSoon";
            // 
            // unumSoon
            // 
            this.unumSoon.AutoSize = true;
            this.unumSoon.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.unumSoon.Location = new System.Drawing.Point(393, 52);
            this.unumSoon.Text = "gün";
            this.unumSoon.Tag = "muted";
            this.unumSoon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.unumSoon.Name = "unumSoon";
            // 
            // knumLate
            // 
            this.knumLate.AutoSize = true;
            this.knumLate.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.knumLate.Location = new System.Drawing.Point(14, 100);
            this.knumLate.Text = "\"Təmir gecikir\"";
            this.knumLate.Name = "knumLate";
            // 
            // dnumLate
            // 
            this.dnumLate.AutoSize = true;
            this.dnumLate.Font = new System.Drawing.Font("Segoe UI", 8.0F);
            this.dnumLate.Location = new System.Drawing.Point(14, 118);
            this.dnumLate.Text = "Son tarixdən neçə gün sonra";
            this.dnumLate.Tag = "muted";
            this.dnumLate.Name = "dnumLate";
            // 
            // numLate
            // 
            this.numLate.Location = new System.Drawing.Point(317, 104);
            this.numLate.Size = new System.Drawing.Size(70, 25);
            this.numLate.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.numLate.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            this.numLate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numLate.Name = "numLate";
            // 
            // unumLate
            // 
            this.unumLate.AutoSize = true;
            this.unumLate.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.unumLate.Location = new System.Drawing.Point(393, 108);
            this.unumLate.Text = "gün";
            this.unumLate.Tag = "muted";
            this.unumLate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.unumLate.Name = "unumLate";
            // 
            // knumReq
            // 
            this.knumReq.AutoSize = true;
            this.knumReq.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.knumReq.Location = new System.Drawing.Point(14, 156);
            this.knumReq.Text = "\"Sorğu cavabsızdır\"";
            this.knumReq.Name = "knumReq";
            // 
            // dnumReq
            // 
            this.dnumReq.AutoSize = true;
            this.dnumReq.Font = new System.Drawing.Font("Segoe UI", 8.0F);
            this.dnumReq.Location = new System.Drawing.Point(14, 174);
            this.dnumReq.Text = "Hissə sorğusu neçə gün cavabsız qalanda";
            this.dnumReq.Tag = "muted";
            this.dnumReq.Name = "dnumReq";
            // 
            // numReq
            // 
            this.numReq.Location = new System.Drawing.Point(317, 160);
            this.numReq.Size = new System.Drawing.Size(70, 25);
            this.numReq.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.numReq.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            this.numReq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numReq.Name = "numReq";
            // 
            // unumReq
            // 
            this.unumReq.AutoSize = true;
            this.unumReq.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.unumReq.Location = new System.Drawing.Point(393, 164);
            this.unumReq.Text = "gün";
            this.unumReq.Tag = "muted";
            this.unumReq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.unumReq.Name = "unumReq";
            // 
            // knumPkg
            // 
            this.knumPkg.AutoSize = true;
            this.knumPkg.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.knumPkg.Location = new System.Drawing.Point(14, 212);
            this.knumPkg.Text = "\"Paket gəlməyib\"";
            this.knumPkg.Name = "knumPkg";
            // 
            // dnumPkg
            // 
            this.dnumPkg.AutoSize = true;
            this.dnumPkg.Font = new System.Drawing.Font("Segoe UI", 8.0F);
            this.dnumPkg.Location = new System.Drawing.Point(14, 230);
            this.dnumPkg.Text = "Zavod neçə gün ixrac etməyəndə";
            this.dnumPkg.Tag = "muted";
            this.dnumPkg.Name = "dnumPkg";
            // 
            // numPkg
            // 
            this.numPkg.Location = new System.Drawing.Point(317, 216);
            this.numPkg.Size = new System.Drawing.Size(70, 25);
            this.numPkg.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.numPkg.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            this.numPkg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numPkg.Name = "numPkg";
            // 
            // unumPkg
            // 
            this.unumPkg.AutoSize = true;
            this.unumPkg.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.unumPkg.Location = new System.Drawing.Point(393, 220);
            this.unumPkg.Text = "gün";
            this.unumPkg.Tag = "muted";
            this.unumPkg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.unumPkg.Name = "unumPkg";
            // 
            // rpGen
            // 
            this.rpGen.Controls.Add(this.dAnim);
            this.rpGen.Controls.Add(this.chkAnim);
            this.rpGen.Controls.Add(this.unumKeep);
            this.rpGen.Controls.Add(this.numKeep);
            this.rpGen.Controls.Add(this.dnumKeep);
            this.rpGen.Controls.Add(this.knumKeep);
            this.rpGen.Controls.Add(this.lblGenTitle);
            this.rpGen.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpGen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpGen.Size = new System.Drawing.Size(447, 614);
            this.rpGen.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.rpGen.Name = "rpGen";
            // 
            // lblGenTitle
            // 
            this.lblGenTitle.AutoSize = false;
            this.lblGenTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblGenTitle.Location = new System.Drawing.Point(0, 0);
            this.lblGenTitle.Text = "Ümumi";
            this.lblGenTitle.Tag = "muted";
            this.lblGenTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGenTitle.Size = new System.Drawing.Size(419, 26);
            this.lblGenTitle.Name = "lblGenTitle";
            // 
            // knumKeep
            // 
            this.knumKeep.AutoSize = true;
            this.knumKeep.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.knumKeep.Location = new System.Drawing.Point(14, 44);
            this.knumKeep.Text = "Saxlanılan ehtiyat nüsxə sayı";
            this.knumKeep.Name = "knumKeep";
            // 
            // dnumKeep
            // 
            this.dnumKeep.AutoSize = true;
            this.dnumKeep.Font = new System.Drawing.Font("Segoe UI", 8.0F);
            this.dnumKeep.Location = new System.Drawing.Point(14, 62);
            this.dnumKeep.Text = "Köhnələri avtomatik silinir";
            this.dnumKeep.Tag = "muted";
            this.dnumKeep.Name = "dnumKeep";
            // 
            // numKeep
            // 
            this.numKeep.Location = new System.Drawing.Point(317, 48);
            this.numKeep.Size = new System.Drawing.Size(70, 25);
            this.numKeep.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.numKeep.Maximum = new decimal(new int[] { 365, 0, 0, 0 });
            this.numKeep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numKeep.Name = "numKeep";
            // 
            // unumKeep
            // 
            this.unumKeep.AutoSize = true;
            this.unumKeep.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.unumKeep.Location = new System.Drawing.Point(393, 52);
            this.unumKeep.Text = "nüsxə";
            this.unumKeep.Tag = "muted";
            this.unumKeep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.unumKeep.Name = "unumKeep";
            // 
            // chkAnim
            // 
            this.chkAnim.AutoSize = true;
            this.chkAnim.Text = "Panel keçid animasiyaları";
            this.chkAnim.Location = new System.Drawing.Point(14, 110);
            this.chkAnim.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.chkAnim.Name = "chkAnim";
            // 
            // dAnim
            // 
            this.dAnim.AutoSize = true;
            this.dAnim.Font = new System.Drawing.Font("Segoe UI", 8.0F);
            this.dAnim.Location = new System.Drawing.Point(32, 132);
            this.dAnim.Text = "Zəif kompüterlərdə söndürün";
            this.dAnim.Tag = "muted";
            this.dAnim.Name = "dAnim";
            // 
            // AdmSystemPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(958, 698);
            this.Controls.Add(this.tlp);
            this.Controls.Add(this.pnlHead);
            this.Name = "AdmSystemPage";
            ((System.ComponentModel.ISupportInitialize)(this.numSoon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPkg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKeep)).EndInit();
            this.rpGen.ResumeLayout(false);
            this.rpGen.PerformLayout();
            this.rpNoti.ResumeLayout(false);
            this.rpNoti.PerformLayout();
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
        private TexnikaReyestri.Controls.RoundPanel rpNoti;
        private System.Windows.Forms.Label lblNotiTitle;
        private System.Windows.Forms.Label knumSoon;
        private System.Windows.Forms.Label dnumSoon;
        private System.Windows.Forms.NumericUpDown numSoon;
        private System.Windows.Forms.Label unumSoon;
        private System.Windows.Forms.Label knumLate;
        private System.Windows.Forms.Label dnumLate;
        private System.Windows.Forms.NumericUpDown numLate;
        private System.Windows.Forms.Label unumLate;
        private System.Windows.Forms.Label knumReq;
        private System.Windows.Forms.Label dnumReq;
        private System.Windows.Forms.NumericUpDown numReq;
        private System.Windows.Forms.Label unumReq;
        private System.Windows.Forms.Label knumPkg;
        private System.Windows.Forms.Label dnumPkg;
        private System.Windows.Forms.NumericUpDown numPkg;
        private System.Windows.Forms.Label unumPkg;
        private TexnikaReyestri.Controls.RoundPanel rpGen;
        private System.Windows.Forms.Label lblGenTitle;
        private System.Windows.Forms.Label knumKeep;
        private System.Windows.Forms.Label dnumKeep;
        private System.Windows.Forms.NumericUpDown numKeep;
        private System.Windows.Forms.Label unumKeep;
        private System.Windows.Forms.CheckBox chkAnim;
        private System.Windows.Forms.Label dAnim;
    }
}
