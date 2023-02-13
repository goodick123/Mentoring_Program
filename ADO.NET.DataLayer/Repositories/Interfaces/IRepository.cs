using System.Data;

namespace ADO.NET.DataLayer.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        void ExecuteConnectedQueryWithParams(string query, Dictionary<string, object> parameters);

        void ExecuteProcedureWithParams(string procedureName, Dictionary<string, object> parameters);

        List<T> ConvertConnectedQueryResultToModel(string query, Dictionary<string, object> parameters);

        List<T> ConvertExecutedProcedureWithParamsToModel(string procedureName, Dictionary<string, object> parameters);

        DataTable ExecuteDisconnectedQuery(string query, Dictionary<string, object> parameters);
    }
}
