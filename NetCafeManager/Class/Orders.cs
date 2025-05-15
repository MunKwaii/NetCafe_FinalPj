using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCafeManager
{
    public class Orders
    {
        [Key]
        private int _orderID;

        [StringLength(50)]
        private string? _customerID;

        [Required]
        [StringLength(100)]
        private string _serviceName;

        [Required]
        private int _quantity;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        private decimal _total;

        [Required]
        private DateTime _orderDate = DateTime.Now;

        [Required]
        [StringLength(50)]
        private string _status = "Pending";

        private int? _shiftID;

        public Orders(string serviceName, int quantity, decimal total)
        {
            _serviceName = serviceName ?? throw new ArgumentNullException(nameof(serviceName));
            _quantity = quantity > 0 ? quantity : throw new ArgumentException("Quantity must be positive.");
            _total = total >= 0 ? total : throw new ArgumentException("Total must be non-negative.");
        }

        public int OrderID
        {
            get => _orderID;
            set => _orderID = value;
        }

        public string? CustomerID
        {
            get => _customerID;
            set => _customerID = value;
        }

        public string ServiceName
        {
            get => _serviceName;
            set => _serviceName = value ?? throw new ArgumentNullException(nameof(ServiceName));
        }

        public int Quantity
        {
            get => _quantity;
            set => _quantity = value > 0 ? value : throw new ArgumentException("Quantity must be positive.");
        }

        public decimal Total
        {
            get => _total;
            set => _total = value >= 0 ? value : throw new ArgumentException("Total must be non-negative.");
        }

        public DateTime OrderDate
        {
            get => _orderDate;
            set => _orderDate = value;
        }

        public string Status
        {
            get => _status;
            set => _status = value ?? throw new ArgumentNullException(nameof(Status));
        }

        public int? ShiftID
        {
            get => _shiftID;
            set => _shiftID = value;
        }

        [ForeignKey("CustomerID")]
        public virtual Customer? Customer { get; set; }

        [ForeignKey("ShiftID")]
        public virtual EmployeeShift? EmployeeShift { get; set; }

        public void UpdateStatus(string newStatus)
        {
            _status = newStatus ?? throw new ArgumentNullException(nameof(newStatus));
        }
    }
}

