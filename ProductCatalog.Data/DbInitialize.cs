namespace ProductCatalog.Data
{
    public class DbInitialize
    {
        public static void Initialize(ProductDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
        }
    }
}
