namespace NetCafeManager.UserControls
{
    partial class UC_ComputerStatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_ComputerStatus));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            ptbComputer = new Guna.UI2.WinForms.Guna2PictureBox();
            btnAddBalance = new Guna.UI2.WinForms.Guna2Button();
            lblID = new Label();
            ((System.ComponentModel.ISupportInitialize)ptbComputer).BeginInit();
            SuspendLayout();
            // 
            // ptbComputer
            // 
            ptbComputer.CustomizableEdges = customizableEdges1;
            ptbComputer.FillColor = Color.Transparent;
            ptbComputer.ImageRotate = 0F;
            ptbComputer.Location = new Point(3, 3);
            ptbComputer.Name = "ptbComputer";
            ptbComputer.ShadowDecoration.CustomizableEdges = customizableEdges2;
            ptbComputer.Size = new Size(160, 97);
            ptbComputer.SizeMode = PictureBoxSizeMode.Zoom;
            ptbComputer.TabIndex = 26;
            ptbComputer.TabStop = false;
            // 
            // btnAddBalance
            // 
            btnAddBalance.CustomizableEdges = customizableEdges3;
            btnAddBalance.DisabledState.BorderColor = Color.DarkGray;
            btnAddBalance.DisabledState.CustomBorderColor = Color.DarkGray;
            btnAddBalance.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnAddBalance.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnAddBalance.FillColor = Color.FromArgb(40, 40, 40);
            btnAddBalance.Font = new Font("SAIBA-45", 11.999999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAddBalance.ForeColor = Color.Black;
            btnAddBalance.Image = (Image)resources.GetObject("btnAddBalance.Image");
            btnAddBalance.ImageSize = new Size(100, 40);
            btnAddBalance.Location = new Point(16, 154);
            btnAddBalance.Name = "btnAddBalance";
            btnAddBalance.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnAddBalance.Size = new Size(133, 43);
            btnAddBalance.TabIndex = 29;
            btnAddBalance.Click += btnAddBalance_Click;
            // 
            // lblID
            // 
            lblID.AutoSize = true;
            lblID.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblID.ForeColor = Color.FromArgb(19, 250, 168);
            lblID.Location = new Point(71, 120);
            lblID.Name = "lblID";
            lblID.Size = new Size(0, 21);
            lblID.TabIndex = 34;
            // 
            // UC_ComputerStatus
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 40, 40);
            Controls.Add(lblID);
            Controls.Add(btnAddBalance);
            Controls.Add(ptbComputer);
            Name = "UC_ComputerStatus";
            Size = new Size(160, 205);
            ((System.ComponentModel.ISupportInitialize)ptbComputer).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Guna.UI2.WinForms.Guna2PictureBox ptbComputer;
        private Guna.UI2.WinForms.Guna2Button btnAddBalance;
        private Label lblID;
    }
}
