using JwtAuthenticationManager.Abstractions;
using JwtAuthenticationManager.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JwtAuthenticationManager.Services
{
    public class JwtTokenService : ITokenService
    {
        private readonly JwtConfig jwtConfig;
        public JwtTokenService(IOptions<JwtConfig> jwtConfig) => this.jwtConfig = jwtConfig.Value;
        public virtual string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: jwtConfig.Issuer,
                audience: jwtConfig.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(jwtConfig.ExpAccToken),
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

        public virtual ClaimsPrincipal GetPrincipalFromExpiredToken(string token) //false
        {
            var tokenValidationParameters = new JwtTokenValidationParameters(jwtConfig).Generate(false);
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }

        public virtual DateTime RefreshTokenExpiryTime() => DateTime.Now.AddMinutes(jwtConfig.ExpRefToken);
    }
}
