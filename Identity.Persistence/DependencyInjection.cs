using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Identity.Domain;

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

            services.AddIdentity<AppUser, AppRole>(config => { config.User.RequireUniqueEmail = true; })
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<AuthDbContext>();
            
            return services;
        }
    }
}
