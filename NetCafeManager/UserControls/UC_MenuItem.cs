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
        public UC_MenuItem()
        {
            InitializeComponent();
        }
        public UC_MenuItem(Image image, string itemName, string price)
        {

            InitializeComponent();
            lblProductName.Text = itemName;
            lblPrice.Text = price;
            lblProductName.TextAlign = ContentAlignment.MiddleCenter;
            lblPrice.TextAlign = ContentAlignment.MiddleCenter;

            ptbProductImage.Image = image;
        }
    }
}
