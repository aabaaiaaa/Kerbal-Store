using KerbalStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KerbalStore.Data
{
    public class KerbalStoreSeeder
    {
        private readonly KerbalStoreContext kerbalStoreContext;

        public KerbalStoreSeeder(KerbalStoreContext kerbalStoreContext)
        {
            this.kerbalStoreContext = kerbalStoreContext;
        }

        public void Seed()
        {
            kerbalStoreContext.Database.EnsureCreated();

            if (!kerbalStoreContext.RocketParts.Any())
            {
                // Seed data
                var rocketParts = new RocketPart[]{
                    new RocketPart()
                    {
                        PartName = "Rocket Engine",
                        Price = 500000
                    },new RocketPart()
                    {
                        PartName = "Command capsule",
                        Price = 200000
                    }
                    };

                // Seed with initial rocket parts
                kerbalStoreContext.RocketParts.AddRange(rocketParts);

                // Seed data
                var order = new Order()
                {
                    OrderReference = "ABC123",
                    //OrderCreated = DateTime.Now,
                    OrderItems = new[] { new OrderItem() { RocketPart = rocketParts.First(), UnitPrice = rocketParts.First().Price, Quantity = 1 } }
                };

                // Seed order item
                kerbalStoreContext.Orders.Add(order);

                // Seed login
                var login = new Login()
                {
                    Username = "jay",
                    Password = "test"
                };
                kerbalStoreContext.Logins.Add(login);

                kerbalStoreContext.SaveChanges();
            }
        }
    }
}
