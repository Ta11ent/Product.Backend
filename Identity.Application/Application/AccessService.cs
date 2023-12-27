using Identity.Application.Common.Abstractions;
using Identity.Application.Common.Models.Access.Login;

namespace Identity.Application.Application
{
    public class AccessService : IAccessService
    {
        public Task<UserLoginResponse> LoginUserAsync(UserLoginCommand user)
        {
            throw new NotImplementedException();
        }
    }
}
