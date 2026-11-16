namespace BakeryPOS
{
    partial class frmReports
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCashierShiftReport = new System.Windows.Forms.Button();
            this.lblReportSubtitle = new System.Windows.Forms.Label();
            this.lblStoreTitle = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblFrom = new System.Windows.Forms.Label();
            this.cmbPeriod = new System.Windows.Forms.ComboBox();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.tabReports = new System.Windows.Forms.TabControl();
            this.tabSales = new System.Windows.Forms.TabPage();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlGridContainer = new System.Windows.Forms.Panel();
            this.dgvBestSelling = new System.Windows.Forms.DataGridView();
            this.pnlGridHeader = new System.Windows.Forms.Panel();
            this.lblPeriodSummary = new System.Windows.Forms.Label();
            this.lblGridTitle = new System.Windows.Forms.Label();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.tblSummary = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCardSales = new System.Windows.Forms.Panel();
            this.lblValTotalSales = new System.Windows.Forms.Label();
            this.lblTitleSales = new System.Windows.Forms.Label();
            this.pnlCardTrans = new System.Windows.Forms.Panel();
            this.lblValTransactions = new System.Windows.Forms.Label();
            this.lblTitleTrans = new System.Windows.Forms.Label();
            this.pnlCardItems = new System.Windows.Forms.Panel();
            this.lblValItemsSold = new System.Windows.Forms.Label();
            this.lblTitleItems = new System.Windows.Forms.Label();
            this.pnlCardAvg = new System.Windows.Forms.Panel();
            this.lblValAvgOrder = new System.Windows.Forms.Label();
            this.lblTitleAvg = new System.Windows.Forms.Label();
            this.tabInventory = new System.Windows.Forms.TabPage();
            this.pnlInventoryContent = new System.Windows.Forms.Panel();
            this.pnlInventoryGridContainer = new System.Windows.Forms.Panel();
            this.dgvInventory = new System.Windows.Forms.DataGridView();
            this.pnlInventoryGridHeader = new System.Windows.Forms.Panel();
            this.lblInventoryNote = new System.Windows.Forms.Label();
            this.lblInventoryGridTitle = new System.Windows.Forms.Label();
            this.pnlInventorySummary = new System.Windows.Forms.Panel();
            this.tblInventorySummary = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCardActiveProd = new System.Windows.Forms.Panel();
            this.lblValActiveProducts = new System.Windows.Forms.Label();
            this.lblTitleActiveProd = new System.Windows.Forms.Label();
            this.pnlCardStockUnits = new System.Windows.Forms.Panel();
            this.lblValTotalStockUnits = new System.Windows.Forms.Label();
            this.lblTitleStockUnits = new System.Windows.Forms.Label();
            this.pnlCardLowStock = new System.Windows.Forms.Panel();
            this.lblValLowStockCount = new System.Windows.Forms.Label();
            this.lblTitleLowStock = new System.Windows.Forms.Label();
            this.tabAiReports = new System.Windows.Forms.TabPage();
            this.pnlAiContent = new System.Windows.Forms.Panel();
            this.pnlAiRestockContainer = new System.Windows.Forms.Panel();
            this.dgvRestockSuggestions = new System.Windows.Forms.DataGridView();
            this.pnlAiRestockHeader = new System.Windows.Forms.Panel();
            this.lblAiRestockNote = new System.Windows.Forms.Label();
            this.lblAiRestockTitle = new System.Windows.Forms.Label();
            this.pnlAiSpacer2 = new System.Windows.Forms.Panel();
            this.pnlCardAiTrends = new System.Windows.Forms.Panel();
            this.lblValAiTrends = new System.Windows.Forms.Label();
            this.lblTitleAiTrends = new System.Windows.Forms.Label();
            this.pnlAiSpacer1 = new System.Windows.Forms.Panel();
            this.pnlAiSummaryCard = new System.Windows.Forms.Panel();
            this.lblAiSummaryText = new System.Windows.Forms.Label();
            this.lblAiSummaryTitle = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.tabReports.SuspendLayout();
            this.tabSales.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlGridContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBestSelling)).BeginInit();
            this.pnlGridHeader.SuspendLayout();
            this.pnlSummary.SuspendLayout();
            this.tblSummary.SuspendLayout();
            this.pnlCardSales.SuspendLayout();
            this.pnlCardTrans.SuspendLayout();
            this.pnlCardItems.SuspendLayout();
            this.pnlCardAvg.SuspendLayout();
            this.tabInventory.SuspendLayout();
            this.pnlInventoryContent.SuspendLayout();
            this.pnlInventoryGridContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).BeginInit();
            this.pnlInventoryGridHeader.SuspendLayout();
            this.pnlInventorySummary.SuspendLayout();
            this.tblInventorySummary.SuspendLayout();
            this.pnlCardActiveProd.SuspendLayout();
            this.pnlCardStockUnits.SuspendLayout();
            this.pnlCardLowStock.SuspendLayout();
            this.tabAiReports.SuspendLayout();
            this.pnlAiContent.SuspendLayout();
            this.pnlAiRestockContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRestockSuggestions)).BeginInit();
            this.pnlAiRestockHeader.SuspendLayout();
            this.pnlCardAiTrends.SuspendLayout();
            this.pnlAiSummaryCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(22)))), ((int)(((byte)(8)))));
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Controls.Add(this.btnCashierShiftReport);
            this.pnlHeader.Controls.Add(this.lblReportSubtitle);
            this.pnlHeader.Controls.Add(this.lblStoreTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.pnlHeader.Size = new System.Drawing.Size(1000, 85);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(110)))), ((int)(((byte)(105)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(895, 24);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(85, 36);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCashierShiftReport
            // 
            this.btnCashierShiftReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCashierShiftReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(115)))), ((int)(((byte)(51)))));
            this.btnCashierShiftReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCashierShiftReport.FlatAppearance.BorderSize = 0;
            this.btnCashierShiftReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCashierShiftReport.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCashierShiftReport.ForeColor = System.Drawing.Color.White;
            this.btnCashierShiftReport.Location = new System.Drawing.Point(715, 24);
            this.btnCashierShiftReport.Name = "btnCashierShiftReport";
            this.btnCashierShiftReport.Size = new System.Drawing.Size(170, 36);
            this.btnCashierShiftReport.TabIndex = 2;
            this.btnCashierShiftReport.Text = "📋 Cashier Shift Report";
            this.btnCashierShiftReport.UseVisualStyleBackColor = false;
            this.btnCashierShiftReport.Click += new System.EventHandler(this.btnCashierShiftReport_Click);
            // 
            // lblReportSubtitle
            // 
            this.lblReportSubtitle.AutoSize = true;
            this.lblReportSubtitle.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblReportSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(150)))));
            this.lblReportSubtitle.Location = new System.Drawing.Point(161, 47);
            this.lblReportSubtitle.Name = "lblReportSubtitle";
            this.lblReportSubtitle.Size = new System.Drawing.Size(194, 21);
            this.lblReportSubtitle.TabIndex = 1;
            this.lblReportSubtitle.Text = "Sales & Inventory Reports";
            // 
            // lblStoreTitle
            // 
            this.lblStoreTitle.AutoSize = true;
            this.lblStoreTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblStoreTitle.ForeColor = System.Drawing.Color.White;
            this.lblStoreTitle.Location = new System.Drawing.Point(160, 14);
            this.lblStoreTitle.Name = "lblStoreTitle";
            this.lblStoreTitle.Size = new System.Drawing.Size(284, 30);
            this.lblStoreTitle.TabIndex = 0;
            this.lblStoreTitle.Text = "BREAD BASKET BAKESHOP";
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(236)))), ((int)(((byte)(226)))));
            this.pnlFilter.Controls.Add(this.btnPrint);
            this.pnlFilter.Controls.Add(this.btnGenerate);
            this.pnlFilter.Controls.Add(this.dtpEndDate);
            this.pnlFilter.Controls.Add(this.lblTo);
            this.pnlFilter.Controls.Add(this.dtpStartDate);
            this.pnlFilter.Controls.Add(this.lblFrom);
            this.pnlFilter.Controls.Add(this.cmbPeriod);
            this.pnlFilter.Controls.Add(this.lblPeriod);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 85);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.pnlFilter.Size = new System.Drawing.Size(1000, 56);
            this.pnlFilter.TabIndex = 1;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(115)))), ((int)(((byte)(51)))));
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(855, 10);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(125, 36);
            this.btnPrint.TabIndex = 7;
            this.btnPrint.Text = "🖨️ Print Report";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(61)))), ((int)(((byte)(34)))));
            this.btnGenerate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerate.FlatAppearance.BorderSize = 0;
            this.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnGenerate.ForeColor = System.Drawing.Color.White;
            this.btnGenerate.Location = new System.Drawing.Point(628, 10);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(150, 36);
            this.btnGenerate.TabIndex = 6;
            this.btnGenerate.Text = "🔍 Generate Report";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "MMM dd, yyyy";
            this.dtpEndDate.Enabled = false;
            this.dtpEndDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(479, 15);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(130, 25);
            this.dtpEndDate.TabIndex = 5;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(22)))), ((int)(((byte)(8)))));
            this.lblTo.Location = new System.Drawing.Point(448, 19);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(27, 17);
            this.lblTo.TabIndex = 4;
            this.lblTo.Text = "To:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "MMM dd, yyyy";
            this.dtpStartDate.Enabled = false;
            this.dtpStartDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(303, 15);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(130, 25);
            this.dtpStartDate.TabIndex = 3;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(22)))), ((int)(((byte)(8)))));
            this.lblFrom.Location = new System.Drawing.Point(255, 19);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(44, 17);
            this.lblFrom.TabIndex = 2;
            this.lblFrom.Text = "From:";
            // 
            // cmbPeriod
            // 
            this.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPeriod.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbPeriod.FormattingEnabled = true;
            this.cmbPeriod.Items.AddRange(new object[] {
            "Daily",
            "Weekly",
            "Monthly",
            "Custom Date Range"});
            this.cmbPeriod.Location = new System.Drawing.Point(73, 15);
            this.cmbPeriod.Name = "cmbPeriod";
            this.cmbPeriod.Size = new System.Drawing.Size(160, 25);
            this.cmbPeriod.TabIndex = 1;
            this.cmbPeriod.SelectedIndexChanged += new System.EventHandler(this.cmbPeriod_SelectedIndexChanged);
            // 
            // lblPeriod
            // 
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblPeriod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(22)))), ((int)(((byte)(8)))));
            this.lblPeriod.Location = new System.Drawing.Point(16, 19);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(52, 17);
            this.lblPeriod.TabIndex = 0;
            this.lblPeriod.Text = "Period:";
            // 
            // tabReports
            // 
            this.tabReports.Controls.Add(this.tabSales);
            this.tabReports.Controls.Add(this.tabInventory);
            this.tabReports.Controls.Add(this.tabAiReports);
            this.tabReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabReports.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tabReports.ItemSize = new System.Drawing.Size(180, 32);
            this.tabReports.Location = new System.Drawing.Point(0, 148);
            this.tabReports.Name = "tabReports";
            this.tabReports.Padding = new System.Drawing.Point(16, 4);
            this.tabReports.SelectedIndex = 0;
            this.tabReports.Size = new System.Drawing.Size(1000, 552);
            this.tabReports.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabReports.TabIndex = 2;
            this.tabReports.SelectedIndexChanged += new System.EventHandler(this.tabReports_SelectedIndexChanged);
            // 
            // tabSales
            // 
            this.tabSales.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(240)))));
            this.tabSales.Controls.Add(this.pnlContent);
            this.tabSales.Controls.Add(this.pnlSummary);
            this.tabSales.Location = new System.Drawing.Point(4, 36);
            this.tabSales.Name = "tabSales";
            this.tabSales.Size = new System.Drawing.Size(992, 512);
            this.tabSales.TabIndex = 0;
            this.tabSales.Text = "💰 Sales Reports";
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(240)))));
            this.pnlContent.Controls.Add(this.pnlGridContainer);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 88);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(16, 0, 16, 16);
            this.pnlContent.Size = new System.Drawing.Size(992, 424);
            this.pnlContent.TabIndex = 1;
            // 
            // pnlGridContainer
            // 
            this.pnlGridContainer.BackColor = System.Drawing.Color.White;
            this.pnlGridContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGridContainer.Controls.Add(this.dgvBestSelling);
            this.pnlGridContainer.Controls.Add(this.pnlGridHeader);
            this.pnlGridContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridContainer.Location = new System.Drawing.Point(16, 0);
            this.pnlGridContainer.Name = "pnlGridContainer";
            this.pnlGridContainer.Size = new System.Drawing.Size(960, 408);
            this.pnlGridContainer.TabIndex = 0;
            // 
            // dgvBestSelling
            // 
            this.dgvBestSelling.AllowUserToAddRows = false;
            this.dgvBestSelling.AllowUserToDeleteRows = false;
            this.dgvBestSelling.AllowUserToResizeRows = false;
            this.dgvBestSelling.BackgroundColor = System.Drawing.Color.White;
            this.dgvBestSelling.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBestSelling.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvBestSelling.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBestSelling.Location = new System.Drawing.Point(0, 38);
            this.dgvBestSelling.MultiSelect = false;
            this.dgvBestSelling.Name = "dgvBestSelling";
            this.dgvBestSelling.ReadOnly = true;
            this.dgvBestSelling.RowHeadersVisible = false;
            this.dgvBestSelling.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBestSelling.Size = new System.Drawing.Size(958, 368);
            this.dgvBestSelling.TabIndex = 1;
            // 
            // pnlGridHeader
            // 
            this.pnlGridHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(238)))), ((int)(((byte)(230)))));
            this.pnlGridHeader.Controls.Add(this.lblPeriodSummary);
            this.pnlGridHeader.Controls.Add(this.lblGridTitle);
            this.pnlGridHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGridHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlGridHeader.Name = "pnlGridHeader";
            this.pnlGridHeader.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlGridHeader.Size = new System.Drawing.Size(958, 38);
            this.pnlGridHeader.TabIndex = 0;
            // 
            // lblPeriodSummary
            // 
            this.lblPeriodSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPeriodSummary.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblPeriodSummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(45)))));
            this.lblPeriodSummary.Location = new System.Drawing.Point(550, 9);
            this.lblPeriodSummary.Name = "lblPeriodSummary";
            this.lblPeriodSummary.Size = new System.Drawing.Size(395, 18);
            this.lblPeriodSummary.TabIndex = 1;
            this.lblPeriodSummary.Text = "Showing results for selected period";
            this.lblPeriodSummary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGridTitle
            // 
            this.lblGridTitle.AutoSize = true;
            this.lblGridTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGridTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(33)))), ((int)(((byte)(9)))));
            this.lblGridTitle.Location = new System.Drawing.Point(8, 9);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Size = new System.Drawing.Size(287, 19);
            this.lblGridTitle.TabIndex = 0;
            this.lblGridTitle.Text = "🏆 Best-Selling Products (Ranked by Qty)";
            // 
            // pnlSummary
            // 
            this.pnlSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(240)))));
            this.pnlSummary.Controls.Add(this.tblSummary);
            this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSummary.Location = new System.Drawing.Point(0, 0);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);
            this.pnlSummary.Size = new System.Drawing.Size(992, 88);
            this.pnlSummary.TabIndex = 0;
            // 
            // tblSummary
            // 
            this.tblSummary.ColumnCount = 4;
            this.tblSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblSummary.Controls.Add(this.pnlCardSales, 0, 0);
            this.tblSummary.Controls.Add(this.pnlCardTrans, 1, 0);
            this.tblSummary.Controls.Add(this.pnlCardItems, 2, 0);
            this.tblSummary.Controls.Add(this.pnlCardAvg, 3, 0);
            this.tblSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblSummary.Location = new System.Drawing.Point(16, 10);
            this.tblSummary.Name = "tblSummary";
            this.tblSummary.RowCount = 1;
            this.tblSummary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblSummary.Size = new System.Drawing.Size(960, 68);
            this.tblSummary.TabIndex = 0;
            // 
            // pnlCardSales
            // 
            this.pnlCardSales.BackColor = System.Drawing.Color.White;
            this.pnlCardSales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCardSales.Controls.Add(this.lblValTotalSales);
            this.pnlCardSales.Controls.Add(this.lblTitleSales);
            this.pnlCardSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardSales.Location = new System.Drawing.Point(3, 3);
            this.pnlCardSales.Name = "pnlCardSales";
            this.pnlCardSales.Size = new System.Drawing.Size(234, 62);
            this.pnlCardSales.TabIndex = 0;
            // 
            // lblValTotalSales
            // 
            this.lblValTotalSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValTotalSales.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.lblValTotalSales.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(60)))), ((int)(((byte)(20)))));
            this.lblValTotalSales.Location = new System.Drawing.Point(0, 20);
            this.lblValTotalSales.Name = "lblValTotalSales";
            this.lblValTotalSales.Size = new System.Drawing.Size(232, 40);
            this.lblValTotalSales.TabIndex = 1;
            this.lblValTotalSales.Text = "₱0.00";
            this.lblValTotalSales.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleSales
            // 
            this.lblTitleSales.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleSales.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblTitleSales.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(45)))));
            this.lblTitleSales.Location = new System.Drawing.Point(0, 0);
            this.lblTitleSales.Name = "lblTitleSales";
            this.lblTitleSales.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblTitleSales.Size = new System.Drawing.Size(232, 20);
            this.lblTitleSales.TabIndex = 0;
            this.lblTitleSales.Text = "TOTAL SALES";
            this.lblTitleSales.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCardTrans
            // 
            this.pnlCardTrans.BackColor = System.Drawing.Color.White;
            this.pnlCardTrans.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCardTrans.Controls.Add(this.lblValTransactions);
            this.pnlCardTrans.Controls.Add(this.lblTitleTrans);
            this.pnlCardTrans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardTrans.Location = new System.Drawing.Point(243, 3);
            this.pnlCardTrans.Name = "pnlCardTrans";
            this.pnlCardTrans.Size = new System.Drawing.Size(234, 62);
            this.pnlCardTrans.TabIndex = 1;
            // 
            // lblValTransactions
            // 
            this.lblValTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValTransactions.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.lblValTransactions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(30)))), ((int)(((byte)(20)))));
            this.lblValTransactions.Location = new System.Drawing.Point(0, 20);
            this.lblValTransactions.Name = "lblValTransactions";
            this.lblValTransactions.Size = new System.Drawing.Size(232, 40);
            this.lblValTransactions.TabIndex = 1;
            this.lblValTransactions.Text = "0";
            this.lblValTransactions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleTrans
            // 
            this.lblTitleTrans.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleTrans.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblTitleTrans.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(45)))));
            this.lblTitleTrans.Location = new System.Drawing.Point(0, 0);
            this.lblTitleTrans.Name = "lblTitleTrans";
            this.lblTitleTrans.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblTitleTrans.Size = new System.Drawing.Size(232, 20);
            this.lblTitleTrans.TabIndex = 0;
            this.lblTitleTrans.Text = "TOTAL TRANSACTIONS";
            this.lblTitleTrans.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCardItems
            // 
            this.pnlCardItems.BackColor = System.Drawing.Color.White;
            this.pnlCardItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCardItems.Controls.Add(this.lblValItemsSold);
            this.pnlCardItems.Controls.Add(this.lblTitleItems);
            this.pnlCardItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardItems.Location = new System.Drawing.Point(483, 3);
            this.pnlCardItems.Name = "pnlCardItems";
            this.pnlCardItems.Size = new System.Drawing.Size(234, 62);
            this.pnlCardItems.TabIndex = 2;
            // 
            // lblValItemsSold
            // 
            this.lblValItemsSold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValItemsSold.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.lblValItemsSold.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(30)))), ((int)(((byte)(20)))));
            this.lblValItemsSold.Location = new System.Drawing.Point(0, 20);
            this.lblValItemsSold.Name = "lblValItemsSold";
            this.lblValItemsSold.Size = new System.Drawing.Size(232, 40);
            this.lblValItemsSold.TabIndex = 1;
            this.lblValItemsSold.Text = "0";
            this.lblValItemsSold.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleItems
            // 
            this.lblTitleItems.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleItems.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblTitleItems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(45)))));
            this.lblTitleItems.Location = new System.Drawing.Point(0, 0);
            this.lblTitleItems.Name = "lblTitleItems";
            this.lblTitleItems.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblTitleItems.Size = new System.Drawing.Size(232, 20);
            this.lblTitleItems.TabIndex = 0;
            this.lblTitleItems.Text = "TOTAL ITEMS SOLD";
            this.lblTitleItems.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCardAvg
            // 
            this.pnlCardAvg.BackColor = System.Drawing.Color.White;
            this.pnlCardAvg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCardAvg.Controls.Add(this.lblValAvgOrder);
            this.pnlCardAvg.Controls.Add(this.lblTitleAvg);
            this.pnlCardAvg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardAvg.Location = new System.Drawing.Point(723, 3);
            this.pnlCardAvg.Name = "pnlCardAvg";
            this.pnlCardAvg.Size = new System.Drawing.Size(234, 62);
            this.pnlCardAvg.TabIndex = 3;
            // 
            // lblValAvgOrder
            // 
            this.lblValAvgOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValAvgOrder.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.lblValAvgOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(60)))), ((int)(((byte)(20)))));
            this.lblValAvgOrder.Location = new System.Drawing.Point(0, 20);
            this.lblValAvgOrder.Name = "lblValAvgOrder";
            this.lblValAvgOrder.Size = new System.Drawing.Size(232, 40);
            this.lblValAvgOrder.TabIndex = 1;
            this.lblValAvgOrder.Text = "₱0.00";
            this.lblValAvgOrder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleAvg
            // 
            this.lblTitleAvg.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleAvg.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblTitleAvg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(45)))));
            this.lblTitleAvg.Location = new System.Drawing.Point(0, 0);
            this.lblTitleAvg.Name = "lblTitleAvg";
            this.lblTitleAvg.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblTitleAvg.Size = new System.Drawing.Size(232, 20);
            this.lblTitleAvg.TabIndex = 0;
            this.lblTitleAvg.Text = "AVERAGE SALE";
            this.lblTitleAvg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabInventory
            // 
            this.tabInventory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(240)))));
            this.tabInventory.Controls.Add(this.pnlInventoryContent);
            this.tabInventory.Controls.Add(this.pnlInventorySummary);
            this.tabInventory.Location = new System.Drawing.Point(4, 36);
            this.tabInventory.Name = "tabInventory";
            this.tabInventory.Size = new System.Drawing.Size(992, 512);
            this.tabInventory.TabIndex = 1;
            this.tabInventory.Text = "📦 Inventory Reports";
            // 
            // pnlInventoryContent
            // 
            this.pnlInventoryContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(240)))));
            this.pnlInventoryContent.Controls.Add(this.pnlInventoryGridContainer);
            this.pnlInventoryContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInventoryContent.Location = new System.Drawing.Point(0, 88);
            this.pnlInventoryContent.Name = "pnlInventoryContent";
            this.pnlInventoryContent.Padding = new System.Windows.Forms.Padding(16, 0, 16, 16);
            this.pnlInventoryContent.Size = new System.Drawing.Size(992, 424);
            this.pnlInventoryContent.TabIndex = 1;
            // 
            // pnlInventoryGridContainer
            // 
            this.pnlInventoryGridContainer.BackColor = System.Drawing.Color.White;
            this.pnlInventoryGridContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInventoryGridContainer.Controls.Add(this.dgvInventory);
            this.pnlInventoryGridContainer.Controls.Add(this.pnlInventoryGridHeader);
            this.pnlInventoryGridContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInventoryGridContainer.Location = new System.Drawing.Point(16, 0);
            this.pnlInventoryGridContainer.Name = "pnlInventoryGridContainer";
            this.pnlInventoryGridContainer.Size = new System.Drawing.Size(960, 408);
            this.pnlInventoryGridContainer.TabIndex = 0;
            // 
            // dgvInventory
            // 
            this.dgvInventory.AllowUserToAddRows = false;
            this.dgvInventory.AllowUserToDeleteRows = false;
            this.dgvInventory.AllowUserToResizeRows = false;
            this.dgvInventory.BackgroundColor = System.Drawing.Color.White;
            this.dgvInventory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvInventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInventory.Location = new System.Drawing.Point(0, 38);
            this.dgvInventory.MultiSelect = false;
            this.dgvInventory.Name = "dgvInventory";
            this.dgvInventory.ReadOnly = true;
            this.dgvInventory.RowHeadersVisible = false;
            this.dgvInventory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInventory.Size = new System.Drawing.Size(958, 368);
            this.dgvInventory.TabIndex = 1;
            // 
            // pnlInventoryGridHeader
            // 
            this.pnlInventoryGridHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(238)))), ((int)(((byte)(230)))));
            this.pnlInventoryGridHeader.Controls.Add(this.lblInventoryNote);
            this.pnlInventoryGridHeader.Controls.Add(this.lblInventoryGridTitle);
            this.pnlInventoryGridHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInventoryGridHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlInventoryGridHeader.Name = "pnlInventoryGridHeader";
            this.pnlInventoryGridHeader.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlInventoryGridHeader.Size = new System.Drawing.Size(958, 38);
            this.pnlInventoryGridHeader.TabIndex = 0;
            // 
            // lblInventoryNote
            // 
            this.lblInventoryNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInventoryNote.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblInventoryNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(45)))));
            this.lblInventoryNote.Location = new System.Drawing.Point(420, 9);
            this.lblInventoryNote.Name = "lblInventoryNote";
            this.lblInventoryNote.Size = new System.Drawing.Size(525, 18);
            this.lblInventoryNote.TabIndex = 1;
            this.lblInventoryNote.Text = "* Quantity Sold reflects sales only. Low Stock threshold: ≤ 10 units.";
            this.lblInventoryNote.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblInventoryGridTitle
            // 
            this.lblInventoryGridTitle.AutoSize = true;
            this.lblInventoryGridTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblInventoryGridTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(33)))), ((int)(((byte)(9)))));
            this.lblInventoryGridTitle.Location = new System.Drawing.Point(8, 9);
            this.lblInventoryGridTitle.Name = "lblInventoryGridTitle";
            this.lblInventoryGridTitle.Size = new System.Drawing.Size(260, 19);
            this.lblInventoryGridTitle.TabIndex = 0;
            this.lblInventoryGridTitle.Text = "📦 Product Stock & Sales Performance";
            // 
            // pnlInventorySummary
            // 
            this.pnlInventorySummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(240)))));
            this.pnlInventorySummary.Controls.Add(this.tblInventorySummary);
            this.pnlInventorySummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInventorySummary.Location = new System.Drawing.Point(0, 0);
            this.pnlInventorySummary.Name = "pnlInventorySummary";
            this.pnlInventorySummary.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);
            this.pnlInventorySummary.Size = new System.Drawing.Size(992, 88);
            this.pnlInventorySummary.TabIndex = 0;
            // 
            // tblInventorySummary
            // 
            this.tblInventorySummary.ColumnCount = 3;
            this.tblInventorySummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblInventorySummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblInventorySummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblInventorySummary.Controls.Add(this.pnlCardActiveProd, 0, 0);
            this.tblInventorySummary.Controls.Add(this.pnlCardStockUnits, 1, 0);
            this.tblInventorySummary.Controls.Add(this.pnlCardLowStock, 2, 0);
            this.tblInventorySummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblInventorySummary.Location = new System.Drawing.Point(16, 10);
            this.tblInventorySummary.Name = "tblInventorySummary";
            this.tblInventorySummary.RowCount = 1;
            this.tblInventorySummary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblInventorySummary.Size = new System.Drawing.Size(960, 68);
            this.tblInventorySummary.TabIndex = 0;
            // 
            // pnlCardActiveProd
            // 
            this.pnlCardActiveProd.BackColor = System.Drawing.Color.White;
            this.pnlCardActiveProd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCardActiveProd.Controls.Add(this.lblValActiveProducts);
            this.pnlCardActiveProd.Controls.Add(this.lblTitleActiveProd);
            this.pnlCardActiveProd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardActiveProd.Location = new System.Drawing.Point(3, 3);
            this.pnlCardActiveProd.Name = "pnlCardActiveProd";
            this.pnlCardActiveProd.Size = new System.Drawing.Size(314, 62);
            this.pnlCardActiveProd.TabIndex = 0;
            // 
            // lblValActiveProducts
            // 
            this.lblValActiveProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValActiveProducts.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.lblValActiveProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(30)))), ((int)(((byte)(20)))));
            this.lblValActiveProducts.Location = new System.Drawing.Point(0, 20);
            this.lblValActiveProducts.Name = "lblValActiveProducts";
            this.lblValActiveProducts.Size = new System.Drawing.Size(312, 40);
            this.lblValActiveProducts.TabIndex = 1;
            this.lblValActiveProducts.Text = "0";
            this.lblValActiveProducts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleActiveProd
            // 
            this.lblTitleActiveProd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleActiveProd.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblTitleActiveProd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(45)))));
            this.lblTitleActiveProd.Location = new System.Drawing.Point(0, 0);
            this.lblTitleActiveProd.Name = "lblTitleActiveProd";
            this.lblTitleActiveProd.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblTitleActiveProd.Size = new System.Drawing.Size(312, 20);
            this.lblTitleActiveProd.TabIndex = 0;
            this.lblTitleActiveProd.Text = "TOTAL ACTIVE PRODUCTS";
            this.lblTitleActiveProd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCardStockUnits
            // 
            this.pnlCardStockUnits.BackColor = System.Drawing.Color.White;
            this.pnlCardStockUnits.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCardStockUnits.Controls.Add(this.lblValTotalStockUnits);
            this.pnlCardStockUnits.Controls.Add(this.lblTitleStockUnits);
            this.pnlCardStockUnits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardStockUnits.Location = new System.Drawing.Point(323, 3);
            this.pnlCardStockUnits.Name = "pnlCardStockUnits";
            this.pnlCardStockUnits.Size = new System.Drawing.Size(314, 62);
            this.pnlCardStockUnits.TabIndex = 1;
            // 
            // lblValTotalStockUnits
            // 
            this.lblValTotalStockUnits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValTotalStockUnits.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.lblValTotalStockUnits.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(60)))), ((int)(((byte)(20)))));
            this.lblValTotalStockUnits.Location = new System.Drawing.Point(0, 20);
            this.lblValTotalStockUnits.Name = "lblValTotalStockUnits";
            this.lblValTotalStockUnits.Size = new System.Drawing.Size(312, 40);
            this.lblValTotalStockUnits.TabIndex = 1;
            this.lblValTotalStockUnits.Text = "0";
            this.lblValTotalStockUnits.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleStockUnits
            // 
            this.lblTitleStockUnits.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleStockUnits.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblTitleStockUnits.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(45)))));
            this.lblTitleStockUnits.Location = new System.Drawing.Point(0, 0);
            this.lblTitleStockUnits.Name = "lblTitleStockUnits";
            this.lblTitleStockUnits.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblTitleStockUnits.Size = new System.Drawing.Size(312, 20);
            this.lblTitleStockUnits.TabIndex = 0;
            this.lblTitleStockUnits.Text = "TOTAL UNITS CURRENTLY IN STOCK";
            this.lblTitleStockUnits.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCardLowStock
            // 
            this.pnlCardLowStock.BackColor = System.Drawing.Color.White;
            this.pnlCardLowStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCardLowStock.Controls.Add(this.lblValLowStockCount);
            this.pnlCardLowStock.Controls.Add(this.lblTitleLowStock);
            this.pnlCardLowStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardLowStock.Location = new System.Drawing.Point(643, 3);
            this.pnlCardLowStock.Name = "pnlCardLowStock";
            this.pnlCardLowStock.Size = new System.Drawing.Size(314, 62);
            this.pnlCardLowStock.TabIndex = 2;
            // 
            // lblValLowStockCount
            // 
            this.lblValLowStockCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValLowStockCount.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.lblValLowStockCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(30)))), ((int)(((byte)(20)))));
            this.lblValLowStockCount.Location = new System.Drawing.Point(0, 20);
            this.lblValLowStockCount.Name = "lblValLowStockCount";
            this.lblValLowStockCount.Size = new System.Drawing.Size(312, 40);
            this.lblValLowStockCount.TabIndex = 1;
            this.lblValLowStockCount.Text = "0";
            this.lblValLowStockCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleLowStock
            // 
            this.lblTitleLowStock.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleLowStock.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblTitleLowStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(45)))));
            this.lblTitleLowStock.Location = new System.Drawing.Point(0, 0);
            this.lblTitleLowStock.Name = "lblTitleLowStock";
            this.lblTitleLowStock.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblTitleLowStock.Size = new System.Drawing.Size(312, 20);
            this.lblTitleLowStock.TabIndex = 0;
            this.lblTitleLowStock.Text = "LOW-STOCK PRODUCTS (≤ 10 UNITS)";
            this.lblTitleLowStock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabAiReports
            // 
            this.tabAiReports.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(240)))));
            this.tabAiReports.Controls.Add(this.pnlAiContent);
            this.tabAiReports.Location = new System.Drawing.Point(4, 36);
            this.tabAiReports.Name = "tabAiReports";
            this.tabAiReports.Size = new System.Drawing.Size(992, 512);
            this.tabAiReports.TabIndex = 2;
            this.tabAiReports.Text = "🤖 AI Smart Reports";
            // 
            // pnlAiContent
            // 
            this.pnlAiContent.AutoScroll = true;
            this.pnlAiContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(240)))));
            this.pnlAiContent.Controls.Add(this.pnlAiRestockContainer);
            this.pnlAiContent.Controls.Add(this.pnlAiSpacer2);
            this.pnlAiContent.Controls.Add(this.pnlCardAiTrends);
            this.pnlAiContent.Controls.Add(this.pnlAiSpacer1);
            this.pnlAiContent.Controls.Add(this.pnlAiSummaryCard);
            this.pnlAiContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAiContent.Location = new System.Drawing.Point(0, 0);
            this.pnlAiContent.Name = "pnlAiContent";
            this.pnlAiContent.Padding = new System.Windows.Forms.Padding(16, 10, 16, 16);
            this.pnlAiContent.Size = new System.Drawing.Size(992, 512);
            this.pnlAiContent.TabIndex = 0;
            // 
            // pnlAiRestockContainer
            // 
            this.pnlAiRestockContainer.BackColor = System.Drawing.Color.White;
            this.pnlAiRestockContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAiRestockContainer.Controls.Add(this.dgvRestockSuggestions);
            this.pnlAiRestockContainer.Controls.Add(this.pnlAiRestockHeader);
            this.pnlAiRestockContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAiRestockContainer.Location = new System.Drawing.Point(16, 270);
            this.pnlAiRestockContainer.Name = "pnlAiRestockContainer";
            this.pnlAiRestockContainer.Size = new System.Drawing.Size(943, 320);
            this.pnlAiRestockContainer.TabIndex = 2;
            // 
            // dgvRestockSuggestions
            // 
            this.dgvRestockSuggestions.AllowUserToAddRows = false;
            this.dgvRestockSuggestions.AllowUserToDeleteRows = false;
            this.dgvRestockSuggestions.AllowUserToResizeRows = false;
            this.dgvRestockSuggestions.BackgroundColor = System.Drawing.Color.White;
            this.dgvRestockSuggestions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRestockSuggestions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvRestockSuggestions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRestockSuggestions.Location = new System.Drawing.Point(0, 38);
            this.dgvRestockSuggestions.MultiSelect = false;
            this.dgvRestockSuggestions.Name = "dgvRestockSuggestions";
            this.dgvRestockSuggestions.ReadOnly = true;
            this.dgvRestockSuggestions.RowHeadersVisible = false;
            this.dgvRestockSuggestions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRestockSuggestions.Size = new System.Drawing.Size(941, 280);
            this.dgvRestockSuggestions.TabIndex = 1;
            // 
            // pnlAiRestockHeader
            // 
            this.pnlAiRestockHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(238)))), ((int)(((byte)(230)))));
            this.pnlAiRestockHeader.Controls.Add(this.lblAiRestockNote);
            this.pnlAiRestockHeader.Controls.Add(this.lblAiRestockTitle);
            this.pnlAiRestockHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAiRestockHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlAiRestockHeader.Name = "pnlAiRestockHeader";
            this.pnlAiRestockHeader.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlAiRestockHeader.Size = new System.Drawing.Size(941, 38);
            this.pnlAiRestockHeader.TabIndex = 0;
            // 
            // lblAiRestockNote
            // 
            this.lblAiRestockNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAiRestockNote.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblAiRestockNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(45)))));
            this.lblAiRestockNote.Location = new System.Drawing.Point(403, 9);
            this.lblAiRestockNote.Name = "lblAiRestockNote";
            this.lblAiRestockNote.Size = new System.Drawing.Size(525, 18);
            this.lblAiRestockNote.TabIndex = 1;
            this.lblAiRestockNote.Text = "* Low stock threshold ≤ 10 units. Prioritized by sales activity.";
            this.lblAiRestockNote.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAiRestockTitle
            // 
            this.lblAiRestockTitle.AutoSize = true;
            this.lblAiRestockTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAiRestockTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(33)))), ((int)(((byte)(9)))));
            this.lblAiRestockTitle.Location = new System.Drawing.Point(8, 9);
            this.lblAiRestockTitle.Name = "lblAiRestockTitle";
            this.lblAiRestockTitle.Size = new System.Drawing.Size(213, 19);
            this.lblAiRestockTitle.TabIndex = 0;
            this.lblAiRestockTitle.Text = "🔄 Smart Restock Suggestions";
            // 
            // pnlAiSpacer2
            // 
            this.pnlAiSpacer2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAiSpacer2.Location = new System.Drawing.Point(16, 260);
            this.pnlAiSpacer2.Name = "pnlAiSpacer2";
            this.pnlAiSpacer2.Size = new System.Drawing.Size(943, 10);
            this.pnlAiSpacer2.TabIndex = 4;
            // 
            // pnlCardAiTrends
            // 
            this.pnlCardAiTrends.BackColor = System.Drawing.Color.White;
            this.pnlCardAiTrends.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCardAiTrends.Controls.Add(this.lblValAiTrends);
            this.pnlCardAiTrends.Controls.Add(this.lblTitleAiTrends);
            this.pnlCardAiTrends.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCardAiTrends.Location = new System.Drawing.Point(16, 160);
            this.pnlCardAiTrends.Name = "pnlCardAiTrends";
            this.pnlCardAiTrends.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlCardAiTrends.Size = new System.Drawing.Size(943, 100);
            this.pnlCardAiTrends.TabIndex = 1;
            // 
            // lblValAiTrends
            // 
            this.lblValAiTrends.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValAiTrends.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblValAiTrends.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(30)))), ((int)(((byte)(20)))));
            this.lblValAiTrends.Location = new System.Drawing.Point(12, 28);
            this.lblValAiTrends.Name = "lblValAiTrends";
            this.lblValAiTrends.Size = new System.Drawing.Size(917, 62);
            this.lblValAiTrends.TabIndex = 1;
            this.lblValAiTrends.Text = "Highest Day: -\r\nLowest Day: -\r\nActive Selling Days: -";
            // 
            // lblTitleAiTrends
            // 
            this.lblTitleAiTrends.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleAiTrends.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblTitleAiTrends.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(45)))));
            this.lblTitleAiTrends.Location = new System.Drawing.Point(12, 8);
            this.lblTitleAiTrends.Name = "lblTitleAiTrends";
            this.lblTitleAiTrends.Size = new System.Drawing.Size(917, 20);
            this.lblTitleAiTrends.TabIndex = 0;
            this.lblTitleAiTrends.Text = "📈 SALES TRENDS";
            // 
            // pnlAiSpacer1
            // 
            this.pnlAiSpacer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAiSpacer1.Location = new System.Drawing.Point(16, 150);
            this.pnlAiSpacer1.Name = "pnlAiSpacer1";
            this.pnlAiSpacer1.Size = new System.Drawing.Size(943, 10);
            this.pnlAiSpacer1.TabIndex = 3;
            // 
            // pnlAiSummaryCard
            // 
            this.pnlAiSummaryCard.BackColor = System.Drawing.Color.White;
            this.pnlAiSummaryCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAiSummaryCard.Controls.Add(this.lblAiSummaryText);
            this.pnlAiSummaryCard.Controls.Add(this.lblAiSummaryTitle);
            this.pnlAiSummaryCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAiSummaryCard.Location = new System.Drawing.Point(16, 10);
            this.pnlAiSummaryCard.Name = "pnlAiSummaryCard";
            this.pnlAiSummaryCard.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlAiSummaryCard.Size = new System.Drawing.Size(943, 140);
            this.pnlAiSummaryCard.TabIndex = 0;
            // 
            // lblAiSummaryText
            // 
            this.lblAiSummaryText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAiSummaryText.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblAiSummaryText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(30)))), ((int)(((byte)(20)))));
            this.lblAiSummaryText.Location = new System.Drawing.Point(12, 30);
            this.lblAiSummaryText.Name = "lblAiSummaryText";
            this.lblAiSummaryText.Size = new System.Drawing.Size(917, 100);
            this.lblAiSummaryText.TabIndex = 1;
            this.lblAiSummaryText.Text = "Generating business summary...";
            // 
            // lblAiSummaryTitle
            // 
            this.lblAiSummaryTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAiSummaryTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblAiSummaryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(70)))), ((int)(((byte)(45)))));
            this.lblAiSummaryTitle.Location = new System.Drawing.Point(12, 8);
            this.lblAiSummaryTitle.Name = "lblAiSummaryTitle";
            this.lblAiSummaryTitle.Size = new System.Drawing.Size(917, 22);
            this.lblAiSummaryTitle.TabIndex = 0;
            this.lblAiSummaryTitle.Text = "💡 BUSINESS SUMMARY";
            // 
            // frmReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.tabReports);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.MinimumSize = new System.Drawing.Size(920, 640);
            this.Name = "frmReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bread Basket Bakeshop - Sales & Inventory Reports";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.tabReports.ResumeLayout(false);
            this.tabSales.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlGridContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBestSelling)).EndInit();
            this.pnlGridHeader.ResumeLayout(false);
            this.pnlGridHeader.PerformLayout();
            this.pnlSummary.ResumeLayout(false);
            this.tblSummary.ResumeLayout(false);
            this.pnlCardSales.ResumeLayout(false);
            this.pnlCardTrans.ResumeLayout(false);
            this.pnlCardItems.ResumeLayout(false);
            this.pnlCardAvg.ResumeLayout(false);
            this.tabInventory.ResumeLayout(false);
            this.pnlInventoryContent.ResumeLayout(false);
            this.pnlInventoryGridContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).EndInit();
            this.pnlInventoryGridHeader.ResumeLayout(false);
            this.pnlInventoryGridHeader.PerformLayout();
            this.pnlInventorySummary.ResumeLayout(false);
            this.tblInventorySummary.ResumeLayout(false);
            this.pnlCardActiveProd.ResumeLayout(false);
            this.pnlCardStockUnits.ResumeLayout(false);
            this.pnlCardLowStock.ResumeLayout(false);
            this.tabAiReports.ResumeLayout(false);
            this.pnlAiContent.ResumeLayout(false);
            this.pnlAiRestockContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRestockSuggestions)).EndInit();
            this.pnlAiRestockHeader.ResumeLayout(false);
            this.pnlAiRestockHeader.PerformLayout();
            this.pnlCardAiTrends.ResumeLayout(false);
            this.pnlAiSummaryCard.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblStoreTitle;
        private System.Windows.Forms.Label lblReportSubtitle;
        private System.Windows.Forms.Button btnCashierShiftReport;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.ComboBox cmbPeriod;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.TabControl tabReports;
        private System.Windows.Forms.TabPage tabSales;
        private System.Windows.Forms.TabPage tabInventory;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.TableLayoutPanel tblSummary;
        private System.Windows.Forms.Panel pnlCardSales;
        private System.Windows.Forms.Label lblTitleSales;
        private System.Windows.Forms.Label lblValTotalSales;
        private System.Windows.Forms.Panel pnlCardTrans;
        private System.Windows.Forms.Label lblTitleTrans;
        private System.Windows.Forms.Label lblValTransactions;
        private System.Windows.Forms.Panel pnlCardItems;
        private System.Windows.Forms.Label lblTitleItems;
        private System.Windows.Forms.Label lblValItemsSold;
        private System.Windows.Forms.Panel pnlCardAvg;
        private System.Windows.Forms.Label lblTitleAvg;
        private System.Windows.Forms.Label lblValAvgOrder;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlGridContainer;
        private System.Windows.Forms.Panel pnlGridHeader;
        private System.Windows.Forms.Label lblGridTitle;
        private System.Windows.Forms.Label lblPeriodSummary;
        private System.Windows.Forms.DataGridView dgvBestSelling;
        private System.Windows.Forms.Panel pnlInventorySummary;
        private System.Windows.Forms.TableLayoutPanel tblInventorySummary;
        private System.Windows.Forms.Panel pnlCardActiveProd;
        private System.Windows.Forms.Label lblTitleActiveProd;
        private System.Windows.Forms.Label lblValActiveProducts;
        private System.Windows.Forms.Panel pnlCardStockUnits;
        private System.Windows.Forms.Label lblTitleStockUnits;
        private System.Windows.Forms.Label lblValTotalStockUnits;
        private System.Windows.Forms.Panel pnlCardLowStock;
        private System.Windows.Forms.Label lblTitleLowStock;
        private System.Windows.Forms.Label lblValLowStockCount;
        private System.Windows.Forms.Panel pnlInventoryContent;
        private System.Windows.Forms.Panel pnlInventoryGridContainer;
        private System.Windows.Forms.Panel pnlInventoryGridHeader;
        private System.Windows.Forms.Label lblInventoryGridTitle;
        private System.Windows.Forms.Label lblInventoryNote;
        private System.Windows.Forms.DataGridView dgvInventory;
        private System.Windows.Forms.TabPage tabAiReports;
        private System.Windows.Forms.Panel pnlAiContent;
        private System.Windows.Forms.Panel pnlAiSummaryCard;
        private System.Windows.Forms.Label lblAiSummaryTitle;
        private System.Windows.Forms.Label lblAiSummaryText;
        private System.Windows.Forms.Panel pnlCardAiTrends;
        private System.Windows.Forms.Label lblTitleAiTrends;
        private System.Windows.Forms.Label lblValAiTrends;
        private System.Windows.Forms.Panel pnlAiSpacer1;
        private System.Windows.Forms.Panel pnlAiSpacer2;
        private System.Windows.Forms.Panel pnlAiRestockContainer;
        private System.Windows.Forms.Panel pnlAiRestockHeader;
        private System.Windows.Forms.Label lblAiRestockTitle;
        private System.Windows.Forms.Label lblAiRestockNote;
        private System.Windows.Forms.DataGridView dgvRestockSuggestions;
    }
}

