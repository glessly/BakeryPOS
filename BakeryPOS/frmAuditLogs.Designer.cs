namespace BakeryPOS
{
    partial class frmAuditLogs
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAuditLogs));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.txtAuditSearch = new System.Windows.Forms.TextBox();
            this.LABEL1 = new System.Windows.Forms.Label();
            this.btnRefreshAudit = new System.Windows.Forms.Button();
            this.btnCloseAudit = new System.Windows.Forms.Button();
            this.dgvAuditLogs = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuditLogs)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(22)))), ((int)(((byte)(8)))));
            this.panel1.Controls.Add(this.picLogo);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1034, 85);
            this.panel1.TabIndex = 2;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblTitle.Location = new System.Drawing.Point(160, 27);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(142, 30);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "AUDIT LOGS";
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.Image = global::BakeryPOS.UI.Styles.GetLogoImage();
            this.picLogo.Location = new System.Drawing.Point(16, 5);
            this.picLogo.Margin = new System.Windows.Forms.Padding(0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(130, 74);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 11;
            this.picLogo.TabStop = false;
            // 
            // txtAuditSearch
            // 
            this.txtAuditSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAuditSearch.Location = new System.Drawing.Point(96, 146);
            this.txtAuditSearch.Name = "txtAuditSearch";
            this.txtAuditSearch.Size = new System.Drawing.Size(360, 25);
            this.txtAuditSearch.TabIndex = 5;
            this.txtAuditSearch.TextChanged += new System.EventHandler(this.txtAuditSearch_TextChanged);
            // 
            // LABEL1
            // 
            this.LABEL1.AutoSize = true;
            this.LABEL1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LABEL1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(22)))), ((int)(((byte)(8)))));
            this.LABEL1.Location = new System.Drawing.Point(30, 150);
            this.LABEL1.Name = "LABEL1";
            this.LABEL1.Size = new System.Drawing.Size(52, 17);
            this.LABEL1.TabIndex = 11;
            this.LABEL1.Text = "Search:";
            // 
            // btnRefreshAudit
            // 
            this.btnRefreshAudit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(115)))), ((int)(((byte)(51)))));
            this.btnRefreshAudit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefreshAudit.FlatAppearance.BorderSize = 0;
            this.btnRefreshAudit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshAudit.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshAudit.ForeColor = System.Drawing.Color.White;
            this.btnRefreshAudit.Location = new System.Drawing.Point(468, 141);
            this.btnRefreshAudit.Name = "btnRefreshAudit";
            this.btnRefreshAudit.Size = new System.Drawing.Size(125, 36);
            this.btnRefreshAudit.TabIndex = 13;
            this.btnRefreshAudit.Text = "↺ REFRESH";
            this.btnRefreshAudit.UseVisualStyleBackColor = false;
            this.btnRefreshAudit.Click += new System.EventHandler(this.btnRefreshAudit_Click);
            // 
            // btnCloseAudit
            // 
            this.btnCloseAudit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseAudit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(110)))), ((int)(((byte)(105)))));
            this.btnCloseAudit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCloseAudit.FlatAppearance.BorderSize = 0;
            this.btnCloseAudit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseAudit.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseAudit.ForeColor = System.Drawing.Color.White;
            this.btnCloseAudit.Location = new System.Drawing.Point(854, 540);
            this.btnCloseAudit.Name = "btnCloseAudit";
            this.btnCloseAudit.Size = new System.Drawing.Size(150, 36);
            this.btnCloseAudit.TabIndex = 14;
            this.btnCloseAudit.Text = "CLOSE";
            this.btnCloseAudit.UseVisualStyleBackColor = false;
            this.btnCloseAudit.Click += new System.EventHandler(this.btnCloseAudit_Click);
            // 
            // dgvAuditLogs
            // 
            this.dgvAuditLogs.AllowUserToAddRows = false;
            this.dgvAuditLogs.AllowUserToDeleteRows = false;
            this.dgvAuditLogs.AllowUserToResizeRows = false;
            this.dgvAuditLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAuditLogs.BackgroundColor = System.Drawing.Color.White;
            this.dgvAuditLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvAuditLogs.ColumnHeadersHeight = 36;
            this.dgvAuditLogs.Location = new System.Drawing.Point(30, 189);
            this.dgvAuditLogs.MultiSelect = false;
            this.dgvAuditLogs.Name = "dgvAuditLogs";
            this.dgvAuditLogs.ReadOnly = true;
            this.dgvAuditLogs.RowHeadersVisible = false;
            this.dgvAuditLogs.RowTemplate.Height = 32;
            this.dgvAuditLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvAuditLogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAuditLogs.Size = new System.Drawing.Size(974, 339);
            this.dgvAuditLogs.TabIndex = 15;
            // 
            // frmAuditLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(245)))), ((int)(((byte)(236)))));
            this.ClientSize = new System.Drawing.Size(1034, 590);
            this.Controls.Add(this.dgvAuditLogs);
            this.Controls.Add(this.btnCloseAudit);
            this.Controls.Add(this.btnRefreshAudit);
            this.Controls.Add(this.LABEL1);
            this.Controls.Add(this.txtAuditSearch);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.MinimumSize = new System.Drawing.Size(1034, 580);
            this.Name = "frmAuditLogs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bakery POS - Audit Logs";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuditLogs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtAuditSearch;
        private System.Windows.Forms.Label LABEL1;
        private System.Windows.Forms.Button btnRefreshAudit;
        private System.Windows.Forms.Button btnCloseAudit;
        private System.Windows.Forms.DataGridView dgvAuditLogs;
    }
}