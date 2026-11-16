using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BakeryPOS
{
    public partial class frmLogin : Form
    {
        private const int MaxFailedAttempts = 3;
        private const int LockoutMinutes = 15;

        public frmLogin()
        {
            InitializeComponent();
            this.Shown += frmLogin_Shown;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtUsername;
            txtUsername.Focus();
        }

        private void frmLogin_Shown(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to exit?",
                    "Exit Application",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtUsername_TextChanged(object sender, EventArgs e) { }
        private void txtPassword_TextChanged(object sender, EventArgs e) { }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!btnLogin.Enabled)
                return;

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter your username.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter your password.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            bool loginSucceeded = false;

            try
            {
                btnLogin.Enabled = false;
                Cursor = Cursors.WaitCursor;

                using (SqlConnection conn = new SqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();

                    string query = @"
                        SELECT user_id, NameOfUser, username, AccessLevel,
                               password, PasswordHash, PasswordSalt,
                               FailedLoginAttempts, LockoutUntil, IsActive
                        FROM Accounts
                        WHERE username = @username";

                    int userId = 0;
                    string nameOfUser = "";
                    string loggedUsername = "";
                    string accessLevel = "";
                    string legacyPlainPassword = "";
                    string passwordHash = "";
                    string passwordSalt = "";
                    int failedAttempts = 0;
                    DateTime? lockoutUntil = null;
                    bool isActive = true;
                    bool userExists = false;

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userExists = true;
                                userId = Convert.ToInt32(reader["user_id"]);
                                nameOfUser = reader["NameOfUser"].ToString();
                                loggedUsername = reader["username"].ToString();
                                accessLevel = reader["AccessLevel"].ToString();
                                legacyPlainPassword = reader["password"] == DBNull.Value ? "" : reader["password"].ToString();
                                passwordHash = reader["PasswordHash"] == DBNull.Value ? "" : reader["PasswordHash"].ToString();
                                passwordSalt = reader["PasswordSalt"] == DBNull.Value ? "" : reader["PasswordSalt"].ToString();
                                failedAttempts = reader["FailedLoginAttempts"] == DBNull.Value ? 0 : Convert.ToInt32(reader["FailedLoginAttempts"]);
                                lockoutUntil = reader["LockoutUntil"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["LockoutUntil"]);
                                isActive = reader["IsActive"] == DBNull.Value ? true : Convert.ToBoolean(reader["IsActive"]);
                            }
                        }
                    }

                    // 1. Unknown username
                    if (!userExists)
                    {
                        LogAudit(conn, username, "FAILED_LOGIN", "Accounts", 0);
                        ShowFailedLogin("Incorrect username or password. Please try again.");
                        return;
                    }

                    // 2. Deactivated account check
                    if (!isActive)
                    {
                        LogAudit(conn, loggedUsername, "FAILED_LOGIN", "Accounts", userId);
                        ShowFailedLogin("This account has been deactivated. Please contact the Administrator.");
                        return;
                    }

                    // 3. Account lockout check
                    if (lockoutUntil.HasValue && lockoutUntil.Value > DateTime.Now)
                    {
                        int minsLeft = (int)Math.Ceiling((lockoutUntil.Value - DateTime.Now).TotalMinutes);
                        LogAudit(conn, loggedUsername, "FAILED_LOGIN", "Accounts", userId);
                        ShowFailedLogin($"Account is temporarily locked due to multiple failed login attempts.\nPlease try again in {minsLeft} minute(s).");
                        return;
                    }

                    // 4. Verify salted hash (and auto-migrate legacy plaintext row on first valid login)
                    bool passwordValid = false;
                    bool needsHashMigration = false;

                    if (!string.IsNullOrEmpty(passwordHash) && !string.IsNullOrEmpty(passwordSalt))
                    {
                        passwordValid = SecurityHelper.VerifyPassword(password, passwordHash, passwordSalt);
                    }
                    else if (!string.IsNullOrEmpty(legacyPlainPassword) && password == legacyPlainPassword)
                    {
                        passwordValid = true;
                        needsHashMigration = true;
                    }

                    if (passwordValid)
                    {
                        loginSucceeded = true;

                        if (needsHashMigration)
                        {
                            var hashed = SecurityHelper.HashPassword(password);
                            using (SqlCommand migrateCmd = new SqlCommand(@"
                                UPDATE Accounts
                                SET PasswordHash = @Hash,
                                    PasswordSalt = @Salt,
                                    password = '***HASHED***',
                                    FailedLoginAttempts = 0,
                                    LockoutUntil = NULL
                                WHERE user_id = @UserId", conn))
                            {
                                migrateCmd.Parameters.AddWithValue("@Hash", hashed.Hash);
                                migrateCmd.Parameters.AddWithValue("@Salt", hashed.Salt);
                                migrateCmd.Parameters.AddWithValue("@UserId", userId);
                                migrateCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            using (SqlCommand resetCmd = new SqlCommand(@"
                                UPDATE Accounts
                                SET FailedLoginAttempts = 0,
                                    LockoutUntil = NULL
                                WHERE user_id = @UserId", conn))
                            {
                                resetCmd.Parameters.AddWithValue("@UserId", userId);
                                resetCmd.ExecuteNonQuery();
                            }
                        }

                        LogAudit(conn, loggedUsername, "LOGIN", "Accounts", userId);

                        MessageBox.Show(
                            "Welcome, " + nameOfUser + "!\n\n" +
                            "Username: " + loggedUsername + "\n" +
                            "Role: " + accessLevel,
                            "Login Successful",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        frmPOS posForm = new frmPOS(nameOfUser, loggedUsername, accessLevel);
                        posForm.FormClosed += (s, args) => this.Close();
                        posForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        failedAttempts++;
                        bool shouldLock = failedAttempts >= MaxFailedAttempts;

                        using (SqlCommand failCmd = new SqlCommand(@"
                            UPDATE Accounts
                            SET FailedLoginAttempts = @Attempts,
                                LockoutUntil = @LockoutUntil
                            WHERE user_id = @UserId", conn))
                        {
                            failCmd.Parameters.AddWithValue("@Attempts", failedAttempts);
                            failCmd.Parameters.AddWithValue("@LockoutUntil", shouldLock ? (object)DateTime.Now.AddMinutes(LockoutMinutes) : DBNull.Value);
                            failCmd.Parameters.AddWithValue("@UserId", userId);
                            failCmd.ExecuteNonQuery();
                        }

                        LogAudit(conn, loggedUsername, "FAILED_LOGIN", "Accounts", userId);

                        if (shouldLock)
                        {
                            ShowFailedLogin($"Account locked after {MaxFailedAttempts} consecutive failed attempts. Try again in {LockoutMinutes} minutes.");
                        }
                        else
                        {
                            int remaining = MaxFailedAttempts - failedAttempts;
                            ShowFailedLogin($"Incorrect username or password. {remaining} attempt(s) remaining before account lockout.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmLogin.btnLogin_Click", ex);
                MessageBox.Show(
                    "We couldn't sign you in due to a system error. Please try again.",
                    "Something Went Wrong",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                Cursor = Cursors.Default;
                if (!loginSucceeded)
                {
                    btnLogin.Enabled = true;
                }
            }
        }

        private void ShowFailedLogin(string message)
        {
            MessageBox.Show(message, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtPassword.Clear();
            txtPassword.PasswordChar = '*';
            btnTogglePassword.Text = "👁";
            txtPassword.Focus();
        }

        private void LogAudit(SqlConnection conn, string username, string action, string module, int recordId)
        {
            try
            {
                using (SqlCommand auditCmd = new SqlCommand(@"
                    INSERT INTO AuditLogs (Username, Action, Module, RecordID, ActionDate)
                    VALUES (@Username, @Action, @Module, @RecordID, GETDATE())", conn))
                {
                    auditCmd.Parameters.AddWithValue("@Username", username);
                    auditCmd.Parameters.AddWithValue("@Action", action);
                    auditCmd.Parameters.AddWithValue("@Module", module);
                    auditCmd.Parameters.AddWithValue("@RecordID", recordId);
                    auditCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmLogin.LogAudit", ex);
            }
        }

        private void picLogo_Click(object sender, EventArgs e) { }
        private void lblTitle_Click(object sender, EventArgs e) { }
        private void lblSubtitle_Click(object sender, EventArgs e) { }
        private void lblUsername_Click(object sender, EventArgs e) { }

        private void btnTogglePassword_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '*')
            {
                txtPassword.PasswordChar = '\0';
                btnTogglePassword.Text = "🙈";
            }
            else
            {
                txtPassword.PasswordChar = '*';
                btnTogglePassword.Text = "👁";
            }
            txtPassword.Focus();
        }
    }
}