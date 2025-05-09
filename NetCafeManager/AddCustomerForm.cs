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

namespace NetCafeManager
{
    public partial class AddCustomerForm : Form
    {
        public AddCustomerForm()
        {
            InitializeComponent();
        }
        private string GenerateNewID()
        {
            string query = "SELECT MAX(ID) FROM Users WHERE ID LIKE 'C%'";
            object result = DatabaseHelper.ExecuteScalar(query);

            string newID = "C01";
            if (result != null && result != DBNull.Value)
            {
                string maxID = result.ToString();
                int number = int.Parse(maxID.Substring(1)) + 1;
                newID = $"C{number:D2}";
            }
            return newID;
        }
        private void btnPower_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string id = guna2TextBox9.Text.Trim();
            if (string.IsNullOrEmpty(id))
            {
                id = GenerateNewID();
                guna2TextBox9.Text = id;
            }

            string fullName = guna2TextBox10.Text.Trim();
            string username = AccountTextBox.Text.Trim();
            string password = PasswordTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string balanceText = BalanceTextBox.Text.Trim();

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(balanceText))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(balanceText, out decimal balance))
            {
                MessageBox.Show("Balance must be a valid number.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string insertUserQuery = "INSERT INTO Users (ID, USERNAME, Password, Role) " +
                                     "VALUES (@ID, @Username, @Password, @Role)";
            SqlParameter[] userParams = new SqlParameter[]
            {
                new SqlParameter("@ID", id),
                new SqlParameter("@Username", username),
                new SqlParameter("@Password", password),
                new SqlParameter("@Role", "Customer")
            };

            int userRowsAffected = DatabaseHelper.ExecuteNonQuery(insertUserQuery, userParams);
            if (userRowsAffected == 0)
            {
                return;
            }

            string insertCustomerQuery = "INSERT INTO Customer (UserID, FullName, Email, Balance) " +
                                         "VALUES (@UserID, @FullName, @Email, @Balance)";
            SqlParameter[] customerParams = new SqlParameter[]
            {
                new SqlParameter("@UserID", id),
                new SqlParameter("@FullName", fullName),
                new SqlParameter("@Email", email),
                new SqlParameter("@Balance", balance)
            };

            int customerRowsAffected = DatabaseHelper.ExecuteNonQuery(insertCustomerQuery, customerParams);
            if (customerRowsAffected > 0)
            {
                MessageBox.Show("Customer added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
    }
}
