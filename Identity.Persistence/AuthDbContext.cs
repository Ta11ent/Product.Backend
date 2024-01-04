using Identity.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Persistence
{
    public class AuthDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // builder.Entity<AppUser>(entity => entity.ToTable(name: "Users"));

            base.OnModelCreating(builder);
        }
    }
}
