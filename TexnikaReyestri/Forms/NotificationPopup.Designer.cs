namespace TexnikaReyestri.Forms
{
    partial class NotificationPopup
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
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlHead = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lnkReadAll = new System.Windows.Forms.LinkLabel();
            this.pnlFoot = new System.Windows.Forms.Panel();
            this.lnkAll = new System.Windows.Forms.LinkLabel();
            this.grid = new TexnikaReyestri.Controls.ModernGrid();
            this.colTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.pnlBody.SuspendLayout();
            this.pnlHead.SuspendLayout();
            this.pnlFoot.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.grid);
            this.pnlBody.Controls.Add(this.pnlFoot);
            this.pnlBody.Controls.Add(this.pnlHead);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Size = new System.Drawing.Size(388, 398);
            this.pnlBody.Tag = "card";
            this.pnlBody.Name = "pnlBody";
            // 
            // pnlHead
            // 
            this.pnlHead.Controls.Add(this.lnkReadAll);
            this.pnlHead.Controls.Add(this.lblTitle);
            this.pnlHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHead.Size = new System.Drawing.Size(388, 42);
            this.pnlHead.Padding = new System.Windows.Forms.Padding(14, 0, 10, 0);
            this.pnlHead.Name = "pnlHead";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 12.0F);
            this.lblTitle.Location = new System.Drawing.Point(14, 10);
            this.lblTitle.Text = "Bildirişlər";
            this.lblTitle.Tag = "title";
            this.lblTitle.Name = "lblTitle";
            // 
            // lnkReadAll
            // 
            this.lnkReadAll.AutoSize = true;
            this.lnkReadAll.Text = "Hamısını oxunmuş et";
            this.lnkReadAll.Location = new System.Drawing.Point(250, 14);
            this.lnkReadAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkReadAll.Name = "lnkReadAll";
            this.lnkReadAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkReadAll_LinkClicked);
            // 
            // pnlFoot
            // 
            this.pnlFoot.Controls.Add(this.lnkAll);
            this.pnlFoot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFoot.Size = new System.Drawing.Size(388, 36);
            this.pnlFoot.Name = "pnlFoot";
            // 
            // lnkAll
            // 
            this.lnkAll.AutoSize = true;
            this.lnkAll.Text = "Hamısına bax →";
            this.lnkAll.Location = new System.Drawing.Point(150, 10);
            this.lnkAll.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top));
            this.lnkAll.Name = "lnkAll";
            this.lnkAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAll_LinkClicked);
            // 
            // grid
            // 
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTitle});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Size = new System.Drawing.Size(388, 320);
            this.grid.ColumnHeadersVisible = false;
            this.grid.Name = "grid";
            this.grid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellClick);
            this.grid.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grid_CellPainting);
            // 
            // colTitle
            // 
            this.colTitle.HeaderText = "";
            this.colTitle.Name = "colTitle";
            this.colTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTitle.MinimumWidth = 100;
            // 
            // NotificationPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(390, 400);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.ShowInTaskbar = false;
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Controls.Add(this.pnlBody);
            this.Name = "NotificationPopup";
            this.Deactivate += new System.EventHandler(this.NotificationPopup_Deactivate);
            this.Load += new System.EventHandler(this.NotificationPopup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.pnlFoot.ResumeLayout(false);
            this.pnlFoot.PerformLayout();
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.LinkLabel lnkReadAll;
        private System.Windows.Forms.Panel pnlFoot;
        private System.Windows.Forms.LinkLabel lnkAll;
        private TexnikaReyestri.Controls.ModernGrid grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitle;
    }
}
