using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCafeManager
{
    public class Customer : UserBase
    {
        [Required]
        [StringLength(100)]
        private string _fullName;

        [StringLength(100)]
        private string? _email;

        [Column(TypeName = "decimal(12,2)")]
        private decimal _balance = 0.00m;

        public Customer(string username, string password, string fullName, string? email = null)
            : base(username, password, "Customer")
        {
            _fullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            _email = email;
        }

        public string FullName
        {
            get => _fullName;
            set => _fullName = value ?? throw new ArgumentNullException(nameof(FullName));
        }

        public string? Email
        {
            get => _email;
            set => _email = value;
        }

        public decimal Balance
        {
            get => _balance;
            private set => _balance = value;
        }

        public virtual ICollection<Feedback>? Feedbacks { get; set; }
        public virtual ICollection<Orders>? Orders { get; set; }

        public void AddBalance(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive.");
            _balance += amount;
        }

        public void DeductBalance(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive.");
            if (_balance < amount)
                throw new InvalidOperationException("Insufficient balance.");
            _balance -= amount;
        }
    }
}
