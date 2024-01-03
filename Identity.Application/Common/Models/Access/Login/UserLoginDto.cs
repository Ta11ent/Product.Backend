namespace Identity.Application.Common.Models.Access.Login
{
    public class UserLoginDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpTime { get; set; }
    }
}
