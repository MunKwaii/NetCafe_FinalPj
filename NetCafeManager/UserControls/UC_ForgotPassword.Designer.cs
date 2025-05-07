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
            NextBtn = new Guna.UI2.WinForms.Guna2Button();
            EmailTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            label2 = new Label();
            lblForgotPassword = new Label();
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
            NextBtn.Font = new Font("Microsoft Sans Serif", 11.999999F, FontStyle.Bold, GraphicsUnit.Point, 0);
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
            EmailTextBox.FillColor = Color.FromArgb(30, 30, 30);
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
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 163);
            label2.ForeColor = Color.FromArgb(19, 250, 168);
            label2.Location = new Point(52, 42);
            label2.Name = "label2";
            label2.Size = new Size(279, 39);
            label2.TabIndex = 117;
            label2.Text = "Forgot Password";
            // 
            // lblForgotPassword
            // 
            lblForgotPassword.Anchor = AnchorStyles.None;
            lblForgotPassword.AutoSize = true;
            lblForgotPassword.Font = new Font("Microsoft Sans Serif", 11.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblForgotPassword.ForeColor = Color.FromArgb(19, 250, 168);
            lblForgotPassword.Location = new Point(41, 117);
            lblForgotPassword.Name = "lblForgotPassword";
            lblForgotPassword.Size = new Size(127, 20);
            lblForgotPassword.TabIndex = 118;
            lblForgotPassword.Text = "Enter your email:";
            // 
            // UC_ForgotPassword
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 40, 40);
            Controls.Add(lblForgotPassword);
            Controls.Add(label2);
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
        private Label label2;
        private Label lblForgotPassword;
    }
}
