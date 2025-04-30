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
        private string computerID; // Lưu ComputerID

        // Sự kiện để thông báo UC_ManageComputers cập nhật thông tin
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
            this.userID = userID; // Lưu UserID
            this.computerStatus = status; // Lưu trạng thái máy
            this.computerID = id; // Lưu ComputerID

            // Tải hình ảnh trạng thái
            string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            string fullPath = Path.Combine(projectPath, "ComputerStatusPic", imagePath);

            if (File.Exists(fullPath))
                ptbComputer.Image = Image.FromFile(fullPath);
            else
                MessageBox.Show($"Không tìm thấy ảnh: {fullPath}");

            // Gán sự kiện Click cho PictureBox
            ptbComputer.Click += (s, e) => OnComputerSelected?.Invoke(this, computerID);
        }

        private void btnAddBalance_Click(object sender, EventArgs e)
        {
            // Chỉ cho phép nạp tiền nếu máy ở trạng thái Active
            if (computerStatus != "Active")
            {
                MessageBox.Show("Máy tính này hiện không có khách hàng sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gọi sự kiện để cập nhật thông tin trong UC_ManageComputers
            OnComputerSelected?.Invoke(this, computerID);

            // Mở AddBalanceForm và truyền UserID
            AddBalanceForm addBalanceForm = new AddBalanceForm(userID);
            addBalanceForm.ShowDialog();
        }
    }
}