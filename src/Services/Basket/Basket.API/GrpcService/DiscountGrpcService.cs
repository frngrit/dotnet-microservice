using System;
using Discount.Grpc.Protos;
using static Discount.Grpc.Protos.DiscountProtoService;

namespace Basket.API.GrpcService
{
    public class DiscountGrpcService : IDiscountGrpcService
    {
        private readonly DiscountProtoServiceClient _discountProtoServiceClient;

        public DiscountGrpcService(DiscountProtoServiceClient discountProtoServiceClient)
        {
            _discountProtoServiceClient = discountProtoServiceClient;
        }

        public async Task<CouponModel> GetCoupon(string productName)
        {
            var request = new GetCouponRequest()
            {
                ProductName = productName
            };

            return await _discountProtoServiceClient.GetCouponAsync(request);
        }
    }
}

