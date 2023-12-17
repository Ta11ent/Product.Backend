using Microsoft.AspNetCore.Identity;

namespace Identity.Domain
{
    public class AppUser : IdentityUser<string>
    {
        public bool Enabled { get; set; }
    }
}
