using ADO.NET.DataLayer.Models;

namespace ADO.NET.DataLayer.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task Create(Product order);

        void Update(Product order);

        Task Delete(int productId);

        Task<Product> GetById(int productId);

        Task<List<Product>> GetAll();
    }
}
