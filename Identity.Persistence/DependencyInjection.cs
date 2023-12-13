using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            //services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<AuthDbContext>();

            services.AddIdentityCore<IdentityUser>(config => { config.User.RequireUniqueEmail = true; })
                .AddEntityFrameworkStores<AuthDbContext>();

            //builder.Services.AddIdentityCore<IdentityUser>(config => { config.User.RequireUniqueEmail = true; })
            //               .AddEntityFrameworkStores<AuthDbContext>()
            //               .AddApiEndpoints();
            return services;
        }
    }
}
