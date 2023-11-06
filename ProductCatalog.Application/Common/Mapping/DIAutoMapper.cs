using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Application.Interfaces;
using System.Reflection;

namespace ProductCatalog.Application.Common.Mapping
{
    public static class DIAutoMapper
    {
        public static IServiceCollection AddCustomAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(
                  config =>
                  {
                      config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                      config.AddProfile(new AssemblyMappingProfile(typeof(IProductDbContext).Assembly));
                  });
            return services;
        }
    }
}
