using Identity.Application.Common.Response;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Common.Models.Access.Login
{
    public class UserLoginResponse : Response<UserLoginDto>
    {
        public UserLoginResponse(UserLoginDto user, IEnumerable<IdentityError> errors) : base(user, errors) { }
    }
}
