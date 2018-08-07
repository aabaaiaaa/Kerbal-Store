using System.ComponentModel.DataAnnotations;

namespace KerbalStore.Models
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }

        // Automapper use by convention RocketPart > Id, > PartName
        public int RocketPartId { get; set; }
        public string RocketPartPartName { get; set; }
    }
}