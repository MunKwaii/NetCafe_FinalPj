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
    public partial class WorkLogViewDetailsForm : Form
    {
        private readonly int _shiftID;

        public WorkLogViewDetailsForm(int shiftID)
        {
            InitializeComponent();
            _shiftID = shiftID;
            LoadBillDetails();
        }

        private void LoadBillDetails()
        {
            try
            {
                lblWorkLogID.Text = _shiftID.ToString();

                string shiftQuery = @"
                    SELECT StartTime, EndTime
                    FROM EmployeeShift
                    WHERE ShiftID = @ShiftID";
                SqlParameter[] shiftParams = new SqlParameter[]
                {
                    new SqlParameter("@ShiftID", _shiftID)
                };
                DataTable shiftDt = DatabaseHelper.ExecuteQuery(shiftQuery, shiftParams);

                if (shiftDt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy ca làm việc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DateTime startTime = Convert.ToDateTime(shiftDt.Rows[0]["StartTime"]);
                DateTime? endTime = shiftDt.Rows[0]["EndTime"] != DBNull.Value ? Convert.ToDateTime(shiftDt.Rows[0]["EndTime"]) : (DateTime?)null;

                string billQuery;
                SqlParameter[] billParams;

                if (endTime.HasValue)
                {
                    billQuery = @"
                        SELECT o.ServiceName, o.Quantity, o.Total
                        FROM Orders o
                        WHERE o.OrderDate BETWEEN @StartTime AND @EndTime
                        AND o.Status = 'Confirmed'";
                    billParams = new SqlParameter[]
                    {
                        new SqlParameter("@StartTime", startTime),
                        new SqlParameter("@EndTime", endTime.Value)
                    };
                }
                else
                {
                    billQuery = @"
                        SELECT o.ServiceName, o.Quantity, o.Total
                        FROM Orders o
                        WHERE o.OrderDate >= @StartTime
                        AND o.Status = 'Confirmed'";
                    billParams = new SqlParameter[]
                    {
                        new SqlParameter("@StartTime", startTime)
                    };
                }

                DataTable billDt = DatabaseHelper.ExecuteQuery(billQuery, billParams);

                if (billDt == null || billDt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có chi tiết hóa đơn cho ca làm việc này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dgvBillDetails.DataSource = billDt;
                dgvBillDetails.ColumnHeadersHeight = 40;

                dgvBillDetails.Columns["ServiceName"].HeaderText = "Tên món";
                dgvBillDetails.Columns["Quantity"].HeaderText = "Số lượng";
                dgvBillDetails.Columns["Total"].HeaderText = "Thành tiền";

                dgvBillDetails.Columns["Total"].DefaultCellStyle.Format = "N0";
                dgvBillDetails.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //dgvWorkLog.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvBillDetails.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}