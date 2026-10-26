namespace TexnikaReyestri.Forms
{
    partial class ExcelExportForm
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTop = new System.Windows.Forms.Label();
            this.pnlFoot = new System.Windows.Forms.Panel();
            this.lblSize = new System.Windows.Forms.Label();
            this.btnOk = new TexnikaReyestri.Controls.FlatButton();
            this.btnCancel = new TexnikaReyestri.Controls.FlatButton();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.lWhat = new System.Windows.Forms.Label();
            this.radFiltered = new System.Windows.Forms.RadioButton();
            this.radAll = new System.Windows.Forms.RadioButton();
            this.radSelected = new System.Windows.Forms.RadioButton();
            this.lCols = new System.Windows.Forms.Label();
            this.radFive = new System.Windows.Forms.RadioButton();
            this.radAllCols = new System.Windows.Forms.RadioButton();
            this.lLook = new System.Windows.Forms.Label();
            this.chkGroups = new System.Windows.Forms.CheckBox();
            this.chkStatus = new System.Windows.Forms.CheckBox();
            this.chkLate = new System.Windows.Forms.CheckBox();
            this.chkFreeze = new System.Windows.Forms.CheckBox();
            this.chkSummary = new System.Windows.Forms.CheckBox();
            this.chkOpen = new System.Windows.Forms.CheckBox();
            this.pnlTop.SuspendLayout();
            this.pnlFoot.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lblTop);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Size = new System.Drawing.Size(724, 42);
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(15, 76, 74);
            this.pnlTop.Name = "pnlTop";
            // 
            // lblTop
            // 
            this.lblTop.AutoSize = true;
            this.lblTop.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F);
            this.lblTop.Location = new System.Drawing.Point(16, 11);
            this.lblTop.Text = "Excel-ə ixrac";
            this.lblTop.Tag = "white";
            this.lblTop.Name = "lblTop";
            // 
            // pnlFoot
            // 
            this.pnlFoot.Controls.Add(this.btnCancel);
            this.pnlFoot.Controls.Add(this.btnOk);
            this.pnlFoot.Controls.Add(this.lblSize);
            this.pnlFoot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFoot.Size = new System.Drawing.Size(724, 58);
            this.pnlFoot.Tag = "alt";
            this.pnlFoot.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);
            this.pnlFoot.Name = "pnlFoot";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblSize.Location = new System.Drawing.Point(16, 20);
            this.lblSize.Text = "";
            this.lblSize.Tag = "muted";
            this.lblSize.Name = "lblSize";
            // 
            // btnOk
            // 
            this.btnOk.Kind = TexnikaReyestri.Controls.ButtonKind.Primary;
            this.btnOk.Location = new System.Drawing.Point(578, 11);
            this.btnOk.Size = new System.Drawing.Size(130, 36);
            this.btnOk.Text = "İxrac et";
            this.btnOk.Glyph = "\uE896";
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Name = "btnOk";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            this.btnCancel.Location = new System.Drawing.Point(440, 11);
            this.btnCancel.Size = new System.Drawing.Size(130, 36);
            this.btnCancel.Text = "Ləğv et";
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.chkOpen);
            this.pnlBody.Controls.Add(this.chkSummary);
            this.pnlBody.Controls.Add(this.chkFreeze);
            this.pnlBody.Controls.Add(this.chkLate);
            this.pnlBody.Controls.Add(this.chkStatus);
            this.pnlBody.Controls.Add(this.chkGroups);
            this.pnlBody.Controls.Add(this.lLook);
            this.pnlBody.Controls.Add(this.radAllCols);
            this.pnlBody.Controls.Add(this.radFive);
            this.pnlBody.Controls.Add(this.lCols);
            this.pnlBody.Controls.Add(this.radSelected);
            this.pnlBody.Controls.Add(this.radAll);
            this.pnlBody.Controls.Add(this.radFiltered);
            this.pnlBody.Controls.Add(this.lWhat);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Size = new System.Drawing.Size(724, 218);
            this.pnlBody.Tag = "card";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(20, 14, 20, 10);
            this.pnlBody.Name = "pnlBody";
            // 
            // lWhat
            // 
            this.lWhat.AutoSize = true;
            this.lWhat.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lWhat.Location = new System.Drawing.Point(20, 14);
            this.lWhat.Text = "Nə ixrac olunsun";
            this.lWhat.Tag = "muted";
            this.lWhat.Name = "lWhat";
            // 
            // radFiltered
            // 
            this.radFiltered.AutoSize = true;
            this.radFiltered.Text = "Ekrandakı filtrlənmiş siyahı";
            this.radFiltered.Location = new System.Drawing.Point(20, 40);
            this.radFiltered.Checked = true;
            this.radFiltered.Name = "radFiltered";
            // 
            // radAll
            // 
            this.radAll.AutoSize = true;
            this.radAll.Text = "Bütün reyestr";
            this.radAll.Location = new System.Drawing.Point(20, 64);
            this.radAll.Name = "radAll";
            // 
            // radSelected
            // 
            this.radSelected.AutoSize = true;
            this.radSelected.Text = "Yalnız seçilmiş sətir";
            this.radSelected.Location = new System.Drawing.Point(20, 88);
            this.radSelected.Name = "radSelected";
            // 
            // lCols
            // 
            this.lCols.AutoSize = true;
            this.lCols.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lCols.Location = new System.Drawing.Point(20, 124);
            this.lCols.Text = "Sütunlar";
            this.lCols.Tag = "muted";
            this.lCols.Name = "lCols";
            // 
            // radFive
            // 
            this.radFive.AutoSize = true;
            this.radFive.Text = "Yalnız reyestr sütunları";
            this.radFive.Location = new System.Drawing.Point(20, 150);
            this.radFive.Name = "radFive";
            // 
            // radAllCols
            // 
            this.radAllCols.AutoSize = true;
            this.radAllCols.Text = "Bütün sütunlar (orijinal reyestr formatı)";
            this.radAllCols.Location = new System.Drawing.Point(20, 174);
            this.radAllCols.Checked = true;
            this.radAllCols.Name = "radAllCols";
            // 
            // lLook
            // 
            this.lLook.AutoSize = true;
            this.lLook.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lLook.Location = new System.Drawing.Point(370, 14);
            this.lLook.Text = "Görünüş";
            this.lLook.Tag = "muted";
            this.lLook.Name = "lLook";
            // 
            // chkGroups
            // 
            this.chkGroups.AutoSize = true;
            this.chkGroups.Text = "Bölmə başlıqları (Qəbul və sənədlər, Təmirin icrası...)";
            this.chkGroups.Location = new System.Drawing.Point(370, 40);
            this.chkGroups.Checked = true;
            this.chkGroups.Name = "chkGroups";
            // 
            // chkStatus
            // 
            this.chkStatus.AutoSize = true;
            this.chkStatus.Text = "Statusa görə rəngli xanalar";
            this.chkStatus.Location = new System.Drawing.Point(370, 64);
            this.chkStatus.Checked = true;
            this.chkStatus.Name = "chkStatus";
            // 
            // chkLate
            // 
            this.chkLate.AutoSize = true;
            this.chkLate.Text = "Gecikən tarixləri qırmızı göstər";
            this.chkLate.Location = new System.Drawing.Point(370, 88);
            this.chkLate.Checked = true;
            this.chkLate.Name = "chkLate";
            // 
            // chkFreeze
            // 
            this.chkFreeze.AutoSize = true;
            this.chkFreeze.Text = "Başlığı dondur və filtr əlavə et";
            this.chkFreeze.Location = new System.Drawing.Point(370, 112);
            this.chkFreeze.Checked = true;
            this.chkFreeze.Name = "chkFreeze";
            // 
            // chkSummary
            // 
            this.chkSummary.AutoSize = true;
            this.chkSummary.Text = "İkinci vərəq: zavodlar üzrə icmal";
            this.chkSummary.Location = new System.Drawing.Point(370, 136);
            this.chkSummary.Checked = true;
            this.chkSummary.Name = "chkSummary";
            // 
            // chkOpen
            // 
            this.chkOpen.AutoSize = true;
            this.chkOpen.Text = "Hazır olanda faylı aç";
            this.chkOpen.Location = new System.Drawing.Point(370, 174);
            this.chkOpen.Checked = true;
            this.chkOpen.Name = "chkOpen";
            // 
            // ExcelExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(726, 320);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ShowInTaskbar = false;
            this.Padding = new System.Windows.Forms.Padding(1);
            this.AcceptButton = this.btnOk;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFoot);
            this.Controls.Add(this.pnlTop);
            this.Name = "ExcelExportForm";
            this.Load += new System.EventHandler(this.ExcelExportForm_Load);
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.pnlFoot.ResumeLayout(false);
            this.pnlFoot.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTop;
        private System.Windows.Forms.Panel pnlFoot;
        private System.Windows.Forms.Label lblSize;
        private TexnikaReyestri.Controls.FlatButton btnOk;
        private TexnikaReyestri.Controls.FlatButton btnCancel;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Label lWhat;
        private System.Windows.Forms.RadioButton radFiltered;
        private System.Windows.Forms.RadioButton radAll;
        private System.Windows.Forms.RadioButton radSelected;
        private System.Windows.Forms.Label lCols;
        private System.Windows.Forms.RadioButton radFive;
        private System.Windows.Forms.RadioButton radAllCols;
        private System.Windows.Forms.Label lLook;
        private System.Windows.Forms.CheckBox chkGroups;
        private System.Windows.Forms.CheckBox chkStatus;
        private System.Windows.Forms.CheckBox chkLate;
        private System.Windows.Forms.CheckBox chkFreeze;
        private System.Windows.Forms.CheckBox chkSummary;
        private System.Windows.Forms.CheckBox chkOpen;
    }
}
