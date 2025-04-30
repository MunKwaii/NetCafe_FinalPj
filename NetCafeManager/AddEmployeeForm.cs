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

namespace NetCafeManager
{
    public partial class AddEmployeeForm : Form
    {
        public AddEmployeeForm()
        {
            InitializeComponent();
        }

        private void btnPower_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            string id = IDTextBox.Text.Trim();
            string fullName = FullNameTextBox.Text.Trim();
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordTextBox.Text.Trim();
            string phoneNumber = PhoneNumberTextBox.Text.Trim();
            decimal salary;
            if (!decimal.TryParse(SalaryTextBox.Text.Trim(), out salary))
            {
                MessageBox.Show("Please enter a valid salary.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DateTime birthday = BirthDayDateTimePicker.Value;
            DateTime hireDate = HireDayDateTimePicker.Value;

            // Validate inputs
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(phoneNumber))
            {
                MessageBox.Show("Please fill in all required fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Gmail can be constructed or left as a default value if not provided
            string gmail = $"{username}@employee.com";

            try
            {
                // Step 1: Insert into Users table
                string insertUserQuery = "INSERT INTO Users (ID, Username, Password, Role) VALUES (@ID, @Username, @Password, @Role)";
                SqlParameter[] userParams = new SqlParameter[]
                {
                    new SqlParameter("@ID", id),
                    new SqlParameter("@Username", username),
                    new SqlParameter("@Password", password),
                    new SqlParameter("@Role", "Employee")
                };

                int userRowsAffected = DatabaseHelper.ExecuteNonQuery(insertUserQuery, userParams);

                if (userRowsAffected == 0)
                {
                    MessageBox.Show("Failed to add user to Users table.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Step 2: Insert into Employee table using the same ID
                string insertEmployeeQuery = "INSERT INTO Employee (ID, Name, Gmail, Salary, PhoneNumber, Birthday, StartDate) " +
                                            "VALUES (@ID, @Name, @Gmail, @Salary, @PhoneNumber, @Birthday, @StartDate)";
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

                int employeeRowsAffected = DatabaseHelper.ExecuteNonQuery(insertEmployeeQuery, employeeParams);

                if (employeeRowsAffected > 0)
                {
                    MessageBox.Show("Employee added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    // If Employee insert fails, rollback the Users insert
                    string deleteUserQuery = "DELETE FROM Users WHERE ID = @ID";
                    SqlParameter[] deleteParams = new SqlParameter[]
                    {
                        new SqlParameter("@ID", id)
                    };
                    DatabaseHelper.ExecuteNonQuery(deleteUserQuery, deleteParams);
                    MessageBox.Show("Failed to add employee to Employee table.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
