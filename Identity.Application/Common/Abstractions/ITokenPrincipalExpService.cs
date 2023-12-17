using Identity.Application.Common.Models;
using System.Security.Claims;

namespace Identity.Application.Common.Abstractions
{
    public interface ITokenPrincipalExpService
    {
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
