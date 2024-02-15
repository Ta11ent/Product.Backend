namespace Identity.Application.Common.Models.Access.Login
{
    public class LogInDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExp { get; set; }
    }
}
