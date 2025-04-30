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
            try
            {
                // check id trong bang customeer
                string customerQuery = @"SELECT FullName, ComputerID 
                                        FROM Customer 
                                        LEFT JOIN Computer ON Customer.UserID = Computer.UserID 
                                        WHERE Customer.UserID = @ID";
                SqlParameter[] customerParams = new SqlParameter[]
                {
                    new SqlParameter("@ID", ID)
                };
                DataTable customerDt = DatabaseHelper.ExecuteQuery(customerQuery, customerParams);

                if (customerDt.Rows.Count > 0)
                {
                    // id customer
                    Usernamelb.Text = customerDt.Rows[0]["FullName"].ToString();
                    UserIDLB.Text = ID;
                    ComputerIDLb.Text = customerDt.Rows[0]["ComputerID"] != DBNull.Value ? customerDt.Rows[0]["ComputerID"].ToString() : "Không có máy";
                }
                else
                {
                    // check bang employee
                    string employeeQuery = @"SELECT Name 
                                            FROM Employee 
                                            WHERE ID = @ID";
                    SqlParameter[] employeeParams = new SqlParameter[]
                    {
                        new SqlParameter("@ID", ID)
                    };
                    DataTable employeeDt = DatabaseHelper.ExecuteQuery(employeeQuery, employeeParams);

                    if (employeeDt.Rows.Count > 0)
                    {
                        // id employeee
                        Usernamelb.Text = employeeDt.Rows[0]["Name"].ToString();
                        UserIDLB.Text = ID;
                        ComputerIDLb.Text = "Nhân viên"; 
                    }
                    else
                    {
                        // chec bang manager
                        string managerQuery = @"SELECT Name 
                                               FROM Manager 
                                               WHERE ID = @ID";
                        SqlParameter[] managerParams = new SqlParameter[]
                        {
                            new SqlParameter("@ID", ID)
                        };
                        DataTable managerDt = DatabaseHelper.ExecuteQuery(managerQuery, managerParams);

                        if (managerDt.Rows.Count > 0)
                        {
                            // id manager
                            Usernamelb.Text = managerDt.Rows[0]["Name"].ToString();
                            UserIDLB.Text = ID;
                            ComputerIDLb.Text = "Quản lý"; 
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy thông tin người dùng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Usernamelb.Text = "Không xác định";
                            UserIDLB.Text = ID;
                            ComputerIDLb.Text = "Không xác định";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin người dùng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Usernamelb.Text = "Lỗi tải dữ liệu";
                UserIDLB.Text = ID;
                ComputerIDLb.Text = "Lỗi tải dữ liệu";
            }
        }
    }
}