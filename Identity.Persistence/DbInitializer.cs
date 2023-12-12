namespace Identity.Persistence
{
    internal static class DbInitializer
    {
        internal static void Initializee(AuthDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
