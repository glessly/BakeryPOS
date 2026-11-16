using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BakeryPOS
{
    public partial class frmProducts : Form
    {
        private int selectedProductID = 0;
        private byte[] selectedProductRowVer = null;
        private string currentUsername;
        private string selectedSourceImagePath = null;
        private string currentRelativeImagePath = null;

        // Runtime controls for Foreign Keys (Category, Supplier) and Restore
        private ComboBox cmbCategory;
        private ComboBox cmbSupplier;
        private Button btnRestoreProduct;

        public frmProducts(string username)
        {
            InitializeComponent();
            currentUsername = username;
        }

        private string GetConnectionString() => DbHelper.GetConnectionString();

        private void InitializeRelationControls()
        {
            if (cmbCategory != null || txtStock == null || txtStock.Parent == null)
                return;

            Control parent = txtStock.Parent;

            Label lblCat = new Label
            {
                Text = "Category:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 22, 8),
                AutoSize = true,
                Location = new Point(16, 194)
            };

            cmbCategory = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                Location = new Point(102, 190),
                Width = 258
            };

            Label lblSup = new Label
            {
                Text = "Supplier:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 22, 8),
                AutoSize = true,
                Location = new Point(16, 228)
            };

            cmbSupplier = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                Location = new Point(102, 224),
                Width = 258
            };

            btnRestoreProduct = new Button
            {
                Text = "Restore Selected",
                BackColor = Color.FromArgb(184, 115, 51),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Size = new Size(176, 36),
                Location = new Point(184, 358),
                Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false
            };
            btnRestoreProduct.FlatAppearance.BorderSize = 0;
            btnRestoreProduct.Click += BtnRestoreProduct_Click;

            parent.Controls.Add(lblCat);
            parent.Controls.Add(cmbCategory);
            parent.Controls.Add(lblSup);
            parent.Controls.Add(cmbSupplier);
            parent.Controls.Add(btnRestoreProduct);
        }

        private void LoadCategoryAndSupplierDropdowns()
        {
            if (cmbCategory == null || cmbSupplier == null) return;
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    using (SqlDataAdapter daCat = new SqlDataAdapter("SELECT CategoryID, CategoryName FROM Categories WHERE IsActive = 1 ORDER BY CategoryName", conn))
                    {
                        DataTable dtCat = new DataTable();
                        daCat.Fill(dtCat);
                        DataRow noneRow = dtCat.NewRow();
                        noneRow["CategoryID"] = DBNull.Value;
                        noneRow["CategoryName"] = "-- Unassigned --";
                        dtCat.Rows.InsertAt(noneRow, 0);

                        cmbCategory.DataSource = dtCat;
                        cmbCategory.DisplayMember = "CategoryName";
                        cmbCategory.ValueMember = "CategoryID";
                    }

                    using (SqlDataAdapter daSup = new SqlDataAdapter("SELECT SupplierID, SupplierName FROM Suppliers WHERE IsActive = 1 ORDER BY SupplierName", conn))
                    {
                        DataTable dtSup = new DataTable();
                        daSup.Fill(dtSup);
                        DataRow noneRow = dtSup.NewRow();
                        noneRow["SupplierID"] = DBNull.Value;
                        noneRow["SupplierName"] = "-- Unassigned --";
                        dtSup.Rows.InsertAt(noneRow, 0);

                        cmbSupplier.DataSource = dtSup;
                        cmbSupplier.DisplayMember = "SupplierName";
                        cmbSupplier.ValueMember = "SupplierID";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmProducts.LoadCategoryAndSupplierDropdowns", ex);
            }
        }

        private void FormatProductsGrid()
        {
            if (dgvProducts.Columns["ImagePath"] != null) dgvProducts.Columns["ImagePath"].Visible = false;
            if (dgvProducts.Columns["CategoryID"] != null) dgvProducts.Columns["CategoryID"].Visible = false;
            if (dgvProducts.Columns["SupplierID"] != null) dgvProducts.Columns["SupplierID"].Visible = false;
            if (dgvProducts.Columns["RowVer"] != null) dgvProducts.Columns["RowVer"].Visible = false;

            if (dgvProducts.Columns["ProductID"] != null)
            {
                dgvProducts.Columns["ProductID"].HeaderText = "ID";
                dgvProducts.Columns["ProductID"].Width = 55;
                dgvProducts.Columns["ProductID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvProducts.Columns["ProductName"] != null)
            {
                dgvProducts.Columns["ProductName"].HeaderText = "Product Name";
                dgvProducts.Columns["ProductName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvProducts.Columns["ProductName"].MinimumWidth = 130;
            }
            if (dgvProducts.Columns["CategoryName"] != null)
            {
                dgvProducts.Columns["CategoryName"].HeaderText = "Category";
                dgvProducts.Columns["CategoryName"].Width = 105;
            }
            if (dgvProducts.Columns["SupplierName"] != null)
            {
                dgvProducts.Columns["SupplierName"].HeaderText = "Supplier";
                dgvProducts.Columns["SupplierName"].Width = 110;
            }
            if (dgvProducts.Columns["Price"] != null)
            {
                dgvProducts.Columns["Price"].HeaderText = "Price";
                dgvProducts.Columns["Price"].Width = 95;
                dgvProducts.Columns["Price"].DefaultCellStyle.Format = "₱#,##0.00";
                dgvProducts.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvProducts.Columns["StockQuantity"] != null)
            {
                dgvProducts.Columns["StockQuantity"].HeaderText = "Stock";
                dgvProducts.Columns["StockQuantity"].Width = 75;
                dgvProducts.Columns["StockQuantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvProducts.Columns["IsActive"] != null)
            {
                dgvProducts.Columns["IsActive"].HeaderText = "Active";
                dgvProducts.Columns["IsActive"].Width = 65;
                dgvProducts.Columns["IsActive"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dgvProducts.ReadOnly = true;
            BakeryPOS.UI.Styles.ApplyGridStyle(dgvProducts);
        }

        private void LoadProducts()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    string query = @"
                        SELECT p.ProductID, p.ProductName,
                               ISNULL(c.CategoryName, 'Unassigned') AS CategoryName,
                               ISNULL(s.SupplierName, 'Unassigned') AS SupplierName,
                               p.Price, p.StockQuantity, p.IsActive, p.ImagePath,
                               p.CategoryID, p.SupplierID, p.RowVer
                        FROM Products p
                        LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                        LEFT JOIN Suppliers s ON p.SupplierID = s.SupplierID
                        ORDER BY p.ProductID";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        dgvProducts.DataSource = table;
                        FormatProductsGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmProducts.LoadProducts", ex);
                MessageBox.Show("We couldn't load the products. Please try again.", "Something Went Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmProducts_Load(object sender, EventArgs e)
        {
            InitializeRelationControls();
            LoadCategoryAndSupplierDropdowns();
            LoadProducts();
        }

        private void pnlProductDetails_Paint(object sender, PaintEventArgs e) { }
        private void picLogo_Click(object sender, EventArgs e) { }
        private void txtProductName_TextChanged(object sender, EventArgs e) { }
        private void lblProductName_Click(object sender, EventArgs e) { }
        private void txtStock_TextChanged(object sender, EventArgs e) { }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp|All Files (*.*)|*.*";
                ofd.Title = "Select Product Image";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedSourceImagePath = ofd.FileName;
                    try
                    {
                        using (var img = Image.FromFile(selectedSourceImagePath))
                        {
                            picPreview.Image = new Bitmap(img);
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError("frmProducts.btnSelectImage_Click", ex);
                        MessageBox.Show("Unable to preview the selected image.", "Image Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private string SaveImageFileSafely(string sourceFilePath)
        {
            if (string.IsNullOrWhiteSpace(sourceFilePath) || !File.Exists(sourceFilePath))
                return null;

            try
            {
                string targetFolder = DbHelper.GetProductImagesFolder();
                string ext = Path.GetExtension(sourceFilePath);
                string fileName = "prod_" + DateTime.Now.ToString("yyyyMMdd_HHmmssfff") + ext;
                string destPath = Path.Combine(targetFolder, fileName);
                File.Copy(sourceFilePath, destPath, true);
                return Path.Combine("ProductImages", fileName);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmProducts.SaveImageFileSafely", ex);
                return null;
            }
        }

        private bool IsProductNameDuplicate(string productName, int excludeProductID = 0)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = excludeProductID > 0
                        ? "SELECT COUNT(*) FROM Products WHERE LOWER(LTRIM(RTRIM(ProductName))) = LOWER(LTRIM(RTRIM(@ProductName))) AND ProductID <> @ProductID"
                        : "SELECT COUNT(*) FROM Products WHERE LOWER(LTRIM(RTRIM(ProductName))) = LOWER(LTRIM(RTRIM(@ProductName)))";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductName", productName.Trim());
                        if (excludeProductID > 0)
                            cmd.Parameters.AddWithValue("@ProductID", excludeProductID);
                        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmProducts.IsProductNameDuplicate", ex);
                return false;
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Please enter the product name.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductName.Focus();
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Please enter a valid product price greater than 0.", "Invalid Price", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return;
            }

            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
            {
                MessageBox.Show("Please enter a valid non-negative stock quantity.", "Invalid Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStock.Focus();
                return;
            }

            string trimmedName = txtProductName.Text.Trim();
            if (IsProductNameDuplicate(trimmedName))
            {
                MessageBox.Show("A product with this name already exists.", "Duplicate Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductName.Focus();
                return;
            }

            if (MessageBox.Show($"Add new product '{trimmedName}'?", "Confirm Add Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            string savedRelativeImagePath = SaveImageFileSafely(selectedSourceImagePath);
            object catVal = (cmbCategory != null && cmbCategory.SelectedValue != null && cmbCategory.SelectedValue != DBNull.Value) ? cmbCategory.SelectedValue : DBNull.Value;
            object supVal = (cmbSupplier != null && cmbSupplier.SelectedValue != null && cmbSupplier.SelectedValue != DBNull.Value) ? cmbSupplier.SelectedValue : DBNull.Value;

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO Products (ProductName, CategoryID, SupplierID, Price, StockQuantity, IsActive, ImagePath)
                        VALUES (@ProductName, @CategoryID, @SupplierID, @Price, @StockQuantity, @IsActive, @ImagePath);
                        SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductName", trimmedName);
                        cmd.Parameters.AddWithValue("@CategoryID", catVal);
                        cmd.Parameters.AddWithValue("@SupplierID", supVal);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@StockQuantity", stock);
                        cmd.Parameters.AddWithValue("@IsActive", chkIsActive.Checked);
                        cmd.Parameters.AddWithValue("@ImagePath", (object)savedRelativeImagePath ?? DBNull.Value);

                        int newProductID = Convert.ToInt32(cmd.ExecuteScalar());
                        LogAudit("ADD", "Product", newProductID);
                    }
                }

                MessageBox.Show("Product added successfully!", "Add Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                LoadProducts();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmProducts.btnAddProduct_Click", ex);
                MessageBox.Show("We couldn't add the product. Please try again.", "Something Went Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProductFromRow(DataGridViewRow row)
        {
            if (row == null || row.Index < 0) return;
            if (row.Cells["ProductID"] == null || row.Cells["ProductID"].Value == null || row.Cells["ProductID"].Value == DBNull.Value) return;

            try
            {
                selectedProductID = Convert.ToInt32(row.Cells["ProductID"].Value);
                selectedProductRowVer = row.Cells["RowVer"].Value != DBNull.Value ? (byte[])row.Cells["RowVer"].Value : null;

                txtProductName.Text = row.Cells["ProductName"].Value?.ToString() ?? "";
                txtPrice.Text = row.Cells["Price"].Value?.ToString() ?? "0";
                txtStock.Text = row.Cells["StockQuantity"].Value?.ToString() ?? "0";
                chkIsActive.Checked = row.Cells["IsActive"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["IsActive"].Value);

                if (cmbCategory != null)
                    cmbCategory.SelectedValue = row.Cells["CategoryID"].Value != DBNull.Value ? row.Cells["CategoryID"].Value : DBNull.Value;
                if (cmbSupplier != null)
                    cmbSupplier.SelectedValue = row.Cells["SupplierID"].Value != DBNull.Value ? row.Cells["SupplierID"].Value : DBNull.Value;

                currentRelativeImagePath = row.Cells["ImagePath"].Value != DBNull.Value ? row.Cells["ImagePath"].Value.ToString() : null;
                selectedSourceImagePath = null;

                if (picPreview.Image != null)
                {
                    picPreview.Image.Dispose();
                    picPreview.Image = null;
                }
                picPreview.Image = DbHelper.LoadProductImage(currentRelativeImagePath);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmProducts.LoadProductFromRow", ex);
            }
        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvProducts.Rows.Count)
                LoadProductFromRow(dgvProducts.Rows[e.RowIndex]);
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvProducts.Rows.Count)
                LoadProductFromRow(dgvProducts.Rows[e.RowIndex]);
        }

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null || selectedProductID == 0)
            {
                MessageBox.Show("Please select a product to update.", "Update Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Please enter the product name.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Please enter a valid product price greater than 0.", "Invalid Price", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
            {
                MessageBox.Show("Please enter a valid stock quantity.", "Invalid Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string trimmedName = txtProductName.Text.Trim();
            if (IsProductNameDuplicate(trimmedName, selectedProductID))
            {
                MessageBox.Show("A product with this name already exists.", "Duplicate Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductName.Focus();
                return;
            }

            if (MessageBox.Show("Are you sure you want to update this product?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            string finalImagePath = currentRelativeImagePath;
            if (!string.IsNullOrWhiteSpace(selectedSourceImagePath))
                finalImagePath = SaveImageFileSafely(selectedSourceImagePath);

            object catVal = (cmbCategory != null && cmbCategory.SelectedValue != null && cmbCategory.SelectedValue != DBNull.Value) ? cmbCategory.SelectedValue : DBNull.Value;
            object supVal = (cmbSupplier != null && cmbSupplier.SelectedValue != null && cmbSupplier.SelectedValue != DBNull.Value) ? cmbSupplier.SelectedValue : DBNull.Value;

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        UPDATE Products
                        SET ProductName = @ProductName,
                            CategoryID = @CategoryID,
                            SupplierID = @SupplierID,
                            Price = @Price,
                            StockQuantity = @StockQuantity,
                            IsActive = @IsActive,
                            ImagePath = @ImagePath
                        WHERE ProductID = @ProductID
                          AND (@RowVer IS NULL OR RowVer = @RowVer)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductName", trimmedName);
                        cmd.Parameters.AddWithValue("@CategoryID", catVal);
                        cmd.Parameters.AddWithValue("@SupplierID", supVal);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@StockQuantity", stock);
                        cmd.Parameters.AddWithValue("@IsActive", chkIsActive.Checked);
                        cmd.Parameters.AddWithValue("@ImagePath", (object)finalImagePath ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ProductID", selectedProductID);

                        SqlParameter rvParam = cmd.Parameters.Add("@RowVer", SqlDbType.Timestamp);
                        rvParam.Value = (object)selectedProductRowVer ?? DBNull.Value;

                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            MessageBox.Show(
                                "This product was modified by another user while you were editing it. Refreshing the list.",
                                "Concurrency Conflict",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                            LoadProducts();
                            return;
                        }

                        LogAudit("UPDATE", "Product", selectedProductID);
                    }
                }

                MessageBox.Show("Product updated successfully!", "Update Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                LoadProducts();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmProducts.btnUpdateProduct_Click", ex);
                MessageBox.Show("We couldn't update the product. Please try again.", "Something Went Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (selectedProductID == 0)
            {
                MessageBox.Show("Please select a product to archive/delete.", "Archive Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to archive (soft-delete) this product?", "Confirm Archive", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    using (SqlCommand deactCmd = new SqlCommand("UPDATE Products SET IsActive = 0 WHERE ProductID = @ProductID", conn))
                    {
                        deactCmd.Parameters.AddWithValue("@ProductID", selectedProductID);
                        deactCmd.ExecuteNonQuery();
                    }

                    LogAudit("DEACTIVATE", "Product", selectedProductID);
                }

                MessageBox.Show("Product archived (IsActive = 0). You can restore it anytime using 'Restore Selected'.", "Product Archived", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                LoadProducts();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmProducts.btnDeleteProduct_Click", ex);
                MessageBox.Show("The system couldn't complete your request. Please try again.", "Connection Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRestoreProduct_Click(object sender, EventArgs e)
        {
            if (selectedProductID == 0)
            {
                MessageBox.Show("Please select an archived product from the list to restore.", "Restore Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Restore this archived product back to active status?", "Confirm Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Products SET IsActive = 1 WHERE ProductID = @ProductID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", selectedProductID);
                        cmd.ExecuteNonQuery();
                    }
                    LogAudit("RESTORE", "Product", selectedProductID);
                }

                MessageBox.Show("Product restored successfully!", "Restored", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                LoadProducts();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmProducts.BtnRestoreProduct_Click", ex);
                MessageBox.Show("Could not restore product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearProduct_Click(object sender, EventArgs e)
        {
            ClearFields();
            txtProductName.Focus();
        }

        private void ClearFields()
        {
            selectedProductID = 0;
            selectedProductRowVer = null;
            selectedSourceImagePath = null;
            currentRelativeImagePath = null;
            txtProductName.Clear();
            txtPrice.Clear();
            txtStock.Clear();
            chkIsActive.Checked = true;
            if (cmbCategory != null && cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;
            if (cmbSupplier != null && cmbSupplier.Items.Count > 0) cmbSupplier.SelectedIndex = 0;

            if (picPreview.Image != null)
            {
                picPreview.Image.Dispose();
                picPreview.Image = null;
            }
        }

        private void txtProductSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    string query = @"
                        SELECT p.ProductID, p.ProductName,
                               ISNULL(c.CategoryName, 'Unassigned') AS CategoryName,
                               ISNULL(s.SupplierName, 'Unassigned') AS SupplierName,
                               p.Price, p.StockQuantity, p.IsActive, p.ImagePath,
                               p.CategoryID, p.SupplierID, p.RowVer
                        FROM Products p
                        LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                        LEFT JOIN Suppliers s ON p.SupplierID = s.SupplierID
                        WHERE p.ProductName LIKE @Search
                           OR ISNULL(c.CategoryName, '') LIKE @Search
                           OR ISNULL(s.SupplierName, '') LIKE @Search
                        ORDER BY p.ProductID";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@Search", "%" + txtProductSearch.Text.Trim() + "%");
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        dgvProducts.DataSource = table;
                        FormatProductsGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmProducts.txtProductSearch_TextChanged", ex);
                MessageBox.Show("We couldn't search the products. Please try again.", "Something Went Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                ErrorLogger.LogError("frmProducts.LogAudit", ex);
            }
        }
    }
}