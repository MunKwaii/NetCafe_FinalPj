using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetCafeManager.UserControls
{
    public partial class UC_UserProfile : UserControl
    {
        string ID;
        public UC_UserProfile(string ID)
        {

            InitializeComponent();
            this.ID = ID;
            LoadData();
        }

        private void LoadData()
        {
            string query = @"SELECT FullName, Balance FROM Customer WHERE UserID = @ID";
            SqlParameter[] Para = new SqlParameter[]
            {
                new SqlParameter("@ID", ID)
            };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, Para);
            Usernamelb.Text = dt.Rows[0]["FullName"].ToString();
            BalanceLb.Text = dt.Rows[0]["Balance"].ToString();
        }
    }
}
