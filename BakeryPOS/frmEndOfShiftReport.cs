using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace BakeryPOS
{
    public partial class frmEndOfShiftReport : Form
    {
        private string _cashierName;
        private string _cashierUsername;
        private DateTime _reportDate;
        private DateTime _generatedTime;

        // KPI Summary Variables
        private int _totalTransactions = 0;
        private decimal _totalSales = 0m;
        private int _totalItemsSold = 0;
        private decimal _averageOrder = 0m;
        private decimal _totalCashReceived = 0m;
        private decimal _totalChangeGiven = 0m;

        private DataTable _dtTransactions = new DataTable();
        private DataTable _dtProducts = new DataTable();

        public frmEndOfShiftReport()
            : this("Cashier", "cashier1")
        {
        }

        public frmEndOfShiftReport(string cashierName, string cashierUsername)
        {
            InitializeComponent();
            _cashierName = string.IsNullOrWhiteSpace(cashierName) ? "Cashier" : cashierName;
            _cashierUsername = string.IsNullOrWhiteSpace(cashierUsername) ? "cashier1" : cashierUsername;
            _reportDate = DateTime.Today;
            _generatedTime = DateTime.Now;

            SetupGridControls();
            LoadReportData();
        }

        private void SetupGridControls()
        {
            this.BackColor = BakeryPOS.UI.Styles.WarmCream;
            pnlSummary.BackColor = Color.FromArgb(245, 236, 226);
            pnlFooter.BackColor = BakeryPOS.UI.Styles.WarmCream;

            PictureBox picLogo = new PictureBox
            {
                Size = new Size(130, 74),
                Location = new Point(16, 5),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent,
                TabStop = false,
                Image = BakeryPOS.UI.Styles.GetLogoImage()
            };
            pnlHeader.Controls.Add(picLogo);

            Font kpiTitleFont = new Font("Segoe UI", 8.75F, FontStyle.Bold);
            Font kpiValFont = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            Color sectionColor = Color.FromArgb(123, 63, 26);

            foreach (Label lbl in new[] { lblTitleTransactions, lblTitleSales, lblTitleItemsSold, lblTitleAvgOrder, lblTitleCash, lblTitleChange })
            {
                lbl.Font = kpiTitleFont;
                lbl.ForeColor = sectionColor;
            }
            foreach (Label lbl in new[] { lblValTransactions, lblValSales, lblValItemsSold, lblValAvgOrder, lblValCash, lblValChange })
            {
                lbl.Font = kpiValFont;
            }
            foreach (Label lbl in new[] { lblTransHeader, lblProdHeader })
            {
                lbl.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                lbl.ForeColor = sectionColor;
            }

            // Setup Transaction Details Grid
            if (dgvTransactions != null)
            {
                dgvTransactions.AutoGenerateColumns = false;
                dgvTransactions.Columns.Clear();
                dgvTransactions.RowHeadersVisible = false;
                dgvTransactions.AllowUserToAddRows = false;
                dgvTransactions.AllowUserToDeleteRows = false;
                dgvTransactions.AllowUserToResizeRows = false;
                dgvTransactions.ReadOnly = true;
                dgvTransactions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvTransactions.MultiSelect = false;

                DataGridViewTextBoxColumn colSaleID = new DataGridViewTextBoxColumn
                {
                    Name = "Sale ID",
                    DataPropertyName = "Sale ID",
                    HeaderText = "Sale ID",
                    Width = 120
                };
                colSaleID.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvTransactions.Columns.Add(colSaleID);

                DataGridViewTextBoxColumn colDateTime = new DataGridViewTextBoxColumn
                {
                    Name = "Date & Time",
                    DataPropertyName = "Date & Time",
                    HeaderText = "Date & Time",
                    Width = 220
                };
                dgvTransactions.Columns.Add(colDateTime);

                DataGridViewTextBoxColumn colTotal = new DataGridViewTextBoxColumn
                {
                    Name = "Total Amount",
                    DataPropertyName = "Total Amount",
                    HeaderText = "Total Amount",
                    Width = 150
                };
                colTotal.DefaultCellStyle.Format = "₱#,##0.00";
                colTotal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvTransactions.Columns.Add(colTotal);

                DataGridViewTextBoxColumn colCash = new DataGridViewTextBoxColumn
                {
                    Name = "Cash Paid",
                    DataPropertyName = "Cash Paid",
                    HeaderText = "Cash Paid",
                    Width = 150
                };
                colCash.DefaultCellStyle.Format = "₱#,##0.00";
                colCash.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvTransactions.Columns.Add(colCash);

                DataGridViewTextBoxColumn colChange = new DataGridViewTextBoxColumn
                {
                    Name = "Change",
                    DataPropertyName = "Change",
                    HeaderText = "Change",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                };
                colChange.DefaultCellStyle.Format = "₱#,##0.00";
                colChange.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvTransactions.Columns.Add(colChange);

                BakeryPOS.UI.Styles.ApplyGridStyle(dgvTransactions);
            }

            // Setup Product Sales Summary Grid
            if (dgvProducts != null)
            {
                dgvProducts.AutoGenerateColumns = false;
                dgvProducts.Columns.Clear();
                dgvProducts.RowHeadersVisible = false;
                dgvProducts.AllowUserToAddRows = false;
                dgvProducts.AllowUserToDeleteRows = false;
                dgvProducts.AllowUserToResizeRows = false;
                dgvProducts.ReadOnly = true;
                dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvProducts.MultiSelect = false;

                DataGridViewTextBoxColumn colProdName = new DataGridViewTextBoxColumn
                {
                    Name = "Product Name",
                    DataPropertyName = "Product Name",
                    HeaderText = "Product Name",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                };
                dgvProducts.Columns.Add(colProdName);

                DataGridViewTextBoxColumn colQtySold = new DataGridViewTextBoxColumn
                {
                    Name = "Total Quantity Sold",
                    DataPropertyName = "Total Quantity Sold",
                    HeaderText = "Total Quantity Sold",
                    Width = 180
                };
                colQtySold.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colQtySold.DefaultCellStyle.Format = "#,##0";
                dgvProducts.Columns.Add(colQtySold);

                DataGridViewTextBoxColumn colSalesAmt = new DataGridViewTextBoxColumn
                {
                    Name = "Total Sales Amount",
                    DataPropertyName = "Total Sales Amount",
                    HeaderText = "Total Sales Amount",
                    Width = 200
                };
                colSalesAmt.DefaultCellStyle.Format = "₱#,##0.00";
                colSalesAmt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvProducts.Columns.Add(colSalesAmt);

                BakeryPOS.UI.Styles.ApplyGridStyle(dgvProducts);
            }
        }

        private void LoadReportData()
        {
            try
            {
                // Update header info labels
                lblCashierInfo.Text = "Cashier: " + _cashierUsername;
                lblReportDate.Text = "Report Date: " + _reportDate.ToString("MMMM dd, yyyy");
                lblGeneratedTime.Text = "Generated: " + _generatedTime.ToString("MMM dd, yyyy hh:mm:ss tt");

                string connectionString = DbHelper.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // 1. Query Summary KPIs for current cashier and current date
                    string summaryQuery = @"
                        SELECT 
                            COUNT(*) AS TotalTransactions,
                            ISNULL(SUM(TotalAmount), 0) AS TotalSales,
                            ISNULL(SUM(CashAmount), 0) AS TotalCashReceived,
                            ISNULL(SUM(ChangeAmount), 0) AS TotalChangeGiven
                        FROM Sales
                        WHERE CashierUsername = @CashierUsername
                          AND CAST(SaleDate AS DATE) = CAST(GETDATE() AS DATE);";

                    using (SqlCommand cmd = new SqlCommand(summaryQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CashierUsername", _cashierUsername);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                _totalTransactions = Convert.ToInt32(reader["TotalTransactions"]);
                                _totalSales = Convert.ToDecimal(reader["TotalSales"]);
                                _totalCashReceived = Convert.ToDecimal(reader["TotalCashReceived"]);
                                _totalChangeGiven = Convert.ToDecimal(reader["TotalChangeGiven"]);
                            }
                        }
                    }

                    // Query Total Items Sold
                    string itemsSoldQuery = @"
                        SELECT ISNULL(SUM(si.Quantity), 0) AS TotalItemsSold
                        FROM SaleItems si
                        INNER JOIN Sales s ON si.SaleID = s.SaleID
                        WHERE s.CashierUsername = @CashierUsername
                          AND CAST(s.SaleDate AS DATE) = CAST(GETDATE() AS DATE);";

                    using (SqlCommand cmd = new SqlCommand(itemsSoldQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CashierUsername", _cashierUsername);
                        object res = cmd.ExecuteScalar();
                        _totalItemsSold = res != null && res != DBNull.Value ? Convert.ToInt32(res) : 0;
                    }

                    _averageOrder = _totalTransactions > 0 ? (_totalSales / _totalTransactions) : 0m;

                    // Display KPIs (fresh defaults 0 / ₱0.00 if no sales)
                    lblValTransactions.Text = _totalTransactions.ToString();
                    lblValSales.Text = "₱" + _totalSales.ToString("N2");
                    lblValItemsSold.Text = _totalItemsSold.ToString();
                    lblValAvgOrder.Text = "₱" + _averageOrder.ToString("N2");
                    lblValCash.Text = "₱" + _totalCashReceived.ToString("N2");
                    lblValChange.Text = "₱" + _totalChangeGiven.ToString("N2");

                    // 2. Query Transaction Details
                    string transQuery = @"
                        SELECT 
                            'ORD-' + RIGHT('0000' + CAST(SaleID AS VARCHAR(10)), 4) AS [Sale ID],
                            CONVERT(VARCHAR(20), SaleDate, 100) AS [Date & Time],
                            TotalAmount AS [Total Amount],
                            CashAmount AS [Cash Paid],
                            ChangeAmount AS [Change]
                        FROM Sales
                        WHERE CashierUsername = @CashierUsername
                          AND CAST(SaleDate AS DATE) = CAST(GETDATE() AS DATE)
                        ORDER BY SaleID ASC;";

                    using (SqlCommand cmd = new SqlCommand(transQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CashierUsername", _cashierUsername);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            _dtTransactions = new DataTable();
                            adapter.Fill(_dtTransactions);
                            if (dgvTransactions != null)
                            {
                                dgvTransactions.DataSource = _dtTransactions;
                            }
                        }
                    }

                    // 3. Query Product Sales Summary
                    string prodQuery = @"
                        SELECT 
                            ISNULL(p.ProductName, 'Unknown Product') AS [Product Name],
                            SUM(si.Quantity) AS [Total Quantity Sold],
                            SUM(si.LineTotal) AS [Total Sales Amount]
                        FROM SaleItems si
                        INNER JOIN Sales s ON si.SaleID = s.SaleID
                        LEFT JOIN Products p ON si.ProductID = p.ProductID
                        WHERE s.CashierUsername = @CashierUsername
                          AND CAST(s.SaleDate AS DATE) = CAST(GETDATE() AS DATE)
                        GROUP BY p.ProductName
                        ORDER BY [Total Quantity Sold] DESC, [Total Sales Amount] DESC;";

                    using (SqlCommand cmd = new SqlCommand(prodQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CashierUsername", _cashierUsername);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            _dtProducts = new DataTable();
                            adapter.Fill(_dtProducts);
                            if (dgvProducts != null)
                            {
                                dgvProducts.DataSource = _dtProducts;
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show(
                    "We couldn't load the report data. Please try again.",
                    "Something Went Wrong",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDocument printDoc = new PrintDocument();
                printDoc.DocumentName = "EndOfShiftReport_" + _reportDate.ToString("yyyyMMdd") + "_" + _cashierUsername;
                printDoc.PrintPage += PrintDocument_PrintPage;

                PrintPreviewDialog preview = new PrintPreviewDialog
                {
                    Document = printDoc,
                    Width = 900,
                    Height = 700,
                    StartPosition = FormStartPosition.CenterParent
                };

                preview.ShowDialog(this);
            }
            catch
            {
                MessageBox.Show(
                    "We couldn't generate the print preview. Please try again.",
                    "Something Went Wrong",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            float printableWidth = e.MarginBounds.Width;
            float currentY = topMargin;

            // Fonts
            using (Font titleFont = new Font("Segoe UI", 16F, FontStyle.Bold))
            using (Font subTitleFont = new Font("Segoe UI", 12F, FontStyle.Bold))
            using (Font sectionFont = new Font("Segoe UI", 10.5F, FontStyle.Bold))
            using (Font boldFont = new Font("Segoe UI", 9F, FontStyle.Bold))
            using (Font regularFont = new Font("Segoe UI", 9F, FontStyle.Regular))
            using (Font smallFont = new Font("Segoe UI", 8.5F, FontStyle.Regular))
            using (Brush darkBrush = new SolidBrush(Color.FromArgb(50, 30, 15)))
            using (Brush headerBrush = new SolidBrush(Color.FromArgb(110, 70, 45)))
            using (Pen linePen = new Pen(Color.FromArgb(180, 160, 140), 1))
            using (Pen thickPen = new Pen(Color.FromArgb(110, 70, 45), 2))
            {
                StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center };
                StringFormat leftFormat = new StringFormat { Alignment = StringAlignment.Near };
                StringFormat rightFormat = new StringFormat { Alignment = StringAlignment.Far };

                // 1. HEADER
                g.DrawString("Bread Basket Bakeshop", titleFont, headerBrush, leftMargin + (printableWidth / 2f), currentY, centerFormat);
                currentY += 28;

                g.DrawString("End-of-Shift Sales Report", subTitleFont, darkBrush, leftMargin + (printableWidth / 2f), currentY, centerFormat);
                currentY += 24;

                g.DrawLine(thickPen, leftMargin, currentY, leftMargin + printableWidth, currentY);
                currentY += 8;

                // Cashier, Report Date, Generated Date/Time
                g.DrawString("Cashier: " + _cashierUsername, boldFont, darkBrush, leftMargin, currentY);
                g.DrawString("Report Date: " + _reportDate.ToString("MMMM dd, yyyy"), regularFont, darkBrush, leftMargin + printableWidth, currentY, rightFormat);
                currentY += 18;

                g.DrawString("Generated: " + _generatedTime.ToString("MMM dd, yyyy hh:mm:ss tt"), smallFont, darkBrush, leftMargin, currentY);
                currentY += 22;

                g.DrawLine(linePen, leftMargin, currentY, leftMargin + printableWidth, currentY);
                currentY += 12;

                // 2. SUMMARY SECTION
                g.DrawString("Summary", sectionFont, headerBrush, leftMargin, currentY);
                currentY += 20;

                float col1 = leftMargin;
                float col2 = leftMargin + (printableWidth / 2f);

                g.DrawString("Total Transactions:", boldFont, darkBrush, col1, currentY);
                g.DrawString(_totalTransactions.ToString(), regularFont, darkBrush, col1 + 160, currentY);
                g.DrawString("Average Order:", boldFont, darkBrush, col2, currentY);
                g.DrawString("₱" + _averageOrder.ToString("N2"), regularFont, darkBrush, col2 + 160, currentY);
                currentY += 18;

                g.DrawString("Total Sales:", boldFont, darkBrush, col1, currentY);
                g.DrawString("₱" + _totalSales.ToString("N2"), boldFont, darkBrush, col1 + 160, currentY);
                g.DrawString("Total Cash Received:", boldFont, darkBrush, col2, currentY);
                g.DrawString("₱" + _totalCashReceived.ToString("N2"), regularFont, darkBrush, col2 + 160, currentY);
                currentY += 18;

                g.DrawString("Total Items Sold:", boldFont, darkBrush, col1, currentY);
                g.DrawString(_totalItemsSold.ToString(), regularFont, darkBrush, col1 + 160, currentY);
                g.DrawString("Total Change Given:", boldFont, darkBrush, col2, currentY);
                g.DrawString("₱" + _totalChangeGiven.ToString("N2"), regularFont, darkBrush, col2 + 160, currentY);
                currentY += 24;

                g.DrawLine(linePen, leftMargin, currentY, leftMargin + printableWidth, currentY);
                currentY += 12;

                // 3. TRANSACTION DETAILS
                g.DrawString("Transaction Details", sectionFont, headerBrush, leftMargin, currentY);
                currentY += 20;

                float tCol1 = leftMargin;             // Sale ID (90)
                float tCol2 = leftMargin + 90;        // Date & Time (180)
                float tCol3 = leftMargin + 270;       // Total Amount (110)
                float tCol4 = leftMargin + 380;       // Cash Paid (110)
                float tCol5 = leftMargin + 490;       // Change (110)

                // Header row
                g.FillRectangle(new SolidBrush(Color.FromArgb(245, 238, 230)), leftMargin, currentY, printableWidth, 20);
                g.DrawString("Sale ID", boldFont, darkBrush, tCol1 + 4, currentY + 2);
                g.DrawString("Date & Time", boldFont, darkBrush, tCol2, currentY + 2);
                g.DrawString("Total Amount", boldFont, darkBrush, tCol3 + 100, currentY + 2, rightFormat);
                g.DrawString("Cash Paid", boldFont, darkBrush, tCol4 + 100, currentY + 2, rightFormat);
                g.DrawString("Change", boldFont, darkBrush, tCol5 + 100, currentY + 2, rightFormat);
                currentY += 22;

                if (_dtTransactions.Rows.Count == 0)
                {
                    g.DrawString("No transactions found for this shift.", regularFont, darkBrush, leftMargin + 10, currentY);
                    currentY += 20;
                }
                else
                {
                    int transCount = 0;
                    foreach (DataRow row in _dtTransactions.Rows)
                    {
                        if (transCount >= 25 && currentY > e.MarginBounds.Bottom - 200)
                        {
                            g.DrawString("... (" + (_dtTransactions.Rows.Count - transCount) + " additional transactions recorded in system)", smallFont, darkBrush, leftMargin + 10, currentY);
                            currentY += 18;
                            break;
                        }

                        string saleID = row["Sale ID"] != DBNull.Value ? row["Sale ID"].ToString() : "-";
                        string dateTime = row["Date & Time"] != DBNull.Value ? row["Date & Time"].ToString() : "-";
                        decimal total = row["Total Amount"] != DBNull.Value ? Convert.ToDecimal(row["Total Amount"]) : 0m;
                        decimal cash = row["Cash Paid"] != DBNull.Value ? Convert.ToDecimal(row["Cash Paid"]) : 0m;
                        decimal change = row["Change"] != DBNull.Value ? Convert.ToDecimal(row["Change"]) : 0m;

                        g.DrawString(saleID, regularFont, darkBrush, tCol1 + 4, currentY);
                        g.DrawString(dateTime, regularFont, darkBrush, tCol2, currentY);
                        g.DrawString("₱" + total.ToString("N2"), regularFont, darkBrush, tCol3 + 100, currentY, rightFormat);
                        g.DrawString("₱" + cash.ToString("N2"), regularFont, darkBrush, tCol4 + 100, currentY, rightFormat);
                        g.DrawString("₱" + change.ToString("N2"), regularFont, darkBrush, tCol5 + 100, currentY, rightFormat);

                        currentY += 18;
                        transCount++;
                    }
                }

                currentY += 8;
                g.DrawLine(linePen, leftMargin, currentY, leftMargin + printableWidth, currentY);
                currentY += 12;

                // 4. PRODUCT SALES SUMMARY
                g.DrawString("Product Sales Summary", sectionFont, headerBrush, leftMargin, currentY);
                currentY += 20;

                float pCol1 = leftMargin;             // Product Name
                float pCol2 = leftMargin + printableWidth - 260; // Total Quantity Sold
                float pCol3 = leftMargin + printableWidth;       // Total Sales Amount

                // Header row
                g.FillRectangle(new SolidBrush(Color.FromArgb(245, 238, 230)), leftMargin, currentY, printableWidth, 20);
                g.DrawString("Product Name", boldFont, darkBrush, pCol1 + 4, currentY + 2);
                g.DrawString("Total Quantity Sold", boldFont, darkBrush, pCol2 + 110, currentY + 2, rightFormat);
                g.DrawString("Total Sales Amount", boldFont, darkBrush, pCol3, currentY + 2, rightFormat);
                currentY += 22;

                if (_dtProducts.Rows.Count == 0)
                {
                    g.DrawString("No product sales recorded for this shift.", regularFont, darkBrush, leftMargin + 10, currentY);
                    currentY += 20;
                }
                else
                {
                    foreach (DataRow row in _dtProducts.Rows)
                    {
                        string prodName = row["Product Name"] != DBNull.Value ? row["Product Name"].ToString() : "Unknown";
                        int qty = row["Total Quantity Sold"] != DBNull.Value ? Convert.ToInt32(row["Total Quantity Sold"]) : 0;
                        decimal salesAmt = row["Total Sales Amount"] != DBNull.Value ? Convert.ToDecimal(row["Total Sales Amount"]) : 0m;

                        g.DrawString(prodName, regularFont, darkBrush, pCol1 + 4, currentY);
                        g.DrawString(qty.ToString("#,##0"), regularFont, darkBrush, pCol2 + 110, currentY, rightFormat);
                        g.DrawString("₱" + salesAmt.ToString("N2"), regularFont, darkBrush, pCol3, currentY, rightFormat);
                        currentY += 18;
                    }
                }

                currentY += 8;
                g.DrawLine(thickPen, leftMargin, currentY, leftMargin + printableWidth, currentY);
            }

            e.HasMorePages = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

