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
        public UC_MyAccount(string ID)
        {
            InitializeComponent();
            this.ID = ID;
            LoadData();
        }
        // Thuộc tính công khai để cập nhật lblTotalFoodFee
        public decimal TotalFoodFee
        {
            get { return totalFoodFee; }
            set
            {
                totalFoodFee = value;
                totalFoodFeeSum +=  totalFoodFee;
                lblTotalFoodFee.Text = totalFoodFeeSum.ToString("N0") + "đ"; // Cập nhật giao diện
            }
        }

        private void LoadData()
        {
            //string userQuery = @"SELECT FullName, Balance, ComputerID FROM Customer JOIN Computer ON Customer.UserID = Computer.UserID WHERE Customer.UserID = @ID";
            //SqlParameter[] parameters = new SqlParameter[]
            //{
            //    new SqlParameter("@ID", ID)
            //};
            //DataTable dt = DatabaseHelper.ExecuteQuery(userQuery, parameters);
            //memberLb.Text = dt.Rows[0]["FullName"].ToString();
            //BalanceLb.Text = dt.Rows[0]["Balance"].ToString();
            //CusNameLB.Text = dt.Rows[0]["FullName"].ToString();
            //ComLB.Text = dt.Rows[0]["ComputerID"].ToString();
            //balance = Convert.ToDecimal(dt.Rows[0]["Balance"]);
            //initialBalance = balance;
            //usedBalance = 0;
            //BalanceLb.Text = balance.ToString();
            //UpdateTimeDisplay();
            //UpdateUsageDisplay();
            //StartTimer();
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

        }
        private void UpdateTimeDisplay()
        {
            decimal totalHours = balance / costPerHour;
            int totalMinutes = (int)(totalHours * 60);
            int hours = totalMinutes / 60;
            int minutes = totalMinutes % 60;
            label6.Text = $"{hours}h {minutes}m";
        }

        private void StartTimer()
        {
            timer = new Timer();
            //timer.Interval = 60000;
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //decimal costPerMinute = costPerHour / 60m;

            //if (balance >= costPerMinute)
            //{
            //    balance -= costPerMinute;
            //    usedBalance += costPerMinute;
            //    BalanceLb.Text = balance.ToString("N0");
            //    UpdateTimeDisplay();
            //    UpdateUsageDisplay();


            //    // Cập nhật vào database
            //    string updateQuery = "UPDATE Customer SET Balance = @Balance WHERE UserID = @ID";
            //    SqlParameter[] updateParams = new SqlParameter[]
            //    {
            //        new SqlParameter("@Balance", balance),
            //        new SqlParameter("@ID", ID)
            //    };
            //    DatabaseHelper.ExecuteNonQuery(updateQuery, updateParams);
            //}
            //else
            //{
            //    timer.Stop();
            //    MessageBox.Show("Your time has run out!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}

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
            decimal costPerSecond = costPerHour / 3600m; // Chi phí mỗi giây (1 giờ = 3600 giây)

            if (balance >= costPerSecond)
            {
                balance -= costPerSecond;
                usedBalance += costPerSecond;
                BalanceLb.Text = balance.ToString("N0");
                UpdateTimeDisplay();
                UpdateUsageDisplay();

                // Cập nhật vào database
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
        //private void UpdateUsageDisplay()
        //{
        //    //usedBalance = initialBalance - balance;
        //    decimal costPerMinute = costPerHour / 60m;
        //    int usedMinutes = (int)Math.Round(usedBalance / costPerMinute);
        //    int usedHours = usedMinutes / 60;
        //    int remainingMinutes = usedMinutes % 60;

        //    TotalFeeLbl.Text = $"{usedBalance:N0}đ";
        //    TotalTimeLbl.Text = $"{usedHours}h {remainingMinutes}m";
        //}

        private void depositBtn_Click(object sender, EventArgs e)
        {

            if (guna2ComboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn phương thức thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy số dư mới nhất từ CSDL
            string query = "SELECT Balance FROM Customer WHERE UserID = @ID";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@ID", ID)
            };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy người dùng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            balance = Convert.ToDecimal(dt.Rows[0]["Balance"]);

            if (decimal.TryParse(depositTxt.Text, out decimal depositAmount) && depositAmount > 0)
            {
                balance += depositAmount;

                string updateQuery = "UPDATE Customer SET Balance = @Balance WHERE UserID = @ID";
                SqlParameter[] updateParams = new SqlParameter[]
                {
            new SqlParameter("@Balance", balance),
            new SqlParameter("@ID", ID)
                };
                DatabaseHelper.ExecuteNonQuery(updateQuery, updateParams);

                BalanceLb.Text = balance.ToString("N0");
                UpdateTimeDisplay();
                UpdateUsageDisplay();

                if (!timer.Enabled)
                {
                    timer.Start();
                }

                MessageBox.Show($"Nạp tiền thành công: {depositAmount:N0}đ", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập số tiền hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            depositTxt.Clear();
        }
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
    }
}
