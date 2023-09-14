using Discount.API.Entities;

namespace Discount.API.Entities.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetCoupon(string productName);

        Task<bool> CreateCounpon(Coupon coupon);

        Task<Coupon> UpdateCoupon(Coupon coupon);

        Task<bool> DeleteCoupon(string productName);
    }
}

