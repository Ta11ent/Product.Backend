using Identity.Application.Common.Response;

namespace Identity.Application.Common.Models.User.Get
{
    public class UsersResponse : Response<List<UserDto>>
    {
        public UsersResponse(List<UserDto> user) 
            : base(user) { }
    }
}
