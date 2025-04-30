using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetCafeManager.UserControls
{
    public partial class UC_ManageEmployee : UserControl
    {
        public UC_ManageEmployee()
        {
            InitializeComponent();
            LoadEmployeeData();

        }
        private void ClearFormFields()
        {
            IDTextBox.Text = "";
            FullNameTextBox.Text = "";
            salaryTexBox.Text = "";
            phonenumberTextBox.Text = "";
            UsernameTextBox.Text = "";
            PasswordTextBox.Text = "";
            BirthDayDateTimePicker.Value = DateTime.Now;
            HireDateDateTimePicker.Value = DateTime.Now;
        }
        private void LoadEmployeeData(string searchKeyword = "")
        {
            try
            {
                // Query to fetch data from Employee and Users tables
                string query = @"
                    SELECT e.ID, e.Name, e.Gmail, e.Salary, e.PhoneNumber, e.Birthday, e.StartDate, 
                           u.Username, u.Password
                    FROM Employee e
                    INNER JOIN Users u ON e.ID = u.ID
                    WHERE u.Role = @role";

                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@role", "Employee")
                };

                if (!string.IsNullOrEmpty(searchKeyword))
                {
                    query += " AND (e.ID LIKE @keyword OR e.Name LIKE @keyword OR e.PhoneNumber LIKE @keyword OR u.Username LIKE @keyword)";
                    parameters.Add(new SqlParameter("@keyword", $"%{searchKeyword}%"));
                }

                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters.ToArray());

                if (dt != null && dt.Rows.Count > 0)
                {
                    // Bind the DataTable to the DataGridView
                    dgvCustomer.DataSource = dt;

                    // Set the column headers for better readability
                    dgvCustomer.Columns["ID"].HeaderText = "Employee ID";
                    dgvCustomer.Columns["Name"].HeaderText = "Full Name";
                    dgvCustomer.Columns["Gmail"].HeaderText = "Email";
                    dgvCustomer.Columns["Salary"].HeaderText = "Salary";
                    dgvCustomer.Columns["PhoneNumber"].HeaderText = "Phone Number";
                    dgvCustomer.Columns["Birthday"].HeaderText = "Birthday";
                    dgvCustomer.Columns["StartDate"].HeaderText = "Start Date";
                    dgvCustomer.Columns["Username"].HeaderText = "Username";
                    dgvCustomer.Columns["Password"].HeaderText = "Password";

                    dgvCustomer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvCustomer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvCustomer.ReadOnly = true;
                    dgvCustomer.AllowUserToAddRows = false;
                    dgvCustomer.ColumnHeadersHeight = 40;
                    dgvCustomer.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                    dgvCustomer.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else
                {
                    MessageBox.Show("No employees found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvCustomer.DataSource = null; // Clear the grid if no data
                    ClearFormFields(); // Clear the form fields if no data
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading employee data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEmployeeForm addEmployeeForm = new AddEmployeeForm();
            addEmployeeForm.ShowDialog();
            LoadEmployeeData();
        }

        private void dgvCustomer_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomer.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvCustomer.SelectedRows[0];

                // Populate the form fields with the selected employee's data
                IDTextBox.Text = selectedRow.Cells["ID"].Value.ToString();
                FullNameTextBox.Text = selectedRow.Cells["Name"].Value.ToString();
                salaryTexBox.Text = selectedRow.Cells["Salary"].Value.ToString();
                phonenumberTextBox.Text = selectedRow.Cells["PhoneNumber"].Value.ToString();
                UsernameTextBox.Text = selectedRow.Cells["Username"].Value.ToString();
                PasswordTextBox.Text = selectedRow.Cells["Password"].Value.ToString();
                BirthDayDateTimePicker.Value = Convert.ToDateTime(selectedRow.Cells["Birthday"].Value);
                HireDateDateTimePicker.Value = Convert.ToDateTime(selectedRow.Cells["StartDate"].Value);
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (dgvCustomer.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an employee to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this employee?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string id = dgvCustomer.SelectedRows[0].Cells["ID"].Value.ToString();

                    // Step 1: Delete from Employee table first (due to foreign key constraint)
                    string deleteEmployeeQuery = "DELETE FROM Employee WHERE ID = @ID";
                    SqlParameter[] employeeParams = new SqlParameter[]
                    {
                        new SqlParameter("@ID", id)
                    };
                    int employeeRowsAffected = DatabaseHelper.ExecuteNonQuery(deleteEmployeeQuery, employeeParams);

                    if (employeeRowsAffected == 0)
                    {
                        MessageBox.Show("Failed to delete employee from Employee table.", "Database Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Step 2: Delete from Users table
                    string deleteUserQuery = "DELETE FROM Users WHERE ID = @ID";
                    SqlParameter[] userParams = new SqlParameter[]
                    {
                        new SqlParameter("@ID", id)
                    };
                    int userRowsAffected = DatabaseHelper.ExecuteNonQuery(deleteUserQuery, userParams);

                    if (userRowsAffected > 0)
                    {
                        MessageBox.Show("Employee deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadEmployeeData(); // Refresh the DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete user from Users table.", "Database Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting employee: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ChangeBtn_Click(object sender, EventArgs e)
        {
            if (dgvCustomer.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an employee to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Retrieve updated values from the form
            string id = IDTextBox.Text.Trim();
            string fullName = FullNameTextBox.Text.Trim();
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordTextBox.Text.Trim();
            string phoneNumber = phonenumberTextBox.Text.Trim();
            decimal salary;
            if (!decimal.TryParse(salaryTexBox.Text.Trim(), out salary))
            {
                MessageBox.Show("Please enter a valid salary.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DateTime birthday = BirthDayDateTimePicker.Value;
            DateTime hireDate = HireDateDateTimePicker.Value;

            // Validate inputs
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(phoneNumber))
            {
                MessageBox.Show("Please fill in all required fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Gmail can be constructed or updated
            string gmail = $"{username}@employee.com";

            try
            {
                // Step 1: Update the Users table
                string updateUserQuery = "UPDATE Users SET Username = @Username, Password = @Password WHERE ID = @ID";
                SqlParameter[] userParams = new SqlParameter[]
                {
                    new SqlParameter("@ID", id),
                    new SqlParameter("@Username", username),
                    new SqlParameter("@Password", password)
                };
                int userRowsAffected = DatabaseHelper.ExecuteNonQuery(updateUserQuery, userParams);

                if (userRowsAffected == 0)
                {
                    MessageBox.Show("Failed to update user in Users table.", "Database Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Step 2: Update the Employee table
                string updateEmployeeQuery = @"
                    UPDATE Employee 
                    SET Name = @Name, Gmail = @Gmail, Salary = @Salary, PhoneNumber = @PhoneNumber, 
                        Birthday = @Birthday, StartDate = @StartDate 
                    WHERE ID = @ID";
                SqlParameter[] employeeParams = new SqlParameter[]
                {
                    new SqlParameter("@ID", id),
                    new SqlParameter("@Name", fullName),
                    new SqlParameter("@Gmail", gmail),
                    new SqlParameter("@Salary", salary),
                    new SqlParameter("@PhoneNumber", phoneNumber),
                    new SqlParameter("@Birthday", birthday),
                    new SqlParameter("@StartDate", hireDate)
                };
                int employeeRowsAffected = DatabaseHelper.ExecuteNonQuery(updateEmployeeQuery, employeeParams);

                if (employeeRowsAffected > 0)
                {
                    MessageBox.Show("Employee updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadEmployeeData(); // Refresh the DataGridView
                }
                else
                {
                    MessageBox.Show("Failed to update employee in Employee table.", "Database Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating employee: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchKeyword = SearchTextBox.Text.Trim();
            LoadEmployeeData(searchKeyword);
        }
    }
}
