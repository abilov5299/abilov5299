namespace TexnikaReyestri.Pages
{
    partial class BackupPage
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
            this.btnNow = new TexnikaReyestri.Controls.FlatButton();
            this.tlp = new System.Windows.Forms.TableLayoutPanel();
            this.rpAuto = new TexnikaReyestri.Controls.RoundPanel();
            this.lblAutoTitle = new System.Windows.Forms.Label();
            this.lblAutoDesc = new System.Windows.Forms.Label();
            this.kState = new System.Windows.Forms.Label();
            this.vState = new System.Windows.Forms.Label();
            this.kFolder = new System.Windows.Forms.Label();
            this.vFolder = new System.Windows.Forms.Label();
            this.kLast = new System.Windows.Forms.Label();
            this.vLast = new System.Windows.Forms.Label();
            this.kSize = new System.Windows.Forms.Label();
            this.vSize = new System.Windows.Forms.Label();
            this.kKept = new System.Windows.Forms.Label();
            this.vKept = new System.Windows.Forms.Label();
            this.lblTip = new System.Windows.Forms.Label();
            this.btnOpenFolder = new TexnikaReyestri.Controls.FlatButton();
            this.rpList = new TexnikaReyestri.Controls.RoundPanel();
            this.lblListTitle = new System.Windows.Forms.Label();
            this.grid = new TexnikaReyestri.Controls.ModernGrid();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKind = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRestore = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.pnlHead.SuspendLayout();
            this.tlp.SuspendLayout();
            this.rpAuto.SuspendLayout();
            this.rpList.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHead
            // 
            this.pnlHead.Controls.Add(this.btnNow);
            this.pnlHead.Controls.Add(this.lblTitle);
            this.pnlHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHead.Height = 44;
            this.pnlHead.Name = "pnlHead";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 18.0F);
            this.lblTitle.Location = new System.Drawing.Point(0, 4);
            this.lblTitle.Text = "Ehtiyat nüsxə";
            this.lblTitle.Tag = "title";
            this.lblTitle.Name = "lblTitle";
            // 
            // btnNow
            // 
            this.btnNow.Kind = TexnikaReyestri.Controls.ButtonKind.Primary;
            this.btnNow.Location = new System.Drawing.Point(1066, 4);
            this.btnNow.Size = new System.Drawing.Size(150, 34);
            this.btnNow.Text = "İndi nüsxə al";
            this.btnNow.Glyph = "\uE7B8";
            this.btnNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNow.Name = "btnNow";
            this.btnNow.Click += new System.EventHandler(this.btnNow_Click);
            // 
            // tlp
            // 
            this.tlp.ColumnCount = 2;
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.0F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.0F));
            this.tlp.RowCount = 1;
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0F));
            this.tlp.Controls.Add(this.rpAuto, 0, 0);
            this.tlp.Controls.Add(this.rpList, 1, 0);
            this.tlp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp.Name = "tlp";
            // 
            // rpAuto
            // 
            this.rpAuto.Controls.Add(this.btnOpenFolder);
            this.rpAuto.Controls.Add(this.lblTip);
            this.rpAuto.Controls.Add(this.vKept);
            this.rpAuto.Controls.Add(this.kKept);
            this.rpAuto.Controls.Add(this.vSize);
            this.rpAuto.Controls.Add(this.kSize);
            this.rpAuto.Controls.Add(this.vLast);
            this.rpAuto.Controls.Add(this.kLast);
            this.rpAuto.Controls.Add(this.vFolder);
            this.rpAuto.Controls.Add(this.kFolder);
            this.rpAuto.Controls.Add(this.vState);
            this.rpAuto.Controls.Add(this.kState);
            this.rpAuto.Controls.Add(this.lblAutoDesc);
            this.rpAuto.Controls.Add(this.lblAutoTitle);
            this.rpAuto.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpAuto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpAuto.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.rpAuto.Name = "rpAuto";
            // 
            // lblAutoTitle
            // 
            this.lblAutoTitle.AutoSize = false;
            this.lblAutoTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblAutoTitle.Location = new System.Drawing.Point(0, 0);
            this.lblAutoTitle.Text = "Avtomatik nüsxə";
            this.lblAutoTitle.Tag = "muted";
            this.lblAutoTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAutoTitle.Height = 26;
            this.lblAutoTitle.Name = "lblAutoTitle";
            // 
            // lblAutoDesc
            // 
            this.lblAutoDesc.AutoSize = true;
            this.lblAutoDesc.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblAutoDesc.Location = new System.Drawing.Point(14, 40);
            this.lblAutoDesc.Text = "Proqram hər gün ilk açılışda bazanın sıxılmış nüsxəsini götürür. Ən son nüsxələr saxlanılır, köhnələri avtomatik silinir.";
            this.lblAutoDesc.Tag = "muted";
            this.lblAutoDesc.MaximumSize = new System.Drawing.Size(440, 0);
            this.lblAutoDesc.Name = "lblAutoDesc";
            // 
            // kState
            // 
            this.kState.AutoSize = true;
            this.kState.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.kState.Location = new System.Drawing.Point(14, 96);
            this.kState.Text = "Vəziyyət";
            this.kState.Tag = "muted";
            this.kState.Name = "kState";
            // 
            // vState
            // 
            this.vState.AutoSize = true;
            this.vState.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.vState.Location = new System.Drawing.Point(170, 96);
            this.vState.Text = "● Aktivdir";
            this.vState.ForeColor = System.Drawing.Color.FromArgb(46, 155, 87);
            this.vState.Name = "vState";
            // 
            // kFolder
            // 
            this.kFolder.AutoSize = true;
            this.kFolder.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.kFolder.Location = new System.Drawing.Point(14, 122);
            this.kFolder.Text = "Qovluq";
            this.kFolder.Tag = "muted";
            this.kFolder.Name = "kFolder";
            // 
            // vFolder
            // 
            this.vFolder.AutoSize = true;
            this.vFolder.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.vFolder.Location = new System.Drawing.Point(170, 122);
            this.vFolder.Text = "C:\\ProgramData\\TexnikaReyestri\\Backup";
            this.vFolder.MaximumSize = new System.Drawing.Size(300, 0);
            this.vFolder.Name = "vFolder";
            // 
            // kLast
            // 
            this.kLast.AutoSize = true;
            this.kLast.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.kLast.Location = new System.Drawing.Point(14, 160);
            this.kLast.Text = "Son nüsxə";
            this.kLast.Tag = "muted";
            this.kLast.Name = "kLast";
            // 
            // vLast
            // 
            this.vLast.AutoSize = true;
            this.vLast.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.vLast.Location = new System.Drawing.Point(170, 160);
            this.vLast.Text = "—";
            this.vLast.Name = "vLast";
            // 
            // kSize
            // 
            this.kSize.AutoSize = true;
            this.kSize.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.kSize.Location = new System.Drawing.Point(14, 186);
            this.kSize.Text = "Bazanın ölçüsü";
            this.kSize.Tag = "muted";
            this.kSize.Name = "kSize";
            // 
            // vSize
            // 
            this.vSize.AutoSize = true;
            this.vSize.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.vSize.Location = new System.Drawing.Point(170, 186);
            this.vSize.Text = "—";
            this.vSize.Name = "vSize";
            // 
            // kKept
            // 
            this.kKept.AutoSize = true;
            this.kKept.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.kKept.Location = new System.Drawing.Point(14, 212);
            this.kKept.Text = "Saxlanılan";
            this.kKept.Tag = "muted";
            this.kKept.Name = "kKept";
            // 
            // vKept
            // 
            this.vKept.AutoSize = true;
            this.vKept.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.vKept.Location = new System.Drawing.Point(170, 212);
            this.vKept.Text = "—";
            this.vKept.Name = "vKept";
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblTip.Location = new System.Drawing.Point(14, 250);
            this.lblTip.Text = "Tövsiyə: nüsxə qovluğunu vaxtaşırı başqa diskə və ya xarici yaddaşa köçürün. Kompüterin diski xarab olsa, nüsxələr də itməsin.";
            this.lblTip.MaximumSize = new System.Drawing.Size(440, 0);
            this.lblTip.BackColor = System.Drawing.Color.FromArgb(255, 244, 229);
            this.lblTip.ForeColor = System.Drawing.Color.FromArgb(138, 75, 6);
            this.lblTip.Padding = new System.Windows.Forms.Padding(6);
            this.lblTip.Name = "lblTip";
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            this.btnOpenFolder.Location = new System.Drawing.Point(14, 330);
            this.btnOpenFolder.Size = new System.Drawing.Size(140, 34);
            this.btnOpenFolder.Text = "Qovluğu aç";
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // rpList
            // 
            this.rpList.Controls.Add(this.grid);
            this.rpList.Controls.Add(this.lblListTitle);
            this.rpList.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpList.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.rpList.Name = "rpList";
            // 
            // lblListTitle
            // 
            this.lblListTitle.AutoSize = false;
            this.lblListTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblListTitle.Location = new System.Drawing.Point(0, 0);
            this.lblListTitle.Text = "Nüsxələr";
            this.lblListTitle.Tag = "muted";
            this.lblListTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblListTitle.Height = 26;
            this.lblListTitle.Name = "lblListTitle";
            // 
            // grid
            // 
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTime,
            this.colSize,
            this.colKind,
            this.colFile,
            this.colRestore});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Name = "grid";
            this.grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellContentClick);
            // 
            // colTime
            // 
            this.colTime.HeaderText = "Tarix";
            this.colTime.Name = "colTime";
            this.colTime.Width = 150;
            // 
            // colSize
            // 
            this.colSize.HeaderText = "Ölçü";
            this.colSize.Name = "colSize";
            this.colSize.Width = 100;
            // 
            // colKind
            // 
            this.colKind.HeaderText = "Növ";
            this.colKind.Name = "colKind";
            this.colKind.Width = 110;
            // 
            // colFile
            // 
            this.colFile.HeaderText = "Fayl";
            this.colFile.Name = "colFile";
            this.colFile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colFile.MinimumWidth = 100;
            // 
            // colRestore
            // 
            this.colRestore.HeaderText = "";
            this.colRestore.Name = "colRestore";
            this.colRestore.Width = 120;
            this.colRestore.Text = "Bərpa et";
            this.colRestore.UseColumnTextForButtonValue = true;
            // 
            // BackupPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(1216, 712);
            this.Controls.Add(this.tlp);
            this.Controls.Add(this.pnlHead);
            this.Name = "BackupPage";
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.rpList.ResumeLayout(false);
            this.rpList.PerformLayout();
            this.rpAuto.ResumeLayout(false);
            this.rpAuto.PerformLayout();
            this.tlp.ResumeLayout(false);
            this.tlp.PerformLayout();
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Label lblTitle;
        private TexnikaReyestri.Controls.FlatButton btnNow;
        private System.Windows.Forms.TableLayoutPanel tlp;
        private TexnikaReyestri.Controls.RoundPanel rpAuto;
        private System.Windows.Forms.Label lblAutoTitle;
        private System.Windows.Forms.Label lblAutoDesc;
        private System.Windows.Forms.Label kState;
        private System.Windows.Forms.Label vState;
        private System.Windows.Forms.Label kFolder;
        private System.Windows.Forms.Label vFolder;
        private System.Windows.Forms.Label kLast;
        private System.Windows.Forms.Label vLast;
        private System.Windows.Forms.Label kSize;
        private System.Windows.Forms.Label vSize;
        private System.Windows.Forms.Label kKept;
        private System.Windows.Forms.Label vKept;
        private System.Windows.Forms.Label lblTip;
        private TexnikaReyestri.Controls.FlatButton btnOpenFolder;
        private TexnikaReyestri.Controls.RoundPanel rpList;
        private System.Windows.Forms.Label lblListTitle;
        private TexnikaReyestri.Controls.ModernGrid grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKind;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFile;
        private System.Windows.Forms.DataGridViewButtonColumn colRestore;
    }
}
