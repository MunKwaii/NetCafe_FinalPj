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

namespace NetCafeManager
{
    public partial class AddBalanceForm : Form
    {
        private string userID;
        private decimal balance;
        private const int costPerHour = 1100000;

        public AddBalanceForm(string userID)
        {
            InitializeComponent();
            this.userID = userID;
            LoadCustomerInfo();
        }

        private void LoadCustomerInfo()
        {
            try
            {
                string query = "SELECT FullName, Balance FROM Customer WHERE UserID = @UserID";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", userID)
                };
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Customer information not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                lblCustomerName.Text = dt.Rows[0]["FullName"].ToString();
                balance = Convert.ToDecimal(dt.Rows[0]["Balance"]);
                lblBalance.Text = balance.ToString("N0") + "đ";
                UpdateTimeDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer information: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void UpdateTimeDisplay()
        {
            decimal totalHours = balance / costPerHour;
            int totalMinutes = (int)(totalHours * 60);
            int hours = totalMinutes / 60;
            int minutes = totalMinutes % 60;
            lblTimeLeft.Text = $"{hours}h {minutes}m";
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!decimal.TryParse(txtDepositAmount.Text, out decimal depositAmount) || depositAmount <= 0)
                {
                    MessageBox.Show("Please enter a valid amount!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string balanceQuery = "SELECT Balance FROM Customer WHERE UserID = @UserID";
                SqlParameter[] balanceParams = new SqlParameter[]
                {
                    new SqlParameter("@UserID", userID)
                };
                DataTable balanceDt = DatabaseHelper.ExecuteQuery(balanceQuery, balanceParams);

                if (balanceDt.Rows.Count == 0)
                {
                    MessageBox.Show("Customer information not found!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                balance = Convert.ToDecimal(balanceDt.Rows[0]["Balance"]);
                decimal newBalance = balance + depositAmount;

                string updateQuery = "UPDATE Customer SET Balance = @Balance WHERE UserID = @UserID";
                SqlParameter[] updateParams = new SqlParameter[]
                {
                    new SqlParameter("@Balance", newBalance),
                    new SqlParameter("@UserID", userID)
                };
                DatabaseHelper.ExecuteNonQuery(updateQuery, updateParams);

                MessageBox.Show($"Deposit successful! New balance: {newBalance:N0}đ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                balance = newBalance;
                LoadCustomerInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during deposit: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPower_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}