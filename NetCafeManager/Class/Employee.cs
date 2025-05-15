using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCafeManager
{
    public class Employee : UserBase
    {
        [Required]
        [StringLength(100)]
        private string _name;

        [Required]
        [StringLength(100)]
        private string _gmail;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        private decimal _salary;

        [StringLength(20)]
        private string? _phoneNumber;

        private DateTime? _birthday;
        private DateTime? _startDate;

        public Employee(string username, string password, string name, string gmail, decimal salary)
            : base(username, password, "Employee")
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _gmail = gmail ?? throw new ArgumentNullException(nameof(gmail));
            _salary = salary;
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

        public decimal Salary
        {
            get => _salary;
            set => _salary = value;
        }

        public string? PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }

        public DateTime? Birthday
        {
            get => _birthday;
            set => _birthday = value;
        }

        public DateTime? StartDate
        {
            get => _startDate;
            set => _startDate = value;
        }

        public virtual ICollection<EmployeeShift>? EmployeeShifts { get; set; }
    }
}
