using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using BakeryPOS.UI;

namespace BakeryPOS
{
    public partial class frmCategories : Form
    {
        private readonly string _username;
        private int _selectedId = 0;
        private bool _selectedIsActive = true;
        private byte[] _selectedRowVer = null;
        private DataTable _dt = new DataTable();

        private TextBox txtName, txtDesc, txtSearch;
        private CheckBox chkShowInactive;
        private Button btnAdd, btnUpdate, btnToggleActive, btnClear;
        private DataGridView dgv;

        public frmCategories() : this("admin") { }

        public frmCategories(string username)
        {
            InitializeComponent();
            _username = string.IsNullOrWhiteSpace(username) ? "admin" : username;
            BuildUI();
        }

        private void BuildUI()
        {
            this.Text = "Bakery POS - Category Management";
            this.ClientSize = new Size(800, 535);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(253, 245, 236);

            Panel pnlHeader = Styles.CreateHeaderBanner("CATEGORY MANAGEMENT");

            Panel pnlLeft = new Panel { Left = 16, Top = 100, Width = 265, Height = 418, BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            Label lblTitle = new Label { Text = "Category Details", Left = 14, Top = 12, AutoSize = true, Font = new Font("Segoe UI", 12F, FontStyle.Bold), ForeColor = Color.FromArgb(123, 63, 26) };

            Label lblName = new Label { Text = "Category Name *", Left = 14, Top = 48, AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(50, 22, 8) };
            txtName = new TextBox { Left = 14, Top = 70, Width = 235, Font = new Font("Segoe UI", 10F, FontStyle.Regular) };

            Label lblDesc = new Label { Text = "Description", Left = 14, Top = 108, AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(50, 22, 8) };
            txtDesc = new TextBox { Left = 14, Top = 130, Width = 235, Height = 72, Multiline = true, Font = new Font("Segoe UI", 10F, FontStyle.Regular) };

            btnAdd = MakeBtn("Add Category", 14, 218, 235, Color.FromArgb(101, 61, 34));
            btnAdd.Click += BtnAdd_Click;

            btnUpdate = MakeBtn("Update Selected", 14, 262, 235, Color.FromArgb(184, 115, 51));
            btnUpdate.Click += BtnUpdate_Click;

            btnToggleActive = MakeBtn("Deactivate", 14, 306, 235, Color.FromArgb(178, 58, 58));
            btnToggleActive.Click += BtnToggleActive_Click;

            btnClear = MakeBtn("Clear / New", 14, 350, 235, Color.FromArgb(115, 110, 105));
            btnClear.Click += (s, e) => ClearForm();

            pnlLeft.Controls.AddRange(new Control[] { lblTitle, lblName, txtName, lblDesc, txtDesc, btnAdd, btnUpdate, btnToggleActive, btnClear });

            Label lblSearch = new Label { Text = "Search:", Left = 296, Top = 105, AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(50, 22, 8) };
            txtSearch = new TextBox { Left = 356, Top = 101, Width = 285, Font = new Font("Segoe UI", 10F, FontStyle.Regular) };
            txtSearch.TextChanged += (s, e) => ApplyFilter();

            chkShowInactive = new CheckBox { Text = "Show Archived", Left = 656, Top = 104, AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(50, 22, 8) };
            chkShowInactive.CheckedChanged += (s, e) => LoadData();

            dgv = new DataGridView
            {
                Left = 296,
                Top = 136,
                Width = 488,
                Height = 382,
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
                    string sql = $@"SELECT CategoryID, CategoryName, Description, IsActive,
                                           CASE WHEN IsActive = 1 THEN 'Active' ELSE 'Archived' END AS Status, RowVer
                                    FROM Categories {where} ORDER BY CategoryName";
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        _dt = new DataTable();
                        da.Fill(_dt);
                        dgv.DataSource = _dt;
                        if (dgv.Columns["CategoryID"] != null)
                        {
                            dgv.Columns["CategoryID"].HeaderText = "ID";
                            dgv.Columns["CategoryID"].FillWeight = 14F;
                            dgv.Columns["CategoryID"].MinimumWidth = 50;
                        }
                        if (dgv.Columns["CategoryName"] != null)
                        {
                            dgv.Columns["CategoryName"].HeaderText = "Category Name";
                            dgv.Columns["CategoryName"].FillWeight = 34F;
                        }
                        if (dgv.Columns["Description"] != null)
                        {
                            dgv.Columns["Description"].HeaderText = "Description";
                            dgv.Columns["Description"].FillWeight = 34F;
                        }
                        if (dgv.Columns["Status"] != null)
                        {
                            dgv.Columns["Status"].HeaderText = "Status";
                            dgv.Columns["Status"].FillWeight = 18F;
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
                ErrorLogger.LogError("frmCategories.LoadData", ex);
                MessageBox.Show("Unable to load categories.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilter()
        {
            if (_dt == null) return;
            string term = txtSearch.Text.Trim().Replace("'", "''");
            _dt.DefaultView.RowFilter = string.IsNullOrEmpty(term) ? "" : $"CategoryName LIKE '%{term}%' OR Description LIKE '%{term}%'";
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgv.Rows[e.RowIndex].DataBoundItem == null) return;
            DataRowView drv = (DataRowView)dgv.Rows[e.RowIndex].DataBoundItem;
            _selectedId = Convert.ToInt32(drv["CategoryID"]);
            _selectedIsActive = Convert.ToBoolean(drv["IsActive"]);
            _selectedRowVer = (byte[])drv["RowVer"];
            txtName.Text = drv["CategoryName"].ToString();
            txtDesc.Text = drv["Description"] == DBNull.Value ? "" : drv["Description"].ToString();
            UpdateButtons();
        }

        private void ClearForm()
        {
            _selectedId = 0;
            _selectedIsActive = true;
            _selectedRowVer = null;
            txtName.Clear();
            txtDesc.Clear();
            dgv?.ClearSelection();
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            bool sel = _selectedId > 0;
            btnUpdate.Enabled = sel;
            btnToggleActive.Enabled = sel;
            btnToggleActive.Text = _selectedIsActive ? "Archive (Soft Delete)" : "Restore Category";
            btnToggleActive.BackColor = _selectedIsActive ? Color.FromArgb(178, 58, 58) : Color.FromArgb(101, 61, 34);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Category Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show($"Add category '{name}'?", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();
                    using (SqlCommand chk = new SqlCommand("SELECT COUNT(*) FROM Categories WHERE LOWER(CategoryName) = LOWER(@Name)", conn))
                    {
                        chk.Parameters.AddWithValue("@Name", name);
                        if (Convert.ToInt32(chk.ExecuteScalar()) > 0)
                        {
                            MessageBox.Show("A category with that name already exists.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Categories (CategoryName, Description, IsActive) VALUES (@Name, @Desc, 1); SELECT SCOPE_IDENTITY();", conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Desc", string.IsNullOrWhiteSpace(txtDesc.Text) ? (object)DBNull.Value : txtDesc.Text.Trim());
                        int newId = Convert.ToInt32(cmd.ExecuteScalar());
                        LogAudit(conn, "ADD", newId);
                    }
                }
                MessageBox.Show("Category added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadData();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmCategories.BtnAdd_Click", ex);
                MessageBox.Show("Could not add category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedId <= 0) return;
            string name = txtName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Category Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show($"Update category to '{name}'?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Categories SET CategoryName = @Name, Description = @Desc WHERE CategoryID = @ID AND RowVer = @RowVer", conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Desc", string.IsNullOrWhiteSpace(txtDesc.Text) ? (object)DBNull.Value : txtDesc.Text.Trim());
                        cmd.Parameters.AddWithValue("@ID", _selectedId);
                        cmd.Parameters.Add("@RowVer", SqlDbType.Timestamp).Value = _selectedRowVer;
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            MessageBox.Show("This category was modified by another user. Refreshing.", "Concurrency Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            LoadData();
                            return;
                        }
                    }
                    LogAudit(conn, "UPDATE", _selectedId);
                }
                MessageBox.Show("Category updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadData();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmCategories.BtnUpdate_Click", ex);
                MessageBox.Show("Could not update category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        using (SqlCommand refCmd = new SqlCommand("SELECT COUNT(*) FROM Products WHERE CategoryID = @ID AND IsActive = 1", conn))
                        {
                            refCmd.Parameters.AddWithValue("@ID", _selectedId);
                            int activeRefs = Convert.ToInt32(refCmd.ExecuteScalar());
                            if (activeRefs > 0)
                            {
                                MessageBox.Show($"Cannot archive this category because {activeRefs} active product(s) still use it.", "Referenced Record", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }

                    if (MessageBox.Show($"Are you sure you want to {actionWord} this category?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                    using (SqlCommand cmd = new SqlCommand("UPDATE Categories SET IsActive = @Active WHERE CategoryID = @ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@Active", target);
                        cmd.Parameters.AddWithValue("@ID", _selectedId);
                        cmd.ExecuteNonQuery();
                    }
                    LogAudit(conn, target ? "RESTORE" : "DEACTIVATE", _selectedId);
                }
                MessageBox.Show($"Category {actionWord}d successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadData();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmCategories.BtnToggleActive_Click", ex);
                MessageBox.Show("Operation failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LogAudit(SqlConnection conn, string action, int id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO AuditLogs (Username, Action, Module, RecordID, ActionDate) VALUES (@U, @A, 'Category', @R, GETDATE())", conn))
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