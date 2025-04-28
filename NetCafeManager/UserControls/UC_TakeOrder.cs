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

        public UC_TakeOrder(string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                MessageBox.Show("UserID không hợp lệ trong UC_TakeOrder!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new ArgumentException("UserID không được null hoặc rỗng.", nameof(userID));
            }

            InitializeComponent();
            this.UserID = userID;
            this.Load += UC_TakeOrder_Load;
            guna2DataGridView1.CellContentClick += Guna2DataGridView1_CellContentClick;

        }
        private void Guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && guna2DataGridView1.Columns[e.ColumnIndex].Name == "Delete")
            {
                // Kiểm tra nếu là hàng mới (new row)
                if (guna2DataGridView1.Rows[e.RowIndex].IsNewRow)
                {
                    MessageBox.Show("Không thể xóa hàng trống!", "Thông báo",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tiếp tục xử lý xóa hàng
                string productName = guna2DataGridView1.Rows[e.RowIndex].Cells["ProductName"].Value?.ToString();

                DialogResult result = MessageBox.Show($"Xóa món {productName}?", "Xác nhận",
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
            // Debug: Kiểm tra giá trị đầu vào
            MessageBox.Show($"Product: {productName}\nPrice: {price}\nQuantity: {quantity}", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            guna2DataGridView1.Columns.Add("ProductName", "Tên món");
            guna2DataGridView1.Columns.Add("Price", "Đơn giá");
            guna2DataGridView1.Columns.Add("Quantity", "Số lượng");
            guna2DataGridView1.Columns.Add("Total", "Thành tiền");
            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnDelete.Name = "Delete";
            btnDelete.HeaderText = "Thao tác";
            btnDelete.Text = "Xóa";
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
            //decimal totalAmount = 0;

            //foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            //{
            //    if (row.Cells["Total"].Value != null)
            //    {
            //        totalAmount += Convert.ToDecimal(row.Cells["Total"].Value);
            //    }
            //}
            //totalAmount = Math.Floor(totalAmount);

            //if (totalAmount == 0)
            //{
            //    MessageBox.Show("Không có món nào để đặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}


            //string query = "SELECT Balance FROM Customer WHERE UserID = @ID";
            //SqlParameter[] parameters = new SqlParameter[]
            //{
            //    new SqlParameter("@ID", UserID)
            //};
            //DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            //if (dt.Rows.Count == 0)
            //{
            //    MessageBox.Show("Không tìm thấy người dùng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //decimal currentBalance = Convert.ToDecimal(dt.Rows[0]["Balance"]);

            //if (currentBalance < totalAmount * 1000)
            //{
            //    MessageBox.Show("Số dư không đủ để đặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //decimal newBalance = currentBalance - (totalAmount * 1000);
            //string updateQuery = "UPDATE Customer SET Balance = @Balance WHERE UserID = @ID";
            //SqlParameter[] updateParams = new SqlParameter[]
            //{
            //        new SqlParameter("@Balance", newBalance),
            //        new SqlParameter("@ID", UserID)
            //};
            //DatabaseHelper.ExecuteNonQuery(updateQuery, updateParams);
            //MessageBox.Show($"{UserID}");
            //MessageBox.Show($"Đặt hàng thành công!\nĐã trừ {totalAmount * 1000:N0}đ", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //guna2DataGridView1.Rows.Clear();
            //label4.Text = "0đ";
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
                MessageBox.Show("Tổng tiền không hợp lệ (<= 0)! Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = "SELECT Balance FROM Customer WHERE UserID = @ID";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@ID", UserID)
            };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy người dùng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal currentBalance = Convert.ToDecimal(dt.Rows[0]["Balance"]);

            if (currentBalance < totalAmount * 1000)
            {
                MessageBox.Show("Số dư không đủ để đặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal newBalance = currentBalance - (totalAmount * 1000);

            string updateQuery = "UPDATE Customer SET Balance = @Balance WHERE UserID = @ID";
            SqlParameter[] updateParams = new SqlParameter[]
            {
        new SqlParameter("@Balance", newBalance),
        new SqlParameter("@ID", UserID)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(updateQuery, updateParams);
            if (rowsAffected == 0)
            {
                MessageBox.Show("Không thể cập nhật số dư. Vui lòng kiểm tra lại UserID hoặc cơ sở dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"Đặt hàng thành công!\nĐã trừ {totalAmount * 1000:N0}đ", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Làm mới số dư trong UC_MyAccount
            if (this.ParentForm is CustomerForm customerForm)
            {
                customerForm.RefreshMyAccountBalance();
            }
            // Truyền tổng số tiền đặt đồ ăn vào UC_MyAccount
            if (this.ParentForm is CustomerForm customerForm1)
            {
                customerForm1.UpdateTotalFoodFee(totalAmount * 1000); // Truyền totalAmount * 1000
            }

            guna2DataGridView1.Rows.Clear();
            label4.Text = "0đ";
        }
    }
}
