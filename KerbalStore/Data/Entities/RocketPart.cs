using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KerbalStore.Data.Entities
{
    public class RocketPart
    {
        public int Id { get; set; }
        public string PartName { get; set; }
        public decimal Price { get; set; }
        public string Summary { get; set; }
    }
}
