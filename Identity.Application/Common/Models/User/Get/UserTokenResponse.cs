using Identity.Application.Common.Response;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Common.Models.User.Get
{
    public class UserTokenResponse : Response<UserTokenDto>
    {
        public UserTokenResponse(UserTokenDto data) 
            : base(data) { }
    }
}
