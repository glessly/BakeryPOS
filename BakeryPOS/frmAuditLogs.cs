using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BakeryPOS
{
    public partial class frmAuditLogs : Form
    {
        private readonly string _accessLevel;
        private ComboBox cmbUserFilter;
        private ComboBox cmbActionFilter;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private CheckBox chkDateFilter;

        public frmAuditLogs() : this("Admin")
        {
        }

        public frmAuditLogs(string accessLevel)
        {
            InitializeComponent();
            _accessLevel = accessLevel ?? "Cashier";
            this.Load += frmAuditLogs_Load;
        }

        private void frmAuditLogs_Load(object sender, EventArgs e)
        {
            if (!_accessLevel.Trim().Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "Access denied. Only Administrators can view Audit Logs.",
                    "Access Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                this.Close();
                return;
            }

            InitializeFilterControls();
            LoadFilterDropdowns();
            LoadAuditLogs();
        }

        private void InitializeFilterControls()
        {
            if (cmbUserFilter != null || dgvAuditLogs == null || dgvAuditLogs.Parent == null)
                return;

            Color darkBrown = Color.FromArgb(50, 22, 8);

            Panel pnlFilters = new Panel
            {
                Height = 44,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(245, 236, 226),
                Padding = new Padding(30, 8, 30, 8)
            };

            FlowLayoutPanel flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.Transparent
            };

            Label lblUser = new Label
            {
                Text = "User:",
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = darkBrown,
                Margin = new Padding(0, 6, 6, 0)
            };
            cmbUserFilter = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 145,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                Margin = new Padding(0, 1, 20, 0)
            };
            cmbUserFilter.SelectedIndexChanged += (s, e) => LoadAuditLogs();

            Label lblAction = new Label
            {
                Text = "Action:",
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = darkBrown,
                Margin = new Padding(0, 6, 6, 0)
            };
            cmbActionFilter = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 145,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                Margin = new Padding(0, 1, 20, 0)
            };
            cmbActionFilter.Items.AddRange(new object[]
            {
                "ALL", "ADD", "UPDATE", "DELETE", "VOID", "DEACTIVATE", "REACTIVATE", "RESTORE", "LOGIN", "LOGOUT", "FAILED_LOGIN", "BACKUP"
            });
            cmbActionFilter.SelectedIndex = 0;
            cmbActionFilter.SelectedIndexChanged += (s, e) => LoadAuditLogs();

            chkDateFilter = new CheckBox
            {
                Text = "Date Range:",
                Checked = false,
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = darkBrown,
                Margin = new Padding(0, 4, 8, 0)
            };
            chkDateFilter.CheckedChanged += (s, e) =>
            {
                dtpFrom.Enabled = chkDateFilter.Checked;
                dtpTo.Enabled = chkDateFilter.Checked;
                LoadAuditLogs();
            };

            dtpFrom = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                Width = 120,
                Enabled = false,
                Value = DateTime.Today.AddDays(-7),
                Margin = new Padding(0, 1, 8, 0)
            };
            dtpFrom.ValueChanged += (s, e) => LoadAuditLogs();

            Label lblTo = new Label
            {
                Text = "to",
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = darkBrown,
                Margin = new Padding(0, 6, 8, 0)
            };

            dtpTo = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                Width = 120,
                Enabled = false,
                Value = DateTime.Today,
                Margin = new Padding(0, 1, 8, 0)
            };
            dtpTo.ValueChanged += (s, e) => LoadAuditLogs();

            flow.Controls.AddRange(new Control[] { lblUser, cmbUserFilter, lblAction, cmbActionFilter, chkDateFilter, dtpFrom, lblTo, dtpTo });
            pnlFilters.Controls.Add(flow);

            Control parent = dgvAuditLogs.Parent;
            parent.Controls.Add(pnlFilters);
            pnlFilters.BringToFront();

            int searchRowTop = pnlFilters.Bottom + 12;
            int searchRowHeight = btnRefreshAudit.Height;

            btnRefreshAudit.Top = searchRowTop;
            txtAuditSearch.Top = searchRowTop + (searchRowHeight - txtAuditSearch.Height) / 2;
            LABEL1.Top = searchRowTop + (searchRowHeight - LABEL1.Height) / 2;

            LABEL1.Left = dgvAuditLogs.Left;
            txtAuditSearch.Left = LABEL1.Right + 10;
            btnRefreshAudit.Left = txtAuditSearch.Right + 12;

            int gridTop = btnRefreshAudit.Bottom + 12;
            int gridBottom = btnCloseAudit.Top - 12;
            dgvAuditLogs.Top = gridTop;
            if (gridBottom > gridTop)
            {
                dgvAuditLogs.Height = gridBottom - gridTop;
            }

            LABEL1.BringToFront();
            txtAuditSearch.BringToFront();
            btnRefreshAudit.BringToFront();
            dgvAuditLogs.BringToFront();
            btnCloseAudit.BringToFront();
        }

        private void LoadFilterDropdowns()
        {
            if (cmbUserFilter == null) return;
            try
            {
                cmbUserFilter.Items.Clear();
                cmbUserFilter.Items.Add("ALL");
                using (SqlConnection conn = new SqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT Username FROM AuditLogs WHERE Username IS NOT NULL ORDER BY Username", conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbUserFilter.Items.Add(reader["Username"].ToString());
                        }
                    }
                }
                cmbUserFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmAuditLogs.LoadFilterDropdowns", ex);
            }
        }

        private void txtAuditSearch_TextChanged(object sender, EventArgs e)
        {
            LoadAuditLogs();
        }

        private void FormatAuditGrid()
        {
            if (dgvAuditLogs.Columns["AuditID"] != null)
            {
                dgvAuditLogs.Columns["AuditID"].HeaderText = "Audit ID";
                dgvAuditLogs.Columns["AuditID"].Width = 90;
            }
            if (dgvAuditLogs.Columns["Username"] != null)
            {
                dgvAuditLogs.Columns["Username"].HeaderText = "User";
                dgvAuditLogs.Columns["Username"].Width = 140;
            }
            if (dgvAuditLogs.Columns["Action"] != null)
            {
                dgvAuditLogs.Columns["Action"].HeaderText = "Action";
                dgvAuditLogs.Columns["Action"].Width = 140;
            }
            if (dgvAuditLogs.Columns["Module"] != null)
            {
                dgvAuditLogs.Columns["Module"].HeaderText = "Module";
                dgvAuditLogs.Columns["Module"].Width = 135;
            }
            if (dgvAuditLogs.Columns["RecordID"] != null)
            {
                dgvAuditLogs.Columns["RecordID"].HeaderText = "Record ID";
                dgvAuditLogs.Columns["RecordID"].Width = 105;
            }
            if (dgvAuditLogs.Columns["ActionDate"] != null)
            {
                dgvAuditLogs.Columns["ActionDate"].HeaderText = "Date & Time";
                dgvAuditLogs.Columns["ActionDate"].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm:ss tt";
                dgvAuditLogs.Columns["ActionDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            dgvAuditLogs.ReadOnly = true;
            dgvAuditLogs.ScrollBars = ScrollBars.Vertical;
            BakeryPOS.UI.Styles.ApplyGridStyle(dgvAuditLogs);
        }

        private void LoadAuditLogs()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();

                    List<string> conditions = new List<string>();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;

                        string search = txtAuditSearch != null ? txtAuditSearch.Text.Trim() : "";
                        if (!string.IsNullOrEmpty(search))
                        {
                            conditions.Add("(Username LIKE @Search OR Action LIKE @Search OR Module LIKE @Search OR CAST(RecordID AS VARCHAR) LIKE @Search)");
                            cmd.Parameters.AddWithValue("@Search", "%" + search + "%");
                        }

                        if (cmbUserFilter != null && cmbUserFilter.SelectedItem != null && cmbUserFilter.SelectedItem.ToString() != "ALL")
                        {
                            conditions.Add("Username = @FilterUser");
                            cmd.Parameters.AddWithValue("@FilterUser", cmbUserFilter.SelectedItem.ToString());
                        }

                        if (cmbActionFilter != null && cmbActionFilter.SelectedItem != null && cmbActionFilter.SelectedItem.ToString() != "ALL")
                        {
                            conditions.Add("Action = @FilterAction");
                            cmd.Parameters.AddWithValue("@FilterAction", cmbActionFilter.SelectedItem.ToString());
                        }

                        if (chkDateFilter != null && chkDateFilter.Checked)
                        {
                            conditions.Add("CAST(ActionDate AS DATE) >= @DateFrom AND CAST(ActionDate AS DATE) <= @DateTo");
                            cmd.Parameters.Add("@DateFrom", SqlDbType.Date).Value = dtpFrom.Value.Date;
                            cmd.Parameters.Add("@DateTo", SqlDbType.Date).Value = dtpTo.Value.Date;
                        }

                        string whereClause = conditions.Count > 0 ? "WHERE " + string.Join(" AND ", conditions) : "";
                        cmd.CommandText = $@"
                            SELECT AuditID, Username, Action, Module, RecordID, ActionDate
                            FROM AuditLogs
                            {whereClause}
                            ORDER BY ActionDate DESC";

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            dgvAuditLogs.DataSource = table;
                            FormatAuditGrid();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("frmAuditLogs.LoadAuditLogs", ex);
                MessageBox.Show(
                    "We couldn't load the audit logs. Please try again.",
                    "Something Went Wrong",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnRefreshAudit_Click(object sender, EventArgs e)
        {
            if (txtAuditSearch != null) txtAuditSearch.Clear();
            if (cmbUserFilter != null && cmbUserFilter.Items.Count > 0) cmbUserFilter.SelectedIndex = 0;
            if (cmbActionFilter != null && cmbActionFilter.Items.Count > 0) cmbActionFilter.SelectedIndex = 0;
            if (chkDateFilter != null) chkDateFilter.Checked = false;
            LoadFilterDropdowns();
            LoadAuditLogs();
        }

        private void btnCloseAudit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}