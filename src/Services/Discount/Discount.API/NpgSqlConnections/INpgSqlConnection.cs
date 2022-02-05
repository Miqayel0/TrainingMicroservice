using Npgsql;

namespace Discount.API.NpgSqlConnections
{
    public interface INpgSqlConnection :  IAsyncDisposable
    {
        NpgsqlConnection GetConnection();
    }
}
