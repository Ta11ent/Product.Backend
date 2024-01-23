using Identity.Application.Application;
using Identity.Application.Common.Abstractions;
using Identity.Application.Common.Mapping;
using Identity.Application.Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Identity.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<JwtConfig>(configuration.GetSection(nameof(JwtConfig)));

            //services.AddAutoMapper(config =>
            //{
            //    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
            //    config.AddProfile(new AssemblyMappingProfile(typeof(AuthDbContext).Assembly));
            //});
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAccessService, AccessService>();

            return services;
        }
    }
}
