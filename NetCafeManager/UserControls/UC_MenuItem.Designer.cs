namespace NetCafeManager.UserControls
{
    partial class UC_MenuItem
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
            lblPrice = new Label();
            lblProductName = new Label();
            btnOrder = new Guna.UI2.WinForms.Guna2Button();
            ptbProductImage = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)ptbProductImage).BeginInit();
            SuspendLayout();
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 163);
            lblPrice.ForeColor = Color.FromArgb(19, 250, 168);
            lblPrice.Location = new Point(47, 187);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(0, 21);
            lblPrice.TabIndex = 113;
            // 
            // lblProductName
            // 
            lblProductName.AutoSize = true;
            lblProductName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblProductName.ForeColor = Color.FromArgb(19, 250, 168);
            lblProductName.Location = new Point(54, 153);
            lblProductName.Name = "lblProductName";
            lblProductName.Size = new Size(0, 21);
            lblProductName.TabIndex = 112;
            // 
            // btnOrder
            // 
            btnOrder.CustomizableEdges = customizableEdges1;
            btnOrder.DisabledState.BorderColor = Color.DarkGray;
            btnOrder.DisabledState.CustomBorderColor = Color.DarkGray;
            btnOrder.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnOrder.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnOrder.FillColor = Color.FromArgb(19, 250, 168);
            btnOrder.Font = new Font("SAIBA-45", 11.999999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnOrder.ForeColor = Color.Black;
            btnOrder.Location = new Point(32, 236);
            btnOrder.Name = "btnOrder";
            btnOrder.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnOrder.Size = new Size(130, 34);
            btnOrder.TabIndex = 114;
            btnOrder.Text = "Order";
            // 
            // ptbProductImage
            // 
            ptbProductImage.CustomizableEdges = customizableEdges3;
            ptbProductImage.FillColor = Color.FromArgb(40, 40, 40);
            ptbProductImage.ImageRotate = 0F;
            ptbProductImage.Location = new Point(26, 12);
            ptbProductImage.Name = "ptbProductImage";
            ptbProductImage.ShadowDecoration.CustomizableEdges = customizableEdges4;
            ptbProductImage.Size = new Size(144, 121);
            ptbProductImage.SizeMode = PictureBoxSizeMode.Zoom;
            ptbProductImage.TabIndex = 115;
            ptbProductImage.TabStop = false;
            // 
            // UC_MenuItem
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 40, 40);
            Controls.Add(ptbProductImage);
            Controls.Add(btnOrder);
            Controls.Add(lblProductName);
            Controls.Add(lblPrice);
            Name = "UC_MenuItem";
            RightToLeft = RightToLeft.No;
            Size = new Size(200, 300);
            ((System.ComponentModel.ISupportInitialize)ptbProductImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPrice;
        private Label lblProductName;
        private Guna.UI2.WinForms.Guna2Button btnOrder;
        private Guna.UI2.WinForms.Guna2PictureBox ptbProductImage;
    }
}
