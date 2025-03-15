using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.SqlClient; // lỗi thời ?
using Microsoft.Data.SqlClient;

namespace NetCafeManager
{
    internal class Connection
    {
        public static string ConnectionString = @"Data Source=Khanh;Initial Catalog=UserManagement;Integrated Security=True;Trust Server Certificate=True";
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
