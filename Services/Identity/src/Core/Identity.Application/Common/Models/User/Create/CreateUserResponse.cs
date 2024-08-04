using Identity.Application.Common.Response;

namespace Identity.Application.Common.Models.User.Create
{
    public class CreateUserResponse : Response<CreateUserResponseDto>
    {
        public CreateUserResponse(CreateUserResponseDto user) : base(user) { }
    }
}
