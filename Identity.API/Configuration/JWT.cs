using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Identity.API.Configuration
{
    public static class JWT
    {
        public static IServiceCollection AddJwtAuthenticationConfiguration(this IServiceCollection services, 
            IConfiguration configuration)
        {
            var issuer = configuration.GetSection("JwtConfig:Issuer").Value!;
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JwtConfig:Secret").Value!));
            var audience = new List<string>() { configuration.GetSection("JwtConfig:Audience").Value! };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudiences = audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = signingKey,
                    ValidateIssuerSigningKey = true
                };
});
            return services; 
        }
    }
}
