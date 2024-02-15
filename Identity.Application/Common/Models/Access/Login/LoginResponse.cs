using Identity.Application.Common.Response;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Common.Models.Access.Login
{
    public class LoginResponse : Response<LogInDto>
    {
        public LoginResponse(LogInDto user, IEnumerable<IdentityError> errors = null!) : base(user, errors) { }
    }
}
