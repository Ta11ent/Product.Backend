using Identity.Application.Common.Models.Access.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Common.Abstractions
{
    public interface IAccessService
    {
        Task<UserLoginResponse> LoginUserAsync(UserLoginCommand user);
    }
}
