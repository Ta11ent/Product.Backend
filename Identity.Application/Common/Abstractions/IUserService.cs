using Identity.Application.Common.Models.User.Create;
using Identity.Application.Common.Models.User.Login;

namespace Identity.Application.Common.Abstractions
{
    public interface IUserService
    {
        Task<UserLoginResponse> LoginUserAsync(UserLoginCommand user);
        Task<CreateUserResponse> CreateUserAsync(CreateUserCommand user);
    }
}
