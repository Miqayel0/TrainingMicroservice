using Npgsql;

namespace Discount.Grpc.NpgSqlConnections
{
    public interface INpgSqlConnection :  IAsyncDisposable
    {
        NpgsqlConnection GetConnection();
    }
}
