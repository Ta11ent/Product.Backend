using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Infrastructure.Services;

namespace ProductCatalog.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.TryAddScoped<ICurrencyService, CurrencyService>();
            return services;
        }
    }
}
