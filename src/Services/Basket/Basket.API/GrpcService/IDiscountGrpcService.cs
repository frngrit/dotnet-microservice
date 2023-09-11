
using Discount.Grpc.Protos;

namespace Basket.API.GrpcService
{
	public interface IDiscountGrpcService
	{
		Task<CouponModel> GetCoupon(string productName);
	}
}

