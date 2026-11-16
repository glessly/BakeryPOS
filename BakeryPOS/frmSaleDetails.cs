using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BakeryPOS
{
    public partial class frmSaleDetails : Form
    {
        private int _saleID;

        public frmSaleDetails()
        {
            InitializeComponent();
            SetupGridControls();
        }

        public frmSaleDetails(int saleID) : this()
        {
            _saleID = saleID;
            LoadDetails();
        }

        private void SetupGridControls()
        {
            if (dgvItems == null) return;

            dgvItems.AutoGenerateColumns = false;
            dgvItems.Columns.Clear();

            dgvItems.RowHeadersVisible = false;
            dgvItems.AllowUserToAddRows = false;
            dgvItems.AllowUserToDeleteRows = false;
            dgvItems.AllowUserToResizeRows = false;
            dgvItems.ReadOnly = true;
            dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItems.MultiSelect = false;

            // Product Name column
            DataGridViewTextBoxColumn colProduct = new DataGridViewTextBoxColumn();
            colProduct.Name = "Product Name";
            colProduct.DataPropertyName = "Product Name";
            colProduct.HeaderText = "Product Name";
            colProduct.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvItems.Columns.Add(colProduct);

            // Quantity column
            DataGridViewTextBoxColumn colQuantity = new DataGridViewTextBoxColumn();
            colQuantity.Name = "Quantity";
            colQuantity.DataPropertyName = "Quantity";
            colQuantity.HeaderText = "Quantity";
            colQuantity.Width = 90;
            colQuantity.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItems.Columns.Add(colQuantity);

            // Unit Price column
            DataGridViewTextBoxColumn colUnitPrice = new DataGridViewTextBoxColumn();
            colUnitPrice.Name = "Unit Price";
            colUnitPrice.DataPropertyName = "Unit Price";
            colUnitPrice.HeaderText = "Unit Price";
            colUnitPrice.Width = 130;
            colUnitPrice.DefaultCellStyle.Format = "₱#,##0.00";
            colUnitPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvItems.Columns.Add(colUnitPrice);

            // Item Total column
            DataGridViewTextBoxColumn colItemTotal = new DataGridViewTextBoxColumn();
            colItemTotal.Name = "Item Total";
            colItemTotal.DataPropertyName = "Item Total";
            colItemTotal.HeaderText = "Item Total";
            colItemTotal.Width = 140;
            colItemTotal.DefaultCellStyle.Format = "₱#,##0.00";
            colItemTotal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvItems.Columns.Add(colItemTotal);

            BakeryPOS.UI.Styles.ApplyGridStyle(dgvItems);
        }

        public void LoadDetails(int saleID = 0)
        {
            if (saleID > 0)
            {
                _saleID = saleID;
            }

            if (_saleID <= 0) return;

            if (this.BindingContext == null)
            {
                this.BindingContext = new BindingContext();
            }
            if (dgvItems != null && dgvItems.BindingContext == null)
            {
                dgvItems.BindingContext = this.BindingContext;
            }

            try
            {
                string connectionString = DbHelper.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // 1. Load Sales header info (Top Details and Payment Summary)
                    string salesQuery = @"
                        SELECT SaleID, SaleDate, TotalAmount, CashAmount, ChangeAmount, CashierUsername
                        FROM Sales
                        WHERE SaleID = @SaleID";

                    using (SqlCommand cmd = new SqlCommand(salesQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@SaleID", _saleID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                if (lblSaleID != null)
                                    lblSaleID.Text = "ORD-" + Convert.ToInt32(reader["SaleID"]).ToString("D4");

                                if (lblSaleDate != null)
                                    lblSaleDate.Text = Convert.ToDateTime(reader["SaleDate"]).ToString("MMM dd, yyyy hh:mm tt");

                                object cashierObj = reader["CashierUsername"];
                                if (lblCashier != null)
                                    lblCashier.Text = (cashierObj != null && cashierObj != DBNull.Value) ? cashierObj.ToString() : "N/A";

                                if (lblTotalAmount != null)
                                    lblTotalAmount.Text = "₱" + Convert.ToDecimal(reader["TotalAmount"]).ToString("N2");

                                decimal cash = reader["CashAmount"] != DBNull.Value ? Convert.ToDecimal(reader["CashAmount"]) : 0m;
                                decimal change = reader["ChangeAmount"] != DBNull.Value ? Convert.ToDecimal(reader["ChangeAmount"]) : 0m;

                                if (lblCashPaid != null)
                                    lblCashPaid.Text = "₱" + cash.ToString("N2");

                                if (lblChange != null)
                                    lblChange.Text = "₱" + change.ToString("N2");
                            }
                        }
                    }

                    // 2. Load SaleItems detail grid (Product Name, Quantity, Unit Price, Item Total)
                    string itemsQuery = @"
                        SELECT 
                            ISNULL(p.ProductName, 'Unknown Product') AS [Product Name],
                            si.Quantity AS [Quantity],
                            si.UnitPrice AS [Unit Price],
                            si.LineTotal AS [Item Total]
                        FROM SaleItems si
                        LEFT JOIN Products p ON si.ProductID = p.ProductID
                        WHERE si.SaleID = @SaleID
                        ORDER BY si.SaleItemID ASC";

                    using (SqlCommand cmd = new SqlCommand(itemsQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@SaleID", _saleID);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            if (dgvItems != null)
                            {
                                dgvItems.DataSource = dt;

                                // Guard all column formatting with existence checks
                                if (dgvItems.Columns.Contains("Product Name") && dgvItems.Columns["Product Name"] != null)
                                {
                                    dgvItems.Columns["Product Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                                }

                                if (dgvItems.Columns.Contains("Quantity") && dgvItems.Columns["Quantity"] != null)
                                {
                                    dgvItems.Columns["Quantity"].Width = 90;
                                    if (dgvItems.Columns["Quantity"].DefaultCellStyle != null)
                                    {
                                        dgvItems.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                    }
                                }

                                if (dgvItems.Columns.Contains("Unit Price") && dgvItems.Columns["Unit Price"] != null)
                                {
                                    dgvItems.Columns["Unit Price"].Width = 130;
                                    if (dgvItems.Columns["Unit Price"].DefaultCellStyle != null)
                                    {
                                        dgvItems.Columns["Unit Price"].DefaultCellStyle.Format = "₱#,##0.00";
                                        dgvItems.Columns["Unit Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                    }
                                }

                                if (dgvItems.Columns.Contains("Item Total") && dgvItems.Columns["Item Total"] != null)
                                {
                                    dgvItems.Columns["Item Total"].Width = 140;
                                    if (dgvItems.Columns["Item Total"].DefaultCellStyle != null)
                                    {
                                        dgvItems.Columns["Item Total"].DefaultCellStyle.Format = "₱#,##0.00";
                                        dgvItems.Columns["Item Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                    }
                                }

                                BakeryPOS.UI.Styles.ApplyGridStyle(dgvItems);
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show(
                    "We couldn't load the transaction details. Please try again.",
                    "Something Went Wrong",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

