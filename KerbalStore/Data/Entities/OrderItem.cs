using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KerbalStore.Data.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public RocketPart RocketPart { get; set; }
        public Order Order { get; set; }
    }
}
