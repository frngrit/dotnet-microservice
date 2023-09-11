using System.Net;
using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class BasketController : ControllerBase
	{
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
		{
			_basketRepository = basketRepository;
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
            // TODO: Communicate to discount.Grpc
			// TODO: and calculate total price 

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

