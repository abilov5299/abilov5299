namespace TexnikaReyestri.Forms
{
    partial class MainForm
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
            this.pnlNav = new System.Windows.Forms.Panel();
            this.logo = new TexnikaReyestri.Controls.LogoMark();
            this.navHome = new TexnikaReyestri.Controls.NavButton();
            this.navRegistry = new TexnikaReyestri.Controls.NavButton();
            this.navAdd = new TexnikaReyestri.Controls.NavButton();
            this.navTech = new TexnikaReyestri.Controls.NavButton();
            this.navRep = new TexnikaReyestri.Controls.NavButton();
            this.lblApp = new System.Windows.Forms.Label();
            this.navNoti = new TexnikaReyestri.Controls.NavButton();
            this.navSync = new TexnikaReyestri.Controls.NavButton();
            this.navLogs = new TexnikaReyestri.Controls.NavButton();
            this.navHelp = new TexnikaReyestri.Controls.NavButton();
            this.navOut = new TexnikaReyestri.Controls.NavButton();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblQurum = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.btnTheme = new TexnikaReyestri.Controls.FlatButton();
            this.cmbLang = new TexnikaReyestri.Controls.RoundComboBox();
            this.btnBell = new TexnikaReyestri.Controls.FlatButton();
            this.lblBadge = new System.Windows.Forms.Label();
            this.btnMin = new TexnikaReyestri.Controls.CaptionButton();
            this.btnMax = new TexnikaReyestri.Controls.CaptionButton();
            this.btnClose = new TexnikaReyestri.Controls.CaptionButton();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlNav.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlNav
            // 
            this.pnlNav.Controls.Add(this.navOut);
            this.pnlNav.Controls.Add(this.navLogs);
            this.pnlNav.Controls.Add(this.navHelp);
            this.pnlNav.Controls.Add(this.navSync);
            this.pnlNav.Controls.Add(this.navNoti);
            this.pnlNav.Controls.Add(this.navRep);
            this.pnlNav.Controls.Add(this.navTech);
            this.pnlNav.Controls.Add(this.navAdd);
            this.pnlNav.Controls.Add(this.navRegistry);
            this.pnlNav.Controls.Add(this.navHome);
            this.pnlNav.Controls.Add(this.lblApp);
            this.pnlNav.Controls.Add(this.logo);
            this.pnlNav.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlNav.Size = new System.Drawing.Size(64, 758);
            this.pnlNav.Tag = "nav";
            this.pnlNav.Name = "pnlNav";
            // 
            // logo
            // 
            this.logo.Location = new System.Drawing.Point(12, 12);
            this.logo.Size = new System.Drawing.Size(40, 40);
            this.logo.Name = "logo";
            this.logo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logo.Click += new System.EventHandler(this.logo_Click);
            // 
            // lblApp
            // 
            this.lblApp.AutoSize = true;
            this.lblApp.Font = new System.Drawing.Font("Bahnschrift SemiBold", 12.0F);
            this.lblApp.ForeColor = System.Drawing.Color.White;
            this.lblApp.Location = new System.Drawing.Point(62, 21);
            this.lblApp.Text = "Təmir Reyestri";
            this.lblApp.Visible = false;
            this.lblApp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblApp.Name = "lblApp";
            this.lblApp.Click += new System.EventHandler(this.logo_Click);
            // 
            // navHome
            // 
            this.navHome.Glyph = "\uE80F";
            this.navHome.Location = new System.Drawing.Point(0, 70);
            this.navHome.Size = new System.Drawing.Size(64, 52);
            this.navHome.ToolTipText = "Ana səhifə (statistika)";
            this.navHome.PageKey = "home";
            this.navHome.Name = "navHome";
            this.navHome.Click += new System.EventHandler(this.nav_Click);
            // 
            // navRegistry
            // 
            this.navRegistry.Glyph = "\uE8FD";
            this.navRegistry.Location = new System.Drawing.Point(0, 174);
            this.navRegistry.Size = new System.Drawing.Size(64, 52);
            this.navRegistry.ToolTipText = "Təmir reyestri";
            this.navRegistry.PageKey = "list";
            this.navRegistry.Name = "navRegistry";
            this.navRegistry.Click += new System.EventHandler(this.nav_Click);
            // 
            // navAdd
            // 
            this.navAdd.Glyph = "\uE710";
            this.navAdd.Location = new System.Drawing.Point(0, 226);
            this.navAdd.Size = new System.Drawing.Size(64, 52);
            this.navAdd.ToolTipText = "Texnika daxil et";
            this.navAdd.PageKey = "add";
            this.navAdd.Name = "navAdd";
            this.navAdd.Click += new System.EventHandler(this.nav_Click);
            // 
            // navTech
            // 
            this.navTech.Glyph = "\uE719";
            this.navTech.Size = new System.Drawing.Size(64, 52);
            this.navTech.ToolTipText = "Təchizat sorğuları";
            this.navTech.PageKey = "tech";
            this.navTech.Name = "navTech";
            this.navTech.Click += new System.EventHandler(this.nav_Click);
            // 
            // navRep
            // 
            this.navRep.Glyph = "\uE9F9";
            this.navRep.Size = new System.Drawing.Size(64, 52);
            this.navRep.ToolTipText = "Hesabatlar";
            this.navRep.PageKey = "rep";
            this.navRep.Name = "navRep";
            this.navRep.Click += new System.EventHandler(this.nav_Click);
            // 
            // navNoti
            // 
            this.navNoti.Glyph = "\uEA8F";
            this.navNoti.Location = new System.Drawing.Point(0, 278);
            this.navNoti.Size = new System.Drawing.Size(64, 52);
            this.navNoti.ToolTipText = "Bildirişlər";
            this.navNoti.PageKey = "noti";
            this.navNoti.Name = "navNoti";
            this.navNoti.Click += new System.EventHandler(this.nav_Click);
            // 
            // navSync
            // 
            this.navSync.Glyph = "\uE895";
            this.navSync.Location = new System.Drawing.Point(0, 330);
            this.navSync.Size = new System.Drawing.Size(64, 52);
            this.navSync.ToolTipText = "İxrac / İdxal (USB)";
            this.navSync.PageKey = "sync";
            this.navSync.Name = "navSync";
            this.navSync.Click += new System.EventHandler(this.nav_Click);
            // 
            // navLogs
            // 
            this.navLogs.Glyph = "\uE713";
            this.navLogs.Location = new System.Drawing.Point(0, 382);
            this.navLogs.Size = new System.Drawing.Size(64, 52);
            this.navLogs.ToolTipText = "Tənzimləmə";
            this.navLogs.PageKey = "set";
            this.navLogs.Name = "navLogs";
            this.navLogs.Click += new System.EventHandler(this.nav_Click);
            // 
            // navHelp
            // 
            this.navHelp.Glyph = "";
            this.navHelp.Location = new System.Drawing.Point(0, 430);
            this.navHelp.Size = new System.Drawing.Size(64, 52);
            this.navHelp.ToolTipText = "Təlimat";
            this.navHelp.PageKey = "help";
            this.navHelp.Name = "navHelp";
            this.navHelp.Click += new System.EventHandler(this.nav_Click);
            // 
            // navOut
            // 
            this.navOut.Glyph = "\uF3B1";
            this.navOut.Location = new System.Drawing.Point(0, 700);
            this.navOut.Size = new System.Drawing.Size(64, 52);
            this.navOut.ToolTipText = "Hesabdan çıxış";
            this.navOut.DangerStyle = true;
            this.navOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.navOut.Name = "navOut";
            this.navOut.Click += new System.EventHandler(this.navOut_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.btnClose);
            this.pnlTop.Controls.Add(this.btnMax);
            this.pnlTop.Controls.Add(this.btnMin);
            this.pnlTop.Controls.Add(this.lblBadge);
            this.pnlTop.Controls.Add(this.btnBell);
            this.pnlTop.Controls.Add(this.cmbLang);
            this.pnlTop.Controls.Add(this.btnTheme);
            this.pnlTop.Controls.Add(this.lblUser);
            this.pnlTop.Controls.Add(this.lblQurum);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Size = new System.Drawing.Size(1214, 48);
            this.pnlTop.Tag = "card";
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTop_MouseDown);
            this.pnlTop.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlTop_Paint);
            this.pnlTop.DoubleClick += new System.EventHandler(this.btnMax_Click);
            // 
            // lblQurum
            // 
            this.lblQurum.AutoSize = true;
            this.lblQurum.Font = new System.Drawing.Font("Bahnschrift SemiBold", 12.0F);
            this.lblQurum.Location = new System.Drawing.Point(20, 13);
            this.lblQurum.Text = "Zavod 3";
            this.lblQurum.Tag = "title";
            this.lblQurum.Name = "lblQurum";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblUser.Location = new System.Drawing.Point(100, 17);
            this.lblUser.Text = "Əli Məmmədov · zavod istifadəçisi";
            this.lblUser.Tag = "muted";
            this.lblUser.Name = "lblUser";
            // 
            // btnTheme
            // 
            this.btnTheme.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            this.btnTheme.Location = new System.Drawing.Point(878, 8);
            this.btnTheme.Size = new System.Drawing.Size(36, 32);
            this.btnTheme.Text = "";
            this.btnTheme.Glyph = "\uE708";
            this.btnTheme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTheme.Name = "btnTheme";
            this.btnTheme.Click += new System.EventHandler(this.btnTheme_Click);
            // 
            // cmbLang
            // 
            this.cmbLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLang.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F);
            this.cmbLang.Location = new System.Drawing.Point(920, 11);
            this.cmbLang.Size = new System.Drawing.Size(56, 25);
            this.cmbLang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLang.Name = "cmbLang";
            this.cmbLang.Items.AddRange(new object[] {
            "AZ",
            "EN",
            "RU"});
            this.cmbLang.SelectedIndexChanged += new System.EventHandler(this.cmbLang_SelectedIndexChanged);
            // 
            // btnBell
            // 
            this.btnBell.Kind = TexnikaReyestri.Controls.ButtonKind.Ghost;
            this.btnBell.Location = new System.Drawing.Point(984, 8);
            this.btnBell.Size = new System.Drawing.Size(40, 32);
            this.btnBell.Text = "";
            this.btnBell.Glyph = "\uEA8F";
            this.btnBell.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBell.Name = "btnBell";
            this.btnBell.Click += new System.EventHandler(this.btnBell_Click);
            // 
            // lblBadge
            // 
            this.lblBadge.AutoSize = true;
            this.lblBadge.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblBadge.Location = new System.Drawing.Point(1008, 4);
            this.lblBadge.Text = "0";
            this.lblBadge.Tag = "white";
            this.lblBadge.BackColor = System.Drawing.Color.FromArgb(201, 74, 68);
            this.lblBadge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBadge.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.lblBadge.Name = "lblBadge";
            // 
            // btnMin
            // 
            this.btnMin.Kind = TexnikaReyestri.Controls.CaptionKind.Minimize;
            this.btnMin.Location = new System.Drawing.Point(1044, 0);
            this.btnMin.Size = new System.Drawing.Size(46, 47);
            this.btnMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMin.Name = "btnMin";
            // 
            // btnMax
            // 
            this.btnMax.Kind = TexnikaReyestri.Controls.CaptionKind.Maximize;
            this.btnMax.Location = new System.Drawing.Point(1090, 0);
            this.btnMax.Size = new System.Drawing.Size(46, 47);
            this.btnMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMax.Name = "btnMax";
            // 
            // btnClose
            // 
            this.btnClose.Kind = TexnikaReyestri.Controls.CaptionKind.Close;
            this.btnClose.Location = new System.Drawing.Point(1136, 0);
            this.btnClose.Size = new System.Drawing.Size(46, 47);
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Name = "btnClose";
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Size = new System.Drawing.Size(1214, 710);
            this.pnlContent.Name = "pnlContent";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1280, 760);
            this.MinimumSize = new System.Drawing.Size(1100, 680);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Təmir Reyestri";
            this.KeyPreview = true;
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlNav);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlNav.ResumeLayout(false);
            this.pnlNav.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlNav;
        private TexnikaReyestri.Controls.LogoMark logo;
        private TexnikaReyestri.Controls.NavButton navHome;
        private TexnikaReyestri.Controls.NavButton navRegistry;
        private TexnikaReyestri.Controls.NavButton navAdd;
        private TexnikaReyestri.Controls.NavButton navTech;
        private TexnikaReyestri.Controls.NavButton navRep;
        private System.Windows.Forms.Label lblApp;
        private TexnikaReyestri.Controls.NavButton navNoti;
        private TexnikaReyestri.Controls.NavButton navSync;
        private TexnikaReyestri.Controls.NavButton navLogs;
        private TexnikaReyestri.Controls.NavButton navHelp;
        private TexnikaReyestri.Controls.NavButton navOut;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblQurum;
        private System.Windows.Forms.Label lblUser;
        private TexnikaReyestri.Controls.FlatButton btnTheme;
        private TexnikaReyestri.Controls.RoundComboBox cmbLang;
        private TexnikaReyestri.Controls.FlatButton btnBell;
        private System.Windows.Forms.Label lblBadge;
        private TexnikaReyestri.Controls.CaptionButton btnMin;
        private TexnikaReyestri.Controls.CaptionButton btnMax;
        private TexnikaReyestri.Controls.CaptionButton btnClose;
        private System.Windows.Forms.Panel pnlContent;
    }
}
