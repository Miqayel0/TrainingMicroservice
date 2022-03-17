using Dapper;
using Discount.Grpc.Entities;
using Discount.Grpc.NpgSqlConnections;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly INpgSqlConnectionPool _connection;

        public DiscountRepository(INpgSqlConnectionPool connection)
        {
            _connection = connection;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            var connection = _connection.GetConnection();

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });

            return coupon ?? Coupon.NotFound;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            var connection = _connection.GetConnection();

            var affected = await connection.ExecuteAsync
                    ("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                            new { coupon.ProductName, coupon.Description, coupon.Amount });

            return affected != default;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            var connection = _connection.GetConnection();

            var affected = await connection.ExecuteAsync
                    ("UPDATE Coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
                            new { coupon.ProductName, coupon.Description, coupon.Amount, coupon.Id });

            return affected != default;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            var connection = _connection.GetConnection();

            var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName",
                new { ProductName = productName });

            return affected != default;
        }
    }
}
