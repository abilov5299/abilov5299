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
            cardTotal.BackColor = Color.Transparent;
            cardTotal.Dock = DockStyle.Fill;
            cardTotal.FilterKey = "";
            cardTotal.Glyph = "";
            cardTotal.Location = new Point(0, 4);
            cardTotal.Margin = new Padding(0, 0, 10, 0);
            cardTotal.Name = "cardTotal";
            cardTotal.Size = new Size(283, 84);
            cardTotal.SubText = "";
            cardTotal.TabIndex = 0;
            cardTotal.Title = "Cəmi texnika";
            cardTotal.Value = "0";
            cardTotal.Click += card_Click;
            // 
            // cardRepair
            // 
            cardRepair.Accent = TexnikaReyestri.Controls.AccentKind.Info;
            cardRepair.BackColor = Color.Transparent;
            cardRepair.Dock = DockStyle.Fill;
            cardRepair.FilterKey = "Təmirdə";
            cardRepair.Glyph = "";
            cardRepair.Location = new Point(293, 4);
            cardRepair.Margin = new Padding(0, 0, 10, 0);
            cardRepair.Name = "cardRepair";
            cardRepair.Size = new Size(283, 84);
            cardRepair.SubText = "";
            cardRepair.TabIndex = 1;
            cardRepair.Title = "Təmirdə";
            cardRepair.Value = "0";
            cardRepair.Click += card_Click;
            // 
            // cardLate
            // 
            cardLate.Accent = TexnikaReyestri.Controls.AccentKind.Bad;
            cardLate.BackColor = Color.Transparent;
            cardLate.Dock = DockStyle.Fill;
            cardLate.FilterKey = "late";
            cardLate.Glyph = "";
            cardLate.Location = new Point(586, 4);
            cardLate.Margin = new Padding(0, 0, 10, 0);
            cardLate.Name = "cardLate";
            cardLate.Size = new Size(283, 84);
            cardLate.SubText = "";
            cardLate.TabIndex = 2;
            cardLate.Title = "Gecikən";
            cardLate.Value = "0";
            cardLate.Click += card_Click;
            // 
            // cardAvg
            // 
            cardAvg.Accent = TexnikaReyestri.Controls.AccentKind.Ok;
            cardAvg.BackColor = Color.Transparent;
            cardAvg.Dock = DockStyle.Fill;
            cardAvg.FilterKey = "";
            cardAvg.Glyph = "";
            cardAvg.Location = new Point(879, 4);
            cardAvg.Margin = new Padding(0, 0, 10, 0);
            cardAvg.Name = "cardAvg";
            cardAvg.Size = new Size(283, 84);
            cardAvg.SubText = "";
            cardAvg.TabIndex = 3;
            cardAvg.Title = "Orta təmir müddəti";
            cardAvg.Value = "0";
            cardAvg.Click += card_Click;
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
            rpMonthly.BackColor = Color.Transparent;
            tlpBody.SetColumnSpan(rpMonthly, 2);
            rpMonthly.Controls.Add(chartMonthly);
            rpMonthly.Controls.Add(lblLegB);
            rpMonthly.Controls.Add(lblLegA);
            rpMonthly.Controls.Add(lblMonthlyTitle);
            rpMonthly.Dock = DockStyle.Fill;
            rpMonthly.Location = new Point(0, 0);
            rpMonthly.Margin = new Padding(0, 0, 10, 10);
            rpMonthly.Name = "rpMonthly";
            rpMonthly.Padding = new Padding(14, 10, 14, 10);
            rpMonthly.Size = new Size(770, 208);
            rpMonthly.TabIndex = 0;
            // 
            // chartMonthly
            // 
            chartMonthly.Dock = DockStyle.Fill;
            chartMonthly.Font = new Font("Segoe UI", 8F);
            chartMonthly.Location = new Point(14, 36);
            chartMonthly.Name = "chartMonthly";
            chartMonthly.SeriesAName = "qəbul";
            chartMonthly.SeriesBName = "təhvil";
            chartMonthly.Size = new Size(742, 162);
            chartMonthly.TabIndex = 0;
            // 
            // lblLegB
            // 
            lblLegB.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblLegB.AutoSize = true;
            lblLegB.Font = new Font("Segoe UI", 8.5F);
            lblLegB.Location = new Point(1230, 12);
            lblLegB.Name = "lblLegB";
            lblLegB.Size = new Size(87, 15);
            lblLegB.TabIndex = 1;
            lblLegB.Tag = "muted";
            lblLegB.Text = "■ Təhvil verilib";
            // 
            // lblLegA
            // 
            lblLegA.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblLegA.AutoSize = true;
            lblLegA.Font = new Font("Segoe UI", 8.5F);
            lblLegA.Location = new Point(1130, 12);
            lblLegA.Name = "lblLegA";
            lblLegA.Size = new Size(84, 15);
            lblLegA.TabIndex = 2;
            lblLegA.Tag = "muted";
            lblLegA.Text = "■ Qəbul edilib";
            // 
            // lblMonthlyTitle
            // 
            lblMonthlyTitle.Dock = DockStyle.Top;
            lblMonthlyTitle.Font = new Font("Bahnschrift SemiBold", 10.5F);
            lblMonthlyTitle.Location = new Point(14, 10);
            lblMonthlyTitle.Name = "lblMonthlyTitle";
            lblMonthlyTitle.Size = new Size(742, 26);
            lblMonthlyTitle.TabIndex = 3;
            lblMonthlyTitle.Tag = "muted";
            lblMonthlyTitle.Text = "Qəbul və təhvil, aylar üzrə";
            // 
            // rpStatus
            // 
            rpStatus.BackColor = Color.Transparent;
            rpStatus.Controls.Add(brkStatus);
            rpStatus.Controls.Add(lblStatusTitle);
            rpStatus.Dock = DockStyle.Fill;
            rpStatus.Location = new Point(780, 0);
            rpStatus.Margin = new Padding(0, 0, 10, 10);
            rpStatus.Name = "rpStatus";
            rpStatus.Padding = new Padding(14, 10, 14, 10);
            rpStatus.Size = new Size(382, 208);
            rpStatus.TabIndex = 1;
            // 
            // brkStatus
            // 
            brkStatus.Dock = DockStyle.Fill;
            brkStatus.Font = new Font("Segoe UI", 8.5F);
            brkStatus.Location = new Point(14, 36);
            brkStatus.Name = "brkStatus";
            brkStatus.Size = new Size(354, 162);
            brkStatus.TabIndex = 0;
            brkStatus.StatusClicked += brkStatus_StatusClicked;
            // 
            // lblStatusTitle
            // 
            lblStatusTitle.Dock = DockStyle.Top;
            lblStatusTitle.Font = new Font("Bahnschrift SemiBold", 10.5F);
            lblStatusTitle.Location = new Point(14, 10);
            lblStatusTitle.Name = "lblStatusTitle";
            lblStatusTitle.Size = new Size(354, 26);
            lblStatusTitle.TabIndex = 1;
            lblStatusTitle.Tag = "muted";
            lblStatusTitle.Text = "Status bölgüsü";
            // 
            // rpTypes
            // 
            rpTypes.BackColor = Color.Transparent;
            rpTypes.Controls.Add(barTypes);
            rpTypes.Controls.Add(lblTypesTitle);
            rpTypes.Dock = DockStyle.Fill;
            rpTypes.Location = new Point(0, 218);
            rpTypes.Margin = new Padding(0, 0, 10, 10);
            rpTypes.Name = "rpTypes";
            rpTypes.Padding = new Padding(14, 10, 14, 10);
            rpTypes.Size = new Size(380, 146);
            rpTypes.TabIndex = 2;
            // 
            // barTypes
            // 
            barTypes.Dock = DockStyle.Fill;
            barTypes.Font = new Font("Segoe UI", 8.5F);
            barTypes.LabelWidth = 170;
            barTypes.Location = new Point(14, 36);
            barTypes.Name = "barTypes";
            barTypes.Size = new Size(352, 100);
            barTypes.TabIndex = 0;
            // 
            // lblTypesTitle
            // 
            lblTypesTitle.Dock = DockStyle.Top;
            lblTypesTitle.Font = new Font("Bahnschrift SemiBold", 10.5F);
            lblTypesTitle.Location = new Point(14, 10);
            lblTypesTitle.Name = "lblTypesTitle";
            lblTypesTitle.Size = new Size(352, 26);
            lblTypesTitle.TabIndex = 1;
            lblTypesTitle.Tag = "muted";
            lblTypesTitle.Text = "Texnika növü üzrə";
            // 
            // rpAvg
            // 
            rpAvg.BackColor = Color.Transparent;
            rpAvg.Controls.Add(barAvg);
            rpAvg.Controls.Add(lblAvgTitle);
            rpAvg.Dock = DockStyle.Fill;
            rpAvg.Location = new Point(390, 218);
            rpAvg.Margin = new Padding(0, 0, 10, 10);
            rpAvg.Name = "rpAvg";
            rpAvg.Padding = new Padding(14, 10, 14, 10);
            rpAvg.Size = new Size(380, 146);
            rpAvg.TabIndex = 3;
            // 
            // barAvg
            // 
            barAvg.BarColor = TexnikaReyestri.Controls.ChartColor.Chart2;
            barAvg.Dock = DockStyle.Fill;
            barAvg.Font = new Font("Segoe UI", 8.5F);
            barAvg.LabelWidth = 110;
            barAvg.Location = new Point(14, 36);
            barAvg.Name = "barAvg";
            barAvg.Size = new Size(352, 100);
            barAvg.TabIndex = 0;
            barAvg.ItemClicked += barAvg_ItemClicked;
            // 
            // lblAvgTitle
            // 
            lblAvgTitle.Dock = DockStyle.Top;
            lblAvgTitle.Font = new Font("Bahnschrift SemiBold", 10.5F);
            lblAvgTitle.Location = new Point(14, 10);
            lblAvgTitle.Name = "lblAvgTitle";
            lblAvgTitle.Size = new Size(352, 26);
            lblAvgTitle.TabIndex = 1;
            lblAvgTitle.Tag = "muted";
            lblAvgTitle.Text = "Təmir növü üzrə orta müddət (gün)";
            // 
            // rpLate
            // 
            rpLate.BackColor = Color.Transparent;
            rpLate.Controls.Add(gridLate);
            rpLate.Controls.Add(lnkLate);
            rpLate.Controls.Add(lblLateTitle);
            rpLate.Dock = DockStyle.Fill;
            rpLate.Location = new Point(780, 218);
            rpLate.Margin = new Padding(0, 0, 10, 10);
            rpLate.Name = "rpLate";
            rpLate.Padding = new Padding(14, 10, 14, 10);
            rpLate.Size = new Size(382, 146);
            rpLate.TabIndex = 4;
            // 
            // gridLate
            // 
            gridLate.AllowUserToAddRows = false;
            gridLate.AllowUserToDeleteRows = false;
            gridLate.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(246, 249, 249);
            gridLate.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            gridLate.BackgroundColor = Color.White;
            gridLate.BorderStyle = BorderStyle.None;
            gridLate.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            gridLate.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 8.5F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(93, 116, 114);
            dataGridViewCellStyle2.Padding = new Padding(8, 0, 0, 0);
            dataGridViewCellStyle2.SelectionBackColor = Color.White;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            gridLate.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            gridLate.ColumnHeadersHeight = 38;
            gridLate.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            gridLate.ColumnHeadersVisible = false;
            gridLate.Columns.AddRange(new DataGridViewColumn[] { colLateName, colLateDays });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(27, 43, 42);
            dataGridViewCellStyle3.Padding = new Padding(8, 0, 0, 0);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(220, 239, 236);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(27, 43, 42);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            gridLate.DefaultCellStyle = dataGridViewCellStyle3;
            gridLate.Dock = DockStyle.Fill;
            gridLate.EnableHeadersVisualStyles = false;
            gridLate.Font = new Font("Segoe UI", 9F);
            gridLate.GridColor = Color.FromArgb(246, 249, 249);
            gridLate.Location = new Point(14, 36);
            gridLate.MultiSelect = false;
            gridLate.Name = "gridLate";
            gridLate.ReadOnly = true;
            gridLate.RowHeadersVisible = false;
            gridLate.ScrollBars = ScrollBars.None;
            gridLate.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridLate.Size = new Size(354, 100);
            gridLate.TabIndex = 0;
            gridLate.CellClick += gridLate_CellClick;
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
            lnkLate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lnkLate.AutoSize = true;
            lnkLate.LinkBehavior = LinkBehavior.HoverUnderline;
            lnkLate.Location = new Point(482, 12);
            lnkLate.Name = "lnkLate";
            lnkLate.Size = new Size(57, 15);
            lnkLate.TabIndex = 1;
            lnkLate.TabStop = true;
            lnkLate.Text = "Hamısı →";
            lnkLate.LinkClicked += lnkLate_LinkClicked;
            // 
            // lblLateTitle
            // 
            lblLateTitle.Dock = DockStyle.Top;
            lblLateTitle.Font = new Font("Bahnschrift SemiBold", 10.5F);
            lblLateTitle.Location = new Point(14, 10);
            lblLateTitle.Name = "lblLateTitle";
            lblLateTitle.Size = new Size(354, 26);
            lblLateTitle.TabIndex = 2;
            lblLateTitle.Tag = "muted";
            lblLateTitle.Text = "Ən çox gecikənlər";
            // 
            // rpEvents
            // 
            rpEvents.BackColor = Color.Transparent;
            tlpBody.SetColumnSpan(rpEvents, 2);
            rpEvents.Controls.Add(gridEvents);
            rpEvents.Controls.Add(lnkLogs);
            rpEvents.Controls.Add(lblEventsTitle);
            rpEvents.Dock = DockStyle.Fill;
            rpEvents.Location = new Point(0, 374);
            rpEvents.Margin = new Padding(0, 0, 10, 10);
            rpEvents.Name = "rpEvents";
            rpEvents.Padding = new Padding(14, 10, 14, 10);
            rpEvents.Size = new Size(770, 136);
            rpEvents.TabIndex = 5;
            // 
            // gridEvents
            // 
            gridEvents.AllowUserToAddRows = false;
            gridEvents.AllowUserToDeleteRows = false;
            gridEvents.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(246, 249, 249);
            gridEvents.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            gridEvents.BackgroundColor = Color.White;
            gridEvents.BorderStyle = BorderStyle.None;
            gridEvents.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            gridEvents.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("Segoe UI Semibold", 8.5F);
            dataGridViewCellStyle5.ForeColor = Color.FromArgb(93, 116, 114);
            dataGridViewCellStyle5.Padding = new Padding(8, 0, 0, 0);
            dataGridViewCellStyle5.SelectionBackColor = Color.White;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            gridEvents.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            gridEvents.ColumnHeadersHeight = 38;
            gridEvents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            gridEvents.ColumnHeadersVisible = false;
            gridEvents.Columns.AddRange(new DataGridViewColumn[] { colEvTime, colEvType, colEvText });
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = Color.FromArgb(27, 43, 42);
            dataGridViewCellStyle6.Padding = new Padding(8, 0, 0, 0);
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(220, 239, 236);
            dataGridViewCellStyle6.SelectionForeColor = Color.FromArgb(27, 43, 42);
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            gridEvents.DefaultCellStyle = dataGridViewCellStyle6;
            gridEvents.Dock = DockStyle.Fill;
            gridEvents.EnableHeadersVisualStyles = false;
            gridEvents.Font = new Font("Segoe UI", 9F);
            gridEvents.GridColor = Color.FromArgb(246, 249, 249);
            gridEvents.Location = new Point(14, 36);
            gridEvents.MultiSelect = false;
            gridEvents.Name = "gridEvents";
            gridEvents.ReadOnly = true;
            gridEvents.RowHeadersVisible = false;
            gridEvents.ScrollBars = ScrollBars.None;
            gridEvents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridEvents.Size = new Size(742, 90);
            gridEvents.TabIndex = 0;
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
