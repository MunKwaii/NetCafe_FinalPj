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

namespace NetCafeManager.UserControls
{
    public partial class UC_NewOrder : UserControl
    {
        public UC_NewOrder()
        {
            InitializeComponent();
            LoadOrders();
            ApplyCustomTheme();
        }

        private void LoadOrders()
        {
            try
            {
                string query = @"
                    SELECT OrderID, CustomerID, ServiceName, Quantity, Total, OrderDate 
                    FROM Orders 
                    WHERE CustomerID IS NOT NULL AND Status = 'Pending'
                    ORDER BY OrderDate DESC";
                DataTable dt = DatabaseHelper.ExecuteQuery(query);

                dgvNewOrder.DataSource = dt;
                dgvNewOrder.ColumnHeadersHeight = 40;

                dgvNewOrder.Columns["OrderID"].HeaderText = "Order ID";
                dgvNewOrder.Columns["CustomerID"].HeaderText = "Customer ID";
                dgvNewOrder.Columns["ServiceName"].HeaderText = "Service Name";
                dgvNewOrder.Columns["Quantity"].HeaderText = "Quantity";
                dgvNewOrder.Columns["Total"].HeaderText = "Total";
                dgvNewOrder.Columns["OrderDate"].HeaderText = "OrderDate";

                dgvNewOrder.Columns["Total"].DefaultCellStyle.Format = "N0";
                dgvNewOrder.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvNewOrder.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNewOrder.Columns["OrderDate"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvNewOrder.ClearSelection();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading order list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (dgvNewOrder.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order to cancel!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int orderID = Convert.ToInt32(dgvNewOrder.SelectedRows[0].Cells["OrderID"].Value);
            string updateQuery = "UPDATE Orders SET Status = 'Cancelled' WHERE OrderID = @OrderID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@OrderID", orderID)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(updateQuery, parameters);
            if (rowsAffected > 0)
            {
                MessageBox.Show("Order has been cancelled!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadOrders();
            }
            else
            {
                MessageBox.Show("Failed to cancel the order. Please try again!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (dgvNewOrder.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order to confirm!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int orderID = Convert.ToInt32(dgvNewOrder.SelectedRows[0].Cells["OrderID"].Value);
            string customerID = dgvNewOrder.SelectedRows[0].Cells["CustomerID"].Value?.ToString();
            decimal total = Convert.ToDecimal(dgvNewOrder.SelectedRows[0].Cells["Total"].Value);
            string balanceQuery = "SELECT Balance FROM Customer WHERE UserID = @CustomerID";
            SqlParameter[] balanceParams = new SqlParameter[]
            {
                new SqlParameter("@CustomerID", customerID)
            };
            DataTable balanceDt = DatabaseHelper.ExecuteQuery(balanceQuery, balanceParams);

            if (balanceDt.Rows.Count == 0)
            {
                MessageBox.Show("Customer information not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal currentBalance = Convert.ToDecimal(balanceDt.Rows[0]["Balance"]);
            if (currentBalance < total)
            {
                MessageBox.Show("Customer's balance is not enough to pay!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            decimal newBalance = currentBalance - total;
            string updateBalanceQuery = "UPDATE Customer SET Balance = @Balance WHERE UserID = @CustomerID";
            SqlParameter[] updateBalanceParams = new SqlParameter[]
            {
                new SqlParameter("@Balance", newBalance),
                new SqlParameter("@CustomerID", customerID)
            };
            DatabaseHelper.ExecuteNonQuery(updateBalanceQuery, updateBalanceParams);
            string confirmQuery = "UPDATE Orders SET Status = 'Confirmed' WHERE OrderID = @OrderID";
            SqlParameter[] confirmParams = new SqlParameter[]
            {
                new SqlParameter("@OrderID", orderID)
            };
            DatabaseHelper.ExecuteNonQuery(confirmQuery, confirmParams);

            MessageBox.Show("Order has been confirmed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadOrders();
        }
        private void ApplyCustomTheme()
        {
            dgvNewOrder.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default;
            dgvNewOrder.EnableHeadersVisualStyles = false;

            // Header
            dgvNewOrder.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(10, 50, 50); 
            dgvNewOrder.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(19, 250, 168); 
            dgvNewOrder.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvNewOrder.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvNewOrder.ColumnHeadersHeight = 40;

            // Normal line
            dgvNewOrder.DefaultCellStyle.BackColor = Color.FromArgb(20, 20, 20); 
            dgvNewOrder.DefaultCellStyle.ForeColor = Color.White; 
            dgvNewOrder.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvNewOrder.DefaultCellStyle.SelectionBackColor = Color.FromArgb(10, 150, 100); 
            dgvNewOrder.DefaultCellStyle.SelectionForeColor = Color.White; 
            dgvNewOrder.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Next Line
            dgvNewOrder.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(20, 20, 20); 

            // DGV
            dgvNewOrder.BackgroundColor = Color.FromArgb(20, 20, 20); 
            dgvNewOrder.BorderStyle = BorderStyle.None;
            dgvNewOrder.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvNewOrder.RowTemplate.Height = 35;


            dgvNewOrder.ReadOnly = true;
            dgvNewOrder.AllowUserToAddRows = false;
            dgvNewOrder.AllowUserToResizeRows = false;
            dgvNewOrder.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNewOrder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}