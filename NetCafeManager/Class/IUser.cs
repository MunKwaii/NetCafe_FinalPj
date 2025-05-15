using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCafeManager
{
    public interface IUser
    {
        string Username { get; }
        string Role { get; }
        bool ValidateCredentials(string password);
    }
}
