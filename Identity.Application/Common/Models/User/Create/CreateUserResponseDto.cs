using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Common.Models.User.Create
{
    public class CreateUserResponseDto
    {
        public string UserName { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}
