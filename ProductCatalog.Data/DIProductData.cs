using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Data
{
    public static class DIProductData
    {
        public static IServiceCollection AddProductData(this IServiceCollection services, 
            IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<ProductDbContext>(config =>
            {
                config.UseSqlServer(connectionString);
            });

            services.AddScoped<IProductDbContext, ProductDbContext>();

            return services;
        }
    }
}
