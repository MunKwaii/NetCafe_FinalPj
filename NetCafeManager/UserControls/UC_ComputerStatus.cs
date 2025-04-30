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
        private string userID; // Lưu UserID của khách hàng (nếu máy Active)
        private string computerStatus; // Trạng thái máy (Idle, Active, Maintain)

        public UC_ComputerStatus()
        {
            InitializeComponent();
        }

        public UC_ComputerStatus(string imagePath, string id, string userID, string status)
        {
            InitializeComponent();
            lblID.Text = id;
            lblID.TextAlign = ContentAlignment.MiddleCenter;
            this.userID = userID; // Lưu UserID
            this.computerStatus = status; // Lưu trạng thái máy

            // Tải hình ảnh trạng thái
            string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            string fullPath = Path.Combine(projectPath, "ComputerStatusPic", imagePath);

            if (File.Exists(fullPath))
                ptbComputer.Image = Image.FromFile(fullPath);
            else
                MessageBox.Show($"Không tìm thấy ảnh: {fullPath}");
        }

        private void btnAddBalance_Click(object sender, EventArgs e)
        {
            // Chỉ cho phép nạp tiền nếu máy ở trạng thái Active
            if (computerStatus != "Active")
            {
                MessageBox.Show("Máy tính này hiện không có khách hàng sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mở AddBalanceForm và truyền UserID
            AddBalanceForm addBalanceForm = new AddBalanceForm(userID);
            addBalanceForm.ShowDialog();
        }
    }
}