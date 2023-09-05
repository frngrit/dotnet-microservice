using System;
using Basket.API.Repositories;

namespace Basket.API.Startups
{
	public static class ServicesRegister
	{
		public static void RegisterRedis(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddStackExchangeRedisCache(options =>
			{
				options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
			});
		}

		public static void RegisterRepositories(this IServiceCollection services)
		{
			services.AddScoped<IBasketRepository, BasketRepository>();
		}
	}
}

