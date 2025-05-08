using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using Guna.UI2.WinForms;
using NetCafeManager.UserControls;

namespace NetCafeManager
{
    public partial class ManagerForm : Form
    {
        string ID;
        private UC_ManageCustomer ucCustomer;
        private UC_ManageEmployee ucEmployee;
        private UC_ManageComputers ucComputers;
        private UC_Setting ucSetting;
        public ManagerForm(string ID)
        {
            InitializeComponent();
            pnlProfileContent.Visible = false;
            this.ID = ID;
            ucCustomer = new UC_ManageCustomer();
            ucEmployee = new UC_ManageEmployee();
            ucSetting = new UC_Setting();
            ucComputers = new UC_ManageComputers();
            pnlMainContent.Controls.Add(ucCustomer);
            pnlMainContent.Controls.Add(ucEmployee);
            pnlMainContent.Controls.Add(ucComputers);
            pnlMainContent.Controls.Add(ucSetting);
            ucSetting.Visible = true;
            ucComputers.Visible = false;
            ucEmployee.Visible = false;
            ucCustomer.Visible = false;
            ChangeActivateButton(btnSetting);
        }


        private void ChangeActivateButton(Guna.UI2.WinForms.Guna2Button activeButton)
        {
            Guna.UI2.WinForms.Guna2Button[] buttons = { btnEmployee, btnComputerStatus, btnCustomer, btnSetting };
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
            //pnlMainContent.Controls.Clear();
            ucSetting.Visible = false;
            ucComputers.Visible = true;
            ucEmployee.Visible = false;
            ucCustomer.Visible = false;
            ChangeActivateButton(btnComputerStatus);
            //ShowUserControl(new UC_ManageComputers());
        }



        private void btnEmployee_Click(object sender, EventArgs e)
        {
            //pnlMainContent.Controls.Clear();
            ucSetting.Visible = false;
            ucComputers.Visible = false;
            ucEmployee.Visible = true;
            ucCustomer.Visible = false;
            ChangeActivateButton(btnEmployee);
            //ShowUserControl(new UC_ManageEmployee());

        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            //pnlMainContent.Controls.Clear();
            ucSetting.Visible = false;
            ucComputers.Visible = false;
            ucEmployee.Visible = false;
            ucCustomer.Visible = true;
            ChangeActivateButton(btnCustomer);
            //ShowUserControl(new UC_ManageCustomer());

        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            //pnlMainContent.Controls.Clear();
            ucSetting.Visible = true;
            ucComputers.Visible = false;
            ucEmployee.Visible = false;
            ucCustomer.Visible = false;
            ChangeActivateButton(btnSetting);
            //ShowUserControl(new UC_Setting());

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
