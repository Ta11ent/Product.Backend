using Identity.Application.Common.Response;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Common.Models.User.Get
{
    public class UsersResponse : Response<List<UserDto>>
    {
        public UsersResponse(List<UserDto> user, IEnumerable<IdentityError> errors = null!) : base(user, errors) { }
    }
}
