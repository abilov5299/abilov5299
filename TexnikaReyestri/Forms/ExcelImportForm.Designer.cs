namespace TexnikaReyestri.Forms
{
    partial class ExcelImportForm
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
            this.btnX = new TexnikaReyestri.Controls.FlatButton();
            this.stepper = new TexnikaReyestri.Controls.StepperControl();
            this.pnlFoot = new System.Windows.Forms.Panel();
            this.btnNext = new TexnikaReyestri.Controls.FlatButton();
            this.btnBack = new TexnikaReyestri.Controls.FlatButton();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlHost = new System.Windows.Forms.Panel();
            this.step1 = new System.Windows.Forms.Panel();
            this.pnlDrop = new System.Windows.Forms.Panel();
            this.lblDrop = new System.Windows.Forms.Label();
            this.lblFile = new System.Windows.Forms.Label();
            this.lSheet = new System.Windows.Forms.Label();
            this.cmbSheet = new TexnikaReyestri.Controls.RoundComboBox();
            this.lHeader = new System.Windows.Forms.Label();
            this.vHeader = new System.Windows.Forms.Label();
            this.lData = new System.Windows.Forms.Label();
            this.vData = new System.Windows.Forms.Label();
            this.lblAuto = new System.Windows.Forms.Label();
            this.step2 = new System.Windows.Forms.Panel();
            this.lblMapSum = new System.Windows.Forms.Label();
            this.gridMap = new TexnikaReyestri.Controls.ModernGrid();
            this.colExcel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSample = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colField = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colMapState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblMapHint = new System.Windows.Forms.Label();
            this.step3 = new System.Windows.Forms.Panel();
            this.tlpSum = new System.Windows.Forms.TableLayoutPanel();
            this.cReady = new TexnikaReyestri.Controls.StatCard();
            this.cWarn = new TexnikaReyestri.Controls.StatCard();
            this.cErr = new TexnikaReyestri.Controls.StatCard();
            this.cEmpty = new TexnikaReyestri.Controls.StatCard();
            this.gridCheck = new TexnikaReyestri.Controls.ModernGrid();
            this.colRow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCNov = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCMarka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCRes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlOpts = new System.Windows.Forms.Panel();
            this.lDup = new System.Windows.Forms.Label();
            this.radDupUpdate = new System.Windows.Forms.RadioButton();
            this.radDupSkip = new System.Windows.Forms.RadioButton();
            this.pnlErrOpt = new System.Windows.Forms.Panel();
            this.lErr = new System.Windows.Forms.Label();
            this.radErrSkip = new System.Windows.Forms.RadioButton();
            this.radErrStop = new System.Windows.Forms.RadioButton();
            this.step4 = new System.Windows.Forms.Panel();
            this.lblDone = new System.Windows.Forms.Label();
            this.lblDoneInfo = new System.Windows.Forms.Label();
            this.tlpDone = new System.Windows.Forms.TableLayoutPanel();
            this.dNew = new TexnikaReyestri.Controls.StatCard();
            this.dUpd = new TexnikaReyestri.Controls.StatCard();
            this.dSkip = new TexnikaReyestri.Controls.StatCard();
            this.dEmpty = new TexnikaReyestri.Controls.StatCard();
            this.btnErrReport = new TexnikaReyestri.Controls.FlatButton();
            this.lblDoneHint = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCheck)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.pnlFoot.SuspendLayout();
            this.pnlHost.SuspendLayout();
            this.step1.SuspendLayout();
            this.pnlDrop.SuspendLayout();
            this.step2.SuspendLayout();
            this.step3.SuspendLayout();
            this.tlpSum.SuspendLayout();
            this.pnlOpts.SuspendLayout();
            this.pnlErrOpt.SuspendLayout();
            this.step4.SuspendLayout();
            this.tlpDone.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.btnX);
            this.pnlTop.Controls.Add(this.lblTop);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 42;
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(15, 76, 74);
            this.pnlTop.Name = "pnlTop";
            // 
            // lblTop
            // 
            this.lblTop.AutoSize = true;
            this.lblTop.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F);
            this.lblTop.Location = new System.Drawing.Point(16, 11);
            this.lblTop.Text = "Excel-dən idxal";
            this.lblTop.Tag = "white";
            this.lblTop.Name = "lblTop";
            // 
            // btnX
            // 
            this.btnX.Kind = TexnikaReyestri.Controls.ButtonKind.Primary;
            this.btnX.Location = new System.Drawing.Point(870, 4);
            this.btnX.Size = new System.Drawing.Size(40, 34);
            this.btnX.Text = "";
            this.btnX.Glyph = "\uE8BB";
            this.btnX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnX.Name = "btnX";
            this.btnX.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // stepper
            // 
            this.stepper.Dock = System.Windows.Forms.DockStyle.Top;
            this.stepper.Height = 64;
            this.stepper.Steps = new string[] {
        "Fayl seç",
        "Sütunları uyğunlaşdır",
        "Yoxla",
        "Nəticə"};
            this.stepper.CurrentStep = 0;
            this.stepper.Name = "stepper";
            // 
            // pnlFoot
            // 
            this.pnlFoot.Controls.Add(this.lblStatus);
            this.pnlFoot.Controls.Add(this.btnBack);
            this.pnlFoot.Controls.Add(this.btnNext);
            this.pnlFoot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFoot.Height = 58;
            this.pnlFoot.Tag = "alt";
            this.pnlFoot.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);
            this.pnlFoot.Name = "pnlFoot";
            // 
            // btnNext
            // 
            this.btnNext.Kind = TexnikaReyestri.Controls.ButtonKind.Primary;
            this.btnNext.Location = new System.Drawing.Point(778, 11);
            this.btnNext.Size = new System.Drawing.Size(130, 36);
            this.btnNext.Text = "Növbəti";
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Name = "btnNext";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnBack
            // 
            this.btnBack.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            this.btnBack.Location = new System.Drawing.Point(640, 11);
            this.btnBack.Size = new System.Drawing.Size(130, 36);
            this.btnBack.Text = "Geri";
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.Name = "btnBack";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblStatus.Location = new System.Drawing.Point(16, 20);
            this.lblStatus.Text = "";
            this.lblStatus.Tag = "muted";
            this.lblStatus.Name = "lblStatus";
            // 
            // pnlHost
            // 
            this.pnlHost.Controls.Add(this.step4);
            this.pnlHost.Controls.Add(this.step3);
            this.pnlHost.Controls.Add(this.step2);
            this.pnlHost.Controls.Add(this.step1);
            this.pnlHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHost.Tag = "card";
            this.pnlHost.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);
            this.pnlHost.Name = "pnlHost";
            // 
            // step1
            // 
            this.step1.Controls.Add(this.lblAuto);
            this.step1.Controls.Add(this.vData);
            this.step1.Controls.Add(this.lData);
            this.step1.Controls.Add(this.vHeader);
            this.step1.Controls.Add(this.lHeader);
            this.step1.Controls.Add(this.cmbSheet);
            this.step1.Controls.Add(this.lSheet);
            this.step1.Controls.Add(this.lblFile);
            this.step1.Controls.Add(this.pnlDrop);
            this.step1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.step1.Name = "step1";
            // 
            // pnlDrop
            // 
            this.pnlDrop.Controls.Add(this.lblDrop);
            this.pnlDrop.Location = new System.Drawing.Point(0, 0);
            this.pnlDrop.Size = new System.Drawing.Size(884, 120);
            this.pnlDrop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDrop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDrop.AllowDrop = true;
            this.pnlDrop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlDrop.Name = "pnlDrop";
            this.pnlDrop.Click += new System.EventHandler(this.btnPick_Click);
            this.pnlDrop.DragEnter += new System.Windows.Forms.DragEventHandler(this.pnlDrop_DragEnter);
            this.pnlDrop.DragDrop += new System.Windows.Forms.DragEventHandler(this.pnlDrop_DragDrop);
            // 
            // lblDrop
            // 
            this.lblDrop.AutoSize = false;
            this.lblDrop.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.lblDrop.Location = new System.Drawing.Point(0, 0);
            this.lblDrop.Text = "Excel faylını bura sürükləyin və ya klikləyib seçin\r\n.xlsx · ən çoxu 1 000 000 sətir";
            this.lblDrop.Tag = "muted";
            this.lblDrop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDrop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDrop.Name = "lblDrop";
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.lblFile.Location = new System.Drawing.Point(0, 136);
            this.lblFile.Text = "Fayl seçilməyib";
            this.lblFile.Name = "lblFile";
            // 
            // lSheet
            // 
            this.lSheet.AutoSize = true;
            this.lSheet.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lSheet.Location = new System.Drawing.Point(0, 172);
            this.lSheet.Text = "Vərəq";
            this.lSheet.Tag = "muted";
            this.lSheet.Name = "lSheet";
            // 
            // cmbSheet
            // 
            this.cmbSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSheet.Font = new System.Drawing.Font("Segoe UI", 10.0F);
            this.cmbSheet.Location = new System.Drawing.Point(0, 192);
            this.cmbSheet.Size = new System.Drawing.Size(240, 25);
            this.cmbSheet.Name = "cmbSheet";
            this.cmbSheet.SelectedIndexChanged += new System.EventHandler(this.cmbSheet_SelectedIndexChanged);
            // 
            // lHeader
            // 
            this.lHeader.AutoSize = true;
            this.lHeader.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lHeader.Location = new System.Drawing.Point(256, 172);
            this.lHeader.Text = "Başlıq sətri";
            this.lHeader.Tag = "muted";
            this.lHeader.Name = "lHeader";
            // 
            // vHeader
            // 
            this.vHeader.AutoSize = true;
            this.vHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.vHeader.Location = new System.Drawing.Point(256, 196);
            this.vHeader.Text = "—";
            this.vHeader.Name = "vHeader";
            // 
            // lData
            // 
            this.lData.AutoSize = true;
            this.lData.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lData.Location = new System.Drawing.Point(420, 172);
            this.lData.Text = "Məlumat başlayır";
            this.lData.Tag = "muted";
            this.lData.Name = "lData";
            // 
            // vData
            // 
            this.vData.AutoSize = true;
            this.vData.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.vData.Location = new System.Drawing.Point(420, 196);
            this.vData.Text = "—";
            this.vData.Name = "vData";
            // 
            // lblAuto
            // 
            this.lblAuto.AutoSize = true;
            this.lblAuto.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblAuto.Location = new System.Drawing.Point(0, 236);
            this.lblAuto.Text = "";
            this.lblAuto.MaximumSize = new System.Drawing.Size(860, 0);
            this.lblAuto.Name = "lblAuto";
            // 
            // step2
            // 
            this.step2.Controls.Add(this.gridMap);
            this.step2.Controls.Add(this.lblMapHint);
            this.step2.Controls.Add(this.lblMapSum);
            this.step2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.step2.Visible = false;
            this.step2.Name = "step2";
            // 
            // lblMapSum
            // 
            this.lblMapSum.AutoSize = false;
            this.lblMapSum.Font = new System.Drawing.Font("Segoe UI Semibold", 9.0F);
            this.lblMapSum.Location = new System.Drawing.Point(0, 0);
            this.lblMapSum.Text = "";
            this.lblMapSum.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMapSum.Height = 28;
            this.lblMapSum.Name = "lblMapSum";
            // 
            // gridMap
            // 
            this.gridMap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colExcel,
            this.colSample,
            this.colField,
            this.colMapState});
            this.gridMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMap.ReadOnly = false;
            this.gridMap.Name = "gridMap";
            this.gridMap.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridMap_CellValueChanged);
            this.gridMap.CurrentCellDirtyStateChanged += new System.EventHandler(this.gridMap_CurrentCellDirtyStateChanged);
            // 
            // colExcel
            // 
            this.colExcel.HeaderText = "Excel sütunu";
            this.colExcel.Name = "colExcel";
            this.colExcel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colExcel.MinimumWidth = 100;
            // 
            // colSample
            // 
            this.colSample.HeaderText = "Nümunə dəyər";
            this.colSample.Name = "colSample";
            this.colSample.Width = 180;
            // 
            // colField
            // 
            this.colField.HeaderText = "Proqram sahəsi";
            this.colField.Name = "colField";
            this.colField.Width = 280;
            this.colField.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            // 
            // colMapState
            // 
            this.colMapState.HeaderText = "Vəziyyət";
            this.colMapState.Name = "Status";
            this.colMapState.Width = 160;
            // 
            // lblMapHint
            // 
            this.lblMapHint.AutoSize = false;
            this.lblMapHint.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblMapHint.Location = new System.Drawing.Point(0, 0);
            this.lblMapHint.Text = "Uyğunlaşdırma yadda saxlanılır: növbəti dəfə eyni formatlı fayl üçün avtomatik tətbiq olunur.";
            this.lblMapHint.Tag = "muted";
            this.lblMapHint.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMapHint.Height = 24;
            this.lblMapHint.Name = "lblMapHint";
            // 
            // step3
            // 
            this.step3.Controls.Add(this.gridCheck);
            this.step3.Controls.Add(this.pnlOpts);
            this.step3.Controls.Add(this.tlpSum);
            this.step3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.step3.Visible = false;
            this.step3.Name = "step3";
            // 
            // tlpSum
            // 
            this.tlpSum.ColumnCount = 4;
            this.tlpSum.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0F));
            this.tlpSum.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0F));
            this.tlpSum.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0F));
            this.tlpSum.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0F));
            this.tlpSum.RowCount = 1;
            this.tlpSum.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0F));
            this.tlpSum.Controls.Add(this.cReady, 0, 0);
            this.tlpSum.Controls.Add(this.cWarn, 1, 0);
            this.tlpSum.Controls.Add(this.cErr, 2, 0);
            this.tlpSum.Controls.Add(this.cEmpty, 3, 0);
            this.tlpSum.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpSum.Height = 84;
            this.tlpSum.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.tlpSum.Name = "tlpSum";
            // 
            // cReady
            // 
            this.cReady.Title = "Hazırdır";
            this.cReady.Glyph = "\uE73E";
            this.cReady.Accent = TexnikaReyestri.Controls.AccentKind.Ok;
            this.cReady.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cReady.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.cReady.Name = "cReady";
            // 
            // cWarn
            // 
            this.cWarn.Title = "Xəbərdarlıq";
            this.cWarn.Glyph = "\uE7BA";
            this.cWarn.Accent = TexnikaReyestri.Controls.AccentKind.Warn;
            this.cWarn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cWarn.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.cWarn.Name = "cWarn";
            // 
            // cErr
            // 
            this.cErr.Title = "Xəta";
            this.cErr.Glyph = "\uE711";
            this.cErr.Accent = TexnikaReyestri.Controls.AccentKind.Bad;
            this.cErr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cErr.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.cErr.Name = "cErr";
            // 
            // cEmpty
            // 
            this.cEmpty.Title = "Boş sətir (ötürülür)";
            this.cEmpty.Glyph = "\uE8FD";
            this.cEmpty.Accent = TexnikaReyestri.Controls.AccentKind.Nav;
            this.cEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cEmpty.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.cEmpty.Name = "cEmpty";
            // 
            // gridCheck
            // 
            this.gridCheck.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRow,
            this.colCNov,
            this.colCMarka,
            this.colCNo,
            this.colCRes});
            this.gridCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCheck.Name = "gridCheck";
            this.gridCheck.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridCheck_CellFormatting);
            // 
            // colRow
            // 
            this.colRow.HeaderText = "Sətir";
            this.colRow.Name = "colRow";
            this.colRow.Width = 70;
            // 
            // colCNov
            // 
            this.colCNov.HeaderText = "Növ";
            this.colCNov.Name = "colCNov";
            this.colCNov.Width = 170;
            // 
            // colCMarka
            // 
            this.colCMarka.HeaderText = "Marka";
            this.colCMarka.Name = "colCMarka";
            this.colCMarka.Width = 130;
            // 
            // colCNo
            // 
            this.colCNo.HeaderText = "Zavod nömrəsi";
            this.colCNo.Name = "colCNo";
            this.colCNo.Width = 150;
            // 
            // colCRes
            // 
            this.colCRes.HeaderText = "Nəticə";
            this.colCRes.Name = "colCRes";
            this.colCRes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCRes.MinimumWidth = 100;
            // 
            // pnlOpts
            // 
            this.pnlOpts.Controls.Add(this.pnlErrOpt);
            this.pnlOpts.Controls.Add(this.radDupSkip);
            this.pnlOpts.Controls.Add(this.radDupUpdate);
            this.pnlOpts.Controls.Add(this.lDup);
            this.pnlOpts.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlOpts.Height = 70;
            this.pnlOpts.Name = "pnlOpts";
            // 
            // lDup
            // 
            this.lDup.AutoSize = true;
            this.lDup.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lDup.Location = new System.Drawing.Point(0, 8);
            this.lDup.Text = "Bazada artıq olan texnika (zavod nömrəsinə görə):";
            this.lDup.Tag = "muted";
            this.lDup.Name = "lDup";
            // 
            // radDupUpdate
            // 
            this.radDupUpdate.AutoSize = true;
            this.radDupUpdate.Text = "Yenilə (boş xanalar köhnə dəyəri silmir)";
            this.radDupUpdate.Location = new System.Drawing.Point(0, 28);
            this.radDupUpdate.Checked = true;
            this.radDupUpdate.Name = "radDupUpdate";
            // 
            // radDupSkip
            // 
            this.radDupSkip.AutoSize = true;
            this.radDupSkip.Text = "Ötür, toxunma";
            this.radDupSkip.Location = new System.Drawing.Point(0, 48);
            this.radDupSkip.Name = "radDupSkip";
            // 
            // pnlErrOpt
            // 
            this.pnlErrOpt.Controls.Add(this.radErrStop);
            this.pnlErrOpt.Controls.Add(this.radErrSkip);
            this.pnlErrOpt.Controls.Add(this.lErr);
            this.pnlErrOpt.Location = new System.Drawing.Point(460, 0);
            this.pnlErrOpt.Size = new System.Drawing.Size(420, 70);
            this.pnlErrOpt.Name = "pnlErrOpt";
            // 
            // lErr
            // 
            this.lErr.AutoSize = true;
            this.lErr.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lErr.Location = new System.Drawing.Point(0, 8);
            this.lErr.Text = "Xətalı sətirlər:";
            this.lErr.Tag = "muted";
            this.lErr.Name = "lErr";
            // 
            // radErrSkip
            // 
            this.radErrSkip.AutoSize = true;
            this.radErrSkip.Text = "Ötür, qalanlarını idxal et";
            this.radErrSkip.Location = new System.Drawing.Point(0, 28);
            this.radErrSkip.Checked = true;
            this.radErrSkip.Name = "radErrSkip";
            // 
            // radErrStop
            // 
            this.radErrStop.AutoSize = true;
            this.radErrStop.Text = "Heç nə idxal etmə";
            this.radErrStop.Location = new System.Drawing.Point(0, 48);
            this.radErrStop.Name = "radErrStop";
            // 
            // step4
            // 
            this.step4.Controls.Add(this.lblDoneHint);
            this.step4.Controls.Add(this.btnErrReport);
            this.step4.Controls.Add(this.tlpDone);
            this.step4.Controls.Add(this.lblDoneInfo);
            this.step4.Controls.Add(this.lblDone);
            this.step4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.step4.Visible = false;
            this.step4.Name = "step4";
            // 
            // lblDone
            // 
            this.lblDone.AutoSize = true;
            this.lblDone.Font = new System.Drawing.Font("Bahnschrift SemiBold", 15.0F);
            this.lblDone.Location = new System.Drawing.Point(0, 0);
            this.lblDone.Text = "İdxal tamamlandı";
            this.lblDone.Tag = "title";
            this.lblDone.Name = "lblDone";
            // 
            // lblDoneInfo
            // 
            this.lblDoneInfo.AutoSize = true;
            this.lblDoneInfo.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblDoneInfo.Location = new System.Drawing.Point(0, 34);
            this.lblDoneInfo.Text = "";
            this.lblDoneInfo.Tag = "muted";
            this.lblDoneInfo.Name = "lblDoneInfo";
            // 
            // tlpDone
            // 
            this.tlpDone.ColumnCount = 4;
            this.tlpDone.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0F));
            this.tlpDone.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0F));
            this.tlpDone.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0F));
            this.tlpDone.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0F));
            this.tlpDone.RowCount = 1;
            this.tlpDone.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0F));
            this.tlpDone.Controls.Add(this.dNew, 0, 0);
            this.tlpDone.Controls.Add(this.dUpd, 1, 0);
            this.tlpDone.Controls.Add(this.dSkip, 2, 0);
            this.tlpDone.Controls.Add(this.dEmpty, 3, 0);
            this.tlpDone.Location = new System.Drawing.Point(0, 64);
            this.tlpDone.Size = new System.Drawing.Size(884, 84);
            this.tlpDone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpDone.Name = "tlpDone";
            // 
            // dNew
            // 
            this.dNew.Title = "Yeni əlavə";
            this.dNew.Glyph = "\uE710";
            this.dNew.Accent = TexnikaReyestri.Controls.AccentKind.Ok;
            this.dNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dNew.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.dNew.Name = "dNew";
            // 
            // dUpd
            // 
            this.dUpd.Title = "Yeniləndi";
            this.dUpd.Glyph = "\uE72C";
            this.dUpd.Accent = TexnikaReyestri.Controls.AccentKind.Info;
            this.dUpd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dUpd.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.dUpd.Name = "dUpd";
            // 
            // dSkip
            // 
            this.dSkip.Title = "Xəta, ötürüldü";
            this.dSkip.Glyph = "\uE711";
            this.dSkip.Accent = TexnikaReyestri.Controls.AccentKind.Bad;
            this.dSkip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dSkip.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.dSkip.Name = "dSkip";
            // 
            // dEmpty
            // 
            this.dEmpty.Title = "Boş sətir";
            this.dEmpty.Glyph = "\uE8FD";
            this.dEmpty.Accent = TexnikaReyestri.Controls.AccentKind.Nav;
            this.dEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dEmpty.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.dEmpty.Name = "dEmpty";
            // 
            // btnErrReport
            // 
            this.btnErrReport.Kind = TexnikaReyestri.Controls.ButtonKind.Secondary;
            this.btnErrReport.Location = new System.Drawing.Point(0, 170);
            this.btnErrReport.Size = new System.Drawing.Size(270, 36);
            this.btnErrReport.Text = "Xəta hesabatını Excel-ə yüklə";
            this.btnErrReport.Glyph = "\uE896";
            this.btnErrReport.Name = "btnErrReport";
            this.btnErrReport.Click += new System.EventHandler(this.btnErrReport_Click);
            // 
            // lblDoneHint
            // 
            this.lblDoneHint.AutoSize = true;
            this.lblDoneHint.Font = new System.Drawing.Font("Segoe UI", 9.0F);
            this.lblDoneHint.Location = new System.Drawing.Point(0, 216);
            this.lblDoneHint.Text = "Hesabatda xətalı sətirlər səbəbi ilə göstərilir. Düzəldib yenidən idxal etmək olar. İdxaldan əvvəl ehtiyat nüsxə avtomatik alındı.";
            this.lblDoneHint.Tag = "muted";
            this.lblDoneHint.MaximumSize = new System.Drawing.Size(860, 0);
            this.lblDoneHint.Name = "lblDoneHint";
            // 
            // ExcelImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(926, 600);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ShowInTaskbar = false;
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Controls.Add(this.pnlHost);
            this.Controls.Add(this.pnlFoot);
            this.Controls.Add(this.stepper);
            this.Controls.Add(this.pnlTop);
            this.Name = "ExcelImportForm";
            this.Load += new System.EventHandler(this.ExcelImportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCheck)).EndInit();
            this.tlpDone.ResumeLayout(false);
            this.tlpDone.PerformLayout();
            this.step4.ResumeLayout(false);
            this.step4.PerformLayout();
            this.pnlErrOpt.ResumeLayout(false);
            this.pnlErrOpt.PerformLayout();
            this.pnlOpts.ResumeLayout(false);
            this.pnlOpts.PerformLayout();
            this.tlpSum.ResumeLayout(false);
            this.tlpSum.PerformLayout();
            this.step3.ResumeLayout(false);
            this.step3.PerformLayout();
            this.step2.ResumeLayout(false);
            this.step2.PerformLayout();
            this.pnlDrop.ResumeLayout(false);
            this.pnlDrop.PerformLayout();
            this.step1.ResumeLayout(false);
            this.step1.PerformLayout();
            this.pnlHost.ResumeLayout(false);
            this.pnlHost.PerformLayout();
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
        private TexnikaReyestri.Controls.FlatButton btnX;
        private TexnikaReyestri.Controls.StepperControl stepper;
        private System.Windows.Forms.Panel pnlFoot;
        private TexnikaReyestri.Controls.FlatButton btnNext;
        private TexnikaReyestri.Controls.FlatButton btnBack;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlHost;
        private System.Windows.Forms.Panel step1;
        private System.Windows.Forms.Panel pnlDrop;
        private System.Windows.Forms.Label lblDrop;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Label lSheet;
        private TexnikaReyestri.Controls.RoundComboBox cmbSheet;
        private System.Windows.Forms.Label lHeader;
        private System.Windows.Forms.Label vHeader;
        private System.Windows.Forms.Label lData;
        private System.Windows.Forms.Label vData;
        private System.Windows.Forms.Label lblAuto;
        private System.Windows.Forms.Panel step2;
        private System.Windows.Forms.Label lblMapSum;
        private TexnikaReyestri.Controls.ModernGrid gridMap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSample;
        private System.Windows.Forms.DataGridViewComboBoxColumn colField;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMapState;
        private System.Windows.Forms.Label lblMapHint;
        private System.Windows.Forms.Panel step3;
        private System.Windows.Forms.TableLayoutPanel tlpSum;
        private TexnikaReyestri.Controls.StatCard cReady;
        private TexnikaReyestri.Controls.StatCard cWarn;
        private TexnikaReyestri.Controls.StatCard cErr;
        private TexnikaReyestri.Controls.StatCard cEmpty;
        private TexnikaReyestri.Controls.ModernGrid gridCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCNov;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCMarka;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCRes;
        private System.Windows.Forms.Panel pnlOpts;
        private System.Windows.Forms.Label lDup;
        private System.Windows.Forms.RadioButton radDupUpdate;
        private System.Windows.Forms.RadioButton radDupSkip;
        private System.Windows.Forms.Panel pnlErrOpt;
        private System.Windows.Forms.Label lErr;
        private System.Windows.Forms.RadioButton radErrSkip;
        private System.Windows.Forms.RadioButton radErrStop;
        private System.Windows.Forms.Panel step4;
        private System.Windows.Forms.Label lblDone;
        private System.Windows.Forms.Label lblDoneInfo;
        private System.Windows.Forms.TableLayoutPanel tlpDone;
        private TexnikaReyestri.Controls.StatCard dNew;
        private TexnikaReyestri.Controls.StatCard dUpd;
        private TexnikaReyestri.Controls.StatCard dSkip;
        private TexnikaReyestri.Controls.StatCard dEmpty;
        private TexnikaReyestri.Controls.FlatButton btnErrReport;
        private System.Windows.Forms.Label lblDoneHint;
    }
}
