using JwtAuthenticationManager.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JwtAuthenticationManager
{
    internal class JwtTokenValidationParameters
    {
        private readonly JwtConfig jwtConfig;
        internal JwtTokenValidationParameters()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();
            jwtConfig = config.GetSection(nameof(JwtConfig)).Get<JwtConfig>()!;
        }
        internal JwtTokenValidationParameters(JwtConfig jwtConfig) => this.jwtConfig = jwtConfig;
        internal virtual TokenValidationParameters Generate(bool validateLifetime)
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = jwtConfig.Issuer is null ? false : true,
                ValidIssuer = jwtConfig.Issuer!,
                ValidateAudience = jwtConfig.Audience is null ? false : true,
                ValidAudiences = new List<string>() { jwtConfig.Audience! },
                ValidateLifetime = validateLifetime,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret)),
                ValidateIssuerSigningKey = true
            };
        }
    }
}