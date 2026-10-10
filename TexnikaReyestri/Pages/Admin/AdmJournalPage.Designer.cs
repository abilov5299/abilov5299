namespace TexnikaReyestri.Pages.Admin
{
    partial class AdmJournalPage
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
            this.lblDesc = new System.Windows.Forms.Label();
            this.rpGrid = new TexnikaReyestri.Controls.RoundPanel();
            this.grid = new TexnikaReyestri.Controls.ModernGrid();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWho = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.pnlHead.SuspendLayout();
            this.rpGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHead
            // 
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
            this.lblTitle.Text = "Admin jurnalı";
            this.lblTitle.Tag = "title";
            this.lblTitle.Name = "lblTitle";
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = false;
            this.lblDesc.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblDesc.Location = new System.Drawing.Point(0, 0);
            this.lblDesc.Text = "Admin panelində edilən hər əməliyyat burada qeyd olunur. Jurnal silinə bilməz.";
            this.lblDesc.Tag = "muted";
            this.lblDesc.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDesc.Size = new System.Drawing.Size(914, 28);
            this.lblDesc.Name = "lblDesc";
            // 
            // rpGrid
            // 
            this.rpGrid.Controls.Add(this.grid);
            this.rpGrid.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.rpGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpGrid.Size = new System.Drawing.Size(914, 596);
            this.rpGrid.Name = "rpGrid";
            // 
            // grid
            // 
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTime,
            this.colWho,
            this.colOp});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Size = new System.Drawing.Size(902, 584);
            this.grid.Name = "grid";
            // 
            // colTime
            // 
            this.colTime.HeaderText = "Tarix";
            this.colTime.Name = "colTime";
            this.colTime.Width = 140;
            // 
            // colWho
            // 
            this.colWho.HeaderText = "Kim";
            this.colWho.Name = "colWho";
            this.colWho.Width = 100;
            // 
            // colOp
            // 
            this.colOp.HeaderText = "Əməliyyat";
            this.colOp.Name = "colOp";
            this.colOp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colOp.MinimumWidth = 100;
            // 
            // AdmJournalPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(958, 698);
            this.Controls.Add(this.rpGrid);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.pnlHead);
            this.Name = "AdmJournalPage";
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.rpGrid.ResumeLayout(false);
            this.rpGrid.PerformLayout();
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDesc;
        private TexnikaReyestri.Controls.RoundPanel rpGrid;
        private TexnikaReyestri.Controls.ModernGrid grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWho;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOp;
    }
}
