namespace NetCafeManager
{
    partial class WorkLogViewDetailsForm
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkLogViewDetailsForm));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            btnExit = new Guna.UI2.WinForms.Guna2Button();
            dgvBillDetails = new Guna.UI2.WinForms.Guna2DataGridView();
            lblWorkLogID = new Label();
            label1 = new Label();
            guna2Panel7 = new Guna.UI2.WinForms.Guna2Panel();
            guna2PictureBox5 = new Guna.UI2.WinForms.Guna2PictureBox();
            guna2PictureBox4 = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)dgvBillDetails).BeginInit();
            ((System.ComponentModel.ISupportInitialize)guna2PictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)guna2PictureBox4).BeginInit();
            SuspendLayout();
            // 
            // btnExit
            // 
            btnExit.BorderRadius = 10;
            btnExit.CustomizableEdges = customizableEdges1;
            btnExit.DisabledState.BorderColor = Color.DarkGray;
            btnExit.DisabledState.CustomBorderColor = Color.DarkGray;
            btnExit.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnExit.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnExit.FillColor = Color.FromArgb(40, 40, 40);
            btnExit.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnExit.ForeColor = Color.FromArgb(19, 250, 168);
            btnExit.HoverState.FillColor = Color.FromArgb(19, 250, 168);
            btnExit.HoverState.Image = (Image)resources.GetObject("resource.Image");
            btnExit.Image = (Image)resources.GetObject("btnExit.Image");
            btnExit.ImageSize = new Size(90, 90);
            btnExit.Location = new Point(817, 3);
            btnExit.Name = "btnExit";
            btnExit.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnExit.Size = new Size(83, 55);
            btnExit.TabIndex = 112;
            btnExit.Click += btnExit_Click;
            // 
            // dgvBillDetails
            // 
            dataGridViewCellStyle1.BackColor = Color.White;
            dgvBillDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvBillDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvBillDetails.ColumnHeadersHeight = 4;
            dgvBillDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvBillDetails.DefaultCellStyle = dataGridViewCellStyle3;
            dgvBillDetails.GridColor = Color.FromArgb(231, 229, 255);
            dgvBillDetails.Location = new Point(50, 114);
            dgvBillDetails.Name = "dgvBillDetails";
            dgvBillDetails.RowHeadersVisible = false;
            dgvBillDetails.Size = new Size(799, 321);
            dgvBillDetails.TabIndex = 111;
            dgvBillDetails.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dgvBillDetails.ThemeStyle.AlternatingRowsStyle.Font = null;
            dgvBillDetails.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dgvBillDetails.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dgvBillDetails.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dgvBillDetails.ThemeStyle.BackColor = Color.White;
            dgvBillDetails.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            dgvBillDetails.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dgvBillDetails.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvBillDetails.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            dgvBillDetails.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dgvBillDetails.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvBillDetails.ThemeStyle.HeaderStyle.Height = 4;
            dgvBillDetails.ThemeStyle.ReadOnly = false;
            dgvBillDetails.ThemeStyle.RowsStyle.BackColor = Color.White;
            dgvBillDetails.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvBillDetails.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            dgvBillDetails.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dgvBillDetails.ThemeStyle.RowsStyle.Height = 25;
            dgvBillDetails.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dgvBillDetails.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // lblWorkLogID
            // 
            lblWorkLogID.AutoSize = true;
            lblWorkLogID.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblWorkLogID.ForeColor = Color.FromArgb(19, 250, 168);
            lblWorkLogID.Location = new Point(248, 18);
            lblWorkLogID.Name = "lblWorkLogID";
            lblWorkLogID.Size = new Size(56, 25);
            lblWorkLogID.TabIndex = 108;
            lblWorkLogID.Text = "1234";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("SAIBA-45", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(19, 250, 168);
            label1.Location = new Point(33, 18);
            label1.Name = "label1";
            label1.Size = new Size(193, 26);
            label1.TabIndex = 107;
            label1.Text = "Bill Detail:";
            // 
            // guna2Panel7
            // 
            guna2Panel7.Anchor = AnchorStyles.None;
            guna2Panel7.CustomizableEdges = customizableEdges3;
            guna2Panel7.FillColor = Color.FromArgb(19, 250, 168);
            guna2Panel7.Location = new Point(97, 75);
            guna2Panel7.Name = "guna2Panel7";
            guna2Panel7.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2Panel7.Size = new Size(700, 5);
            guna2Panel7.TabIndex = 106;
            // 
            // guna2PictureBox5
            // 
            guna2PictureBox5.CustomizableEdges = customizableEdges5;
            guna2PictureBox5.FillColor = Color.FromArgb(40, 40, 40);
            guna2PictureBox5.Image = (Image)resources.GetObject("guna2PictureBox5.Image");
            guna2PictureBox5.ImageRotate = 0F;
            guna2PictureBox5.Location = new Point(761, 149);
            guna2PictureBox5.Name = "guna2PictureBox5";
            guna2PictureBox5.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2PictureBox5.Size = new Size(150, 190);
            guna2PictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            guna2PictureBox5.TabIndex = 110;
            guna2PictureBox5.TabStop = false;
            // 
            // guna2PictureBox4
            // 
            guna2PictureBox4.CustomizableEdges = customizableEdges7;
            guna2PictureBox4.FillColor = Color.FromArgb(40, 40, 40);
            guna2PictureBox4.Image = (Image)resources.GetObject("guna2PictureBox4.Image");
            guna2PictureBox4.ImageRotate = 0F;
            guna2PictureBox4.Location = new Point(-106, 328);
            guna2PictureBox4.Name = "guna2PictureBox4";
            guna2PictureBox4.ShadowDecoration.CustomizableEdges = customizableEdges8;
            guna2PictureBox4.Size = new Size(150, 190);
            guna2PictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            guna2PictureBox4.TabIndex = 109;
            guna2PictureBox4.TabStop = false;
            // 
            // WorkLogViewDetailsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 40, 40);
            ClientSize = new Size(900, 500);
            Controls.Add(btnExit);
            Controls.Add(dgvBillDetails);
            Controls.Add(lblWorkLogID);
            Controls.Add(label1);
            Controls.Add(guna2Panel7);
            Controls.Add(guna2PictureBox5);
            Controls.Add(guna2PictureBox4);
            FormBorderStyle = FormBorderStyle.None;
            Name = "WorkLogViewDetailsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WorkLogViewDetailsForm";
            ((System.ComponentModel.ISupportInitialize)dgvBillDetails).EndInit();
            ((System.ComponentModel.ISupportInitialize)guna2PictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)guna2PictureBox4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btnExit;
        private Guna.UI2.WinForms.Guna2DataGridView dgvBillDetails;
        private Label lblWorkLogID;
        private Label label1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel7;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox5;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox4;
    }
}