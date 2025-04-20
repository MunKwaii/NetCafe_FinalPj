using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace NetCafeManager.UserControls
{

    public partial class UC_Service : UserControl
    {
        //do dai cua tab page
        private int indexPage = 1, lengthPage = 10, currentPage = 1;
        private List<Guna2Button> List_buttonPage;
        public UC_Service(bool flag = false)
        {
            InitializeComponent();
            LoadMenu();
            List_buttonPage = new List<Guna2Button> { btnFirst_page, btnSecond_page, btnThird_page };
            if (flag)
                pnlNewOrders.Hide();
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
        // nút mũi tên bên phải của xử lý trang
        private void btnPage_Next_Click(object sender, EventArgs e)
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
        // nút mũi tên bên trái của xử lý trang
        private void btnPage_Back_Click(object sender, EventArgs e)
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
        private void btnFirst_Page_Click_1(object sender, EventArgs e)
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
        private void btnSecond_page_Click(object sender, EventArgs e)
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
        private void btnThird_page_Click(object sender, EventArgs e)
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
            }
        }

        private void UC_CustomerService_Load(object sender, EventArgs e)
        {

            Panel panel = new Panel
            {
                Size = new Size(1, 1),
                Margin = new Padding(1, 1, 1, 1),
                BackColor = Color.FromArgb(20, 20, 20)
            };
            flpnMenuContent.Controls.Add(panel);
            btnFirst_page.FillColor = Color.FromArgb(19, 250, 168);
            btnFirst_page.ForeColor = Color.FromArgb(40, 40, 40);
        }
        private void ShowUserControl(UserControl uc)
        {
            pnlOrder.Controls.Clear();
            pnlOrder.Controls.Add(uc);
        }
        private void btnTakeOrder_Click(object sender, EventArgs e)
        {
            ShowUserControl(new UC_TakeOrder());
        }

        private void btnNewOrder_Click(object sender, EventArgs e)
        {
            ShowUserControl(new UC_NewOrder());
        }
        private List<Product> GetProducts(int pageIndex, int pageSize)
        {
            List<Product> products = new List<Product>();

            string query = @"
                SELECT * FROM (
                    SELECT ROW_NUMBER() OVER (ORDER BY ID) AS RowNum, *
                    FROM Service WHERE Status = 1
                ) AS Temp
                WHERE RowNum BETWEEN @StartRow AND @EndRow
            ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@StartRow", (pageIndex - 1) * pageSize + 1),
                new SqlParameter("@EndRow", pageIndex * pageSize)
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
            foreach (DataRow row in dt.Rows)
            {
                products.Add(new Product
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Name = row["Name"].ToString(),
                    Price = Convert.ToDecimal(row["Price"]),
                    Image = row["Image"] != DBNull.Value ? (byte[])row["Image"] : null,
                    Status = Convert.ToBoolean(row["Status"])
                });
            }

            return products;
        }
        private void LoadMenu()
        {
            //flpnMenuContent.Controls.Clear();
            //List<Product> products = GetProducts(indexPage, 10);

            //foreach (var item in products)
            //{
            //    Image image = null;
            //    if (item.Image != null)
            //    {
            //        using (MemoryStream ms = new MemoryStream(item.Image))
            //        {
            //            image = Image.FromStream(ms);
            //        }
            //    }

            //    UC_MenuItem menuItem = new UC_MenuItem(image, item.Name, item.Price.ToString("N0") + "000đ");
            //    flpnMenuContent.Controls.Add(menuItem);
            flpnMenuContent.Controls.Clear();
            List<Product> products = GetProducts(indexPage, 10);

            foreach (var product in products)
            {
                Image image = product.Image != null ?
                    Image.FromStream(new MemoryStream(product.Image)) : null;

                var menuItem = new UC_MenuItem(
                    image,
                    product.Name,
                    product.Price
                );

                menuItem.OrderClicked += (sender, productInfo) =>
                {
                    foreach (Control control in pnlOrder.Controls)
                    {
                        if (control is UC_TakeOrder takeOrder)
                        {
                            takeOrder.AddProductToOrder(productInfo.Name, productInfo.Price);
                            break;
                        }
                    }
                };
                flpnMenuContent.Controls.Add(menuItem);
            }
        }

        
    }
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public byte[] Image { get; set; }
        public bool Status { get; set; }
    }
}
