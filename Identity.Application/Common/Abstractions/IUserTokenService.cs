using Identity.Application.Common.Models.User.Get;

namespace Identity.Application.Common.Abstractions
{
    public interface IUserTokenService : IDisposable
    {
        Task SetUserTokenAsync(string userId, string loginProvider, string tokenName, string? tokenValue, DateTime expDate);
        Task<UserTokenResponse> GetUserTokenAsync(string userId, string loginProvider, string tokenName);
        Task<UserTokenResponse> GetUserTokenAsync(string token);
        Task RemoveUserTokenAsync(string id);
    }
}
