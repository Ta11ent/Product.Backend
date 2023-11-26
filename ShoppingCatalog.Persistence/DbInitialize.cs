namespace ShoppingCart.Persistence
{
    public class DbInitialize
    {
        public static void Initialize(OrderDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
        }
    }
}
