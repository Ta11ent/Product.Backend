using ProductCatalog.Application.Common.Pagination;
using ProductCatalog.Domain;

namespace ProductCatalog.Application.Common.Abstractions
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryByIdAsync(Guid categoryId, CancellationToken cancellationToken);
        Task<IEnumerable<Category>> GetAllCategoriesAsync(IPagination pagination, CancellationToken cancellationToken);
        Task CreateCategoryAsync(Category category, CancellationToken cancellationToken);
        void DeleteCategory(Category category);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
