using Identity.Application.Application;
using Identity.Application.Common.Abstractions;
using Identity.Application.Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<JwtConfig>(configuration.GetSection(nameof(JwtConfig)));
            services.AddTransient<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
           // services.AddScoped<ITokenPrincipalExpService, TokenPrincipalExpService>();
            return services;
        }
    }
}
