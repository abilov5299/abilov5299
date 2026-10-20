namespace TexnikaReyestri.Pages
{
    partial class DetailPage
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
            this.rpHead = new TexnikaReyestri.Controls.RoundPanel();
            this.btnBack = new TexnikaReyestri.Controls.FlatButton();
            this.lblName = new System.Windows.Forms.Label();
            this.lblSub = new System.Windows.Forms.Label();
            this.chipStatus = new TexnikaReyestri.Controls.StatusChip();
            this.chipLate = new TexnikaReyestri.Controls.StatusChip();
            this.btnEdit = new TexnikaReyestri.Controls.FlatButton();
            this.btnPdf = new TexnikaReyestri.Controls.FlatButton();
            this.btnNext = new TexnikaReyestri.Controls.FlatButton();
            this.btnPrev = new TexnikaReyestri.Controls.FlatButton();
            this.rpStep = new TexnikaReyestri.Controls.RoundPanel();
            this.stepper = new TexnikaReyestri.Controls.StepperControl();
            this.pnlGap = new System.Windows.Forms.Panel();
            this.scroll = new TexnikaReyestri.Controls.ScrollPanel();
            this.rpHist = new TexnikaReyestri.Controls.RoundPanel();
            this.lblHistTitle = new System.Windows.Forms.Label();
            this.flHist = new System.Windows.Forms.FlowLayoutPanel();
            this.rpHead.SuspendLayout();
            this.rpStep.SuspendLayout();
            this.pnlGap.SuspendLayout();
            this.scroll.SuspendLayout();
            this.rpHist.SuspendLayout();
            this.flHist.SuspendLayout();
            this.SuspendLayout();
            // 
            // rpHead
            // 
            this.rpHead.Controls.Add(this.btnPrev);
            this.rpHead.Controls.Add(this.btnNext);
            this.rpHead.Controls.Add(this.btnPdf);
            this.rpHead.Controls.Add(this.btnEdit);
            this.rpHead.Controls.Add(this.chipLate);
            this.rpHead.Controls.Add(this.chipStatus);
            this.rpHead.Controls.Add(this.lblSub);
            this.rpHead.Controls.Add(this.lblName);
            this.rpHead.Controls.Add(this.btnBack);
            this.rpHead.Padding = new System.Windows.Forms.Padding(14, 12, 14, 12);
            this.rpHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.rpHead.Size = new System.Drawing.Size(1172, 78);
            this.rpHead.Name = "rpHead";
            // 
            // btnBack
            // 
            this.btnBack.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            this.btnBack.Location = new System.Drawing.Point(14, 20);
            this.btnBack.Size = new System.Drawing.Size(150, 34);
            this.btnBack.Text = "Reyestrə qayıt";
            this.btnBack.Glyph = "\uE72B";
            this.btnBack.Name = "btnBack";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Bahnschrift SemiBold", 17.0F);
            this.lblName.Location = new System.Drawing.Point(178, 14);
            this.lblName.Text = "T-72 M-1 · B12BT3716";
            this.lblName.Tag = "title";
            this.lblName.Name = "lblName";
            // 
            // lblSub
            // 
            this.lblSub.AutoSize = true;
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSub.Location = new System.Drawing.Point(180, 46);
            this.lblSub.Text = "Əsas döyüş tankı · Zavod 3 · daxil olub 05.08.2026";
            this.lblSub.Tag = "muted";
            this.lblSub.Name = "lblSub";
            // 
            // chipStatus
            // 
            this.chipStatus.Location = new System.Drawing.Point(470, 24);
            this.chipStatus.Size = new System.Drawing.Size(80, 22);
            this.chipStatus.Text = "Təmirdə";
            this.chipStatus.Name = "chipStatus";
            // 
            // chipLate
            // 
            this.chipLate.Location = new System.Drawing.Point(556, 24);
            this.chipLate.Size = new System.Drawing.Size(70, 22);
            this.chipLate.Text = "Gecikir";
            this.chipLate.Name = "chipLate";
            // 
            // btnEdit
            // 
            this.btnEdit.Kind = TexnikaReyestri.Controls.ButtonKind.Primary;
            this.btnEdit.Location = new System.Drawing.Point(1046, 20);
            this.btnEdit.Size = new System.Drawing.Size(112, 34);
            this.btnEdit.Text = "Redaktə";
            this.btnEdit.Glyph = "\uE70F";
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnPdf
            // 
            this.btnPdf.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            this.btnPdf.Location = new System.Drawing.Point(918, 20);
            this.btnPdf.Size = new System.Drawing.Size(122, 34);
            this.btnPdf.Text = "PDF pasport";
            this.btnPdf.Glyph = "\uE749";
            this.btnPdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPdf.Name = "btnPdf";
            this.btnPdf.Click += new System.EventHandler(this.btnPdf_Click);
            // 
            // btnNext
            // 
            this.btnNext.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            this.btnNext.Location = new System.Drawing.Point(814, 20);
            this.btnNext.Size = new System.Drawing.Size(98, 34);
            this.btnNext.Text = "Növbəti";
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Name = "btnNext";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            this.btnPrev.Location = new System.Drawing.Point(710, 20);
            this.btnPrev.Size = new System.Drawing.Size(98, 34);
            this.btnPrev.Text = "Əvvəlki";
            this.btnPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // rpStep
            // 
            this.rpStep.Controls.Add(this.stepper);
            this.rpStep.Padding = new System.Windows.Forms.Padding(14, 10, 14, 6);
            this.rpStep.Dock = System.Windows.Forms.DockStyle.Top;
            this.rpStep.Size = new System.Drawing.Size(1172, 78);
            this.rpStep.Name = "rpStep";
            // 
            // stepper
            // 
            this.stepper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stepper.Size = new System.Drawing.Size(1144, 62);
            this.stepper.Name = "stepper";
            // 
            // pnlGap
            // 
            this.pnlGap.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGap.Size = new System.Drawing.Size(1172, 6);
            this.pnlGap.Name = "pnlGap";
            // 
            // scroll
            // 
            this.scroll.Controls.Add(this.rpHist);
            this.scroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scroll.Size = new System.Drawing.Size(1172, 520);
            this.scroll.Name = "scroll";
            // 
            // rpHist
            // 
            this.rpHist.Controls.Add(this.flHist);
            this.rpHist.Controls.Add(this.lblHistTitle);
            this.rpHist.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.rpHist.Size = new System.Drawing.Size(396, 200);
            this.rpHist.Location = new System.Drawing.Point(0, 0);
            this.rpHist.Name = "rpHist";
            // 
            // lblHistTitle
            // 
            this.lblHistTitle.AutoSize = false;
            this.lblHistTitle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 10.5F);
            this.lblHistTitle.Location = new System.Drawing.Point(0, 0);
            this.lblHistTitle.Text = "Dəyişiklik tarixçəsi";
            this.lblHistTitle.Tag = "muted";
            this.lblHistTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHistTitle.Height = 26;
            this.lblHistTitle.Name = "lblHistTitle";
            // 
            // flHist
            // 
            this.flHist.Dock = System.Windows.Forms.DockStyle.Top;
            this.flHist.AutoSize = true;
            this.flHist.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flHist.WrapContents = false;
            this.flHist.Name = "flHist";
            // 
            // DetailPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(1216, 712);
            this.Controls.Add(this.scroll);
            this.Controls.Add(this.pnlGap);
            this.Controls.Add(this.rpStep);
            this.Controls.Add(this.rpHead);
            this.Name = "DetailPage";
            this.flHist.ResumeLayout(false);
            this.flHist.PerformLayout();
            this.rpHist.ResumeLayout(false);
            this.rpHist.PerformLayout();
            this.scroll.ResumeLayout(false);
            this.pnlGap.ResumeLayout(false);
            this.pnlGap.PerformLayout();
            this.rpStep.ResumeLayout(false);
            this.rpStep.PerformLayout();
            this.rpHead.ResumeLayout(false);
            this.rpHead.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private TexnikaReyestri.Controls.RoundPanel rpHead;
        private TexnikaReyestri.Controls.FlatButton btnBack;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblSub;
        private TexnikaReyestri.Controls.StatusChip chipStatus;
        private TexnikaReyestri.Controls.StatusChip chipLate;
        private TexnikaReyestri.Controls.FlatButton btnEdit;
        private TexnikaReyestri.Controls.FlatButton btnPdf;
        private TexnikaReyestri.Controls.FlatButton btnNext;
        private TexnikaReyestri.Controls.FlatButton btnPrev;
        private TexnikaReyestri.Controls.RoundPanel rpStep;
        private TexnikaReyestri.Controls.StepperControl stepper;
        private System.Windows.Forms.Panel pnlGap;
        private TexnikaReyestri.Controls.ScrollPanel scroll;
        private TexnikaReyestri.Controls.RoundPanel rpHist;
        private System.Windows.Forms.Label lblHistTitle;
        private System.Windows.Forms.FlowLayoutPanel flHist;
    }
}
