namespace TexnikaReyestri.Pages
{
    partial class RegistryPage
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
            this.tlpTiles = new TexnikaReyestri.Controls.KartPaneli();
            this.pnlFoot = new System.Windows.Forms.Panel();
            this.rpGrid = new TexnikaReyestri.Controls.RoundPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnExport = new TexnikaReyestri.Controls.FlatButton();
            this.btnImport = new TexnikaReyestri.Controls.FlatButton();
            this.txtSearch = new TexnikaReyestri.Controls.RoundTextBox();
            this.cmbStatus = new TexnikaReyestri.Controls.RoundComboBox();
            this.cmbZavod = new TexnikaReyestri.Controls.RoundComboBox();
            this.grid = new TexnikaReyestri.Controls.ModernGrid();
            this.colNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNov = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMarka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colZavodNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBitme = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetail = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblHint = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.pnlHead.SuspendLayout();
            this.tlpTiles.SuspendLayout();
            this.pnlFoot.SuspendLayout();
            this.rpGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHead
            // 
            this.pnlHead.Controls.Add(this.cmbZavod);
            this.pnlHead.Controls.Add(this.cmbStatus);
            this.pnlHead.Controls.Add(this.txtSearch);
            this.pnlHead.Controls.Add(this.btnImport);
            this.pnlHead.Controls.Add(this.btnExport);
            this.pnlHead.Controls.Add(this.lblTitle);
            this.pnlHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHead.Size = new System.Drawing.Size(1172, 44);
            this.pnlHead.Name = "pnlHead";
            // 
            // tlpTiles
            // 
            this.tlpTiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpTiles.Name = "tlpTiles";
            this.tlpTiles.KartClicked += new System.EventHandler<string>(this.kart_Clicked);
            // 
            // pnlFoot
            // 
            this.pnlFoot.Controls.Add(this.lblHint);
            this.pnlFoot.Controls.Add(this.lblCount);
            this.pnlFoot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFoot.Size = new System.Drawing.Size(1172, 26);
            this.pnlFoot.Name = "pnlFoot";
            // 
            // rpGrid
            // 
            this.rpGrid.Controls.Add(this.grid);
            this.rpGrid.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.rpGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpGrid.Size = new System.Drawing.Size(1172, 516);
            this.rpGrid.Name = "rpGrid";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 18.0F);
            this.lblTitle.Location = new System.Drawing.Point(0, 4);
            this.lblTitle.Text = "Reyestr";
            this.lblTitle.Tag = "title";
            this.lblTitle.Name = "lblTitle";
            // 
            // btnExport
            // 
            this.btnExport.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            this.btnExport.Location = new System.Drawing.Point(1026, 4);
            this.btnExport.Size = new System.Drawing.Size(140, 34);
            this.btnExport.Text = "Excel-ə çıxar";
            this.btnExport.Glyph = "\uE896";
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Name = "btnExport";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            this.btnImport.Location = new System.Drawing.Point(876, 4);
            this.btnImport.Size = new System.Drawing.Size(144, 34);
            this.btnImport.Text = "Excel-dən idxal";
            this.btnImport.Glyph = "\uE898";
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Name = "btnImport";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.txtSearch.Location = new System.Drawing.Point(656, 8);
            this.txtSearch.Size = new System.Drawing.Size(212, 25);
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.PlaceholderText = "Marka, zavod nömrəsi...";
            this.txtSearch.Name = "txtSearch";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.cmbStatus.Location = new System.Drawing.Point(490, 7);
            this.cmbStatus.Size = new System.Drawing.Size(158, 25);
            this.cmbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.filtr_Changed);
            // 
            // cmbZavod
            // 
            this.cmbZavod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZavod.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.cmbZavod.Location = new System.Drawing.Point(350, 7);
            this.cmbZavod.Size = new System.Drawing.Size(132, 25);
            this.cmbZavod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbZavod.Name = "cmbZavod";
            this.cmbZavod.SelectedIndexChanged += new System.EventHandler(this.filtr_Changed);
            // 
            // grid
            // 
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNo,
            this.colNov,
            this.colMarka,
            this.colZavodNo,
            this.colStatus,
            this.colBitme,
            this.colDetail});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Size = new System.Drawing.Size(1160, 504);
            this.grid.VirtualMode = true;
            this.grid.Name = "grid";
            this.grid.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.grid_CellValueNeeded);
            this.grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellContentClick);
            this.grid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellDoubleClick);
            this.grid.Scroll += new System.Windows.Forms.ScrollEventHandler(this.grid_Scroll);
            this.grid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grid_CellFormatting);
            // 
            // colNo
            // 
            this.colNo.HeaderText = "№";
            this.colNo.Name = "colNo";
            this.colNo.Width = 70;
            this.colNo.Visible = false;
            // 
            // colNov
            // 
            this.colNov.HeaderText = "Texnika / inventar kodu";
            this.colNov.Name = "colNov";
            this.colNov.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colNov.MinimumWidth = 180;
            // 
            // colMarka
            // 
            this.colMarka.HeaderText = "Zavod";
            this.colMarka.Name = "colMarka";
            this.colMarka.Width = 240;
            // 
            // colZavodNo
            // 
            this.colZavodNo.HeaderText = "İcra statusu";
            this.colZavodNo.Name = "Status";
            this.colZavodNo.Width = 300;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Məsul icraçı";
            this.colStatus.Name = "colStatus";
            this.colStatus.Width = 220;
            // 
            // colBitme
            // 
            this.colBitme.HeaderText = "Təmirin gözlənilən bitmə tarixi";
            this.colBitme.Name = "colBitme";
            this.colBitme.Width = 230;
            this.colBitme.Visible = false;
            // 
            // colDetail
            // 
            this.colDetail.HeaderText = "";
            this.colDetail.Name = "colDetail";
            this.colDetail.Width = 140;
            this.colDetail.Text = "Geniş bax →";
            this.colDetail.UseColumnTextForButtonValue = true;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblCount.Location = new System.Drawing.Point(0, 6);
            this.lblCount.Text = "Göstərilir: 0 / 0";
            this.lblCount.Tag = "muted";
            this.lblCount.Name = "lblCount";
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblHint.Location = new System.Drawing.Point(966, 6);
            this.lblHint.Text = "Ctrl+F axtarış · Ctrl+N yeni · F5 yenilə";
            this.lblHint.Tag = "muted";
            this.lblHint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHint.Name = "lblHint";
            // 
            // RegistryPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(1216, 712);
            this.Controls.Add(this.rpGrid);
            this.Controls.Add(this.pnlFoot);
            this.Controls.Add(this.tlpTiles);
            this.Controls.Add(this.pnlHead);
            this.Name = "RegistryPage";
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.rpGrid.ResumeLayout(false);
            this.rpGrid.PerformLayout();
            this.pnlFoot.ResumeLayout(false);
            this.pnlFoot.PerformLayout();
            this.tlpTiles.ResumeLayout(false);
            this.tlpTiles.PerformLayout();
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlHead;
        private TexnikaReyestri.Controls.KartPaneli tlpTiles;
        private System.Windows.Forms.Panel pnlFoot;
        private TexnikaReyestri.Controls.RoundPanel rpGrid;
        private System.Windows.Forms.Label lblTitle;
        private TexnikaReyestri.Controls.FlatButton btnExport;
        private TexnikaReyestri.Controls.FlatButton btnImport;
        private TexnikaReyestri.Controls.RoundTextBox txtSearch;
        private TexnikaReyestri.Controls.RoundComboBox cmbStatus;
        private TexnikaReyestri.Controls.RoundComboBox cmbZavod;
        private TexnikaReyestri.Controls.ModernGrid grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNov;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMarka;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZavodNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBitme;
        private System.Windows.Forms.DataGridViewButtonColumn colDetail;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblHint;
    }
}
