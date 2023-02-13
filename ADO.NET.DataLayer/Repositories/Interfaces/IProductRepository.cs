using ADO.NET.DataLayer.Models;

namespace ADO.NET.DataLayer.Repositories.Interfaces
{
    public interface IProductRepository
    {
        void Create(Product order);

        void Update(Product order);

        void Delete(int productId);

        Product GetById(int productId);

        List<Product> GetAll();
    }
}
