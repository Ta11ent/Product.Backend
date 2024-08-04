using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Application.Common.Abstractions;

namespace ShoppingCart.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration) 
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<OrderDbContext>(config =>
            {
                config.UseSqlServer(connectionString);
            });

            //services.AddScoped<IOrderDbContext, OrderDbContext>();
            services.AddScoped<IOrderDbContext>(provide => provide.GetService<OrderDbContext>());

            return services;
        }
    }
}
