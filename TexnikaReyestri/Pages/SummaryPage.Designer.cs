namespace TexnikaReyestri.Pages
{
    partial class SummaryPage
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
            this.lblAsOf = new System.Windows.Forms.Label();
            this.btnExport = new TexnikaReyestri.Controls.FlatButton();
            this.tlpTiles = new TexnikaReyestri.Controls.KartPaneli();
            this.rpGrid = new TexnikaReyestri.Controls.RoundPanel();
            this.grid = new TexnikaReyestri.Controls.ModernGrid();
            this.colZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblFoot = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.pnlHead.SuspendLayout();
            this.tlpTiles.SuspendLayout();
            this.rpGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHead
            // 
            this.pnlHead.Controls.Add(this.btnExport);
            this.pnlHead.Controls.Add(this.lblAsOf);
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
            this.lblTitle.Text = "İcmal: zavodlar üzrə";
            this.lblTitle.Tag = "title";
            this.lblTitle.Name = "lblTitle";
            // 
            // lblAsOf
            // 
            this.lblAsOf.AutoSize = true;
            this.lblAsOf.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblAsOf.Location = new System.Drawing.Point(810, 14);
            this.lblAsOf.Text = "Son idxala görə";
            this.lblAsOf.Tag = "muted";
            this.lblAsOf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAsOf.Name = "lblAsOf";
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
            // tlpTiles
            // 
            this.tlpTiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpTiles.Name = "tlpTiles";
            this.tlpTiles.KartClicked += new System.EventHandler<string>(this.kart_Clicked);
            // 
            // rpGrid
            // 
            this.rpGrid.Controls.Add(this.grid);
            this.rpGrid.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.rpGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpGrid.Size = new System.Drawing.Size(1172, 520);
            this.rpGrid.Name = "rpGrid";
            // 
            // grid
            // 
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colZ,
            this.colC,
            this.colT,
            this.colH,
            this.colG,
            this.colP,
            this.colO,
            this.colS});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Size = new System.Drawing.Size(1160, 508);
            this.grid.Name = "grid";
            this.grid.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grid_CellPainting);
            this.grid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellClick);
            this.grid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grid_CellFormatting);
            // 
            // colZ
            // 
            this.colZ.HeaderText = "Zavod";
            this.colZ.Name = "colZ";
            this.colZ.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colZ.MinimumWidth = 130;
            // 
            // colC
            // 
            this.colC.HeaderText = "Cəmi";
            this.colC.Name = "colC";
            this.colC.Width = 90;
            // 
            // colT
            // 
            this.colT.HeaderText = "Təmirdə";
            this.colT.Name = "colT";
            this.colT.Width = 100;
            // 
            // colH
            // 
            this.colH.HeaderText = "Ehtiyat hissələrinin təminatı gözlənilir";
            this.colH.Name = "colH";
            this.colH.Width = 170;
            // 
            // colG
            // 
            this.colG.HeaderText = "Gecikən";
            this.colG.Name = "colG";
            this.colG.Width = 90;
            // 
            // colP
            // 
            this.colP.HeaderText = "Gecikmə faizi";
            this.colP.Name = "Faiz";
            this.colP.Width = 200;
            // 
            // colO
            // 
            this.colO.HeaderText = "Orta təmir (gün)";
            this.colO.Name = "colO";
            this.colO.Width = 130;
            // 
            // colS
            // 
            this.colS.HeaderText = "Son paket";
            this.colS.Name = "colS";
            this.colS.Width = 170;
            // 
            // lblFoot
            // 
            this.lblFoot.AutoSize = false;
            this.lblFoot.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblFoot.Location = new System.Drawing.Point(0, 0);
            this.lblFoot.Text = "Zavod sətrinə klikləyəndə həmin zavodun reyestri açılır. Rəqəmlər zavodlardan son idxal olunan paketlərə görədir.";
            this.lblFoot.Tag = "muted";
            this.lblFoot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblFoot.Size = new System.Drawing.Size(1172, 22);
            this.lblFoot.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFoot.Name = "lblFoot";
            // 
            // SummaryPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(1216, 712);
            this.Controls.Add(this.rpGrid);
            this.Controls.Add(this.lblFoot);
            this.Controls.Add(this.tlpTiles);
            this.Controls.Add(this.pnlHead);
            this.Name = "SummaryPage";
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.rpGrid.ResumeLayout(false);
            this.rpGrid.PerformLayout();
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
        private System.Windows.Forms.Label lblAsOf;
        private TexnikaReyestri.Controls.FlatButton btnExport;
        private TexnikaReyestri.Controls.KartPaneli tlpTiles;
        private TexnikaReyestri.Controls.RoundPanel rpGrid;
        private TexnikaReyestri.Controls.ModernGrid grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn colC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colH;
        private System.Windows.Forms.DataGridViewTextBoxColumn colG;
        private System.Windows.Forms.DataGridViewTextBoxColumn colP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colO;
        private System.Windows.Forms.DataGridViewTextBoxColumn colS;
        private System.Windows.Forms.Label lblFoot;
    }
}
