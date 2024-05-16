using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ShoppingCart.Application.Application;
using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Application.Common.Models.Options;

namespace ShoppingCart.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IOrderReppository, OrderRepository>();
            services.AddScoped<IProductRangeRepository, ProductRangeRepository>();
            services.TryAddScoped<IRabbitMqProducerService, ProducerService>();

            services.Configure<Endpoints>(configuration.GetSection(nameof(Endpoints)));

            services.AddHttpClient(nameof(ProductService), option =>
            {
                option.BaseAddress = new Uri(configuration["ServiceURL:ProductAPI"]!);
                option.Timeout = new TimeSpan(0, 0, 20);
            });

            services.AddHttpClient(nameof(ProducerService), option =>
            {
                option.BaseAddress = new Uri(configuration["ServiceURL:ProducerAPI"]!);
                option.Timeout = new TimeSpan(0, 0, 20);
            });
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
