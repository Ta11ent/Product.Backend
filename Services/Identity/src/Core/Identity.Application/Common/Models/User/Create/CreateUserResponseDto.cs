using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Common.Models.User.Create
{
    public class CreateUserResponseDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
    }
}
