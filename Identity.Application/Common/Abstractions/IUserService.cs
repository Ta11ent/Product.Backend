using Identity.Application.Common.Models.User.Create;
using Identity.Application.Common.Models.User.Password;
using Identity.Application.Common.Models.User.Get;
using Identity.Application.Common.Response;

namespace Identity.Application.Common.Abstractions
{
    public interface IUserService : IDisposable
    {
        Task<CreateUserResponse> CreateUserAsync(CreateUserCommand user);
        Task<Response<string>> DisableUserAsync(string id);
        Task<Response<string>> EnableUserAsync(string id);
        Task<Response<string>> ResetPassword(ResetPasswordCommand entity);
        Task<UsersResponse> GetUsersAsync();
        Task<UserResponse> GetUserAsync(string id);
    }
}
