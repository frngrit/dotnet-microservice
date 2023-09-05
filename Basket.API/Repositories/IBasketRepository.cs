using Basket.API.Entities;

namespace Basket.API.Repositories
{
	public interface IBasketRepository
	{
		Task<ShoppingCart> GetShopping(string username);

		Task<ShoppingCart> UpdateBasket(ShoppingCart basket);

		Task DeleteBasket(string username);
	}
}

