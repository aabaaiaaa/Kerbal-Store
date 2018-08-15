using KerbalStore.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KerbalStore.Data
{
    public class KerbalStoreSeeder
    {
        private readonly KerbalStoreContext kerbalStoreContext;
        private readonly UserManager<ShopUser> userManager;

        public KerbalStoreSeeder(KerbalStoreContext kerbalStoreContext, UserManager<ShopUser> userManager)
        {
            this.kerbalStoreContext = kerbalStoreContext;
            this.userManager = userManager;
        }

        public async Task Seed()
        {
            kerbalStoreContext.Database.EnsureCreated();

            // Create development user
            var user = await userManager.FindByNameAsync("jay");
            if(user == null)
            {
                user = new ShopUser()
                {
                    UserName = "jay",
                    Email = "jay@kerbal-shop.com"
                };
                var result = await userManager.CreateAsync(user, "P@ssw0rd!");
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException("failed to create test user");
                }
            }


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

                kerbalStoreContext.SaveChanges();
            }
        }
    }
}
