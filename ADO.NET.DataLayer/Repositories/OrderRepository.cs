using ADO.NET.DataLayer.Models;
using ADO.NET.DataLayer.Repositories.Interfaces;

namespace ADO.NET.DataLayer.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public const string CREATEORDER = "INSERT INTO Orders (Status, CreateDate, UpdateDate, ProductId) VALUES (@Status, @CreateDate, @UpdateDate, @ProductId);";
        public const string DELETEORDER = "DELETE FROM Orders WHERE Id = @id;";
        public const string GETALLORDERS = "SELECT * FROM Orders;";
        public const string GETORDERBYID = "SELECT * FROM Orders WHERE Id = @Id;";
        public const string UPDATEORDER = "UPDATE Orders SET Status=@Status, UpdateDate=@UpdateDate, ProductId=@ProductId WHERE Id=@Id;";
        public OrderRepository(DbContext connection) : base(connection)
        {
        }

        public async Task Create(Order order)
        {
            var paramsDictionary = GetParamsDictionary(order);

            await ExecuteConnectedQueryWithParams(CREATEORDER, paramsDictionary);
        }

        public void Update(Order order)
        {
            var paramsDictionary = GetParamsDictionary(order);

            ExecuteDisconnectedQuery(UPDATEORDER, paramsDictionary);
        }

        public async Task Delete(int orderId)
        {
            await ExecuteConnectedQueryWithParams(DELETEORDER, new Dictionary<string, object> { { "Id", orderId } });
        }

        public async Task<Order> GetById(int orderId)
        {
            var result = await ConvertConnectedQueryResultToModel(GETORDERBYID, new Dictionary<string, object> { { "Id", orderId } });

            return result.First();
        }

        public async Task<List<Order>> GetAll()
        {
            var result = await ConvertConnectedQueryResultToModel(GETALLORDERS, new Dictionary<string, object>());

            return result;
        }

        public async Task<List<Order>> GetOrders(Filter filter)
        {
            var paramsDictionary = GetParamsDictionary(filter);

            var result = await ConvertExecutedProcedureWithParamsToModel("GetOrders", paramsDictionary);

            return result;
        }

        public async Task DeleteOrdersInBulk(Filter filter)
        {
            var paramsDictionary = GetParamsDictionary(filter);

            await ExecuteProcedureWithParams("DeleteOrders", paramsDictionary);
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
