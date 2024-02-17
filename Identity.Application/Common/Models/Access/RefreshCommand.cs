namespace Identity.Application.Common.Models.Access
{
    public class RefreshCommand
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get;set; } = string.Empty;
    }
}
