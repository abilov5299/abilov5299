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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            pnlHead = new Panel();
            segPeriod = new TexnikaReyestri.Controls.SegmentControl();
            lblTitle = new Label();
            tlpTiles = new TableLayoutPanel();
            cardTotal = new TexnikaReyestri.Controls.StatCard();
            cardRepair = new TexnikaReyestri.Controls.StatCard();
            cardLate = new TexnikaReyestri.Controls.StatCard();
            cardAvg = new TexnikaReyestri.Controls.StatCard();
            tlpBody = new TableLayoutPanel();
            rpMonthly = new TexnikaReyestri.Controls.RoundPanel();
            chartMonthly = new TexnikaReyestri.Controls.ColumnChart();
            lblLegB = new Label();
            lblLegA = new Label();
            lblMonthlyTitle = new Label();
            rpStatus = new TexnikaReyestri.Controls.RoundPanel();
            brkStatus = new TexnikaReyestri.Controls.StatusBreakdown();
            lblStatusTitle = new Label();
            rpTypes = new TexnikaReyestri.Controls.RoundPanel();
            barTypes = new TexnikaReyestri.Controls.BarList();
            lblTypesTitle = new Label();
            rpAvg = new TexnikaReyestri.Controls.RoundPanel();
            barAvg = new TexnikaReyestri.Controls.BarList();
            lblAvgTitle = new Label();
            rpLate = new TexnikaReyestri.Controls.RoundPanel();
            gridLate = new TexnikaReyestri.Controls.ModernGrid();
            colLateName = new DataGridViewTextBoxColumn();
            colLateDays = new DataGridViewTextBoxColumn();
            lnkLate = new LinkLabel();
            lblLateTitle = new Label();
            rpEvents = new TexnikaReyestri.Controls.RoundPanel();
            gridEvents = new TexnikaReyestri.Controls.ModernGrid();
            colEvTime = new DataGridViewTextBoxColumn();
            colEvType = new DataGridViewTextBoxColumn();
            colEvText = new DataGridViewTextBoxColumn();
            lnkLogs = new LinkLabel();
            lblEventsTitle = new Label();
            lblFoot = new Label();
            pnlHead.SuspendLayout();
            tlpTiles.SuspendLayout();
            tlpBody.SuspendLayout();
            rpMonthly.SuspendLayout();
            rpStatus.SuspendLayout();
            rpTypes.SuspendLayout();
            rpAvg.SuspendLayout();
            rpLate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridLate).BeginInit();
            rpEvents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridEvents).BeginInit();
            SuspendLayout();
            // 
            // pnlHead
            // 
            pnlHead.Controls.Add(segPeriod);
            pnlHead.Controls.Add(lblTitle);
            pnlHead.Dock = DockStyle.Top;
            pnlHead.Location = new Point(22, 16);
            pnlHead.Name = "pnlHead";
            pnlHead.Size = new Size(1172, 44);
            pnlHead.TabIndex = 3;
            // 
            // segPeriod
            // 
            segPeriod.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            segPeriod.Font = new Font("Segoe UI", 8.5F);
            segPeriod.Items = new string[]
    {
    "30 gün",
    "3 ay",
    "6 ay",
    "1 il"
    };
            segPeriod.Location = new Point(906, 6);
            segPeriod.Name = "segPeriod";
            segPeriod.Size = new Size(260, 30);
            segPeriod.TabIndex = 0;
            segPeriod.SelectedIndexChanged += segPeriod_SelectedIndexChanged;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Bahnschrift SemiBold", 18F);
            lblTitle.Location = new Point(14, 6);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(226, 29);
            lblTitle.TabIndex = 1;
            lblTitle.Tag = "title";
            lblTitle.Text = "Ana səhifə · Zavod 3";
            // 
            // tlpTiles
            // 
            tlpTiles.ColumnCount = 4;
            tlpTiles.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpTiles.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpTiles.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpTiles.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpTiles.Controls.Add(cardTotal, 0, 0);
            tlpTiles.Controls.Add(cardRepair, 1, 0);
            tlpTiles.Controls.Add(cardLate, 2, 0);
            tlpTiles.Controls.Add(cardAvg, 3, 0);
            tlpTiles.Dock = DockStyle.Top;
            tlpTiles.Location = new Point(22, 60);
            tlpTiles.Name = "tlpTiles";
            tlpTiles.Padding = new Padding(0, 4, 0, 8);
            tlpTiles.RowCount = 1;
            tlpTiles.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpTiles.Size = new Size(1172, 96);
            tlpTiles.TabIndex = 2;
            // 
            // cardTotal
            // 
            this.cardTotal.Title = "Cəmi texnika";
            this.cardTotal.Value = "0";
            this.cardTotal.Glyph = "\uE8FD";
            this.cardTotal.Accent = TexnikaReyestri.Controls.AccentKind.Nav;
            this.cardTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardTotal.Size = new System.Drawing.Size(283, 96);
            this.cardTotal.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.cardTotal.FilterKey = "";
            this.cardTotal.Name = "cardTotal";
            this.cardTotal.Click += new System.EventHandler(this.card_Click);
            // 
            // cardRepair
            // 
            this.cardRepair.Title = "Təmirdə";
            this.cardRepair.Value = "0";
            this.cardRepair.Glyph = "\uE90F";
            this.cardRepair.Accent = TexnikaReyestri.Controls.AccentKind.Info;
            this.cardRepair.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardRepair.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.cardRepair.FilterKey = "Təmirdə";
            this.cardRepair.Name = "cardRepair";
            this.cardRepair.Click += new System.EventHandler(this.card_Click);
            // 
            // cardLate
            // 
            this.cardLate.Title = "Gecikən";
            this.cardLate.Value = "0";
            this.cardLate.Glyph = "\uE823";
            this.cardLate.Accent = TexnikaReyestri.Controls.AccentKind.Bad;
            this.cardLate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardLate.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.cardLate.FilterKey = "late";
            this.cardLate.Name = "cardLate";
            this.cardLate.Click += new System.EventHandler(this.card_Click);
            // 
            // cardAvg
            // 
            this.cardAvg.Title = "Orta təmir müddəti";
            this.cardAvg.Value = "0";
            this.cardAvg.Glyph = "\uE916";
            this.cardAvg.Accent = TexnikaReyestri.Controls.AccentKind.Ok;
            this.cardAvg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardAvg.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.cardAvg.FilterKey = "";
            this.cardAvg.Name = "cardAvg";
            this.cardAvg.Click += new System.EventHandler(this.card_Click);
            // 
            // tlpBody
            // 
            tlpBody.ColumnCount = 3;
            tlpBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            tlpBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tlpBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tlpBody.Controls.Add(rpMonthly, 0, 0);
            tlpBody.Controls.Add(rpStatus, 2, 0);
            tlpBody.Controls.Add(rpTypes, 0, 1);
            tlpBody.Controls.Add(rpAvg, 1, 1);
            tlpBody.Controls.Add(rpLate, 2, 1);
            tlpBody.Controls.Add(rpEvents, 0, 2);
            tlpBody.Dock = DockStyle.Fill;
            tlpBody.Location = new Point(22, 156);
            tlpBody.Name = "tlpBody";
            tlpBody.RowCount = 3;
            tlpBody.RowStyles.Add(new RowStyle(SizeType.Percent, 42F));
            tlpBody.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tlpBody.RowStyles.Add(new RowStyle(SizeType.Percent, 28F));
            tlpBody.Size = new Size(1172, 520);
            tlpBody.TabIndex = 0;
            // 
            // rpMonthly
            // 
            this.rpMonthly.Controls.Add(this.chartMonthly);
            this.rpMonthly.Controls.Add(this.lblLegB);
            this.rpMonthly.Controls.Add(this.lblLegA);
            this.rpMonthly.Controls.Add(this.lblMonthlyTitle);
            this.rpMonthly.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpMonthly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpMonthly.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.rpMonthly.Name = "rpMonthly";
            // 
            // chartMonthly
            // 
            this.lblMonthlyTitle.AutoSize = false;
            this.lblMonthlyTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblMonthlyTitle.Location = new System.Drawing.Point(0, 0);
            this.lblMonthlyTitle.Text = "Qəbul və təhvil, aylar üzrə";
            this.lblMonthlyTitle.Tag = "muted";
            this.lblMonthlyTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMonthlyTitle.Height = 26;
            this.lblMonthlyTitle.Name = "lblMonthlyTitle";
            // 
            // lblLegB
            // 
            this.lblLegA.AutoSize = true;
            this.lblLegA.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblLegA.Location = new System.Drawing.Point(560, 12);
            this.lblLegA.Text = "■ Qəbul edilib";
            this.lblLegA.Tag = "muted";
            this.lblLegA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLegA.Name = "lblLegA";
            // 
            // lblLegA
            // 
            this.lblLegB.AutoSize = true;
            this.lblLegB.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblLegB.Location = new System.Drawing.Point(660, 12);
            this.lblLegB.Text = "■ Təhvil verilib";
            this.lblLegB.Tag = "muted";
            this.lblLegB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLegB.Name = "lblLegB";
            // 
            // lblMonthlyTitle
            // 
            this.chartMonthly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartMonthly.Name = "chartMonthly";
            // 
            // rpStatus
            // 
            this.rpStatus.Controls.Add(this.brkStatus);
            this.rpStatus.Controls.Add(this.lblStatusTitle);
            this.rpStatus.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpStatus.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.rpStatus.Name = "rpStatus";
            // 
            // brkStatus
            // 
            this.lblStatusTitle.AutoSize = false;
            this.lblStatusTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblStatusTitle.Location = new System.Drawing.Point(0, 0);
            this.lblStatusTitle.Text = "Status bölgüsü";
            this.lblStatusTitle.Tag = "muted";
            this.lblStatusTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatusTitle.Height = 26;
            this.lblStatusTitle.Name = "lblStatusTitle";
            // 
            // lblStatusTitle
            // 
            this.brkStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.brkStatus.Name = "brkStatus";
            this.brkStatus.StatusClicked += new System.EventHandler<string>(this.brkStatus_StatusClicked);
            // 
            // rpTypes
            // 
            this.rpTypes.Controls.Add(this.barTypes);
            this.rpTypes.Controls.Add(this.lblTypesTitle);
            this.rpTypes.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpTypes.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.rpTypes.Name = "rpTypes";
            // 
            // lblTypesTitle
            // 
            this.lblTypesTitle.AutoSize = false;
            this.lblTypesTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblTypesTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTypesTitle.Text = "Texnika növü üzrə";
            this.lblTypesTitle.Tag = "muted";
            this.lblTypesTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTypesTitle.Height = 26;
            this.lblTypesTitle.Name = "lblTypesTitle";
            // 
            // barTypes
            // 
            this.barTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.barTypes.LabelWidth = 170;
            this.barTypes.Name = "barTypes";
            // 
            // lblTypesTitle
            // 
            this.rpAvg.Controls.Add(this.barAvg);
            this.rpAvg.Controls.Add(this.lblAvgTitle);
            this.rpAvg.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpAvg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpAvg.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.rpAvg.Name = "rpAvg";
            // 
            // rpAvg
            // 
            this.lblAvgTitle.AutoSize = false;
            this.lblAvgTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblAvgTitle.Location = new System.Drawing.Point(0, 0);
            this.lblAvgTitle.Text = "Təmir növü üzrə orta müddət (gün)";
            this.lblAvgTitle.Tag = "muted";
            this.lblAvgTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAvgTitle.Height = 26;
            this.lblAvgTitle.Name = "lblAvgTitle";
            // 
            // barAvg
            // 
            this.barAvg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.barAvg.BarColor = TexnikaReyestri.Controls.ChartColor.Chart2;
            this.barAvg.LabelWidth = 110;
            this.barAvg.Name = "barAvg";
            this.barAvg.ItemClicked += new System.EventHandler<string>(this.barAvg_ItemClicked);
            // 
            // rpLate
            // 
            this.rpLate.Controls.Add(this.gridLate);
            this.rpLate.Controls.Add(this.lnkLate);
            this.rpLate.Controls.Add(this.lblLateTitle);
            this.rpLate.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpLate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpLate.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.rpLate.Name = "rpLate";
            // 
            // lblAvgTitle
            // 
            this.lblLateTitle.AutoSize = false;
            this.lblLateTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblLateTitle.Location = new System.Drawing.Point(0, 0);
            this.lblLateTitle.Text = "Ən çox gecikənlər";
            this.lblLateTitle.Tag = "muted";
            this.lblLateTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLateTitle.Height = 26;
            this.lblLateTitle.Name = "lblLateTitle";
            // 
            // rpLate
            // 
            this.lnkLate.AutoSize = true;
            this.lnkLate.Text = "Hamısı →";
            this.lnkLate.Location = new System.Drawing.Point(300, 12);
            this.lnkLate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkLate.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkLate.Name = "lnkLate";
            this.lnkLate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLate_LinkClicked);
            // 
            // gridLate
            // 
            this.gridLate.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLateName,
            this.colLateDays});
            this.gridLate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLate.ColumnHeadersVisible = false;
            this.gridLate.Name = "gridLate";
            this.gridLate.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridLate_CellClick);
            // 
            // colLateName
            // 
            colLateName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colLateName.HeaderText = "Texnika";
            colLateName.MinimumWidth = 100;
            colLateName.Name = "colLateName";
            colLateName.ReadOnly = true;
            // 
            // colLateDays
            // 
            colLateDays.HeaderText = "Gecikmə";
            colLateDays.Name = "colLateDays";
            colLateDays.ReadOnly = true;
            colLateDays.Width = 80;
            // 
            // lnkLate
            // 
            this.rpEvents.Controls.Add(this.gridEvents);
            this.rpEvents.Controls.Add(this.lnkLogs);
            this.rpEvents.Controls.Add(this.lblEventsTitle);
            this.rpEvents.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpEvents.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.rpEvents.Name = "rpEvents";
            // 
            // lblLateTitle
            // 
            this.lblEventsTitle.AutoSize = false;
            this.lblEventsTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblEventsTitle.Location = new System.Drawing.Point(0, 0);
            this.lblEventsTitle.Text = "Son hadisələr";
            this.lblEventsTitle.Tag = "muted";
            this.lblEventsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEventsTitle.Height = 26;
            this.lblEventsTitle.Name = "lblEventsTitle";
            // 
            // rpEvents
            // 
            this.lnkLogs.AutoSize = true;
            this.lnkLogs.Text = "Loglar →";
            this.lnkLogs.Location = new System.Drawing.Point(700, 12);
            this.lnkLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkLogs.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkLogs.Name = "lnkLogs";
            this.lnkLogs.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLogs_LinkClicked);
            // 
            // gridEvents
            // 
            this.gridEvents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colEvTime,
            this.colEvType,
            this.colEvText});
            this.gridEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridEvents.ColumnHeadersVisible = false;
            this.gridEvents.Name = "gridEvents";
            // 
            // colEvTime
            // 
            colEvTime.HeaderText = "Tarix";
            colEvTime.Name = "colEvTime";
            colEvTime.ReadOnly = true;
            colEvTime.Width = 130;
            // 
            // colEvType
            // 
            colEvType.HeaderText = "Növ";
            colEvType.Name = "colEvType";
            colEvType.ReadOnly = true;
            colEvType.Width = 110;
            // 
            // colEvText
            // 
            colEvText.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colEvText.HeaderText = "Hadisə";
            colEvText.MinimumWidth = 100;
            colEvText.Name = "colEvText";
            colEvText.ReadOnly = true;
            // 
            // lnkLogs
            // 
            lnkLogs.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lnkLogs.AutoSize = true;
            lnkLogs.LinkBehavior = LinkBehavior.HoverUnderline;
            lnkLogs.Location = new Point(1270, 12);
            lnkLogs.Name = "lnkLogs";
            lnkLogs.Size = new Size(53, 15);
            lnkLogs.TabIndex = 1;
            lnkLogs.TabStop = true;
            lnkLogs.Text = "Loglar →";
            lnkLogs.LinkClicked += lnkLogs_LinkClicked;
            // 
            // lblEventsTitle
            // 
            lblEventsTitle.Dock = DockStyle.Top;
            lblEventsTitle.Font = new Font("Bahnschrift SemiBold", 10.5F);
            lblEventsTitle.Location = new Point(14, 10);
            lblEventsTitle.Name = "lblEventsTitle";
            lblEventsTitle.Size = new Size(742, 26);
            lblEventsTitle.TabIndex = 2;
            lblEventsTitle.Tag = "muted";
            lblEventsTitle.Text = "Son hadisələr";
            // 
            // lblFoot
            // 
            lblFoot.Dock = DockStyle.Bottom;
            lblFoot.Font = new Font("Segoe UI", 8.5F);
            lblFoot.Location = new Point(22, 676);
            lblFoot.Name = "lblFoot";
            lblFoot.Size = new Size(1172, 22);
            lblFoot.TabIndex = 1;
            lblFoot.Tag = "muted";
            lblFoot.Text = "Kartlara, status zolağına və zavodlara klikləyəndə reyestr həmin filtrlə açılır.";
            lblFoot.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // HomePage
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            Controls.Add(tlpBody);
            Controls.Add(lblFoot);
            Controls.Add(tlpTiles);
            Controls.Add(pnlHead);
            Name = "HomePage";
            Size = new Size(1216, 712);
            pnlHead.ResumeLayout(false);
            pnlHead.PerformLayout();
            tlpTiles.ResumeLayout(false);
            tlpBody.ResumeLayout(false);
            rpMonthly.ResumeLayout(false);
            rpMonthly.PerformLayout();
            rpStatus.ResumeLayout(false);
            rpTypes.ResumeLayout(false);
            rpAvg.ResumeLayout(false);
            rpLate.ResumeLayout(false);
            rpLate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridLate).EndInit();
            rpEvents.ResumeLayout(false);
            rpEvents.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridEvents).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Label lblTitle;
        private TexnikaReyestri.Controls.SegmentControl segPeriod;
        private System.Windows.Forms.TableLayoutPanel tlpTiles;
        private TexnikaReyestri.Controls.StatCard cardTotal;
        private TexnikaReyestri.Controls.StatCard cardRepair;
        private TexnikaReyestri.Controls.StatCard cardLate;
        private TexnikaReyestri.Controls.StatCard cardAvg;
        private System.Windows.Forms.TableLayoutPanel tlpBody;
        private TexnikaReyestri.Controls.RoundPanel rpMonthly;
        private System.Windows.Forms.Label lblMonthlyTitle;
        private System.Windows.Forms.Label lblLegA;
        private System.Windows.Forms.Label lblLegB;
        private TexnikaReyestri.Controls.ColumnChart chartMonthly;
        private TexnikaReyestri.Controls.RoundPanel rpStatus;
        private System.Windows.Forms.Label lblStatusTitle;
        private TexnikaReyestri.Controls.StatusBreakdown brkStatus;
        private TexnikaReyestri.Controls.RoundPanel rpTypes;
        private System.Windows.Forms.Label lblTypesTitle;
        private TexnikaReyestri.Controls.BarList barTypes;
        private TexnikaReyestri.Controls.RoundPanel rpAvg;
        private System.Windows.Forms.Label lblAvgTitle;
        private TexnikaReyestri.Controls.BarList barAvg;
        private TexnikaReyestri.Controls.RoundPanel rpLate;
        private System.Windows.Forms.Label lblLateTitle;
        private System.Windows.Forms.LinkLabel lnkLate;
        private TexnikaReyestri.Controls.ModernGrid gridLate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLateName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLateDays;
        private TexnikaReyestri.Controls.RoundPanel rpEvents;
        private System.Windows.Forms.Label lblEventsTitle;
        private System.Windows.Forms.LinkLabel lnkLogs;
        private TexnikaReyestri.Controls.ModernGrid gridEvents;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEvTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEvType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEvText;
        private System.Windows.Forms.Label lblFoot;
    }
}
