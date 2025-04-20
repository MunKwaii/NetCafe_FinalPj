using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetCafeManager.UserControls
{
    public partial class UC_TakeOrder : UserControl
    {
        public UC_TakeOrder()
        {
            InitializeComponent();
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

        
    }
}
