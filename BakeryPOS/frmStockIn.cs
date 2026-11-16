using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using BakeryPOS.UI;

namespace BakeryPOS
{
    public partial class frmStockIn : Form
    {
        private readonly string _username;
        private DataTable _cartTable;

        private TextBox txtRefNo, txtQty, txtUnitCost, txtRemarks;
        private ComboBox cmbSupplier, cmbProduct;
        private DataGridView dgvItems, dgvHistory;
        private Label lblTotalCost;
        private Button btnAddLine, btnRemoveLine, btnSaveStockIn, btnVoidStockIn;

        public frmStockIn() : this("admin") { }

        public frmStockIn(string username)
        {
            InitializeComponent();
            _username = string.IsNullOrWhiteSpace(username) ? "admin" : username;
            BuildUI();
        }

        private void BuildUI()
        {
            this.Text = "Bakery POS - Stock-In / Inventory Restock";
            this.ClientSize = new Size(1120, 630);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(253, 245, 236);

            Panel pnlHeader = Styles.CreateHeaderBanner("STOCK-IN / INVENTORY RESTOCK");

            // Left: New Stock-In Transaction
            Panel pnlLeft = new Panel { Left = 16, Top = 100, Width = 485, Height = 514, BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            Label lblTitle = new Label { Text = "New Stock-In Transaction", Left = 14, Top = 10, AutoSize = true, Font = new Font("Segoe UI", 12F, FontStyle.Bold), ForeColor = Color.FromArgb(123, 63, 26) };

            Label lRef = new Label { Text = "Reference No. *", Left = 14, Top = 42, AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(50, 22, 8) };
            txtRefNo = new TextBox { Left = 14, Top = 62, Width = 195, Font = new Font("Segoe UI", 10F, FontStyle.Regular), Text = "SI-" + DateTime.Now.ToString("yyyyMMdd-HHmm") };

            Label lSup = new Label { Text = "Supplier *", Left = 222, Top = 42, AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(50, 22, 8) };
            cmbSupplier = new ComboBox { Left = 222, Top = 62, Width = 247, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10F, FontStyle.Regular) };

            Label lProd = new Label { Text = "Product *", Left = 14, Top = 96, AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(50, 22, 8) };
            cmbProduct = new ComboBox { Left = 14, Top = 116, Width = 195, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10F, FontStyle.Regular) };

            Label lQty = new Label { Text = "Qty *", Left = 217, Top = 96, AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(50, 22, 8) };
            txtQty = new TextBox { Left = 217, Top = 116, Width = 68, Font = new Font("Segoe UI", 10F, FontStyle.Regular), Text = "10" };

            Label lCost = new Label { Text = "Unit Cost *", Left = 293, Top = 96, AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(50, 22, 8) };
            txtUnitCost = new TextBox { Left = 293, Top = 116, Width = 88, Font = new Font("Segoe UI", 10F, FontStyle.Regular), Text = "15.00" };

            btnAddLine = new Button { Text = "+ Add", Left = 389, Top = 111, Width = 80, Height = 36, BackColor = Color.FromArgb(101, 61, 34), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), Cursor = Cursors.Hand, UseVisualStyleBackColor = false };
            btnAddLine.FlatAppearance.BorderSize = 0;
            btnAddLine.Click += BtnAddLine_Click;

            _cartTable = new DataTable();
            _cartTable.Columns.Add("ProductID", typeof(int));
            _cartTable.Columns.Add("ProductName", typeof(string));
            _cartTable.Columns.Add("Quantity", typeof(int));
            _cartTable.Columns.Add("UnitCost", typeof(decimal));
            _cartTable.Columns.Add("Subtotal", typeof(decimal));

            dgvItems = new DataGridView
            {
                Left = 14,
                Top = 156,
                Width = 455,
                Height = 205,
                DataSource = _cartTable,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White
            };
            Styles.ApplyGridStyle(dgvItems);

            btnRemoveLine = new Button { Text = "Remove Selected Item", Left = 14, Top = 368, Width = 175, Height = 36, BackColor = Color.FromArgb(178, 58, 58), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), Cursor = Cursors.Hand, UseVisualStyleBackColor = false };
            btnRemoveLine.FlatAppearance.BorderSize = 0;
            btnRemoveLine.Click += (s, e) => { if (dgvItems.CurrentRow != null) dgvItems.Rows.Remove(dgvItems.CurrentRow); RecalcTotal(); };

            lblTotalCost = new Label { Text = "Total Cost: ₱0.00", Left = 240, Top = 374, Width = 229, TextAlign = ContentAlignment.MiddleRight, Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.FromArgb(123, 63, 26) };

            Label lRem = new Label { Text = "Remarks", Left = 14, Top = 410, AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(50, 22, 8) };
            txtRemarks = new TextBox { Left = 14, Top = 430, Width = 455, Font = new Font("Segoe UI", 10F, FontStyle.Regular) };

            btnSaveStockIn = new Button { Text = "COMMIT STOCK-IN TRANSACTION", Left = 14, Top = 464, Width = 455, Height = 38, BackColor = Color.FromArgb(101, 61, 34), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), Cursor = Cursors.Hand, UseVisualStyleBackColor = false };
            btnSaveStockIn.FlatAppearance.BorderSize = 0;
            btnSaveStockIn.Click += BtnSaveStockIn_Click;

            pnlLeft.Controls.AddRange(new Control[] { lblTitle, lRef, txtRefNo, lSup, cmbSupplier, lProd, cmbProduct, lQty, txtQty, lCost, txtUnitCost, btnAddLine, dgvItems, btnRemoveLine, lblTotalCost, lRem, txtRemarks, btnSaveStockIn });

            // Right: Stock-In History & Void
            Label lblHist = new Label { Text = "Stock-In History", Left = 516, Top = 102, AutoSize = true, Font = new Font("Segoe UI", 12F, FontStyle.Bold), ForeColor = Color.FromArgb(123, 63, 26) };

            dgvHistory = new DataGridView
            {
                Left = 516,
                Top = 132,
                Width = 588,
                Height = 434,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White
            };
            Styles.ApplyGridStyle(dgvHistory);

            btnVoidStockIn = new Button { Text = "Void Selected Stock-In", Left = 516, Top = 578, Width = 195, Height = 36, BackColor = Color.FromArgb(178, 58, 58), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), Cursor = Cursors.Hand, UseVisualStyleBackColor = false };
            btnVoidStockIn.FlatAppearance.BorderSize = 0;
            btnVoidStockIn.Click += BtnVoidStockIn_Click;

            this.Controls.AddRange(new Control[] { pnlLeft, lblHist, dgvHistory, btnVoidStockIn, pnlHeader });
            this.Load += (s, e) =>
            {
                LoadDropdowns();
                LoadHistory();
                FormatItemsGrid();
            };
        }

        private void FormatItemsGrid()
        {
            if (dgvItems == null) return;
            Styles.ApplyGridStyle(dgvItems);
            dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (dgvItems.Columns["ProductID"] != null) dgvItems.Columns["ProductID"].Visible = false;
            if (dgvItems.Columns["ProductName"] != null)
            {
                dgvItems.Columns["ProductName"].HeaderText = "Product";
                dgvItems.Columns["ProductName"].FillWeight = 42F;
                dgvItems.Columns["ProductName"].MinimumWidth = 130;
            }
            if (dgvItems.Columns["Quantity"] != null)
            {
                dgvItems.Columns["Quantity"].HeaderText = "Qty";
                dgvItems.Columns["Quantity"].FillWeight = 16F;
                dgvItems.Columns["Quantity"].MinimumWidth = 55;
                dgvItems.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvItems.Columns["UnitCost"] != null)
            {
                dgvItems.Columns["UnitCost"].HeaderText = "Unit Cost";
                dgvItems.Columns["UnitCost"].FillWeight = 21F;
                dgvItems.Columns["UnitCost"].MinimumWidth = 80;
                dgvItems.Columns["UnitCost"].DefaultCellStyle.Format = "₱#,##0.00";
                dgvItems.Columns["UnitCost"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvItems.Columns["Subtotal"] != null)
            {
                dgvItems.Columns["Subtotal"].HeaderText = "Subtotal";
                dgvItems.Columns["Subtotal"].FillWeight = 21F;
                dgvItems.Columns["Subtotal"].MinimumWidth = 85;
                dgvItems.Columns["Subtotal"].DefaultCellStyle.Format = "₱#,##0.00";
                dgvItems.Columns["Subtotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void LoadDropdowns()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();
                    using (SqlDataAdapter daSup = new SqlDataAdapter("SELECT SupplierID, SupplierName FROM Suppliers WHERE IsActive = 1 ORDER BY SupplierName", conn))
                    {
                        DataTable dtSup = new DataTable();
                        daSup.Fill(dtSup);
                        cmbSupplier.DataSource = dtSup;
                        cmbSupplier.DisplayMember = "SupplierName";
                        cmbSupplier.ValueMember = "SupplierID";
                    }
                    using (SqlDataAdapter daProd = new SqlDataAdapter("SELECT ProductID, ProductName FROM Products WHERE IsActive = 1 ORDER BY ProductName", conn))
                    {
                        DataTable dtProd = new DataTable();
                        daProd.Fill(dtProd);
                        cmbProduct.DataSource = dtProd;
                        cmbProduct.DisplayMember = "ProductName";
                        cmbProduct.ValueMember = "ProductID";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmStockIn.LoadDropdowns", ex);
            }
        }

        private void LoadHistory()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();
                    string sql = @"
                        SELECT si.StockInID, si.ReferenceNo, s.SupplierName, si.TotalCost, si.Status, si.StockInDate
                        FROM StockIns si
                        INNER JOIN Suppliers s ON si.SupplierID = s.SupplierID
                        ORDER BY si.StockInID DESC";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvHistory.DataSource = dt;
                        dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        if (dgvHistory.Columns["StockInID"] != null)
                        {
                            dgvHistory.Columns["StockInID"].HeaderText = "StockInID";
                            dgvHistory.Columns["StockInID"].FillWeight = 12F;
                            dgvHistory.Columns["StockInID"].MinimumWidth = 75;
                            dgvHistory.Columns["StockInID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        if (dgvHistory.Columns["ReferenceNo"] != null)
                        {
                            dgvHistory.Columns["ReferenceNo"].HeaderText = "ReferenceNo";
                            dgvHistory.Columns["ReferenceNo"].FillWeight = 22F;
                            dgvHistory.Columns["ReferenceNo"].MinimumWidth = 115;
                        }
                        if (dgvHistory.Columns["SupplierName"] != null)
                        {
                            dgvHistory.Columns["SupplierName"].HeaderText = "SupplierName";
                            dgvHistory.Columns["SupplierName"].FillWeight = 21F;
                            dgvHistory.Columns["SupplierName"].MinimumWidth = 110;
                        }
                        if (dgvHistory.Columns["TotalCost"] != null)
                        {
                            dgvHistory.Columns["TotalCost"].HeaderText = "TotalCost";
                            dgvHistory.Columns["TotalCost"].FillWeight = 15F;
                            dgvHistory.Columns["TotalCost"].MinimumWidth = 85;
                            dgvHistory.Columns["TotalCost"].DefaultCellStyle.Format = "₱#,##0.00";
                            dgvHistory.Columns["TotalCost"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        }
                        if (dgvHistory.Columns["Status"] != null)
                        {
                            dgvHistory.Columns["Status"].HeaderText = "Status";
                            dgvHistory.Columns["Status"].FillWeight = 12F;
                            dgvHistory.Columns["Status"].MinimumWidth = 70;
                            dgvHistory.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        if (dgvHistory.Columns["StockInDate"] != null)
                        {
                            dgvHistory.Columns["StockInDate"].HeaderText = "StockInDate";
                            dgvHistory.Columns["StockInDate"].FillWeight = 18F;
                            dgvHistory.Columns["StockInDate"].MinimumWidth = 110;
                            dgvHistory.Columns["StockInDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                        }

                        Styles.ApplyGridStyle(dgvHistory);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmStockIn.LoadHistory", ex);
            }
        }

        private void BtnAddLine_Click(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedValue == null)
            {
                MessageBox.Show("Select a product first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtQty.Text.Trim(), out int qty) || qty <= 0)
            {
                MessageBox.Show("Quantity must be greater than 0.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txtUnitCost.Text.Trim(), out decimal cost) || cost <= 0)
            {
                MessageBox.Show("Unit Cost must be greater than 0.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int prodId = Convert.ToInt32(cmbProduct.SelectedValue);
            string prodName = cmbProduct.Text;

            foreach (DataRow row in _cartTable.Rows)
            {
                if (Convert.ToInt32(row["ProductID"]) == prodId)
                {
                    int newQty = Convert.ToInt32(row["Quantity"]) + qty;
                    row["Quantity"] = newQty;
                    row["UnitCost"] = cost;
                    row["Subtotal"] = newQty * cost;
                    RecalcTotal();
                    return;
                }
            }

            _cartTable.Rows.Add(prodId, prodName, qty, cost, qty * cost);
            RecalcTotal();
        }

        private void RecalcTotal()
        {
            decimal total = 0;
            foreach (DataRow row in _cartTable.Rows)
                total += Convert.ToDecimal(row["Subtotal"]);
            lblTotalCost.Text = "Total Cost: ₱" + total.ToString("N2");
        }

        private void BtnSaveStockIn_Click(object sender, EventArgs e)
        {
            string refNo = txtRefNo.Text.Trim();
            if (string.IsNullOrWhiteSpace(refNo) || cmbSupplier.SelectedValue == null || _cartTable.Rows.Count == 0)
            {
                MessageBox.Show("Reference No., Supplier, and at least one item are required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Commit this Stock-In transaction and add quantities to inventory?", "Confirm Stock-In", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            decimal totalCost = 0;
            foreach (DataRow row in _cartTable.Rows)
                totalCost += Convert.ToDecimal(row["Subtotal"]);

            try
            {
                using (SqlConnection conn = new SqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();
                    using (SqlTransaction tx = conn.BeginTransaction())
                    {
                        try
                        {
                            int stockInId;
                            using (SqlCommand hdrCmd = new SqlCommand(@"
                                INSERT INTO StockIns (ReferenceNo, SupplierID, ReceivedBy, StockInDate, TotalCost, Remarks, Status)
                                VALUES (@Ref, @Sup, @User, GETDATE(), @Total, @Rem, 'Completed');
                                SELECT SCOPE_IDENTITY();", conn, tx))
                            {
                                hdrCmd.Parameters.AddWithValue("@Ref", refNo);
                                hdrCmd.Parameters.AddWithValue("@Sup", Convert.ToInt32(cmbSupplier.SelectedValue));
                                hdrCmd.Parameters.AddWithValue("@User", _username);
                                hdrCmd.Parameters.AddWithValue("@Total", totalCost);
                                hdrCmd.Parameters.AddWithValue("@Rem", string.IsNullOrWhiteSpace(txtRemarks.Text) ? (object)DBNull.Value : txtRemarks.Text.Trim());
                                stockInId = Convert.ToInt32(hdrCmd.ExecuteScalar());
                            }

                            foreach (DataRow row in _cartTable.Rows)
                            {
                                int prodId = Convert.ToInt32(row["ProductID"]);
                                int qty = Convert.ToInt32(row["Quantity"]);
                                decimal cost = Convert.ToDecimal(row["UnitCost"]);

                                using (SqlCommand detCmd = new SqlCommand(@"
                                    INSERT INTO StockInItems (StockInID, ProductID, Quantity, UnitCost)
                                    VALUES (@SI, @Prod, @Qty, @Cost)", conn, tx))
                                {
                                    detCmd.Parameters.AddWithValue("@SI", stockInId);
                                    detCmd.Parameters.AddWithValue("@Prod", prodId);
                                    detCmd.Parameters.AddWithValue("@Qty", qty);
                                    detCmd.Parameters.AddWithValue("@Cost", cost);
                                    detCmd.ExecuteNonQuery();
                                }

                                using (SqlCommand stkCmd = new SqlCommand("UPDATE Products SET StockQuantity = StockQuantity + @Qty WHERE ProductID = @Prod", conn, tx))
                                {
                                    stkCmd.Parameters.AddWithValue("@Qty", qty);
                                    stkCmd.Parameters.AddWithValue("@Prod", prodId);
                                    stkCmd.ExecuteNonQuery();
                                }
                            }

                            tx.Commit();
                            LogAudit(conn, "ADD", stockInId);
                        }
                        catch (Exception txEx)
                        {
                            tx.Rollback();
                            ErrorLogger.LogError("frmStockIn.BtnSaveStockIn_Click (Tx)", txEx);
                            MessageBox.Show("Transaction failed and was rolled back.\n" + txEx.Message, "Rollback", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                MessageBox.Show("Stock-In recorded and inventory updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _cartTable.Rows.Clear();
                RecalcTotal();
                txtRefNo.Text = "SI-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
                txtRemarks.Clear();
                LoadHistory();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmStockIn.BtnSaveStockIn_Click", ex);
                MessageBox.Show("Could not save Stock-In transaction.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnVoidStockIn_Click(object sender, EventArgs e)
        {
            if (dgvHistory.CurrentRow == null) return;
            int stockInId = Convert.ToInt32(dgvHistory.CurrentRow.Cells["StockInID"].Value);
            string status = dgvHistory.CurrentRow.Cells["Status"].Value.ToString();

            if (status == "Voided")
            {
                MessageBox.Show("This Stock-In transaction is already voided.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"Void Stock-In #{stockInId} and deduct its items back from stock?", "Confirm Void", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();
                    using (SqlTransaction tx = conn.BeginTransaction())
                    {
                        try
                        {
                            DataTable items = new DataTable();
                            using (SqlDataAdapter da = new SqlDataAdapter("SELECT ProductID, Quantity FROM StockInItems WHERE StockInID = @ID", conn))
                            {
                                da.SelectCommand.Transaction = tx;
                                da.SelectCommand.Parameters.AddWithValue("@ID", stockInId);
                                da.Fill(items);
                            }

                            foreach (DataRow r in items.Rows)
                            {
                                int prodId = Convert.ToInt32(r["ProductID"]);
                                int qty = Convert.ToInt32(r["Quantity"]);
                                using (SqlCommand upd = new SqlCommand("UPDATE Products SET StockQuantity = StockQuantity - @Qty WHERE ProductID = @Prod AND StockQuantity >= @Qty", conn, tx))
                                {
                                    upd.Parameters.AddWithValue("@Qty", qty);
                                    upd.Parameters.AddWithValue("@Prod", prodId);
                                    if (upd.ExecuteNonQuery() == 0)
                                        throw new Exception("Cannot void: current product stock is lower than the Stock-In quantity.");
                                }
                            }

                            using (SqlCommand voidCmd = new SqlCommand("UPDATE StockIns SET Status = 'Voided' WHERE StockInID = @ID", conn, tx))
                            {
                                voidCmd.Parameters.AddWithValue("@ID", stockInId);
                                voidCmd.ExecuteNonQuery();
                            }

                            tx.Commit();
                            LogAudit(conn, "VOID", stockInId);
                        }
                        catch (Exception txEx)
                        {
                            tx.Rollback();
                            ErrorLogger.LogError("frmStockIn.BtnVoidStockIn_Click (Tx)", txEx);
                            MessageBox.Show("Void failed and was rolled back.\n" + txEx.Message, "Rollback", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                MessageBox.Show("Stock-In voided and inventory adjusted.", "Voided", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadHistory();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmStockIn.BtnVoidStockIn_Click", ex);
                MessageBox.Show("Could not void Stock-In.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LogAudit(SqlConnection conn, string action, int id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO AuditLogs (Username, Action, Module, RecordID, ActionDate) VALUES (@U, @A, 'StockIn', @R, GETDATE())", conn))
                {
                    cmd.Parameters.AddWithValue("@U", _username);
                    cmd.Parameters.AddWithValue("@A", action);
                    cmd.Parameters.AddWithValue("@R", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch { }
        }
    }
}