using System.Data;
using System.Data.SqlClient;
using ADO.NET.DataLayer.Repositories.Interfaces;

namespace ADO.NET.DataLayer.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : new()
    {
        private readonly IDbConnection _connection;

        public BaseRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void ExecuteConnectedQueryWithParams(string query, Dictionary<string, object> parameters)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = query;

                foreach (var param in parameters)
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@" + param.Key;
                    parameter.Value = param.Value;
                    command.Parameters.Add(parameter);
                }

                OpenConnection();

                command.ExecuteNonQuery();
            }
        }

        public void ExecuteProcedureWithParams(string procedureName, Dictionary<string, object> parameters)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;

                foreach (var param in parameters)
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@" + param.Key;
                    parameter.Value = param.Value;
                    command.Parameters.Add(parameter);
                }

                OpenConnection();
                command.ExecuteNonQuery();
            }
        }

        public List<T> ConvertExecutedProcedureWithParamsToModel(string procedureName, Dictionary<string, object> parameters)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;

                foreach (var param in parameters)
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@" + param.Key;
                    parameter.Value = param.Value;
                    command.Parameters.Add(parameter);
                }

                var readStream = command.ExecuteReader();

                var result = new List<T>();
                while (readStream.Read())
                {
                    var item = new T();
                    foreach (var prop in item.GetType().GetProperties())
                    {
                        prop.SetValue(item, readStream[prop.Name]);
                    }
                    result.Add(item);
                }

                return result;
            }
        }

        public List<T> ConvertConnectedQueryResultToModel(string query, Dictionary<string, object> parameters)
        {
            OpenConnection();

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = query;

                foreach (var param in parameters)
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@" + param.Key;
                    parameter.Value = param.Value;
                    command.Parameters.Add(parameter);
                }

                var readStream = command.ExecuteReader();

                var result = new List<T>();
                while (readStream.Read())
                {
                    var item = new T();
                    foreach (var prop in item.GetType().GetProperties())
                    {
                        prop.SetValue(item, readStream[prop.Name]);
                    }
                    result.Add(item);
                }

                return result;
            }
        }

        public DataTable ExecuteDisconnectedQuery(string query, Dictionary<string, object> parameters)
        {
            using (var connection = _connection)
            {
                OpenConnection();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;

                    foreach (var param in parameters)
                    {
                        var parameter = command.CreateParameter();
                        parameter.ParameterName = "@" + param.Key;
                        parameter.Value = param.Value;
                        command.Parameters.Add(parameter);
                    }

                    using (var adapter = new SqlDataAdapter(command as SqlCommand))
                    {
                        var table = new DataTable();
                        adapter.Fill(table);
                        return table;
                    }
                }
            }
        }

        private void OpenConnection()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }
    }
}
