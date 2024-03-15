using JwtAuthenticationManager.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JwtAuthenticationManager.Configuration
{
    public static class JwtAuthentication
    {
        public static IServiceCollection AddJwtAuthenticationConfiguration(this IServiceCollection services)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();
            var jwtConfig = config.GetSection(nameof(JwtConfig)).Get<JwtConfig>()!;
            services.Configure<JwtConfig>(config.GetSection(nameof(JwtConfig)));


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new JwtTokenValidationParameters(jwtConfig).Generate();
            });

            return services; 
        }
    }
}
