using ADO.NET.DataLayer.Models;
using ADO.NET.DataLayer.Repositories.Interfaces;

namespace ADO.NET.DataLayer.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public const string CREATEPRODUCT = "INSERT INTO Products (Id, Name, Description, Weight, Length, Width, Height) VALUES (@Id, @Name, @Description, @Weight, @Length, @Width, @Height);";
        public const string DELETEPRODUCT = "DELETE FROM Products WHERE Id = @id;";
        public const string GETALLPRODUCT = "SELECT * FROM Products;";
        public const string GETPRODUCTBYID = "SELECT * FROM Products WHERE Id = @Id;";
        public const string UPDATEPRODUCT = "UPDATE Products SET Name=@Name, Description=@Description, Weight=@Weight, Length=@Length, Width=@Width, Height=@Height WHERE Id=@Id;";
        public ProductRepository(DbContext connection) : base(connection)
        {
        }

        public async Task Create(Product order)
        {
            var paramsDictionary = GetParamsDictionary(order);

            await ExecuteConnectedQueryWithParams(CREATEPRODUCT, paramsDictionary);
        }

        public void Update(Product order)
        {
            var paramsDictionary = GetParamsDictionary(order);

            ExecuteDisconnectedQuery(GETPRODUCTBYID, paramsDictionary);
        }

        public async Task Delete(int orderId)
        {
            await ExecuteConnectedQueryWithParams(DELETEPRODUCT, new Dictionary<string, object> { { "Id", orderId } });
        }

        public async Task<Product> GetById(int orderId)
        {
            var result = await ConvertConnectedQueryResultToModel(GETPRODUCTBYID, new Dictionary<string, object> { { "Id", orderId } });

            return result.First();
        }

        public async Task<List<Product>> GetAll()
        {
            var result = await ConvertConnectedQueryResultToModel(GETALLPRODUCT, new Dictionary<string, object>());

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
