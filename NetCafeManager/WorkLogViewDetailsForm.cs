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
            ApplyCustomTheme();
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
                    MessageBox.Show("Shift not found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DateTime startTime = Convert.ToDateTime(shiftDt.Rows[0]["StartTime"]);
                DateTime? endTime = null;

                if (shiftDt.Rows[0]["EndTime"] != DBNull.Value)
                {
                    endTime = Convert.ToDateTime(shiftDt.Rows[0]["EndTime"]);
                }

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
                    MessageBox.Show("No bill details found for this shift!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dgvBillDetails.DataSource = billDt;
                dgvBillDetails.ColumnHeadersHeight = 40;

                dgvBillDetails.Columns["ServiceName"].HeaderText = "Service Name";
                dgvBillDetails.Columns["Quantity"].HeaderText = "Quantity";
                dgvBillDetails.Columns["Total"].HeaderText = "Total";

                dgvBillDetails.Columns["Total"].DefaultCellStyle.Format = "N0";
                dgvBillDetails.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvBillDetails.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading bill details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ApplyCustomTheme()
        {
            // Set DataGridView  Theme
            dgvBillDetails.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default;
            dgvBillDetails.EnableHeadersVisualStyles = false;


            // Header
            dgvBillDetails.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(10, 50, 50); 
            dgvBillDetails.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(19, 250, 168); 
            dgvBillDetails.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvBillDetails.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBillDetails.ColumnHeadersHeight = 40;

            // Normal line
            dgvBillDetails.DefaultCellStyle.BackColor = Color.FromArgb(20, 20, 20); 
            dgvBillDetails.DefaultCellStyle.ForeColor = Color.White; 
            dgvBillDetails.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvBillDetails.DefaultCellStyle.SelectionBackColor = Color.FromArgb(10, 150, 100); 
            dgvBillDetails.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvBillDetails.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Next line 
            dgvBillDetails.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(20, 20, 20); 

            // DGV
            dgvBillDetails.BackgroundColor = Color.FromArgb(20, 20, 20); 
            dgvBillDetails.BorderStyle = BorderStyle.None;
            dgvBillDetails.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvBillDetails.RowTemplate.Height = 35;


            dgvBillDetails.ReadOnly = true;
            dgvBillDetails.AllowUserToAddRows = false;
            dgvBillDetails.AllowUserToResizeRows = false;
            dgvBillDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBillDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}