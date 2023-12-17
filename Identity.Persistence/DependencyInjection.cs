using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Identity.Domain;
using Microsoft.AspNetCore.Identity;

namespace Identity.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, 
            IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<AuthDbContext>(config =>
            {
                config.UseSqlServer(connectionString);
            });

            services.AddIdentityCore<AppUser>(config => { config.User.RequireUniqueEmail = true; })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>();

                return services;
        }
    }
}
