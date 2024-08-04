using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Identity.Domain;
using Identity.Application.Common.Abstractions;
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

            services.AddScoped<IAuthDbContext, AuthDbContext>();
            services.AddIdentity<AppUser, AppRole>(config => { config.User.RequireUniqueEmail = true; })
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();
            
            return services;
        }
    }
}
