namespace TexnikaReyestri.Pages
{
    partial class SyncPage
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
            this.lblWarn = new System.Windows.Forms.Label();
            this.pnlGap = new System.Windows.Forms.Panel();
            this.tlp = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.rpExport = new TexnikaReyestri.Controls.RoundPanel();
            this.lblExportTitle = new System.Windows.Forms.Label();
            this.lblExpDesc = new System.Windows.Forms.Label();
            this.kPkg = new System.Windows.Forms.Label();
            this.vPkg = new System.Windows.Forms.Label();
            this.kCnt = new System.Windows.Forms.Label();
            this.vCnt = new System.Windows.Forms.Label();
            this.kRec = new System.Windows.Forms.Label();
            this.vRec = new System.Windows.Forms.Label();
            this.kLast = new System.Windows.Forms.Label();
            this.vLast = new System.Windows.Forms.Label();
            this.kDrive = new System.Windows.Forms.Label();
            this.cmbDrive = new TexnikaReyestri.Controls.RoundComboBox();
            this.btnDrives = new TexnikaReyestri.Controls.FlatButton();
            this.btnExport = new TexnikaReyestri.Controls.FlatButton();
            this.rpImport = new TexnikaReyestri.Controls.RoundPanel();
            this.lblImportTitle = new System.Windows.Forms.Label();
            this.lstPackages = new System.Windows.Forms.ListBox();
            this.btnPick = new TexnikaReyestri.Controls.FlatButton();
            this.btnImport = new TexnikaReyestri.Controls.FlatButton();
            this.rpProc = new TexnikaReyestri.Controls.RoundPanel();
            this.lblProcTitle = new System.Windows.Forms.Label();
            this.steps = new TexnikaReyestri.Controls.StepList();
            this.lblResult = new System.Windows.Forms.Label();
            this.rpHist = new TexnikaReyestri.Controls.RoundPanel();
            this.lblHistTitle = new System.Windows.Forms.Label();
            this.gridHist = new TexnikaReyestri.Controls.ModernGrid();
            this.colHTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHOp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHNew = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHChg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHDel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHRes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridHist)).BeginInit();
            this.pnlHead.SuspendLayout();
            this.pnlGap.SuspendLayout();
            this.tlp.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.rpExport.SuspendLayout();
            this.rpImport.SuspendLayout();
            this.rpProc.SuspendLayout();
            this.rpHist.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHead
            // 
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
            this.lblTitle.Text = "İxrac / İdxal";
            this.lblTitle.Tag = "title";
            this.lblTitle.Name = "lblTitle";
            // 
            // lblWarn
            // 
            this.lblWarn.AutoSize = false;
            this.lblWarn.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblWarn.Location = new System.Drawing.Point(0, 0);
            this.lblWarn.Text = "⚠ Son ixrac 5 gün əvvəl olub.";
            this.lblWarn.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblWarn.Size = new System.Drawing.Size(1172, 34);
            this.lblWarn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblWarn.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblWarn.BackColor = System.Drawing.Color.FromArgb(255, 244, 229);
            this.lblWarn.ForeColor = System.Drawing.Color.FromArgb(138, 75, 6);
            this.lblWarn.Name = "lblWarn";
            // 
            // pnlGap
            // 
            this.pnlGap.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGap.Size = new System.Drawing.Size(1172, 8);
            this.pnlGap.Name = "pnlGap";
            // 
            // tlp
            // 
            this.tlp.ColumnCount = 2;
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0F));
            this.tlp.RowCount = 2;
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56.0F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.0F));
            this.tlp.Controls.Add(this.pnlLeft, 0, 0);
            this.tlp.Controls.Add(this.rpProc, 1, 0);
            this.tlp.Controls.Add(this.rpHist, 0, 1);
            this.tlp.SetColumnSpan(this.rpHist, 2);
            this.tlp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp.Size = new System.Drawing.Size(1172, 596);
            this.tlp.Name = "tlp";
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.rpImport);
            this.pnlLeft.Controls.Add(this.rpExport);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.pnlLeft.Name = "pnlLeft";
            // 
            // rpExport
            // 
            this.rpExport.Controls.Add(this.btnExport);
            this.rpExport.Controls.Add(this.btnDrives);
            this.rpExport.Controls.Add(this.cmbDrive);
            this.rpExport.Controls.Add(this.kDrive);
            this.rpExport.Controls.Add(this.vLast);
            this.rpExport.Controls.Add(this.kLast);
            this.rpExport.Controls.Add(this.vRec);
            this.rpExport.Controls.Add(this.kRec);
            this.rpExport.Controls.Add(this.vCnt);
            this.rpExport.Controls.Add(this.kCnt);
            this.rpExport.Controls.Add(this.vPkg);
            this.rpExport.Controls.Add(this.kPkg);
            this.rpExport.Controls.Add(this.lblExpDesc);
            this.rpExport.Controls.Add(this.lblExportTitle);
            this.rpExport.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpExport.Name = "rpExport";
            // 
            // lblExportTitle
            // 
            this.lblExportTitle.AutoSize = false;
            this.lblExportTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblExportTitle.Location = new System.Drawing.Point(0, 0);
            this.lblExportTitle.Text = "Yeni paket";
            this.lblExportTitle.Tag = "muted";
            this.lblExportTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblExportTitle.Height = 26;
            this.lblExportTitle.Name = "lblExportTitle";
            // 
            // lblExpDesc
            // 
            this.lblExpDesc.AutoSize = true;
            this.lblExpDesc.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblExpDesc.Location = new System.Drawing.Point(14, 40);
            this.lblExpDesc.Text = "Son ixracdan bəri dəyişənlər paketə düşür. Paketi yalnız üst qurum aça bilər.";
            this.lblExpDesc.Tag = "muted";
            this.lblExpDesc.MaximumSize = new System.Drawing.Size(520, 0);
            this.lblExpDesc.Name = "lblExpDesc";
            // 
            // kPkg
            // 
            this.kPkg.AutoSize = true;
            this.kPkg.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.kPkg.Location = new System.Drawing.Point(14, 80);
            this.kPkg.Text = "Paket nömrəsi";
            this.kPkg.Tag = "muted";
            this.kPkg.Name = "kPkg";
            // 
            // vPkg
            // 
            this.vPkg.AutoSize = true;
            this.vPkg.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.vPkg.Location = new System.Drawing.Point(200, 80);
            this.vPkg.Text = "TQX-Z3-000001";
            this.vPkg.Name = "vPkg";
            // 
            // kCnt
            // 
            this.kCnt.AutoSize = true;
            this.kCnt.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.kCnt.Location = new System.Drawing.Point(14, 106);
            this.kCnt.Text = "Dəyişiklik";
            this.kCnt.Tag = "muted";
            this.kCnt.Name = "kCnt";
            // 
            // vCnt
            // 
            this.vCnt.AutoSize = true;
            this.vCnt.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.vCnt.Location = new System.Drawing.Point(200, 106);
            this.vCnt.Text = "0";
            this.vCnt.Name = "vCnt";
            // 
            // kRec
            // 
            this.kRec.AutoSize = true;
            this.kRec.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.kRec.Location = new System.Drawing.Point(14, 132);
            this.kRec.Text = "Alıcı";
            this.kRec.Tag = "muted";
            this.kRec.Name = "kRec";
            // 
            // vRec
            // 
            this.vRec.AutoSize = true;
            this.vRec.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.vRec.Location = new System.Drawing.Point(200, 132);
            this.vRec.Text = "Üst qurum";
            this.vRec.Name = "vRec";
            // 
            // kLast
            // 
            this.kLast.AutoSize = true;
            this.kLast.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.kLast.Location = new System.Drawing.Point(14, 158);
            this.kLast.Text = "Son ixrac";
            this.kLast.Tag = "muted";
            this.kLast.Name = "kLast";
            // 
            // vLast
            // 
            this.vLast.AutoSize = true;
            this.vLast.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.vLast.Location = new System.Drawing.Point(200, 158);
            this.vLast.Text = "—";
            this.vLast.Name = "vLast";
            // 
            // kDrive
            // 
            this.kDrive.AutoSize = true;
            this.kDrive.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.kDrive.Location = new System.Drawing.Point(14, 196);
            this.kDrive.Text = "USB yaddaş";
            this.kDrive.Tag = "muted";
            this.kDrive.Name = "kDrive";
            // 
            // cmbDrive
            // 
            this.cmbDrive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDrive.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.cmbDrive.Location = new System.Drawing.Point(200, 192);
            this.cmbDrive.Size = new System.Drawing.Size(250, 25);
            this.cmbDrive.Name = "cmbDrive";
            // 
            // btnDrives
            // 
            this.btnDrives.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            this.btnDrives.Location = new System.Drawing.Point(456, 191);
            this.btnDrives.Size = new System.Drawing.Size(36, 28);
            this.btnDrives.Text = "";
            this.btnDrives.Glyph = "\uE72C";
            this.btnDrives.Name = "btnDrives";
            this.btnDrives.Click += new System.EventHandler(this.btnDrives_Click);
            // 
            // btnExport
            // 
            this.btnExport.Kind = TexnikaReyestri.Controls.ButtonKind.Primary;
            this.btnExport.Location = new System.Drawing.Point(14, 240);
            this.btnExport.Size = new System.Drawing.Size(220, 40);
            this.btnExport.Text = "USB-yə ixrac et";
            this.btnExport.Glyph = "\uE896";
            this.btnExport.Name = "btnExport";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // rpImport
            // 
            this.rpImport.Controls.Add(this.btnImport);
            this.rpImport.Controls.Add(this.btnPick);
            this.rpImport.Controls.Add(this.lstPackages);
            this.rpImport.Controls.Add(this.lblImportTitle);
            this.rpImport.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpImport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpImport.Name = "rpImport";
            // 
            // lblImportTitle
            // 
            this.lblImportTitle.AutoSize = false;
            this.lblImportTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblImportTitle.Location = new System.Drawing.Point(0, 0);
            this.lblImportTitle.Text = "USB-dəki paketlər";
            this.lblImportTitle.Tag = "muted";
            this.lblImportTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblImportTitle.Height = 26;
            this.lblImportTitle.Name = "lblImportTitle";
            // 
            // lstPackages
            // 
            this.lstPackages.Location = new System.Drawing.Point(14, 42);
            this.lstPackages.Size = new System.Drawing.Size(560, 150);
            this.lstPackages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPackages.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.lstPackages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstPackages.IntegralHeight = false;
            this.lstPackages.Name = "lstPackages";
            this.lstPackages.SelectedIndexChanged += new System.EventHandler(this.lstPackages_SelectedIndexChanged);
            // 
            // btnPick
            // 
            this.btnPick.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            this.btnPick.Location = new System.Drawing.Point(14, 204);
            this.btnPick.Size = new System.Drawing.Size(140, 36);
            this.btnPick.Text = "Fayl seç...";
            this.btnPick.Glyph = "\uE898";
            this.btnPick.Name = "btnPick";
            this.btnPick.Click += new System.EventHandler(this.btnPick_Click);
            // 
            // btnImport
            // 
            this.btnImport.Kind = TexnikaReyestri.Controls.ButtonKind.Primary;
            this.btnImport.Location = new System.Drawing.Point(162, 204);
            this.btnImport.Size = new System.Drawing.Size(230, 36);
            this.btnImport.Text = "Seçilmiş paketi idxal et";
            this.btnImport.Glyph = "\uE898";
            this.btnImport.Name = "btnImport";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // rpProc
            // 
            this.rpProc.Controls.Add(this.lblResult);
            this.rpProc.Controls.Add(this.steps);
            this.rpProc.Controls.Add(this.lblProcTitle);
            this.rpProc.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpProc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpProc.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.rpProc.Name = "rpProc";
            // 
            // lblProcTitle
            // 
            this.lblProcTitle.AutoSize = false;
            this.lblProcTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblProcTitle.Location = new System.Drawing.Point(0, 0);
            this.lblProcTitle.Text = "Proses";
            this.lblProcTitle.Tag = "muted";
            this.lblProcTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProcTitle.Height = 26;
            this.lblProcTitle.Name = "lblProcTitle";
            // 
            // steps
            // 
            this.steps.Location = new System.Drawing.Point(14, 42);
            this.steps.Size = new System.Drawing.Size(540, 150);
            this.steps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.steps.Name = "steps";
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.lblResult.Location = new System.Drawing.Point(14, 200);
            this.lblResult.Text = "";
            this.lblResult.MaximumSize = new System.Drawing.Size(540, 0);
            this.lblResult.Name = "lblResult";
            // 
            // rpHist
            // 
            this.rpHist.Controls.Add(this.gridHist);
            this.rpHist.Controls.Add(this.lblHistTitle);
            this.rpHist.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpHist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpHist.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.rpHist.Name = "rpHist";
            // 
            // lblHistTitle
            // 
            this.lblHistTitle.AutoSize = false;
            this.lblHistTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblHistTitle.Location = new System.Drawing.Point(0, 0);
            this.lblHistTitle.Text = "Sinxronizasiya tarixçəsi";
            this.lblHistTitle.Tag = "muted";
            this.lblHistTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHistTitle.Height = 26;
            this.lblHistTitle.Name = "lblHistTitle";
            // 
            // gridHist
            // 
            this.gridHist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colHTime,
            this.colHOp,
            this.colHFile,
            this.colHNew,
            this.colHChg,
            this.colHDel,
            this.colHRes,
            this.colHNote});
            this.gridHist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridHist.Name = "gridHist";
            // 
            // colHTime
            // 
            this.colHTime.HeaderText = "Tarix";
            this.colHTime.Name = "colHTime";
            this.colHTime.Width = 130;
            // 
            // colHOp
            // 
            this.colHOp.HeaderText = "Əməliyyat";
            this.colHOp.Name = "colHOp";
            this.colHOp.Width = 100;
            // 
            // colHFile
            // 
            this.colHFile.HeaderText = "Paket";
            this.colHFile.Name = "colHFile";
            this.colHFile.Width = 170;
            // 
            // colHNew
            // 
            this.colHNew.HeaderText = "Yeni";
            this.colHNew.Name = "colHNew";
            this.colHNew.Width = 70;
            // 
            // colHChg
            // 
            this.colHChg.HeaderText = "Dəyişiklik";
            this.colHChg.Name = "colHChg";
            this.colHChg.Width = 90;
            // 
            // colHDel
            // 
            this.colHDel.HeaderText = "Silinmə";
            this.colHDel.Name = "colHDel";
            this.colHDel.Width = 80;
            // 
            // colHRes
            // 
            this.colHRes.HeaderText = "Nəticə";
            this.colHRes.Name = "Status";
            this.colHRes.Width = 120;
            // 
            // colHNote
            // 
            this.colHNote.HeaderText = "Qeyd";
            this.colHNote.Name = "colHNote";
            this.colHNote.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colHNote.MinimumWidth = 100;
            // 
            // SyncPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(1216, 712);
            this.Controls.Add(this.tlp);
            this.Controls.Add(this.pnlGap);
            this.Controls.Add(this.lblWarn);
            this.Controls.Add(this.pnlHead);
            this.Name = "SyncPage";
            ((System.ComponentModel.ISupportInitialize)(this.gridHist)).EndInit();
            this.rpHist.ResumeLayout(false);
            this.rpHist.PerformLayout();
            this.rpProc.ResumeLayout(false);
            this.rpProc.PerformLayout();
            this.rpImport.ResumeLayout(false);
            this.rpImport.PerformLayout();
            this.rpExport.ResumeLayout(false);
            this.rpExport.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.tlp.ResumeLayout(false);
            this.tlp.PerformLayout();
            this.pnlGap.ResumeLayout(false);
            this.pnlGap.PerformLayout();
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblWarn;
        private System.Windows.Forms.Panel pnlGap;
        private System.Windows.Forms.TableLayoutPanel tlp;
        private System.Windows.Forms.Panel pnlLeft;
        private TexnikaReyestri.Controls.RoundPanel rpExport;
        private System.Windows.Forms.Label lblExportTitle;
        private System.Windows.Forms.Label lblExpDesc;
        private System.Windows.Forms.Label kPkg;
        private System.Windows.Forms.Label vPkg;
        private System.Windows.Forms.Label kCnt;
        private System.Windows.Forms.Label vCnt;
        private System.Windows.Forms.Label kRec;
        private System.Windows.Forms.Label vRec;
        private System.Windows.Forms.Label kLast;
        private System.Windows.Forms.Label vLast;
        private System.Windows.Forms.Label kDrive;
        private TexnikaReyestri.Controls.RoundComboBox cmbDrive;
        private TexnikaReyestri.Controls.FlatButton btnDrives;
        private TexnikaReyestri.Controls.FlatButton btnExport;
        private TexnikaReyestri.Controls.RoundPanel rpImport;
        private System.Windows.Forms.Label lblImportTitle;
        private System.Windows.Forms.ListBox lstPackages;
        private TexnikaReyestri.Controls.FlatButton btnPick;
        private TexnikaReyestri.Controls.FlatButton btnImport;
        private TexnikaReyestri.Controls.RoundPanel rpProc;
        private System.Windows.Forms.Label lblProcTitle;
        private TexnikaReyestri.Controls.StepList steps;
        private System.Windows.Forms.Label lblResult;
        private TexnikaReyestri.Controls.RoundPanel rpHist;
        private System.Windows.Forms.Label lblHistTitle;
        private TexnikaReyestri.Controls.ModernGrid gridHist;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHOp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHNew;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHChg;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHDel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHRes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHNote;
    }
}
