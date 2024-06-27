using Identity.Application.Common.Response;

namespace Identity.Application.Common.Models.User.Get
{
    public class UserResponse : Response<UserDto>
    {
        public UserResponse(UserDto user) 
            : base(user){ }
    }
}
