using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Infrastructure.Services;
using ShoppingCart.ShoppingCart.Infrastructure.Options;

namespace ShoppingCart.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure
            (this IServiceCollection services, IConfiguration configuration)
        {
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

            services.AddHttpClient(nameof(UserService), option =>
            {
                option.BaseAddress = new Uri(configuration["ServiceURL:UserAPI"]!);
                option.Timeout = new TimeSpan(0, 0, 20);
            });

            services.TryAddScoped<IRabbitMqProducerService, ProducerService>();
            services.TryAddScoped<IProductService, ProductService>();
            services.TryAddScoped<IUserService,UserService>();

            return services;
        }
    }
}
