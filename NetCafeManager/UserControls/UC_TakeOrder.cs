using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

namespace NetCafeManager.UserControls
{
    public partial class UC_TakeOrder : UserControl
    {
        public string UserID { get; set; }
        private bool requireUserID;

        public UC_TakeOrder(string userID, bool requireUserID = true)
        {
            if (requireUserID && string.IsNullOrEmpty(userID))
            {
                MessageBox.Show("Invalid UserID in UC_TakeOrder!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new ArgumentException("UserID cannot be null or empty.", nameof(userID));
            }

            InitializeComponent();
            this.UserID = userID;
            this.requireUserID = requireUserID;
            this.Load += UC_TakeOrder_Load;
            guna2DataGridView1.CellContentClick += Guna2DataGridView1_CellContentClick;
            ApplyCustomTheme();
        }

        private void Guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && guna2DataGridView1.Columns[e.ColumnIndex].Name == "Delete")
            {
                if (guna2DataGridView1.Rows[e.RowIndex].IsNewRow)
                {
                    MessageBox.Show("Cannot delete an empty row!", "Warning",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string productName = guna2DataGridView1.Rows[e.RowIndex].Cells["ProductName"].Value?.ToString();
                DialogResult result = MessageBox.Show($"Delete the item {productName}?", "Confirmation",
                                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    guna2DataGridView1.Rows.RemoveAt(e.RowIndex);
                    CalculateTotal();
                }
            }
        }

        public void AddProductToOrder(string productName, decimal price, int quantity = 1)
        {
            bool productExists = false;
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.Cells["ProductName"].Value?.ToString() == productName)
                {
                    int currentQty = Convert.ToInt32(row.Cells["Quantity"].Value);
                    row.Cells["Quantity"].Value = currentQty + quantity;
                    row.Cells["Total"].Value = (currentQty + quantity) * price;
                    productExists = true;
                    break;
                }
            }

            if (!productExists)
            {
                guna2DataGridView1.Rows.Add(productName, price, quantity, price * quantity);
            }
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.Cells["Total"].Value != null)
                {
                    total += Convert.ToDecimal(row.Cells["Total"].Value);
                }
            }
            label4.Text = total.ToString("N0") + "000đ";
        }

        private void UC_TakeOrder_Load(object sender, EventArgs e)
        {
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            guna2DataGridView1.Columns.Clear();
            guna2DataGridView1.Columns.Add("ProductName", "Product Name");
            guna2DataGridView1.Columns.Add("Price", "Price");
            guna2DataGridView1.Columns.Add("Quantity", "Quantity");
            guna2DataGridView1.Columns.Add("Total", "Total");
            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnDelete.Name = "Delete";
            btnDelete.HeaderText = "Action";
            btnDelete.Text = "Delete";
            btnDelete.UseColumnTextForButtonValue = true;
            btnDelete.Width = 80;
            btnDelete.DefaultCellStyle.BackColor = Color.FromArgb(255, 80, 80);
            btnDelete.DefaultCellStyle.ForeColor = Color.White;
            btnDelete.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            guna2DataGridView1.Columns.Add(btnDelete);
            guna2DataGridView1.Columns["Price"].DefaultCellStyle.Format = "N0";
            guna2DataGridView1.Columns["Total"].DefaultCellStyle.Format = "N0";
            guna2DataGridView1.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            guna2DataGridView1.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            guna2DataGridView1.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            decimal totalAmount = 0;
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.Cells["Total"].Value != null)
                {
                    totalAmount += Convert.ToDecimal(row.Cells["Total"].Value);
                }
            }
            totalAmount = Math.Floor(totalAmount);

            if (totalAmount <= 0)
            {
                MessageBox.Show("Invalid total amount (<= 0)! Please check again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Nếu requireUserID = true (khách đặt), lưu với Status = 'Pending'
            // Nếu requireUserID = false (nhân viên đặt), lưu với Status = 'Confirmed'
            string orderStatus;
            if (requireUserID)
            {
                orderStatus = "Pending";
            }
            else
            {
                orderStatus = "Confirmed";
            }

            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.Cells["ProductName"].Value != null)
                {
                    string productName = row.Cells["ProductName"].Value.ToString();
                    int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                    decimal total = Convert.ToDecimal(row.Cells["Total"].Value);

                    string insertQuery = @"
                        INSERT INTO Orders (CustomerID, ServiceName, Quantity, Total, OrderDate, Status)
                        VALUES (@CustomerID, @ServiceName, @Quantity, @Total, @OrderDate, @Status)";
                    SqlParameter[] insertParams = new SqlParameter[]
                    {
                        new SqlParameter("@CustomerID", requireUserID ? (object)UserID : DBNull.Value),
                        new SqlParameter("@ServiceName", productName),
                        new SqlParameter("@Quantity", quantity),
                        new SqlParameter("@Total", total * 1000),
                        new SqlParameter("@OrderDate", DateTime.Now),
                        new SqlParameter("@Status", orderStatus)
                    };
                    DatabaseHelper.ExecuteNonQuery(insertQuery, insertParams);
                }
            }

            if (requireUserID)
            {
                if (this.ParentForm is CustomerForm customerForm)
                {
                    customerForm.UpdateTotalFoodFee(totalAmount * 1000);
                }

                MessageBox.Show($"Order placed successfully! The order is pending confirmation.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DialogResult result = MessageBox.Show($"Total amount: {totalAmount * 1000:N0}đ\nHas the customer paid ?", "Confirm Payment", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    MessageBox.Show($"Order placed successfully!\nTotal amount: {totalAmount * 1000:N0}đ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please ensure the customer has paid before continuing!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            guna2DataGridView1.Rows.Clear();
            label4.Text = "0đ";
        }
         private void ApplyCustomTheme()
        {
            // Set DataGridView  Theme
            guna2DataGridView1.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default;
            guna2DataGridView1.EnableHeadersVisualStyles = false;


            // Header
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(10, 50, 50); 
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(19, 250, 168); 
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            guna2DataGridView1.ColumnHeadersHeight = 40;

            // Normal line
            guna2DataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(20, 20, 20); 
            guna2DataGridView1.DefaultCellStyle.ForeColor = Color.White; 
            guna2DataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            guna2DataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(10, 150, 100); 
            guna2DataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            guna2DataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Next line
            guna2DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(20, 20, 20); 

            // DGV
            guna2DataGridView1.BackgroundColor = Color.FromArgb(20, 20, 20); 
            guna2DataGridView1.BorderStyle = BorderStyle.None;
            guna2DataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            guna2DataGridView1.RowTemplate.Height = 35;
         

            guna2DataGridView1.ReadOnly = true;
            guna2DataGridView1.AllowUserToAddRows = false;
            guna2DataGridView1.AllowUserToResizeRows = false;
            guna2DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}