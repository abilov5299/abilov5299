namespace TexnikaQeydiyyat.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabSiyahi = new System.Windows.Forms.TabPage();
            this.tabDaxilEt = new System.Windows.Forms.TabPage();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.lblZavod = new System.Windows.Forms.Label();
            this.cmbZavod = new System.Windows.Forms.ComboBox();
            this.btnYenile = new System.Windows.Forms.Button();
            this.btnSil = new System.Windows.Forms.Button();
            this.dgvTexnika = new System.Windows.Forms.DataGridView();
            this.lblAd = new System.Windows.Forms.Label();
            this.txtAd = new System.Windows.Forms.TextBox();
            this.lblModel = new System.Windows.Forms.Label();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.lblSeriya = new System.Windows.Forms.Label();
            this.txtSeriya = new System.Windows.Forms.TextBox();
            this.lblQebul = new System.Windows.Forms.Label();
            this.dtpQebul = new System.Windows.Forms.DateTimePicker();
            this.lblQeyd = new System.Windows.Forms.Label();
            this.txtQeyd = new System.Windows.Forms.TextBox();
            this.lblParam = new System.Windows.Forms.Label();
            this.dgvParam = new System.Windows.Forms.DataGridView();
            this.colParamAd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colParamDeyer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnYaddaSaxla = new System.Windows.Forms.Button();
            this.btnLogYenile = new System.Windows.Forms.Button();
            this.dgvLog = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTexnika)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabSiyahi.SuspendLayout();
            this.tabDaxilEt.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.SuspendLayout();
            //
            // tabControl
            //
            this.tabControl.Controls.Add(this.tabSiyahi);
            this.tabControl.Controls.Add(this.tabDaxilEt);
            this.tabControl.Controls.Add(this.tabLog);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            //
            // tabSiyahi
            //
            this.tabSiyahi.Controls.Add(this.lblZavod);
            this.tabSiyahi.Controls.Add(this.cmbZavod);
            this.tabSiyahi.Controls.Add(this.btnYenile);
            this.tabSiyahi.Controls.Add(this.btnSil);
            this.tabSiyahi.Controls.Add(this.dgvTexnika);
            this.tabSiyahi.Name = "tabSiyahi";
            this.tabSiyahi.Text = "Texnikalar";
            //
            // lblZavod
            //
            this.lblZavod.AutoSize = true;
            this.lblZavod.Location = new System.Drawing.Point(10, 14);
            this.lblZavod.Name = "lblZavod";
            this.lblZavod.Text = "Zavod:";
            //
            // cmbZavod
            //
            this.cmbZavod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZavod.Location = new System.Drawing.Point(60, 10);
            this.cmbZavod.Name = "cmbZavod";
            this.cmbZavod.Size = new System.Drawing.Size(200, 23);
            //
            // btnYenile
            //
            this.btnYenile.Location = new System.Drawing.Point(270, 9);
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new System.Drawing.Size(90, 26);
            this.btnYenile.Text = "Yenilə";
            this.btnYenile.Click += new System.EventHandler(this.btnYenile_Click);
            //
            // btnSil
            //
            this.btnSil.Location = new System.Drawing.Point(370, 9);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(90, 26);
            this.btnSil.Text = "Sil";
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            //
            // dgvTexnika
            //
            this.dgvTexnika.AllowUserToAddRows = false;
            this.dgvTexnika.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTexnika.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTexnika.Location = new System.Drawing.Point(10, 45);
            this.dgvTexnika.Name = "dgvTexnika";
            this.dgvTexnika.ReadOnly = true;
            this.dgvTexnika.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTexnika.Size = new System.Drawing.Size(950, 540);
            //
            // tabDaxilEt
            //
            this.tabDaxilEt.Controls.Add(this.lblAd);
            this.tabDaxilEt.Controls.Add(this.txtAd);
            this.tabDaxilEt.Controls.Add(this.lblModel);
            this.tabDaxilEt.Controls.Add(this.txtModel);
            this.tabDaxilEt.Controls.Add(this.lblSeriya);
            this.tabDaxilEt.Controls.Add(this.txtSeriya);
            this.tabDaxilEt.Controls.Add(this.lblQebul);
            this.tabDaxilEt.Controls.Add(this.dtpQebul);
            this.tabDaxilEt.Controls.Add(this.lblQeyd);
            this.tabDaxilEt.Controls.Add(this.txtQeyd);
            this.tabDaxilEt.Controls.Add(this.lblParam);
            this.tabDaxilEt.Controls.Add(this.dgvParam);
            this.tabDaxilEt.Controls.Add(this.btnYaddaSaxla);
            this.tabDaxilEt.Name = "tabDaxilEt";
            this.tabDaxilEt.Text = "Texnika daxil et";
            //
            // lblAd
            //
            this.lblAd.AutoSize = true;
            this.lblAd.Location = new System.Drawing.Point(20, 20);
            this.lblAd.Name = "lblAd";
            this.lblAd.Text = "Ad *";
            //
            // txtAd
            //
            this.txtAd.Location = new System.Drawing.Point(140, 17);
            this.txtAd.Name = "txtAd";
            this.txtAd.Size = new System.Drawing.Size(300, 23);
            //
            // lblModel
            //
            this.lblModel.AutoSize = true;
            this.lblModel.Location = new System.Drawing.Point(20, 55);
            this.lblModel.Name = "lblModel";
            this.lblModel.Text = "Model";
            //
            // txtModel
            //
            this.txtModel.Location = new System.Drawing.Point(140, 52);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(300, 23);
            //
            // lblSeriya
            //
            this.lblSeriya.AutoSize = true;
            this.lblSeriya.Location = new System.Drawing.Point(20, 90);
            this.lblSeriya.Name = "lblSeriya";
            this.lblSeriya.Text = "Seriya nömrəsi";
            //
            // txtSeriya
            //
            this.txtSeriya.Location = new System.Drawing.Point(140, 87);
            this.txtSeriya.Name = "txtSeriya";
            this.txtSeriya.Size = new System.Drawing.Size(300, 23);
            //
            // lblQebul
            //
            this.lblQebul.AutoSize = true;
            this.lblQebul.Location = new System.Drawing.Point(20, 125);
            this.lblQebul.Name = "lblQebul";
            this.lblQebul.Text = "Qəbul tarixi";
            //
            // dtpQebul
            //
            this.dtpQebul.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpQebul.Location = new System.Drawing.Point(140, 122);
            this.dtpQebul.Name = "dtpQebul";
            this.dtpQebul.Size = new System.Drawing.Size(150, 23);
            //
            // lblQeyd
            //
            this.lblQeyd.AutoSize = true;
            this.lblQeyd.Location = new System.Drawing.Point(20, 160);
            this.lblQeyd.Name = "lblQeyd";
            this.lblQeyd.Text = "Qeyd";
            //
            // txtQeyd
            //
            this.txtQeyd.Location = new System.Drawing.Point(140, 157);
            this.txtQeyd.Multiline = true;
            this.txtQeyd.Name = "txtQeyd";
            this.txtQeyd.Size = new System.Drawing.Size(300, 60);
            //
            // lblParam
            //
            this.lblParam.AutoSize = true;
            this.lblParam.Location = new System.Drawing.Point(480, 20);
            this.lblParam.Name = "lblParam";
            this.lblParam.Text = "Parametrlər";
            //
            // dgvParam
            //
            this.dgvParam.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colParamAd, this.colParamDeyer });
            this.dgvParam.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvParam.Location = new System.Drawing.Point(480, 45);
            this.dgvParam.Name = "dgvParam";
            this.dgvParam.Size = new System.Drawing.Size(450, 300);
            //
            // colParamAd
            //
            this.colParamAd.HeaderText = "Parametr";
            this.colParamAd.Name = "colParamAd";
            //
            // colParamDeyer
            //
            this.colParamDeyer.HeaderText = "Dəyər";
            this.colParamDeyer.Name = "colParamDeyer";
            //
            // btnYaddaSaxla
            //
            this.btnYaddaSaxla.Location = new System.Drawing.Point(140, 235);
            this.btnYaddaSaxla.Name = "btnYaddaSaxla";
            this.btnYaddaSaxla.Size = new System.Drawing.Size(150, 32);
            this.btnYaddaSaxla.Text = "Yadda saxla";
            this.btnYaddaSaxla.Click += new System.EventHandler(this.btnYaddaSaxla_Click);
            //
            // tabLog
            //
            this.tabLog.Controls.Add(this.btnLogYenile);
            this.tabLog.Controls.Add(this.dgvLog);
            this.tabLog.Name = "tabLog";
            this.tabLog.Text = "Loglar";
            //
            // btnLogYenile
            //
            this.btnLogYenile.Location = new System.Drawing.Point(10, 9);
            this.btnLogYenile.Name = "btnLogYenile";
            this.btnLogYenile.Size = new System.Drawing.Size(90, 26);
            this.btnLogYenile.Text = "Yenilə";
            this.btnLogYenile.Click += new System.EventHandler(this.btnLogYenile_Click);
            //
            // dgvLog
            //
            this.dgvLog.AllowUserToAddRows = false;
            this.dgvLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLog.Location = new System.Drawing.Point(10, 45);
            this.dgvLog.Name = "dgvLog";
            this.dgvLog.ReadOnly = true;
            this.dgvLog.Size = new System.Drawing.Size(950, 540);
            //
            // MainForm
            //
            this.ClientSize = new System.Drawing.Size(984, 621);
            this.Controls.Add(this.tabControl);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Texnika qeydiyyatı";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTexnika)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
            this.tabSiyahi.ResumeLayout(false);
            this.tabSiyahi.PerformLayout();
            this.tabDaxilEt.ResumeLayout(false);
            this.tabDaxilEt.PerformLayout();
            this.tabLog.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabSiyahi;
        private System.Windows.Forms.TabPage tabDaxilEt;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.Label lblZavod;
        private System.Windows.Forms.ComboBox cmbZavod;
        private System.Windows.Forms.Button btnYenile;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.DataGridView dgvTexnika;
        private System.Windows.Forms.Label lblAd;
        private System.Windows.Forms.TextBox txtAd;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Label lblSeriya;
        private System.Windows.Forms.TextBox txtSeriya;
        private System.Windows.Forms.Label lblQebul;
        private System.Windows.Forms.DateTimePicker dtpQebul;
        private System.Windows.Forms.Label lblQeyd;
        private System.Windows.Forms.TextBox txtQeyd;
        private System.Windows.Forms.Label lblParam;
        private System.Windows.Forms.DataGridView dgvParam;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParamAd;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParamDeyer;
        private System.Windows.Forms.Button btnYaddaSaxla;
        private System.Windows.Forms.Button btnLogYenile;
        private System.Windows.Forms.DataGridView dgvLog;
    }
}
