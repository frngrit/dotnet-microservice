using Discount.Grpc.Entities.Repositories;

namespace Discount.Grpc.Startups
{
	public static class ServicesRegistration
	{
		public static void RegisterRepositories(this IServiceCollection services)
		{
			services.AddScoped<IDiscountRepository, DiscountRepository>();
		}
	}
}

