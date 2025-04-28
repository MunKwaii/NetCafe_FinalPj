using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using NetCafeManager.UserControls;

namespace NetCafeManager
{
    public partial class CustomerForm : Form
    {
        string ID;
        private UC_Service ucService;
        private UC_MyAccount ucMyAccount;
        public CustomerForm(string ID)
        {
            InitializeComponent();
            pnlProfileContent.Visible = false;
            this.ID = ID;
            ucService = new UC_Service(ID);
            ucMyAccount = new UC_MyAccount(ID);
            ucService.Visible = false;
            ucMyAccount.Visible = true;
            pnlMainContent.Controls.Add(ucService);
            pnlMainContent.Controls.Add(ucMyAccount);
            ChangeActivateButton(btnMyAccount);
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
        //private void ShowUserControl(UserControl uc)
        //{
        //    pnlMainContent.Controls.Clear();
        //    pnlMainContent.Controls.Add(uc);
        //}
        // Thêm phương thức để truyền TotalFoodFee
        public void UpdateTotalFoodFee(decimal foodFee)
        {
            ucMyAccount.TotalFoodFee = foodFee; // Cập nhật TotalFoodFee trong UC_MyAccount
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
            ucMyAccount.RefreshBalance(); // Làm mới số dư khi chuyển sang tab My Account
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
                this.Close();
            }
        }
    }
}
