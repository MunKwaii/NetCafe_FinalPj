using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCafeManager
{
    public class EmployeeShift
    {
        [Key]
        private int _shiftID;

        [Required]
        [StringLength(50)]
        private string _employeeID;

        [Required]
        private DateTime _startTime;

        private DateTime? _endTime;

        [Column(TypeName = "decimal(18,2)")]
        private decimal _totalAmount = 0.00m;

        public EmployeeShift(string employeeID, DateTime startTime)
        {
            _employeeID = employeeID ?? throw new ArgumentNullException(nameof(employeeID));
            _startTime = startTime;
        }

        public int ShiftID
        {
            get => _shiftID;
            set => _shiftID = value;
        }

        public string EmployeeID
        {
            get => _employeeID;
            set => _employeeID = value ?? throw new ArgumentNullException(nameof(EmployeeID));
        }

        public DateTime StartTime
        {
            get => _startTime;
            set => _startTime = value;
        }

        public DateTime? EndTime
        {
            get => _endTime;
            set => _endTime = value;
        }

        public decimal TotalAmount
        {
            get => _totalAmount;
            private set => _totalAmount = value;
        }

        [ForeignKey("EmployeeID")]
        public virtual Employee? Employee { get; set; }

        public virtual ICollection<Orders>? Orders { get; set; }

        public void EndShift()
        {
            if (_endTime.HasValue)
                throw new InvalidOperationException("Shift already ended.");
            _endTime = DateTime.Now;
        }

        public void AddOrderAmount(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive.");
            _totalAmount += amount;
        }
    }
}
