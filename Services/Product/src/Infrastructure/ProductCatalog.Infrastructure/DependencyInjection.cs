using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Infrastructure.Repositories;
using ProductCatalog.Infrastructure.Services;

namespace ProductCatalog.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddScoped<ICurrencyService, CurrencyService>();

            services.TryAddScoped<ICashService, CashService>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.Decorate<ICategoryRepository, CashedCategoryRepository>();

            services.AddStackExchangeRedisCache(redisOptions =>
            {
                string connection = configuration.GetConnectionString("Redis")!;
                redisOptions.Configuration = connection;
            });

            return services;
        }
    }
}
