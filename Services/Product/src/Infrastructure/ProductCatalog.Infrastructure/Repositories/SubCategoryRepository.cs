using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Application.Common.Pagination;
using ProductCatalog.Domain;

namespace ProductCatalog.Infrastructure.Repositories
{
    public class SubCategoryRepository : BaseRepository, ISubCategoryRepository
    {
        public SubCategoryRepository(IProductDbContext dbContext) : base(dbContext) { }

        public async Task CreateSubCategoryAsync(SubCategory subcategory, CancellationToken cancellationToken)
            => await _dbContext.SubCategories.AddAsync(subcategory, cancellationToken);
        public void DeleteSubCategory(SubCategory subcategory)
            => _dbContext.SubCategories.Remove(subcategory);

        public async Task<IEnumerable<SubCategory>> GetAllSubCategoriesAsync(Guid categoryId, IPagination pagination, CancellationToken cancellationToken)
        {
            var data =
                 await _dbContext.SubCategories
                     .Where(x => x.CategoryId == categoryId)
                     .Skip((pagination.Page - 1) * pagination.PageSize)
                     .Take(pagination.PageSize)
                     .OrderBy(x => x.Name)
                     .ToListAsync(cancellationToken);
            return data;
        }
        public async Task<SubCategory> GetSubCategoryByIdAsync(Guid categoryId, Guid subcategoryId, CancellationToken cancellationToken)
        {
            var data = await _dbContext.SubCategories
                  .Include(x => x.Category)
                  .Where(x => x.CategoryId == categoryId
                      && x.SubCategoryId == subcategoryId)
                  .AsNoTracking()
                  .FirstOrDefaultAsync(cancellationToken);
            return data!;
        }
    }
}
