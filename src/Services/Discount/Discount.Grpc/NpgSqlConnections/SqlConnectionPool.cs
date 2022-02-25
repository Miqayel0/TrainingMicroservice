using Npgsql;

namespace Discount.Grpc.NpgSqlConnections
{
    public class SqlConnectionPool : INpgSqlConnectionPool
    {
        private NpgsqlConnection _connection;
        public SqlConnectionPool(string connectionString)
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
