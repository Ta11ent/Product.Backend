using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ShoppingCart.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(config =>
                config.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly));

            services.AddMassTransit(config =>
            {
                config.SetKebabCaseEndpointNameFormatter();
                config.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(configuration["RabbitMqConfig:Host"]!), details =>
                    {
                        details.Username(configuration["RabbitMqConfig:Username"]!);
                        details.Password(configuration["RabbitMqConfig:Password"]!);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
