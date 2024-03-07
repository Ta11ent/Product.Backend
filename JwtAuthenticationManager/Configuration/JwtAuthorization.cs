using Microsoft.Extensions.DependencyInjection;

namespace JwtAuthenticationManager.Configuration
{
    public static class JwtAuthorization
    {
        public static IServiceCollection AddAuthorizationConfiguration(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin",
                     policy => policy.RequireRole("Admin"));
                options.AddPolicy("User",
                     policy => policy.RequireRole("User"));
            });

            return services;
        }
    }
}
