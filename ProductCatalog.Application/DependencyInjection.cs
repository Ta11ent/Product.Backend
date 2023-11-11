using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Application.Common.Mapping;
using System.Reflection;

namespace ProductCatalog.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config => 
            config.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly));

            services.AddAutoMapper(config =>
                 {
                     config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                     config.AddProfile(new AssemblyMappingProfile(typeof(IProductDbContext).Assembly));
                 });

            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });

            return services;
        }
    }
}
