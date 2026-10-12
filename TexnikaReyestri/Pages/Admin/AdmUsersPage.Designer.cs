namespace TexnikaReyestri.Pages.Admin
{
    partial class AdmUsersPage
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
            this.cmbOrg = new TexnikaReyestri.Controls.RoundComboBox();
            this.txtFilter = new TexnikaReyestri.Controls.RoundTextBox();
            this.rpGrid = new TexnikaReyestri.Controls.RoundPanel();
            this.grid = new TexnikaReyestri.Controls.ModernGrid();
            this.colLogin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRole = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLast = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colReset = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colBlock = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblFoot = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.pnlHead.SuspendLayout();
            this.rpGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHead
            // 
            this.pnlHead.Controls.Add(this.txtFilter);
            this.pnlHead.Controls.Add(this.cmbOrg);
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
            this.lblTitle.Text = "İstifadəçilər";
            this.lblTitle.Tag = "title";
            this.lblTitle.Name = "lblTitle";
            // 
            // btnAdd
            // 
            this.btnAdd.Kind = TexnikaReyestri.Controls.ButtonKind.Primary;
            this.btnAdd.Location = new System.Drawing.Point(758, 4);
            this.btnAdd.Size = new System.Drawing.Size(150, 34);
            this.btnAdd.Text = "Yeni istifadəçi";
            this.btnAdd.Glyph = "\uE710";
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cmbOrg
            // 
            this.cmbOrg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrg.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.cmbOrg.Location = new System.Drawing.Point(590, 8);
            this.cmbOrg.Size = new System.Drawing.Size(160, 25);
            this.cmbOrg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbOrg.Name = "cmbOrg";
            this.cmbOrg.SelectedIndexChanged += new System.EventHandler(this.filtr_Changed);
            // 
            // txtFilter
            // 
            this.txtFilter.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.txtFilter.Location = new System.Drawing.Point(390, 8);
            this.txtFilter.Size = new System.Drawing.Size(192, 25);
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.PlaceholderText = "Ad və ya login";
            this.txtFilter.Name = "txtFilter";
            // 
            // rpGrid
            // 
            this.rpGrid.Controls.Add(this.grid);
            this.rpGrid.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.rpGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpGrid.Size = new System.Drawing.Size(914, 602);
            this.rpGrid.Name = "rpGrid";
            // 
            // grid
            // 
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLogin,
            this.colName,
            this.colOrg,
            this.colRole,
            this.colLast,
            this.colState,
            this.colEdit,
            this.colReset,
            this.colBlock});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Size = new System.Drawing.Size(902, 590);
            this.grid.Name = "grid";
            this.grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellContentClick);
            // 
            // colLogin
            // 
            this.colLogin.HeaderText = "Login";
            this.colLogin.Name = "colLogin";
            this.colLogin.Width = 80;
            // 
            // colName
            // 
            this.colName.HeaderText = "Ad Soyad";
            this.colName.Name = "colName";
            this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colName.MinimumWidth = 140;
            // 
            // colOrg
            // 
            this.colOrg.HeaderText = "Qurum";
            this.colOrg.Name = "colOrg";
            this.colOrg.Width = 90;
            // 
            // colRole
            // 
            this.colRole.HeaderText = "Rol";
            this.colRole.Name = "colRole";
            this.colRole.Width = 110;
            // 
            // colLast
            // 
            this.colLast.HeaderText = "Son giriş";
            this.colLast.Name = "colLast";
            this.colLast.Width = 110;
            // 
            // colState
            // 
            this.colState.HeaderText = "Vəziyyət";
            this.colState.Name = "Status";
            this.colState.Width = 70;
            // 
            // colEdit
            // 
            this.colEdit.HeaderText = "";
            this.colEdit.Name = "colEdit";
            this.colEdit.Width = 76;
            this.colEdit.Text = "Redaktə";
            this.colEdit.UseColumnTextForButtonValue = true;
            // 
            // colReset
            // 
            this.colReset.HeaderText = "";
            this.colReset.Name = "colReset";
            this.colReset.Width = 106;
            this.colReset.Text = "Parolu sıfırla";
            this.colReset.UseColumnTextForButtonValue = true;
            // 
            // colBlock
            // 
            this.colBlock.HeaderText = "";
            this.colBlock.Name = "colBlock";
            this.colBlock.Width = 84;
            this.colBlock.Text = "Blokla";
            this.colBlock.UseColumnTextForButtonValue = true;
            // 
            // lblFoot
            // 
            this.lblFoot.AutoSize = false;
            this.lblFoot.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblFoot.Location = new System.Drawing.Point(0, 0);
            this.lblFoot.Text = "Silmək əvəzinə bloklayın: istifadəçinin etdiyi dəyişikliklər tarixçədə onun adı ilə qalır.";
            this.lblFoot.Tag = "muted";
            this.lblFoot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblFoot.Size = new System.Drawing.Size(914, 22);
            this.lblFoot.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFoot.Name = "lblFoot";
            // 
            // AdmUsersPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(958, 698);
            this.Controls.Add(this.rpGrid);
            this.Controls.Add(this.lblFoot);
            this.Controls.Add(this.pnlHead);
            this.Name = "AdmUsersPage";
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
        private TexnikaReyestri.Controls.FlatButton btnAdd;
        private TexnikaReyestri.Controls.RoundComboBox cmbOrg;
        private TexnikaReyestri.Controls.RoundTextBox txtFilter;
        private TexnikaReyestri.Controls.RoundPanel rpGrid;
        private TexnikaReyestri.Controls.ModernGrid grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLogin;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrg;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRole;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLast;
        private System.Windows.Forms.DataGridViewTextBoxColumn colState;
        private System.Windows.Forms.DataGridViewButtonColumn colEdit;
        private System.Windows.Forms.DataGridViewButtonColumn colReset;
        private System.Windows.Forms.DataGridViewButtonColumn colBlock;
        private System.Windows.Forms.Label lblFoot;
    }
}
