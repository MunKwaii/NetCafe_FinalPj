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

namespace NetCafeManager.UserControls
{
    public partial class UC_ManageComputers : UserControl
    {
        private int indexPage = 1, lengthPage = 10, currentPage = 1;
        private List<Guna2Button> List_buttonPage;
        public UC_ManageComputers()
        {
            InitializeComponent();
            LoadComputer();
            List_buttonPage = new List<Guna2Button> { btnFirst_page, btnSecond_page, btnThird_page };

        }
        private void LoadComputer()
        {
            flpnComputerList.Controls.Clear(); // Xóa danh sách cũ

            List<(string image, string id)> computerItems = new List<(string, string)>
    {
        ("idle.png", "1"),
        ("active.png", "2"),
        ("maintain.png", "3"),
    };

            foreach (var item in computerItems)
            {
                UC_ComputerStatus computerItem = new UC_ComputerStatus(item.image, item.id);
                flpnComputerList.Controls.Add(computerItem);
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            ModifyComputerForm modifyComputerForm = new ModifyComputerForm();
            modifyComputerForm.ShowDialog();
        }

        //do dai cua tab page


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
        // nút mũi tên bên trái của xử lý trang
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
                //chạy database

            }
        }
        // nút được biểu thị là số 1 trong xử lý trang
        private void btnFirst_page_Click(object sender, EventArgs e)
        {
            if (currentPage != 1)// tránh nhấn lại nút 
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
                //chạy database

            }
        }
        // nút được biểu thị là số 2 trong xử lý trang
        private void btnSecond_page_Click_1(object sender, EventArgs e)
        {
            if (currentPage != 2)// tránh nhấn lại nút 
            {
                if (currentPage == 1) indexPage++;
                else if (currentPage == 3) indexPage--;
                change_Color_page(2);
                currentPage = 2;
                //chạy database

            }
        }
        // nút được biểu thị là số 3 trong xử lý trang
        private void btnThird_page_Click_1(object sender, EventArgs e)
        {
            if (currentPage != 3)// tránh nhấn lại nút 
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
                //chạy database

            }
        }
        // nút mũi tên bên phải của xử lý trang
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
            }
        }
    }
}
