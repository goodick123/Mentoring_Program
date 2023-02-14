using ADO.NET.DataLayer.Repositories.Interfaces;

namespace ADO.NET.DataLayer.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : new()
    {
        private readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public async Task ExecuteConnectedQueryWithParams(string query, Dictionary<string, object> parameters)
        {

            var command = _context._connection.CreateCommand();

            command.CommandText = query;

            foreach (var param in parameters)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = "@" + param.Key;
                parameter.Value = param.Value;
                command.Parameters.Add(parameter);
            }

            using (var connection = new SqlConnection(_context._connectionString))
            {

                await command.ExecuteNonQueryAsync();

            }
        }

        public async Task ExecuteProcedureWithParams(string procedureName, Dictionary<string, object> parameters)
        {

            var command = _context._connection.CreateCommand();
            command.CommandText = procedureName;
            command.CommandType = CommandType.StoredProcedure;

            foreach (var param in parameters)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = "@" + param.Key;
                parameter.Value = param.Value;
                command.Parameters.Add(parameter);
            }

            using (var connection = new SqlConnection(_context._connectionString))
            {
                await command.ExecuteNonQueryAsync();
            }

        }

        public async Task<List<T>> ConvertExecutedProcedureWithParamsToModel(string procedureName, Dictionary<string, object> parameters)
        {
            var command = _context._connection.CreateCommand();

            command.CommandText = procedureName;
            command.CommandType = CommandType.StoredProcedure;

            foreach (var param in parameters)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = "@" + param.Key;
                parameter.Value = param.Value;
                command.Parameters.Add(parameter);
            }

            using (var readStream = await command.ExecuteReaderAsync())
            {

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

        public async Task<List<T>> ConvertConnectedQueryResultToModel(string query, Dictionary<string, object> parameters)
        {
            using (var connection = new SqlConnection(_context._connectionString))
            {

                var command = _context._connection.CreateCommand();


                command.CommandText = query;

                foreach (var param in parameters)
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@" + param.Key;
                    parameter.Value = param.Value;
                    command.Parameters.Add(parameter);
                }

                using (var readStream = await command.ExecuteReaderAsync())
                {

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
        }

        public DataTable ExecuteDisconnectedQuery(string query, Dictionary<string, object> parameters)
        {
            using (var connection = new SqlConnection(_context._connectionString))
            {

                var command = _context._connection.CreateCommand();
                command.CommandText = query;

                foreach (var param in parameters)
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@" + param.Key;
                    parameter.Value = param.Value;
                    command.Parameters.Add(parameter);
                }

                using (var adapter = new SqlDataAdapter(command))
                {
                    var table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
        }
    }
}
