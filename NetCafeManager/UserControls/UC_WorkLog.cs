using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using Microsoft.Data.SqlClient;

namespace NetCafeManager.UserControls
{
    public partial class UC_WorkLog : UserControl
    {
        public UC_WorkLog()
        {
            InitializeComponent();
            dgvWorkLog.CellFormatting += DgvWorkLog_CellFormatting;
            LoadShiftInfo();
            LoadWorkLog();
            ApplyCustomTheme();
        }

        private void DgvWorkLog_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvWorkLog.Columns["EndTime"].Index && e.Value == DBNull.Value)
            {
                e.Value = "Not Ended";
                e.FormattingApplied = true;
            }
            else if (e.ColumnIndex == dgvWorkLog.Columns["EndTime"].Index && e.Value != null)
            {
                if (DateTime.TryParse(e.Value.ToString(), out DateTime endTime))
                {
                    e.Value = endTime.ToString("dd/MM/yyyy HH:mm");
                    e.FormattingApplied = true;
                }
            }
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (dgvWorkLog.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a work shift to view details!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int shiftID = Convert.ToInt32(dgvWorkLog.SelectedRows[0].Cells["ShiftID"].Value);
            WorkLogViewDetailsForm detailsForm = new WorkLogViewDetailsForm(shiftID);
            detailsForm.ShowDialog();
        }

        private void LoadShiftInfo()
        {
            try
            {
                string query = @"
                    SELECT TOP 1 s.StartTime, e.Name AS EmployeeName
                    FROM EmployeeShift s
                    LEFT JOIN Employee e ON s.EmployeeID = e.ID
                    ORDER BY s.StartTime DESC";
                DataTable dt = DatabaseHelper.ExecuteQuery(query);

                if (dt.Rows.Count > 0)
                {
                    DateTime startTime = (DateTime)dt.Rows[0]["StartTime"];
                    string employeeName = dt.Rows[0]["EmployeeName"].ToString();
                    lblStartDate.Text = startTime.ToString("dd/MM/yyyy");
                    lblStartTime.Text = startTime.ToString("HH:mm") + $" (Employee: {employeeName})";
                }
                else
                {
                    lblStartDate.Text = "No Work Shift Available";
                    lblStartTime.Text = "No Work Shift Available";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading shift information: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStartDate.Text = "Error Loading Data";
                lblStartTime.Text = "Error Loading Data";
            }
        }

        private void LoadWorkLog()
        {
            try
            {
                string query = @"
                    SELECT s.ShiftID, e.Name AS EmployeeName, s.StartTime, 
                           s.EndTime, s.TotalAmount
                    FROM EmployeeShift s
                    LEFT JOIN Employee e ON s.EmployeeID = e.ID
                    ORDER BY s.StartTime DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(query);
                if (dt == null) throw new Exception("Cannot load data from the database");
                foreach (DataRow row in dt.Rows)
                {
                    int shiftID = Convert.ToInt32(row["ShiftID"]);
                    DateTime startTime = Convert.ToDateTime(row["StartTime"]);
                    DateTime? endTime = row["EndTime"] != DBNull.Value ? Convert.ToDateTime(row["EndTime"]) : (DateTime?)null;
                    string billQuery;
                    SqlParameter[] billParams;
                    decimal totalAmount = 0;

                    if (endTime.HasValue)
                    {
                        billQuery = @"
                            SELECT SUM(o.Total) AS Total
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
                            SELECT SUM(o.Total) AS Total
                            FROM Orders o
                            WHERE o.OrderDate >= @StartTime
                            AND o.Status = 'Confirmed'";
                        billParams = new SqlParameter[]
                        {
                            new SqlParameter("@StartTime", startTime)
                        };
                    }

                    DataTable billDt = DatabaseHelper.ExecuteQuery(billQuery, billParams);
                    if (billDt.Rows.Count > 0 && billDt.Rows[0]["Total"] != DBNull.Value)
                    {
                        totalAmount = Convert.ToDecimal(billDt.Rows[0]["Total"]);
                    }
                    string updateQuery = @"
                        UPDATE EmployeeShift
                        SET TotalAmount = @TotalAmount
                        WHERE ShiftID = @ShiftID";
                    SqlParameter[] updateParams = new SqlParameter[]
                    {
                        new SqlParameter("@TotalAmount", totalAmount),
                        new SqlParameter("@ShiftID", shiftID)
                    };
                    DatabaseHelper.ExecuteNonQuery(updateQuery, updateParams);
                    row["TotalAmount"] = totalAmount;
                }

                dgvWorkLog.DataSource = dt;
                dgvWorkLog.ColumnHeadersHeight = 40;

                dgvWorkLog.Columns["ShiftID"].HeaderText = "Shift ID";
                dgvWorkLog.Columns["EmployeeName"].HeaderText = "Employee Name";
                dgvWorkLog.Columns["StartTime"].HeaderText = "Start Time";
                dgvWorkLog.Columns["EndTime"].HeaderText = "End Time";
                dgvWorkLog.Columns["TotalAmount"].HeaderText = "Total Amount";

                dgvWorkLog.Columns["TotalAmount"].DefaultCellStyle.Format = "N0";
                dgvWorkLog.Columns["TotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvWorkLog.Columns["StartTime"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                dgvWorkLog.ClearSelection();
                decimal grandTotal = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["TotalAmount"] != DBNull.Value)
                    {
                        grandTotal += Convert.ToDecimal(row["TotalAmount"]);
                    }
                }
                lblTotalAmount.Text = grandTotal.ToString("N0") + "đ";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading work log: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShiftSummary_Click(object sender, EventArgs e)
        {
            if (dgvWorkLog.Rows.Count == 0)
            {
                MessageBox.Show("No data to export!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to export the Shift Summary?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV Files (*.csv)|*.csv";
                sfd.FileName = "ShiftSummary_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var writer = new StreamWriter(sfd.FileName, false, new UTF8Encoding(true)))
                        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                        {
                            csv.WriteField("Shift ID");
                            csv.WriteField("Employee Name");
                            csv.WriteField("Start Time");
                            csv.WriteField("End Time");
                            csv.WriteField("Total Amount");
                            csv.NextRecord();

                            foreach (DataGridViewRow row in dgvWorkLog.Rows)
                            {
                                csv.WriteField(row.Cells["ShiftID"].Value?.ToString());
                                csv.WriteField(row.Cells["EmployeeName"].Value?.ToString());
                                csv.WriteField(row.Cells["StartTime"].Value != null ? Convert.ToDateTime(row.Cells["StartTime"].Value).ToString("dd/MM/yyyy HH:mm") : "");
                                if (row.Cells["EndTime"].Value == null || row.Cells["EndTime"].Value == DBNull.Value)
                                {
                                    csv.WriteField("Not Ended");
                                }
                                else
                                {
                                    csv.WriteField(Convert.ToDateTime(row.Cells["EndTime"].Value).ToString("dd/MM/yyyy HH:mm"));
                                }
                                csv.WriteField(row.Cells["TotalAmount"].Value?.ToString());
                                csv.NextRecord();
                            }

                            writer.Flush();
                            MessageBox.Show("Shift Summary exported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error exporting file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void ApplyCustomTheme()
        {
            // Set DataGridView  Theme
            dgvWorkLog.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default;
            dgvWorkLog.EnableHeadersVisualStyles = false;


            // Header
            dgvWorkLog.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(10, 50, 50); 
            dgvWorkLog.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(19, 250, 168); 
            dgvWorkLog.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvWorkLog.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvWorkLog.ColumnHeadersHeight = 40;

            // Normal line
            dgvWorkLog.DefaultCellStyle.BackColor = Color.FromArgb(20, 20, 20); 
            dgvWorkLog.DefaultCellStyle.ForeColor = Color.White; 
            dgvWorkLog.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvWorkLog.DefaultCellStyle.SelectionBackColor = Color.FromArgb(10, 150, 100); 
            dgvWorkLog.DefaultCellStyle.SelectionForeColor = Color.White; 
            dgvWorkLog.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Next line
            dgvWorkLog.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(20, 20, 20); 

            // DGV
            dgvWorkLog.BackgroundColor = Color.FromArgb(20, 20, 20); 
            dgvWorkLog.BorderStyle = BorderStyle.None;
            dgvWorkLog.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvWorkLog.RowTemplate.Height = 35;


            dgvWorkLog.ReadOnly = true;
            dgvWorkLog.AllowUserToAddRows = false;
            dgvWorkLog.AllowUserToResizeRows = false;
            dgvWorkLog.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvWorkLog.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}