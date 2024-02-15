using Identity.Application.Common.Models.Access.Login;

namespace Identity.Application.Common.Abstractions
{
    public interface IAccessService
    {
        Task<LoginResponse> LoginUserAsync(LoginCommand command);
    }
}
