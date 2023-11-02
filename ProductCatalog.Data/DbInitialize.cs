namespace ProductCatalog.Data
{
    internal class DbInitialize
    {
        internal static void Initialize(ProductDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
        }
    }
}
