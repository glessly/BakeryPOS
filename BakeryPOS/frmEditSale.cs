using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BakeryPOS
{
    public partial class frmEditSale : Form
    {
        private string GetConnectionString() => DbHelper.GetConnectionString();

        private int saleID;
        private string currentUsername;
        private decimal originalCashAmount = 0m;
        private byte[] saleRowVer = null;

        public frmEditSale()
        {
            InitializeComponent();
        }

        public frmEditSale(int selectedSaleID, string username) : this()
        {
            saleID = selectedSaleID;
            currentUsername = username;
            lblSaleID.Text = selectedSaleID.ToString();
            LoadSaleItems();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void lblSaleID_Click(object sender, EventArgs e) { }

        private void LoadSaleItems()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    using (SqlCommand hdrCmd = new SqlCommand("SELECT CashAmount, RowVer FROM Sales WHERE SaleID = @SaleID", conn))
                    {
                        hdrCmd.Parameters.AddWithValue("@SaleID", saleID);
                        using (SqlDataReader rdr = hdrCmd.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                originalCashAmount = rdr["CashAmount"] != DBNull.Value ? Convert.ToDecimal(rdr["CashAmount"]) : 0m;
                                saleRowVer = rdr["RowVer"] != DBNull.Value ? (byte[])rdr["RowVer"] : null;
                            }
                        }
                    }

                    string query = @"
                        SELECT
                            si.SaleItemID,
                            si.ProductID,
                            p.ProductName,
                            si.Quantity,
                            si.Quantity AS OriginalQuantity,
                            si.UnitPrice,
                            si.LineTotal
                        FROM SaleItems si
                        INNER JOIN Products p ON si.ProductID = p.ProductID
                        WHERE si.SaleID = @SaleID
                        ORDER BY si.SaleItemID";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@SaleID", saleID);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        dgvSaleItems.DataSource = table;
                        FormatSaleItemsGrid();
                        UpdateSaleTotal();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmEditSale.LoadSaleItems", ex);
                MessageBox.Show(
                    "We couldn't load the sale items. Please try again.",
                    "Something Went Wrong",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void FormatSaleItemsGrid()
        {
            if (dgvSaleItems.Columns.Count == 0) return;

            BakeryPOS.UI.Styles.ApplyGridStyle(dgvSaleItems);
            dgvSaleItems.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSaleItems.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvSaleItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvSaleItems.Columns.Contains("SaleItemID")) { dgvSaleItems.Columns["SaleItemID"].Visible = false; dgvSaleItems.Columns["SaleItemID"].ReadOnly = true; }
            if (dgvSaleItems.Columns.Contains("ProductID")) { dgvSaleItems.Columns["ProductID"].Visible = false; dgvSaleItems.Columns["ProductID"].ReadOnly = true; }
            if (dgvSaleItems.Columns.Contains("OriginalQuantity")) { dgvSaleItems.Columns["OriginalQuantity"].Visible = false; dgvSaleItems.Columns["OriginalQuantity"].ReadOnly = true; }

            if (dgvSaleItems.Columns.Contains("ProductName"))
            {
                var col = dgvSaleItems.Columns["ProductName"];
                col.HeaderText = "Product Name";
                col.ReadOnly = true;
                col.FillWeight = 40F;
                col.MinimumWidth = 150;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
            if (dgvSaleItems.Columns.Contains("Quantity"))
            {
                var col = dgvSaleItems.Columns["Quantity"];
                col.HeaderText = "Quantity";
                col.ReadOnly = false;
                col.FillWeight = 18F;
                col.MinimumWidth = 80;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvSaleItems.Columns.Contains("UnitPrice"))
            {
                var col = dgvSaleItems.Columns["UnitPrice"];
                col.HeaderText = "Unit Price";
                col.ReadOnly = true;
                col.FillWeight = 21F;
                col.MinimumWidth = 100;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                col.DefaultCellStyle.Format = "₱#,##0.00";
            }
            if (dgvSaleItems.Columns.Contains("LineTotal"))
            {
                var col = dgvSaleItems.Columns["LineTotal"];
                col.HeaderText = "Item Total";
                col.ReadOnly = true;
                col.FillWeight = 21F;
                col.MinimumWidth = 100;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                col.DefaultCellStyle.Format = "₱#,##0.00";
            }

            foreach (DataGridViewRow row in dgvSaleItems.Rows)
                row.Height = 34;
        }

        private int GetAvailableStock(int productID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT StockQuantity FROM Products WHERE ProductID = @ProductID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", productID);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                            return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmEditSale.GetAvailableStock", ex);
            }
            return 0;
        }

        private void dgvSaleItems_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void dgvSaleItems_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (this.Disposing || this.IsDisposed) return;
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvSaleItems.Columns[e.ColumnIndex].Name == "Quantity")
            {
                string rawInput = e.FormattedValue != null ? e.FormattedValue.ToString().Trim() : "";
                if (!int.TryParse(rawInput, out int newQuantity) || newQuantity <= 0)
                {
                    MessageBox.Show("Quantity must be a whole number greater than 0.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }

                DataGridViewRow row = dgvSaleItems.Rows[e.RowIndex];
                if (row != null && row.Cells["ProductID"].Value != null && row.Cells["OriginalQuantity"].Value != null)
                {
                    int productID = Convert.ToInt32(row.Cells["ProductID"].Value);
                    int originalQuantity = Convert.ToInt32(row.Cells["OriginalQuantity"].Value);
                    int currentStock = GetAvailableStock(productID);
                    int maxAllowed = currentStock + originalQuantity;

                    if (newQuantity > maxAllowed)
                    {
                        string prodName = row.Cells["ProductName"].Value != null ? row.Cells["ProductName"].Value.ToString() : "Product";
                        MessageBox.Show(
                            $"The quantity entered ({newQuantity}) for '{prodName}' exceeds available stock.\r\n\r\n" +
                            $"Current Available Stock: {currentStock}\r\n" +
                            $"Quantity in this Sale: {originalQuantity}\r\n" +
                            $"Maximum Allowable Quantity: {maxAllowed}",
                            "Insufficient Stock",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        e.Cancel = true;
                    }
                }
            }
        }

        private void dgvSaleItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            MessageBox.Show("Please enter a valid whole number greater than 0.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.Cancel = false;
        }

        private void dgvSaleItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgvSaleItems.Columns[e.ColumnIndex].Name == "Quantity")
                RecalculateRowAndTotal(e.RowIndex);
        }

        private void dgvSaleItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgvSaleItems.Columns[e.ColumnIndex].Name == "Quantity")
                RecalculateRowAndTotal(e.RowIndex);
        }

        private void RecalculateRowAndTotal(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvSaleItems.Rows.Count) return;
            DataGridViewRow row = dgvSaleItems.Rows[rowIndex];
            if (row.IsNewRow) return;

            if (row.Cells["Quantity"].Value != null && row.Cells["UnitPrice"].Value != null)
            {
                if (int.TryParse(row.Cells["Quantity"].Value.ToString(), out int qty) &&
                    decimal.TryParse(row.Cells["UnitPrice"].Value.ToString(), out decimal unitPrice))
                {
                    decimal newItemTotal = qty * unitPrice;
                    object currentVal = row.Cells["LineTotal"].Value;
                    if (currentVal == null || !decimal.TryParse(currentVal.ToString(), out decimal currentDec) || currentDec != newItemTotal)
                        row.Cells["LineTotal"].Value = newItemTotal;
                }
            }
            UpdateSaleTotal();
        }

        private void UpdateSaleTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvSaleItems.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["LineTotal"].Value != null && row.Cells["LineTotal"].Value != DBNull.Value &&
                    decimal.TryParse(row.Cells["LineTotal"].Value.ToString(), out decimal lineTotal))
                {
                    total += lineTotal;
                }
            }
            lblTotalAmount.Text = "₱" + total.ToString("N2");
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (!dgvSaleItems.EndEdit()) return;

            DialogResult result = MessageBox.Show(
                "Are you sure you want to save the changes to this sale?",
                "Confirm Update",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (result != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            decimal newSubtotal = 0;

                            foreach (DataGridViewRow row in dgvSaleItems.Rows)
                            {
                                if (row.IsNewRow) continue;

                                int saleItemID = Convert.ToInt32(row.Cells["SaleItemID"].Value);
                                int productID = Convert.ToInt32(row.Cells["ProductID"].Value);
                                int newQuantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                                int originalQuantity = Convert.ToInt32(row.Cells["OriginalQuantity"].Value);
                                decimal unitPrice = Convert.ToDecimal(row.Cells["UnitPrice"].Value);
                                decimal newLineTotal = newQuantity * unitPrice;

                                int quantityDifference = newQuantity - originalQuantity;

                                if (quantityDifference != 0)
                                {
                                    string stockQuery = @"
                                        UPDATE Products
                                        SET StockQuantity = StockQuantity - @Difference
                                        WHERE ProductID = @ProductID
                                          AND StockQuantity - @Difference >= 0";

                                    using (SqlCommand stockCmd = new SqlCommand(stockQuery, conn, transaction))
                                    {
                                        stockCmd.Parameters.AddWithValue("@Difference", quantityDifference);
                                        stockCmd.Parameters.AddWithValue("@ProductID", productID);
                                        if (stockCmd.ExecuteNonQuery() == 0)
                                            throw new Exception("Insufficient stock for product ID " + productID);
                                    }
                                }

                                string itemQuery = @"
                                    UPDATE SaleItems
                                    SET Quantity = @Quantity,
                                        LineTotal = @LineTotal
                                    WHERE SaleItemID = @SaleItemID";

                                using (SqlCommand itemCmd = new SqlCommand(itemQuery, conn, transaction))
                                {
                                    itemCmd.Parameters.AddWithValue("@Quantity", newQuantity);
                                    itemCmd.Parameters.AddWithValue("@LineTotal", newLineTotal);
                                    itemCmd.Parameters.AddWithValue("@SaleItemID", saleItemID);
                                    itemCmd.ExecuteNonQuery();
                                }

                                newSubtotal += newLineTotal;
                            }

                            // Payment consistency check & recalculation
                            decimal adjustedCash = originalCashAmount >= newSubtotal ? originalCashAmount : newSubtotal;
                            decimal newChange = adjustedCash - newSubtotal;

                            string saleQuery = @"
                                UPDATE Sales
                                SET TotalAmount = @TotalAmount,
                                    CashAmount = @CashAmount,
                                    ChangeAmount = @ChangeAmount
                                WHERE SaleID = @SaleID
                                  AND (@RowVer IS NULL OR RowVer = @RowVer)";

                            using (SqlCommand saleCmd = new SqlCommand(saleQuery, conn, transaction))
                            {
                                saleCmd.Parameters.AddWithValue("@TotalAmount", newSubtotal);
                                saleCmd.Parameters.AddWithValue("@CashAmount", adjustedCash);
                                saleCmd.Parameters.AddWithValue("@ChangeAmount", newChange);
                                saleCmd.Parameters.AddWithValue("@SaleID", saleID);

                                SqlParameter rvParam = saleCmd.Parameters.Add("@RowVer", SqlDbType.Timestamp);
                                rvParam.Value = (object)saleRowVer ?? DBNull.Value;

                                if (saleCmd.ExecuteNonQuery() == 0)
                                    throw new Exception("This sale was modified or voided by another user. Please refresh.");
                            }

                            transaction.Commit();
                            LogAudit("UPDATE", "Sale", saleID);

                            MessageBox.Show("Sale updated successfully.", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadSaleItems();
                        }
                        catch (Exception txEx)
                        {
                            transaction.Rollback();
                            ErrorLogger.LogError("frmEditSale.btnSaveChanges_Click (Tx)", txEx);
                            MessageBox.Show(
                                "We couldn't update the sale.\n" + txEx.Message,
                                "Update Failed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmEditSale.btnSaveChanges_Click", ex);
                MessageBox.Show("The system couldn't complete your request. Please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LogAudit(string action, string module, int recordID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO AuditLogs (Username, Action, Module, RecordID, ActionDate)
                        VALUES (@Username, @Action, @Module, @RecordID, GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", currentUsername);
                        cmd.Parameters.AddWithValue("@Action", action);
                        cmd.Parameters.AddWithValue("@Module", module);
                        cmd.Parameters.AddWithValue("@RecordID", recordID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmEditSale.LogAudit", ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();
    }
}