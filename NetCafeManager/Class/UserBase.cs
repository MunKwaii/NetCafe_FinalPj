using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCafeManager
{
    public abstract class UserBase : BaseEntity, IUser
    {
        [Required]
        [StringLength(50)]
        private string _username;

        [Required]
        [StringLength(255)]
        private string _password;

        [Required]
        [StringLength(20)]
        private string _role;

        protected UserBase(string username, string password, string role)
        {
            if (!new[] { "Customer", "Employee", "Manager" }.Contains(role))
                throw new ArgumentException("Invalid role. Must be Customer, Employee, or Manager.");

            _username = username;
            _password = password; 
            _role = role;
        }

        public string Username
        {
            get => _username;
            set => _username = value ?? throw new ArgumentNullException(nameof(Username));
        }

        public string Role => _role;

        public void SetPassword(string password)
        {
            _password = password ?? throw new ArgumentNullException(nameof(password));
        }

        public bool ValidateCredentials(string password)
        {
            return _password == password; 
        }
    }
}
