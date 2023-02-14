using ADO.NET.DataLayer.Models;

namespace ADO.NET.DataLayer.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task Create(Order order);

        void Update(Order order);

        Task Delete(int orderId);

        Task<Order> GetById(int orderId);

        Task<List<Order>> GetAll();

        Task<List<Order>> GetOrders(Filter filter);

        Task DeleteOrdersInBulk(Filter filter);
    }
}
