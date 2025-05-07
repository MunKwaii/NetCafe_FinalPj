using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Net.Mail;
using System.Net;

namespace NetCafeManager.UserControls
{
    public partial class UC_ForgotPassword : UserControl
    {
        private string verificationCode;
        private string userEmail;
        private DateTime codeCreationTime;
        private readonly TimeSpan codeValidityDuration = TimeSpan.FromMinutes(10);
        private int verificationAttempts;
        private const int maxVerificationAttempts = 3;
        private string userId;

        public UC_ForgotPassword()
        {
            InitializeComponent();
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            string email = EmailTextBox.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please enter your email!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string query = @"
                    SELECT u.ID 
                    FROM Users u
                    INNER JOIN Customer c ON u.ID = c.UserID
                    WHERE c.Email = @email";
                SqlParameter[] parameters = { new SqlParameter("@email", email) };
                object result = DatabaseHelper.ExecuteScalar(query, parameters);

                if (result == null)
                {
                    MessageBox.Show("Email does not exist in the system!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                userEmail = email;
                userId = result.ToString();
                verificationCode = GenerateVerificationCode();
                codeCreationTime = DateTime.Now;
                verificationAttempts = 0;
                SendVerificationEmail(email, verificationCode);

                ShowVerificationForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerateVerificationCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        private void SendVerificationEmail(string email, string code)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("tinhongmai1012@gmail.com", "exwfnohwpewlxygx"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("tinhongmai1012@gmail.com"),
                    Subject = "Password Recovery Verification Code",
                    Body = $"Your verification code is: {code}\nThis code will expire in 10 minutes.",
                    IsBodyHtml = false,
                };
                mailMessage.To.Add(email);

                smtpClient.Send(mailMessage);
                MessageBox.Show("A verification code has been sent to your email!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending email: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowVerificationForm()
        {
            VerificationForm verificationForm = new VerificationForm(verificationCode, userId, codeCreationTime);
            verificationForm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.FindForm()?.Close(); 
        }
    }
}