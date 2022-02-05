using Npgsql;

namespace Discount.API.NpgSqlConnections
{
    public class SqlConnection : INpgSqlConnection
    {
        private NpgsqlConnection _connection;
        public SqlConnection(string connectionString)
        {
            _connection = new NpgsqlConnection(connectionString);
        }

        public NpgsqlConnection GetConnection()
        {
            return _connection 
                ?? throw new Exception("Sql Connection Failed");
        }

        public async ValueTask DisposeAsync()
        {
            if (_connection != null)
            {
                await _connection.DisposeAsync();
                _connection = null;
            }
            GC.SuppressFinalize(this);
        }
    }
}
