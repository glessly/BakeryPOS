using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using BakeryPOS.UI;

namespace BakeryPOS
{
    public partial class frmSuppliers : Form
    {
        private readonly string _username;
        private int _selectedId = 0;
        private bool _selectedIsActive = true;
        private byte[] _selectedRowVer = null;
        private DataTable _dt = new DataTable();

        private TextBox txtName, txtContactPerson, txtContactNo, txtAddress, txtSearch;
        private CheckBox chkShowInactive;
        private Button btnAdd, btnUpdate, btnToggleActive, btnClear;
        private DataGridView dgv;

        public frmSuppliers() : this("admin") { }

        public frmSuppliers(string username)
        {
            InitializeComponent();
            _username = string.IsNullOrWhiteSpace(username) ? "admin" : username;
            BuildUI();
        }

        private void BuildUI()
        {
            this.Text = "Bakery POS - Supplier Management";
            this.ClientSize = new Size(920, 590);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(253, 245, 236);

            Panel pnlHeader = Styles.CreateHeaderBanner("SUPPLIER MANAGEMENT");

            Panel pnlLeft = new Panel { Left = 16, Top = 100, Width = 280, Height = 474, BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            Label lblTitle = new Label { Text = "Supplier Details", Left = 14, Top = 10, AutoSize = true, Font = new Font("Segoe UI", 12F, FontStyle.Bold), ForeColor = Color.FromArgb(123, 63, 26) };

            Label l1 = new Label { Text = "Supplier Name *", Left = 14, Top = 42, AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(50, 22, 8) };
            txtName = new TextBox { Left = 14, Top = 62, Width = 250, Font = new Font("Segoe UI", 10F, FontStyle.Regular) };

            Label l2 = new Label { Text = "Contact Person", Left = 14, Top = 96, AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(50, 22, 8) };
            txtContactPerson = new TextBox { Left = 14, Top = 116, Width = 250, Font = new Font("Segoe UI", 10F, FontStyle.Regular) };

            Label l3 = new Label { Text = "Contact Number", Left = 14, Top = 150, AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(50, 22, 8) };
            txtContactNo = new TextBox { Left = 14, Top = 170, Width = 250, Font = new Font("Segoe UI", 10F, FontStyle.Regular) };

            Label l4 = new Label { Text = "Address", Left = 14, Top = 204, AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(50, 22, 8) };
            txtAddress = new TextBox { Left = 14, Top = 224, Width = 250, Height = 52, Multiline = true, Font = new Font("Segoe UI", 10F, FontStyle.Regular) };

            btnAdd = MakeBtn("Add Supplier", 14, 288, 250, Color.FromArgb(101, 61, 34));
            btnAdd.Click += BtnAdd_Click;

            btnUpdate = MakeBtn("Update Selected", 14, 332, 250, Color.FromArgb(184, 115, 51));
            btnUpdate.Click += BtnUpdate_Click;

            btnToggleActive = MakeBtn("Archive (Soft Delete)", 14, 376, 250, Color.FromArgb(178, 58, 58));
            btnToggleActive.Click += BtnToggleActive_Click;

            btnClear = MakeBtn("Clear / New", 14, 420, 250, Color.FromArgb(115, 110, 105));
            btnClear.Click += (s, e) => ClearForm();

            pnlLeft.Controls.AddRange(new Control[] { lblTitle, l1, txtName, l2, txtContactPerson, l3, txtContactNo, l4, txtAddress, btnAdd, btnUpdate, btnToggleActive, btnClear });

            Label lblSearch = new Label { Text = "Search:", Left = 312, Top = 105, AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(50, 22, 8) };
            txtSearch = new TextBox { Left = 372, Top = 101, Width = 380, Font = new Font("Segoe UI", 10F, FontStyle.Regular) };
            txtSearch.TextChanged += (s, e) => ApplyFilter();

            chkShowInactive = new CheckBox { Text = "Show Archived", Left = 768, Top = 104, AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(50, 22, 8) };
            chkShowInactive.CheckedChanged += (s, e) => LoadData();

            dgv = new DataGridView
            {
                Left = 312,
                Top = 136,
                Width = 592,
                Height = 438,
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
            Styles.ApplyGridStyle(dgv);
            dgv.CellClick += Dgv_CellClick;

            this.Controls.AddRange(new Control[] { pnlLeft, lblSearch, txtSearch, chkShowInactive, dgv, pnlHeader });
            this.Load += (s, e) => { ClearForm(); LoadData(); };
        }

        private Button MakeBtn(string text, int x, int y, int w, Color bg)
        {
            Button btn = new Button
            {
                Text = text,
                Left = x,
                Top = y,
                Width = w,
                Height = 36,
                BackColor = bg,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();
                    string where = chkShowInactive.Checked ? "" : "WHERE IsActive = 1";
                    string sql = $@"SELECT SupplierID, SupplierName, ContactPerson, ContactNumber, Address, IsActive,
                                           CASE WHEN IsActive = 1 THEN 'Active' ELSE 'Archived' END AS Status, RowVer
                                    FROM Suppliers {where} ORDER BY SupplierName";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        _dt = new DataTable();
                        da.Fill(_dt);
                        dgv.DataSource = _dt;
                        if (dgv.Columns["SupplierID"] != null)
                        {
                            dgv.Columns["SupplierID"].HeaderText = "ID";
                            dgv.Columns["SupplierID"].FillWeight = 10F;
                            dgv.Columns["SupplierID"].MinimumWidth = 45;
                        }
                        if (dgv.Columns["SupplierName"] != null)
                        {
                            dgv.Columns["SupplierName"].HeaderText = "Supplier Name";
                            dgv.Columns["SupplierName"].FillWeight = 24F;
                        }
                        if (dgv.Columns["ContactPerson"] != null)
                        {
                            dgv.Columns["ContactPerson"].HeaderText = "Contact Person";
                            dgv.Columns["ContactPerson"].FillWeight = 20F;
                        }
                        if (dgv.Columns["ContactNumber"] != null)
                        {
                            dgv.Columns["ContactNumber"].HeaderText = "Contact No.";
                            dgv.Columns["ContactNumber"].FillWeight = 17F;
                        }
                        if (dgv.Columns["Address"] != null)
                        {
                            dgv.Columns["Address"].HeaderText = "Address";
                            dgv.Columns["Address"].FillWeight = 17F;
                        }
                        if (dgv.Columns["Status"] != null)
                        {
                            dgv.Columns["Status"].HeaderText = "Status";
                            dgv.Columns["Status"].FillWeight = 12F;
                        }
                        if (dgv.Columns["IsActive"] != null) dgv.Columns["IsActive"].Visible = false;
                        if (dgv.Columns["RowVer"] != null) dgv.Columns["RowVer"].Visible = false;
                        Styles.ApplyGridStyle(dgv);
                        ApplyFilter();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmSuppliers.LoadData", ex);
                MessageBox.Show("Unable to load suppliers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilter()
        {
            if (_dt == null) return;
            string term = txtSearch.Text.Trim().Replace("'", "''");
            _dt.DefaultView.RowFilter = string.IsNullOrEmpty(term) ? "" : $"SupplierName LIKE '%{term}%' OR ContactPerson LIKE '%{term}%' OR ContactNumber LIKE '%{term}%'";
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgv.Rows[e.RowIndex].DataBoundItem == null) return;
            DataRowView drv = (DataRowView)dgv.Rows[e.RowIndex].DataBoundItem;
            _selectedId = Convert.ToInt32(drv["SupplierID"]);
            _selectedIsActive = Convert.ToBoolean(drv["IsActive"]);
            _selectedRowVer = (byte[])drv["RowVer"];
            txtName.Text = drv["SupplierName"].ToString();
            txtContactPerson.Text = drv["ContactPerson"] == DBNull.Value ? "" : drv["ContactPerson"].ToString();
            txtContactNo.Text = drv["ContactNumber"] == DBNull.Value ? "" : drv["ContactNumber"].ToString();
            txtAddress.Text = drv["Address"] == DBNull.Value ? "" : drv["Address"].ToString();
            UpdateButtons();
        }

        private void ClearForm()
        {
            _selectedId = 0;
            _selectedIsActive = true;
            _selectedRowVer = null;
            txtName.Clear();
            txtContactPerson.Clear();
            txtContactNo.Clear();
            txtAddress.Clear();
            dgv?.ClearSelection();
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            bool sel = _selectedId > 0;
            btnUpdate.Enabled = sel;
            btnToggleActive.Enabled = sel;
            btnToggleActive.Text = _selectedIsActive ? "Archive (Soft Delete)" : "Restore Supplier";
            btnToggleActive.BackColor = _selectedIsActive ? Color.FromArgb(178, 58, 58) : Color.FromArgb(101, 61, 34);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Supplier Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show($"Add supplier '{name}'?", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();
                    using (SqlCommand chk = new SqlCommand("SELECT COUNT(*) FROM Suppliers WHERE LOWER(SupplierName) = LOWER(@Name)", conn))
                    {
                        chk.Parameters.AddWithValue("@Name", name);
                        if (Convert.ToInt32(chk.ExecuteScalar()) > 0)
                        {
                            MessageBox.Show("Supplier already exists.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO Suppliers (SupplierName, ContactPerson, ContactNumber, Address, IsActive)
                        VALUES (@Name, @Person, @No, @Addr, 1); SELECT SCOPE_IDENTITY();", conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Person", string.IsNullOrWhiteSpace(txtContactPerson.Text) ? (object)DBNull.Value : txtContactPerson.Text.Trim());
                        cmd.Parameters.AddWithValue("@No", string.IsNullOrWhiteSpace(txtContactNo.Text) ? (object)DBNull.Value : txtContactNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@Addr", string.IsNullOrWhiteSpace(txtAddress.Text) ? (object)DBNull.Value : txtAddress.Text.Trim());
                        int newId = Convert.ToInt32(cmd.ExecuteScalar());
                        LogAudit(conn, "ADD", newId);
                    }
                }
                MessageBox.Show("Supplier added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadData();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmSuppliers.BtnAdd_Click", ex);
                MessageBox.Show("Could not add supplier.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedId <= 0) return;
            string name = txtName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Supplier Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show($"Update supplier '{name}'?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(@"
                        UPDATE Suppliers
                        SET SupplierName = @Name, ContactPerson = @Person, ContactNumber = @No, Address = @Addr
                        WHERE SupplierID = @ID AND RowVer = @RowVer", conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Person", string.IsNullOrWhiteSpace(txtContactPerson.Text) ? (object)DBNull.Value : txtContactPerson.Text.Trim());
                        cmd.Parameters.AddWithValue("@No", string.IsNullOrWhiteSpace(txtContactNo.Text) ? (object)DBNull.Value : txtContactNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@Addr", string.IsNullOrWhiteSpace(txtAddress.Text) ? (object)DBNull.Value : txtAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@ID", _selectedId);
                        cmd.Parameters.Add("@RowVer", SqlDbType.Timestamp).Value = _selectedRowVer;
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            MessageBox.Show("This supplier was modified by another user. Refreshing.", "Concurrency Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            LoadData();
                            return;
                        }
                    }
                    LogAudit(conn, "UPDATE", _selectedId);
                }
                MessageBox.Show("Supplier updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadData();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmSuppliers.BtnUpdate_Click", ex);
                MessageBox.Show("Could not update supplier.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnToggleActive_Click(object sender, EventArgs e)
        {
            if (_selectedId <= 0) return;
            bool target = !_selectedIsActive;
            string actionWord = target ? "restore" : "archive";

            try
            {
                using (SqlConnection conn = new SqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();
                    if (!target)
                    {
                        using (SqlCommand refCmd = new SqlCommand("SELECT COUNT(*) FROM StockIns WHERE SupplierID = @ID", conn))
                        {
                            refCmd.Parameters.AddWithValue("@ID", _selectedId);
                            int refs = Convert.ToInt32(refCmd.ExecuteScalar());
                            if (refs > 0)
                            {
                                MessageBox.Show($"Note: Supplier has {refs} stock-in transaction(s). Proceeding with soft-archive only.", "Referenced Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }

                    if (MessageBox.Show($"Are you sure you want to {actionWord} this supplier?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                    using (SqlCommand cmd = new SqlCommand("UPDATE Suppliers SET IsActive = @Active WHERE SupplierID = @ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@Active", target);
                        cmd.Parameters.AddWithValue("@ID", _selectedId);
                        cmd.ExecuteNonQuery();
                    }
                    LogAudit(conn, target ? "RESTORE" : "DEACTIVATE", _selectedId);
                }
                MessageBox.Show($"Supplier {actionWord}d successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadData();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmSuppliers.BtnToggleActive_Click", ex);
                MessageBox.Show("Operation failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LogAudit(SqlConnection conn, string action, int id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO AuditLogs (Username, Action, Module, RecordID, ActionDate) VALUES (@U, @A, 'Supplier', @R, GETDATE())", conn))
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