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
            pnlNav = new Panel();
            navOut = new TexnikaReyestri.Controls.NavButton();
            navBackup = new TexnikaReyestri.Controls.NavButton();
            navLogs = new TexnikaReyestri.Controls.NavButton();
            navSync = new TexnikaReyestri.Controls.NavButton();
            navNoti = new TexnikaReyestri.Controls.NavButton();
            navAdd = new TexnikaReyestri.Controls.NavButton();
            navRegistry = new TexnikaReyestri.Controls.NavButton();
            navSummary = new TexnikaReyestri.Controls.NavButton();
            navHome = new TexnikaReyestri.Controls.NavButton();
            logo = new TexnikaReyestri.Controls.LogoMark();
            pnlTop = new Panel();
            btnClose = new TexnikaReyestri.Controls.CaptionButton();
            btnMax = new TexnikaReyestri.Controls.CaptionButton();
            btnMin = new TexnikaReyestri.Controls.CaptionButton();
            lblBadge = new Label();
            btnBell = new TexnikaReyestri.Controls.FlatButton();
            cmbLang = new TexnikaReyestri.Controls.RoundComboBox();
            btnTheme = new TexnikaReyestri.Controls.FlatButton();
            lblUser = new Label();
            lblQurum = new Label();
            pnlContent = new Panel();
            pnlNav.SuspendLayout();
            pnlTop.SuspendLayout();
            SuspendLayout();
            // 
            // pnlNav
            // 
            pnlNav.Controls.Add(navOut);
            pnlNav.Controls.Add(navBackup);
            pnlNav.Controls.Add(navLogs);
            pnlNav.Controls.Add(navSync);
            pnlNav.Controls.Add(navNoti);
            pnlNav.Controls.Add(navAdd);
            pnlNav.Controls.Add(navRegistry);
            pnlNav.Controls.Add(navSummary);
            pnlNav.Controls.Add(navHome);
            pnlNav.Controls.Add(logo);
            pnlNav.Dock = DockStyle.Left;
            pnlNav.Location = new Point(1, 1);
            pnlNav.Name = "pnlNav";
            pnlNav.Size = new Size(64, 758);
            pnlNav.TabIndex = 2;
            pnlNav.Tag = "nav";
            // 
            // navOut
            // 
            navOut.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            navOut.DangerStyle = true;
            navOut.Glyph = "";
            navOut.Location = new Point(0, 700);
            navOut.Name = "navOut";
            navOut.PageKey = "";
            navOut.Size = new Size(64, 52);
            navOut.TabIndex = 0;
            navOut.ToolTipText = "Hesabdan çıxış";
            navOut.Click += navOut_Click;
            // 
            // navBackup
            // 
            navBackup.Glyph = "";
            navBackup.Location = new Point(0, 434);
            navBackup.Name = "navBackup";
            navBackup.PageKey = "bak";
            navBackup.Size = new Size(64, 52);
            navBackup.TabIndex = 1;
            navBackup.ToolTipText = "Ehtiyat nüsxə";
            navBackup.Click += nav_Click;
            // 
            // navLogs
            // 
            navLogs.Glyph = "";
            navLogs.Location = new Point(0, 382);
            navLogs.Name = "navLogs";
            navLogs.PageKey = "log";
            navLogs.Size = new Size(64, 52);
            navLogs.TabIndex = 2;
            navLogs.ToolTipText = "Loglar";
            navLogs.Click += nav_Click;
            // 
            // navSync
            // 
            navSync.Glyph = "";
            navSync.Location = new Point(0, 330);
            navSync.Name = "navSync";
            navSync.PageKey = "sync";
            navSync.Size = new Size(64, 52);
            navSync.TabIndex = 3;
            navSync.ToolTipText = "İxrac / İdxal (USB)";
            navSync.Click += nav_Click;
            // 
            // navNoti
            // 
            navNoti.Glyph = "";
            navNoti.Location = new Point(0, 278);
            navNoti.Name = "navNoti";
            navNoti.PageKey = "noti";
            navNoti.Size = new Size(64, 52);
            navNoti.TabIndex = 4;
            navNoti.ToolTipText = "Bildirişlər";
            navNoti.Click += nav_Click;
            // 
            // navAdd
            // 
            navAdd.Glyph = "";
            navAdd.Location = new Point(0, 226);
            navAdd.Name = "navAdd";
            navAdd.PageKey = "add";
            navAdd.Size = new Size(64, 52);
            navAdd.TabIndex = 5;
            navAdd.ToolTipText = "Texnika daxil et";
            navAdd.Click += nav_Click;
            // 
            // navRegistry
            // 
            navRegistry.Glyph = "";
            navRegistry.Location = new Point(0, 174);
            navRegistry.Name = "navRegistry";
            navRegistry.PageKey = "list";
            navRegistry.Size = new Size(64, 52);
            navRegistry.TabIndex = 6;
            navRegistry.ToolTipText = "Reyestr";
            navRegistry.Click += nav_Click;
            // 
            // navSummary
            // 
            navSummary.Glyph = "";
            navSummary.Location = new Point(0, 122);
            navSummary.Name = "navSummary";
            navSummary.PageKey = "sum";
            navSummary.Size = new Size(64, 52);
            navSummary.TabIndex = 7;
            navSummary.ToolTipText = "İcmal (zavodlar üzrə)";
            navSummary.Click += nav_Click;
            // 
            // navHome
            // 
            navHome.Glyph = "";
            navHome.Location = new Point(0, 70);
            navHome.Name = "navHome";
            navHome.PageKey = "home";
            navHome.Size = new Size(64, 52);
            navHome.TabIndex = 8;
            navHome.ToolTipText = "Ana səhifə (statistika)";
            navHome.Click += nav_Click;
            // 
            // logo
            // 
            logo.BackColor = Color.Transparent;
            logo.Location = new Point(12, 12);
            logo.Name = "logo";
            logo.Size = new Size(40, 40);
            logo.TabIndex = 9;
            // 
            // pnlTop
            // 
            pnlTop.Controls.Add(btnClose);
            pnlTop.Controls.Add(btnMax);
            pnlTop.Controls.Add(btnMin);
            pnlTop.Controls.Add(lblBadge);
            pnlTop.Controls.Add(btnBell);
            pnlTop.Controls.Add(cmbLang);
            pnlTop.Controls.Add(btnTheme);
            pnlTop.Controls.Add(lblUser);
            pnlTop.Controls.Add(lblQurum);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(65, 1);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(1214, 48);
            pnlTop.TabIndex = 1;
            pnlTop.Tag = "card";
            pnlTop.Paint += pnlTop_Paint;
            pnlTop.DoubleClick += btnMax_Click;
            pnlTop.MouseDown += pnlTop_MouseDown;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Kind = TexnikaReyestri.Controls.CaptionKind.Close;
            btnClose.Location = new Point(1142, -1);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(46, 47);
            btnClose.TabIndex = 0;
            // 
            // btnMax
            // 
            btnMax.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMax.Kind = TexnikaReyestri.Controls.CaptionKind.Maximize;
            btnMax.Location = new Point(1090, 0);
            btnMax.Name = "btnMax";
            btnMax.Size = new Size(46, 47);
            btnMax.TabIndex = 1;
            // 
            // btnMin
            // 
            btnMin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMin.Location = new Point(1044, 0);
            btnMin.Name = "btnMin";
            btnMin.Size = new Size(46, 47);
            btnMin.TabIndex = 2;
            // 
            // lblBadge
            // 
            lblBadge.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblBadge.AutoSize = true;
            lblBadge.BackColor = Color.FromArgb(201, 74, 68);
            lblBadge.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblBadge.Location = new Point(1008, 4);
            lblBadge.Name = "lblBadge";
            lblBadge.Padding = new Padding(3, 0, 3, 0);
            lblBadge.Size = new Size(17, 12);
            lblBadge.TabIndex = 3;
            lblBadge.Tag = "white";
            lblBadge.Text = "0";
            // 
            // btnBell
            // 
            btnBell.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBell.BackColor = Color.Transparent;
            btnBell.FlatStyle = FlatStyle.Flat;
            btnBell.Font = new Font("Segoe UI Semibold", 9F);
            btnBell.Glyph = "";
            btnBell.Kind = TexnikaReyestri.Controls.ButtonKind.Ghost;
            btnBell.Location = new Point(984, 8);
            btnBell.Name = "btnBell";
            btnBell.Size = new Size(40, 32);
            btnBell.TabIndex = 4;
            btnBell.UseVisualStyleBackColor = false;
            btnBell.Click += btnBell_Click;
            // 
            // cmbLang
            // 
            cmbLang.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbLang.DrawMode = DrawMode.OwnerDrawFixed;
            cmbLang.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLang.FlatStyle = FlatStyle.Flat;
            cmbLang.Font = new Font("Segoe UI Semibold", 9.5F);
            cmbLang.IntegralHeight = false;
            cmbLang.ItemHeight = 22;
            cmbLang.Items.AddRange(new object[] { "AZ", "EN", "RU" });
            cmbLang.Location = new Point(920, 11);
            cmbLang.Name = "cmbLang";
            cmbLang.Size = new Size(56, 28);
            cmbLang.TabIndex = 5;
            cmbLang.SelectedIndexChanged += cmbLang_SelectedIndexChanged;
            // 
            // btnTheme
            // 
            btnTheme.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTheme.BackColor = Color.Transparent;
            btnTheme.FlatStyle = FlatStyle.Flat;
            btnTheme.Font = new Font("Segoe UI Semibold", 9F);
            btnTheme.Glyph = "";
            btnTheme.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            btnTheme.Location = new Point(878, 8);
            btnTheme.Name = "btnTheme";
            btnTheme.Size = new Size(36, 32);
            btnTheme.TabIndex = 6;
            btnTheme.UseVisualStyleBackColor = false;
            btnTheme.Click += btnTheme_Click;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Font = new Font("Segoe UI", 9F);
            lblUser.Location = new Point(100, 17);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(188, 15);
            lblUser.TabIndex = 7;
            lblUser.Tag = "muted";
            lblUser.Text = "Əli Məmmədov · zavod istifadəçisi";
            // 
            // lblQurum
            // 
            lblQurum.AutoSize = true;
            lblQurum.Font = new Font("Bahnschrift SemiBold", 12F);
            lblQurum.Location = new Point(20, 13);
            lblQurum.Name = "lblQurum";
            lblQurum.Size = new Size(65, 19);
            lblQurum.TabIndex = 8;
            lblQurum.Tag = "title";
            lblQurum.Text = "Zavod 3";
            // 
            // pnlContent
            // 
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(65, 49);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(1214, 710);
            pnlContent.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1280, 760);
            Controls.Add(pnlContent);
            Controls.Add(pnlTop);
            Controls.Add(pnlNav);
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            MinimumSize = new Size(1100, 680);
            Name = "MainForm";
            Padding = new Padding(1);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Texnika Reyestri";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            KeyDown += MainForm_KeyDown;
            pnlNav.ResumeLayout(false);
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlNav;
        private TexnikaReyestri.Controls.LogoMark logo;
        private TexnikaReyestri.Controls.NavButton navHome;
        private TexnikaReyestri.Controls.NavButton navSummary;
        private TexnikaReyestri.Controls.NavButton navRegistry;
        private TexnikaReyestri.Controls.NavButton navAdd;
        private TexnikaReyestri.Controls.NavButton navNoti;
        private TexnikaReyestri.Controls.NavButton navSync;
        private TexnikaReyestri.Controls.NavButton navLogs;
        private TexnikaReyestri.Controls.NavButton navBackup;
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
        private System.Windows.Forms.Panel pnlContent;
        private Controls.CaptionButton btnClose;
    }
}
