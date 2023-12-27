namespace Identity.Application.Common.Models.Access.Login
{
    public class UserLoginResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
