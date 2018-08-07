using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KerbalStore.Models
{
    public class RequestPartModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Use less than 20 characters")]
        public string RocketPart { get; set; }
    }
}
