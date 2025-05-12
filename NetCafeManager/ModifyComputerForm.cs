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
    public partial class ModifyComputerForm : Form
    {
        public ModifyComputerForm()
        {
            InitializeComponent();
        }

        private void btnPower_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string computerID = txtComputerID.Text.Trim();

            if (string.IsNullOrEmpty(computerID))
            {
                MessageBox.Show("Please enter ComputerID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string checkQuery = "SELECT COUNT(*) FROM Computer WHERE ComputerID = @ComputerID";
                SqlParameter[] checkParams = new SqlParameter[]
                {
                    new SqlParameter("@ComputerID", computerID)
                };
                int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(checkQuery, checkParams));

                if (count > 0)
                {
                    MessageBox.Show("ComputerID already exists! Please enter a different ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string insertQuery = @"
                    INSERT INTO Computer (ComputerID, UserID, StartTime, EndTime, Status)
                    VALUES (@ComputerID, NULL, NULL, NULL, 'Idle')";
                SqlParameter[] insertParams = new SqlParameter[]
                {
                    new SqlParameter("@ComputerID", computerID)
                };
                DatabaseHelper.ExecuteNonQuery(insertQuery, insertParams);

                MessageBox.Show($"Computer {computerID} added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtComputerID.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding computer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            string computerID = txtComputerID.Text.Trim();

            if (string.IsNullOrEmpty(computerID))
            {
                MessageBox.Show("Please enter ComputerID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string checkQuery = "SELECT UserID FROM Computer WHERE ComputerID = @ComputerID";
                SqlParameter[] checkParams = new SqlParameter[]
                {
                    new SqlParameter("@ComputerID", computerID)
                };
                DataTable dt = DatabaseHelper.ExecuteQuery(checkQuery, checkParams);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("ComputerID does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (dt.Rows[0]["UserID"] != DBNull.Value)
                {
                    MessageBox.Show("This computer is currently in use! Please log out the customer before deleting!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string deleteQuery = "DELETE FROM Computer WHERE ComputerID = @ComputerID";
                SqlParameter[] deleteParams = new SqlParameter[]
                {
                    new SqlParameter("@ComputerID", computerID)
                };
                DatabaseHelper.ExecuteNonQuery(deleteQuery, deleteParams);

                MessageBox.Show($"Computer {computerID} deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtComputerID.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting computer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMaintain_Click(object sender, EventArgs e)
        {
            string computerID = txtComputerID.Text.Trim();

            if (string.IsNullOrEmpty(computerID))
            {
                MessageBox.Show("Please enter ComputerID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string checkQuery = "SELECT UserID, Status FROM Computer WHERE ComputerID = @ComputerID";
                SqlParameter[] checkParams = new SqlParameter[]
                {
                    new SqlParameter("@ComputerID", computerID)
                };
                DataTable dt = DatabaseHelper.ExecuteQuery(checkQuery, checkParams);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("ComputerID does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (dt.Rows[0]["UserID"] != DBNull.Value)
                {
                    MessageBox.Show("This computer is currently in use! Please log out the customer before maintenance.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string currentStatus = dt.Rows[0]["Status"].ToString();

                string newStatus;
                if (currentStatus == "Maintain")
                {
                    newStatus = "Idle";
                }
                else
                {
                    newStatus = "Maintain";
                }

                string updateQuery = "UPDATE Computer SET Status = @Status WHERE ComputerID = @ComputerID";
                SqlParameter[] updateParams = new SqlParameter[]
                {
                    new SqlParameter("@Status", newStatus),
                    new SqlParameter("@ComputerID", computerID)
                };
                DatabaseHelper.ExecuteNonQuery(updateQuery, updateParams);

                string message;
                if (newStatus == "Maintain")
                {
                    message = "The computer has been set to maintenance status!";
                }
                else
                {
                    message = "The maintenance status has been removed for the computer!";
                }

                MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtComputerID.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error changing maintenance status: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}