using System.Data.SqlClient;

namespace ADO.NET.DataLayer
{
    public class DbContext
    {
        public readonly SqlConnection _connection;
        public readonly string _connectionString;
        public DbContext(string connection)
        {
            _connectionString = connection;
            _connection = new SqlConnection(connection);
        }
    }
}
