
using Identity.Application.Common.Models.Access;
using Identity.Application.Common.Models.Access.Login;

namespace Identity.Application.Common.Abstractions
{
    public interface IAccessService
    {
        Task<TokenResponse> LoginUserAsync(LoginCommand command);
        Task<TokenResponse> RefreshUserAsync(RefreshCommand command);
    }
}
