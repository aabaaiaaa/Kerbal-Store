using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KerbalStore.Data.Entities
{
    //TODO: remove after identity replaced implemented
    public class Login
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiry { get; set; }
        public DateTime LoginCreated = DateTime.Now;
        public DateTime LastLogin { get; set; }
    }
}
