using Microsoft.AspNetCore.Identity;

namespace Identity.Domain
{
    public class AppRole : IdentityRole<string>
    {
        public List<AppUser> Users { get; set; } = new();
    }
}
