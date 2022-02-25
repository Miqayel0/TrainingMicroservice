using Npgsql;

namespace Discount.Grpc.NpgSqlConnections
{
    public interface INpgSqlConnectionPool :  IAsyncDisposable
    {
        NpgsqlConnection GetConnection();
    }
}
