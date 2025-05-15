using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCafeManager
{
    public class Users : UserBase
    {
        public Users(string username, string password, string role)
            : base(username, password, role)
        {
        }

        // Navigation properties
        public virtual Computer? Computer { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual Manager? Manager { get; set; }
    }
}
