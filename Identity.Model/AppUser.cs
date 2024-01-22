using Microsoft.AspNetCore.Identity;

namespace Identity.Domain
{
    public class AppUser : IdentityUser
    {
        public bool Enabled { get; set; }
        public ICollection<AppUserRole> AppUserRoles { get; set; }
    }
}
