namespace TexnikaReyestri.Pages
{
    partial class LogsPage
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
            this.dtFrom = new TexnikaReyestri.Controls.RoundDatePicker();
            this.lblDash = new System.Windows.Forms.Label();
            this.dtTo = new TexnikaReyestri.Controls.RoundDatePicker();
            this.cmbType = new TexnikaReyestri.Controls.RoundComboBox();
            this.btnSearch = new TexnikaReyestri.Controls.FlatButton();
            this.rpGrid = new TexnikaReyestri.Controls.RoundPanel();
            this.grid = new TexnikaReyestri.Controls.ModernGrid();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colZavod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblFoot = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.pnlHead.SuspendLayout();
            this.rpGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHead
            // 
            this.pnlHead.Controls.Add(this.btnSearch);
            this.pnlHead.Controls.Add(this.cmbType);
            this.pnlHead.Controls.Add(this.dtTo);
            this.pnlHead.Controls.Add(this.lblDash);
            this.pnlHead.Controls.Add(this.dtFrom);
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
            this.lblTitle.Text = "Loglar";
            this.lblTitle.Tag = "title";
            this.lblTitle.Name = "lblTitle";
            // 
            // dtFrom
            // 
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFrom.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.dtFrom.Location = new System.Drawing.Point(640, 8);
            this.dtFrom.Size = new System.Drawing.Size(120, 25);
            this.dtFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtFrom.Name = "dtFrom";
            // 
            // lblDash
            // 
            this.lblDash.AutoSize = true;
            this.lblDash.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblDash.Location = new System.Drawing.Point(766, 11);
            this.lblDash.Text = "—";
            this.lblDash.Tag = "muted";
            this.lblDash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDash.Name = "lblDash";
            // 
            // dtTo
            // 
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtTo.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.dtTo.Location = new System.Drawing.Point(788, 8);
            this.dtTo.Size = new System.Drawing.Size(120, 25);
            this.dtTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtTo.Name = "dtTo";
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.cmbType.Location = new System.Drawing.Point(916, 8);
            this.cmbType.Size = new System.Drawing.Size(160, 25);
            this.cmbType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbType.Name = "cmbType";
            this.cmbType.Items.AddRange(new object[] {
            "Növ: Hamısı",
            "Giriş",
            "Giriş xətası",
            "Çıxış",
            "Əlavə",
            "Redaktə",
            "Silmə",
            "İxrac",
            "İdxal",
            "Xəta"});
            // 
            // btnSearch
            // 
            this.btnSearch.Kind = TexnikaReyestri.Controls.ButtonKind.Primary;
            this.btnSearch.Location = new System.Drawing.Point(1086, 4);
            this.btnSearch.Size = new System.Drawing.Size(130, 34);
            this.btnSearch.Text = "Axtar";
            this.btnSearch.Glyph = "\uE721";
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // rpGrid
            // 
            this.rpGrid.Controls.Add(this.grid);
            this.rpGrid.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.rpGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpGrid.Size = new System.Drawing.Size(1172, 616);
            this.rpGrid.Name = "rpGrid";
            // 
            // grid
            // 
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTime,
            this.colUser,
            this.colZavod,
            this.colType,
            this.colText});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Size = new System.Drawing.Size(1160, 604);
            this.grid.Name = "grid";
            // 
            // colTime
            // 
            this.colTime.HeaderText = "Tarix";
            this.colTime.Name = "colTime";
            this.colTime.Width = 140;
            // 
            // colUser
            // 
            this.colUser.HeaderText = "İstifadəçi";
            this.colUser.Name = "colUser";
            this.colUser.Width = 170;
            // 
            // colZavod
            // 
            this.colZavod.HeaderText = "Zavod";
            this.colZavod.Name = "colZavod";
            this.colZavod.Width = 110;
            // 
            // colType
            // 
            this.colType.HeaderText = "Növ";
            this.colType.Name = "Status";
            this.colType.Width = 130;
            // 
            // colText
            // 
            this.colText.HeaderText = "Hadisə";
            this.colText.Name = "colText";
            this.colText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colText.MinimumWidth = 100;
            // 
            // lblFoot
            // 
            this.lblFoot.AutoSize = false;
            this.lblFoot.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblFoot.Location = new System.Drawing.Point(0, 0);
            this.lblFoot.Text = "Son 1000 qeyd göstərilir. Zavod istifadəçisi yalnız öz zavodunun loglarını görür.";
            this.lblFoot.Tag = "muted";
            this.lblFoot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblFoot.Size = new System.Drawing.Size(1172, 22);
            this.lblFoot.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFoot.Name = "lblFoot";
            // 
            // LogsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(1216, 712);
            this.Controls.Add(this.rpGrid);
            this.Controls.Add(this.lblFoot);
            this.Controls.Add(this.pnlHead);
            this.Name = "LogsPage";
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
        private TexnikaReyestri.Controls.RoundDatePicker dtFrom;
        private System.Windows.Forms.Label lblDash;
        private TexnikaReyestri.Controls.RoundDatePicker dtTo;
        private TexnikaReyestri.Controls.RoundComboBox cmbType;
        private TexnikaReyestri.Controls.FlatButton btnSearch;
        private TexnikaReyestri.Controls.RoundPanel rpGrid;
        private TexnikaReyestri.Controls.ModernGrid grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZavod;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colText;
        private System.Windows.Forms.Label lblFoot;
    }
}
