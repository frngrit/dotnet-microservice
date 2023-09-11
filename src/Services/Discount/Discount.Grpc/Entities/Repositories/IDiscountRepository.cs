using Discount.Grpc.Entities;

namespace Discount.Grpc.Entities.Repositories
{
	public interface IDiscountRepository
	{
		Task<Coupon> GetCoupon(string productName);

        Task<bool> CreateCounpon(Coupon coupon);

        Task<Coupon> UpdateCoupon(Coupon coupon);

        Task<bool> DeleteCoupon(string productName);
    }
}

