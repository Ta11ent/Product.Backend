using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Application.Application;
using ShoppingCart.Application.Common.Abstractions;

namespace ShoppingCart.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IOrderReppository, OrderRepository>();
            services.AddScoped<IProductRangeRepository, ProductRangeRepository>();
            
            services.AddHttpClient(nameof(ProductService), option =>
            {
                option.BaseAddress = new Uri(configuration["ServiceURLs:ProductAPI"]);
                option.Timeout = new TimeSpan(0, 0, 20);
            });
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
