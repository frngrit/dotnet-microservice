using System;
using Basket.API.GrpcService;
using Basket.API.Repositories;
using static Discount.Grpc.Protos.DiscountProtoService;

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
            services.AddScoped<IDiscountGrpcService, DiscountGrpcService>();
        }

        public static void RegisterGrpc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpcClient<DiscountProtoServiceClient>
                (
                    options =>
                    {
                        string grpcAddress = configuration.GetValue<string>("GrpcSettings:DiscountUrl")
                        ?? throw new ArgumentNullException(nameof(grpcAddress));

                        options.Address = new Uri(grpcAddress);
                    }
                );
        }
    }
}

