using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetCafeManager.UserControls;
using Microsoft.Data.SqlClient;

namespace NetCafeManager
{
    public partial class CustomerForm : Form
    {
        string ID;
        private UC_Service ucService;
        private UC_MyAccount ucMyAccount;
        private string computerID; // Lưu ComputerID của máy khách đang sử dụng

        public CustomerForm(string ID, string computerID = null)
        {
            InitializeComponent();
            pnlProfileContent.Visible = false;
            this.ID = ID;
            this.computerID = computerID; // Lưu ComputerID (nếu có)

            // Kiểm tra computerID trước khi bắt đầu phiên làm việc
            if (string.IsNullOrEmpty(computerID))
            {
                MessageBox.Show("Không xác định được máy tính để đăng nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                StartSession();
            }

            ucService = new UC_Service(ID, true);
            ucMyAccount = new UC_MyAccount(ID);
            ucService.Visible = false;
            ucMyAccount.Visible = true;
            pnlMainContent.Controls.Add(ucService);
            pnlMainContent.Controls.Add(ucMyAccount);
            ChangeActivateButton(btnMyAccount);
        }

        private void StartSession()
        {
            try
            {
                string query = @"
                    UPDATE Computer
                    SET UserID = @UserID, StartTime = @StartTime, EndTime = NULL, Status = 'Active'
                    WHERE ComputerID = @ComputerID";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", ID),
                    new SqlParameter("@StartTime", DateTime.Now),
                    new SqlParameter("@ComputerID", computerID)
                };
                DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi bắt đầu phiên làm việc: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EndSession()
        {
            try
            {
                string query = @"
                    UPDATE Computer
                    SET UserID = NULL, EndTime = @EndTime, Status = 'Idle'
                    WHERE ComputerID = @ComputerID";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@EndTime", DateTime.Now),
                    new SqlParameter("@ComputerID", computerID)
                };
                DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi kết thúc phiên làm việc: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChangeActivateButton(Guna.UI2.WinForms.Guna2Button activeButton)
        {
            Guna.UI2.WinForms.Guna2Button[] buttons = { btnService, btnMyAccount };
            foreach (var btn in buttons)
            {
                btn.FillColor = Color.FromArgb(20, 20, 20);
                btn.ForeColor = Color.FromArgb(19, 250, 168);
            }
            activeButton.FillColor = Color.FromArgb(19, 250, 168);
            activeButton.ForeColor = Color.Black;
        }

        private void ShowUserControl(UserControl uc)
        {
            pnlMainContent.Controls.Clear();
            pnlMainContent.Controls.Add(uc);
        }

        public void UpdateTotalFoodFee(decimal foodFee)
        {
            ucMyAccount.TotalFoodFee = foodFee;
        }

        public void RefreshMyAccountBalance()
        {
            ucMyAccount.RefreshBalance();
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            pnlProfileContent.Controls.Clear();
            ChangeActivateButton(btnService);
            ucService.Visible = true;
            ucMyAccount.Visible = false;
        }

        private void btnMyAccount_Click(object sender, EventArgs e)
        {
            pnlProfileContent.Controls.Clear();
            ChangeActivateButton(btnMyAccount);
            ucService.Visible = false;
            ucMyAccount.Visible = true;
            ucMyAccount.RefreshBalance();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            pnlProfileContent.Visible = !pnlProfileContent.Visible;
            pnlProfileContent.Controls.Add(new UC_UserProfile(ID));
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Kết thúc phiên làm việc khi khách hàng đăng xuất
                if (!string.IsNullOrEmpty(computerID))
                {
                    EndSession();
                }
                this.Close();
            }
        }

        private void CustomerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Kết thúc phiên làm việc khi form đóng (bao gồm cả khi nhấn nút X)
            if (!string.IsNullOrEmpty(computerID))
            {
                EndSession();
            }
        }
    }
}