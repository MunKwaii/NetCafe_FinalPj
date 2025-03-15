using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetCafeManager
{
    public partial class AddFoodForm : Form
    {
        public AddFoodForm()
        {
            InitializeComponent();
        }

        private void btnPower_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
