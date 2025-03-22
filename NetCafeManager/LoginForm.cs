using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using Guna.UI2.WinForms;
using System;
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter complete information!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT Role FROM Users WHERE Username = @user AND Password = @pass";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", password);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string role = result.ToString().Trim();
                        MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Form newForm = null;
                        switch (role)
                        {
                            case "Manager":
                                newForm = new ManagerForm();
                                break;
                            case "Employee":
                                newForm = new EmployeeForm();
                                break;
                            case "Customer":
                                newForm = new CustomerForm();
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
