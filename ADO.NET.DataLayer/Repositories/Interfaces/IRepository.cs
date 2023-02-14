using System.Data;

namespace ADO.NET.DataLayer.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task ExecuteConnectedQueryWithParams(string query, Dictionary<string, object> parameters);

        Task ExecuteProcedureWithParams(string procedureName, Dictionary<string, object> parameters);

        Task<List<T>> ConvertConnectedQueryResultToModel(string query, Dictionary<string, object> parameters);

        Task<List<T>> ConvertExecutedProcedureWithParamsToModel(string procedureName, Dictionary<string, object> parameters);

        DataTable ExecuteDisconnectedQuery(string query, Dictionary<string, object> parameters);
    }
}
