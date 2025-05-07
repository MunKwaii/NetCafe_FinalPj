namespace NetCafeManager.UserControls
{
    partial class UC_ForgotPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_ForgotPassword));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            NextBtn = new Guna.UI2.WinForms.Guna2Button();
            EmailTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            lblForgotPassword = new Label();
            label9 = new Label();
            label8 = new Label();
            btnExit = new Guna.UI2.WinForms.Guna2Button();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            SuspendLayout();
            // 
            // NextBtn
            // 
            NextBtn.CustomizableEdges = customizableEdges1;
            NextBtn.DisabledState.BorderColor = Color.DarkGray;
            NextBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            NextBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            NextBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            NextBtn.FillColor = Color.FromArgb(19, 250, 168);
            NextBtn.Font = new Font("SAIBA-45", 11.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            NextBtn.ForeColor = Color.Black;
            NextBtn.Location = new Point(128, 232);
            NextBtn.Name = "NextBtn";
            NextBtn.ShadowDecoration.CustomizableEdges = customizableEdges2;
            NextBtn.Size = new Size(130, 34);
            NextBtn.TabIndex = 115;
            NextBtn.Text = "Next";
            NextBtn.Click += NextBtn_Click;
            // 
            // EmailTextBox
            // 
            EmailTextBox.Anchor = AnchorStyles.None;
            EmailTextBox.BorderThickness = 0;
            EmailTextBox.CustomizableEdges = customizableEdges3;
            EmailTextBox.DefaultText = "";
            EmailTextBox.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            EmailTextBox.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            EmailTextBox.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            EmailTextBox.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            EmailTextBox.FillColor = Color.FromArgb(20, 20, 20);
            EmailTextBox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            EmailTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            EmailTextBox.ForeColor = Color.White;
            EmailTextBox.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            EmailTextBox.Location = new Point(41, 150);
            EmailTextBox.Margin = new Padding(4);
            EmailTextBox.Name = "EmailTextBox";
            EmailTextBox.PlaceholderText = "";
            EmailTextBox.SelectedText = "";
            EmailTextBox.ShadowDecoration.CustomizableEdges = customizableEdges4;
            EmailTextBox.Size = new Size(302, 50);
            EmailTextBox.TabIndex = 116;
            // 
            // lblForgotPassword
            // 
            lblForgotPassword.Anchor = AnchorStyles.None;
            lblForgotPassword.AutoSize = true;
            lblForgotPassword.Font = new Font("SAIBA-45", 11.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblForgotPassword.ForeColor = Color.FromArgb(19, 250, 168);
            lblForgotPassword.Location = new Point(41, 117);
            lblForgotPassword.Name = "lblForgotPassword";
            lblForgotPassword.Size = new Size(169, 16);
            lblForgotPassword.TabIndex = 118;
            lblForgotPassword.Text = "Enter your email:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("SAIBA-45", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.White;
            label9.Location = new Point(179, 77);
            label9.Name = "label9";
            label9.Size = new Size(163, 26);
            label9.TabIndex = 119;
            label9.Text = "Password";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("SAIBA-45 Outline", 35.9999962F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.FromArgb(19, 250, 168);
            label8.Location = new Point(29, 24);
            label8.Name = "label8";
            label8.Size = new Size(233, 53);
            label8.TabIndex = 120;
            label8.Text = "Forgot";
            // 
            // btnExit
            // 
            btnExit.BorderRadius = 10;
            btnExit.CustomizableEdges = customizableEdges5;
            btnExit.DisabledState.BorderColor = Color.DarkGray;
            btnExit.DisabledState.CustomBorderColor = Color.DarkGray;
            btnExit.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnExit.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnExit.FillColor = Color.FromArgb(20, 20, 20);
            btnExit.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnExit.ForeColor = Color.FromArgb(19, 250, 168);
            btnExit.HoverState.FillColor = Color.FromArgb(19, 250, 168);
            btnExit.HoverState.Image = (Image)resources.GetObject("resource.Image");
            btnExit.Image = (Image)resources.GetObject("btnExit.Image");
            btnExit.ImageSize = new Size(90, 90);
            btnExit.Location = new Point(316, 3);
            btnExit.Name = "btnExit";
            btnExit.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnExit.Size = new Size(59, 49);
            btnExit.TabIndex = 121;
            btnExit.Click += btnExit_Click;
            // 
            // guna2Panel1
            // 
            guna2Panel1.Anchor = AnchorStyles.None;
            guna2Panel1.CustomizableEdges = customizableEdges7;
            guna2Panel1.FillColor = Color.FromArgb(19, 250, 168);
            guna2Panel1.Location = new Point(43, 200);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges8;
            guna2Panel1.Size = new Size(295, 5);
            guna2Panel1.TabIndex = 122;
            // 
            // UC_ForgotPassword
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(20, 20, 20);
            Controls.Add(guna2Panel1);
            Controls.Add(btnExit);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(lblForgotPassword);
            Controls.Add(EmailTextBox);
            Controls.Add(NextBtn);
            Name = "UC_ForgotPassword";
            Size = new Size(375, 312);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button NextBtn;
        private Guna.UI2.WinForms.Guna2TextBox EmailTextBox;
        private Label lblForgotPassword;
        private Label label9;
        private Label label8;
        private Guna.UI2.WinForms.Guna2Button btnExit;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
    }
}
