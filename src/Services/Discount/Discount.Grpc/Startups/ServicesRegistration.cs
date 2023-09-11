using AutoMapper;
using Discount.Grpc.Entities.Repositories;
using Discount.Grpc.Mappers;

namespace Discount.Grpc.Startups
{
	public static class ServicesRegistration
	{
		public static void RegisterRepositories(this IServiceCollection services)
		{
			services.AddScoped<IDiscountRepository, DiscountRepository>();
			services.AddAutoMapper(typeof(Program));
        }
    }
}

