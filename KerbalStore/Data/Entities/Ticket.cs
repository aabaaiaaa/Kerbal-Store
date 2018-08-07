using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KerbalStore.Data.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public string FromWho { get; set; }
        public string PartRequested { get; set; }
        public DateTime DateRequested = DateTime.Now;
    }
}
