using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories
{
	public class BasketRepository : IBasketRepository
	{
        private IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
		{
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task DeleteBasket(string username)
        {
            await _redisCache.RemoveAsync(username);
        }

        public async Task<ShoppingCart?> GetBasket(string username)
        {
            var basket = await _redisCache.GetStringAsync(username);

            if (string.IsNullOrEmpty(basket)) return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            var stringBasket = JsonConvert.SerializeObject(basket);

            await _redisCache.SetStringAsync(basket.UserName, stringBasket);

            return (await GetBasket(basket.UserName))!;
        }
    }
}

