using System.Security.Claims;
namespace JwtAuthenticationManager.Abstractions
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        DateTime RefreshTokenExpiryTime();
    }
}
