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
        public UC_ComputerStatus()
        {
            InitializeComponent();
        }
        public UC_ComputerStatus(string imagePath, string id)
        {
            InitializeComponent();
            lblID.Text = id;
         
            lblID.TextAlign = ContentAlignment.MiddleCenter;
            string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            string fullPath = Path.Combine(projectPath, "ComputerStatusPic", imagePath);

            if (File.Exists(fullPath))
                ptbComputer.Image = Image.FromFile(fullPath);
            else
                MessageBox.Show($"Không tìm thấy ảnh: {fullPath}");
        }
        private void btnAddBalance_Click(object sender, EventArgs e)
        {
            AddBalanceForm addBalanceForm = new AddBalanceForm();
            addBalanceForm.ShowDialog();
        }
    }
}
