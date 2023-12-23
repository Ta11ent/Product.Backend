using Identity.Application.Common.Response;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Common.Models.User.Get
{
    public class UserResponse : Response<UserDto>
    {
        public UserResponse(UserDto user, IEnumerable<IdentityError> errors) : base(user, errors){ }
    }
}
