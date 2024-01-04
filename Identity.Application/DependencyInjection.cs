using Identity.Application.Application;
using Identity.Application.Common.Abstractions;
using Identity.Application.Common.Models;
using Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Identity.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<JwtConfig>(configuration.GetSection(nameof(JwtConfig)));

           // services.TryAddScoped<SignInManager<AppUser>>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccessService, AccessService>();
           
           // services.AddScoped<ITokenPrincipalExpService, TokenPrincipalExpService>();
            return services;
        }
    }
}
