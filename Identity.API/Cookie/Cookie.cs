namespace Identity.API.Cookies
{
    internal class Cookie
    {
        private readonly HttpContext context;
        internal Cookie(HttpContext context) => this.context = context;
        internal void SetRefreshToken(string token, DateTime expTime)
        {
            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                Expires = expTime
            };
            context.Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}
