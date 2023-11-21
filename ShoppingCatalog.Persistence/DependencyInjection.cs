using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Application.Abstractions;

namespace ShoppingCart.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration) 
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<OrderDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IOrderDbContext, OrderDbContext>();

            return services;
        }
    }
}
