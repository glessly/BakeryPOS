using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace BakeryPOS
{
    public partial class frmReports : Form
    {
        private string _username = "admin";
        private decimal _totalSales = 0m;
        private int _totalTransactions = 0;
        private int _totalItemsSold = 0;
        private decimal _averageOrder = 0m;
        private DataTable _dtBestSelling = new DataTable();

        // Inventory metrics
        private int _totalActiveProducts = 0;
        private int _totalUnitsInStock = 0;
        private int _lowStockCount = 0;
        private DataTable _dtInventory = new DataTable();

        // AI Smart Reports metrics
        private DataTable _dtRestockSuggestions = new DataTable();

        // Automatic refresh and validation control
        private bool _isUpdatingPeriod = false;
        private bool _isValidatingDate = false;

        public frmReports() : this("admin")
        {
        }

        public frmReports(string username)
        {
            InitializeComponent();
            _username = !string.IsNullOrWhiteSpace(username) ? username : "admin";

            ApplyStandardReportStyling();
            ConfigureSalesGrid();
            ConfigureInventoryGrid();
            ConfigureRestockGrid();

            pnlAiContent.Resize += pnlAiContent_Resize;
            AdjustAiRestockContainerHeight();

            // Set initial period to Daily
            cmbPeriod.SelectedIndex = 0;
        }

        private void ApplyStandardReportStyling()
        {
            this.BackColor = BakeryPOS.UI.Styles.WarmCream;
            tabSales.BackColor = BakeryPOS.UI.Styles.WarmCream;
            pnlContent.BackColor = BakeryPOS.UI.Styles.WarmCream;
            pnlSummary.BackColor = BakeryPOS.UI.Styles.WarmCream;
            tabInventory.BackColor = BakeryPOS.UI.Styles.WarmCream;
            pnlInventoryContent.BackColor = BakeryPOS.UI.Styles.WarmCream;
            pnlInventorySummary.BackColor = BakeryPOS.UI.Styles.WarmCream;
            tabAiReports.BackColor = BakeryPOS.UI.Styles.WarmCream;
            pnlAiContent.BackColor = BakeryPOS.UI.Styles.WarmCream;

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

            Font kpiTitleFont = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            Font kpiValFont = new Font("Segoe UI", 15F, FontStyle.Bold);
            Color sectionColor = Color.FromArgb(123, 63, 26);

            foreach (Label lbl in new[] { lblTitleSales, lblTitleTrans, lblTitleItems, lblTitleAvg, lblTitleActiveProd, lblTitleStockUnits, lblTitleLowStock })
            {
                lbl.Font = kpiTitleFont;
                lbl.ForeColor = sectionColor;
            }
            foreach (Label lbl in new[] { lblValTotalSales, lblValTransactions, lblValItemsSold, lblValAvgOrder, lblValActiveProducts, lblValTotalStockUnits, lblValLowStockCount })
            {
                lbl.Font = kpiValFont;
            }
            foreach (Label lbl in new[] { lblGridTitle, lblInventoryGridTitle, lblAiRestockTitle, lblAiSummaryTitle, lblTitleAiTrends })
            {
                lbl.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                lbl.ForeColor = sectionColor;
            }
            lblValAiTrends.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            lblAiSummaryText.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
        }

        private void ConfigureSalesGrid()
        {
            dgvBestSelling.AutoGenerateColumns = false;
            dgvBestSelling.Columns.Clear();

            DataGridViewTextBoxColumn colRank = new DataGridViewTextBoxColumn
            {
                Name = "Rank",
                DataPropertyName = "Rank",
                HeaderText = "#",
                Width = 50
            };
            colRank.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colRank.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBestSelling.Columns.Add(colRank);

            DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn
            {
                Name = "Product Name",
                DataPropertyName = "Product Name",
                HeaderText = "Product Name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 200
            };
            colName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvBestSelling.Columns.Add(colName);

            DataGridViewTextBoxColumn colQty = new DataGridViewTextBoxColumn
            {
                Name = "Total Quantity Sold",
                DataPropertyName = "Total Quantity Sold",
                HeaderText = "Total Quantity Sold",
                Width = 170
            };
            colQty.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colQty.DefaultCellStyle.Format = "N0";
            colQty.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBestSelling.Columns.Add(colQty);

            DataGridViewTextBoxColumn colAmount = new DataGridViewTextBoxColumn
            {
                Name = "Total Sales Amount",
                DataPropertyName = "Total Sales Amount",
                HeaderText = "Total Sales Amount",
                Width = 190
            };
            colAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colAmount.DefaultCellStyle.Format = "₱#,##0.00";
            colAmount.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvBestSelling.Columns.Add(colAmount);

            BakeryPOS.UI.Styles.ApplyGridStyle(dgvBestSelling);
            dgvBestSelling.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 248, 240);
        }

        private void ConfigureInventoryGrid()
        {
            dgvInventory.AutoGenerateColumns = false;
            dgvInventory.Columns.Clear();

            DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn
            {
                Name = "Product Name",
                DataPropertyName = "Product Name",
                HeaderText = "Product Name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 200
            };
            colName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvInventory.Columns.Add(colName);

            DataGridViewTextBoxColumn colStock = new DataGridViewTextBoxColumn
            {
                Name = "Current Stock",
                DataPropertyName = "Current Stock",
                HeaderText = "Current Stock",
                Width = 150
            };
            colStock.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colStock.DefaultCellStyle.Format = "N0";
            colStock.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvInventory.Columns.Add(colStock);

            DataGridViewTextBoxColumn colQtySold = new DataGridViewTextBoxColumn
            {
                Name = "Quantity Sold",
                DataPropertyName = "Quantity Sold",
                HeaderText = "Quantity Sold (Selected Period)",
                Width = 240
            };
            colQtySold.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colQtySold.DefaultCellStyle.Format = "N0";
            colQtySold.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvInventory.Columns.Add(colQtySold);

            DataGridViewTextBoxColumn colStatus = new DataGridViewTextBoxColumn
            {
                Name = "Stock Status",
                DataPropertyName = "Stock Status",
                HeaderText = "Stock Status",
                Width = 150
            };
            colStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colStatus.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvInventory.Columns.Add(colStatus);

            BakeryPOS.UI.Styles.ApplyGridStyle(dgvInventory);
            dgvInventory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 248, 240);

            dgvInventory.CellFormatting += DgvInventory_CellFormatting;
        }

        private void DgvInventory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvInventory.Columns[e.ColumnIndex].Name == "Stock Status" && e.Value != null)
            {
                string status = e.Value.ToString();
                if (status.Equals("Low Stock", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.Crimson;
                    e.CellStyle.Font = new Font(dgvInventory.Font, FontStyle.Bold);
                }
                else
                {
                    e.CellStyle.ForeColor = Color.DarkGreen;
                    e.CellStyle.Font = new Font(dgvInventory.Font, FontStyle.Bold);
                }
            }
        }

        private void ConfigureRestockGrid()
        {
            dgvRestockSuggestions.AutoGenerateColumns = false;
            dgvRestockSuggestions.Columns.Clear();

            DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn
            {
                Name = "Product Name",
                DataPropertyName = "Product Name",
                HeaderText = "Product Name",
                Width = 200,
                MinimumWidth = 180,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            colName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvRestockSuggestions.Columns.Add(colName);

            DataGridViewTextBoxColumn colStock = new DataGridViewTextBoxColumn
            {
                Name = "Current Stock",
                DataPropertyName = "Current Stock",
                HeaderText = "Current Stock",
                Width = 105,
                MinimumWidth = 95
            };
            colStock.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colStock.DefaultCellStyle.Format = "N0";
            colStock.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRestockSuggestions.Columns.Add(colStock);

            DataGridViewTextBoxColumn colQtySold = new DataGridViewTextBoxColumn
            {
                Name = "Quantity Sold",
                DataPropertyName = "Quantity Sold",
                HeaderText = "Quantity Sold",
                Width = 125,
                MinimumWidth = 115
            };
            colQtySold.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colQtySold.DefaultCellStyle.Format = "N0";
            colQtySold.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRestockSuggestions.Columns.Add(colQtySold);

            DataGridViewTextBoxColumn colPriority = new DataGridViewTextBoxColumn
            {
                Name = "Restock Priority",
                DataPropertyName = "Restock Priority",
                HeaderText = "Restock Priority",
                Width = 130,
                MinimumWidth = 120
            };
            colPriority.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colPriority.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRestockSuggestions.Columns.Add(colPriority);

            DataGridViewTextBoxColumn colReason = new DataGridViewTextBoxColumn
            {
                Name = "Reason & Rationale",
                DataPropertyName = "Reason & Rationale",
                HeaderText = "Suggestion & Reason",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 320
            };
            colReason.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colReason.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            colReason.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvRestockSuggestions.Columns.Add(colReason);

            BakeryPOS.UI.Styles.ApplyGridStyle(dgvRestockSuggestions);
            dgvRestockSuggestions.RowTemplate.Height = 40;
            dgvRestockSuggestions.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvRestockSuggestions.DefaultCellStyle.Padding = new Padding(4, 4, 4, 4);
            dgvRestockSuggestions.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvRestockSuggestions.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 248, 240);

            dgvRestockSuggestions.CellFormatting += DgvRestock_CellFormatting;
        }

        private void DgvRestock_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvRestockSuggestions.Columns[e.ColumnIndex].Name == "Restock Priority" && e.Value != null)
            {
                string priority = e.Value.ToString();
                if (priority.IndexOf("High", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    e.CellStyle.ForeColor = Color.Crimson;
                    e.CellStyle.Font = new Font(dgvRestockSuggestions.Font, FontStyle.Bold);
                }
                else if (priority.IndexOf("Moderate", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    e.CellStyle.ForeColor = Color.FromArgb(190, 110, 10);
                    e.CellStyle.Font = new Font(dgvRestockSuggestions.Font, FontStyle.Bold);
                }
                else
                {
                    e.CellStyle.ForeColor = Color.DarkGreen;
                    e.CellStyle.Font = new Font(dgvRestockSuggestions.Font, FontStyle.Bold);
                }
            }
        }

        private void cmbPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedPeriod = cmbPeriod.SelectedItem != null ? cmbPeriod.SelectedItem.ToString() : "Daily";

            _isUpdatingPeriod = true;
            try
            {
                switch (selectedPeriod)
                {
                    case "Daily":
                        dtpStartDate.Value = DateTime.Today;
                        dtpEndDate.Value = DateTime.Today;
                        dtpStartDate.Enabled = false;
                        dtpEndDate.Enabled = false;
                        break;

                    case "Weekly":
                        dtpStartDate.Value = DateTime.Today.AddDays(-6);
                        dtpEndDate.Value = DateTime.Today;
                        dtpStartDate.Enabled = false;
                        dtpEndDate.Enabled = false;
                        break;

                    case "Monthly":
                        dtpStartDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                        dtpEndDate.Value = DateTime.Today;
                        dtpStartDate.Enabled = false;
                        dtpEndDate.Enabled = false;
                        break;

                    case "Custom Date Range":
                        dtpStartDate.Enabled = true;
                        dtpEndDate.Enabled = true;
                        break;
                }
            }
            finally
            {
                _isUpdatingPeriod = false;
            }

            // Automatically refresh all report tabs once period and dates have finished updating
            LoadAllReports();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            if (_isUpdatingPeriod)
            {
                return;
            }

            // Automatically refresh reports when either date changes in Custom Date Range mode
            LoadAllReports();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            LoadAllReports();
        }

        private bool ValidateDateRange()
        {
            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date;

            if (startDate > endDate)
            {
                if (!_isValidatingDate)
                {
                    _isValidatingDate = true;
                    try
                    {
                        MessageBox.Show(
                            "The start date cannot be later than the end date. Please select a valid date range.",
                            "Invalid Date Range",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
                    finally
                    {
                        _isValidatingDate = false;
                    }
                }

                lblPeriodSummary.Text = "⚠️ Invalid date range (Start date is later than End date)";
                return false;
            }

            return true;
        }

        private void LoadAllReports()
        {
            if (!ValidateDateRange())
            {
                return;
            }

            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date;

            if (startDate == endDate)
            {
                lblPeriodSummary.Text = "Period: " + startDate.ToString("MMMM dd, yyyy");
            }
            else
            {
                lblPeriodSummary.Text = "Period: " + startDate.ToString("MMM dd, yyyy") + " - " + endDate.ToString("MMM dd, yyyy");
            }

            LoadReportData();
            LoadInventoryReportData();
            LoadAiSmartReports();
        }

        private void LoadReportData()
        {
            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date;

            try
            {
                using (SqlConnection conn = new SqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();

                    // 1. Total Transactions & Total Sales
                    string summarySql = @"
                        SELECT 
                            COUNT(*) AS TotalTransactions,
                            ISNULL(SUM(TotalAmount), 0) AS TotalSales
                        FROM Sales
                        WHERE CAST(SaleDate AS DATE) >= @StartDate AND CAST(SaleDate AS DATE) <= @EndDate";

                    using (SqlCommand cmd = new SqlCommand(summarySql, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                _totalTransactions = Convert.ToInt32(reader["TotalTransactions"]);
                                _totalSales = Convert.ToDecimal(reader["TotalSales"]);
                            }
                            else
                            {
                                _totalTransactions = 0;
                                _totalSales = 0m;
                            }
                        }
                    }

                    // 2. Total Items Sold
                    string itemsSql = @"
                        SELECT ISNULL(SUM(si.Quantity), 0) AS TotalItemsSold
                        FROM SaleItems si
                        INNER JOIN Sales s ON si.SaleID = s.SaleID
                        WHERE CAST(s.SaleDate AS DATE) >= @StartDate AND CAST(s.SaleDate AS DATE) <= @EndDate";

                    using (SqlCommand cmd = new SqlCommand(itemsSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);

                        object result = cmd.ExecuteScalar();
                        _totalItemsSold = (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
                    }

                    // 3. Best-Selling Products
                    string bestSellingSql = @"
                        SELECT 
                            ISNULL(p.ProductName, 'Unknown Product') AS [Product Name],
                            SUM(si.Quantity) AS [Total Quantity Sold],
                            SUM(si.LineTotal) AS [Total Sales Amount]
                        FROM SaleItems si
                        INNER JOIN Sales s ON si.SaleID = s.SaleID
                        LEFT JOIN Products p ON si.ProductID = p.ProductID
                        WHERE CAST(s.SaleDate AS DATE) >= @StartDate AND CAST(s.SaleDate AS DATE) <= @EndDate
                        GROUP BY p.ProductName
                        ORDER BY [Total Quantity Sold] DESC, [Total Sales Amount] DESC";

                    using (SqlCommand cmd = new SqlCommand(bestSellingSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            _dtBestSelling = new DataTable();
                            adapter.Fill(_dtBestSelling);

                            _dtBestSelling.Columns.Add("Rank", typeof(int));
                            for (int i = 0; i < _dtBestSelling.Rows.Count; i++)
                            {
                                _dtBestSelling.Rows[i]["Rank"] = i + 1;
                            }

                            dgvBestSelling.DataSource = _dtBestSelling;
                        }
                    }
                }

                // Calculate Average Order Value
                _averageOrder = _totalTransactions > 0 ? (_totalSales / _totalTransactions) : 0m;

                // Update UI KPI Cards
                lblValTotalSales.Text = "₱" + _totalSales.ToString("N2");
                lblValTransactions.Text = _totalTransactions.ToString("N0");
                lblValItemsSold.Text = _totalItemsSold.ToString("N0");
                lblValAvgOrder.Text = "₱" + _averageOrder.ToString("N2");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load sales report data. Please try again.\n\n" + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                ResetStats();
            }
        }

        private void LoadInventoryReportData()
        {
            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date;

            try
            {
                using (SqlConnection conn = new SqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();

                    // 1. Inventory Summary Cards (Active products, Total in stock, Low stock count)
                    string summarySql = @"
                        SELECT 
                            COUNT(*) AS TotalActiveProducts,
                            ISNULL(SUM(StockQuantity), 0) AS TotalUnitsInStock,
                            ISNULL(SUM(CASE WHEN StockQuantity <= 10 THEN 1 ELSE 0 END), 0) AS LowStockCount
                        FROM Products
                        WHERE IsActive = 1";

                    using (SqlCommand cmd = new SqlCommand(summarySql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                _totalActiveProducts = Convert.ToInt32(reader["TotalActiveProducts"]);
                                _totalUnitsInStock = Convert.ToInt32(reader["TotalUnitsInStock"]);
                                _lowStockCount = Convert.ToInt32(reader["LowStockCount"]);
                            }
                            else
                            {
                                _totalActiveProducts = 0;
                                _totalUnitsInStock = 0;
                                _lowStockCount = 0;
                            }
                        }
                    }

                    // 2. Inventory Table: Active products with Current Stock and Quantity Sold in selected period
                    string inventorySql = @"
                        SELECT 
                            p.ProductID,
                            p.ProductName AS [Product Name],
                            p.StockQuantity AS [Current Stock],
                            ISNULL(salesSummary.QtySold, 0) AS [Quantity Sold],
                            CASE 
                                WHEN p.StockQuantity <= 10 THEN 'Low Stock'
                                ELSE 'Normal'
                            END AS [Stock Status]
                        FROM Products p
                        LEFT JOIN (
                            SELECT 
                                si.ProductID,
                                SUM(si.Quantity) AS QtySold
                            FROM SaleItems si
                            INNER JOIN Sales s ON si.SaleID = s.SaleID
                            WHERE CAST(s.SaleDate AS DATE) >= @StartDate AND CAST(s.SaleDate AS DATE) <= @EndDate
                            GROUP BY si.ProductID
                        ) salesSummary ON p.ProductID = salesSummary.ProductID
                        WHERE p.IsActive = 1
                        ORDER BY ISNULL(salesSummary.QtySold, 0) DESC, p.ProductName ASC";

                    using (SqlCommand cmd = new SqlCommand(inventorySql, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            _dtInventory = new DataTable();
                            adapter.Fill(_dtInventory);
                            dgvInventory.DataSource = _dtInventory;
                        }
                    }
                }

                // Update UI Inventory Cards
                lblValActiveProducts.Text = _totalActiveProducts.ToString("N0");
                lblValTotalStockUnits.Text = _totalUnitsInStock.ToString("N0");
                lblValLowStockCount.Text = _lowStockCount.ToString("N0");

                if (_lowStockCount > 0)
                {
                    lblValLowStockCount.ForeColor = Color.Crimson;
                }
                else
                {
                    lblValLowStockCount.ForeColor = Color.FromArgb(40, 30, 20);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load inventory report data. Please try again.\n\n" + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                ResetInventoryStats();
            }
        }

        private void ResetStats()
        {
            _totalSales = 0m;
            _totalTransactions = 0;
            _totalItemsSold = 0;
            _averageOrder = 0m;

            lblValTotalSales.Text = "₱0.00";
            lblValTransactions.Text = "0";
            lblValItemsSold.Text = "0";
            lblValAvgOrder.Text = "₱0.00";

            if (_dtBestSelling != null)
            {
                _dtBestSelling.Clear();
            }
        }

        private void ResetInventoryStats()
        {
            _totalActiveProducts = 0;
            _totalUnitsInStock = 0;
            _lowStockCount = 0;

            lblValActiveProducts.Text = "0";
            lblValTotalStockUnits.Text = "0";
            lblValLowStockCount.Text = "0";
            lblValLowStockCount.ForeColor = Color.FromArgb(40, 30, 20);

            if (_dtInventory != null)
            {
                _dtInventory.Clear();
            }
        }

        private void ResetAiSmartReports()
        {
            lblAiSummaryText.Text = "No data available for the selected period.";
            lblValAiTrends.Text = "Highest Day: -\r\nLowest Day: -\r\nActive Selling Days: -";

            if (_dtRestockSuggestions != null)
            {
                _dtRestockSuggestions.Clear();
            }
        }

        private void LoadAiSmartReports()
        {
            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date;
            int totalCalendarDays = (endDate - startDate).Days + 1;
            string periodStr = startDate == endDate 
                ? startDate.ToString("MMMM dd, yyyy") 
                : startDate.ToString("MMM dd, yyyy") + " - " + endDate.ToString("MMM dd, yyyy");

            try
            {
                using (SqlConnection conn = new SqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();

                    // 1. Sales Trends: Daily Sales Aggregation (Highest & Lowest Day)
                    string dailySql = @"
                        SELECT 
                            CAST(SaleDate AS DATE) AS SaleDay,
                            ISNULL(SUM(TotalAmount), 0) AS DailyTotal,
                            COUNT(*) AS DailyTransactions
                        FROM Sales
                        WHERE CAST(SaleDate AS DATE) >= @StartDate AND CAST(SaleDate AS DATE) <= @EndDate
                        GROUP BY CAST(SaleDate AS DATE)
                        ORDER BY DailyTotal DESC";

                    DataTable dtDaily = new DataTable();
                    using (SqlCommand cmd = new SqlCommand(dailySql, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dtDaily);
                        }
                    }

                    int activeSalesDays = dtDaily.Rows.Count;
                    string highestDayStr = "None (₱0.00)";
                    string lowestDayStr = "None (₱0.00)";
                    string trendSummary = "0 active sales days (" + totalCalendarDays + " calendar day(s) in selected period)";
                    string highestDayNameForSummary = "";
                    decimal highestDaySalesForSummary = 0m;
                    int highestDayTxnsForSummary = 0;

                    if (activeSalesDays == 1)
                    {
                        DataRow hRow = dtDaily.Rows[0];
                        DateTime hDate = Convert.ToDateTime(hRow["SaleDay"]);
                        highestDaySalesForSummary = Convert.ToDecimal(hRow["DailyTotal"]);
                        highestDayTxnsForSummary = Convert.ToInt32(hRow["DailyTransactions"]);
                        highestDayNameForSummary = hDate.ToString("MMMM dd, yyyy");

                        highestDayStr = hDate.ToString("MMM dd, yyyy") + " — ₱" + highestDaySalesForSummary.ToString("N2") + " (" + highestDayTxnsForSummary + " txn" + (highestDayTxnsForSummary > 1 ? "s" : "") + ")";
                        
                        if (totalCalendarDays > 1)
                        {
                            lowestDayStr = "₱0.00 (" + (totalCalendarDays - 1) + " inactive day(s) in period)";
                            trendSummary = "1 active selling day; " + (totalCalendarDays - 1) + " day(s) with ₱0 sales in range.";
                        }
                        else
                        {
                            lowestDayStr = "Same as highest day (single-day reporting period)";
                            trendSummary = "Single-day reporting period.";
                        }
                    }
                    else if (activeSalesDays > 1)
                    {
                        DataRow hRow = dtDaily.Rows[0];
                        DateTime hDate = Convert.ToDateTime(hRow["SaleDay"]);
                        highestDaySalesForSummary = Convert.ToDecimal(hRow["DailyTotal"]);
                        highestDayTxnsForSummary = Convert.ToInt32(hRow["DailyTransactions"]);
                        highestDayNameForSummary = hDate.ToString("MMMM dd, yyyy");

                        highestDayStr = hDate.ToString("MMM dd, yyyy") + " — ₱" + highestDaySalesForSummary.ToString("N2") + " (" + highestDayTxnsForSummary + " txns)";

                        DataRow lRow = dtDaily.Rows[activeSalesDays - 1];
                        DateTime lDate = Convert.ToDateTime(lRow["SaleDay"]);
                        decimal lowestTotal = Convert.ToDecimal(lRow["DailyTotal"]);
                        int lowestTxns = Convert.ToInt32(lRow["DailyTransactions"]);

                        lowestDayStr = lDate.ToString("MMM dd, yyyy") + " — ₱" + lowestTotal.ToString("N2") + " (" + lowestTxns + " txn" + (lowestTxns > 1 ? "s" : "") + ")";

                        if (totalCalendarDays > activeSalesDays)
                        {
                            trendSummary = activeSalesDays + " active days with sales; " + (totalCalendarDays - activeSalesDays) + " day(s) with ₱0 sales.";
                        }
                        else
                        {
                            trendSummary = "Sales recorded consistently across all " + totalCalendarDays + " days in period.";
                        }
                    }

                    // 2. Best-Selling Products for Period
                    string topProductsSql = @"
                        SELECT 
                            ISNULL(p.ProductName, 'Unknown Product') AS ProductName,
                            SUM(si.Quantity) AS TotalQtySold,
                            SUM(si.LineTotal) AS TotalSalesAmount
                        FROM SaleItems si
                        INNER JOIN Sales s ON si.SaleID = s.SaleID
                        LEFT JOIN Products p ON si.ProductID = p.ProductID
                        WHERE CAST(s.SaleDate AS DATE) >= @StartDate AND CAST(s.SaleDate AS DATE) <= @EndDate
                        GROUP BY p.ProductName
                        ORDER BY TotalQtySold DESC, TotalSalesAmount DESC";

                    DataTable dtTop = new DataTable();
                    using (SqlCommand cmd = new SqlCommand(topProductsSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dtTop);
                        }
                    }

                    string topProdName = "";
                    int topProdQty = 0;
                    decimal topProdAmt = 0m;
                    string topSellerSummary = "";

                    if (dtTop.Rows.Count > 0)
                    {
                        topProdName = dtTop.Rows[0]["ProductName"].ToString();
                        topProdQty = Convert.ToInt32(dtTop.Rows[0]["TotalQtySold"]);
                        topProdAmt = Convert.ToDecimal(dtTop.Rows[0]["TotalSalesAmount"]);

                        topSellerSummary = "#1 Top Seller: " + topProdName + " (" + topProdQty.ToString("N0") + " sold · ₱" + topProdAmt.ToString("N2") + ")";

                        if (dtTop.Rows.Count > 1)
                        {
                            string runnerName = dtTop.Rows[1]["ProductName"].ToString();
                            int runnerQty = Convert.ToInt32(dtTop.Rows[1]["TotalQtySold"]);
                            decimal runnerAmt = Convert.ToDecimal(dtTop.Rows[1]["TotalSalesAmount"]);
                            topSellerSummary += "\r\n#2 Runner-Up: " + runnerName + " (" + runnerQty.ToString("N0") + " sold · ₱" + runnerAmt.ToString("N2") + ")";
                        }

                        topSellerSummary += "\r\nTotal Products Sold: " + dtTop.Rows.Count + " distinct product(s)";
                    }
                    else
                    {
                        topSellerSummary = "Top Performer: None (0 units sold)\r\nTotal Products Sold: 0\r\nNo customer purchases in selected period.";
                    }

                    // 3. Smart Restock Suggestions
                    string restockSql = @"
                        SELECT 
                            p.ProductID,
                            p.ProductName,
                            p.StockQuantity,
                            ISNULL(salesSummary.QtySold, 0) AS QtySold
                        FROM Products p
                        LEFT JOIN (
                            SELECT 
                                si.ProductID,
                                SUM(si.Quantity) AS QtySold
                            FROM SaleItems si
                            INNER JOIN Sales s ON si.SaleID = s.SaleID
                            WHERE CAST(s.SaleDate AS DATE) >= @StartDate AND CAST(s.SaleDate AS DATE) <= @EndDate
                            GROUP BY si.ProductID
                        ) salesSummary ON p.ProductID = salesSummary.ProductID
                        WHERE p.IsActive = 1
                        ORDER BY 
                            CASE 
                                WHEN p.StockQuantity <= 10 AND ISNULL(salesSummary.QtySold, 0) > 0 THEN 1
                                WHEN p.StockQuantity <= 10 THEN 2
                                WHEN p.StockQuantity <= 20 AND ISNULL(salesSummary.QtySold, 0) > 0 THEN 3
                                ELSE 4
                            END ASC,
                            ISNULL(salesSummary.QtySold, 0) DESC,
                            p.StockQuantity ASC,
                            p.ProductName ASC";

                    DataTable dtRawRestock = new DataTable();
                    using (SqlCommand cmd = new SqlCommand(restockSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dtRawRestock);
                        }
                    }

                    _dtRestockSuggestions = new DataTable();
                    _dtRestockSuggestions.Columns.Add("Product Name", typeof(string));
                    _dtRestockSuggestions.Columns.Add("Current Stock", typeof(int));
                    _dtRestockSuggestions.Columns.Add("Quantity Sold", typeof(int));
                    _dtRestockSuggestions.Columns.Add("Restock Priority", typeof(string));
                    _dtRestockSuggestions.Columns.Add("Reason & Rationale", typeof(string));

                    int highPriorityCount = 0;
                    int moderatePriorityCount = 0;

                    foreach (DataRow row in dtRawRestock.Rows)
                    {
                        string pName = row["ProductName"].ToString();
                        int stock = Convert.ToInt32(row["StockQuantity"]);
                        int qtySold = Convert.ToInt32(row["QtySold"]);
                        string priority;
                        string reason;

                        if (stock <= 10 && qtySold > 0)
                        {
                            priority = "High Priority";
                            reason = "Critical stock (" + stock.ToString("N0") + " units) with active sales demand (" + qtySold.ToString("N0") + " sold). Immediate restock advised.";
                            highPriorityCount++;
                        }
                        else if (stock <= 10)
                        {
                            priority = "High Priority";
                            reason = "Stock is at or below minimum threshold (" + stock.ToString("N0") + " units remaining). Restock advised.";
                            highPriorityCount++;
                        }
                        else if (stock <= 20 && qtySold > 0)
                        {
                            priority = "Moderate Priority";
                            reason = "Moderate stock (" + stock.ToString("N0") + " units) with active sales velocity (" + qtySold.ToString("N0") + " sold). Monitor inventory.";
                            moderatePriorityCount++;
                        }
                        else if (qtySold > 0)
                        {
                            priority = "Normal";
                            reason = "Stock is currently adequate (" + stock.ToString("N0") + " units remaining; " + qtySold.ToString("N0") + " units sold in period).";
                        }
                        else
                        {
                            priority = "Normal";
                            reason = "Stock is stable (" + stock.ToString("N0") + " units remaining) with no sales recorded in this period.";
                        }

                        _dtRestockSuggestions.Rows.Add(pName, stock, qtySold, priority, reason);
                    }

                    dgvRestockSuggestions.DataSource = _dtRestockSuggestions;

                    // 4. Insight Summary Bullet Points
                    List<string> bullets = new List<string>();

                    if (_totalTransactions > 0 && !string.IsNullOrEmpty(topProdName))
                    {
                        bullets.Add("• Best-Selling Product: " + topProdName + " was the best-selling product during this period with " + topProdQty.ToString("N0") + " units sold, generating ₱" + topProdAmt.ToString("N2") + " in sales.");
                        
                        bullets.Add("• Highest Sales Day: " + highestDayNameForSummary + " generating ₱" + highestDaySalesForSummary.ToString("N2") + " across " + highestDayTxnsForSummary + " transaction(s). (" + trendSummary + ")");

                        if (highPriorityCount > 0)
                        {
                            bullets.Add("• Restock Alert: " + highPriorityCount + " active product(s) have critical/low stock (≤ 10 units) and require restocking attention.");
                        }
                        else if (moderatePriorityCount > 0)
                        {
                            bullets.Add("• Inventory Watch: All products meet the minimum threshold, but " + moderatePriorityCount + " product(s) have moderate stock with ongoing customer demand.");
                        }
                        else
                        {
                            bullets.Add("• Inventory Health: All active products maintain healthy stock levels above minimum threshold (> 10 units).");
                        }

                        bullets.Add("• Total Sales: ₱" + _totalSales.ToString("N2") + " across " + _totalTransactions.ToString("N0") + " order(s) (average sale ₱" + _averageOrder.ToString("N2") + "; " + _totalItemsSold.ToString("N0") + " items sold).");
                    }
                    else
                    {
                        bullets.Add("• Period Notice: No customer transactions were recorded for the selected period (" + periodStr + ").");
                        bullets.Add("• Sales Trends: Daily peak sales and product velocities cannot be evaluated for this period because total sales are ₱0.00.");

                        if (_lowStockCount > 0)
                        {
                            bullets.Add("• Restock Alert: " + _lowStockCount + " active product(s) currently need restocking due to stock at or below 10 units.");
                        }
                        else
                        {
                            bullets.Add("• Inventory Health: All " + _totalActiveProducts + " active catalog products have adequate stock (> 10 units; " + _totalUnitsInStock.ToString("N0") + " total units available).");
                        }
                    }

                    lblAiSummaryText.Text = string.Join("\r\n", bullets.ToArray());
                    lblValAiTrends.Text = "Highest Day: " + highestDayStr + "\r\nLowest Day: " + lowestDayStr + "\r\nActive Selling Days: " + trendSummary;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load AI smart report insights. Please try again.\n\n" + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                ResetAiSmartReports();
            }
        }

        private void tabReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabReports.SelectedTab == tabAiReports)
            {
                AdjustAiRestockContainerHeight();
            }
        }

        private void pnlAiContent_Resize(object sender, EventArgs e)
        {
            AdjustAiRestockContainerHeight();
        }

        private void AdjustAiRestockContainerHeight()
        {
            if (pnlAiContent == null || pnlAiRestockContainer == null)
            {
                return;
            }

            int fixedUsedHeight = pnlAiSummaryCard.Height + pnlCardAiTrends.Height + 20 + pnlAiContent.Padding.Top + pnlAiContent.Padding.Bottom;
            int available = pnlAiContent.ClientSize.Height - fixedUsedHeight;

            // Maintain at least 320px for restock table so suggestions are readable, expanding if extra vertical space is available
            pnlAiRestockContainer.Height = Math.Max(320, available);
        }

        private void btnCashierShiftReport_Click(object sender, EventArgs e)
        {
            using (frmEndOfShiftReport shiftReport = new frmEndOfShiftReport("Cashier", _username))
            {
                shiftReport.ShowDialog(this);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDocument printDoc = new PrintDocument();
                if (tabReports.SelectedTab == tabInventory)
                {
                    printDoc.DocumentName = "BakeryPOS_InventoryReport";
                    printDoc.PrintPage += PrintInventoryDoc_PrintPage;
                }
                else if (tabReports.SelectedTab == tabAiReports)
                {
                    printDoc.DocumentName = "BakeryPOS_AISmartReport";
                    printDoc.PrintPage += PrintAiSmartDoc_PrintPage;
                }
                else
                {
                    printDoc.DocumentName = "BakeryPOS_SalesReport";
                    printDoc.PrintPage += PrintDoc_PrintPage;
                }

                using (PrintPreviewDialog previewDialog = new PrintPreviewDialog())
                {
                    previewDialog.Document = printDoc;
                    previewDialog.Width = 850;
                    previewDialog.Height = 650;
                    previewDialog.StartPosition = FormStartPosition.CenterParent;
                    previewDialog.ShowDialog(this);
                }
            }
            catch
            {
                MessageBox.Show(
                    "Unable to print report at this time.",
                    "Print Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            float printableWidth = e.MarginBounds.Width;
            float currentY = topMargin;

            using (Font titleFont = new Font("Segoe UI", 16F, FontStyle.Bold))
            using (Font subTitleFont = new Font("Segoe UI", 12F, FontStyle.Bold))
            using (Font sectionFont = new Font("Segoe UI", 11F, FontStyle.Bold))
            using (Font boldFont = new Font("Segoe UI", 9.5F, FontStyle.Bold))
            using (Font regularFont = new Font("Segoe UI", 9.5F, FontStyle.Regular))
            using (Brush darkBrush = new SolidBrush(Color.FromArgb(40, 30, 20)))
            using (Brush brownBrush = new SolidBrush(Color.FromArgb(110, 70, 45)))
            using (Pen thickPen = new Pen(Color.FromArgb(110, 70, 45), 2))
            using (Pen linePen = new Pen(Color.FromArgb(200, 190, 180), 1))
            {
                StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center };
                StringFormat rightFormat = new StringFormat { Alignment = StringAlignment.Far };

                // Header
                g.DrawString("Bread Basket Bakeshop", titleFont, brownBrush, leftMargin + (printableWidth / 2f), currentY, centerFormat);
                currentY += 26;

                g.DrawString("Sales & Performance Report", subTitleFont, darkBrush, leftMargin + (printableWidth / 2f), currentY, centerFormat);
                currentY += 24;

                g.DrawLine(thickPen, leftMargin, currentY, leftMargin + printableWidth, currentY);
                currentY += 8;

                // Period and Generated date
                string periodStr = dtpStartDate.Value.Date == dtpEndDate.Value.Date
                    ? dtpStartDate.Value.ToString("MMMM dd, yyyy")
                    : dtpStartDate.Value.ToString("MMM dd, yyyy") + " - " + dtpEndDate.Value.ToString("MMM dd, yyyy");

                g.DrawString("Report Period: " + periodStr, boldFont, darkBrush, leftMargin, currentY);
                g.DrawString("Generated: " + DateTime.Now.ToString("MMM dd, yyyy hh:mm tt"), regularFont, darkBrush, leftMargin + printableWidth, currentY, rightFormat);
                currentY += 22;

                g.DrawLine(linePen, leftMargin, currentY, leftMargin + printableWidth, currentY);
                currentY += 14;

                // KPI Summary
                g.DrawString("Summary Overview", sectionFont, brownBrush, leftMargin, currentY);
                currentY += 20;

                float col1 = leftMargin;
                float col2 = leftMargin + (printableWidth / 2f);

                g.DrawString("Total Sales:", boldFont, darkBrush, col1, currentY);
                g.DrawString("₱" + _totalSales.ToString("N2"), boldFont, brownBrush, col1 + 160, currentY);
                g.DrawString("Total Transactions:", boldFont, darkBrush, col2, currentY);
                g.DrawString(_totalTransactions.ToString(), regularFont, darkBrush, col2 + 160, currentY);
                currentY += 18;

                g.DrawString("Total Items Sold:", boldFont, darkBrush, col1, currentY);
                g.DrawString(_totalItemsSold.ToString(), regularFont, darkBrush, col1 + 160, currentY);
                g.DrawString("Average Sale:", boldFont, darkBrush, col2, currentY);
                g.DrawString("₱" + _averageOrder.ToString("N2"), regularFont, darkBrush, col2 + 160, currentY);
                currentY += 24;

                g.DrawLine(linePen, leftMargin, currentY, leftMargin + printableWidth, currentY);
                currentY += 14;

                // Best Selling Products Table
                g.DrawString("Best-Selling Products", sectionFont, brownBrush, leftMargin, currentY);
                currentY += 22;

                // Table Header
                g.FillRectangle(new SolidBrush(Color.FromArgb(245, 238, 230)), leftMargin, currentY, printableWidth, 22);
                g.DrawString("#", boldFont, brownBrush, leftMargin + 4, currentY + 3);
                g.DrawString("Product Name", boldFont, brownBrush, leftMargin + 40, currentY + 3);
                g.DrawString("Qty Sold", boldFont, brownBrush, leftMargin + printableWidth - 170, currentY + 3);
                g.DrawString("Total Amount", boldFont, brownBrush, leftMargin + printableWidth - 20, currentY + 3, rightFormat);
                currentY += 26;

                if (_dtBestSelling != null && _dtBestSelling.Rows.Count > 0)
                {
                    int rank = 1;
                    foreach (DataRow row in _dtBestSelling.Rows)
                    {
                        g.DrawString(rank.ToString(), regularFont, darkBrush, leftMargin + 4, currentY);
                        g.DrawString(row["Product Name"].ToString(), regularFont, darkBrush, leftMargin + 40, currentY);
                        g.DrawString(Convert.ToInt32(row["Total Quantity Sold"]).ToString("N0"), regularFont, darkBrush, leftMargin + printableWidth - 170, currentY);
                        g.DrawString("₱" + Convert.ToDecimal(row["Total Sales Amount"]).ToString("N2"), regularFont, darkBrush, leftMargin + printableWidth - 20, currentY, rightFormat);
                        currentY += 18;
                        rank++;

                        if (currentY > e.MarginBounds.Bottom - 40) break;
                    }
                }
                else
                {
                    g.DrawString("No sales recorded for this period.", regularFont, darkBrush, leftMargin + 40, currentY);
                    currentY += 20;
                }

                currentY += 10;
                g.DrawLine(thickPen, leftMargin, currentY, leftMargin + printableWidth, currentY);
            }
        }

        private void PrintInventoryDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            float printableWidth = e.MarginBounds.Width;
            float currentY = topMargin;

            using (Font titleFont = new Font("Segoe UI", 16F, FontStyle.Bold))
            using (Font subTitleFont = new Font("Segoe UI", 12F, FontStyle.Bold))
            using (Font sectionFont = new Font("Segoe UI", 11F, FontStyle.Bold))
            using (Font boldFont = new Font("Segoe UI", 9.5F, FontStyle.Bold))
            using (Font regularFont = new Font("Segoe UI", 9.5F, FontStyle.Regular))
            using (Font noteFont = new Font("Segoe UI", 8F, FontStyle.Italic))
            using (Brush darkBrush = new SolidBrush(Color.FromArgb(40, 30, 20)))
            using (Brush brownBrush = new SolidBrush(Color.FromArgb(110, 70, 45)))
            using (Brush redBrush = new SolidBrush(Color.Crimson))
            using (Brush greenBrush = new SolidBrush(Color.DarkGreen))
            using (Pen thickPen = new Pen(Color.FromArgb(110, 70, 45), 2))
            using (Pen linePen = new Pen(Color.FromArgb(200, 190, 180), 1))
            {
                StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center };
                StringFormat rightFormat = new StringFormat { Alignment = StringAlignment.Far };

                // Header
                g.DrawString("Bread Basket Bakeshop", titleFont, brownBrush, leftMargin + (printableWidth / 2f), currentY, centerFormat);
                currentY += 26;

                g.DrawString("Inventory & Stock Performance Report", subTitleFont, darkBrush, leftMargin + (printableWidth / 2f), currentY, centerFormat);
                currentY += 24;

                g.DrawLine(thickPen, leftMargin, currentY, leftMargin + printableWidth, currentY);
                currentY += 8;

                // Period and Generated date
                string periodStr = dtpStartDate.Value.Date == dtpEndDate.Value.Date
                    ? dtpStartDate.Value.ToString("MMMM dd, yyyy")
                    : dtpStartDate.Value.ToString("MMM dd, yyyy") + " - " + dtpEndDate.Value.ToString("MMM dd, yyyy");

                g.DrawString("Report Period: " + periodStr, boldFont, darkBrush, leftMargin, currentY);
                g.DrawString("Generated: " + DateTime.Now.ToString("MMM dd, yyyy hh:mm tt"), regularFont, darkBrush, leftMargin + printableWidth, currentY, rightFormat);
                currentY += 22;

                g.DrawLine(linePen, leftMargin, currentY, leftMargin + printableWidth, currentY);
                currentY += 14;

                // Inventory Summary Cards
                g.DrawString("Inventory Summary", sectionFont, brownBrush, leftMargin, currentY);
                currentY += 20;

                float col1 = leftMargin;
                float col2 = leftMargin + (printableWidth / 3f);
                float col3 = leftMargin + (2f * printableWidth / 3f);

                g.DrawString("Active Products:", boldFont, darkBrush, col1, currentY);
                g.DrawString(_totalActiveProducts.ToString(), boldFont, brownBrush, col1 + 130, currentY);

                g.DrawString("Units in Stock:", boldFont, darkBrush, col2, currentY);
                g.DrawString(_totalUnitsInStock.ToString(), boldFont, brownBrush, col2 + 120, currentY);

                g.DrawString("Low-Stock (≤10):", boldFont, darkBrush, col3, currentY);
                g.DrawString(_lowStockCount.ToString(), boldFont, _lowStockCount > 0 ? redBrush : greenBrush, col3 + 120, currentY);
                currentY += 24;

                g.DrawLine(linePen, leftMargin, currentY, leftMargin + printableWidth, currentY);
                currentY += 14;

                // Inventory Table
                g.DrawString("Product Stock & Sales Performance", sectionFont, brownBrush, leftMargin, currentY);
                currentY += 22;

                // Table Header
                g.FillRectangle(new SolidBrush(Color.FromArgb(245, 238, 230)), leftMargin, currentY, printableWidth, 22);
                g.DrawString("Product Name", boldFont, brownBrush, leftMargin + 4, currentY + 3);
                g.DrawString("Current Stock", boldFont, brownBrush, leftMargin + printableWidth - 260, currentY + 3);
                g.DrawString("Qty Sold", boldFont, brownBrush, leftMargin + printableWidth - 140, currentY + 3);
                g.DrawString("Stock Status", boldFont, brownBrush, leftMargin + printableWidth - 20, currentY + 3, rightFormat);
                currentY += 26;

                if (_dtInventory != null && _dtInventory.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtInventory.Rows)
                    {
                        g.DrawString(row["Product Name"].ToString(), regularFont, darkBrush, leftMargin + 4, currentY);
                        g.DrawString(Convert.ToInt32(row["Current Stock"]).ToString("N0"), regularFont, darkBrush, leftMargin + printableWidth - 240, currentY);
                        g.DrawString(Convert.ToInt32(row["Quantity Sold"]).ToString("N0"), regularFont, darkBrush, leftMargin + printableWidth - 120, currentY);

                        string status = row["Stock Status"].ToString();
                        Brush statusBrush = status.Equals("Low Stock", StringComparison.OrdinalIgnoreCase) ? redBrush : greenBrush;
                        g.DrawString(status, boldFont, statusBrush, leftMargin + printableWidth - 20, currentY, rightFormat);
                        currentY += 18;

                        if (currentY > e.MarginBounds.Bottom - 50) break;
                    }
                }
                else
                {
                    g.DrawString("No active products available.", regularFont, darkBrush, leftMargin + 4, currentY);
                    currentY += 20;
                }

                currentY += 12;
                g.DrawString("* Note: Quantity Sold represents customer sales only during the selected period.", noteFont, darkBrush, leftMargin, currentY);
                currentY += 16;
                g.DrawLine(thickPen, leftMargin, currentY, leftMargin + printableWidth, currentY);
            }
        }

        private void PrintAiSmartDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            float printableWidth = e.MarginBounds.Width;
            float currentY = topMargin;

            using (Font titleFont = new Font("Segoe UI", 16F, FontStyle.Bold))
            using (Font subTitleFont = new Font("Segoe UI", 12F, FontStyle.Bold))
            using (Font sectionFont = new Font("Segoe UI", 11F, FontStyle.Bold))
            using (Font boldFont = new Font("Segoe UI", 9.5F, FontStyle.Bold))
            using (Font regularFont = new Font("Segoe UI", 9.5F, FontStyle.Regular))
            using (Font noteFont = new Font("Segoe UI", 8.5F, FontStyle.Italic))
            using (Brush darkBrush = new SolidBrush(Color.FromArgb(40, 30, 20)))
            using (Brush brownBrush = new SolidBrush(Color.FromArgb(110, 70, 45)))
            using (Brush redBrush = new SolidBrush(Color.Crimson))
            using (Brush orangeBrush = new SolidBrush(Color.FromArgb(190, 110, 10)))
            using (Brush greenBrush = new SolidBrush(Color.DarkGreen))
            using (Pen thickPen = new Pen(Color.FromArgb(110, 70, 45), 2))
            using (Pen linePen = new Pen(Color.FromArgb(200, 190, 180), 1))
            {
                StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center };
                StringFormat rightFormat = new StringFormat { Alignment = StringAlignment.Far };

                // Header
                g.DrawString("Bread Basket Bakeshop", titleFont, brownBrush, leftMargin + (printableWidth / 2f), currentY, centerFormat);
                currentY += 26;

                g.DrawString("AI Smart Reports & Operational Insights", subTitleFont, darkBrush, leftMargin + (printableWidth / 2f), currentY, centerFormat);
                currentY += 24;

                g.DrawLine(thickPen, leftMargin, currentY, leftMargin + printableWidth, currentY);
                currentY += 8;

                // Period and Generated date
                string periodStr = dtpStartDate.Value.Date == dtpEndDate.Value.Date
                    ? dtpStartDate.Value.ToString("MMMM dd, yyyy")
                    : dtpStartDate.Value.ToString("MMM dd, yyyy") + " - " + dtpEndDate.Value.ToString("MMM dd, yyyy");

                g.DrawString("Report Period: " + periodStr, boldFont, darkBrush, leftMargin, currentY);
                g.DrawString("Generated: " + DateTime.Now.ToString("MMM dd, yyyy hh:mm tt"), regularFont, darkBrush, leftMargin + printableWidth, currentY, rightFormat);
                currentY += 22;

                g.DrawLine(linePen, leftMargin, currentY, leftMargin + printableWidth, currentY);
                currentY += 14;

                // Business Summary Section
                g.DrawString("Business Summary", sectionFont, brownBrush, leftMargin, currentY);
                currentY += 20;

                string[] summaryLines = lblAiSummaryText.Text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in summaryLines)
                {
                    g.DrawString(line, regularFont, darkBrush, leftMargin + 8, currentY);
                    currentY += 18;
                }
                currentY += 6;

                g.DrawLine(linePen, leftMargin, currentY, leftMargin + printableWidth, currentY);
                currentY += 14;

                // Sales Trends Section
                g.DrawString("Sales Trends", sectionFont, brownBrush, leftMargin, currentY);
                currentY += 20;

                string[] trendLines = lblValAiTrends.Text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string tLine in trendLines)
                {
                    g.DrawString(tLine, regularFont, darkBrush, leftMargin + 8, currentY);
                    currentY += 16;
                }

                currentY += 6;
                g.DrawLine(linePen, leftMargin, currentY, leftMargin + printableWidth, currentY);
                currentY += 14;

                // Smart Restock Table
                g.DrawString("Smart Restock Suggestions", sectionFont, brownBrush, leftMargin, currentY);
                currentY += 22;

                // Table Header
                g.FillRectangle(new SolidBrush(Color.FromArgb(245, 238, 230)), leftMargin, currentY, printableWidth, 22);
                g.DrawString("Product Name", boldFont, brownBrush, leftMargin + 4, currentY + 3);
                g.DrawString("Stock", boldFont, brownBrush, leftMargin + printableWidth - 410, currentY + 3);
                g.DrawString("Quantity Sold", boldFont, brownBrush, leftMargin + printableWidth - 340, currentY + 3);
                g.DrawString("Priority", boldFont, brownBrush, leftMargin + printableWidth - 230, currentY + 3);
                g.DrawString("Suggestion & Reason", boldFont, brownBrush, leftMargin + printableWidth - 140, currentY + 3);
                currentY += 26;

                if (_dtRestockSuggestions != null && _dtRestockSuggestions.Rows.Count > 0)
                {
                    foreach (DataRow row in _dtRestockSuggestions.Rows)
                    {
                        g.DrawString(row["Product Name"].ToString(), regularFont, darkBrush, leftMargin + 4, currentY);
                        g.DrawString(Convert.ToInt32(row["Current Stock"]).ToString("N0"), regularFont, darkBrush, leftMargin + printableWidth - 405, currentY);
                        g.DrawString(Convert.ToInt32(row["Quantity Sold"]).ToString("N0"), regularFont, darkBrush, leftMargin + printableWidth - 320, currentY);

                        string priority = row["Restock Priority"].ToString();
                        Brush priorityBrush = priority.IndexOf("High", StringComparison.OrdinalIgnoreCase) >= 0 ? redBrush
                            : (priority.IndexOf("Moderate", StringComparison.OrdinalIgnoreCase) >= 0 ? orangeBrush : greenBrush);

                        g.DrawString(priority, boldFont, priorityBrush, leftMargin + printableWidth - 230, currentY);

                        string reason = row["Reason & Rationale"].ToString();
                        if (reason.Length > 44)
                        {
                            reason = reason.Substring(0, 41) + "...";
                        }
                        g.DrawString(reason, regularFont, darkBrush, leftMargin + printableWidth - 140, currentY);

                        currentY += 18;
                        if (currentY > e.MarginBounds.Bottom - 50) break;
                    }
                }
                else
                {
                    g.DrawString("No products require restocking attention.", regularFont, darkBrush, leftMargin + 4, currentY);
                    currentY += 20;
                }

                currentY += 12;
                g.DrawString("* Note: AI Smart Reports provides transparent rule-based analysis on verified POS sales and inventory records.", noteFont, darkBrush, leftMargin, currentY);
                currentY += 16;
                g.DrawLine(thickPen, leftMargin, currentY, leftMargin + printableWidth, currentY);
            }
        }
    }
}

