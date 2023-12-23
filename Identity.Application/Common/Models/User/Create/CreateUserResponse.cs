using Identity.Application.Common.Response;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Common.Models.User.Create
{
    public class CreateUserResponse : Response<CreateUserResponseDto>
    {
        public CreateUserResponse(CreateUserResponseDto user, IEnumerable<IdentityError> errors) : base(user, errors) { }
    }
}
