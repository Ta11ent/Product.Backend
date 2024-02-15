using Identity.Domain;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Common.Abstractions
{
    public interface IAuthDbContext : IDisposable
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUserRole> AppUserRole { get; set; }
        public DbSet<AppUserToken> AppUserTokens { get; set; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
