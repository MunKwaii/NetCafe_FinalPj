using System;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using Guna.UI2.WinForms;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace NetCafeManager
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
        }

        private string GetAvailableComputer()
        {
            try
            {
                // Truy vấn máy tính khả dụng (UserID là NULL và không ở trạng thái Maintain)
                string query = @"
                    SELECT TOP 1 ComputerID
                    FROM Computer
                    WHERE UserID IS NULL AND Status != 'Maintain'";
                object result = DatabaseHelper.ExecuteScalar(query);

                if (result != null)
                {
                    return result.ToString();
                }
                else
                {
                    MessageBox.Show("Không có máy tính nào khả dụng để đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm máy tính khả dụng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter complete information!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT Role, ID FROM Users WHERE Username = @user AND Password = @pass";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", username);
                        cmd.Parameters.AddWithValue("@pass", password);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string role = reader["Role"].ToString().Trim();
                                string ID = reader["ID"].ToString().Trim();

                                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                Form newForm = null;
                                switch (role)
                                {
                                    case "Manager":
                                        newForm = new ManagerForm(ID);
                                        break;
                                    case "Employee":
                                        newForm = new EmployeeForm(ID);
                                        break;
                                    case "Customer":
                                        // Tự động gán máy tính khả dụng cho khách hàng
                                        string computerID = GetAvailableComputer();
                                        if (computerID != null)
                                        {
                                            newForm = new CustomerForm(ID, computerID);
                                        }
                                        break;
                                }

                                if (newForm != null)
                                {
                                    this.Hide(); // Ẩn LoginForm nhưng không đóng
                                    newForm.ShowDialog(); // Chờ đến khi form mới đóng
                                    txtUsername.Clear();
                                    txtPassword.Clear();
                                    this.Show(); // Khi form mới đóng, LoginForm xuất hiện lại
                                }
                            }
                            else
                            {
                                MessageBox.Show("Invalid username or password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

        private void lblForgotPassword_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please contact the nearest staff member to retrieve your password!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}