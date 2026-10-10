namespace TexnikaReyestri.Pages
{
    partial class NotificationsPage
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
            this.btnReadAll = new TexnikaReyestri.Controls.FlatButton();
            this.segFilter = new TexnikaReyestri.Controls.SegmentControl();
            this.rpList = new TexnikaReyestri.Controls.RoundPanel();
            this.grid = new TexnikaReyestri.Controls.ModernGrid();
            this.colKind = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colZavod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblFoot = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.pnlHead.SuspendLayout();
            this.rpList.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHead
            // 
            this.pnlHead.Controls.Add(this.segFilter);
            this.pnlHead.Controls.Add(this.btnReadAll);
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
            this.lblTitle.Text = "Bildirişlər";
            this.lblTitle.Tag = "title";
            this.lblTitle.Name = "lblTitle";
            // 
            // btnReadAll
            // 
            this.btnReadAll.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            this.btnReadAll.Location = new System.Drawing.Point(990, 4);
            this.btnReadAll.Size = new System.Drawing.Size(176, 34);
            this.btnReadAll.Text = "Hamısını oxunmuş et";
            this.btnReadAll.Glyph = "\uE73E";
            this.btnReadAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReadAll.Name = "btnReadAll";
            this.btnReadAll.Click += new System.EventHandler(this.btnReadAll_Click);
            // 
            // segFilter
            // 
            this.segFilter.Location = new System.Drawing.Point(350, 6);
            this.segFilter.Size = new System.Drawing.Size(630, 30);
            this.segFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.segFilter.Items = new string[] {
        "Hamısı",
        "Oxunmamış",
        "Gecikmə",
        "Son tarix",
        "Hissə",
        "Paket",
        "Yeni qəbul"};
            this.segFilter.SelectedIndex = 0;
            this.segFilter.Name = "segFilter";
            this.segFilter.SelectedIndexChanged += new System.EventHandler(this.segFilter_SelectedIndexChanged);
            // 
            // rpList
            // 
            this.rpList.Controls.Add(this.grid);
            this.rpList.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.rpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpList.Size = new System.Drawing.Size(1172, 616);
            this.rpList.Name = "rpList";
            // 
            // grid
            // 
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colKind,
            this.colTitle,
            this.colText,
            this.colZavod,
            this.colTime});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Size = new System.Drawing.Size(1160, 604);
            this.grid.Name = "grid";
            this.grid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellClick);
            this.grid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grid_CellFormatting);
            // 
            // colKind
            // 
            this.colKind.HeaderText = "";
            this.colKind.Name = "colKind";
            this.colKind.Width = 34;
            // 
            // colTitle
            // 
            this.colTitle.HeaderText = "Başlıq";
            this.colTitle.Name = "colTitle";
            this.colTitle.Width = 210;
            // 
            // colText
            // 
            this.colText.HeaderText = "Mətn";
            this.colText.Name = "colText";
            this.colText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colText.MinimumWidth = 100;
            // 
            // colZavod
            // 
            this.colZavod.HeaderText = "Zavod";
            this.colZavod.Name = "colZavod";
            this.colZavod.Width = 110;
            // 
            // colTime
            // 
            this.colTime.HeaderText = "Tarix";
            this.colTime.Name = "colTime";
            this.colTime.Width = 130;
            // 
            // lblFoot
            // 
            this.lblFoot.AutoSize = false;
            this.lblFoot.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblFoot.Location = new System.Drawing.Point(0, 0);
            this.lblFoot.Text = "Gecikmə, son tarix, hissə və paket bildirişləri proqram açılanda və hər saat avtomatik yoxlanılır. Qaydalar: Admin → Sistem.";
            this.lblFoot.Tag = "muted";
            this.lblFoot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblFoot.Size = new System.Drawing.Size(1172, 22);
            this.lblFoot.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFoot.Name = "lblFoot";
            // 
            // NotificationsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(1216, 712);
            this.Controls.Add(this.rpList);
            this.Controls.Add(this.lblFoot);
            this.Controls.Add(this.pnlHead);
            this.Name = "NotificationsPage";
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.rpList.ResumeLayout(false);
            this.rpList.PerformLayout();
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Label lblTitle;
        private TexnikaReyestri.Controls.FlatButton btnReadAll;
        private TexnikaReyestri.Controls.SegmentControl segFilter;
        private TexnikaReyestri.Controls.RoundPanel rpList;
        private TexnikaReyestri.Controls.ModernGrid grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKind;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colText;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZavod;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.Label lblFoot;
    }
}
