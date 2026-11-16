namespace BakeryPOS
{
    partial class frmEndOfShiftReport
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblGeneratedTime = new System.Windows.Forms.Label();
            this.lblReportDate = new System.Windows.Forms.Label();
            this.lblCashierInfo = new System.Windows.Forms.Label();
            this.lblReportSubtitle = new System.Windows.Forms.Label();
            this.lblStoreTitle = new System.Windows.Forms.Label();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.tblSummary = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCard1 = new System.Windows.Forms.Panel();
            this.lblValTransactions = new System.Windows.Forms.Label();
            this.lblTitleTransactions = new System.Windows.Forms.Label();
            this.pnlCard2 = new System.Windows.Forms.Panel();
            this.lblValSales = new System.Windows.Forms.Label();
            this.lblTitleSales = new System.Windows.Forms.Label();
            this.pnlCard3 = new System.Windows.Forms.Panel();
            this.lblValItemsSold = new System.Windows.Forms.Label();
            this.lblTitleItemsSold = new System.Windows.Forms.Label();
            this.pnlCard4 = new System.Windows.Forms.Panel();
            this.lblValAvgOrder = new System.Windows.Forms.Label();
            this.lblTitleAvgOrder = new System.Windows.Forms.Label();
            this.pnlCard5 = new System.Windows.Forms.Panel();
            this.lblValCash = new System.Windows.Forms.Label();
            this.lblTitleCash = new System.Windows.Forms.Label();
            this.pnlCard6 = new System.Windows.Forms.Panel();
            this.lblValChange = new System.Windows.Forms.Label();
            this.lblTitleChange = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tblGrids = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTransSection = new System.Windows.Forms.Panel();
            this.dgvTransactions = new System.Windows.Forms.DataGridView();
            this.lblTransHeader = new System.Windows.Forms.Label();
            this.pnlProdSection = new System.Windows.Forms.Panel();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.lblProdHeader = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlSummary.SuspendLayout();
            this.tblSummary.SuspendLayout();
            this.pnlCard1.SuspendLayout();
            this.pnlCard2.SuspendLayout();
            this.pnlCard3.SuspendLayout();
            this.pnlCard4.SuspendLayout();
            this.pnlCard5.SuspendLayout();
            this.pnlCard6.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.tblGrids.SuspendLayout();
            this.pnlTransSection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).BeginInit();
            this.pnlProdSection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(22)))), ((int)(((byte)(8)))));
            this.pnlHeader.Controls.Add(this.lblGeneratedTime);
            this.pnlHeader.Controls.Add(this.lblReportDate);
            this.pnlHeader.Controls.Add(this.lblCashierInfo);
            this.pnlHeader.Controls.Add(this.lblReportSubtitle);
            this.pnlHeader.Controls.Add(this.lblStoreTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.pnlHeader.Size = new System.Drawing.Size(920, 85);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblGeneratedTime
            // 
            this.lblGeneratedTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGeneratedTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGeneratedTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(210)))), ((int)(((byte)(200)))));
            this.lblGeneratedTime.Location = new System.Drawing.Point(540, 56);
            this.lblGeneratedTime.Name = "lblGeneratedTime";
            this.lblGeneratedTime.Size = new System.Drawing.Size(364, 18);
            this.lblGeneratedTime.TabIndex = 4;
            this.lblGeneratedTime.Text = "Generated: -";
            this.lblGeneratedTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblReportDate
            // 
            this.lblReportDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReportDate.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblReportDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(235)))), ((int)(((byte)(230)))));
            this.lblReportDate.Location = new System.Drawing.Point(540, 34);
            this.lblReportDate.Name = "lblReportDate";
            this.lblReportDate.Size = new System.Drawing.Size(364, 20);
            this.lblReportDate.TabIndex = 3;
            this.lblReportDate.Text = "Report Date: -";
            this.lblReportDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCashierInfo
            // 
            this.lblCashierInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCashierInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblCashierInfo.ForeColor = System.Drawing.Color.White;
            this.lblCashierInfo.Location = new System.Drawing.Point(540, 12);
            this.lblCashierInfo.Name = "lblCashierInfo";
            this.lblCashierInfo.Size = new System.Drawing.Size(364, 20);
            this.lblCashierInfo.TabIndex = 2;
            this.lblCashierInfo.Text = "Cashier: -";
            this.lblCashierInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblReportSubtitle
            // 
            this.lblReportSubtitle.AutoSize = true;
            this.lblReportSubtitle.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblReportSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(150)))));
            this.lblReportSubtitle.Location = new System.Drawing.Point(161, 47);
            this.lblReportSubtitle.Name = "lblReportSubtitle";
            this.lblReportSubtitle.Size = new System.Drawing.Size(200, 21);
            this.lblReportSubtitle.TabIndex = 1;
            this.lblReportSubtitle.Text = "End-of-Shift Sales Report";
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
            // pnlSummary
            // 
            this.pnlSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(238)))), ((int)(((byte)(230)))));
            this.pnlSummary.Controls.Add(this.tblSummary);
            this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSummary.Location = new System.Drawing.Point(0, 92);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Padding = new System.Windows.Forms.Padding(12, 10, 12, 10);
            this.pnlSummary.Size = new System.Drawing.Size(920, 84);
            this.pnlSummary.TabIndex = 1;
            // 
            // tblSummary
            // 
            this.tblSummary.ColumnCount = 6;
            this.tblSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblSummary.Controls.Add(this.pnlCard1, 0, 0);
            this.tblSummary.Controls.Add(this.pnlCard2, 1, 0);
            this.tblSummary.Controls.Add(this.pnlCard3, 2, 0);
            this.tblSummary.Controls.Add(this.pnlCard4, 3, 0);
            this.tblSummary.Controls.Add(this.pnlCard5, 4, 0);
            this.tblSummary.Controls.Add(this.pnlCard6, 5, 0);
            this.tblSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblSummary.Location = new System.Drawing.Point(12, 10);
            this.tblSummary.Name = "tblSummary";
            this.tblSummary.RowCount = 1;
            this.tblSummary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblSummary.Size = new System.Drawing.Size(896, 64);
            this.tblSummary.TabIndex = 0;
            // 
            // pnlCard1
            // 
            this.pnlCard1.BackColor = System.Drawing.Color.White;
            this.pnlCard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard1.Controls.Add(this.lblValTransactions);
            this.pnlCard1.Controls.Add(this.lblTitleTransactions);
            this.pnlCard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCard1.Location = new System.Drawing.Point(3, 3);
            this.pnlCard1.Name = "pnlCard1";
            this.pnlCard1.Size = new System.Drawing.Size(143, 58);
            this.pnlCard1.TabIndex = 0;
            // 
            // lblValTransactions
            // 
            this.lblValTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValTransactions.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblValTransactions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(30)))), ((int)(((byte)(20)))));
            this.lblValTransactions.Location = new System.Drawing.Point(0, 20);
            this.lblValTransactions.Name = "lblValTransactions";
            this.lblValTransactions.Size = new System.Drawing.Size(141, 36);
            this.lblValTransactions.TabIndex = 1;
            this.lblValTransactions.Text = "0";
            this.lblValTransactions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleTransactions
            // 
            this.lblTitleTransactions.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleTransactions.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblTitleTransactions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(45)))));
            this.lblTitleTransactions.Location = new System.Drawing.Point(0, 0);
            this.lblTitleTransactions.Name = "lblTitleTransactions";
            this.lblTitleTransactions.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblTitleTransactions.Size = new System.Drawing.Size(141, 20);
            this.lblTitleTransactions.TabIndex = 0;
            this.lblTitleTransactions.Text = "TOTAL TRANSACTIONS";
            this.lblTitleTransactions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCard2
            // 
            this.pnlCard2.BackColor = System.Drawing.Color.White;
            this.pnlCard2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard2.Controls.Add(this.lblValSales);
            this.pnlCard2.Controls.Add(this.lblTitleSales);
            this.pnlCard2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCard2.Location = new System.Drawing.Point(152, 3);
            this.pnlCard2.Name = "pnlCard2";
            this.pnlCard2.Size = new System.Drawing.Size(143, 58);
            this.pnlCard2.TabIndex = 1;
            // 
            // lblValSales
            // 
            this.lblValSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValSales.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblValSales.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(60)))), ((int)(((byte)(20)))));
            this.lblValSales.Location = new System.Drawing.Point(0, 20);
            this.lblValSales.Name = "lblValSales";
            this.lblValSales.Size = new System.Drawing.Size(141, 36);
            this.lblValSales.TabIndex = 1;
            this.lblValSales.Text = "₱0.00";
            this.lblValSales.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleSales
            // 
            this.lblTitleSales.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleSales.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblTitleSales.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(45)))));
            this.lblTitleSales.Location = new System.Drawing.Point(0, 0);
            this.lblTitleSales.Name = "lblTitleSales";
            this.lblTitleSales.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblTitleSales.Size = new System.Drawing.Size(141, 20);
            this.lblTitleSales.TabIndex = 0;
            this.lblTitleSales.Text = "TOTAL SALES";
            this.lblTitleSales.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCard3
            // 
            this.pnlCard3.BackColor = System.Drawing.Color.White;
            this.pnlCard3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard3.Controls.Add(this.lblValItemsSold);
            this.pnlCard3.Controls.Add(this.lblTitleItemsSold);
            this.pnlCard3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCard3.Location = new System.Drawing.Point(301, 3);
            this.pnlCard3.Name = "pnlCard3";
            this.pnlCard3.Size = new System.Drawing.Size(143, 58);
            this.pnlCard3.TabIndex = 2;
            // 
            // lblValItemsSold
            // 
            this.lblValItemsSold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValItemsSold.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblValItemsSold.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(30)))), ((int)(((byte)(20)))));
            this.lblValItemsSold.Location = new System.Drawing.Point(0, 20);
            this.lblValItemsSold.Name = "lblValItemsSold";
            this.lblValItemsSold.Size = new System.Drawing.Size(141, 36);
            this.lblValItemsSold.TabIndex = 1;
            this.lblValItemsSold.Text = "0";
            this.lblValItemsSold.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleItemsSold
            // 
            this.lblTitleItemsSold.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleItemsSold.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblTitleItemsSold.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(45)))));
            this.lblTitleItemsSold.Location = new System.Drawing.Point(0, 0);
            this.lblTitleItemsSold.Name = "lblTitleItemsSold";
            this.lblTitleItemsSold.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblTitleItemsSold.Size = new System.Drawing.Size(141, 20);
            this.lblTitleItemsSold.TabIndex = 0;
            this.lblTitleItemsSold.Text = "TOTAL ITEMS SOLD";
            this.lblTitleItemsSold.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCard4
            // 
            this.pnlCard4.BackColor = System.Drawing.Color.White;
            this.pnlCard4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard4.Controls.Add(this.lblValAvgOrder);
            this.pnlCard4.Controls.Add(this.lblTitleAvgOrder);
            this.pnlCard4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCard4.Location = new System.Drawing.Point(450, 3);
            this.pnlCard4.Name = "pnlCard4";
            this.pnlCard4.Size = new System.Drawing.Size(143, 58);
            this.pnlCard4.TabIndex = 3;
            // 
            // lblValAvgOrder
            // 
            this.lblValAvgOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValAvgOrder.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblValAvgOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(30)))), ((int)(((byte)(20)))));
            this.lblValAvgOrder.Location = new System.Drawing.Point(0, 20);
            this.lblValAvgOrder.Name = "lblValAvgOrder";
            this.lblValAvgOrder.Size = new System.Drawing.Size(141, 36);
            this.lblValAvgOrder.TabIndex = 1;
            this.lblValAvgOrder.Text = "₱0.00";
            this.lblValAvgOrder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleAvgOrder
            // 
            this.lblTitleAvgOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleAvgOrder.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblTitleAvgOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(45)))));
            this.lblTitleAvgOrder.Location = new System.Drawing.Point(0, 0);
            this.lblTitleAvgOrder.Name = "lblTitleAvgOrder";
            this.lblTitleAvgOrder.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblTitleAvgOrder.Size = new System.Drawing.Size(141, 20);
            this.lblTitleAvgOrder.TabIndex = 0;
            this.lblTitleAvgOrder.Text = "AVERAGE ORDER";
            this.lblTitleAvgOrder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCard5
            // 
            this.pnlCard5.BackColor = System.Drawing.Color.White;
            this.pnlCard5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard5.Controls.Add(this.lblValCash);
            this.pnlCard5.Controls.Add(this.lblTitleCash);
            this.pnlCard5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCard5.Location = new System.Drawing.Point(599, 3);
            this.pnlCard5.Name = "pnlCard5";
            this.pnlCard5.Size = new System.Drawing.Size(143, 58);
            this.pnlCard5.TabIndex = 4;
            // 
            // lblValCash
            // 
            this.lblValCash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValCash.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblValCash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(30)))), ((int)(((byte)(20)))));
            this.lblValCash.Location = new System.Drawing.Point(0, 20);
            this.lblValCash.Name = "lblValCash";
            this.lblValCash.Size = new System.Drawing.Size(141, 36);
            this.lblValCash.TabIndex = 1;
            this.lblValCash.Text = "₱0.00";
            this.lblValCash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleCash
            // 
            this.lblTitleCash.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleCash.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblTitleCash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(45)))));
            this.lblTitleCash.Location = new System.Drawing.Point(0, 0);
            this.lblTitleCash.Name = "lblTitleCash";
            this.lblTitleCash.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblTitleCash.Size = new System.Drawing.Size(141, 20);
            this.lblTitleCash.TabIndex = 0;
            this.lblTitleCash.Text = "CASH RECEIVED";
            this.lblTitleCash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCard6
            // 
            this.pnlCard6.BackColor = System.Drawing.Color.White;
            this.pnlCard6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard6.Controls.Add(this.lblValChange);
            this.pnlCard6.Controls.Add(this.lblTitleChange);
            this.pnlCard6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCard6.Location = new System.Drawing.Point(748, 3);
            this.pnlCard6.Name = "pnlCard6";
            this.pnlCard6.Size = new System.Drawing.Size(145, 58);
            this.pnlCard6.TabIndex = 5;
            // 
            // lblValChange
            // 
            this.lblValChange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblValChange.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblValChange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(126)))), ((int)(((byte)(52)))));
            this.lblValChange.Location = new System.Drawing.Point(0, 20);
            this.lblValChange.Name = "lblValChange";
            this.lblValChange.Size = new System.Drawing.Size(143, 36);
            this.lblValChange.TabIndex = 1;
            this.lblValChange.Text = "₱0.00";
            this.lblValChange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleChange
            // 
            this.lblTitleChange.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleChange.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblTitleChange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(45)))));
            this.lblTitleChange.Location = new System.Drawing.Point(0, 0);
            this.lblTitleChange.Name = "lblTitleChange";
            this.lblTitleChange.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblTitleChange.Size = new System.Drawing.Size(143, 20);
            this.lblTitleChange.TabIndex = 0;
            this.lblTitleChange.Text = "CHANGE GIVEN";
            this.lblTitleChange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(235)))), ((int)(((byte)(230)))));
            this.pnlFooter.Controls.Add(this.btnPrint);
            this.pnlFooter.Controls.Add(this.btnClose);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 626);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.pnlFooter.Size = new System.Drawing.Size(920, 54);
            this.pnlFooter.TabIndex = 3;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(61)))), ((int)(((byte)(34)))));
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(620, 9);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(160, 36);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "🖨️ PRINT REPORT";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(110)))), ((int)(((byte)(105)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(792, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(112, 36);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tblGrids
            // 
            this.tblGrids.ColumnCount = 1;
            this.tblGrids.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblGrids.Controls.Add(this.pnlTransSection, 0, 0);
            this.tblGrids.Controls.Add(this.pnlProdSection, 0, 1);
            this.tblGrids.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblGrids.Location = new System.Drawing.Point(0, 176);
            this.tblGrids.Name = "tblGrids";
            this.tblGrids.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.tblGrids.RowCount = 2;
            this.tblGrids.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52F));
            this.tblGrids.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48F));
            this.tblGrids.Size = new System.Drawing.Size(920, 450);
            this.tblGrids.TabIndex = 2;
            // 
            // pnlTransSection
            // 
            this.pnlTransSection.Controls.Add(this.dgvTransactions);
            this.pnlTransSection.Controls.Add(this.lblTransHeader);
            this.pnlTransSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTransSection.Location = new System.Drawing.Point(15, 9);
            this.pnlTransSection.Name = "pnlTransSection";
            this.pnlTransSection.Size = new System.Drawing.Size(890, 221);
            this.pnlTransSection.TabIndex = 0;
            // 
            // dgvTransactions
            // 
            this.dgvTransactions.AllowUserToAddRows = false;
            this.dgvTransactions.AllowUserToDeleteRows = false;
            this.dgvTransactions.AllowUserToResizeRows = false;
            this.dgvTransactions.BackgroundColor = System.Drawing.Color.White;
            this.dgvTransactions.ColumnHeadersHeight = 32;
            this.dgvTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTransactions.Location = new System.Drawing.Point(0, 24);
            this.dgvTransactions.MultiSelect = false;
            this.dgvTransactions.Name = "dgvTransactions";
            this.dgvTransactions.ReadOnly = true;
            this.dgvTransactions.RowHeadersVisible = false;
            this.dgvTransactions.RowTemplate.Height = 30;
            this.dgvTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTransactions.Size = new System.Drawing.Size(890, 197);
            this.dgvTransactions.TabIndex = 1;
            // 
            // lblTransHeader
            // 
            this.lblTransHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTransHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTransHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(30)))));
            this.lblTransHeader.Location = new System.Drawing.Point(0, 0);
            this.lblTransHeader.Name = "lblTransHeader";
            this.lblTransHeader.Size = new System.Drawing.Size(890, 24);
            this.lblTransHeader.TabIndex = 0;
            this.lblTransHeader.Text = "TRANSACTION DETAILS";
            this.lblTransHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlProdSection
            // 
            this.pnlProdSection.Controls.Add(this.dgvProducts);
            this.pnlProdSection.Controls.Add(this.lblProdHeader);
            this.pnlProdSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProdSection.Location = new System.Drawing.Point(15, 236);
            this.pnlProdSection.Name = "pnlProdSection";
            this.pnlProdSection.Size = new System.Drawing.Size(890, 205);
            this.pnlProdSection.TabIndex = 1;
            // 
            // dgvProducts
            // 
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AllowUserToDeleteRows = false;
            this.dgvProducts.AllowUserToResizeRows = false;
            this.dgvProducts.BackgroundColor = System.Drawing.Color.White;
            this.dgvProducts.ColumnHeadersHeight = 32;
            this.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProducts.Location = new System.Drawing.Point(0, 24);
            this.dgvProducts.MultiSelect = false;
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.RowHeadersVisible = false;
            this.dgvProducts.RowTemplate.Height = 30;
            this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size = new System.Drawing.Size(890, 181);
            this.dgvProducts.TabIndex = 1;
            // 
            // lblProdHeader
            // 
            this.lblProdHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProdHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblProdHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(30)))));
            this.lblProdHeader.Location = new System.Drawing.Point(0, 0);
            this.lblProdHeader.Name = "lblProdHeader";
            this.lblProdHeader.Size = new System.Drawing.Size(890, 24);
            this.lblProdHeader.TabIndex = 0;
            this.lblProdHeader.Text = "PRODUCT SALES SUMMARY";
            this.lblProdHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmEndOfShiftReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(920, 680);
            this.Controls.Add(this.tblGrids);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlSummary);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(880, 640);
            this.Name = "frmEndOfShiftReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "End-of-Shift Sales Report - Bakery POS";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlSummary.ResumeLayout(false);
            this.tblSummary.ResumeLayout(false);
            this.pnlCard1.ResumeLayout(false);
            this.pnlCard2.ResumeLayout(false);
            this.pnlCard3.ResumeLayout(false);
            this.pnlCard4.ResumeLayout(false);
            this.pnlCard5.ResumeLayout(false);
            this.pnlCard6.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
            this.tblGrids.ResumeLayout(false);
            this.pnlTransSection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).EndInit();
            this.pnlProdSection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblStoreTitle;
        private System.Windows.Forms.Label lblReportSubtitle;
        private System.Windows.Forms.Label lblCashierInfo;
        private System.Windows.Forms.Label lblReportDate;
        private System.Windows.Forms.Label lblGeneratedTime;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.TableLayoutPanel tblSummary;
        private System.Windows.Forms.Panel pnlCard1;
        private System.Windows.Forms.Label lblTitleTransactions;
        private System.Windows.Forms.Label lblValTransactions;
        private System.Windows.Forms.Panel pnlCard2;
        private System.Windows.Forms.Label lblTitleSales;
        private System.Windows.Forms.Label lblValSales;
        private System.Windows.Forms.Panel pnlCard3;
        private System.Windows.Forms.Label lblTitleItemsSold;
        private System.Windows.Forms.Label lblValItemsSold;
        private System.Windows.Forms.Panel pnlCard4;
        private System.Windows.Forms.Label lblTitleAvgOrder;
        private System.Windows.Forms.Label lblValAvgOrder;
        private System.Windows.Forms.Panel pnlCard5;
        private System.Windows.Forms.Label lblTitleCash;
        private System.Windows.Forms.Label lblValCash;
        private System.Windows.Forms.Panel pnlCard6;
        private System.Windows.Forms.Label lblTitleChange;
        private System.Windows.Forms.Label lblValChange;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TableLayoutPanel tblGrids;
        private System.Windows.Forms.Panel pnlTransSection;
        private System.Windows.Forms.Label lblTransHeader;
        private System.Windows.Forms.DataGridView dgvTransactions;
        private System.Windows.Forms.Panel pnlProdSection;
        private System.Windows.Forms.Label lblProdHeader;
        private System.Windows.Forms.DataGridView dgvProducts;
    }
}

