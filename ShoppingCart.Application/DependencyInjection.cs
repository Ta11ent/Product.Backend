using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Application.Application;
using ShoppingCart.Application.Common.Abstractions;

namespace ShoppingCart.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IOrderReppository, OrderRepository>();
            services.AddTransient<IProductRangeRepository, ProductRangeRepository>();

            return services;
        }
    }
}
