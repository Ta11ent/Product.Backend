using Identity.Application.Common.Abstractions;
using Identity.Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Application.Application
{
    public class TokenService : ITokenService
    {    
        private readonly JwtConfig _jwtConfig;
        public TokenService(IOptions<JwtConfig> options) => _jwtConfig = options.Value;
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtConfig.ExpAccToken),
                signingCredentials: signinCredentials
            );

             return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }

        public virtual string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtConfig.Issuer is null ? false : true,
                ValidIssuer = _jwtConfig.Issuer!,
                ValidateAudience = _jwtConfig.Audience is null ? false : true,
                ValidAudiences = new List<string>() { _jwtConfig.Audience! },
                ValidateLifetime = false,
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

        public DateTime RefreshTokenExpiryTime() => DateTime.Now.AddMinutes(_jwtConfig.ExpRefToken);
    }

}
