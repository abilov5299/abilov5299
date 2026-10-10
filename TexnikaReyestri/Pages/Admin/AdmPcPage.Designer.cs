namespace TexnikaReyestri.Pages.Admin
{
    partial class AdmPcPage
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
            this.tlp = new System.Windows.Forms.TableLayoutPanel();
            this.rpInfo = new TexnikaReyestri.Controls.RoundPanel();
            this.lblInfoTitle = new System.Windows.Forms.Label();
            this.kQurum = new System.Windows.Forms.Label();
            this.vQurum = new System.Windows.Forms.Label();
            this.kDb = new System.Windows.Forms.Label();
            this.vDb = new System.Windows.Forms.Label();
            this.kSize = new System.Windows.Forms.Label();
            this.vSize = new System.Windows.Forms.Label();
            this.kCount = new System.Windows.Forms.Label();
            this.vCount = new System.Windows.Forms.Label();
            this.kApp = new System.Windows.Forms.Label();
            this.vApp = new System.Windows.Forms.Label();
            this.lblWarn = new System.Windows.Forms.Label();
            this.rpMaint = new TexnikaReyestri.Controls.RoundPanel();
            this.lblMaintTitle = new System.Windows.Forms.Label();
            this.btnCheck = new TexnikaReyestri.Controls.FlatButton();
            this.btnVacuum = new TexnikaReyestri.Controls.FlatButton();
            this.lblMaint = new System.Windows.Forms.Label();
            this.lblDemo = new System.Windows.Forms.Label();
            this.lblDemoDesc = new System.Windows.Forms.Label();
            this.numDemo = new System.Windows.Forms.NumericUpDown();
            this.btnDemo = new TexnikaReyestri.Controls.FlatButton();
            ((System.ComponentModel.ISupportInitialize)(this.numDemo)).BeginInit();
            this.pnlHead.SuspendLayout();
            this.tlp.SuspendLayout();
            this.rpInfo.SuspendLayout();
            this.rpMaint.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHead
            // 
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
            this.lblTitle.Text = "Bu kompüter";
            this.lblTitle.Tag = "title";
            this.lblTitle.Name = "lblTitle";
            // 
            // tlp
            // 
            this.tlp.ColumnCount = 2;
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0F));
            this.tlp.RowCount = 1;
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0F));
            this.tlp.Controls.Add(this.rpInfo, 0, 0);
            this.tlp.Controls.Add(this.rpMaint, 1, 0);
            this.tlp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp.Name = "tlp";
            // 
            // rpInfo
            // 
            this.rpInfo.Controls.Add(this.lblWarn);
            this.rpInfo.Controls.Add(this.vApp);
            this.rpInfo.Controls.Add(this.kApp);
            this.rpInfo.Controls.Add(this.vCount);
            this.rpInfo.Controls.Add(this.kCount);
            this.rpInfo.Controls.Add(this.vSize);
            this.rpInfo.Controls.Add(this.kSize);
            this.rpInfo.Controls.Add(this.vDb);
            this.rpInfo.Controls.Add(this.kDb);
            this.rpInfo.Controls.Add(this.vQurum);
            this.rpInfo.Controls.Add(this.kQurum);
            this.rpInfo.Controls.Add(this.lblInfoTitle);
            this.rpInfo.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpInfo.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.rpInfo.Name = "rpInfo";
            // 
            // lblInfoTitle
            // 
            this.lblInfoTitle.AutoSize = false;
            this.lblInfoTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblInfoTitle.Location = new System.Drawing.Point(0, 0);
            this.lblInfoTitle.Text = "Quraşdırma";
            this.lblInfoTitle.Tag = "muted";
            this.lblInfoTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInfoTitle.Height = 26;
            this.lblInfoTitle.Name = "lblInfoTitle";
            // 
            // kQurum
            // 
            this.kQurum.AutoSize = true;
            this.kQurum.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.kQurum.Location = new System.Drawing.Point(14, 44);
            this.kQurum.Text = "Bu kompüterin qurumu";
            this.kQurum.Tag = "muted";
            this.kQurum.Name = "kQurum";
            // 
            // vQurum
            // 
            this.vQurum.AutoSize = true;
            this.vQurum.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.vQurum.Location = new System.Drawing.Point(170, 44);
            this.vQurum.Text = "—";
            this.vQurum.MaximumSize = new System.Drawing.Size(270, 0);
            this.vQurum.Name = "vQurum";
            // 
            // kDb
            // 
            this.kDb.AutoSize = true;
            this.kDb.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.kDb.Location = new System.Drawing.Point(14, 74);
            this.kDb.Text = "Baza faylı";
            this.kDb.Tag = "muted";
            this.kDb.Name = "kDb";
            // 
            // vDb
            // 
            this.vDb.AutoSize = true;
            this.vDb.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.vDb.Location = new System.Drawing.Point(170, 74);
            this.vDb.Text = "—";
            this.vDb.MaximumSize = new System.Drawing.Size(270, 0);
            this.vDb.Name = "vDb";
            // 
            // kSize
            // 
            this.kSize.AutoSize = true;
            this.kSize.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.kSize.Location = new System.Drawing.Point(14, 104);
            this.kSize.Text = "Ölçü";
            this.kSize.Tag = "muted";
            this.kSize.Name = "kSize";
            // 
            // vSize
            // 
            this.vSize.AutoSize = true;
            this.vSize.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.vSize.Location = new System.Drawing.Point(170, 104);
            this.vSize.Text = "—";
            this.vSize.MaximumSize = new System.Drawing.Size(270, 0);
            this.vSize.Name = "vSize";
            // 
            // kCount
            // 
            this.kCount.AutoSize = true;
            this.kCount.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.kCount.Location = new System.Drawing.Point(14, 134);
            this.kCount.Text = "Texnika sayı";
            this.kCount.Tag = "muted";
            this.kCount.Name = "kCount";
            // 
            // vCount
            // 
            this.vCount.AutoSize = true;
            this.vCount.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.vCount.Location = new System.Drawing.Point(170, 134);
            this.vCount.Text = "—";
            this.vCount.MaximumSize = new System.Drawing.Size(270, 0);
            this.vCount.Name = "vCount";
            // 
            // kApp
            // 
            this.kApp.AutoSize = true;
            this.kApp.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.kApp.Location = new System.Drawing.Point(14, 164);
            this.kApp.Text = "Proqram versiyası";
            this.kApp.Tag = "muted";
            this.kApp.Name = "kApp";
            // 
            // vApp
            // 
            this.vApp.AutoSize = true;
            this.vApp.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.vApp.Location = new System.Drawing.Point(170, 164);
            this.vApp.Text = "—";
            this.vApp.MaximumSize = new System.Drawing.Size(270, 0);
            this.vApp.Name = "vApp";
            // 
            // lblWarn
            // 
            this.lblWarn.AutoSize = true;
            this.lblWarn.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblWarn.Location = new System.Drawing.Point(14, 210);
            this.lblWarn.Text = "Hər kompüterin öz bazası var və o, bir quruma bağlıdır. Qurumu dəyişmək üçün proqramı yenidən quraşdırın (baza sıfırlanır).";
            this.lblWarn.MaximumSize = new System.Drawing.Size(420, 0);
            this.lblWarn.BackColor = System.Drawing.Color.FromArgb(255, 244, 229);
            this.lblWarn.ForeColor = System.Drawing.Color.FromArgb(138, 75, 6);
            this.lblWarn.Padding = new System.Windows.Forms.Padding(6);
            this.lblWarn.Name = "lblWarn";
            // 
            // rpMaint
            // 
            this.rpMaint.Controls.Add(this.btnDemo);
            this.rpMaint.Controls.Add(this.numDemo);
            this.rpMaint.Controls.Add(this.lblDemoDesc);
            this.rpMaint.Controls.Add(this.lblDemo);
            this.rpMaint.Controls.Add(this.lblMaint);
            this.rpMaint.Controls.Add(this.btnVacuum);
            this.rpMaint.Controls.Add(this.btnCheck);
            this.rpMaint.Controls.Add(this.lblMaintTitle);
            this.rpMaint.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpMaint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpMaint.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.rpMaint.Name = "rpMaint";
            // 
            // lblMaintTitle
            // 
            this.lblMaintTitle.AutoSize = false;
            this.lblMaintTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblMaintTitle.Location = new System.Drawing.Point(0, 0);
            this.lblMaintTitle.Text = "Baza xidməti";
            this.lblMaintTitle.Tag = "muted";
            this.lblMaintTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMaintTitle.Height = 26;
            this.lblMaintTitle.Name = "lblMaintTitle";
            // 
            // btnCheck
            // 
            this.btnCheck.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            this.btnCheck.Location = new System.Drawing.Point(14, 44);
            this.btnCheck.Size = new System.Drawing.Size(180, 36);
            this.btnCheck.Text = "Bütövlüyü yoxla";
            this.btnCheck.Glyph = "\uE73E";
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // btnVacuum
            // 
            this.btnVacuum.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            this.btnVacuum.Location = new System.Drawing.Point(200, 44);
            this.btnVacuum.Size = new System.Drawing.Size(160, 36);
            this.btnVacuum.Text = "Bazanı sıx";
            this.btnVacuum.Name = "btnVacuum";
            this.btnVacuum.Click += new System.EventHandler(this.btnVacuum_Click);
            // 
            // lblMaint
            // 
            this.lblMaint.AutoSize = true;
            this.lblMaint.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblMaint.Location = new System.Drawing.Point(14, 90);
            this.lblMaint.Text = "Bu əməliyyatlar ayda bir dəfə və ya proqram yavaşlayanda edilir.";
            this.lblMaint.Tag = "muted";
            this.lblMaint.MaximumSize = new System.Drawing.Size(420, 0);
            this.lblMaint.Name = "lblMaint";
            // 
            // lblDemo
            // 
            this.lblDemo.AutoSize = true;
            this.lblDemo.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblDemo.Location = new System.Drawing.Point(14, 140);
            this.lblDemo.Text = "Sürət sınağı";
            this.lblDemo.Tag = "muted";
            this.lblDemo.Name = "lblDemo";
            // 
            // lblDemoDesc
            // 
            this.lblDemoDesc.AutoSize = true;
            this.lblDemoDesc.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblDemoDesc.Location = new System.Drawing.Point(14, 166);
            this.lblDemoDesc.Text = "Nümunə məlumat yaradır (zavod nömrəsi \"NÜMUNƏ-\" ilə başlayır). Yalnız sınaq kompüterində istifadə edin.";
            this.lblDemoDesc.Tag = "muted";
            this.lblDemoDesc.MaximumSize = new System.Drawing.Size(420, 0);
            this.lblDemoDesc.Name = "lblDemoDesc";
            // 
            // numDemo
            // 
            this.numDemo.Location = new System.Drawing.Point(14, 212);
            this.numDemo.Size = new System.Drawing.Size(110, 25);
            this.numDemo.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.numDemo.Maximum = new decimal(new int[] { 2000000, 0, 0, 0 });
            this.numDemo.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            this.numDemo.Increment = new decimal(new int[] { 10000, 0, 0, 0 });
            this.numDemo.Value = new decimal(new int[] { 100000, 0, 0, 0 });
            this.numDemo.Name = "numDemo";
            // 
            // btnDemo
            // 
            this.btnDemo.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            this.btnDemo.Location = new System.Drawing.Point(132, 208);
            this.btnDemo.Size = new System.Drawing.Size(210, 34);
            this.btnDemo.Text = "Nümunə məlumat yarat";
            this.btnDemo.Name = "btnDemo";
            this.btnDemo.Click += new System.EventHandler(this.btnDemo_Click);
            // 
            // AdmPcPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(958, 698);
            this.Controls.Add(this.tlp);
            this.Controls.Add(this.pnlHead);
            this.Name = "AdmPcPage";
            ((System.ComponentModel.ISupportInitialize)(this.numDemo)).EndInit();
            this.rpMaint.ResumeLayout(false);
            this.rpMaint.PerformLayout();
            this.rpInfo.ResumeLayout(false);
            this.rpInfo.PerformLayout();
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
        private System.Windows.Forms.TableLayoutPanel tlp;
        private TexnikaReyestri.Controls.RoundPanel rpInfo;
        private System.Windows.Forms.Label lblInfoTitle;
        private System.Windows.Forms.Label kQurum;
        private System.Windows.Forms.Label vQurum;
        private System.Windows.Forms.Label kDb;
        private System.Windows.Forms.Label vDb;
        private System.Windows.Forms.Label kSize;
        private System.Windows.Forms.Label vSize;
        private System.Windows.Forms.Label kCount;
        private System.Windows.Forms.Label vCount;
        private System.Windows.Forms.Label kApp;
        private System.Windows.Forms.Label vApp;
        private System.Windows.Forms.Label lblWarn;
        private TexnikaReyestri.Controls.RoundPanel rpMaint;
        private System.Windows.Forms.Label lblMaintTitle;
        private TexnikaReyestri.Controls.FlatButton btnCheck;
        private TexnikaReyestri.Controls.FlatButton btnVacuum;
        private System.Windows.Forms.Label lblMaint;
        private System.Windows.Forms.Label lblDemo;
        private System.Windows.Forms.Label lblDemoDesc;
        private System.Windows.Forms.NumericUpDown numDemo;
        private TexnikaReyestri.Controls.FlatButton btnDemo;
    }
}
