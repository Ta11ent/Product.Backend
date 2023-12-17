using Microsoft.AspNetCore.Identity;

namespace Identity.Persistence
{
    public static class DbInitializer
    {
        public static void Initialize(AuthDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
