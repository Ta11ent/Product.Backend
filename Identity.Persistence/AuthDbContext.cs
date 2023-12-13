using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Persistence
{
    public class AuthDbContext : IdentityDbContext
    {
      //  public DbSet<IdentityUser> AppUsers { get; set; }
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options) { }
    }
}
