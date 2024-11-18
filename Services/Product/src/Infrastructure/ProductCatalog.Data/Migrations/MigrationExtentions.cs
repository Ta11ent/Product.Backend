using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Data;

namespace ProductCatalog.Persistence.Migrations
{
    public static class MigrationExtentions
    {
        public static void ApplyMigration(this IApplicationBuilder app)
        {
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
                using ProductDbContext dbContext = 
                    scope.ServiceProvider.GetRequiredService<ProductDbContext>();
                dbContext.Database.Migrate();
            }
        }
    }
}
