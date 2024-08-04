using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Identity.API.Configuration
{
    public static class JwtAuthentication
    {
        public static IServiceCollection AddJwtAuthenticationConfiguration(this IServiceCollection services, 
            IConfiguration configuration)
        {
            var jwtConfig = configuration.GetSection(nameof(JwtConfig)).Get<JwtConfig>();

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
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = jwtConfig!.Issuer is null ? false : true,
                    ValidIssuer = jwtConfig.Issuer!,
                    ValidateAudience = jwtConfig.Audience is null ? false : true,
                    ValidAudiences = new List<string>() { jwtConfig.Audience! },
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret)),
                    ValidateIssuerSigningKey = true
                };
            });

            return services; 
        }
    }
}
