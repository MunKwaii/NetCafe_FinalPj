using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCafeManager
{
    public abstract class BaseEntity
    {
        [Key]
        public virtual string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
