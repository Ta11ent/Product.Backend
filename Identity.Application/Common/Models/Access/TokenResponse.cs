using Identity.Application.Common.Response;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Common.Models.Access.Login
{
    public class TokenResponse : Response<TokenDto>
    {
        public TokenResponse(TokenDto user, IEnumerable<IdentityError> errors = null!) : base(user, errors) { }
    }
}