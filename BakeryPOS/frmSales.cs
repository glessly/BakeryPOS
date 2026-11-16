using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BakeryPOS
{
    public partial class frmSales : Form
    {
        private string currentUsername;

        private string GetConnectionString() => DbHelper.GetConnectionString();

        public frmSales()
        {
            InitializeComponent();
            txtSalesSearch.Text = "Search Sale ID or Cashier...";
            txtSalesSearch.ForeColor = Color.Gray;
        }

        public frmSales(string username) : this()
        {
            currentUsername = username;
        }

        private void LoadSales(string searchFilter = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    string whereClause = string.IsNullOrWhiteSpace(searchFilter)
                        ? ""
                        : "WHERE CAST(SaleID AS VARCHAR) LIKE @Search OR CashierUsername LIKE @Search OR ISNULL(Status, 'Completed') LIKE @Search";

                    string query = $@"
                        SELECT
                            SaleID AS [Sale ID],
                            SaleDate AS [Date],
                            TotalAmount AS [Total Amount],
                            CashAmount AS [Cash Paid],
                            CashierUsername AS [Cashier],
                            ISNULL(Status, 'Completed') AS [Status]
                        FROM Sales
                        {whereClause}
                        ORDER BY SaleDate DESC";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(searchFilter))
                            adapter.SelectCommand.Parameters.AddWithValue("@Search", "%" + searchFilter + "%");

                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        dgvSales.DataSource = table;
                        FormatSalesGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmSales.LoadSales", ex);
                MessageBox.Show(
                    "We couldn't load the sales records. Please try again.",
                    "Something Went Wrong",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void frmSales_Load(object sender, EventArgs e)
        {
            if (btnDeleteSale != null)
                btnDeleteSale.Text = "Void Sale";

            dgvSales.CellDoubleClick += (s, ev) =>
            {
                if (ev.RowIndex >= 0)
                    btnViewDetails_Click(s, ev);
            };
            LoadSales();
        }

        private void txtSalesSearch_Enter(object sender, EventArgs e)
        {
            if (txtSalesSearch.Text == "Search Sale ID or Cashier...")
            {
                txtSalesSearch.Text = "";
                txtSalesSearch.ForeColor = Color.Black;
            }
        }

        private void txtSalesSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSalesSearch.Text))
            {
                txtSalesSearch.Text = "Search Sale ID or Cashier...";
                txtSalesSearch.ForeColor = Color.Gray;
            }
        }

        private void txtSalesSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSalesSearch.Text == "Search Sale ID or Cashier...")
                return;

            LoadSales(txtSalesSearch.Text.Trim());
        }

        private void FormatSalesGrid()
        {
            dgvSales.RowHeadersVisible = false;
            dgvSales.ReadOnly = true;
            dgvSales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSales.MultiSelect = false;
            dgvSales.AllowUserToAddRows = false;
            dgvSales.AllowUserToDeleteRows = false;
            dgvSales.AllowUserToResizeRows = false;
            dgvSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvSales.Columns["Sale ID"] != null)
            {
                dgvSales.Columns["Sale ID"].FillWeight = 10F;
                dgvSales.Columns["Sale ID"].MinimumWidth = 60;
                dgvSales.Columns["Sale ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvSales.Columns["Date"] != null)
            {
                dgvSales.Columns["Date"].FillWeight = 24F;
                dgvSales.Columns["Date"].MinimumWidth = 155;
                dgvSales.Columns["Date"].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm:ss tt";
                dgvSales.Columns["Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvSales.Columns["Total Amount"] != null)
            {
                dgvSales.Columns["Total Amount"].FillWeight = 18F;
                dgvSales.Columns["Total Amount"].MinimumWidth = 100;
                dgvSales.Columns["Total Amount"].DefaultCellStyle.Format = "₱#,##0.00";
                dgvSales.Columns["Total Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvSales.Columns["Cash Paid"] != null)
            {
                dgvSales.Columns["Cash Paid"].FillWeight = 18F;
                dgvSales.Columns["Cash Paid"].MinimumWidth = 100;
                dgvSales.Columns["Cash Paid"].DefaultCellStyle.Format = "₱#,##0.00";
                dgvSales.Columns["Cash Paid"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvSales.Columns["Cashier"] != null)
            {
                dgvSales.Columns["Cashier"].FillWeight = 16F;
                dgvSales.Columns["Cashier"].MinimumWidth = 95;
            }
            if (dgvSales.Columns["Status"] != null)
            {
                dgvSales.Columns["Status"].FillWeight = 14F;
                dgvSales.Columns["Status"].MinimumWidth = 85;
                dgvSales.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            BakeryPOS.UI.Styles.ApplyGridStyle(dgvSales);

            foreach (DataGridViewRow row in dgvSales.Rows)
            {
                row.Height = 32;
                if (dgvSales.Columns.Contains("Status") && row.Cells["Status"].Value?.ToString() == "Voided")
                {
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(178, 58, 58);
                    row.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Strikeout);
                }
            }
        }

        private DataGridViewRow GetSelectedRow()
        {
            if (dgvSales.SelectedRows.Count > 0) return dgvSales.SelectedRows[0];
            return dgvSales.CurrentRow;
        }

        private int GetSelectedSaleID()
        {
            DataGridViewRow row = GetSelectedRow();
            if (row == null || row.Index < 0) return 0;

            string colName = dgvSales.Columns.Contains("Sale ID") ? "Sale ID" : (dgvSales.Columns.Contains("SaleID") ? "SaleID" : null);
            if (colName != null && row.Cells[colName].Value != null && int.TryParse(row.Cells[colName].Value.ToString(), out int id))
                return id;

            return 0;
        }

        private bool IsSelectedSaleVoided()
        {
            DataGridViewRow row = GetSelectedRow();
            if (row == null || !dgvSales.Columns.Contains("Status")) return false;
            return string.Equals(row.Cells["Status"].Value?.ToString(), "Voided", StringComparison.OrdinalIgnoreCase);
        }

        private void btnDeleteSale_Click(object sender, EventArgs e)
        {
            int selectedSaleID = GetSelectedSaleID();
            if (selectedSaleID == 0)
            {
                MessageBox.Show("Please select a sale first.", "Void Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (IsSelectedSaleVoided())
            {
                MessageBox.Show("This sale has already been voided.", "Already Voided", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to VOID Sale ID " + selectedSaleID + "?\n\nThe transaction will be marked as Voided and the sold items will be returned to stock.",
                "Confirm Void Sale",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1. Restore sold quantities back to Products.StockQuantity
                            string restoreStockQuery = @"
                                UPDATE p
                                SET p.StockQuantity = p.StockQuantity + si.Quantity
                                FROM Products p
                                INNER JOIN SaleItems si ON p.ProductID = si.ProductID
                                WHERE si.SaleID = @SaleID";

                            using (SqlCommand cmd = new SqlCommand(restoreStockQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@SaleID", selectedSaleID);
                                cmd.ExecuteNonQuery();
                            }

                            // 2. Soft-cancel (Void) the sale instead of physical DELETE
                            string voidSaleQuery = @"
                                UPDATE Sales
                                SET Status = 'Voided',
                                    VoidReason = 'Voided by user',
                                    VoidedBy = @VoidedBy,
                                    VoidedDate = GETDATE()
                                WHERE SaleID = @SaleID
                                  AND ISNULL(Status, 'Completed') = 'Completed'";

                            using (SqlCommand cmd = new SqlCommand(voidSaleQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@VoidedBy", currentUsername ?? "System");
                                cmd.Parameters.AddWithValue("@SaleID", selectedSaleID);
                                if (cmd.ExecuteNonQuery() == 0)
                                    throw new Exception("Sale is already voided or not found.");
                            }

                            transaction.Commit();
                            LogAudit("VOID", "Sale", selectedSaleID);

                            MessageBox.Show(
                                "Sale #" + selectedSaleID + " has been voided and product stock has been restored.",
                                "Void Successful",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );

                            LoadSales();
                        }
                        catch (Exception txEx)
                        {
                            transaction.Rollback();
                            ErrorLogger.LogError("frmSales.btnDeleteSale_Click (Tx)", txEx);
                            MessageBox.Show(
                                "We couldn't void the sale. No changes were made.",
                                "Something Went Wrong",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmSales.btnDeleteSale_Click", ex);
                MessageBox.Show(
                    "The system couldn't complete your request. Please try again.",
                    "Connection Problem",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnUpdateSale_Click(object sender, EventArgs e)
        {
            int selectedSaleID = GetSelectedSaleID();
            if (selectedSaleID == 0)
            {
                MessageBox.Show("Please select a sale first.", "Update Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (IsSelectedSaleVoided())
            {
                MessageBox.Show("Voided sales cannot be edited.", "Action Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (frmEditSale editForm = new frmEditSale(selectedSaleID, currentUsername))
            {
                editForm.ShowDialog(this);
            }
            LoadSales();
        }

        private void btnSearch_Click(object sender, EventArgs e) => txtSalesSearch_TextChanged(sender, e);

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSalesSearch.Text = "Search Sale ID or Cashier...";
            txtSalesSearch.ForeColor = Color.Gray;
            LoadSales();
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            int saleID = GetSelectedSaleID();
            if (saleID <= 0)
            {
                MessageBox.Show("Please select a sale from the list first.", "View Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (frmSaleDetails detailsForm = new frmSaleDetails(saleID))
            {
                detailsForm.ShowDialog(this);
            }
        }

        private void dgvSales_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

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
                        cmd.Parameters.AddWithValue("@Username", currentUsername ?? "System");
                        cmd.Parameters.AddWithValue("@Action", action);
                        cmd.Parameters.AddWithValue("@Module", module);
                        cmd.Parameters.AddWithValue("@RecordID", recordID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmSales.LogAudit", ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}