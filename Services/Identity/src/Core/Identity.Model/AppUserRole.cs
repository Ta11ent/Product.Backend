using Microsoft.AspNetCore.Identity;

namespace Identity.Domain
{
    public class AppUserRole : IdentityUserRole<string>
    {
        public AppUser AppUser { get; set; }
        public AppRole AppRole { get; set; }
    }
}
