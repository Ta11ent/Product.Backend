using Identity.Application.Application;
using Identity.Application.Common.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using JwtAuthenticationManager.Abstractions;
using JwtAuthenticationManager.Services;

namespace Identity.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, JwtTokenService>();
            services.AddScoped<IAccessService, AccessService>();

            return services;
        }
    }
}
