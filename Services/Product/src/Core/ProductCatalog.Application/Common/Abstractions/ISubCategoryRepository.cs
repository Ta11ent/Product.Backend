using ProductCatalog.Application.Common.Pagination;
using ProductCatalog.Domain;

namespace ProductCatalog.Application.Common.Abstractions
{
    public interface ISubCategoryRepository
    {
        Task<SubCategory> GetSubCategoryByIdAsync(Guid categoryId, Guid subcategoryId, CancellationToken cancellationToken);
        Task<IEnumerable<SubCategory>> GetAllSubCategoriesAsync(Guid categoryId, IPagination pagination, CancellationToken cancellationToken);
        Task CreateSubCategoryAsync(SubCategory subcategory, CancellationToken cancellationToken);
        void DeleteSubCategory(SubCategory subcategory);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
