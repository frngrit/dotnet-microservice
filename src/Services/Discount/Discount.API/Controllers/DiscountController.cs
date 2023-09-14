using System.Net;
using Discount.API.Entities;
using Discount.API.Entities.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _disCountRepository;

        public DiscountController(IDiscountRepository disCountRepository)
        {
            _disCountRepository = disCountRepository;
        }

        [HttpGet("{productName}", Name = "GetCoupon")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetCoupon(string productName)
        {
            return Ok(await _disCountRepository.GetCoupon(productName));
        }

        [HttpPost(Name = "CreateCoupon")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<bool>> Create(Coupon coupon)
        {
            return await _disCountRepository.CreateCounpon(coupon);
        }

        [HttpPut(Name = "UpdateCoupon")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> UpdateCoupon(Coupon coupon)
        {
            return await _disCountRepository.UpdateCoupon(coupon);
        }

        [HttpDelete(Name = "DeleteCoupon")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteCoupon(string productName)
        {
            return await _disCountRepository.DeleteCoupon(productName);
        }

    }
}

