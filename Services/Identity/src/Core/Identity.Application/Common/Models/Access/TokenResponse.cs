using Identity.Application.Common.Response;

namespace Identity.Application.Common.Models.Access.Login
{
    public class TokenResponse : Response<TokenDto>
    {
        public TokenResponse(TokenDto user) 
            : base(user) { }
    }
}