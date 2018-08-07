using System.Collections.Generic;
using KerbalStore.Data.Entities;

namespace KerbalStore.Data
{
    public interface IKerbalStoreRepository
    {
        IEnumerable<RocketPart> GetAllRocketParts();
        IEnumerable<RocketPart> GetRocketPartsByName(string query);
        IEnumerable<RocketPart> GetRocketPartsLessThanPrice(int price);

        IEnumerable<Order> GetAllOrders();
        Order GetOrder(int orderId);
        void Add<T>(T model) where T : Order;
        bool SaveAll();
    }
}