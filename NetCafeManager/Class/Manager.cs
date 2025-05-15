using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCafeManager
{
    public class Manager : UserBase
    {
        [Required]
        [StringLength(100)]
        private string _name;

        [Required]
        [StringLength(100)]
        private string _gmail;

        public Manager(string username, string password, string name, string gmail)
            : base(username, password, "Manager")
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _gmail = gmail ?? throw new ArgumentNullException(nameof(gmail));
        }

        public string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(nameof(Name));
        }

        public string Gmail
        {
            get => _gmail;
            set => _gmail = value ?? throw new ArgumentNullException(nameof(Gmail));
        }
    }
}
