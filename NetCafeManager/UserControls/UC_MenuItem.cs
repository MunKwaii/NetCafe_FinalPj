using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetCafeManager.UserControls
{
    public partial class UC_MenuItem : UserControl
    {
        public event EventHandler<(string Name, decimal Price)> OrderClicked; 
        public string ProductName { get; set; }
        public decimal ProductPrice
        { get; set; }
        public UC_MenuItem()
        {
            InitializeComponent();
        }
        public UC_MenuItem(Image image, string itemName, decimal price)
        {

            InitializeComponent();
            lblProductName.Text = itemName;
            lblPrice.Text = price.ToString("N0") + "000đ";
            ptbProductImage.Image = image;

            ProductName = itemName;
            ProductPrice = price;
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            OrderClicked?.Invoke(this, (ProductName, ProductPrice));
        }
    }
}
