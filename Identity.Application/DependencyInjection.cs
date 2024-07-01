using Identity.Application.Common.Abstractions;
using Identity.Application.Common.Mapping;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Identity.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //services.AddAutoMapper(config =>
            //{
            //    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
            //    config.AddProfile(new AssemblyMappingProfile(typeof(IAuthDbContext).Assembly));
            //});
            return services;
        }
    }
}
