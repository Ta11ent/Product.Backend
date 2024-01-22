using Identity.Application.Application;
using Identity.Application.Common.Abstractions;
using Identity.Application.Common.Models;
using Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,
            IConfiguration configuration)
        {
           // services.Configure<JwtConfig>(configuration.GetSection(nameof(JwtConfig)));

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccessService, AccessService>();

            return services;
        }
    }
}
