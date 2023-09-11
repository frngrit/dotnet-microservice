using Dapper;
using Npgsql;

namespace Discount.Grpc.Entities.Repositories
{
	public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public string ConnectionString => _configuration.GetValue<string>("DatabaseSettingss:ConnectionString")
                    ?? throw new ArgumentNullException(nameof(ConnectionString));

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> CreateCounpon(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            var affected = await connection.ExecuteAsync
                ("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                new { coupon.ProductName, coupon.Description, coupon.Amount });

            return affected != 0;
        }

        public async Task<bool> DeleteCoupon(string productName)
        {
            using var connection = new NpgsqlConnection(ConnectionString);

            var affected = await connection.ExecuteAsync
                ("DELETE FROM Coupon WHERE ProductName = @ProductName",
                new { ProductName = productName });

            return affected != 0;
        }

        public async Task<Coupon> GetCoupon(string productName)
        {
            using var connection =  new NpgsqlConnection(ConnectionString);
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });

            if (coupon == null) return new Coupon()
            {
                ProductName = "No discount",
                Amount = 0,
                Description = "No discount Description"
            };

            return coupon;
        }

        public async Task<Coupon> UpdateCoupon(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            var affected = await connection.ExecuteAsync
                ("UPDATE Coupon SET ProductName=@ProductName, Description=@Description, Amount=@Amount WHERE Id=@Id",
                new { coupon.ProductName, coupon.Description, coupon.Amount, coupon.Id });

            return await GetCoupon(coupon.ProductName);
        }
    }
}

