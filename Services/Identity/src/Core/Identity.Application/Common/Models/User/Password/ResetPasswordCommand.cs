namespace Identity.Application.Common.Models.User.Password
{
    public class ResetPasswordCommand
    {
        public string Id { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
