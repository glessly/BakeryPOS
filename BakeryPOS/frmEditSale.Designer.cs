namespace BakeryPOS
{
    partial class frmEditSale
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditSale));
            this.panel1 = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlSaleInfo = new System.Windows.Forms.Panel();
            this.lblSale = new System.Windows.Forms.Label();
            this.lblSaleID = new System.Windows.Forms.Label();
            this.dgvSaleItems = new System.Windows.Forms.DataGridView();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.tblTotal = new System.Windows.Forms.TableLayoutPanel();
            this.lblTotalLabel = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.pnlCenterButtons = new System.Windows.Forms.Panel();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.tblMain.SuspendLayout();
            this.pnlSaleInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleItems)).BeginInit();
            this.pnlSummary.SuspendLayout();
            this.tblTotal.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.pnlCenterButtons.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(880, 85);
            this.panel1.TabIndex = 0;
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
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblTitle.Location = new System.Drawing.Point(160, 27);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(212, 30);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "EDIT SALE DETAILS";
            // 
            // tblMain
            // 
            this.tblMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(245)))), ((int)(((byte)(236)))));
            this.tblMain.ColumnCount = 1;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Controls.Add(this.pnlSaleInfo, 0, 0);
            this.tblMain.Controls.Add(this.dgvSaleItems, 0, 1);
            this.tblMain.Controls.Add(this.pnlSummary, 0, 2);
            this.tblMain.Controls.Add(this.pnlActions, 0, 3);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(0, 75);
            this.tblMain.Name = "tblMain";
            this.tblMain.Padding = new System.Windows.Forms.Padding(28, 12, 28, 16);
            this.tblMain.RowCount = 4;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tblMain.Size = new System.Drawing.Size(880, 525);
            this.tblMain.TabIndex = 1;
            // 
            // pnlSaleInfo
            // 
            this.pnlSaleInfo.Controls.Add(this.lblSale);
            this.pnlSaleInfo.Controls.Add(this.lblSaleID);
            this.pnlSaleInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSaleInfo.Location = new System.Drawing.Point(28, 12);
            this.pnlSaleInfo.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSaleInfo.Name = "pnlSaleInfo";
            this.pnlSaleInfo.Size = new System.Drawing.Size(824, 40);
            this.pnlSaleInfo.TabIndex = 0;
            // 
            // lblSale
            // 
            this.lblSale.AutoSize = true;
            this.lblSale.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblSale.ForeColor = System.Drawing.Color.SaddleBrown;
            this.lblSale.Location = new System.Drawing.Point(0, 8);
            this.lblSale.Name = "lblSale";
            this.lblSale.Size = new System.Drawing.Size(76, 25);
            this.lblSale.TabIndex = 3;
            this.lblSale.Text = "Sale ID:";
            // 
            // lblSaleID
            // 
            this.lblSaleID.AutoSize = true;
            this.lblSaleID.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblSaleID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(22)))), ((int)(((byte)(8)))));
            this.lblSaleID.Location = new System.Drawing.Point(82, 8);
            this.lblSaleID.Name = "lblSaleID";
            this.lblSaleID.Size = new System.Drawing.Size(19, 25);
            this.lblSaleID.TabIndex = 9;
            this.lblSaleID.Text = "-";
            this.lblSaleID.Click += new System.EventHandler(this.lblSaleID_Click);
            // 
            // dgvSaleItems
            // 
            this.dgvSaleItems.AllowUserToAddRows = false;
            this.dgvSaleItems.AllowUserToDeleteRows = false;
            this.dgvSaleItems.AllowUserToResizeRows = false;
            this.dgvSaleItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSaleItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvSaleItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvSaleItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSaleItems.Location = new System.Drawing.Point(28, 56);
            this.dgvSaleItems.Margin = new System.Windows.Forms.Padding(0, 4, 0, 8);
            this.dgvSaleItems.MultiSelect = false;
            this.dgvSaleItems.Name = "dgvSaleItems";
            this.dgvSaleItems.RowHeadersVisible = false;
            this.dgvSaleItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSaleItems.Size = new System.Drawing.Size(824, 321);
            this.dgvSaleItems.TabIndex = 1;
            this.dgvSaleItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSaleItems_CellContentClick);
            this.dgvSaleItems.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSaleItems_CellEndEdit);
            this.dgvSaleItems.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvSaleItems_CellValidating);
            this.dgvSaleItems.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSaleItems_CellValueChanged);
            this.dgvSaleItems.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvSaleItems_DataError);
            // 
            // pnlSummary
            // 
            this.pnlSummary.Controls.Add(this.tblTotal);
            this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSummary.Location = new System.Drawing.Point(28, 389);
            this.pnlSummary.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(824, 54);
            this.pnlSummary.TabIndex = 2;
            // 
            // tblTotal
            // 
            this.tblTotal.AutoSize = true;
            this.tblTotal.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblTotal.ColumnCount = 2;
            this.tblTotal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tblTotal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tblTotal.Controls.Add(this.lblTotalLabel, 0, 0);
            this.tblTotal.Controls.Add(this.lblTotalAmount, 1, 0);
            this.tblTotal.Dock = System.Windows.Forms.DockStyle.Right;
            this.tblTotal.Location = new System.Drawing.Point(544, 0);
            this.tblTotal.Margin = new System.Windows.Forms.Padding(0);
            this.tblTotal.Name = "tblTotal";
            this.tblTotal.RowCount = 1;
            this.tblTotal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTotal.Size = new System.Drawing.Size(280, 54);
            this.tblTotal.TabIndex = 0;
            // 
            // lblTotalLabel
            // 
            this.lblTotalLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTotalLabel.AutoSize = true;
            this.lblTotalLabel.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTotalLabel.ForeColor = System.Drawing.Color.SaddleBrown;
            this.lblTotalLabel.Location = new System.Drawing.Point(0, 14);
            this.lblTotalLabel.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblTotalLabel.Name = "lblTotalLabel";
            this.lblTotalLabel.Size = new System.Drawing.Size(133, 25);
            this.lblTotalLabel.TabIndex = 0;
            this.lblTotalLabel.Text = "Total Amount:";
            this.lblTotalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(22)))), ((int)(((byte)(8)))));
            this.lblTotalAmount.Location = new System.Drawing.Point(141, 12);
            this.lblTotalAmount.Margin = new System.Windows.Forms.Padding(0);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(65, 30);
            this.lblTotalAmount.TabIndex = 1;
            this.lblTotalAmount.Text = "₱0.00";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.pnlCenterButtons);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlActions.Location = new System.Drawing.Point(28, 443);
            this.pnlActions.Margin = new System.Windows.Forms.Padding(0);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(824, 56);
            this.pnlActions.TabIndex = 3;
            // 
            // pnlCenterButtons
            // 
            this.pnlCenterButtons.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlCenterButtons.Controls.Add(this.btnSaveChanges);
            this.pnlCenterButtons.Controls.Add(this.btnCancel);
            this.pnlCenterButtons.Location = new System.Drawing.Point(222, 6);
            this.pnlCenterButtons.Margin = new System.Windows.Forms.Padding(0);
            this.pnlCenterButtons.Name = "pnlCenterButtons";
            this.pnlCenterButtons.Size = new System.Drawing.Size(380, 44);
            this.pnlCenterButtons.TabIndex = 0;
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(61)))), ((int)(((byte)(34)))));
            this.btnSaveChanges.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveChanges.FlatAppearance.BorderSize = 0;
            this.btnSaveChanges.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveChanges.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSaveChanges.ForeColor = System.Drawing.Color.White;
            this.btnSaveChanges.Location = new System.Drawing.Point(0, 4);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(180, 36);
            this.btnSaveChanges.TabIndex = 0;
            this.btnSaveChanges.Text = "SAVE CHANGES";
            this.btnSaveChanges.UseVisualStyleBackColor = false;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(110)))), ((int)(((byte)(105)))));
            this.btnCancel.CausesValidation = false;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(200, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(180, 36);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmEditSale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(245)))), ((int)(((byte)(236)))));
            this.ClientSize = new System.Drawing.Size(880, 600);
            this.Controls.Add(this.tblMain);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.SaddleBrown;
            this.MinimumSize = new System.Drawing.Size(780, 520);
            this.Name = "frmEditSale";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BakeryPOS - Edit Sale Details";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.tblMain.ResumeLayout(false);
            this.pnlSaleInfo.ResumeLayout(false);
            this.pnlSaleInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleItems)).EndInit();
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            this.tblTotal.ResumeLayout(false);
            this.tblTotal.PerformLayout();
            this.pnlActions.ResumeLayout(false);
            this.pnlCenterButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.Panel pnlSaleInfo;
        private System.Windows.Forms.Label lblSale;
        private System.Windows.Forms.Label lblSaleID;
        private System.Windows.Forms.DataGridView dgvSaleItems;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.TableLayoutPanel tblTotal;
        private System.Windows.Forms.Label lblTotalLabel;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Panel pnlCenterButtons;
        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.Button btnCancel;
    }
}

