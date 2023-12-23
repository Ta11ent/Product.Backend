using Identity.Application.Common.Models.User.Create;
using Identity.Application.Common.Models.User.Password;
using Identity.Application.Common.Models.User.Get;

namespace Identity.Application.Common.Abstractions
{
    public interface IUserService
    {
       // Task<UserLoginResponse> LoginUserAsync(UserLoginCommand user);
        Task<CreateUserResponse> CreateUserAsync(CreateUserCommand user);
        
        Task<Response.Response<string>> DisableUserAsync(string Id);
        Task<Response.Response<string>> EnableUserAsync(string Id);
        Task<Response.Response<string>> ResetPassword(ResetPasswordCommand entity);
        Task<UsersResponse> GetUsersAsync();
        Task<UserResponse> GetUserAsync(string Id);

    }
}
