using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace NetCafeManager
{
    public partial class VerificationForm : Form
    {
        private string verificationCode;
        private string userId;
        private DateTime codeCreationTime;
        private readonly TimeSpan codeValidityDuration = TimeSpan.FromMinutes(10);
        private int verificationAttempts;
        private const int maxVerificationAttempts = 3;

        public VerificationForm(string verificationCode, string userId, DateTime codeCreationTime)
        {
            InitializeComponent();

            this.verificationCode = verificationCode;
            this.userId = userId;
            this.codeCreationTime = codeCreationTime;
            this.verificationAttempts = 0;

        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (DateTime.Now > codeCreationTime.Add(codeValidityDuration))
            {
                MessageBox.Show("Verification code has expired! Please request a new code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (verificationAttempts >= maxVerificationAttempts)
            {
                MessageBox.Show("You have exceeded the maximum number of attempts! Please request a new code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (txtCode.Text == verificationCode)
            {
                string newPassword = txtNewPassword.Text.Trim();
                if (string.IsNullOrEmpty(newPassword))
                {
                    MessageBox.Show("Please enter a new password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    string query = "UPDATE Users SET Password = @password WHERE ID = @userId";
                    SqlParameter[] parameters = {
                        new SqlParameter("@password", newPassword),
                        new SqlParameter("@userId", userId)
                    };
                    int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Password changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        this.FindForm()?.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error updating password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                verificationAttempts++;
                int remainingAttempts = maxVerificationAttempts - verificationAttempts;
                MessageBox.Show($"Invalid verification code! You have {remainingAttempts} attempts remaining.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}