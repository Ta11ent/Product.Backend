using Microsoft.AspNetCore.Identity;

namespace Identity.Domain
{
    public class AppRole : IdentityRole
    {
        public ICollection<AppUserRole> AppUserRoles { get; set; }
    }
}
