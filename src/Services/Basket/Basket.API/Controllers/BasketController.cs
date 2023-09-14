using System.Net;
using Basket.API.Entities;
using Basket.API.GrpcService;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IDiscountGrpcService _discountGrpcService;

        public BasketController(
            IBasketRepository basketRepository,
            IDiscountGrpcService discountGrpcService
            )
        {
            _basketRepository = basketRepository;
            _discountGrpcService = discountGrpcService;
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var record = await _basketRepository.GetBasket(userName);

            return Ok(record ?? new ShoppingCart(userName: userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket(ShoppingCart basket)
        {
            foreach (var item in basket.Items)
            {
                // TODO: Communicate to discount.
                var coupon = await _discountGrpcService.GetCoupon(item.ProductName);
                // TODO: and calculate total price 
                item.Price -= coupon.Amount;
            }


            var result = await _basketRepository.UpdateBasket(basket);

            return Ok(result);
        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteBasket(string userName)
        {
            await _basketRepository.DeleteBasket(userName);

            return Ok();
        }
    }
}

