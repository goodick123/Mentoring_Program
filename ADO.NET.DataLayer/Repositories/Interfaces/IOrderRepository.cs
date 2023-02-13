using ADO.NET.DataLayer.Models;

namespace ADO.NET.DataLayer.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        void Create(Order order);

        void Update(Order order);

        void Delete(int orderId);

        Order GetById(int orderId);

        List<Order> GetAll();

        List<Order> GetOrders(Filter filter);

        void DeleteOrdersInBulk(Filter filter);
    }
}
