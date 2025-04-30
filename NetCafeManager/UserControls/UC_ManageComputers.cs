using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Microsoft.Data.SqlClient;
using Timer = System.Windows.Forms.Timer;

namespace NetCafeManager.UserControls
{
    public partial class UC_ManageComputers : UserControl
    {
        private int indexPage = 1, lengthPage = 10, currentPage = 1;
        private List<Guna2Button> List_buttonPage;
        private Timer refreshTimer; // Timer để làm mới trạng thái máy
        private bool isSearching; // Biến để kiểm tra trạng thái tìm kiếm

        public UC_ManageComputers()
        {
            InitializeComponent();
            LoadComputer();
            List_buttonPage = new List<Guna2Button> { btnFirst_page, btnSecond_page, btnThird_page };
            isSearching = false; // Ban đầu không ở trạng thái tìm kiếm

            // Khởi tạo Timer để làm mới trạng thái máy mỗi 5 giây
            refreshTimer = new Timer();
            refreshTimer.Interval = 10000; // 5 giây
            refreshTimer.Tick += (s, e) => LoadComputer();
            refreshTimer.Start();
        }

        private void LoadComputer()
        {
            // Chỉ làm mới nếu không đang ở trạng thái tìm kiếm
            if (isSearching)
            {
                return;
            }

            flpnComputerList.Controls.Clear(); // Xóa danh sách cũ

            try
            {
                // Truy vấn danh sách máy tính từ bảng Computer
                string query = @"
                    SELECT ComputerID, UserID, StartTime, EndTime, Status
                    FROM Computer
                    ORDER BY ComputerID";
                DataTable dt = DatabaseHelper.ExecuteQuery(query);

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có máy tính nào trong cơ sở dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (DataRow row in dt.Rows)
                {
                    string computerID = row["ComputerID"].ToString();
                    string userID = row["UserID"] != DBNull.Value ? row["UserID"].ToString() : null;
                    string status = row["Status"].ToString();
                    DateTime? startTime = row["StartTime"] != DBNull.Value ? (DateTime?)row["StartTime"] : null;
                    DateTime? endTime = row["EndTime"] != DBNull.Value ? (DateTime?)row["EndTime"] : null;

                    // Xác định trạng thái máy tính và hình ảnh tương ứng
                    string imagePath;
                    string displayStatus;

                    if (status == "Maintain")
                    {
                        imagePath = "maintain.png"; // Máy đang bảo trì
                        displayStatus = "Maintain";
                    }
                    else if (startTime != null && endTime == null)
                    {
                        imagePath = "active.png"; // Máy đang có phiên làm việc (Active)
                        displayStatus = "Active";
                    }
                    else
                    {
                        imagePath = "idle.png"; // Máy không có phiên làm việc (Idle)
                        displayStatus = "Idle";
                    }

                    UC_ComputerStatus computerItem = new UC_ComputerStatus(imagePath, computerID, userID, displayStatus);
                    flpnComputerList.Controls.Add(computerItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách máy tính: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchID = txtSearchByID.Text.Trim();

            flpnComputerList.Controls.Clear(); // Xóa danh sách cũ

            try
            {
                if (string.IsNullOrEmpty(searchID))
                {
                    // Nếu không nhập ID, hiển thị toàn bộ danh sách máy tính
                    isSearching = false; // Thoát trạng thái tìm kiếm
                    refreshTimer.Start(); // Khởi động lại Timer
                    LoadComputer();
                    return;
                }

                // Tạm ngưng Timer khi tìm kiếm
                isSearching = true;
                refreshTimer.Stop();

                // Truy vấn máy tính theo ComputerID
                string query = @"
                    SELECT ComputerID, UserID, StartTime, EndTime, Status
                    FROM Computer
                    WHERE ComputerID = @ComputerID";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ComputerID", searchID)
                };
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy máy tính với ID này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    isSearching = false; // Thoát trạng thái tìm kiếm
                    refreshTimer.Start(); // Khởi động lại Timer
                    LoadComputer(); // Hiển thị lại toàn bộ danh sách nếu không tìm thấy
                    return;
                }

                foreach (DataRow row in dt.Rows)
                {
                    string computerID = row["ComputerID"].ToString();
                    string userID = row["UserID"] != DBNull.Value ? row["UserID"].ToString() : null;
                    string status = row["Status"].ToString();
                    DateTime? startTime = row["StartTime"] != DBNull.Value ? (DateTime?)row["StartTime"] : null;
                    DateTime? endTime = row["EndTime"] != DBNull.Value ? (DateTime?)row["EndTime"] : null;

                    string imagePath;
                    string displayStatus;

                    if (status == "Maintain")
                    {
                        imagePath = "maintain.png";
                        displayStatus = "Maintain";
                    }
                    else if (startTime != null && endTime == null)
                    {
                        imagePath = "active.png";
                        displayStatus = "Active";
                    }
                    else
                    {
                        imagePath = "idle.png";
                        displayStatus = "Idle";
                    }

                    UC_ComputerStatus computerItem = new UC_ComputerStatus(imagePath, computerID, userID, displayStatus);
                    flpnComputerList.Controls.Add(computerItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm máy tính: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isSearching = false; // Thoát trạng thái tìm kiếm
                refreshTimer.Start(); // Khởi động lại Timer
                LoadComputer(); // Hiển thị lại toàn bộ danh sách nếu có lỗi
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            ModifyComputerForm modifyComputerForm = new ModifyComputerForm();
            modifyComputerForm.ShowDialog();
            LoadComputer(); // Tải lại danh sách sau khi chỉnh sửa
        }

        private void change_Color_page(int turn)
        {
            if (turn == 1)
            {
                btnFirst_page.FillColor = Color.FromArgb(19, 250, 168);
                btnFirst_page.ForeColor = Color.FromArgb(40, 40, 40);
            }
            else if (turn == 2)
            {
                btnSecond_page.FillColor = Color.FromArgb(19, 250, 168);
                btnSecond_page.ForeColor = Color.FromArgb(40, 40, 40);
            }
            else
            {
                btnThird_page.FillColor = Color.FromArgb(19, 250, 168);
                btnThird_page.ForeColor = Color.FromArgb(40, 40, 40);
            }
            List_buttonPage[currentPage - 1].FillColor = Color.Transparent;
            List_buttonPage[currentPage - 1].ForeColor = Color.FromArgb(19, 250, 168);
        }

        private void update_page(int indexPage)
        {
            btnFirst_page.Text = (indexPage - 1).ToString();
            btnSecond_page.Text = (indexPage).ToString();
            btnThird_page.Text = (indexPage + 1).ToString();
        }

        private void UC_ManageComputers_Load(object sender, EventArgs e)
        {
            Panel panel = new Panel
            {
                Size = new Size(1, 1),
                Margin = new Padding(1, 1, 1, 1),
                BackColor = Color.FromArgb(20, 20, 20)
            };
            flpnComputerList.Controls.Add(panel);
            btnFirst_page.FillColor = Color.FromArgb(19, 250, 168);
            btnFirst_page.ForeColor = Color.FromArgb(40, 40, 40);
        }

        private void btnBack_page_Click(object sender, EventArgs e)
        {
            if (indexPage > 1)
            {
                if (indexPage == 2)
                {
                    change_Color_page(1);
                    currentPage = 1;
                    indexPage--;
                }
                else
                {
                    if (currentPage == 3)
                    {
                        change_Color_page(2);
                        currentPage = 2;
                        indexPage--;
                    }
                    else if (currentPage == 2)
                    {
                        indexPage--;
                        update_page(indexPage);
                    }
                }
                LoadComputer(); // Tải lại danh sách máy tính
            }
        }

        private void btnFirst_page_Click(object sender, EventArgs e)
        {
            if (currentPage != 1)
            {
                if (indexPage == 2)
                {
                    indexPage = int.Parse(btnFirst_page.Text);
                    change_Color_page(1);
                    currentPage = 1;
                }
                else
                {
                    if (currentPage == 3)
                    {
                        change_Color_page(2);
                        currentPage = 2;
                        indexPage = int.Parse(btnFirst_page.Text);
                        update_page(indexPage);
                    }
                    else if (currentPage == 2)
                    {
                        indexPage = int.Parse(btnFirst_page.Text);
                        update_page(indexPage);
                    }
                }
                LoadComputer(); // Tải lại danh sách máy tính
            }
        }

        private void btnSecond_page_Click_1(object sender, EventArgs e)
        {
            if (currentPage != 2)
            {
                if (currentPage == 1) indexPage++;
                else if (currentPage == 3) indexPage--;
                change_Color_page(2);
                currentPage = 2;
                LoadComputer(); // Tải lại danh sách máy tính
            }
        }

        private void btnThird_page_Click_1(object sender, EventArgs e)
        {
            if (currentPage != 3)
            {
                if (indexPage == lengthPage - 1)
                {
                    indexPage = int.Parse(btnThird_page.Text);
                    change_Color_page(3);
                    currentPage = 3;
                }
                else
                {
                    if (currentPage == 1)
                    {
                        change_Color_page(2);
                        currentPage = 2;
                        indexPage = int.Parse(btnThird_page.Text);
                        update_page(indexPage);
                    }
                    else if (currentPage == 2)
                    {
                        indexPage = int.Parse(btnThird_page.Text);
                        update_page(indexPage);
                    }
                }
                LoadComputer(); // Tải lại danh sách máy tính
            }
        }

        private void btnNext_page_Click(object sender, EventArgs e)
        {
            if (indexPage < lengthPage)
            {
                if (indexPage == lengthPage - 1)
                {
                    change_Color_page(3);
                    currentPage = 3;
                    indexPage += 1;
                }
                if (currentPage == 1)
                {
                    change_Color_page(2);
                    currentPage = 2;
                    indexPage++;
                }
                else if (currentPage == 2)
                {
                    indexPage++;
                    update_page(indexPage);
                }
                LoadComputer(); // Tải lại danh sách máy tính
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            // Dừng Timer khi UserControl bị hủy
            refreshTimer.Stop();
            base.OnHandleDestroyed(e);
        }
    }
}