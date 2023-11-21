namespace ShoppingCart.Persistence
{
    internal class DbInitialize
    {
        internal static void Initialize(OrderDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
        }
    }
}
