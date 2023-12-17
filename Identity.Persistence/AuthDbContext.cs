using Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Persistence
{
    public class AuthDbContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           // builder.Entity<AppUser>(entity => entity.ToTable(name: "Users"));
            base.OnModelCreating(builder);
        }
    }
}
