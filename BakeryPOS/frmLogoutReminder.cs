using System;
using System.Drawing;
using System.Windows.Forms;

namespace BakeryPOS
{
    public class frmLogoutReminder : Form
    {
        public enum ReminderChoice
        {
            Cancel,
            Logout,
            GenerateReport
        }

        public ReminderChoice SelectedChoice { get; private set; } = ReminderChoice.Cancel;

        public frmLogoutReminder()
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Text = "Logout Reminder";
            this.Size = new Size(540, 240);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(250, 245, 240);
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            // Header panel
            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 46,
                BackColor = Color.FromArgb(110, 70, 45)
            };

            Label lblHeader = new Label
            {
                Text = "⚠️ End-of-Shift Logout Reminder",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(16, 12),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblHeader);

            // Question prompt
            Label lblPrompt = new Label
            {
                Text = "Have you generated your end-of-shift report before logging out?",
                Font = new Font("Segoe UI", 11.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 40, 20),
                Location = new Point(20, 64),
                Size = new Size(490, 48),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Buttons: Logout | Generate Report | Cancel
            Button btnLogout = new Button
            {
                Text = "🚪 Logout",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                BackColor = Color.FromArgb(140, 50, 30),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Size = new Size(130, 42),
                Location = new Point(28, 130)
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += (s, e) =>
            {
                SelectedChoice = ReminderChoice.Logout;
                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            Button btnGenerateReport = new Button
            {
                Text = "📊 Generate Report",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                BackColor = Color.FromArgb(205, 133, 63),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Size = new Size(180, 42),
                Location = new Point(176, 130)
            };
            btnGenerateReport.FlatAppearance.BorderSize = 0;
            btnGenerateReport.Click += (s, e) =>
            {
                SelectedChoice = ReminderChoice.GenerateReport;
                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            Button btnCancel = new Button
            {
                Text = "Cancel",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                BackColor = Color.FromArgb(245, 238, 230),
                ForeColor = Color.FromArgb(80, 45, 20),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Size = new Size(110, 42),
                Location = new Point(374, 130)
            };
            btnCancel.FlatAppearance.BorderSize = 1;
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(180, 140, 110);
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(235, 222, 210);
            btnCancel.Click += (s, e) =>
            {
                SelectedChoice = ReminderChoice.Cancel;
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            this.Controls.Add(pnlHeader);
            this.Controls.Add(lblPrompt);
            this.Controls.Add(btnLogout);
            this.Controls.Add(btnGenerateReport);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnGenerateReport;
            this.CancelButton = btnCancel;
        }
    }
}

