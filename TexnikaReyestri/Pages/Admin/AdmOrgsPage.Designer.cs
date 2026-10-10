namespace TexnikaReyestri.Pages.Admin
{
    partial class AdmOrgsPage
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
            this.btnAdd = new TexnikaReyestri.Controls.FlatButton();
            this.tlp = new System.Windows.Forms.TableLayoutPanel();
            this.rpTree = new TexnikaReyestri.Controls.RoundPanel();
            this.lblTreeTitle = new System.Windows.Forms.Label();
            this.tree = new System.Windows.Forms.TreeView();
            this.rpList = new TexnikaReyestri.Controls.RoundPanel();
            this.lblListTitle = new System.Windows.Forms.Label();
            this.grid = new TexnikaReyestri.Controls.ModernGrid();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colParent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlListFoot = new System.Windows.Forms.Panel();
            this.lblHint = new System.Windows.Forms.Label();
            this.btnSave = new TexnikaReyestri.Controls.FlatButton();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.pnlHead.SuspendLayout();
            this.tlp.SuspendLayout();
            this.rpTree.SuspendLayout();
            this.rpList.SuspendLayout();
            this.pnlListFoot.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHead
            // 
            this.pnlHead.Controls.Add(this.btnAdd);
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
            this.lblTitle.Text = "Qurumlar";
            this.lblTitle.Tag = "title";
            this.lblTitle.Name = "lblTitle";
            // 
            // btnAdd
            // 
            this.btnAdd.Kind = TexnikaReyestri.Controls.ButtonKind.Primary;
            this.btnAdd.Location = new System.Drawing.Point(828, 4);
            this.btnAdd.Size = new System.Drawing.Size(130, 34);
            this.btnAdd.Text = "Yeni zavod";
            this.btnAdd.Glyph = "\uE710";
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tlp
            // 
            this.tlp.ColumnCount = 2;
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0F));
            this.tlp.RowCount = 1;
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0F));
            this.tlp.Controls.Add(this.rpTree, 0, 0);
            this.tlp.Controls.Add(this.rpList, 1, 0);
            this.tlp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp.Size = new System.Drawing.Size(914, 624);
            this.tlp.Name = "tlp";
            // 
            // rpTree
            // 
            this.rpTree.Controls.Add(this.tree);
            this.rpTree.Controls.Add(this.lblTreeTitle);
            this.rpTree.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpTree.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.rpTree.Name = "rpTree";
            // 
            // lblTreeTitle
            // 
            this.lblTreeTitle.AutoSize = false;
            this.lblTreeTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblTreeTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTreeTitle.Text = "Struktur";
            this.lblTreeTitle.Tag = "muted";
            this.lblTreeTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTreeTitle.Height = 26;
            this.lblTreeTitle.Name = "lblTreeTitle";
            // 
            // tree
            // 
            this.tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tree.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.tree.ShowLines = true;
            this.tree.ItemHeight = 26;
            this.tree.Name = "tree";
            // 
            // rpList
            // 
            this.rpList.Controls.Add(this.grid);
            this.rpList.Controls.Add(this.pnlListFoot);
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
            this.lblListTitle.Text = "Zavodlar";
            this.lblListTitle.Tag = "muted";
            this.lblListTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblListTitle.Height = 26;
            this.lblListTitle.Name = "lblListTitle";
            // 
            // grid
            // 
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCode,
            this.colName,
            this.colParent,
            this.colUsers});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.ReadOnly = false;
            this.grid.Name = "grid";
            // 
            // colCode
            // 
            this.colCode.HeaderText = "Kod";
            this.colCode.Name = "colCode";
            this.colCode.Width = 70;
            // 
            // colName
            // 
            this.colName.HeaderText = "Ad";
            this.colName.Name = "colName";
            this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colName.MinimumWidth = 100;
            // 
            // colParent
            // 
            this.colParent.HeaderText = "Tabe olduğu";
            this.colParent.Name = "colParent";
            this.colParent.Width = 130;
            // 
            // colUsers
            // 
            this.colUsers.HeaderText = "İstifadəçi";
            this.colUsers.Name = "colUsers";
            this.colUsers.Width = 90;
            // 
            // pnlListFoot
            // 
            this.pnlListFoot.Controls.Add(this.btnSave);
            this.pnlListFoot.Controls.Add(this.lblHint);
            this.pnlListFoot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlListFoot.Height = 52;
            this.pnlListFoot.Name = "pnlListFoot";
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblHint.Location = new System.Drawing.Point(0, 18);
            this.lblHint.Text = "Kod paket adlarında və açarlarda istifadə olunur, dəyişmir. Ad dəyişə bilər.";
            this.lblHint.Tag = "muted";
            this.lblHint.Name = "lblHint";
            // 
            // btnSave
            // 
            this.btnSave.Kind = TexnikaReyestri.Controls.ButtonKind.Primary;
            this.btnSave.Location = new System.Drawing.Point(380, 10);
            this.btnSave.Size = new System.Drawing.Size(170, 34);
            this.btnSave.Text = "Adları yadda saxla";
            this.btnSave.Glyph = "\uE74E";
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // AdmOrgsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(958, 698);
            this.Controls.Add(this.tlp);
            this.Controls.Add(this.pnlHead);
            this.Name = "AdmOrgsPage";
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.pnlListFoot.ResumeLayout(false);
            this.pnlListFoot.PerformLayout();
            this.rpList.ResumeLayout(false);
            this.rpList.PerformLayout();
            this.rpTree.ResumeLayout(false);
            this.rpTree.PerformLayout();
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
        private TexnikaReyestri.Controls.FlatButton btnAdd;
        private System.Windows.Forms.TableLayoutPanel tlp;
        private TexnikaReyestri.Controls.RoundPanel rpTree;
        private System.Windows.Forms.Label lblTreeTitle;
        private System.Windows.Forms.TreeView tree;
        private TexnikaReyestri.Controls.RoundPanel rpList;
        private System.Windows.Forms.Label lblListTitle;
        private TexnikaReyestri.Controls.ModernGrid grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParent;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsers;
        private System.Windows.Forms.Panel pnlListFoot;
        private System.Windows.Forms.Label lblHint;
        private TexnikaReyestri.Controls.FlatButton btnSave;
    }
}
