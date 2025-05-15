using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCafeManager
{
    public class Revenue
    {
        [Column(TypeName = "decimal(10,2)")]
        private decimal? _totalFoodRevenue;

        [Column(TypeName = "decimal(10,2)")]
        private decimal? _totalTimeRevenue;

        public decimal? TotalFoodRevenue
        {
            get => _totalFoodRevenue;
            set => _totalFoodRevenue = value;
        }

        public decimal? TotalTimeRevenue
        {
            get => _totalTimeRevenue;
            set => _totalTimeRevenue = value;
        }

        public void AddFoodRevenue(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Revenue cannot be negative.");
            _totalFoodRevenue = (_totalFoodRevenue ?? 0) + amount;
        }

        public void AddTimeRevenue(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Revenue cannot be negative.");
            _totalTimeRevenue = (_totalTimeRevenue ?? 0) + amount;
        }
    }
}
