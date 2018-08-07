using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KerbalStore.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderCreated { get; set; }
        [Required]
        [MinLength(4)]
        public string OrderReference { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
