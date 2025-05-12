using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetCafeManager.UserControls
{
    public partial class UC_ComputerStatus : UserControl
    {
        private string userID; 
        private string computerStatus; 
        private string computerID; 

        public event EventHandler<string> OnComputerSelected;

        public UC_ComputerStatus()
        {
            InitializeComponent();
        }

        public UC_ComputerStatus(string imagePath, string id, string userID, string status)
        {
            InitializeComponent();
            lblID.Text = id;
            lblID.TextAlign = ContentAlignment.MiddleCenter;
            this.userID = userID; 
            this.computerStatus = status; 
            this.computerID = id; 

            string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            string fullPath = Path.Combine(projectPath, "ComputerStatusPic", imagePath);

            if (File.Exists(fullPath))
                ptbComputer.Image = Image.FromFile(fullPath);
            else
                MessageBox.Show($"Image not found: {fullPath}");

            ptbComputer.Click += (s, e) =>
            {
                if (OnComputerSelected != null) 
                {
                    OnComputerSelected(this, computerID); 
                }
            };
        }

        private void btnAddBalance_Click(object sender, EventArgs e)
        {
            if (computerStatus != "Active")
            {
                MessageBox.Show("This computer is not currently in use by any customer!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (OnComputerSelected != null) 
            {
                OnComputerSelected(this, computerID); 
            }

            AddBalanceForm addBalanceForm = new AddBalanceForm(userID);
            addBalanceForm.ShowDialog();
        }
    }
}