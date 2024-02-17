using Identity.Application.Common.Models.User.Create;
using Identity.Application.Common.Models.User.Password;
using Identity.Application.Common.Models.User.Get;
using Identity.Application.Common.Response;
using Identity.Application.Common.Models.Access.Login;

namespace Identity.Application.Common.Abstractions
{
    public interface IUserService : IDisposable
    {
        Task<CreateUserResponse> CreateUserAsync(CreateUserCommand command);
        Task<Response<string>> DisableUserAsync(string id);
        Task<Response<string>> EnableUserAsync(string id);
        Task<Response<string>> ResetPasswordAsync(ResetPasswordCommand command);
        Task<UsersResponse> GetUsersAsync();
        Task<UserResponse> GetUserByIdAsync(string id);
        Task<UserResponse> GetUserByNameAsync(string name);
        Task<bool> CheckPasswordAsync(CheckPasswordCommand command);
        Task SetUserTokenAsync(string userId, string loginProvider, string tokenName, string? tokenValue, DateTime expDate);
        Task<UserTokenResponse> GetUserTokenAsync(string userId, string loginProvider, string tokenName);
        
    }
}
