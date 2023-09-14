using System;
using Discount.API.Entities.Repositories;

namespace Discount.API.Startups
{
    public static class ServicesRegistration
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDiscountRepository, DiscountRepository>();
        }
    }
}

