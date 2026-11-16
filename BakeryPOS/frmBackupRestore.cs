using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using BakeryPOS.UI;

namespace BakeryPOS
{
    public partial class frmBackupRestore : Form
    {
        private readonly string _adminUsername;
        private TextBox txtBackupPath, txtRestorePath;

        public frmBackupRestore() : this("admin") { }

        public frmBackupRestore(string adminUsername)
        {
            InitializeComponent();
            _adminUsername = string.IsNullOrWhiteSpace(adminUsername) ? "admin" : adminUsername;
            BuildUI();
        }

        private void BuildUI()
        {
            this.Text = "Bakery POS - Database Backup & Restore (Admin Only)";
            this.ClientSize = new Size(600, 405);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(253, 245, 236);

            Panel pnlHeader = Styles.CreateHeaderBanner("DATABASE BACKUP & RESTORE");

            // Backup Group
            GroupBox grpBackup = new GroupBox
            {
                Text = "1. Backup Database (BakeryPOS_DB)",
                Left = 18,
                Top = 100,
                Width = 564,
                Height = 135,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(123, 63, 26)
            };
            txtBackupPath = new TextBox
            {
                Left = 16,
                Top = 38,
                Width = 420,
                ReadOnly = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.FromArgb(50, 22, 8)
            };
            Button btnBrowseBackup = new Button
            {
                Text = "Browse...",
                Left = 446,
                Top = 33,
                Width = 102,
                Height = 36,
                BackColor = Color.FromArgb(184, 115, 51),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false
            };
            btnBrowseBackup.FlatAppearance.BorderSize = 0;
            btnBrowseBackup.Click += (s, e) =>
            {
                using (SaveFileDialog sfd = new SaveFileDialog { Filter = "SQL Backup (*.bak)|*.bak", FileName = $"BakeryPOS_DB_{DateTime.Now:yyyyMMdd_HHmm}.bak" })
                {
                    if (sfd.ShowDialog() == DialogResult.OK) txtBackupPath.Text = sfd.FileName;
                }
            };

            Button btnRunBackup = new Button
            {
                Text = "Create Backup (.bak)",
                Left = 16,
                Top = 82,
                Width = 532,
                Height = 36,
                BackColor = Color.FromArgb(101, 61, 34),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false
            };
            btnRunBackup.FlatAppearance.BorderSize = 0;
            btnRunBackup.Click += BtnRunBackup_Click;
            grpBackup.Controls.AddRange(new Control[] { txtBackupPath, btnBrowseBackup, btnRunBackup });

            // Restore Group
            GroupBox grpRestore = new GroupBox
            {
                Text = "2. Restore Database from Backup (.bak)",
                Left = 18,
                Top = 250,
                Width = 564,
                Height = 135,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(123, 63, 26)
            };
            txtRestorePath = new TextBox
            {
                Left = 16,
                Top = 38,
                Width = 420,
                ReadOnly = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.FromArgb(50, 22, 8)
            };
            Button btnBrowseRestore = new Button
            {
                Text = "Browse...",
                Left = 446,
                Top = 33,
                Width = 102,
                Height = 36,
                BackColor = Color.FromArgb(184, 115, 51),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false
            };
            btnBrowseRestore.FlatAppearance.BorderSize = 0;
            btnBrowseRestore.Click += (s, e) =>
            {
                using (OpenFileDialog ofd = new OpenFileDialog { Filter = "SQL Backup (*.bak)|*.bak" })
                {
                    if (ofd.ShowDialog() == DialogResult.OK) txtRestorePath.Text = ofd.FileName;
                }
            };

            Button btnRunRestore = new Button
            {
                Text = "Restore Database (Overwrites Current Data)",
                Left = 16,
                Top = 82,
                Width = 532,
                Height = 36,
                BackColor = Color.FromArgb(178, 58, 58),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false
            };
            btnRunRestore.FlatAppearance.BorderSize = 0;
            btnRunRestore.Click += BtnRunRestore_Click;
            grpRestore.Controls.AddRange(new Control[] { txtRestorePath, btnBrowseRestore, btnRunRestore });

            this.Controls.AddRange(new Control[] { grpBackup, grpRestore, pnlHeader });
        }

        private void BtnRunBackup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBackupPath.Text))
            {
                MessageBox.Show("Please select a destination file path first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Backup BakeryPOS_DB to:\n{txtBackupPath.Text}?", "Confirm Backup", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("BACKUP DATABASE [BakeryPOS_DB] TO DISK = @Path WITH INIT", conn))
                    {
                        cmd.Parameters.AddWithValue("@Path", txtBackupPath.Text);
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand auditCmd = new SqlCommand("INSERT INTO AuditLogs (Username, Action, Module, RecordID, ActionDate) VALUES (@U, 'BACKUP', 'Database', 0, GETDATE())", conn))
                    {
                        auditCmd.Parameters.AddWithValue("@U", _adminUsername);
                        auditCmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Database backup completed successfully!", "Backup Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmBackupRestore.BtnRunBackup_Click", ex);
                MessageBox.Show("Database backup failed. Ensure SQL Server has write permission to the chosen folder (e.g., C:\\Backup or C:\\Users\\Public).", "Backup Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRunRestore_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRestorePath.Text))
            {
                MessageBox.Show("Please select a .bak file to restore.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("WARNING: Restoring will overwrite ALL current data in BakeryPOS_DB with the selected backup.\n\nAre you sure you want to continue?", "Confirm Database Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(DbHelper.GetConnectionString())
                {
                    InitialCatalog = "master"
                };

                using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand singleCmd = new SqlCommand("ALTER DATABASE [BakeryPOS_DB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", conn))
                        singleCmd.ExecuteNonQuery();

                    using (SqlCommand restoreCmd = new SqlCommand("RESTORE DATABASE [BakeryPOS_DB] FROM DISK = @Path WITH REPLACE", conn))
                    {
                        restoreCmd.Parameters.AddWithValue("@Path", txtRestorePath.Text);
                        restoreCmd.ExecuteNonQuery();
                    }

                    using (SqlCommand multiCmd = new SqlCommand("ALTER DATABASE [BakeryPOS_DB] SET MULTI_USER", conn))
                        multiCmd.ExecuteNonQuery();
                }

                using (SqlConnection logConn = new SqlConnection(DbHelper.GetConnectionString()))
                {
                    logConn.Open();
                    using (SqlCommand auditCmd = new SqlCommand("INSERT INTO AuditLogs (Username, Action, Module, RecordID, ActionDate) VALUES (@U, 'RESTORE', 'Database', 0, GETDATE())", logConn))
                    {
                        auditCmd.Parameters.AddWithValue("@U", _adminUsername);
                        auditCmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Database restored successfully!", "Restore Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmBackupRestore.BtnRunRestore_Click", ex);
                MessageBox.Show("Database restore failed. Please check the backup file and try again.", "Restore Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}