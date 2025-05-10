namespace NetCafeManager.UserControls
{
    partial class UC_WorkLog
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            lblStartDate = new Label();
            label1 = new Label();
            lblStartTime = new Label();
            btnViewDetails = new Guna.UI2.WinForms.Guna2Button();
            btnShiftSummary = new Guna.UI2.WinForms.Guna2Button();
            dgvWorkLog = new Guna.UI2.WinForms.Guna2DataGridView();
            label3 = new Label();
            lblTotalAmount = new Label();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvWorkLog).BeginInit();
            SuspendLayout();
            // 
            // guna2Panel2
            // 
            guna2Panel2.Anchor = AnchorStyles.None;
            guna2Panel2.CustomizableEdges = customizableEdges1;
            guna2Panel2.FillColor = Color.FromArgb(19, 250, 168);
            guna2Panel2.Location = new Point(607, 51);
            guna2Panel2.Name = "guna2Panel2";
            guna2Panel2.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Panel2.Size = new Size(249, 5);
            guna2Panel2.TabIndex = 34;
            // 
            // lblStartDate
            // 
            lblStartDate.AutoSize = true;
            lblStartDate.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblStartDate.ForeColor = Color.FromArgb(19, 250, 168);
            lblStartDate.Location = new Point(406, 35);
            lblStartDate.Name = "lblStartDate";
            lblStartDate.Size = new Size(67, 21);
            lblStartDate.TabIndex = 35;
            lblStartDate.Text = "hh/mm";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(19, 250, 168);
            label1.Location = new Point(32, 35);
            label1.Name = "label1";
            label1.Size = new Size(132, 21);
            label1.TabIndex = 36;
            label1.Text = "Shift Start Time:";
            // 
            // lblStartTime
            // 
            lblStartTime.AutoSize = true;
            lblStartTime.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblStartTime.ForeColor = Color.FromArgb(19, 250, 168);
            lblStartTime.Location = new Point(237, 35);
            lblStartTime.Name = "lblStartTime";
            lblStartTime.Size = new Size(110, 21);
            lblStartTime.TabIndex = 37;
            lblStartTime.Text = "dd/mm/yyyy";
            // 
            // btnViewDetails
            // 
            btnViewDetails.CustomizableEdges = customizableEdges3;
            btnViewDetails.DisabledState.BorderColor = Color.DarkGray;
            btnViewDetails.DisabledState.CustomBorderColor = Color.DarkGray;
            btnViewDetails.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnViewDetails.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnViewDetails.FillColor = Color.FromArgb(19, 250, 168);
            btnViewDetails.Font = new Font("SAIBA-45", 11.999999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnViewDetails.ForeColor = Color.Black;
            btnViewDetails.Location = new Point(1007, 20);
            btnViewDetails.Name = "btnViewDetails";
            btnViewDetails.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnViewDetails.Size = new Size(171, 36);
            btnViewDetails.TabIndex = 38;
            btnViewDetails.Text = "View Details";
            btnViewDetails.Click += btnViewDetails_Click;
            // 
            // btnShiftSummary
            // 
            btnShiftSummary.CustomizableEdges = customizableEdges5;
            btnShiftSummary.DisabledState.BorderColor = Color.DarkGray;
            btnShiftSummary.DisabledState.CustomBorderColor = Color.DarkGray;
            btnShiftSummary.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnShiftSummary.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnShiftSummary.FillColor = Color.FromArgb(19, 250, 168);
            btnShiftSummary.Font = new Font("SAIBA-45", 11.999999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnShiftSummary.ForeColor = Color.Black;
            btnShiftSummary.Location = new Point(1257, 20);
            btnShiftSummary.Name = "btnShiftSummary";
            btnShiftSummary.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnShiftSummary.Size = new Size(171, 36);
            btnShiftSummary.TabIndex = 39;
            btnShiftSummary.Text = "Shift Summary";
            btnShiftSummary.Click += btnShiftSummary_Click;
            // 
            // dgvWorkLog
            // 
            dataGridViewCellStyle1.BackColor = Color.White;
            dgvWorkLog.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvWorkLog.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvWorkLog.ColumnHeadersHeight = 4;
            dgvWorkLog.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvWorkLog.DefaultCellStyle = dataGridViewCellStyle3;
            dgvWorkLog.GridColor = Color.FromArgb(231, 229, 255);
            dgvWorkLog.Location = new Point(32, 105);
            dgvWorkLog.Name = "dgvWorkLog";
            dgvWorkLog.RowHeadersVisible = false;
            dgvWorkLog.Size = new Size(1396, 525);
            dgvWorkLog.TabIndex = 40;
            dgvWorkLog.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dgvWorkLog.ThemeStyle.AlternatingRowsStyle.Font = null;
            dgvWorkLog.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dgvWorkLog.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dgvWorkLog.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dgvWorkLog.ThemeStyle.BackColor = Color.White;
            dgvWorkLog.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            dgvWorkLog.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dgvWorkLog.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvWorkLog.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            dgvWorkLog.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dgvWorkLog.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvWorkLog.ThemeStyle.HeaderStyle.Height = 4;
            dgvWorkLog.ThemeStyle.ReadOnly = false;
            dgvWorkLog.ThemeStyle.RowsStyle.BackColor = Color.White;
            dgvWorkLog.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvWorkLog.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            dgvWorkLog.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dgvWorkLog.ThemeStyle.RowsStyle.Height = 25;
            dgvWorkLog.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dgvWorkLog.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(19, 250, 168);
            label3.Location = new Point(1105, 665);
            label3.Name = "label3";
            label3.Size = new Size(118, 21);
            label3.TabIndex = 36;
            label3.Text = "Total Amount:";
            // 
            // lblTotalAmount
            // 
            lblTotalAmount.AutoSize = true;
            lblTotalAmount.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotalAmount.ForeColor = Color.FromArgb(19, 250, 168);
            lblTotalAmount.Location = new Point(1270, 665);
            lblTotalAmount.Name = "lblTotalAmount";
            lblTotalAmount.Size = new Size(19, 21);
            lblTotalAmount.TabIndex = 35;
            lblTotalAmount.Text = "$";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label5.ForeColor = Color.FromArgb(19, 250, 168);
            label5.Location = new Point(1996, 1292);
            label5.Name = "label5";
            label5.Size = new Size(132, 21);
            label5.TabIndex = 36;
            label5.Text = "Shift Start Time:";
            // 
            // UC_WorkLog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(20, 20, 20);
            Controls.Add(dgvWorkLog);
            Controls.Add(btnShiftSummary);
            Controls.Add(btnViewDetails);
            Controls.Add(lblStartTime);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(lblTotalAmount);
            Controls.Add(label1);
            Controls.Add(lblStartDate);
            Controls.Add(guna2Panel2);
            Name = "UC_WorkLog";
            Size = new Size(1466, 735);
            ((System.ComponentModel.ISupportInitialize)dgvWorkLog).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Label lblStartDate;
        private Label label1;
        private Label lblStartTime;
        private Guna.UI2.WinForms.Guna2Button btnViewDetails;
        private Guna.UI2.WinForms.Guna2Button btnShiftSummary;
        private Guna.UI2.WinForms.Guna2DataGridView dgvWorkLog;
        private Label label3;
        private Label lblTotalAmount;
        private Label label5;
    }
}
