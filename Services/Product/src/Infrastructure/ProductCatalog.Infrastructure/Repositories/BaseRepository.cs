using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Infrastructure.Repositories
{
    public class BaseRepository : IDisposable
    {
        public readonly IProductDbContext _dbContext;
        private bool _disposed = false;
        protected BaseRepository(IProductDbContext dbContext) 
            => _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        public async Task SaveChangesAsync(CancellationToken cancellationToken) => await _dbContext.SaveChangesAsync(cancellationToken);
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
