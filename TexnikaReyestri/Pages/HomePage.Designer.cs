namespace TexnikaReyestri.Pages
{
    partial class HomePage
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
            this.segPeriod = new TexnikaReyestri.Controls.SegmentControl();
            this.tlpTiles = new TexnikaReyestri.Controls.KartPaneli();
            this.tlpBody = new System.Windows.Forms.TableLayoutPanel();
            this.rpMonthly = new TexnikaReyestri.Controls.RoundPanel();
            this.lblMonthlyTitle = new System.Windows.Forms.Label();
            this.lblLegA = new System.Windows.Forms.Label();
            this.lblLegB = new System.Windows.Forms.Label();
            this.chartMonthly = new TexnikaReyestri.Controls.ColumnChart();
            this.rpAvg = new TexnikaReyestri.Controls.RoundPanel();
            this.lblAvgTitle = new System.Windows.Forms.Label();
            this.gridZav = new TexnikaReyestri.Controls.ModernGrid();
            this.colZavZavod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colZavDavam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colZavGozl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colZavTehvil = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colZavCemi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridZav)).BeginInit();
            this.pnlHead.SuspendLayout();
            this.tlpTiles.SuspendLayout();
            this.tlpBody.SuspendLayout();
            this.rpMonthly.SuspendLayout();
            this.rpAvg.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHead
            // 
            this.pnlHead.Controls.Add(this.segPeriod);
            this.pnlHead.Controls.Add(this.lblTitle);
            this.pnlHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHead.Size = new System.Drawing.Size(1172, 44);
            this.pnlHead.Name = "pnlHead";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 18.0F);
            this.lblTitle.Location = new System.Drawing.Point(0, 4);
            this.lblTitle.Text = "Ana səhifə · Zavod 3";
            this.lblTitle.Tag = "title";
            this.lblTitle.Name = "lblTitle";
            // 
            // segPeriod
            // 
            this.segPeriod.Location = new System.Drawing.Point(906, 6);
            this.segPeriod.Size = new System.Drawing.Size(260, 30);
            this.segPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.segPeriod.Items = new string[] {
        "30 gün",
        "3 ay",
        "6 ay",
        "1 il"};
            this.segPeriod.SelectedIndex = 2;
            this.segPeriod.Name = "segPeriod";
            this.segPeriod.SelectedIndexChanged += new System.EventHandler(this.segPeriod_SelectedIndexChanged);
            // 
            // tlpTiles
            // 
            this.tlpTiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpTiles.Name = "tlpTiles";
            this.tlpTiles.KartClicked += new System.EventHandler<string>(this.kart_Clicked);
            // 
            // tlpBody
            // 
            this.tlpBody.ColumnCount = 3;
            this.tlpBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tlpBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tlpBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tlpBody.RowCount = 3;
            this.tlpBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.0F));
            this.tlpBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.0F));
            this.tlpBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.0F));
            this.tlpBody.Controls.Add(this.rpMonthly, 0, 0);
            this.tlpBody.Controls.Add(this.rpAvg, 1, 1);
            this.tlpBody.SetColumnSpan(this.rpMonthly, 2);
            this.tlpBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBody.Size = new System.Drawing.Size(1172, 520);
            this.tlpBody.Name = "tlpBody";
            // 
            // rpMonthly
            // 
            this.rpMonthly.Controls.Add(this.chartMonthly);
            this.rpMonthly.Controls.Add(this.lblLegB);
            this.rpMonthly.Controls.Add(this.lblLegA);
            this.rpMonthly.Controls.Add(this.lblMonthlyTitle);
            this.rpMonthly.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpMonthly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpMonthly.Size = new System.Drawing.Size(380, 208);
            this.rpMonthly.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.rpMonthly.Name = "rpMonthly";
            // 
            // lblMonthlyTitle
            // 
            this.lblMonthlyTitle.AutoSize = false;
            this.lblMonthlyTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblMonthlyTitle.Location = new System.Drawing.Point(0, 0);
            this.lblMonthlyTitle.Text = "Qəbul və təhvil, aylar üzrə";
            this.lblMonthlyTitle.Tag = "muted";
            this.lblMonthlyTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMonthlyTitle.Size = new System.Drawing.Size(352, 26);
            this.lblMonthlyTitle.Name = "lblMonthlyTitle";
            // 
            // lblLegA
            // 
            this.lblLegA.AutoSize = true;
            this.lblLegA.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblLegA.Location = new System.Drawing.Point(226, 12);
            this.lblLegA.Text = "■ Qəbul edilib";
            this.lblLegA.Tag = "muted";
            this.lblLegA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLegA.Name = "lblLegA";
            // 
            // lblLegB
            // 
            this.lblLegB.AutoSize = true;
            this.lblLegB.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblLegB.Location = new System.Drawing.Point(326, 12);
            this.lblLegB.Text = "■ Təhvil verilib";
            this.lblLegB.Tag = "muted";
            this.lblLegB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLegB.Name = "lblLegB";
            // 
            // chartMonthly
            // 
            this.chartMonthly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartMonthly.Size = new System.Drawing.Size(352, 162);
            this.chartMonthly.Name = "chartMonthly";
            // 
            // rpAvg
            // 
            this.rpAvg.Controls.Add(this.gridZav);
            this.rpAvg.Controls.Add(this.lblAvgTitle);
            this.rpAvg.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpAvg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpAvg.Size = new System.Drawing.Size(380, 146);
            this.rpAvg.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.rpAvg.Name = "rpAvg";
            // 
            // lblAvgTitle
            // 
            this.lblAvgTitle.AutoSize = false;
            this.lblAvgTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblAvgTitle.Location = new System.Drawing.Point(0, 0);
            this.lblAvgTitle.Text = "Zavodlar üzrə icra vəziyyəti";
            this.lblAvgTitle.Tag = "muted";
            this.lblAvgTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAvgTitle.Size = new System.Drawing.Size(352, 26);
            this.lblAvgTitle.Name = "lblAvgTitle";
            // 
            // gridZav
            // 
            this.gridZav.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colZavZavod,
            this.colZavDavam,
            this.colZavGozl,
            this.colZavTehvil,
            this.colZavCemi});
            this.gridZav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridZav.Size = new System.Drawing.Size(352, 100);
            this.gridZav.Name = "gridZav";
            this.gridZav.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridZav_CellClick);
            // 
            // colZavZavod
            // 
            this.colZavZavod.HeaderText = "Zavod";
            this.colZavZavod.Name = "colZavZavod";
            this.colZavZavod.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colZavZavod.MinimumWidth = 100;
            // 
            // colZavDavam
            // 
            this.colZavDavam.HeaderText = "Təmiri davam edir";
            this.colZavDavam.Name = "colZavDavam";
            this.colZavDavam.Width = 90;
            // 
            // colZavGozl
            // 
            this.colZavGozl.HeaderText = "Təhvil gözlənilir";
            this.colZavGozl.Name = "colZavGozl";
            this.colZavGozl.Width = 90;
            // 
            // colZavTehvil
            // 
            this.colZavTehvil.HeaderText = "Təhvil verilib";
            this.colZavTehvil.Name = "colZavTehvil";
            this.colZavTehvil.Width = 80;
            // 
            // colZavCemi
            // 
            this.colZavCemi.HeaderText = "Cəmi";
            this.colZavCemi.Name = "colZavCemi";
            this.colZavCemi.Width = 70;
            // 
            // HomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(1216, 712);
            this.Controls.Add(this.tlpBody);
            this.Controls.Add(this.tlpTiles);
            this.Controls.Add(this.pnlHead);
            this.Name = "HomePage";
            ((System.ComponentModel.ISupportInitialize)(this.gridZav)).EndInit();
            this.rpAvg.ResumeLayout(false);
            this.rpAvg.PerformLayout();
            this.rpMonthly.ResumeLayout(false);
            this.rpMonthly.PerformLayout();
            this.tlpBody.ResumeLayout(false);
            this.tlpBody.PerformLayout();
            this.tlpTiles.ResumeLayout(false);
            this.tlpTiles.PerformLayout();
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Label lblTitle;
        private TexnikaReyestri.Controls.SegmentControl segPeriod;
        private TexnikaReyestri.Controls.KartPaneli tlpTiles;
        private System.Windows.Forms.TableLayoutPanel tlpBody;
        private TexnikaReyestri.Controls.RoundPanel rpMonthly;
        private System.Windows.Forms.Label lblMonthlyTitle;
        private System.Windows.Forms.Label lblLegA;
        private System.Windows.Forms.Label lblLegB;
        private TexnikaReyestri.Controls.ColumnChart chartMonthly;
        private TexnikaReyestri.Controls.RoundPanel rpAvg;
        private System.Windows.Forms.Label lblAvgTitle;
        private TexnikaReyestri.Controls.ModernGrid gridZav;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZavZavod;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZavDavam;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZavGozl;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZavTehvil;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZavCemi;
    }
}
