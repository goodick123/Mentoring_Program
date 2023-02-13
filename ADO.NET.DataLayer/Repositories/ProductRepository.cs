using System.Data;
using ADO.NET.DataLayer.Models;
using ADO.NET.DataLayer.Repositories.Interfaces;

namespace ADO.NET.DataLayer.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IDbConnection connection) : base(connection)
        {
        }

        public void Create(Product order)
        {
            var query = File.ReadAllText(
                "C:\\Users\\Oleksii_Shtanko1\\Pictures\\orm\\ADO.NET\\ADO.NET.DataLayer\\Queries\\Order\\CreateOrder.sql");

            var paramsDictionary = GetParamsDictionary(order);

            base.ExecuteConnectedQueryWithParams(query, paramsDictionary);
        }

        public void Update(Product order)
        {
            var query = File.ReadAllText(
                "C:\\Users\\Oleksii_Shtanko1\\Pictures\\orm\\ADO.NET\\ADO.NET.DataLayer\\Queries\\Order\\UpdateOrder.sql");

            var paramsDictionary = GetParamsDictionary(order);

            base.ExecuteDisconnectedQuery(query, paramsDictionary);
        }

        public void Delete(int orderId)
        {
            var query = File.ReadAllText(
                "C:\\Users\\Oleksii_Shtanko1\\Pictures\\orm\\ADO.NET\\ADO.NET.DataLayer\\Queries\\Order\\\DeleteOrder.sql");

            base.ExecuteConnectedQueryWithParams(query, new Dictionary<string, object> { { "Id", orderId } });
        }

        public Product GetById(int orderId)
        {
            var query = File.ReadAllText(
                "C:\\Users\\Oleksii_Shtanko1\\Pictures\\orm\\ADO.NET\\ADO.NET.DataLayer\\Queries\\Order\\GetOrderById.sql");

            var result = base.ConvertConnectedQueryResultToModel(query, new Dictionary<string, object> { { "Id", orderId } });

            return result.First();
        }

        public List<Product> GetAll()
        {
            var query = File.ReadAllText(
                "C:\\Users\\Oleksii_Shtanko1\\Pictures\\orm\\ADO.NET\\ADO.NET.DataLayer\\Queries\\Order\\GetAllOrders.sql");

            var result = base.ConvertConnectedQueryResultToModel(query, new Dictionary<string, object>());

            return result;
        }

        private Dictionary<string, object> GetParamsDictionary(object item)
        {
            var paramsDictionary = new Dictionary<string, object>();

            foreach (var property in item.GetType().GetProperties())
            {
                paramsDictionary.Add(property.Name, property.GetValue(item));
            }

            return paramsDictionary;
        }
    }
}
