using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace NetCafeManager.UserControls
{
    public partial class UC_MyAccount : UserControl
    {
        string ID;
        private decimal balance;
        private int costPerHour = 1100000;
        private Timer timer;
        private decimal usedBalance;
        private decimal initialBalance;
        private decimal totalFoodFee;
        decimal totalFoodFeeSum;
        private Guna.UI2.WinForms.Guna2PictureBox ptbNotify;

        public UC_MyAccount(string ID, Guna.UI2.WinForms.Guna2PictureBox ptbNotify)
        {
            InitializeComponent();
            this.ID = ID;
            this.ptbNotify = ptbNotify;
            LoadData();
            ApplyCustomTheme();
        }

        public decimal TotalFoodFee
        {
            get { return totalFoodFee; }
            set
            {
                totalFoodFee = value;
                totalFoodFeeSum += totalFoodFee;
                lblTotalFoodFee.Text = totalFoodFeeSum.ToString("N0") + "đ";
            }
        }

        private void LoadData()
        {
            string userQuery = @"SELECT FullName, Balance, ComputerID FROM Customer JOIN Computer ON Customer.UserID = Computer.UserID WHERE Customer.UserID = @ID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", ID)
            };
            DataTable dt = DatabaseHelper.ExecuteQuery(userQuery, parameters);
            memberLb.Text = dt.Rows[0]["FullName"].ToString();
            CusNameLB.Text = dt.Rows[0]["FullName"].ToString();
            ComLB.Text = dt.Rows[0]["ComputerID"].ToString();
            balance = Convert.ToDecimal(dt.Rows[0]["Balance"]);
            initialBalance = balance;
            usedBalance = 0;
            BalanceLb.Text = balance.ToString("N0");
            UpdateTimeDisplay();
            UpdateUsageDisplay();
            StartTimer();

            InitializeTransactionDataGridView();
            LoadOrdersToDataGridView();
        }

        private void InitializeTransactionDataGridView()
        {
            dgvTransaction.Columns.Clear();
            dgvTransaction.Columns.Add("OrderID", "Order ID");
            dgvTransaction.Columns.Add("ServiceName", "Service Name");
            dgvTransaction.Columns.Add("Quantity", "Quantity");
            dgvTransaction.Columns.Add("Total", "Total");
            dgvTransaction.Columns.Add("OrderDate", "Order Date");
            dgvTransaction.Columns.Add("Status", "Status");
            dgvTransaction.ColumnHeadersHeight = 40;
            dgvTransaction.Columns["Quantity"].Width = 55;
            dgvTransaction.Columns["OrderID"].Width = 50;

            dgvTransaction.Columns["Total"].DefaultCellStyle.Format = "N0";
            dgvTransaction.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvTransaction.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTransaction.Columns["OrderDate"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
        }

        private void LoadOrdersToDataGridView()
        {
            string orderQuery = @"
                SELECT OrderID, ServiceName, Quantity, Total, OrderDate, Status 
                FROM Orders 
                WHERE CustomerID = @ID AND Status IN ('Confirmed', 'Cancelled')";
            SqlParameter[] orderParams = new SqlParameter[]
            {
                new SqlParameter("@ID", ID)
            };
            DataTable orderDt = DatabaseHelper.ExecuteQuery(orderQuery, orderParams);

            foreach (DataRow row in orderDt.Rows)
            {
                dgvTransaction.Rows.Add(
                    row["OrderID"].ToString(),
                    row["ServiceName"].ToString(),
                    Convert.ToInt32(row["Quantity"]),
                    Convert.ToDecimal(row["Total"]),
                    Convert.ToDateTime(row["OrderDate"]),
                    row["Status"].ToString()
                );
            }
        }

        public void StopTimerAndSaveRevenue()
        {
            if (timer != null && timer.Enabled)
            {
                timer.Stop();
            }
            SaveRevenueToDatabase();
        }

        private void SaveRevenueToDatabase()
        {
            decimal totalTimeRevenue = usedBalance;
            decimal totalFoodRevenue = totalFoodFeeSum;

            string insertQuery = "INSERT INTO Revenue (TotalFoodRevenue, TotalTimeRevenue) VALUES (@TotalFoodRevenue, @TotalTimeRevenue)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TotalFoodRevenue", totalFoodRevenue),
                new SqlParameter("@TotalTimeRevenue", totalTimeRevenue)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(insertQuery, parameters);
            if (rowsAffected > 0)
            {

            }
            else
            {
                MessageBox.Show("Lưu doanh thu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateTimeDisplay()
        {
            decimal totalHours = balance / costPerHour;
            int totalMinutes = (int)(totalHours * 60);
            int hours = totalMinutes / 60;
            int minutes = totalMinutes % 60;
            TimeleftLb.Text = $"{hours}h {minutes}m";
        }

        private void StartTimer()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            string query = "SELECT Balance FROM Customer WHERE UserID = @ID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", ID)
            };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0)
            {
                timer.Stop();
                MessageBox.Show("Không tìm thấy người dùng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            balance = Convert.ToDecimal(dt.Rows[0]["Balance"]);
            decimal costPerSecond = costPerHour / 3600m;

            if (balance >= costPerSecond)
            {
                balance -= costPerSecond;
                usedBalance += costPerSecond;
                BalanceLb.Text = balance.ToString("N0");
                UpdateTimeDisplay();
                UpdateUsageDisplay();

                decimal totalHours = balance / costPerHour;
                int totalMinutes = (int)(totalHours * 60);
                if (totalMinutes < 5)
                {
                    if (ptbNotify != null)
                        ptbNotify.Visible = true;
                }
                else
                {
                    if (ptbNotify != null)
                        ptbNotify.Visible = false;
                }

                string updateQuery = "UPDATE Customer SET Balance = @Balance WHERE UserID = @ID";
                SqlParameter[] updateParams = new SqlParameter[]
                {
                    new SqlParameter("@Balance", balance),
                    new SqlParameter("@ID", ID)
                };
                DatabaseHelper.ExecuteNonQuery(updateQuery, updateParams);
            }
            else
            {
                timer.Stop();
                MessageBox.Show("Your time has run out!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UpdateUsageDisplay()
        {
            decimal costPerSecond = costPerHour / 3600m;
            int usedSeconds = (int)Math.Round(usedBalance / costPerSecond);
            int usedMinutes = usedSeconds / 60;
            int usedHours = usedMinutes / 60;
            int remainingMinutes = usedMinutes % 60;

            TotalFeeLbl.Text = $"{usedBalance:N0}đ";
            TotalTimeLbl.Text = $"{usedHours}h {remainingMinutes}m";
        }

        //private void depositBtn_Click(object sender, EventArgs e)
        //{
        //    if (guna2ComboBox2.SelectedIndex == -1)
        //    {
        //        MessageBox.Show("Vui lòng chọn phương thức thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    string query = "SELECT Balance FROM Customer WHERE UserID = @ID";
        //    SqlParameter[] parameters = new SqlParameter[]
        //    {
        //        new SqlParameter("@ID", ID)
        //    };
        //    DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

        //    if (dt.Rows.Count == 0)
        //    {
        //        MessageBox.Show("Không tìm thấy người dùng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    balance = Convert.ToDecimal(dt.Rows[0]["Balance"]);

        //    if (decimal.TryParse(depositTxt.Text, out decimal depositAmount) && depositAmount > 0)
        //    {
        //        balance += depositAmount;

        //        string updateQuery = "UPDATE Customer SET Balance = @Balance WHERE UserID = @ID";
        //        SqlParameter[] updateParams = new SqlParameter[]
        //        {
        //            new SqlParameter("@Balance", balance),
        //            new SqlParameter("@ID", ID)
        //        };
        //        DatabaseHelper.ExecuteNonQuery(updateQuery, updateParams);

        //        BalanceLb.Text = balance.ToString("N0");
        //        UpdateTimeDisplay();
        //        UpdateUsageDisplay();

        //        if (!timer.Enabled)
        //        {
        //            timer.Start();
        //        }

        //        MessageBox.Show($"Nạp tiền thành công: {depositAmount:N0}đ", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    else
        //    {
        //        MessageBox.Show("Vui lòng nhập số tiền hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        //    depositTxt.Clear();
        //}

        public void RefreshBalance()
        {
            string query = "SELECT Balance FROM Customer WHERE UserID = @ID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", ID)
            };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                balance = Convert.ToDecimal(dt.Rows[0]["Balance"]);
                BalanceLb.Text = balance.ToString("N0");
                UpdateTimeDisplay();
                UpdateUsageDisplay();
            }
            else
            {
                BalanceLb.Text = "0";
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string feedbackContent = txtFeedback.Text.Trim();

            if (string.IsNullOrEmpty(feedbackContent))
            {
                MessageBox.Show("Please enter feedback!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string insertQuery = "INSERT INTO Feedback (UserID, Content, CreatedAt, Status) VALUES (@UserID, @Content, GETDATE(), 0)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", this.ID),
                new SqlParameter("@Content", feedbackContent)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(insertQuery, parameters);
            if (rowsAffected > 0)
            {
                txtFeedback.Text = "";
                MessageBox.Show("Feedback sent!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to send feedback!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadOrdersToDataGridView();
        }

        private void ApplyCustomTheme()
        {
            // Set DataGridView  Theme
           dgvTransaction.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default;
           dgvTransaction.EnableHeadersVisualStyles = false;
            // Header
           dgvTransaction.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(10, 50, 50); // Xám đậm với tông cyan
           dgvTransaction.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(19, 250, 168); // Cyan chủ đạo
           dgvTransaction.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
           dgvTransaction.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
           dgvTransaction.ColumnHeadersHeight = 40;

            // Dòng thường
           dgvTransaction.DefaultCellStyle.BackColor = Color.FromArgb(20, 20, 20); // Xám đậm với tông xanh lam
           dgvTransaction.DefaultCellStyle.ForeColor = Color.White; // Trắng với chút sắc cyan
           dgvTransaction.DefaultCellStyle.Font = new Font("Segoe UI", 10);
           dgvTransaction.DefaultCellStyle.SelectionBackColor = Color.FromArgb(10, 150, 100); // Cyan đậm khi chọn
           dgvTransaction.DefaultCellStyle.SelectionForeColor = Color.White; // Chữ trắng khi chọn
           dgvTransaction.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Dòng xen kẽ
           dgvTransaction.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(20, 20, 20); // Xám đậm hơn một chút

            // DGV
           dgvTransaction.BackgroundColor = Color.FromArgb(20, 20, 20); // Xám rất đậm, gần đen
           dgvTransaction.BorderStyle = BorderStyle.None;
           dgvTransaction.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
           dgvTransaction.RowTemplate.Height = 35;

           dgvTransaction.ReadOnly = true;
           dgvTransaction.AllowUserToAddRows = false;
           dgvTransaction.AllowUserToResizeRows = false;
           dgvTransaction.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
           dgvTransaction.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}