using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using Microsoft.VisualBasic.ApplicationServices;



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
                MessageBox.Show("Vui lòng nhập email!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Email không tồn tại trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    Subject = "Mã xác nhận khôi phục mật khẩu",
                    Body = $"Mã xác nhận của bạn là: {code}\nMã này sẽ hết hạn sau 10 phút.",
                    IsBodyHtml = false,
                };
                mailMessage.To.Add(email);

                smtpClient.Send(mailMessage);
                MessageBox.Show("Mã xác nhận đã được gửi đến email của bạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi gửi email: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowVerificationForm()
        {
            Form verificationForm = new Form
            {
                Text = "Xác nhận mã và đổi mật khẩu",
                Size = new Size(400, 300),
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lblCode = new Label
            {
                Text = "Nhập mã xác nhận:",
                Location = new Point(20, 20),
                Size = new Size(150, 20)
            };

            TextBox txtCode = new TextBox
            {
                Location = new Point(20, 50),
                Size = new Size(340, 30)
            };

            Label lblNewPassword = new Label
            {
                Text = "Nhập mật khẩu mới:",
                Location = new Point(20, 90),
                Size = new Size(150, 20)
            };

            TextBox txtNewPassword = new TextBox
            {
                Location = new Point(20, 120),
                Size = new Size(340, 30),
                PasswordChar = '*'
            };

            Button btnVerify = new Button
            {
                Text = "Xác nhận",
                Location = new Point(20, 170),
                Size = new Size(100, 30)
            };

            btnVerify.Click += (s, e) =>
            {
                if (DateTime.Now > codeCreationTime.Add(codeValidityDuration))
                {
                    MessageBox.Show("Mã xác nhận đã hết hạn! Vui lòng yêu cầu mã mới.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    verificationForm.Close();
                    return;
                }

                if (verificationAttempts >= maxVerificationAttempts)
                {
                    MessageBox.Show("Bạn đã vượt quá số lần thử cho phép! Vui lòng yêu cầu mã mới.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    verificationForm.Close();
                    return;
                }

                if (txtCode.Text == verificationCode)
                {
                    string newPassword = txtNewPassword.Text.Trim();
                    if (string.IsNullOrEmpty(newPassword))
                    {
                        MessageBox.Show("Vui lòng nhập mật khẩu mới!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            MessageBox.Show("Đổi mật khẩu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            verificationForm.Close();
                            this.ParentForm?.Close();
                        }
                        else
                        {
                            MessageBox.Show("Lỗi khi cập nhật mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    verificationAttempts++;
                    int remainingAttempts = maxVerificationAttempts - verificationAttempts;
                    MessageBox.Show($"Mã xác nhận không đúng! Bạn còn {remainingAttempts} lần thử.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            verificationForm.Controls.AddRange(new Control[] { lblCode, txtCode, lblNewPassword, txtNewPassword, btnVerify });
            verificationForm.ShowDialog();
        }
    }
}
