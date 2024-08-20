using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Application.Common.Pagination;
using ProductCatalog.Domain;

namespace ProductCatalog.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository, IDisposable
    {
        private readonly IProductDbContext _dbContext;
        private bool _disposed = false;
        public CategoryRepository(IProductDbContext dbContext) => _dbContext = dbContext;

        public async Task<Category> GetCategoryByIdAsync(Guid categoryId, CancellationToken cancellationToken)
        {
            var data = 
                await _dbContext.Categories
                    .FirstOrDefaultAsync(x => x.CategoryId == categoryId, cancellationToken);
            return data!;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(IPagination pagination, CancellationToken cancellationToken)
        {
            var data =
                await _dbContext.Categories
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync(cancellationToken);
            return data;
        }

        public async Task CreateCategoryAsync(Category category, CancellationToken cancellationToken)
            =>await _dbContext.Categories.AddAsync(category);
        public void DeleteCategory(Category category)
            => _dbContext.Categories.Remove(category);
        public async Task SaveChangesAsync(CancellationToken cancellationToken) 
            => await _dbContext.SaveChangesAsync(cancellationToken);

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
