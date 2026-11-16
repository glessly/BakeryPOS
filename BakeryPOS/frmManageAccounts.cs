using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BakeryPOS
{
    public partial class frmManageAccounts : Form
    {
        private string _adminUsername;
        private int _selectedUserId = 0;
        private bool _selectedIsActive = true;
        private byte[] _selectedRowVer = null;
        private DataTable _accountsTable = new DataTable();

        // Runtime controls for full User Management CRUD without breaking Designer layout
        private ComboBox cmbRole;
        private TextBox txtSearchAccount;
        private Button btnUpdateAccount;
        private Button btnResetPassword;
        private Button btnToggleActive;
        private Button btnClearSelection;

        public frmManageAccounts() : this("admin")
        {
        }

        public frmManageAccounts(string adminUsername)
        {
            InitializeComponent();
            _adminUsername = string.IsNullOrWhiteSpace(adminUsername) ? "admin" : adminUsername;
        }

        private void frmManageAccounts_Load(object sender, EventArgs e)
        {
            InitializeManagementControls();
            FormatAccountsGrid();
            LoadAccounts();
        }

        private void InitializeManagementControls()
        {
            // 1. Role selector placed below pnlPassContainer inside pnlForm
            if (cmbRole == null)
            {
                Label lblRoleSelect = new Label
                {
                    Text = "Role",
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(50, 22, 8),
                    AutoSize = true,
                    Location = new Point(18, 208)
                };

                cmbRole = new ComboBox
                {
                    Name = "cmbRole",
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                    Location = new Point(20, 230),
                    Width = 292
                };
                cmbRole.Items.AddRange(new object[] { "Cashier", "Admin" });
                cmbRole.SelectedIndex = 0;

                pnlForm.Controls.Add(lblRoleSelect);
                pnlForm.Controls.Add(cmbRole);
            }

            // 2. Search box placed to the right of "Existing Accounts" label with zero overlap
            if (txtSearchAccount == null)
            {
                int searchLeft = lblAccountsList.Right + 24;
                Label lblSearch = new Label
                {
                    Text = "Search:",
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(50, 22, 8),
                    AutoSize = true,
                    Location = new Point(searchLeft, 9)
                };

                int txtLeft = searchLeft + 58;
                txtSearchAccount = new TextBox
                {
                    Name = "txtSearchAccount",
                    Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                    Location = new Point(txtLeft, 5),
                    Width = Math.Max(180, (dgvAccounts.Right) - txtLeft),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };
                txtSearchAccount.TextChanged += (s, e) => ApplyAccountFilter();

                dgvAccounts.Parent.Controls.Add(lblSearch);
                dgvAccounts.Parent.Controls.Add(txtSearchAccount);
            }

            // 3. Action bar below dgvAccounts
            if (btnUpdateAccount == null)
            {
                int barHeight = 38;
                dgvAccounts.Height = dgvAccounts.Parent.ClientSize.Height - dgvAccounts.Top - barHeight - 8;

                FlowLayoutPanel actionPanel = new FlowLayoutPanel
                {
                    Location = new Point(dgvAccounts.Left, dgvAccounts.Bottom + 6),
                    Size = new Size(dgvAccounts.Width, barHeight),
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                    FlowDirection = FlowDirection.LeftToRight,
                    WrapContents = false,
                    BackColor = Color.Transparent
                };

                btnUpdateAccount = CreateActionButton("Update", Color.FromArgb(184, 115, 51), 110);
                btnUpdateAccount.Click += BtnUpdateAccount_Click;

                btnResetPassword = CreateActionButton("Reset Pass", Color.FromArgb(184, 115, 51), 115);
                btnResetPassword.Click += BtnResetPassword_Click;

                btnToggleActive = CreateActionButton("Deactivate", Color.FromArgb(178, 58, 58), 115);
                btnToggleActive.Click += BtnToggleActive_Click;

                btnClearSelection = CreateActionButton("Clear", Color.FromArgb(115, 110, 105), 95);
                btnClearSelection.Click += (s, e) => ClearInputFields();

                actionPanel.Controls.Add(btnUpdateAccount);
                actionPanel.Controls.Add(btnResetPassword);
                actionPanel.Controls.Add(btnToggleActive);
                actionPanel.Controls.Add(btnClearSelection);

                dgvAccounts.Parent.Controls.Add(actionPanel);
                UpdateActionButtonStates();
            }
        }

        private Button CreateActionButton(string text, Color backColor, int width = 105)
        {
            Button btn = new Button
            {
                Text = text,
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Height = 36,
                Width = width,
                Margin = new Padding(0, 0, 8, 0),
                Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void FormatAccountsGrid()
        {
            dgvAccounts.AutoGenerateColumns = false;
            dgvAccounts.Columns.Clear();

            dgvAccounts.RowHeadersVisible = false;
            dgvAccounts.AllowUserToAddRows = false;
            dgvAccounts.AllowUserToDeleteRows = false;
            dgvAccounts.AllowUserToResizeRows = false;
            dgvAccounts.ReadOnly = true;
            dgvAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAccounts.MultiSelect = false;

            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "username",
                DataPropertyName = "username",
                HeaderText = "Username",
                Width = 110
            });

            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NameOfUser",
                DataPropertyName = "NameOfUser",
                HeaderText = "Full Name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 130
            });

            DataGridViewTextBoxColumn colRole = new DataGridViewTextBoxColumn
            {
                Name = "AccessLevel",
                DataPropertyName = "AccessLevel",
                HeaderText = "Role",
                Width = 85
            };
            colRole.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAccounts.Columns.Add(colRole);

            DataGridViewTextBoxColumn colStatus = new DataGridViewTextBoxColumn
            {
                Name = "StatusText",
                DataPropertyName = "StatusText",
                HeaderText = "Status",
                Width = 85
            };
            colStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAccounts.Columns.Add(colStatus);

            BakeryPOS.UI.Styles.ApplyGridStyle(dgvAccounts);

            dgvAccounts.CellFormatting -= DgvAccounts_CellFormatting;
            dgvAccounts.CellFormatting += DgvAccounts_CellFormatting;

            dgvAccounts.CellClick -= DgvAccounts_CellClick;
            dgvAccounts.CellClick += DgvAccounts_CellClick;
        }

        private void DgvAccounts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value == null) return;

            string colName = dgvAccounts.Columns[e.ColumnIndex].Name;
            if (colName == "AccessLevel")
            {
                string val = e.Value.ToString().Trim();
                e.Value = val.Equals("Admin", StringComparison.OrdinalIgnoreCase) ? "Admin" : "Cashier";
                e.FormattingApplied = true;
            }
            else if (colName == "StatusText")
            {
                string status = e.Value.ToString();
                e.CellStyle.ForeColor = status == "Active" ? Color.ForestGreen : Color.Firebrick;
                e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }
        }

        private void DgvAccounts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvAccounts.Rows[e.RowIndex].DataBoundItem == null)
                return;

            DataRowView drv = (DataRowView)dgvAccounts.Rows[e.RowIndex].DataBoundItem;
            _selectedUserId = Convert.ToInt32(drv["user_id"]);
            _selectedIsActive = drv["IsActive"] != DBNull.Value && Convert.ToBoolean(drv["IsActive"]);
            _selectedRowVer = drv["RowVer"] == DBNull.Value ? null : (byte[])drv["RowVer"];

            txtFullName.Text = drv["NameOfUser"].ToString();
            txtUsername.Text = drv["username"].ToString();
            txtPassword.Clear();

            string role = drv["AccessLevel"].ToString().Trim();
            cmbRole.SelectedItem = role.Equals("Admin", StringComparison.OrdinalIgnoreCase) ? "Admin" : "Cashier";

            UpdateActionButtonStates();
        }

        private void UpdateActionButtonStates()
        {
            bool hasSelection = _selectedUserId > 0;
            if (btnUpdateAccount != null) btnUpdateAccount.Enabled = hasSelection;
            if (btnResetPassword != null) btnResetPassword.Enabled = hasSelection;
            if (btnToggleActive != null)
            {
                btnToggleActive.Enabled = hasSelection;
                btnToggleActive.Text = _selectedIsActive ? "Deactivate" : "Reactivate";
                btnToggleActive.BackColor = _selectedIsActive ? Color.FromArgb(178, 58, 58) : Color.FromArgb(101, 61, 34);
            }
        }

        private void LoadAccounts()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        SELECT user_id, username, NameOfUser, AccessLevel,
                               ISNULL(IsActive, 1) AS IsActive,
                               CASE WHEN ISNULL(IsActive, 1) = 1 THEN 'Active' ELSE 'Inactive' END AS StatusText,
                               RowVer
                        FROM Accounts
                        ORDER BY user_id ASC";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        _accountsTable = new DataTable();
                        adapter.Fill(_accountsTable);
                        dgvAccounts.DataSource = _accountsTable;
                        ApplyAccountFilter();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmManageAccounts.LoadAccounts", ex);
                MessageBox.Show(
                    "We couldn't load the accounts. Please try again.",
                    "Something Went Wrong",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void ApplyAccountFilter()
        {
            if (_accountsTable == null || txtSearchAccount == null) return;

            string term = txtSearchAccount.Text.Trim().Replace("'", "''");
            if (string.IsNullOrEmpty(term))
            {
                _accountsTable.DefaultView.RowFilter = string.Empty;
            }
            else
            {
                _accountsTable.DefaultView.RowFilter =
                    $"username LIKE '%{term}%' OR NameOfUser LIKE '%{term}%' OR AccessLevel LIKE '%{term}%' OR StatusText LIKE '%{term}%'";
            }
        }

        private void btnTogglePass_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '*')
            {
                txtPassword.PasswordChar = '\0';
                btnTogglePass.Text = "🙈";
            }
            else
            {
                txtPassword.PasswordChar = '*';
                btnTogglePass.Text = "👁";
            }
            txtPassword.Focus();
        }

        private void ClearInputFields()
        {
            _selectedUserId = 0;
            _selectedIsActive = true;
            _selectedRowVer = null;

            txtFullName.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtPassword.PasswordChar = '*';
            btnTogglePass.Text = "👁";
            if (cmbRole != null) cmbRole.SelectedIndex = 0;

            dgvAccounts.ClearSelection();
            UpdateActionButtonStates();
            txtFullName.Focus();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string role = cmbRole != null && cmbRole.SelectedItem != null ? cmbRole.SelectedItem.ToString() : "Cashier";

            if (string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Please enter the user's Full Name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter a Username.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {
                MessageBox.Show("Please enter a Password (at least 6 characters).", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Create new {role} account for '{fullName}' ({username})?",
                "Confirm Account Creation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (confirm != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Accounts WHERE LOWER(username) = LOWER(@username)";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@username", username);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (count > 0)
                        {
                            MessageBox.Show(
                                "Username '" + username + "' already exists. Please choose a different username.",
                                "Duplicate Username",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                            txtUsername.Focus();
                            txtUsername.SelectAll();
                            return;
                        }
                    }

                    var hashed = SecurityHelper.HashPassword(password);
                    string email = username.ToLower() + "@bakerypos.com";

                    string insertQuery = @"
                        INSERT INTO Accounts (username, password, PasswordHash, PasswordSalt, NameOfUser, AccessLevel, EmailAddress, IsActive, FailedLoginAttempts)
                        VALUES (@username, '***HASHED***', @PasswordHash, @PasswordSalt, @NameOfUser, @AccessLevel, @EmailAddress, 1, 0);
                        SELECT SCOPE_IDENTITY();";

                    int newUserId;
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@PasswordHash", hashed.Hash);
                        cmd.Parameters.AddWithValue("@PasswordSalt", hashed.Salt);
                        cmd.Parameters.AddWithValue("@NameOfUser", fullName);
                        cmd.Parameters.AddWithValue("@AccessLevel", role);
                        cmd.Parameters.AddWithValue("@EmailAddress", email);

                        newUserId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    LogAudit(conn, "ADD", newUserId);

                    MessageBox.Show(
                        $"The {role} account was created successfully with salted password hashing.",
                        "Account Created",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    ClearInputFields();
                    LoadAccounts();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmManageAccounts.btnCreate_Click", ex);
                MessageBox.Show(
                    "The system couldn't complete your request. Please try again.",
                    "Something Went Wrong",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void BtnUpdateAccount_Click(object sender, EventArgs e)
        {
            if (_selectedUserId <= 0)
            {
                MessageBox.Show("Please select an account from the list first.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fullName = txtFullName.Text.Trim();
            string username = txtUsername.Text.Trim();
            string role = cmbRole != null && cmbRole.SelectedItem != null ? cmbRole.SelectedItem.ToString() : "Cashier";

            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Full Name and Username are required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Save changes to account '{username}' (Full Name: {fullName}, Role: {role})?",
                "Confirm Update",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (confirm != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();

                    string dupQuery = "SELECT COUNT(*) FROM Accounts WHERE LOWER(username) = LOWER(@username) AND user_id <> @userId";
                    using (SqlCommand dupCmd = new SqlCommand(dupQuery, conn))
                    {
                        dupCmd.Parameters.AddWithValue("@username", username);
                        dupCmd.Parameters.AddWithValue("@userId", _selectedUserId);
                        if (Convert.ToInt32(dupCmd.ExecuteScalar()) > 0)
                        {
                            MessageBox.Show("Another account already uses that username.", "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string updateQuery = @"
                        UPDATE Accounts
                        SET NameOfUser = @NameOfUser,
                            username = @username,
                            AccessLevel = @AccessLevel
                        WHERE user_id = @userId
                          AND (@RowVer IS NULL OR RowVer = @RowVer)";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@NameOfUser", fullName);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@AccessLevel", role);
                        cmd.Parameters.AddWithValue("@userId", _selectedUserId);

                        SqlParameter rvParam = cmd.Parameters.Add("@RowVer", SqlDbType.Timestamp);
                        rvParam.Value = (object)_selectedRowVer ?? DBNull.Value;

                        int rows = cmd.ExecuteNonQuery();
                        if (rows == 0)
                        {
                            MessageBox.Show(
                                "This account was modified by another user while you were editing it. Refreshing the list.",
                                "Concurrency Conflict",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                            LoadAccounts();
                            return;
                        }
                    }

                    LogAudit(conn, "UPDATE", _selectedUserId);

                    MessageBox.Show("Account updated successfully.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputFields();
                    LoadAccounts();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmManageAccounts.BtnUpdateAccount_Click", ex);
                MessageBox.Show("Could not update account. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnResetPassword_Click(object sender, EventArgs e)
        {
            if (_selectedUserId <= 0)
            {
                MessageBox.Show("Please select an account from the list first.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newPassword = txtPassword.Text;
            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
            {
                MessageBox.Show("Enter the new password (at least 6 characters) in the Password box first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Reset password and clear any lockout for '{txtUsername.Text.Trim()}'?",
                "Confirm Password Reset",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (confirm != DialogResult.Yes) return;

            try
            {
                var hashed = SecurityHelper.HashPassword(newPassword);

                using (SqlConnection conn = new SqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        UPDATE Accounts
                        SET PasswordHash = @Hash,
                            PasswordSalt = @Salt,
                            password = '***HASHED***',
                            FailedLoginAttempts = 0,
                            LockoutUntil = NULL
                        WHERE user_id = @userId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Hash", hashed.Hash);
                        cmd.Parameters.AddWithValue("@Salt", hashed.Salt);
                        cmd.Parameters.AddWithValue("@userId", _selectedUserId);
                        cmd.ExecuteNonQuery();
                    }

                    LogAudit(conn, "UPDATE", _selectedUserId);

                    MessageBox.Show("Password reset successfully.", "Password Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputFields();
                    LoadAccounts();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmManageAccounts.BtnResetPassword_Click", ex);
                MessageBox.Show("Could not reset password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnToggleActive_Click(object sender, EventArgs e)
        {
            if (_selectedUserId <= 0)
            {
                MessageBox.Show("Please select an account from the list first.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtUsername.Text.Trim().Equals(_adminUsername, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("You cannot deactivate your own currently logged-in account.", "Action Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool targetActive = !_selectedIsActive;
            string verb = targetActive ? "reactivate" : "deactivate";

            DialogResult confirm = MessageBox.Show(
                $"Are you sure you want to {verb} account '{txtUsername.Text.Trim()}'?",
                "Confirm Status Change",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (confirm != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        UPDATE Accounts
                        SET IsActive = @IsActive,
                            FailedLoginAttempts = 0,
                            LockoutUntil = NULL
                        WHERE user_id = @userId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IsActive", targetActive);
                        cmd.Parameters.AddWithValue("@userId", _selectedUserId);
                        cmd.ExecuteNonQuery();
                    }

                    LogAudit(conn, targetActive ? "REACTIVATE" : "DEACTIVATE", _selectedUserId);

                    MessageBox.Show($"Account {verb}d successfully.", "Status Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputFields();
                    LoadAccounts();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmManageAccounts.BtnToggleActive_Click", ex);
                MessageBox.Show("Could not change account status. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LogAudit(SqlConnection conn, string action, int recordId)
        {
            try
            {
                string auditQuery = @"
                    INSERT INTO AuditLogs (Username, Action, Module, RecordID, ActionDate)
                    VALUES (@Username, @Action, 'Account', @RecordID, GETDATE())";

                using (SqlCommand auditCmd = new SqlCommand(auditQuery, conn))
                {
                    auditCmd.Parameters.AddWithValue("@Username", _adminUsername);
                    auditCmd.Parameters.AddWithValue("@Action", action);
                    auditCmd.Parameters.AddWithValue("@RecordID", recordId);
                    auditCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmManageAccounts.LogAudit", ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}