using System.Security.Claims;

namespace Identity.Application.Common.Abstractions
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
    }
}
