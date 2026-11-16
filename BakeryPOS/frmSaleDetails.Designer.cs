namespace BakeryPOS
{
    partial class frmSaleDetails
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSaleIDTitle = new System.Windows.Forms.Label();
            this.lblSaleID = new System.Windows.Forms.Label();
            this.lblDateTitle = new System.Windows.Forms.Label();
            this.lblSaleDate = new System.Windows.Forms.Label();
            this.lblCashierTitle = new System.Windows.Forms.Label();
            this.lblCashier = new System.Windows.Forms.Label();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.lblTotalTitle = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblCashPaidTitle = new System.Windows.Forms.Label();
            this.lblCashPaid = new System.Windows.Forms.Label();
            this.lblChangeTitle = new System.Windows.Forms.Label();
            this.lblChange = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(22)))), ((int)(((byte)(8)))));
            this.pnlHeader.Controls.Add(this.picLogo);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(720, 85);
            this.pnlHeader.TabIndex = 0;
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.Image = global::BakeryPOS.UI.Styles.GetLogoImage();
            this.picLogo.Location = new System.Drawing.Point(16, 5);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(130, 74);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 1;
            this.picLogo.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(160, 27);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(252, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "TRANSACTION DETAILS";
            // 
            // lblSaleIDTitle
            // 
            this.lblSaleIDTitle.AutoSize = true;
            this.lblSaleIDTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSaleIDTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(22)))), ((int)(((byte)(8)))));
            this.lblSaleIDTitle.Location = new System.Drawing.Point(24, 98);
            this.lblSaleIDTitle.Name = "lblSaleIDTitle";
            this.lblSaleIDTitle.Size = new System.Drawing.Size(110, 17);
            this.lblSaleIDTitle.TabIndex = 1;
            this.lblSaleIDTitle.Text = "Sale / Order ID:";
            // 
            // lblSaleID
            // 
            this.lblSaleID.AutoSize = true;
            this.lblSaleID.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblSaleID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(63)))), ((int)(((byte)(26)))));
            this.lblSaleID.Location = new System.Drawing.Point(145, 97);
            this.lblSaleID.Name = "lblSaleID";
            this.lblSaleID.Size = new System.Drawing.Size(15, 19);
            this.lblSaleID.TabIndex = 2;
            this.lblSaleID.Text = "-";
            // 
            // lblDateTitle
            // 
            this.lblDateTitle.AutoSize = true;
            this.lblDateTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblDateTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(22)))), ((int)(((byte)(8)))));
            this.lblDateTitle.Location = new System.Drawing.Point(360, 98);
            this.lblDateTitle.Name = "lblDateTitle";
            this.lblDateTitle.Size = new System.Drawing.Size(70, 17);
            this.lblDateTitle.TabIndex = 3;
            this.lblDateTitle.Text = "Sale Date:";
            // 
            // lblSaleDate
            // 
            this.lblSaleDate.AutoSize = true;
            this.lblSaleDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblSaleDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(22)))), ((int)(((byte)(8)))));
            this.lblSaleDate.Location = new System.Drawing.Point(440, 98);
            this.lblSaleDate.Name = "lblSaleDate";
            this.lblSaleDate.Size = new System.Drawing.Size(15, 19);
            this.lblSaleDate.TabIndex = 4;
            this.lblSaleDate.Text = "-";
            // 
            // lblCashierTitle
            // 
            this.lblCashierTitle.AutoSize = true;
            this.lblCashierTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblCashierTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(22)))), ((int)(((byte)(8)))));
            this.lblCashierTitle.Location = new System.Drawing.Point(24, 126);
            this.lblCashierTitle.Name = "lblCashierTitle";
            this.lblCashierTitle.Size = new System.Drawing.Size(59, 17);
            this.lblCashierTitle.TabIndex = 7;
            this.lblCashierTitle.Text = "Cashier:";
            // 
            // lblCashier
            // 
            this.lblCashier.AutoSize = true;
            this.lblCashier.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblCashier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(22)))), ((int)(((byte)(8)))));
            this.lblCashier.Location = new System.Drawing.Point(145, 126);
            this.lblCashier.Name = "lblCashier";
            this.lblCashier.Size = new System.Drawing.Size(15, 19);
            this.lblCashier.TabIndex = 8;
            this.lblCashier.Text = "-";
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AllowUserToResizeRows = false;
            this.dgvItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvItems.ColumnHeadersHeight = 36;
            this.dgvItems.Location = new System.Drawing.Point(24, 158);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.RowTemplate.Height = 32;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(672, 288);
            this.dgvItems.TabIndex = 9;
            // 
            // lblTotalTitle
            // 
            this.lblTotalTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalTitle.AutoSize = true;
            this.lblTotalTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTotalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(22)))), ((int)(((byte)(8)))));
            this.lblTotalTitle.Location = new System.Drawing.Point(24, 468);
            this.lblTotalTitle.Name = "lblTotalTitle";
            this.lblTotalTitle.Size = new System.Drawing.Size(96, 17);
            this.lblTotalTitle.TabIndex = 10;
            this.lblTotalTitle.Text = "Total Amount:";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(63)))), ((int)(((byte)(26)))));
            this.lblTotalAmount.Location = new System.Drawing.Point(126, 465);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(51, 21);
            this.lblTotalAmount.TabIndex = 11;
            this.lblTotalAmount.Text = "₱0.00";
            // 
            // lblCashPaidTitle
            // 
            this.lblCashPaidTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCashPaidTitle.AutoSize = true;
            this.lblCashPaidTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblCashPaidTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(22)))), ((int)(((byte)(8)))));
            this.lblCashPaidTitle.Location = new System.Drawing.Point(235, 468);
            this.lblCashPaidTitle.Name = "lblCashPaidTitle";
            this.lblCashPaidTitle.Size = new System.Drawing.Size(72, 17);
            this.lblCashPaidTitle.TabIndex = 13;
            this.lblCashPaidTitle.Text = "Cash Paid:";
            // 
            // lblCashPaid
            // 
            this.lblCashPaid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCashPaid.AutoSize = true;
            this.lblCashPaid.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCashPaid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(22)))), ((int)(((byte)(8)))));
            this.lblCashPaid.Location = new System.Drawing.Point(312, 465);
            this.lblCashPaid.Name = "lblCashPaid";
            this.lblCashPaid.Size = new System.Drawing.Size(51, 21);
            this.lblCashPaid.TabIndex = 14;
            this.lblCashPaid.Text = "₱0.00";
            // 
            // lblChangeTitle
            // 
            this.lblChangeTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblChangeTitle.AutoSize = true;
            this.lblChangeTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblChangeTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(22)))), ((int)(((byte)(8)))));
            this.lblChangeTitle.Location = new System.Drawing.Point(418, 468);
            this.lblChangeTitle.Name = "lblChangeTitle";
            this.lblChangeTitle.Size = new System.Drawing.Size(59, 17);
            this.lblChangeTitle.TabIndex = 15;
            this.lblChangeTitle.Text = "Change:";
            // 
            // lblChange
            // 
            this.lblChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblChange.AutoSize = true;
            this.lblChange.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblChange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(126)))), ((int)(((byte)(52)))));
            this.lblChange.Location = new System.Drawing.Point(482, 465);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(51, 21);
            this.lblChange.TabIndex = 16;
            this.lblChange.Text = "₱0.00";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(110)))), ((int)(((byte)(105)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(586, 462);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 36);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmSaleDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(245)))), ((int)(((byte)(236)))));
            this.ClientSize = new System.Drawing.Size(720, 520);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblChange);
            this.Controls.Add(this.lblChangeTitle);
            this.Controls.Add(this.lblCashPaid);
            this.Controls.Add(this.lblCashPaidTitle);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.lblTotalTitle);
            this.Controls.Add(this.dgvItems);
            this.Controls.Add(this.lblCashier);
            this.Controls.Add(this.lblCashierTitle);
            this.Controls.Add(this.lblSaleDate);
            this.Controls.Add(this.lblDateTitle);
            this.Controls.Add(this.lblSaleID);
            this.Controls.Add(this.lblSaleIDTitle);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.MinimumSize = new System.Drawing.Size(720, 520);
            this.Name = "frmSaleDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bakery POS - Transaction Details";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSaleIDTitle;
        private System.Windows.Forms.Label lblSaleID;
        private System.Windows.Forms.Label lblDateTitle;
        private System.Windows.Forms.Label lblSaleDate;
        private System.Windows.Forms.Label lblCashierTitle;
        private System.Windows.Forms.Label lblCashier;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.Label lblTotalTitle;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lblCashPaidTitle;
        private System.Windows.Forms.Label lblCashPaid;
        private System.Windows.Forms.Label lblChangeTitle;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.Button btnClose;
    }
}
