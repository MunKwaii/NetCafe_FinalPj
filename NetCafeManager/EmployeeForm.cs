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

namespace NetCafeManager
{
    public partial class EmployeeForm : Form
    {
        string ID;

        public EmployeeForm(string ID)
        {
            InitializeComponent();
            pnlProfileContent.Visible = false;
            this.ID = ID;
        }
        private void ChangeActivateButton(Guna.UI2.WinForms.Guna2Button activeButton)
        {
            Guna.UI2.WinForms.Guna2Button[] buttons = { btnWorkLog, btnComputerStatus, btnCustomer, btnService };
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

        private void btnComputerStatus_Click(object sender, EventArgs e)
        {
            ChangeActivateButton(btnComputerStatus);
            ShowUserControl(new UC_ManageComputers());
        }

        private void btnWorkLog_Click(object sender, EventArgs e)
        {
            ChangeActivateButton(btnWorkLog);
            ShowUserControl(new UC_WorkLog());
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            ChangeActivateButton(btnCustomer);
            ShowUserControl(new UC_ManageCustomer());

        }

        private void btnService_Click(object sender, EventArgs e)
        {
            ChangeActivateButton(btnService);
            ShowUserControl(new UC_Service());

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

        private void uC_UserProfile1_Load(object sender, EventArgs e)
        {

        }

        private void pnlProfileContent_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
