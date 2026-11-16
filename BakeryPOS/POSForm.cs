using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BakeryPOS
{
    public partial class frmPOS : Form
    {
        private class ProductCardItem
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public int StockQuantity { get; set; }
            public string ImagePath { get; set; }
            public int Quantity { get; set; }
            public Panel CardPanel { get; set; }
            public TextBox QtyInput { get; set; }
            public Label QtyLabel { get; set; }
            public Label StockLabel { get; set; }
            public Button PlusButton { get; set; }
            public Button MinusButton { get; set; }
        }

        private Dictionary<int, ProductCardItem> _productItems = new Dictionary<int, ProductCardItem>();

        private string currentName = "Cashier";
        private string currentUsername = "cashier1";
        private string currentAccessLevel = "Cashier";

        private bool transactionCompleted = false;
        private int lastSaleID = 0;
        private int lastCompletedSaleID = 0;
        private decimal lastCash = 0;
        private decimal lastChange = 0;
        private DateTime _previouslyDisplayedDate = DateTime.Today;

        // Runtime navigation buttons for added Master, Transaction, & Admin Backup modules
        private Panel pnlExtraModules;
        private Button btnCategoriesNav;
        private Button btnSuppliersNav;
        private Button btnStockInNav;
        private Button btnBackupRestoreNav;

        private bool IsAdmin => !string.IsNullOrWhiteSpace(currentAccessLevel) &&
                                currentAccessLevel.Trim().Equals("Admin", StringComparison.OrdinalIgnoreCase);

        public frmPOS()
        {
            InitializeComponent();
            this.KeyPreview = true;
            ApplyUserSessionUI();
        }

        public frmPOS(string nameOfUser, string username, string accessLevel)
            : this()
        {
            currentName = nameOfUser;
            currentUsername = username;
            currentAccessLevel = accessLevel;
            ApplyUserSessionUI();
        }

        private void ApplyUserSessionUI()
        {
            string displayRole = IsAdmin ? "Admin" : "Cashier";
            string displayUsername = !string.IsNullOrWhiteSpace(currentUsername) ? currentUsername : "Cashier";

            if (lblRole != null)
            {
                lblRole.Text = $"Logged in: {displayUsername} ({displayRole})";
            }
            if (lblReceiptCashier != null)
            {
                lblReceiptCashier.Text = "Cashier: " + displayUsername;
            }
            if (btnManageAccounts != null)
            {
                btnManageAccounts.Visible = IsAdmin;
                btnManageAccounts.Enabled = IsAdmin;
            }
            if (btnAuditLogs != null)
            {
                btnAuditLogs.Visible = IsAdmin;
                btnAuditLogs.Enabled = IsAdmin;
            }
            if (btnBackupRestoreNav != null)
            {
                btnBackupRestoreNav.Visible = IsAdmin;
                btnBackupRestoreNav.Enabled = IsAdmin;
            }

            if (!IsAdmin && btnSalesHistory != null && btnReceipt != null && btnLogout != null && btnAuditLogs != null)
            {
                int totalWidth = btnLogout.Right - btnSalesHistory.Left;
                int gap = 14;
                int buttonWidth = (totalWidth - (gap * 2)) / 3;

                btnSalesHistory.Width = buttonWidth;
                btnReceipt.Left = btnSalesHistory.Right + gap;
                btnReceipt.Width = buttonWidth;
                btnLogout.Left = btnReceipt.Right + gap;
                btnLogout.Width = totalWidth - (buttonWidth * 2) - (gap * 2);
            }
        }

        private string GetConnectionString()
        {
            return DbHelper.GetConnectionString();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                txtSearch.Focus();
                txtSearch.SelectAll();
                return true;
            }
            else if (keyData == Keys.F2)
            {
                btnCheckout_Click(this, EventArgs.Empty);
                return true;
            }
            else if (keyData == Keys.F3)
            {
                btnProducts_Click(this, EventArgs.Empty);
                return true;
            }
            else if (keyData == Keys.F4)
            {
                btnSalesHistory_Click(this, EventArgs.Empty);
                return true;
            }
            else if (keyData == Keys.F5)
            {
                btnAuditLogs_Click(this, EventArgs.Empty);
                return true;
            }
            else if (keyData == (Keys.Control | Keys.R))
            {
                btnReceipt_Click(this, EventArgs.Empty);
                return true;
            }
            else if (keyData == (Keys.Control | Keys.L))
            {
                btnLogout_Click(this, EventArgs.Empty);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void frmPOS_Load(object sender, EventArgs e)
        {
            if (btnCalculate != null) btnCalculate.Visible = false;
            if (panel10 != null) panel10.Visible = false;

            InitializeExtraModuleBar();
            ApplyUserSessionUI();

            if (lblReceiptDate != null)
                lblReceiptDate.Text = "Date: " + DateTime.Now.ToString("MMM dd, yyyy hh:mm tt");
            if (lblReceiptCashValue != null)
                lblReceiptCashValue.Text = "₱0.00";
            if (lblReceiptChangeValue != null)
                lblReceiptChangeValue.Text = "₱0.00";

            txtSearch.Text = "Search product name...";
            txtSearch.ForeColor = Color.Gray;

            txtSearch.Enter += (s, ev) =>
            {
                if (txtSearch.Text == "Search product name...")
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor = Color.Black;
                }
            };

            txtSearch.Leave += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text = "Search product name...";
                    txtSearch.ForeColor = Color.Gray;
                }
            };

            dgvReceipt.AllowUserToAddRows = false;
            dgvReceipt.Rows.Clear();
            dgvReceipt.RowTemplate.Height = 32;
            dgvReceipt.ColumnHeadersHeight = 34;
            dgvReceipt.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);
            dgvReceipt.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            dgvReceipt.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            if (dgvReceipt.Columns["colQty"] != null) dgvReceipt.Columns["colQty"].Width = 46;
            if (dgvReceipt.Columns["colPrice"] != null) dgvReceipt.Columns["colPrice"].Width = 80;
            if (dgvReceipt.Columns["colTotal"] != null) dgvReceipt.Columns["colTotal"].Width = 85;
            if (dgvReceipt.Columns["ColItem"] != null) dgvReceipt.Columns["ColItem"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            LoadDynamicProductsFromDb();
            UpdateOrderNoPreview();

            lblSubtotalValue.Text = "₱0.00";
            lblDiscountValue.Text = "₱0.00";
            lblTotalValue.Text = "₱0.00";
            label23.Text = "₱0.00";
            label24.Text = "₱0.00";
            label22.Text = "₱0.00";

            lblTodaySales.Text = "₱0.00";
            lblTransactions.Text = "0";
            lblItemsSold.Text = "0";
            lblAvgOrder.Text = "₱0.00";

            RefreshDailySalesStats();
            this.Activated += (s, ev) => RefreshDailySalesStats();

            pnlProductsHeader.SendToBack();
            flpProducts.BringToFront();
            flpProducts.AutoScrollPosition = new Point(0, 0);

            _previouslyDisplayedDate = DateTime.Today;
            if (lblDateValue != null)
                lblDateValue.Text = DateTime.Now.ToString("MMM dd, yyyy");
            if (lblLiveTime != null)
                lblLiveTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            if (timerClock != null)
            {
                timerClock.Interval = 1000;
                timerClock.Enabled = true;
                timerClock.Start();
            }

            flpProducts.Resize += (s, ev) => AdjustProductCardMargins();
            this.Shown += (s, ev) => AdjustProductCardMargins();
        }

        private void InitializeExtraModuleBar()
        {
            if (pnlExtraModules != null || flpProducts == null || flpProducts.Parent == null)
                return;

            pnlExtraModules = new Panel
            {
                Height = 36,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(245, 236, 226),
                Padding = new Padding(8, 3, 8, 3)
            };

            FlowLayoutPanel flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.Transparent
            };

            btnCategoriesNav = CreateNavChip("Categories", Color.FromArgb(120, 78, 52));
            btnCategoriesNav.Click += (s, e) =>
            {
                using (frmCategories frm = new frmCategories(currentUsername))
                {
                    frm.ShowDialog(this);
                }
                LoadDynamicProductsFromDb();
            };

            btnSuppliersNav = CreateNavChip("Suppliers", Color.FromArgb(120, 78, 52));
            btnSuppliersNav.Click += (s, e) =>
            {
                using (frmSuppliers frm = new frmSuppliers(currentUsername))
                {
                    frm.ShowDialog(this);
                }
            };

            btnStockInNav = CreateNavChip("Stock-In / Restock", Color.FromArgb(46, 125, 50));
            btnStockInNav.Click += (s, e) =>
            {
                using (frmStockIn frm = new frmStockIn(currentUsername))
                {
                    frm.ShowDialog(this);
                }
                LoadDynamicProductsFromDb();
            };

            btnBackupRestoreNav = CreateNavChip("Backup & Restore (Admin)", Color.FromArgb(65, 85, 115));
            btnBackupRestoreNav.Visible = IsAdmin;
            btnBackupRestoreNav.Enabled = IsAdmin;
            btnBackupRestoreNav.Click += (s, e) =>
            {
                if (!IsAdmin)
                {
                    MessageBox.Show("Access denied. Only Administrators can access Backup & Restore.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using (frmBackupRestore frm = new frmBackupRestore(currentUsername))
                {
                    frm.ShowDialog(this);
                }
                LoadDynamicProductsFromDb();
                RefreshDailySalesStats();
            };

            flow.Controls.Add(btnCategoriesNav);
            flow.Controls.Add(btnSuppliersNav);
            flow.Controls.Add(btnStockInNav);
            flow.Controls.Add(btnBackupRestoreNav);

            pnlExtraModules.Controls.Add(flow);
            flpProducts.Parent.Controls.Add(pnlExtraModules);
            pnlExtraModules.BringToFront();
            flpProducts.BringToFront();
        }

        private Button CreateNavChip(string text, Color backColor)
        {
            return new Button
            {
                Text = text,
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                AutoSize = true,
                Height = 28,
                Padding = new Padding(8, 0, 8, 0),
                Margin = new Padding(0, 0, 8, 0),
                Cursor = Cursors.Hand
            };
        }

        private void LoadDynamicProductsFromDb()
        {
            try
            {
                flpProducts.SuspendLayout();
                flpProducts.AutoScrollPosition = new Point(0, 0);

                Dictionary<int, int> activeQuantities = new Dictionary<int, int>();
                foreach (var kvp in _productItems)
                {
                    if (kvp.Value.Quantity > 0)
                        activeQuantities[kvp.Key] = kvp.Value.Quantity;
                }

                flpProducts.Controls.Clear();
                _productItems.Clear();

                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        SELECT ProductID, ProductName, Price, StockQuantity, ImagePath
                        FROM Products
                        WHERE IsActive = 1
                        ORDER BY ProductName";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["ProductID"]);
                            string name = reader["ProductName"].ToString();
                            decimal price = Convert.ToDecimal(reader["Price"]);
                            int stock = Convert.ToInt32(reader["StockQuantity"]);
                            string imagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : null;

                            int existingQty = 0;
                            if (activeQuantities.TryGetValue(id, out existingQty))
                            {
                                if (existingQty > stock) existingQty = stock;
                            }

                            ProductCardItem item = new ProductCardItem
                            {
                                ProductID = id,
                                ProductName = name,
                                Price = price,
                                StockQuantity = stock,
                                ImagePath = imagePath,
                                Quantity = existingQty
                            };

                            Panel card = CreateProductCardPanel(item);
                            item.CardPanel = card;

                            _productItems[id] = item;
                            flpProducts.Controls.Add(card);
                        }
                    }
                }

                flpProducts.ResumeLayout(true);
                AdjustProductCardMargins();
                flpProducts.AutoScrollPosition = new Point(0, 0);
                UpdateOrderSummary();
            }
            catch (Exception ex)
            {
                flpProducts.ResumeLayout(true);
                ErrorLogger.LogError("frmPOS.LoadDynamicProductsFromDb", ex);
                MessageBox.Show(
                    "We couldn't load the bakery products. Please try again.",
                    "Something Went Wrong",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void AdjustProductCardMargins()
        {
            if (flpProducts == null || flpProducts.Controls.Count == 0) return;

            const int cardWidth = 208;
            const int minMargin = 4;
            const int basePadLeft = 12;
            const int basePadRight = 12;

            int availableWidth = flpProducts.ClientSize.Width - basePadLeft - basePadRight;
            if (availableWidth <= 0) return;

            int minCardSlot = cardWidth + (minMargin * 2);
            int cardsPerRow = availableWidth / minCardSlot;
            if (cardsPerRow > 4) cardsPerRow = 4;
            if (cardsPerRow < 1) cardsPerRow = 1;

            int totalCardWidth = cardsPerRow * cardWidth;
            int remainingSpace = availableWidth - totalCardWidth;
            if (remainingSpace < 0) remainingSpace = 0;

            int horizontalMargin = remainingSpace / (cardsPerRow * 2);
            if (horizontalMargin < minMargin) horizontalMargin = minMargin;

            int usedMarginSpace = horizontalMargin * cardsPerRow * 2;
            int remainder = remainingSpace - usedMarginSpace;
            int extraPad = remainder > 0 ? remainder / 2 : 0;

            flpProducts.SuspendLayout();
            flpProducts.Padding = new Padding(basePadLeft + extraPad, 10, basePadRight + extraPad, 40);

            foreach (Control c in flpProducts.Controls)
            {
                if (c is Panel)
                    c.Margin = new Padding(horizontalMargin, 10, horizontalMargin, 10);
            }
            flpProducts.ResumeLayout(true);
        }

        private Panel CreateProductCardPanel(ProductCardItem item)
        {
            Panel card = new Panel
            {
                Width = 208,
                Height = 252,
                Margin = new Padding(8, 10, 8, 10),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(255, 252, 248)
            };

            PictureBox pic = new PictureBox
            {
                Width = 184,
                Height = 128,
                Location = new Point(11, 6),
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = DbHelper.LoadProductImage(item.ImagePath)
            };
            card.Controls.Add(pic);

            Label lblName = new Label
            {
                Text = item.ProductName,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                Location = new Point(5, 138),
                Width = 196,
                Height = 28,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(60, 40, 20)
            };
            card.Controls.Add(lblName);

            Label lblPriceInfo = new Label
            {
                Text = "₱" + item.Price.ToString("N2"),
                Font = new Font("Segoe UI", 11.5F, FontStyle.Bold),
                Location = new Point(5, 166),
                Width = 196,
                Height = 20,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(140, 60, 20)
            };
            card.Controls.Add(lblPriceInfo);

            Label lblStockInfo = new Label
            {
                Text = "Stock: " + item.StockQuantity,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Location = new Point(5, 186),
                Width = 196,
                Height = 18,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = item.StockQuantity > 0 ? Color.DarkGreen : Color.Red
            };
            item.StockLabel = lblStockInfo;
            card.Controls.Add(lblStockInfo);

            Button btnMinus = new Button
            {
                Text = "-",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Width = 38,
                Height = 32,
                Location = new Point(16, 208),
                BackColor = Color.FromArgb(210, 100, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnMinus.FlatAppearance.BorderSize = 0;
            btnMinus.Click += (s, e) => { OnMinusClicked(item.ProductID); };
            card.Controls.Add(btnMinus);

            TextBox txtQty = new TextBox
            {
                Text = item.Quantity.ToString(),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Width = 80,
                Height = 32,
                Location = new Point(63, 209),
                TextAlign = HorizontalAlignment.Center,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(40, 30, 20)
            };

            txtQty.KeyPress += (s, e) =>
            {
                if (char.IsControl(e.KeyChar)) return;
                if (!char.IsDigit(e.KeyChar))
                    e.Handled = true;
            };

            txtQty.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    ValidateAndApplyTypedQuantity(item);
                }
            };

            txtQty.Leave += (s, e) => { ValidateAndApplyTypedQuantity(item); };
            txtQty.Enter += (s, e) => { txtQty.SelectAll(); };
            txtQty.Click += (s, e) => { txtQty.SelectAll(); };

            item.QtyInput = txtQty;
            card.Controls.Add(txtQty);

            Button btnPlus = new Button
            {
                Text = "+",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Width = 38,
                Height = 32,
                Location = new Point(152, 208),
                BackColor = Color.FromArgb(90, 150, 90),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnPlus.FlatAppearance.BorderSize = 0;
            btnPlus.Click += (s, e) => { OnPlusClicked(item.ProductID); };
            card.Controls.Add(btnPlus);

            return card;
        }

        private void ValidateAndApplyTypedQuantity(ProductCardItem item)
        {
            if (item == null || item.QtyInput == null) return;

            string text = item.QtyInput.Text.Trim();
            int newQty;

            if (string.IsNullOrEmpty(text) || !int.TryParse(text, out newQty))
                newQty = 0;

            if (newQty < 0)
                newQty = 0;

            if (newQty > item.StockQuantity)
            {
                MessageBox.Show(
                    "Cannot order " + newQty + " units of " + item.ProductName + ".\nAvailable stock: " + item.StockQuantity,
                    "Stock Limit Exceeded",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                newQty = (item.Quantity >= 0 && item.Quantity <= item.StockQuantity)
                    ? item.Quantity
                    : (item.StockQuantity > 0 ? item.StockQuantity : 0);
            }

            item.Quantity = newQty;
            item.QtyInput.Text = newQty.ToString();
            if (item.QtyLabel != null)
                item.QtyLabel.Text = newQty.ToString();

            UpdateOrderSummary();
        }

        private void OnPlusClicked(int productID)
        {
            if (!_productItems.ContainsKey(productID)) return;
            ProductCardItem item = _productItems[productID];

            if (item.Quantity + 1 > item.StockQuantity)
            {
                MessageBox.Show(
                    "Cannot order more than available stock for " + item.ProductName + ".\nAvailable Stock: " + item.StockQuantity,
                    "Stock Limit Reached",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            item.Quantity++;
            if (item.QtyInput != null) item.QtyInput.Text = item.Quantity.ToString();
            if (item.QtyLabel != null) item.QtyLabel.Text = item.Quantity.ToString();

            UpdateOrderSummary();
        }

        private void OnMinusClicked(int productID)
        {
            if (!_productItems.ContainsKey(productID)) return;
            ProductCardItem item = _productItems[productID];

            if (item.Quantity > 0)
            {
                item.Quantity--;
                if (item.QtyInput != null) item.QtyInput.Text = item.Quantity.ToString();
                if (item.QtyLabel != null) item.QtyLabel.Text = item.Quantity.ToString();

                UpdateOrderSummary();
            }
        }

        private void UpdateOrderSummary()
        {
            dgvReceipt.Rows.Clear();
            decimal subtotal = 0;

            foreach (var item in _productItems.Values)
            {
                if (item.Quantity > 0)
                {
                    decimal lineTotal = item.Quantity * item.Price;
                    subtotal += lineTotal;

                    dgvReceipt.Rows.Add(
                        item.ProductName,
                        item.Quantity,
                        "₱" + item.Price.ToString("N2"),
                        "₱" + lineTotal.ToString("N2")
                    );
                }
            }

            decimal discount = 0;
            decimal total = subtotal - discount;

            lblSubtotalValue.Text = "₱" + subtotal.ToString("N2");
            lblDiscountValue.Text = "₱" + discount.ToString("N2");
            lblTotalValue.Text = "₱" + total.ToString("N2");

            label23.Text = "₱" + subtotal.ToString("N2");
            label24.Text = "₱" + discount.ToString("N2");
            label22.Text = "₱" + total.ToString("N2");

            UpdateCashAndChange();
        }

        private void UpdateCashAndChange()
        {
            decimal total = 0;
            string totalStr = lblTotalValue.Text.Replace("₱", "").Replace(",", "").Trim();
            decimal.TryParse(totalStr, out total);

            decimal cash = 0;
            string cashStr = txtCash.Text.Trim();
            bool hasValidCash = decimal.TryParse(cashStr, out cash) && cash >= 0;

            if (hasValidCash && !string.IsNullOrWhiteSpace(cashStr))
            {
                lblReceiptCashValue.Text = "₱" + cash.ToString("N2");

                decimal change = cash - total;
                if (change >= 0)
                {
                    lblChangeValue.Text = "₱" + change.ToString("N2");
                    lblReceiptChangeValue.Text = "₱" + change.ToString("N2");
                    lblChangeValue.ForeColor = Color.FromArgb(30, 126, 52);
                    lblReceiptChangeValue.ForeColor = Color.FromArgb(30, 126, 52);
                }
                else
                {
                    lblChangeValue.Text = "₱0.00";
                    lblReceiptChangeValue.Text = "₱0.00";
                }
            }
            else
            {
                lblReceiptCashValue.Text = "₱0.00";
                lblChangeValue.Text = "₱0.00";
                lblReceiptChangeValue.Text = "₱0.00";
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim().ToLower();
            if (search == "search product name...") search = "";

            flpProducts.SuspendLayout();
            foreach (var item in _productItems.Values)
            {
                item.CardPanel.Visible = string.IsNullOrEmpty(search) || item.ProductName.ToLower().Contains(search);
            }
            flpProducts.ResumeLayout(true);
            AdjustProductCardMargins();
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (transactionCompleted)
            {
                MessageBox.Show("This order has already been completed.", "Checkout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var item in _productItems.Values)
            {
                if (item.QtyInput != null)
                    ValidateAndApplyTypedQuantity(item);
            }

            UpdateOrderSummary();

            int validItemsCount = 0;
            int totalQuantity = 0;
            foreach (var item in _productItems.Values)
            {
                if (item.Quantity > 0)
                {
                    validItemsCount++;
                    totalQuantity += item.Quantity;
                }
            }

            if (validItemsCount == 0 || totalQuantity == 0)
            {
                MessageBox.Show("Please select at least one product with a valid quantity before checkout.", "Checkout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal total = 0;
            string totalStr = lblTotalValue.Text.Replace("₱", "").Replace(",", "").Trim();
            if (!decimal.TryParse(totalStr, out total) || total <= 0)
            {
                MessageBox.Show("Invalid order total amount.", "Checkout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal cash = 0;
            if (!decimal.TryParse(txtCash.Text.Trim(), out cash))
            {
                MessageBox.Show("Please enter a valid cash payment amount.", "Invalid Cash Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCash.Focus();
                return;
            }

            if (cash < total)
            {
                MessageBox.Show("Insufficient cash payment amount.", "Insufficient Cash", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCash.Focus();
                return;
            }

            decimal change = cash - total;
            lblChangeValue.Text = "₱" + change.ToString("N2");
            lblReceiptCashValue.Text = "₱" + cash.ToString("N2");
            lblReceiptChangeValue.Text = "₱" + change.ToString("N2");
            lastCash = cash;
            lastChange = change;

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            foreach (var item in _productItems.Values)
                            {
                                if (item.Quantity > 0)
                                {
                                    string checkStockQuery = "SELECT StockQuantity FROM Products WHERE ProductID = @ProductID";
                                    using (SqlCommand checkCmd = new SqlCommand(checkStockQuery, conn, transaction))
                                    {
                                        checkCmd.Parameters.AddWithValue("@ProductID", item.ProductID);
                                        object res = checkCmd.ExecuteScalar();
                                        int currentDbStock = res != null ? Convert.ToInt32(res) : 0;

                                        if (currentDbStock < item.Quantity)
                                        {
                                            throw new Exception("Insufficient stock for " + item.ProductName + ". Current stock: " + currentDbStock);
                                        }
                                    }
                                }
                            }

                            string saleQuery = @"
                                INSERT INTO Sales
                                (CashierUsername, SaleDate, TotalAmount, CashAmount, ChangeAmount, Status)
                                VALUES
                                (@CashierUsername, GETDATE(), @TotalAmount, @CashAmount, @ChangeAmount, 'Completed');
                                SELECT SCOPE_IDENTITY();";

                            int saleID;
                            using (SqlCommand cmd = new SqlCommand(saleQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@CashierUsername", currentUsername);
                                cmd.Parameters.AddWithValue("@TotalAmount", total);
                                cmd.Parameters.AddWithValue("@CashAmount", cash);
                                cmd.Parameters.AddWithValue("@ChangeAmount", change);

                                saleID = Convert.ToInt32(cmd.ExecuteScalar());
                                lastSaleID = saleID;
                            }

                            foreach (var item in _productItems.Values)
                            {
                                if (item.Quantity > 0)
                                {
                                    decimal lineTotal = item.Quantity * item.Price;

                                    string insertItemQuery = @"
                                        INSERT INTO SaleItems (SaleID, ProductID, Quantity, UnitPrice, LineTotal)
                                        VALUES (@SaleID, @ProductID, @Quantity, @UnitPrice, @LineTotal)";

                                    using (SqlCommand itemCmd = new SqlCommand(insertItemQuery, conn, transaction))
                                    {
                                        itemCmd.Parameters.AddWithValue("@SaleID", saleID);
                                        itemCmd.Parameters.AddWithValue("@ProductID", item.ProductID);
                                        itemCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                                        itemCmd.Parameters.AddWithValue("@UnitPrice", item.Price);
                                        itemCmd.Parameters.AddWithValue("@LineTotal", lineTotal);
                                        itemCmd.ExecuteNonQuery();
                                    }

                                    string updateStockQuery = @"
                                        UPDATE Products
                                        SET StockQuantity = StockQuantity - @Quantity
                                        WHERE ProductID = @ProductID";

                                    using (SqlCommand stockCmd = new SqlCommand(updateStockQuery, conn, transaction))
                                    {
                                        stockCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                                        stockCmd.Parameters.AddWithValue("@ProductID", item.ProductID);
                                        stockCmd.ExecuteNonQuery();
                                    }
                                }
                            }

                            transaction.Commit();
                            lastCompletedSaleID = saleID;
                            lastSaleID = saleID;
                            transactionCompleted = true;

                            LogAudit("ADD", "Sale", saleID);
                            GenerateReceiptFromDatabase(saleID, false);
                            RefreshDailySalesStats();

                            MessageBox.Show(
                                "Payment successful and transaction completed!\n\nChange: ₱" + change.ToString("N2"),
                                "Checkout Complete",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );

                            PrepareNextOrderState(saleID);
                        }
                        catch (Exception txEx)
                        {
                            transaction.Rollback();
                            ErrorLogger.LogError("frmPOS.btnCheckout_Click (Tx)", txEx);
                            MessageBox.Show(
                                "The transaction could not be completed and was rolled back.\n" + txEx.Message,
                                "Transaction Rolled Back",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmPOS.btnCheckout_Click", ex);
                MessageBox.Show(
                    "The system couldn't complete your request. Please try again.",
                    "Connection Problem",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void PrepareNextOrderState(int completedSaleID)
        {
            foreach (var item in _productItems.Values)
            {
                item.Quantity = 0;
                if (item.QtyInput != null) item.QtyInput.Text = "0";
                if (item.QtyLabel != null) item.QtyLabel.Text = "0";
            }

            dgvReceipt.Rows.Clear();
            txtCash.Clear();
            lblChangeValue.Text = "₱0.00";
            lblReceiptCashValue.Text = "₱0.00";
            lblReceiptChangeValue.Text = "₱0.00";

            lblSubtotalValue.Text = "₱0.00";
            lblDiscountValue.Text = "₱0.00";
            lblTotalValue.Text = "₱0.00";
            label23.Text = "₱0.00";
            label24.Text = "₱0.00";
            label22.Text = "₱0.00";

            transactionCompleted = false;
            LoadDynamicProductsFromDb();

            lblOrderNo.Text = "ORD-" + (completedSaleID + 1).ToString("D4");

            if (lblReceiptCashier != null)
                lblReceiptCashier.Text = "Cashier: " + (!string.IsNullOrWhiteSpace(currentUsername) ? currentUsername : "Cashier");

            RefreshDailySalesStats();
        }

        private void UpdateOrderNoPreview()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = "SELECT ISNULL(MAX(SaleID), 0) FROM Sales";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        int maxID = Convert.ToInt32(cmd.ExecuteScalar());
                        lblOrderNo.Text = "ORD-" + (maxID + 1).ToString("D4");
                    }
                }
            }
            catch
            {
                lblOrderNo.Text = "ORD-0001";
            }
        }

        private void GenerateReceiptFromDatabase(int saleID, bool showSuccessMessage = false)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string saleQuery = "SELECT SaleID, SaleDate, TotalAmount, CashAmount, ChangeAmount, CashierUsername FROM Sales WHERE SaleID = @SaleID";
                    decimal total = 0, cash = 0, change = 0;
                    string cashier = currentUsername;
                    DateTime saleDate = DateTime.Now;

                    using (SqlCommand cmd = new SqlCommand(saleQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@SaleID", saleID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                total = Convert.ToDecimal(reader["TotalAmount"]);
                                cash = Convert.ToDecimal(reader["CashAmount"]);
                                change = Convert.ToDecimal(reader["ChangeAmount"]);
                                object cashierObj = reader["CashierUsername"];
                                if (cashierObj != null && cashierObj != DBNull.Value && !string.IsNullOrWhiteSpace(cashierObj.ToString()))
                                    cashier = cashierObj.ToString();
                                saleDate = Convert.ToDateTime(reader["SaleDate"]);
                            }
                            else
                            {
                                if (showSuccessMessage)
                                    MessageBox.Show("We couldn't find the transaction record for this receipt.", "Receipt Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }

                    string itemsQuery = @"
                        SELECT si.Quantity, si.UnitPrice, si.LineTotal, ISNULL(p.ProductName, 'Unknown Product') AS ProductName
                        FROM SaleItems si
                        LEFT JOIN Products p ON si.ProductID = p.ProductID
                        WHERE si.SaleID = @SaleID";

                    var items = new List<Tuple<string, int, decimal, decimal>>();
                    using (SqlCommand itemCmd = new SqlCommand(itemsQuery, conn))
                    {
                        itemCmd.Parameters.AddWithValue("@SaleID", saleID);
                        using (SqlDataReader itemReader = itemCmd.ExecuteReader())
                        {
                            while (itemReader.Read())
                            {
                                items.Add(Tuple.Create(
                                    itemReader["ProductName"].ToString(),
                                    Convert.ToInt32(itemReader["Quantity"]),
                                    Convert.ToDecimal(itemReader["UnitPrice"]),
                                    Convert.ToDecimal(itemReader["LineTotal"])
                                ));
                            }
                        }
                    }

                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string filePath = Path.Combine(desktopPath, "Bakery_Receipt_" + saleID.ToString("D4") + "_" + saleDate.ToString("yyyyMMdd_HHmmss") + ".txt");

                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.WriteLine("========================================");
                        writer.WriteLine("          BREAD BASKET BAKESHOP");
                        writer.WriteLine("       Freshly Baked, Every Day!");
                        writer.WriteLine("          Panacan, Davao City");
                        writer.WriteLine("          Contact: 0912-345-6789");
                        writer.WriteLine("========================================");
                        writer.WriteLine();
                        writer.WriteLine("Receipt No.: ORD-" + saleID.ToString("D4"));
                        writer.WriteLine("Cashier: " + cashier);
                        writer.WriteLine("Date: " + saleDate.ToString("MMM dd, yyyy hh:mm tt"));
                        writer.WriteLine();
                        writer.WriteLine("----------------------------------------");
                        writer.WriteLine("Item              Qty    Price    Total");
                        writer.WriteLine("----------------------------------------");

                        foreach (var item in items)
                        {
                            writer.WriteLine(
                                item.Item1.PadRight(18) +
                                item.Item2.ToString().PadRight(7) +
                                ("₱" + item.Item3.ToString("N2")).PadRight(9) +
                                ("₱" + item.Item4.ToString("N2"))
                            );
                        }

                        writer.WriteLine("----------------------------------------");
                        writer.WriteLine();
                        writer.WriteLine("TOTAL AMOUNT:   ₱" + total.ToString("N2"));
                        writer.WriteLine();
                        writer.WriteLine("Cash:           ₱" + cash.ToString("N2"));
                        writer.WriteLine("Change:         ₱" + change.ToString("N2"));
                        writer.WriteLine();
                        writer.WriteLine("========================================");
                        writer.WriteLine("       Thank you for your purchase!");
                        writer.WriteLine("              Come Again!");
                        writer.WriteLine("========================================");
                    }

                    if (showSuccessMessage)
                    {
                        MessageBox.Show(
                            "Receipt generated successfully!\nSaved to Desktop:\n" + Path.GetFileName(filePath),
                            "Receipt Generated",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmPOS.GenerateReceiptFromDatabase", ex);
                if (showSuccessMessage)
                {
                    MessageBox.Show("We couldn't generate the receipt. Please try again.", "Something Went Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnReceipt_Click(object sender, EventArgs e)
        {
            if (lastCompletedSaleID <= 0)
            {
                MessageBox.Show("No completed transaction is available for receipt generation. Please complete a checkout first.", "Receipt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            GenerateReceiptFromDatabase(lastCompletedSaleID, true);
        }

        private void btnClearOrder_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Clear the current order?", "Confirm Clear Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            foreach (var item in _productItems.Values)
            {
                item.Quantity = 0;
                if (item.QtyInput != null) item.QtyInput.Text = "0";
                if (item.QtyLabel != null) item.QtyLabel.Text = "0";
            }

            dgvReceipt.Rows.Clear();
            txtCash.Clear();
            lblChangeValue.Text = "₱0.00";
            lblReceiptCashValue.Text = "₱0.00";
            lblReceiptChangeValue.Text = "₱0.00";

            lblSubtotalValue.Text = "₱0.00";
            lblDiscountValue.Text = "₱0.00";
            lblTotalValue.Text = "₱0.00";
            label23.Text = "₱0.00";
            label24.Text = "₱0.00";
            label22.Text = "₱0.00";
        }

        private void btnManageAccounts_Click(object sender, EventArgs e)
        {
            if (!IsAdmin)
            {
                MessageBox.Show("Access denied. Only Administrators can manage user accounts.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (frmManageAccounts frm = new frmManageAccounts(currentUsername))
            {
                frm.ShowDialog(this);
            }
        }

        private void btnOrder_Click(object sender, EventArgs e) => PrepareNextOrderState(lastSaleID);
        private void btnReset_Click(object sender, EventArgs e) => PrepareNextOrderState(lastSaleID);

        private void btnProducts_Click(object sender, EventArgs e)
        {
            frmProducts productsForm = new frmProducts(currentUsername);
            productsForm.ShowDialog();
            LoadDynamicProductsFromDb();
        }

        private void btnSalesHistory_Click(object sender, EventArgs e)
        {
            frmSales salesForm = new frmSales(currentUsername);
            salesForm.ShowDialog();
            LoadDynamicProductsFromDb();
            RefreshDailySalesStats();
        }

        private void btnAuditLogs_Click(object sender, EventArgs e)
        {
            if (!IsAdmin)
            {
                MessageBox.Show("Access denied. Only Administrators can view Audit Logs.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (frmAuditLogs auditForm = new frmAuditLogs(currentAccessLevel))
            {
                auditForm.ShowDialog(this);
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            using (frmReports reportsForm = new frmReports(currentUsername))
            {
                reportsForm.ShowDialog(this);
            }
        }

        private void OpenEndOfShiftReport()
        {
            string role = string.IsNullOrWhiteSpace(currentAccessLevel) ? (string.IsNullOrWhiteSpace(currentName) ? "Cashier" : currentName) : currentAccessLevel.Trim();
            using (frmEndOfShiftReport reportForm = new frmEndOfShiftReport(role, currentUsername))
            {
                reportForm.ShowDialog(this);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            using (frmLogoutReminder reminder = new frmLogoutReminder())
            {
                reminder.ShowDialog(this);

                if (reminder.SelectedChoice == frmLogoutReminder.ReminderChoice.Logout)
                {
                    LogAudit("LOGOUT", "Accounts", 0);
                    frmLogin loginForm = new frmLogin();
                    loginForm.Show();
                    this.Hide();
                }
                else if (reminder.SelectedChoice == frmLogoutReminder.ReminderChoice.GenerateReport)
                {
                    OpenEndOfShiftReport();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit the POS?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                LogAudit("LOGOUT", "Accounts", 0);
                Application.Exit();
            }
        }

        private void RefreshDailySalesStats()
        {
            decimal todaySales = 0m;
            int transactionCount = 0;
            int itemsSold = 0;
            decimal averageOrder = 0m;

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    DateTime currentDate = DateTime.Today;

                    string salesQuery = @"
                        SELECT ISNULL(SUM(TotalAmount), 0) AS TotalSales,
                               COUNT(*) AS TransactionCount
                        FROM Sales
                        WHERE CAST(SaleDate AS DATE) = @Today
                          AND ISNULL(Status, 'Completed') = 'Completed'";

                    using (SqlCommand cmd = new SqlCommand(salesQuery, conn))
                    {
                        cmd.Parameters.Add("@Today", SqlDbType.Date).Value = currentDate;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                todaySales = (reader["TotalSales"] != DBNull.Value) ? Convert.ToDecimal(reader["TotalSales"]) : 0m;
                                transactionCount = (reader["TransactionCount"] != DBNull.Value) ? Convert.ToInt32(reader["TransactionCount"]) : 0;
                            }
                        }
                    }

                    string itemsQuery = @"
                        SELECT ISNULL(SUM(si.Quantity), 0) AS TotalItemsSold
                        FROM SaleItems si
                        INNER JOIN Sales s ON si.SaleID = s.SaleID
                        WHERE CAST(s.SaleDate AS DATE) = @Today
                          AND ISNULL(s.Status, 'Completed') = 'Completed'";

                    using (SqlCommand cmd = new SqlCommand(itemsQuery, conn))
                    {
                        cmd.Parameters.Add("@Today", SqlDbType.Date).Value = currentDate;
                        object res = cmd.ExecuteScalar();
                        itemsSold = (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
                    }

                    averageOrder = transactionCount > 0 ? todaySales / transactionCount : 0m;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmPOS.RefreshDailySalesStats", ex);
                todaySales = 0m;
                transactionCount = 0;
                itemsSold = 0;
                averageOrder = 0m;
            }

            if (lblTodaySales != null) lblTodaySales.Text = "₱" + todaySales.ToString("N2");
            if (lblTransactions != null) lblTransactions.Text = transactionCount.ToString();
            if (lblItemsSold != null) lblItemsSold.Text = itemsSold.ToString();
            if (lblAvgOrder != null) lblAvgOrder.Text = "₱" + averageOrder.ToString("N2");
        }

        private void LoadTodaySales() => RefreshDailySalesStats();
        private void LoadTodayTransactions() => RefreshDailySalesStats();
        private void LoadTodayItemsSold() => RefreshDailySalesStats();
        private void LoadAverageOrder() => RefreshDailySalesStats();

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
                ErrorLogger.LogError("frmPOS.LogAudit", ex);
            }
        }

        private void txtCash_TextChanged(object sender, EventArgs e) => UpdateCashAndChange();

        private void timerClock_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime today = now.Date;

            if (today != _previouslyDisplayedDate)
            {
                _previouslyDisplayedDate = today;
                if (lblDateValue != null)
                    lblDateValue.Text = now.ToString("MMM dd, yyyy");
                RefreshDailySalesStats();
            }

            if (lblLiveTime != null)
                lblLiveTime.Text = now.ToString("hh:mm:ss tt");
        }

        // Empty event handler stubs to satisfy Designer connections
        private void lblTitle_Click(object sender, EventArgs e) { }
        private void lblCashier_Click(object sender, EventArgs e) { }
        private void lblDate_Click(object sender, EventArgs e) { }
        private void checkBox1_CheckedChanged(object sender, EventArgs e) { }
        private void checkBox2_CheckedChanged(object sender, EventArgs e) { }
        private void checkBox5_CheckedChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void numPandesal_ValueChanged(object sender, EventArgs e) { }
        private void numEnsaymada_ValueChanged(object sender, EventArgs e) { }
        private void numLoafBread_ValueChanged(object sender, EventArgs e) { }
        private void txtDiscount_Click(object sender, EventArgs e) { }
        private void txtChange_TextChanged(object sender, EventArgs e) { }
        private void btnCalculate_Click(object sender, EventArgs e) { }
        private void txtDiscount_TextChanged(object sender, EventArgs e) { }
        private void txtSubtotal_TextChanged(object sender, EventArgs e) { }
        private void pnlProducts_Paint(object sender, PaintEventArgs e) { }
        private void lstOrderSummary_SelectedIndexChanged(object sender, EventArgs e) { }
        private void lblPriceLoafBread_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) { }
        private void label5_Click_1(object sender, EventArgs e) { }
        private void picLogo_Click(object sender, EventArgs e) { }
        private void pnlStat4_Paint(object sender, PaintEventArgs e) { }
        private void label4_Click_1(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void pnlPandesal_Paint(object sender, PaintEventArgs e) { }
        private void picLoaf_Click(object sender, EventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void label2_Click_1(object sender, EventArgs e) { }
        private void lblRole_Click(object sender, EventArgs e) { }
        private void lblLiveTimeTitle_Click(object sender, EventArgs e) { }
        private void lblCashierName_Click(object sender, EventArgs e) { }
        private void btnPlusCheese_Click(object sender, EventArgs e) { }
        private void label19_Click(object sender, EventArgs e) { }
        private void pnlReceipt_Paint(object sender, PaintEventArgs e) { }
        private void label20_Click(object sender, EventArgs e) { }
        private void btnMinusEnsaymada_Click(object sender, EventArgs e) { }
        private void btnPlusEnsaymada_Click(object sender, EventArgs e) { }
        private void lblQtyEnsaymada_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void pnlTransactionInfo_Paint(object sender, PaintEventArgs e) { }
        private void checkBox1_CheckedChanged_1(object sender, EventArgs e) { }
        private void label11_Click(object sender, EventArgs e) { }
        private void label16_Click(object sender, EventArgs e) { }
        private void lblStat4Value_Click(object sender, EventArgs e) { }
        private void lblTodaySales_Click(object sender, EventArgs e) { }
        private void lblContact_Click(object sender, EventArgs e) { }
        private void lblTotalValue_Click(object sender, EventArgs e) { }
        private void lblCash_Click(object sender, EventArgs e) { }
        private void lblSubtotalValue_Click(object sender, EventArgs e) { }
        private void lblDiscountValue_Click(object sender, EventArgs e) { }
        private void lblAddress_Click(object sender, EventArgs e) { }
        private void btnPlusPandesal_Click(object sender, EventArgs e) { }
        private void btnMinusPandesal_Click(object sender, EventArgs e) { }
        private void lblQtyPandesal_Click(object sender, EventArgs e) { }
        private void picPandesal_Click(object sender, EventArgs e) { }
        private void label24_Click(object sender, EventArgs e) { }
        private void btnPlusSpanish_Click(object sender, EventArgs e) { }
        private void btnMinusSpanish_Click(object sender, EventArgs e) { }
        private void btnMinusCheese_Click(object sender, EventArgs e) { }
        private void btnPlusLoaf_Click(object sender, EventArgs e) { }
        private void btnMinusLoaf_Click(object sender, EventArgs e) { }
        private void lblDate_Click_1(object sender, EventArgs e) { }
        private void lblTime_Click(object sender, EventArgs e) { }
        private void panel8_Paint(object sender, PaintEventArgs e) { }
        private void label22_Click(object sender, EventArgs e) { }
        private void flpProducts_Paint(object sender, PaintEventArgs e) { }
        private void lblOrderNo_Click(object sender, EventArgs e) { }
        private void lblTransactions_Click(object sender, EventArgs e) { }
        private void dgvReceipt_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void lblBakeryName_Click(object sender, EventArgs e) { }
        private void picSpanish_Click(object sender, EventArgs e) { }
    }
}