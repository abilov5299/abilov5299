namespace TexnikaReyestri.Pages
{
    partial class EditPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.pnlHead = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnSave = new TexnikaReyestri.Controls.FlatButton();
            this.btnNextG = new TexnikaReyestri.Controls.FlatButton();
            this.btnPrevG = new TexnikaReyestri.Controls.FlatButton();
            this.tabs = new TexnikaReyestri.Controls.PageTabs();
            this.tab1 = new System.Windows.Forms.TabPage();
            this.tab2 = new System.Windows.Forms.TabPage();
            this.tab3 = new System.Windows.Forms.TabPage();
            this.tab5 = new System.Windows.Forms.TabPage();
            this.chipTabs = new TexnikaReyestri.Controls.ChipTabBar();
            this.lblFoot = new System.Windows.Forms.Label();
            this.pnlHead.SuspendLayout();
            this.tabs.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlHead
            //
            this.pnlHead.Controls.Add(this.btnPrevG);
            this.pnlHead.Controls.Add(this.btnNextG);
            this.pnlHead.Controls.Add(this.btnSave);
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
            this.lblTitle.Text = "Texnika əlavə et";
            this.lblTitle.Tag = "title";
            this.lblTitle.Name = "lblTitle";
            //
            // btnSave
            //
            this.btnSave.Kind = TexnikaReyestri.Controls.ButtonKind.Primary;
            this.btnSave.Location = new System.Drawing.Point(1036, 4);
            this.btnSave.Size = new System.Drawing.Size(130, 34);
            this.btnSave.Text = "Yadda saxla";
            this.btnSave.Glyph = "";
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // btnNextG
            //
            this.btnNextG.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            this.btnNextG.Location = new System.Drawing.Point(876, 4);
            this.btnNextG.Size = new System.Drawing.Size(154, 34);
            this.btnNextG.Text = "Növbəti bölmə";
            this.btnNextG.Glyph = "";
            this.btnNextG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextG.Name = "btnNextG";
            this.btnNextG.Click += new System.EventHandler(this.btnNextG_Click);
            //
            // btnPrevG
            //
            this.btnPrevG.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            this.btnPrevG.Location = new System.Drawing.Point(716, 4);
            this.btnPrevG.Size = new System.Drawing.Size(154, 34);
            this.btnPrevG.Text = "Əvvəlki bölmə";
            this.btnPrevG.Glyph = "";
            this.btnPrevG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevG.Name = "btnPrevG";
            this.btnPrevG.Click += new System.EventHandler(this.btnPrevG_Click);
            //
            // tabs
            //
            this.tabs.Controls.Add(this.tab1);
            this.tabs.Controls.Add(this.tab2);
            this.tabs.Controls.Add(this.tab3);
            this.tabs.Controls.Add(this.tab5);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Size = new System.Drawing.Size(1172, 516);
            this.tabs.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndexChanged += new System.EventHandler(this.tabs_SelectedIndexChanged);
            //
            // tab1, tab2, tab3, tab5 (məzmun EditPage.cs-də qurulur)
            //
            this.tab1.Text = "1. Qəbul və sənədlər"; this.tab1.Name = "tab1"; this.tab1.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.tab2.Text = "2. İstismar göstəriciləri və son təmir"; this.tab2.Name = "tab2"; this.tab2.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.tab3.Text = "3. Təmirin icrası"; this.tab3.Name = "tab3"; this.tab3.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.tab5.Text = "4. Təmirin tamamlanması və təhvil"; this.tab5.Name = "tab5"; this.tab5.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            //
            // chipTabs
            //
            this.chipTabs.Dock = System.Windows.Forms.DockStyle.Top;
            this.chipTabs.Size = new System.Drawing.Size(1172, 50);
            this.chipTabs.Target = this.tabs;
            this.chipTabs.Name = "chipTabs";
            //
            // lblFoot
            //
            this.lblFoot.AutoSize = false;
            this.lblFoot.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblFoot.Text = "* məcburi sahələr. Tarix xanasında işarəni götürsəniz, tarix boş qalır.";
            this.lblFoot.Tag = "muted";
            this.lblFoot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblFoot.Size = new System.Drawing.Size(1172, 22);
            this.lblFoot.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFoot.Name = "lblFoot";
            //
            // EditPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(1216, 712);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.chipTabs);
            this.Controls.Add(this.lblFoot);
            this.Controls.Add(this.pnlHead);
            this.Name = "EditPage";
            this.Load += new System.EventHandler(this.EditPage_Load);
            this.tabs.ResumeLayout(false);
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Label lblTitle;
        private TexnikaReyestri.Controls.FlatButton btnSave;
        private TexnikaReyestri.Controls.FlatButton btnNextG;
        private TexnikaReyestri.Controls.FlatButton btnPrevG;
        private TexnikaReyestri.Controls.PageTabs tabs;
        private System.Windows.Forms.TabPage tab1, tab2, tab3, tab5;
        private TexnikaReyestri.Controls.ChipTabBar chipTabs;
        private System.Windows.Forms.Label lblFoot;
    }
}
