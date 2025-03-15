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
        public UC_MenuItem(string imagePath, string itemName, string price)
        {

            InitializeComponent();
            lblProductName.Text = itemName;
            lblPrice.Text = price;
            lblProductName.TextAlign = ContentAlignment.MiddleCenter;
            lblPrice.TextAlign = ContentAlignment.MiddleCenter;
            string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            string fullPath = Path.Combine(projectPath, "MenuItemPic", imagePath);

            if (File.Exists(fullPath))
                ptbProductImage.Image = Image.FromFile(fullPath);
            else
                MessageBox.Show($"Không tìm thấy ảnh: {fullPath}");
        }
    }
}
