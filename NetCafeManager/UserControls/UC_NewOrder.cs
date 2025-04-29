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

                // Tùy chỉnh cột
                dgvNewOrder.Columns["OrderID"].HeaderText = "Mã đơn hàng";
                dgvNewOrder.Columns["CustomerID"].HeaderText = "ID Khách hàng";
                dgvNewOrder.Columns["ServiceName"].HeaderText = "Tên món";
                dgvNewOrder.Columns["Quantity"].HeaderText = "Số lượng";
                dgvNewOrder.Columns["Total"].HeaderText = "Thành tiền";
                dgvNewOrder.Columns["OrderDate"].HeaderText = "Thời gian đặt";

                // Định dạng cột
                dgvNewOrder.Columns["Total"].DefaultCellStyle.Format = "N0";
                dgvNewOrder.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvNewOrder.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNewOrder.Columns["OrderDate"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                // Đảm bảo không có hàng nào được chọn mặc định
                dgvNewOrder.ClearSelection();

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Hiện tại không có đơn hàng nào đang chờ xử lý!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách đơn hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (dgvNewOrder.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng để hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("Đơn hàng đã được hủy!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadOrders(); // Làm mới danh sách
            }
            else
            {
                MessageBox.Show("Không thể hủy đơn hàng. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (dgvNewOrder.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng để xác nhận!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int orderID = Convert.ToInt32(dgvNewOrder.SelectedRows[0].Cells["OrderID"].Value);
            string customerID = dgvNewOrder.SelectedRows[0].Cells["CustomerID"].Value?.ToString();
            decimal total = Convert.ToDecimal(dgvNewOrder.SelectedRows[0].Cells["Total"].Value);

            // Kiểm tra số dư của khách hàng
            string balanceQuery = "SELECT Balance FROM Customer WHERE UserID = @CustomerID";
            SqlParameter[] balanceParams = new SqlParameter[]
            {
                new SqlParameter("@CustomerID", customerID)
            };
            DataTable balanceDt = DatabaseHelper.ExecuteQuery(balanceQuery, balanceParams);

            if (balanceDt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy thông tin khách hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal currentBalance = Convert.ToDecimal(balanceDt.Rows[0]["Balance"]);
            if (currentBalance < total)
            {
                MessageBox.Show("Số dư của khách hàng không đủ để thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Cập nhật số dư khách hàng
            decimal newBalance = currentBalance - total;
            string updateBalanceQuery = "UPDATE Customer SET Balance = @Balance WHERE UserID = @CustomerID";
            SqlParameter[] updateBalanceParams = new SqlParameter[]
            {
                new SqlParameter("@Balance", newBalance),
                new SqlParameter("@CustomerID", customerID)
            };
            DatabaseHelper.ExecuteNonQuery(updateBalanceQuery, updateBalanceParams);

            // Cập nhật trạng thái đơn hàng thành Confirmed
            string confirmQuery = "UPDATE Orders SET Status = 'Confirmed' WHERE OrderID = @OrderID";
            SqlParameter[] confirmParams = new SqlParameter[]
            {
                new SqlParameter("@OrderID", orderID)
            };
            DatabaseHelper.ExecuteNonQuery(confirmQuery, confirmParams);

            MessageBox.Show("Đơn hàng đã được xác nhận!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Tải lại danh sách đơn hàng
            LoadOrders();
        }
    }
}