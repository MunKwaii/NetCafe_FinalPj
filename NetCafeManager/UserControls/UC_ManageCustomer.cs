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
            ApplyCustomTheme();

        }
        private void LoadCustomerData(string searchKeyword = "")
        {
            try
            {
                string query = @"
                    SELECT Users.ID, Users.Username, Users.Password, Customer.FullName, Customer.Email, Customer.Balance 
                    FROM Users
                    JOIN Customer ON Users.ID = Customer.UserID
                    WHERE Users.Role = @role";

                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@role", "Customer")
                };

                if (!string.IsNullOrEmpty(searchKeyword))
                {
                    query += " AND (Users.Username LIKE @keyword OR Customer.FullName LIKE @keyword OR Customer.Email LIKE @keyword)";
                    parameters.Add(new SqlParameter("@keyword", $"%{searchKeyword}%"));
                }

                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters.ToArray());

                dgvCustomer.DataSource = dt;

                dgvCustomer.Columns["ID"].HeaderText = "Customer ID";
                dgvCustomer.Columns["Username"].HeaderText = "Account Name";
                dgvCustomer.Columns["Password"].HeaderText = "Password";
                dgvCustomer.Columns["FullName"].HeaderText = "Full Name";
                dgvCustomer.Columns["Email"].HeaderText = "Email";
                dgvCustomer.Columns["Balance"].HeaderText = "Balance";
                dgvCustomer.Columns["Balance"].DefaultCellStyle.Format = "N0";

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
                DataGridViewRow selectedRow = dgvCustomer.SelectedRows[0];

                IDTextBox.Text = selectedRow.Cells["ID"].Value.ToString();
                AccountTextBox.Text = selectedRow.Cells["Username"].Value.ToString();
                EmailTextBox.Text = selectedRow.Cells["Email"].Value.ToString();
                BalanceTextBox.Text = Convert.ToDecimal(selectedRow.Cells["Balance"].Value).ToString("N0");
                guna2TextBox10.Text = selectedRow.Cells["FullName"].Value.ToString();

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
            if (dgvCustomer.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string customerId = dgvCustomer.SelectedRows[0].Cells["ID"].Value.ToString();

            DialogResult result = MessageBox.Show($"Are you sure you want to delete customer with ID {customerId}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string deleteCustomerQuery = "DELETE FROM Customer WHERE UserID = @id";
                    SqlParameter[] customerParams = new SqlParameter[]
                    {
                        new SqlParameter("@id", customerId)
                    };
                    int customerRowsAffected = DatabaseHelper.ExecuteNonQuery(deleteCustomerQuery, customerParams);

                    string deleteUserQuery = "DELETE FROM Users WHERE ID = @id";
                    SqlParameter[] userParams = new SqlParameter[]
                    {
                        new SqlParameter("@id", customerId)
                    };
                    int userRowsAffected = DatabaseHelper.ExecuteNonQuery(deleteUserQuery, userParams);

                    if (customerRowsAffected > 0 && userRowsAffected > 0)
                    {
                        MessageBox.Show("Customer deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCustomerData();
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

            string id = IDTextBox.Text.Trim();
            string username = AccountTextBox.Text.Trim();
            string password = PasswordTextBox.Text.Trim();
            string fullName = guna2TextBox10.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string balanceText = BalanceTextBox.Text.Trim();

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(fullName) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(balanceText))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(balanceText, out decimal balance))
            {
                MessageBox.Show("Balance must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Are you sure you want to update customer with ID {id}?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string updateUserQuery = "UPDATE Users SET Username = @Username, Password = @Password WHERE ID = @ID";
                    SqlParameter[] userParams = new SqlParameter[]
                    {
                        new SqlParameter("@Username", username),
                        new SqlParameter("@Password", password),
                        new SqlParameter("@ID", id)
                    };
                    int userRowsAffected = DatabaseHelper.ExecuteNonQuery(updateUserQuery, userParams);

                    string updateCustomerQuery = "UPDATE Customer SET FullName = @FullName, Email = @Email, Balance = @Balance WHERE UserID = @UserID";
                    SqlParameter[] customerParams = new SqlParameter[]
                    {
                        new SqlParameter("@FullName", fullName),
                        new SqlParameter("@Email", email),
                        new SqlParameter("@Balance", balance),
                        new SqlParameter("@UserID", id)
                    };
                    int customerRowsAffected = DatabaseHelper.ExecuteNonQuery(updateCustomerQuery, customerParams);

                    if (userRowsAffected > 0 && customerRowsAffected > 0)
                    {
                        MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCustomerData(); 
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

        private void ApplyCustomTheme()
        {
            // Set DataGridView  Theme
            dgvCustomer.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default;
            dgvCustomer.EnableHeadersVisualStyles = false;


            // Header
            dgvCustomer.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(10, 50, 50); 
            dgvCustomer.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(19, 250, 168); 
            dgvCustomer.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvCustomer.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCustomer.ColumnHeadersHeight = 40;

            // Normal line
            dgvCustomer.DefaultCellStyle.BackColor = Color.FromArgb(20, 20, 20); 
            dgvCustomer.DefaultCellStyle.ForeColor = Color.White; 
            dgvCustomer.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvCustomer.DefaultCellStyle.SelectionBackColor = Color.FromArgb(10, 150, 100); 
            dgvCustomer.DefaultCellStyle.SelectionForeColor = Color.White; 
            dgvCustomer.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Next line
            dgvCustomer.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(20, 20, 20); 

            // DGV
            dgvCustomer.BackgroundColor = Color.FromArgb(20, 20, 20); 
            dgvCustomer.BorderStyle = BorderStyle.None;
            dgvCustomer.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCustomer.RowTemplate.Height = 35;
         

            dgvCustomer.ReadOnly = true;
            dgvCustomer.AllowUserToAddRows = false;
            dgvCustomer.AllowUserToResizeRows = false;
            dgvCustomer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
