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
            services.AddScoped<IProductService, ProductService>();
            services.AddHttpClient("Product", option =>
                option.BaseAddress = new Uri(configuration["ServiceURLs:ProductAPI"]));

            return services;
        }
    }
}
