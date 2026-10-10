namespace TexnikaReyestri.Forms
{
    partial class AdminForm
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
            this.pnlAside = new System.Windows.Forms.Panel();
            this.lblAside = new System.Windows.Forms.Label();
            this.radUsers = new System.Windows.Forms.RadioButton();
            this.radOrgs = new System.Windows.Forms.RadioButton();
            this.radPc = new System.Windows.Forms.RadioButton();
            this.radSec = new System.Windows.Forms.RadioButton();
            this.radKeys = new System.Windows.Forms.RadioButton();
            this.radSys = new System.Windows.Forms.RadioButton();
            this.radLog = new System.Windows.Forms.RadioButton();
            this.btnExit = new TexnikaReyestri.Controls.FlatButton();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlAside.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlAside
            // 
            this.pnlAside.Controls.Add(this.btnExit);
            this.pnlAside.Controls.Add(this.radLog);
            this.pnlAside.Controls.Add(this.radSys);
            this.pnlAside.Controls.Add(this.radKeys);
            this.pnlAside.Controls.Add(this.radSec);
            this.pnlAside.Controls.Add(this.radPc);
            this.pnlAside.Controls.Add(this.radOrgs);
            this.pnlAside.Controls.Add(this.radUsers);
            this.pnlAside.Controls.Add(this.lblAside);
            this.pnlAside.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlAside.Width = 220;
            this.pnlAside.BackColor = System.Drawing.Color.FromArgb(27, 43, 42);
            this.pnlAside.Padding = new System.Windows.Forms.Padding(0, 12, 0, 12);
            this.pnlAside.Name = "pnlAside";
            // 
            // lblAside
            // 
            this.lblAside.AutoSize = true;
            this.lblAside.Font = new System.Drawing.Font("Bahnschrift SemiBold", 14.0F);
            this.lblAside.Location = new System.Drawing.Point(18, 14);
            this.lblAside.Text = "🔒 Admin paneli";
            this.lblAside.Tag = "white";
            this.lblAside.Name = "lblAside";
            // 
            // radUsers
            // 
            this.radUsers.Appearance = System.Windows.Forms.Appearance.Button;
            this.radUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radUsers.Text = "👤 İstifadəçilər";
            this.radUsers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.radUsers.Location = new System.Drawing.Point(0, 60);
            this.radUsers.Size = new System.Drawing.Size(220, 40);
            this.radUsers.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.radUsers.ForeColor = System.Drawing.Color.FromArgb(207, 224, 222);
            this.radUsers.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.radUsers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radUsers.Checked = true;
            this.radUsers.Name = "radUsers";
            this.radUsers.CheckedChanged += new System.EventHandler(this.tab_CheckedChanged);
            // 
            // radOrgs
            // 
            this.radOrgs.Appearance = System.Windows.Forms.Appearance.Button;
            this.radOrgs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radOrgs.Text = "🏭 Qurumlar";
            this.radOrgs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.radOrgs.Location = new System.Drawing.Point(0, 102);
            this.radOrgs.Size = new System.Drawing.Size(220, 40);
            this.radOrgs.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.radOrgs.ForeColor = System.Drawing.Color.FromArgb(207, 224, 222);
            this.radOrgs.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.radOrgs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radOrgs.Checked = false;
            this.radOrgs.Name = "radOrgs";
            this.radOrgs.CheckedChanged += new System.EventHandler(this.tab_CheckedChanged);
            // 
            // radPc
            // 
            this.radPc.Appearance = System.Windows.Forms.Appearance.Button;
            this.radPc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radPc.Text = "🖥 Bu kompüter";
            this.radPc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.radPc.Location = new System.Drawing.Point(0, 144);
            this.radPc.Size = new System.Drawing.Size(220, 40);
            this.radPc.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.radPc.ForeColor = System.Drawing.Color.FromArgb(207, 224, 222);
            this.radPc.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.radPc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radPc.Checked = false;
            this.radPc.Name = "radPc";
            this.radPc.CheckedChanged += new System.EventHandler(this.tab_CheckedChanged);
            // 
            // radSec
            // 
            this.radSec.Appearance = System.Windows.Forms.Appearance.Button;
            this.radSec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radSec.Text = "🛡 Təhlükəsizlik";
            this.radSec.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.radSec.Location = new System.Drawing.Point(0, 186);
            this.radSec.Size = new System.Drawing.Size(220, 40);
            this.radSec.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.radSec.ForeColor = System.Drawing.Color.FromArgb(207, 224, 222);
            this.radSec.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.radSec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radSec.Checked = false;
            this.radSec.Name = "radSec";
            this.radSec.CheckedChanged += new System.EventHandler(this.tab_CheckedChanged);
            // 
            // radKeys
            // 
            this.radKeys.Appearance = System.Windows.Forms.Appearance.Button;
            this.radKeys.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radKeys.Text = "🔑 USB açarları";
            this.radKeys.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.radKeys.Location = new System.Drawing.Point(0, 228);
            this.radKeys.Size = new System.Drawing.Size(220, 40);
            this.radKeys.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.radKeys.ForeColor = System.Drawing.Color.FromArgb(207, 224, 222);
            this.radKeys.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.radKeys.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radKeys.Checked = false;
            this.radKeys.Name = "radKeys";
            this.radKeys.CheckedChanged += new System.EventHandler(this.tab_CheckedChanged);
            // 
            // radSys
            // 
            this.radSys.Appearance = System.Windows.Forms.Appearance.Button;
            this.radSys.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radSys.Text = "⚙ Sistem";
            this.radSys.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.radSys.Location = new System.Drawing.Point(0, 270);
            this.radSys.Size = new System.Drawing.Size(220, 40);
            this.radSys.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.radSys.ForeColor = System.Drawing.Color.FromArgb(207, 224, 222);
            this.radSys.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.radSys.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radSys.Checked = false;
            this.radSys.Name = "radSys";
            this.radSys.CheckedChanged += new System.EventHandler(this.tab_CheckedChanged);
            // 
            // radLog
            // 
            this.radLog.Appearance = System.Windows.Forms.Appearance.Button;
            this.radLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radLog.Text = "📜 Admin jurnalı";
            this.radLog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.radLog.Location = new System.Drawing.Point(0, 312);
            this.radLog.Size = new System.Drawing.Size(220, 40);
            this.radLog.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.radLog.ForeColor = System.Drawing.Color.FromArgb(207, 224, 222);
            this.radLog.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.radLog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radLog.Checked = false;
            this.radLog.Name = "radLog";
            this.radLog.CheckedChanged += new System.EventHandler(this.tab_CheckedChanged);
            // 
            // btnExit
            // 
            this.btnExit.Kind = TexnikaReyestri.Controls.ButtonKind.Danger;
            this.btnExit.Location = new System.Drawing.Point(12, 640);
            this.btnExit.Size = new System.Drawing.Size(196, 38);
            this.btnExit.Text = "Admindən çıx";
            this.btnExit.Glyph = "\uF3B1";
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Name = "btnExit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Name = "pnlContent";
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1180, 700);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Texnika Reyestri · Admin";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlAside);
            this.Name = "AdminForm";
            this.Load += new System.EventHandler(this.AdminForm_Load);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.pnlAside.ResumeLayout(false);
            this.pnlAside.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlAside;
        private System.Windows.Forms.Label lblAside;
        private System.Windows.Forms.RadioButton radUsers;
        private System.Windows.Forms.RadioButton radOrgs;
        private System.Windows.Forms.RadioButton radPc;
        private System.Windows.Forms.RadioButton radSec;
        private System.Windows.Forms.RadioButton radKeys;
        private System.Windows.Forms.RadioButton radSys;
        private System.Windows.Forms.RadioButton radLog;
        private TexnikaReyestri.Controls.FlatButton btnExit;
        private System.Windows.Forms.Panel pnlContent;
    }
}
