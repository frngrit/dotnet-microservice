using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Entities.Repositories;
using Discount.Grpc.Mappers;
using Discount.Grpc.Protos;
using Grpc.Core;

namespace Discount.Grpc.Services
{
	public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;

        public DiscountService(
            IDiscountRepository discountRepository,
            ILogger<DiscountService> logger,
            IMapper mapper
            )
		{
			_discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public override async Task<CouponModel> GetCoupon(GetCouponRequest request, ServerCallContext context)
        {
            var coupon = await _discountRepository.GetCoupon(request.ProductName);

            var couponModel = _mapper.Map<CouponModel>(coupon);

            _logger.LogInformation($"Discount Grpc is retriving information for: {request.ProductName}");

            return couponModel;
        }

        public override async Task<CouponModel> CreateCoupon(CreateCouponRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            var isSuccess = await _discountRepository.CreateCounpon(coupon);

            if (!isSuccess) throw new Exception("Cannot create coupon");

            var createdCoupon = await _discountRepository.GetCoupon(coupon.ProductName);

            return _mapper.Map<CouponModel>(createdCoupon);
        }

        public override async Task<CouponModel> UpdateCoupon(UpdateCouponRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            var updatedCoupon = await _discountRepository.UpdateCoupon(coupon);

            return _mapper.Map<CouponModel>(updatedCoupon);
        }

        public override async Task<DeleteCouponResponse> DeleteCoupon(DeleteCouponRequest request, ServerCallContext context)
        {

            var success = await _discountRepository.DeleteCoupon(request.ProductName);

            return new DeleteCouponResponse()
            {
                Success = success
            };
        }
    }
}

