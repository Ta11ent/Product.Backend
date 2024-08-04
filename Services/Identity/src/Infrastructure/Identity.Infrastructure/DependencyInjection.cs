using Identity.Application.Common.Abstractions;
using Identity.Infrastructure.Services;
using Identity.src.Core.Application.Common.Abstractions;
using JwtAuthenticationManager.Abstractions;
using JwtAuthenticationManager.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace Identity.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.TryAddScoped<IUserService, UserService>();
            services.TryAddScoped<IUserTokenService, TokenService>();
            services.TryAddScoped<ITokenService, JwtTokenService>();
            services.TryAddScoped<IAccessService, AccessService>();

            return services;
        }
    }
}
