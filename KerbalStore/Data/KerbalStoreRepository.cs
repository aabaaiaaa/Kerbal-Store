using KerbalStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KerbalStore.Data
{
    public class KerbalStoreRepository : IKerbalStoreRepository, ITicketRepository
    {
        private readonly KerbalStoreContext kerbalStoreContext;
        private readonly ILogger<IKerbalStoreRepository> logger;

        public KerbalStoreRepository(KerbalStoreContext kerbalStoreContext, ILogger<IKerbalStoreRepository> logger)
        {
            this.kerbalStoreContext = kerbalStoreContext;
            this.logger = logger;
        }

        public void Add<T>(T model) where T : Order
        {
            kerbalStoreContext.Add(model);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            logger.LogInformation("GetAllOrders called");
            return kerbalStoreContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.RocketPart)
                .OrderByDescending(o => o.Id)
                .ToList();
        }

        public IEnumerable<RocketPart> GetAllRocketParts()
        {
            logger.LogInformation("GetAllRocketParts called");
            return kerbalStoreContext.RocketParts.OrderBy(rp => rp.PartName).ToList();
        }

        public Order GetOrder(int orderId)
        {
            logger.LogInformation("GetOrder called");
            return kerbalStoreContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(rp => rp.RocketPart)
                .Where(o => o.Id == orderId).FirstOrDefault();
        }

        public IEnumerable<RocketPart> GetRocketPartsByName(string query)
        {
            logger.LogInformation("GetRocketPartsByName called");
            return kerbalStoreContext.RocketParts.Where(rp => rp.PartName.Contains(query)).OrderBy(rp => rp.PartName).ToList();
        }

        public IEnumerable<RocketPart> GetRocketPartsLessThanPrice(int price)
        {
            return kerbalStoreContext.RocketParts.Where(rp => rp.Price < price).OrderBy(rp => rp.Price).ToList();
        }

        public bool SaveAll()
        {
            return kerbalStoreContext.SaveChanges() > 0;
        }

        public void SubmitTicketRequest(string who, string partRequested)
        {
            kerbalStoreContext.Tickets.Add(new Ticket()
            {
                FromWho = who,
                PartRequested = partRequested
            });
            kerbalStoreContext.SaveChanges();
        }
    }
}
