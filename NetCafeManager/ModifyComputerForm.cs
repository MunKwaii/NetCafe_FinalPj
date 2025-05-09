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
                MessageBox.Show("Vui lòng nhập ComputerID!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show("ComputerID đã tồn tại! Vui lòng nhập ID khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                MessageBox.Show($"Đã thêm máy tính {computerID} thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtComputerID.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm máy tính: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            string computerID = txtComputerID.Text.Trim();

            if (string.IsNullOrEmpty(computerID))
            {
                MessageBox.Show("Vui lòng nhập ComputerID!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show("ComputerID không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (dt.Rows[0]["UserID"] != DBNull.Value)
                {
                    MessageBox.Show("Máy tính này đang được sử dụng! Vui lòng đăng xuất khách hàng trước khi xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string deleteQuery = "DELETE FROM Computer WHERE ComputerID = @ComputerID";
                SqlParameter[] deleteParams = new SqlParameter[]
                {
                    new SqlParameter("@ComputerID", computerID)
                };
                DatabaseHelper.ExecuteNonQuery(deleteQuery, deleteParams);

                MessageBox.Show($"Đã xóa máy tính {computerID} thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtComputerID.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa máy tính: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMaintain_Click(object sender, EventArgs e)
        {
            string computerID = txtComputerID.Text.Trim();

            if (string.IsNullOrEmpty(computerID))
            {
                MessageBox.Show("Vui lòng nhập ComputerID!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show("ComputerID không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (dt.Rows[0]["UserID"] != DBNull.Value)
                {
                    MessageBox.Show("Máy tính này đang được sử dụng! Vui lòng đăng xuất khách hàng trước khi bảo trì.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string currentStatus = dt.Rows[0]["Status"].ToString();
                string newStatus = (currentStatus == "Maintain") ? "Idle" : "Maintain";

                string updateQuery = "UPDATE Computer SET Status = @Status WHERE ComputerID = @ComputerID";
                SqlParameter[] updateParams = new SqlParameter[]
                {
                    new SqlParameter("@Status", newStatus),
                    new SqlParameter("@ComputerID", computerID)
                };
                DatabaseHelper.ExecuteNonQuery(updateQuery, updateParams);

                string message = (newStatus == "Maintain") ? "Đã đặt máy tính vào trạng thái bảo trì!" : "Đã bỏ trạng thái bảo trì cho máy tính!";
                MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtComputerID.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thay đổi trạng thái bảo trì: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}