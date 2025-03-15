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
    public partial class UC_WorkLog : UserControl
    {
        public UC_WorkLog()
        {
            InitializeComponent();
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            WorkLogViewDetailsForm workLogViewDetailsForm = new WorkLogViewDetailsForm();
            workLogViewDetailsForm.ShowDialog();
        }
    }
}
