﻿using System;
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
    public partial class UC_Setting : UserControl
    {
        public UC_Setting()
        {
            InitializeComponent();
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            AddFoodForm addFoodForm = new AddFoodForm();
            addFoodForm.ShowDialog();
        }
    }
}
