using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetCafeManager.UserControls
{
    public partial class UC_ManageCustomer : UserControl
    {
        public UC_ManageCustomer()
        {
            InitializeComponent();
            LoadCustomerData();
        }
        private void LoadCustomerData(string searchKeyword = "")
        {
            //try
            //{
            //    // Truy vấn SQL để lấy danh sách customer, join giữa bảng Users và UserInfo
            //    string query = @"
            //        SELECT Users.ID, Users.Username, Users.Password, Customer.FullName, Customer.Email, Customer.Balance 
            //        FROM Users
            //        JOIN Customer ON Users.ID = Customer.UserID
            //        WHERE Users.Role = @role";

            //    SqlParameter[] parameters = new SqlParameter[]
            //    {
            //        new SqlParameter("@role", "Customer")
            //    };

            //    // Thực thi truy vấn và lấy dữ liệu
            //    DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            //    // Gán dữ liệu vào DataGridView
            //    dgvCustomer.DataSource = dt;

            //    // Tùy chỉnh tên cột
            //    dgvCustomer.Columns["ID"].HeaderText = "Customer ID";
            //    dgvCustomer.Columns["Username"].HeaderText = "Account Name";
            //    dgvCustomer.Columns["Password"].HeaderText = "Password";
            //    dgvCustomer.Columns["FullName"].HeaderText = "Full Name";
            //    dgvCustomer.Columns["Email"].HeaderText = "Email";
            //    dgvCustomer.Columns["Balance"].HeaderText = "Balance";

            //    // Định dạng cột Balance (hiển thị dưới dạng tiền tệ, không số thập phân)
            //    dgvCustomer.Columns["Balance"].DefaultCellStyle.Format = "N0";

            //    // Tùy chỉnh giao diện DataGridView
            //    dgvCustomer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //    dgvCustomer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //    dgvCustomer.ReadOnly = true;
            //    dgvCustomer.AllowUserToAddRows = false;
            //    dgvCustomer.ColumnHeadersHeight = 40;
            //    dgvCustomer.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            //    dgvCustomer.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Error loading customer data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            try
            {
                // Truy vấn SQL để lấy danh sách customer, join giữa bảng Users và Customer
                string query = @"
                    SELECT Users.ID, Users.Username, Users.Password, Customer.FullName, Customer.Email, Customer.Balance 
                    FROM Users
                    JOIN Customer ON Users.ID = Customer.UserID
                    WHERE Users.Role = @role";

                // Thêm điều kiện tìm kiếm nếu từ khóa không rỗng
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@role", "Customer")
                };

                if (!string.IsNullOrEmpty(searchKeyword))
                {
                    query += " AND (Users.Username LIKE @keyword OR Customer.FullName LIKE @keyword OR Customer.Email LIKE @keyword)";
                    parameters.Add(new SqlParameter("@keyword", $"%{searchKeyword}%"));
                }

                // Thực thi truy vấn và lấy dữ liệu
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters.ToArray());

                // Gán dữ liệu vào DataGridView
                dgvCustomer.DataSource = dt;

                // Tùy chỉnh tên cột
                dgvCustomer.Columns["ID"].HeaderText = "Customer ID";
                dgvCustomer.Columns["Username"].HeaderText = "Account Name";
                dgvCustomer.Columns["Password"].HeaderText = "Password";
                dgvCustomer.Columns["FullName"].HeaderText = "Full Name";
                dgvCustomer.Columns["Email"].HeaderText = "Email";
                dgvCustomer.Columns["Balance"].HeaderText = "Balance";

                // Định dạng cột Balance (hiển thị dưới dạng tiền tệ, không số thập phân)
                dgvCustomer.Columns["Balance"].DefaultCellStyle.Format = "N0";

                // Tùy chỉnh giao diện DataGridView
                dgvCustomer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvCustomer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvCustomer.ReadOnly = true;
                dgvCustomer.AllowUserToAddRows = false;
                dgvCustomer.ColumnHeadersHeight = 40;
                dgvCustomer.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                dgvCustomer.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddCustomerForm addCustomerForm = new AddCustomerForm();
            addCustomerForm.ShowDialog();
            LoadCustomerData();
        }

        private void dgvCustomer_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomer.SelectedRows.Count > 0)
            {
                // Lấy hàng được chọn
                DataGridViewRow selectedRow = dgvCustomer.SelectedRows[0];

                // Gán dữ liệu từ hàng được chọn vào các TextBox
                IDTextBox.Text = selectedRow.Cells["ID"].Value.ToString();
                AccountTextBox.Text = selectedRow.Cells["Username"].Value.ToString();
                EmailTextBox.Text = selectedRow.Cells["Email"].Value.ToString();
                BalanceTextBox.Text = Convert.ToDecimal(selectedRow.Cells["Balance"].Value).ToString("N0");
                guna2TextBox10.Text = selectedRow.Cells["FullName"].Value.ToString();

                // Lấy mật khẩu từ cơ sở dữ liệu dựa trên ID
                string customerId = selectedRow.Cells["ID"].Value.ToString();
                string query = "SELECT Password FROM Users WHERE ID = @id";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", customerId)
                };

                try
                {
                    object password = DatabaseHelper.ExecuteScalar(query, parameters);
                    PasswordTextBox.Text = password != null ? password.ToString() : string.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error retrieving password: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PasswordTextBox.Text = string.Empty;
                }
            }
            else
            {
                // Nếu không có hàng nào được chọn, xóa nội dung các TextBox
                IDTextBox.Text = string.Empty;
                AccountTextBox.Text = string.Empty;
                EmailTextBox.Text = string.Empty;
                BalanceTextBox.Text = string.Empty;
                PasswordTextBox.Text = string.Empty;
                guna2TextBox10.Text = string.Empty;
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn trong DataGridView không
            if (dgvCustomer.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy ID của customer được chọn
            string customerId = dgvCustomer.SelectedRows[0].Cells["ID"].Value.ToString();

            // Hiển thị thông báo xác nhận xóa
            DialogResult result = MessageBox.Show($"Are you sure you want to delete customer with ID {customerId}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Xóa bản ghi từ bảng Customer trước (vì ID là khóa ngoại)
                    string deleteCustomerQuery = "DELETE FROM Customer WHERE UserID = @id";
                    SqlParameter[] customerParams = new SqlParameter[]
                    {
                        new SqlParameter("@id", customerId)
                    };
                    int customerRowsAffected = DatabaseHelper.ExecuteNonQuery(deleteCustomerQuery, customerParams);

                    // Sau đó xóa bản ghi từ bảng Users
                    string deleteUserQuery = "DELETE FROM Users WHERE ID = @id";
                    SqlParameter[] userParams = new SqlParameter[]
                    {
                        new SqlParameter("@id", customerId)
                    };
                    int userRowsAffected = DatabaseHelper.ExecuteNonQuery(deleteUserQuery, userParams);

                    // Kiểm tra xem xóa có thành công không
                    if (customerRowsAffected > 0 && userRowsAffected > 0)
                    {
                        MessageBox.Show("Customer deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCustomerData(); // Tải lại dữ liệu để cập nhật DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete customer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting customer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (dgvCustomer.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy dữ liệu từ các TextBox
            string id = IDTextBox.Text.Trim();
            string username = AccountTextBox.Text.Trim();
            string password = PasswordTextBox.Text.Trim();
            string fullName = guna2TextBox10.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string balanceText = BalanceTextBox.Text.Trim();

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(fullName) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(balanceText))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra định dạng số cho Balance
            if (!decimal.TryParse(balanceText, out decimal balance))
            {
                MessageBox.Show("Balance must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hiển thị thông báo xác nhận cập nhật
            DialogResult result = MessageBox.Show($"Are you sure you want to update customer with ID {id}?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // 1. Cập nhật bảng Users
                    string updateUserQuery = "UPDATE Users SET Username = @Username, Password = @Password WHERE ID = @ID";
                    SqlParameter[] userParams = new SqlParameter[]
                    {
                        new SqlParameter("@Username", username),
                        new SqlParameter("@Password", password),
                        new SqlParameter("@ID", id)
                    };
                    int userRowsAffected = DatabaseHelper.ExecuteNonQuery(updateUserQuery, userParams);

                    // 2. Cập nhật bảng Customer
                    string updateCustomerQuery = "UPDATE Customer SET FullName = @FullName, Email = @Email, Balance = @Balance WHERE UserID = @UserID";
                    SqlParameter[] customerParams = new SqlParameter[]
                    {
                        new SqlParameter("@FullName", fullName),
                        new SqlParameter("@Email", email),
                        new SqlParameter("@Balance", balance),
                        new SqlParameter("@UserID", id)
                    };
                    int customerRowsAffected = DatabaseHelper.ExecuteNonQuery(updateCustomerQuery, customerParams);

                    // Kiểm tra xem cập nhật có thành công không
                    if (userRowsAffected > 0 && customerRowsAffected > 0)
                    {
                        MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCustomerData(); // Tải lại dữ liệu để cập nhật DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Failed to update customer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating customer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchKeyword = SearchTextBox.Text.Trim();
            LoadCustomerData(searchKeyword);
        }
    }
}
