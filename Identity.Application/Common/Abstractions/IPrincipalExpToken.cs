using Identity.Application.Common.Models;
using System.Security.Claims;

namespace Identity.Application.Common.Abstractions
{
    public interface IPrincipalExpToken
    {
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
