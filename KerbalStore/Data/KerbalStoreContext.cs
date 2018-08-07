using KerbalStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KerbalStore.Data
{
    public class KerbalStoreContext : DbContext
    {
        public KerbalStoreContext(DbContextOptions<KerbalStoreContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<RocketPart> RocketParts { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<Login> Logins { get; set; }

        public DbSet<Ticket> Tickets { get; set; }
    }
}
