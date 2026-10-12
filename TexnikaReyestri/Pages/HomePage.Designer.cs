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
            this.tlpTiles = new System.Windows.Forms.TableLayoutPanel();
            this.cardTotal = new TexnikaReyestri.Controls.StatCard();
            this.cardRepair = new TexnikaReyestri.Controls.StatCard();
            this.cardLate = new TexnikaReyestri.Controls.StatCard();
            this.cardAvg = new TexnikaReyestri.Controls.StatCard();
            this.tlpBody = new System.Windows.Forms.TableLayoutPanel();
            this.rpMonthly = new TexnikaReyestri.Controls.RoundPanel();
            this.lblMonthlyTitle = new System.Windows.Forms.Label();
            this.lblLegA = new System.Windows.Forms.Label();
            this.lblLegB = new System.Windows.Forms.Label();
            this.chartMonthly = new TexnikaReyestri.Controls.ColumnChart();
            this.rpStatus = new TexnikaReyestri.Controls.RoundPanel();
            this.lblStatusTitle = new System.Windows.Forms.Label();
            this.brkStatus = new TexnikaReyestri.Controls.StatusBreakdown();
            this.rpTypes = new TexnikaReyestri.Controls.RoundPanel();
            this.lblTypesTitle = new System.Windows.Forms.Label();
            this.barTypes = new TexnikaReyestri.Controls.BarList();
            this.rpAvg = new TexnikaReyestri.Controls.RoundPanel();
            this.lblAvgTitle = new System.Windows.Forms.Label();
            this.barAvg = new TexnikaReyestri.Controls.BarList();
            this.rpLate = new TexnikaReyestri.Controls.RoundPanel();
            this.lblLateTitle = new System.Windows.Forms.Label();
            this.lnkLate = new System.Windows.Forms.LinkLabel();
            this.gridLate = new TexnikaReyestri.Controls.ModernGrid();
            this.colLateName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLateDays = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rpEvents = new TexnikaReyestri.Controls.RoundPanel();
            this.lblEventsTitle = new System.Windows.Forms.Label();
            this.lnkLogs = new System.Windows.Forms.LinkLabel();
            this.gridEvents = new TexnikaReyestri.Controls.ModernGrid();
            this.colEvTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEvType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEvText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblFoot = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridLate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridEvents)).BeginInit();
            this.pnlHead.SuspendLayout();
            this.tlpTiles.SuspendLayout();
            this.tlpBody.SuspendLayout();
            this.rpMonthly.SuspendLayout();
            this.rpStatus.SuspendLayout();
            this.rpTypes.SuspendLayout();
            this.rpAvg.SuspendLayout();
            this.rpLate.SuspendLayout();
            this.rpEvents.SuspendLayout();
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
            this.tlpTiles.ColumnCount = 4;
            this.tlpTiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0F));
            this.tlpTiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0F));
            this.tlpTiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0F));
            this.tlpTiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0F));
            this.tlpTiles.RowCount = 1;
            this.tlpTiles.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0F));
            this.tlpTiles.Controls.Add(this.cardTotal, 0, 0);
            this.tlpTiles.Controls.Add(this.cardRepair, 1, 0);
            this.tlpTiles.Controls.Add(this.cardLate, 2, 0);
            this.tlpTiles.Controls.Add(this.cardAvg, 3, 0);
            this.tlpTiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpTiles.Size = new System.Drawing.Size(1172, 96);
            this.tlpTiles.Padding = new System.Windows.Forms.Padding(0, 4, 0, 8);
            this.tlpTiles.Name = "tlpTiles";
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
            this.cardRepair.Size = new System.Drawing.Size(283, 96);
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
            this.cardLate.Size = new System.Drawing.Size(283, 96);
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
            this.cardAvg.Size = new System.Drawing.Size(283, 96);
            this.cardAvg.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.cardAvg.FilterKey = "";
            this.cardAvg.Name = "cardAvg";
            this.cardAvg.Click += new System.EventHandler(this.card_Click);
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
            this.tlpBody.Controls.Add(this.rpStatus, 2, 0);
            this.tlpBody.Controls.Add(this.rpTypes, 0, 1);
            this.tlpBody.Controls.Add(this.rpAvg, 1, 1);
            this.tlpBody.Controls.Add(this.rpLate, 2, 1);
            this.tlpBody.Controls.Add(this.rpEvents, 0, 2);
            this.tlpBody.SetColumnSpan(this.rpMonthly, 2);
            this.tlpBody.SetColumnSpan(this.rpEvents, 2);
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
            this.lblLegA.Location = new System.Drawing.Point(66, 12);
            this.lblLegA.Text = "■ Qəbul edilib";
            this.lblLegA.Tag = "muted";
            this.lblLegA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLegA.Name = "lblLegA";
            // 
            // lblLegB
            // 
            this.lblLegB.AutoSize = true;
            this.lblLegB.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblLegB.Location = new System.Drawing.Point(166, 12);
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
            // rpStatus
            // 
            this.rpStatus.Controls.Add(this.brkStatus);
            this.rpStatus.Controls.Add(this.lblStatusTitle);
            this.rpStatus.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpStatus.Size = new System.Drawing.Size(380, 208);
            this.rpStatus.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.rpStatus.Name = "rpStatus";
            // 
            // lblStatusTitle
            // 
            this.lblStatusTitle.AutoSize = false;
            this.lblStatusTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblStatusTitle.Location = new System.Drawing.Point(0, 0);
            this.lblStatusTitle.Text = "Status bölgüsü";
            this.lblStatusTitle.Tag = "muted";
            this.lblStatusTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatusTitle.Size = new System.Drawing.Size(352, 26);
            this.lblStatusTitle.Name = "lblStatusTitle";
            // 
            // brkStatus
            // 
            this.brkStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.brkStatus.Size = new System.Drawing.Size(352, 162);
            this.brkStatus.Name = "brkStatus";
            this.brkStatus.StatusClicked += new System.EventHandler<string>(this.brkStatus_StatusClicked);
            // 
            // rpTypes
            // 
            this.rpTypes.Controls.Add(this.barTypes);
            this.rpTypes.Controls.Add(this.lblTypesTitle);
            this.rpTypes.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpTypes.Size = new System.Drawing.Size(380, 146);
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
            this.lblTypesTitle.Size = new System.Drawing.Size(352, 26);
            this.lblTypesTitle.Name = "lblTypesTitle";
            // 
            // barTypes
            // 
            this.barTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.barTypes.Size = new System.Drawing.Size(352, 100);
            this.barTypes.LabelWidth = 170;
            this.barTypes.Name = "barTypes";
            // 
            // rpAvg
            // 
            this.rpAvg.Controls.Add(this.barAvg);
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
            this.lblAvgTitle.Text = "Təmir növü üzrə orta müddət (gün)";
            this.lblAvgTitle.Tag = "muted";
            this.lblAvgTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAvgTitle.Size = new System.Drawing.Size(352, 26);
            this.lblAvgTitle.Name = "lblAvgTitle";
            // 
            // barAvg
            // 
            this.barAvg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.barAvg.Size = new System.Drawing.Size(352, 100);
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
            this.rpLate.Size = new System.Drawing.Size(380, 146);
            this.rpLate.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.rpLate.Name = "rpLate";
            // 
            // lblLateTitle
            // 
            this.lblLateTitle.AutoSize = false;
            this.lblLateTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblLateTitle.Location = new System.Drawing.Point(0, 0);
            this.lblLateTitle.Text = "Ən çox gecikənlər";
            this.lblLateTitle.Tag = "muted";
            this.lblLateTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLateTitle.Size = new System.Drawing.Size(352, 26);
            this.lblLateTitle.Name = "lblLateTitle";
            // 
            // lnkLate
            // 
            this.lnkLate.AutoSize = true;
            this.lnkLate.Text = "Hamısı →";
            this.lnkLate.Location = new System.Drawing.Point(166, 12);
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
            this.gridLate.Size = new System.Drawing.Size(352, 100);
            this.gridLate.ColumnHeadersVisible = false;
            this.gridLate.Name = "gridLate";
            this.gridLate.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridLate_CellClick);
            // 
            // colLateName
            // 
            this.colLateName.HeaderText = "Texnika";
            this.colLateName.Name = "colLateName";
            this.colLateName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colLateName.MinimumWidth = 100;
            // 
            // colLateDays
            // 
            this.colLateDays.HeaderText = "Gecikmə";
            this.colLateDays.Name = "colLateDays";
            this.colLateDays.Width = 80;
            // 
            // rpEvents
            // 
            this.rpEvents.Controls.Add(this.gridEvents);
            this.rpEvents.Controls.Add(this.lnkLogs);
            this.rpEvents.Controls.Add(this.lblEventsTitle);
            this.rpEvents.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpEvents.Size = new System.Drawing.Size(380, 135);
            this.rpEvents.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.rpEvents.Name = "rpEvents";
            // 
            // lblEventsTitle
            // 
            this.lblEventsTitle.AutoSize = false;
            this.lblEventsTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblEventsTitle.Location = new System.Drawing.Point(0, 0);
            this.lblEventsTitle.Text = "Son hadisələr";
            this.lblEventsTitle.Tag = "muted";
            this.lblEventsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEventsTitle.Size = new System.Drawing.Size(352, 26);
            this.lblEventsTitle.Name = "lblEventsTitle";
            // 
            // lnkLogs
            // 
            this.lnkLogs.AutoSize = true;
            this.lnkLogs.Text = "Loglar →";
            this.lnkLogs.Location = new System.Drawing.Point(166, 12);
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
            this.gridEvents.Size = new System.Drawing.Size(352, 89);
            this.gridEvents.ColumnHeadersVisible = false;
            this.gridEvents.Name = "gridEvents";
            // 
            // colEvTime
            // 
            this.colEvTime.HeaderText = "Tarix";
            this.colEvTime.Name = "colEvTime";
            this.colEvTime.Width = 130;
            // 
            // colEvType
            // 
            this.colEvType.HeaderText = "Növ";
            this.colEvType.Name = "Status";
            this.colEvType.Width = 110;
            // 
            // colEvText
            // 
            this.colEvText.HeaderText = "Hadisə";
            this.colEvText.Name = "colEvText";
            this.colEvText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colEvText.MinimumWidth = 100;
            // 
            // lblFoot
            // 
            this.lblFoot.AutoSize = false;
            this.lblFoot.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblFoot.Location = new System.Drawing.Point(0, 0);
            this.lblFoot.Text = "Kartlara, status zolağına və zavodlara klikləyəndə reyestr həmin filtrlə açılır.";
            this.lblFoot.Tag = "muted";
            this.lblFoot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblFoot.Size = new System.Drawing.Size(1172, 22);
            this.lblFoot.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFoot.Name = "lblFoot";
            // 
            // HomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(1216, 712);
            this.Controls.Add(this.tlpBody);
            this.Controls.Add(this.lblFoot);
            this.Controls.Add(this.tlpTiles);
            this.Controls.Add(this.pnlHead);
            this.Name = "HomePage";
            ((System.ComponentModel.ISupportInitialize)(this.gridLate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridEvents)).EndInit();
            this.rpEvents.ResumeLayout(false);
            this.rpEvents.PerformLayout();
            this.rpLate.ResumeLayout(false);
            this.rpLate.PerformLayout();
            this.rpAvg.ResumeLayout(false);
            this.rpAvg.PerformLayout();
            this.rpTypes.ResumeLayout(false);
            this.rpTypes.PerformLayout();
            this.rpStatus.ResumeLayout(false);
            this.rpStatus.PerformLayout();
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
