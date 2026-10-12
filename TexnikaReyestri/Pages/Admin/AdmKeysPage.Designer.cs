namespace TexnikaReyestri.Pages.Admin
{
    partial class AdmKeysPage
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
            this.tlp = new System.Windows.Forms.TableLayoutPanel();
            this.rpMine = new TexnikaReyestri.Controls.RoundPanel();
            this.lblMineTitle = new System.Windows.Forms.Label();
            this.kType = new System.Windows.Forms.Label();
            this.vType = new System.Windows.Forms.Label();
            this.kFp = new System.Windows.Forms.Label();
            this.vFp = new System.Windows.Forms.Label();
            this.btnExportKey = new TexnikaReyestri.Controls.FlatButton();
            this.lblMineHint = new System.Windows.Forms.Label();
            this.btnNewKey = new TexnikaReyestri.Controls.FlatButton();
            this.rpTrusted = new TexnikaReyestri.Controls.RoundPanel();
            this.lblTrustedTitle = new System.Windows.Forms.Label();
            this.grid = new TexnikaReyestri.Controls.ModernGrid();
            this.colOrg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTFoot = new System.Windows.Forms.Panel();
            this.btnImportKey = new TexnikaReyestri.Controls.FlatButton();
            this.lblTrustHint = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.pnlHead.SuspendLayout();
            this.tlp.SuspendLayout();
            this.rpMine.SuspendLayout();
            this.rpTrusted.SuspendLayout();
            this.pnlTFoot.SuspendLayout();
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
            this.lblTitle.Text = "USB açarları";
            this.lblTitle.Tag = "title";
            this.lblTitle.Name = "lblTitle";
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = false;
            this.lblDesc.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblDesc.Location = new System.Drawing.Point(0, 0);
            this.lblDesc.Text = "USB paketlərini şifrələmək və imzalamaq üçün açarlar. Gizli açar bu kompüterdən çıxmır, açıq açar isə digər qurumlara verilir.";
            this.lblDesc.Tag = "muted";
            this.lblDesc.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDesc.Size = new System.Drawing.Size(914, 28);
            this.lblDesc.Name = "lblDesc";
            // 
            // tlp
            // 
            this.tlp.ColumnCount = 2;
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.0F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.0F));
            this.tlp.RowCount = 1;
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0F));
            this.tlp.Controls.Add(this.rpMine, 0, 0);
            this.tlp.Controls.Add(this.rpTrusted, 1, 0);
            this.tlp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp.Size = new System.Drawing.Size(914, 596);
            this.tlp.Name = "tlp";
            // 
            // rpMine
            // 
            this.rpMine.Controls.Add(this.btnNewKey);
            this.rpMine.Controls.Add(this.lblMineHint);
            this.rpMine.Controls.Add(this.btnExportKey);
            this.rpMine.Controls.Add(this.vFp);
            this.rpMine.Controls.Add(this.kFp);
            this.rpMine.Controls.Add(this.vType);
            this.rpMine.Controls.Add(this.kType);
            this.rpMine.Controls.Add(this.lblMineTitle);
            this.rpMine.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpMine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpMine.Size = new System.Drawing.Size(401, 586);
            this.rpMine.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.rpMine.Name = "rpMine";
            // 
            // lblMineTitle
            // 
            this.lblMineTitle.AutoSize = false;
            this.lblMineTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblMineTitle.Location = new System.Drawing.Point(0, 0);
            this.lblMineTitle.Text = "Bu kompüterin açarı";
            this.lblMineTitle.Tag = "muted";
            this.lblMineTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMineTitle.Size = new System.Drawing.Size(373, 26);
            this.lblMineTitle.Name = "lblMineTitle";
            // 
            // kType
            // 
            this.kType.AutoSize = true;
            this.kType.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.kType.Location = new System.Drawing.Point(14, 44);
            this.kType.Text = "Növ";
            this.kType.Tag = "muted";
            this.kType.Name = "kType";
            // 
            // vType
            // 
            this.vType.AutoSize = true;
            this.vType.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.vType.Location = new System.Drawing.Point(120, 44);
            this.vType.Text = "ECDSA P-384 (imza) + ECDH P-384";
            this.vType.Name = "vType";
            // 
            // kFp
            // 
            this.kFp.AutoSize = true;
            this.kFp.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.kFp.Location = new System.Drawing.Point(14, 72);
            this.kFp.Text = "Barmaq izi";
            this.kFp.Tag = "muted";
            this.kFp.Name = "kFp";
            // 
            // vFp
            // 
            this.vFp.AutoSize = true;
            this.vFp.Font = new System.Drawing.Font("Consolas", 13.0F, System.Drawing.FontStyle.Bold);
            this.vFp.Location = new System.Drawing.Point(120, 68);
            this.vFp.Text = "—";
            this.vFp.Tag = "title";
            this.vFp.Name = "vFp";
            // 
            // btnExportKey
            // 
            this.btnExportKey.Kind = TexnikaReyestri.Controls.ButtonKind.Primary;
            this.btnExportKey.Location = new System.Drawing.Point(14, 112);
            this.btnExportKey.Size = new System.Drawing.Size(220, 36);
            this.btnExportKey.Text = "Açıq açarı USB-yə yaz";
            this.btnExportKey.Glyph = "\uE896";
            this.btnExportKey.Name = "btnExportKey";
            this.btnExportKey.Click += new System.EventHandler(this.btnExportKey_Click);
            // 
            // lblMineHint
            // 
            this.lblMineHint.AutoSize = true;
            this.lblMineHint.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblMineHint.Location = new System.Drawing.Point(14, 160);
            this.lblMineHint.Text = "Açıq açar faylını alıcı qurum bir dəfə idxal edir. Bundan sonra bu kompüterin paketlərini yoxlaya və aça bilir.";
            this.lblMineHint.Tag = "muted";
            this.lblMineHint.MaximumSize = new System.Drawing.Size(380, 0);
            this.lblMineHint.Name = "lblMineHint";
            // 
            // btnNewKey
            // 
            this.btnNewKey.Kind = TexnikaReyestri.Controls.ButtonKind.Danger;
            this.btnNewKey.Location = new System.Drawing.Point(14, 230);
            this.btnNewKey.Size = new System.Drawing.Size(170, 34);
            this.btnNewKey.Text = "Yeni açar yarat";
            this.btnNewKey.Name = "btnNewKey";
            this.btnNewKey.Click += new System.EventHandler(this.btnNewKey_Click);
            // 
            // rpTrusted
            // 
            this.rpTrusted.Controls.Add(this.grid);
            this.rpTrusted.Controls.Add(this.pnlTFoot);
            this.rpTrusted.Controls.Add(this.lblTrustedTitle);
            this.rpTrusted.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpTrusted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpTrusted.Size = new System.Drawing.Size(492, 586);
            this.rpTrusted.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.rpTrusted.Name = "rpTrusted";
            // 
            // lblTrustedTitle
            // 
            this.lblTrustedTitle.AutoSize = false;
            this.lblTrustedTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblTrustedTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTrustedTitle.Text = "Etibar edilən açarlar";
            this.lblTrustedTitle.Tag = "muted";
            this.lblTrustedTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTrustedTitle.Size = new System.Drawing.Size(464, 26);
            this.lblTrustedTitle.Name = "lblTrustedTitle";
            // 
            // grid
            // 
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colOrg,
            this.colFp,
            this.colDate});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Size = new System.Drawing.Size(464, 454);
            this.grid.Name = "grid";
            // 
            // colOrg
            // 
            this.colOrg.HeaderText = "Qurum";
            this.colOrg.Name = "colOrg";
            this.colOrg.Width = 120;
            // 
            // colFp
            // 
            this.colFp.HeaderText = "Barmaq izi";
            this.colFp.Name = "colFp";
            this.colFp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colFp.MinimumWidth = 100;
            // 
            // colDate
            // 
            this.colDate.HeaderText = "Əlavə edilib";
            this.colDate.Name = "colDate";
            this.colDate.Width = 130;
            // 
            // pnlTFoot
            // 
            this.pnlTFoot.Controls.Add(this.lblTrustHint);
            this.pnlTFoot.Controls.Add(this.btnImportKey);
            this.pnlTFoot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTFoot.Size = new System.Drawing.Size(464, 86);
            this.pnlTFoot.Name = "pnlTFoot";
            // 
            // btnImportKey
            // 
            this.btnImportKey.Kind = TexnikaReyestri.Controls.ButtonKind.Primary;
            this.btnImportKey.Location = new System.Drawing.Point(0, 8);
            this.btnImportKey.Size = new System.Drawing.Size(210, 36);
            this.btnImportKey.Text = "Açar faylını idxal et";
            this.btnImportKey.Glyph = "\uE898";
            this.btnImportKey.Name = "btnImportKey";
            this.btnImportKey.Click += new System.EventHandler(this.btnImportKey_Click);
            // 
            // lblTrustHint
            // 
            this.lblTrustHint.AutoSize = true;
            this.lblTrustHint.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTrustHint.Location = new System.Drawing.Point(0, 50);
            this.lblTrustHint.Text = "Yeni açar idxal ediləndə barmaq izini telefonla və ya şəxsən yoxlayın. Bu, saxta açar riskini aradan qaldırır.";
            this.lblTrustHint.Tag = "muted";
            this.lblTrustHint.MaximumSize = new System.Drawing.Size(480, 0);
            this.lblTrustHint.Name = "lblTrustHint";
            // 
            // AdmKeysPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(958, 698);
            this.Controls.Add(this.tlp);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.pnlHead);
            this.Name = "AdmKeysPage";
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.pnlTFoot.ResumeLayout(false);
            this.pnlTFoot.PerformLayout();
            this.rpTrusted.ResumeLayout(false);
            this.rpTrusted.PerformLayout();
            this.rpMine.ResumeLayout(false);
            this.rpMine.PerformLayout();
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
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TableLayoutPanel tlp;
        private TexnikaReyestri.Controls.RoundPanel rpMine;
        private System.Windows.Forms.Label lblMineTitle;
        private System.Windows.Forms.Label kType;
        private System.Windows.Forms.Label vType;
        private System.Windows.Forms.Label kFp;
        private System.Windows.Forms.Label vFp;
        private TexnikaReyestri.Controls.FlatButton btnExportKey;
        private System.Windows.Forms.Label lblMineHint;
        private TexnikaReyestri.Controls.FlatButton btnNewKey;
        private TexnikaReyestri.Controls.RoundPanel rpTrusted;
        private System.Windows.Forms.Label lblTrustedTitle;
        private TexnikaReyestri.Controls.ModernGrid grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrg;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.Panel pnlTFoot;
        private TexnikaReyestri.Controls.FlatButton btnImportKey;
        private System.Windows.Forms.Label lblTrustHint;
    }
}
