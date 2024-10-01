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
            services.TryAddScoped<ICashService, CashService>();

            services.TryAddScoped<ICategoryRepository, CategoryRepository>();
            services.TryAddScoped<ISubCategoryRepository, SubCategoryRepository>();
            services.TryAddScoped<ICurrencyRepository, CurrencyRepository>();
            services.TryAddScoped<IManufacturerRepository, ManufacturerRepository>();
            services.TryAddScoped<IROERepository, ROERepository>();
            services.TryAddScoped<IProductRepository, ProductRepository>();

            ///<summary
            ///Cashed Repository
            ///</summary>
            services.Decorate<ICategoryRepository, CashedCategoryRepository>();
            services.Decorate<ISubCategoryRepository, CashedSubCategoryRepository>();
            services.Decorate<ICurrencyRepository, CashedCurrencyRepoitory>();
            services.Decorate<IManufacturerRepository, CashedManufacturerRepository>();

            services.AddStackExchangeRedisCache(redisOptions =>
            {
                string connection = configuration.GetConnectionString("Redis")!;
                redisOptions.Configuration = connection;
            });

            return services;
        }
    }
}
