using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCafeManager
{
    public class Service
    {
        [Key]
        private int _id;

        [Required]
        [StringLength(100)]
        private string _name;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        private decimal _price;

        private byte[]? _image;

        [Required]
        private bool _status = true;

        public Service(string name, decimal price)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _price = price >= 0 ? price : throw new ArgumentException("Price must be non-negative.");
        }

        public int ID
        {
            get => _id;
            set => _id = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(nameof(Name));
        }

        public decimal Price
        {
            get => _price;
            set => _price = value >= 0 ? value : throw new ArgumentException("Price must be non-negative.");
        }

        public byte[]? Image
        {
            get => _image;
            set => _image = value;
        }

        public bool Status
        {
            get => _status;
            set => _status = value;
        }

        public void ToggleStatus()
        {
            _status = !_status;
        }
    }
}
