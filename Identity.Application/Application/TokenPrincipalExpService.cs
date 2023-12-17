using Identity.Application.Common.Abstractions;
using Identity.Application.Common.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Application.Application
{
    public class TokenPrincipalExpService : ITokenPrincipalExpService
    {
        private readonly JwtConfig _jwtConfig;
        public TokenPrincipalExpService(IOptions<JwtConfig> options) => _jwtConfig = options.Value;
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                    ValidateIssuer = _jwtConfig.Issuer is null ? false : true,
                    ValidIssuer = _jwtConfig.Issuer!,
                    ValidateAudience = _jwtConfig.Audience is null ? false : true,
                    ValidAudiences = new List<string>() { _jwtConfig.Audience! },
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret)),
                    ValidateIssuerSigningKey = true
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
    }
}
