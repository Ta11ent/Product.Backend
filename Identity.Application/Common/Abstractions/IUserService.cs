using Identity.Application.Common.Models.User.Create;
using Identity.Application.Common.Models.User.Login;
using Identity.Application.Common.Models.User.Common;

namespace Identity.Application.Common.Abstractions
{
    public interface IUserService
    {
        Task<UserLoginResponse> LoginUserAsync(UserLoginCommand user);
        Task<CreateUserResponse> CreateUserAsync(CreateUserCommand user);
        Task<CommonResponse> DisableUserAsync(string Id);
        Task<CommonResponse> EnableUserAsync(string Id);
    }
}
