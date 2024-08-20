using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, 
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MsSQL");
            services.AddDbContext<ProductDbContext>(config =>
            {
                config.UseSqlServer(connectionString);
            });

            services.AddScoped<IProductDbContext, ProductDbContext>();

            return services;
        }
    }
}
