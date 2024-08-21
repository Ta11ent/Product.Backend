using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Pagination;
using ProductCatalog.Domain;

namespace ProductCatalog.Infrastructure.Repositories
{
    public class CashedSubCategoryRepository : ISubCategoryRepository
    {
        private readonly ISubCategoryRepository _decorated;
        private readonly ICashService _cashService;
        public CashedSubCategoryRepository(ISubCategoryRepository decorated, ICashService cashService)
        {
            _decorated = decorated;
            _cashService = cashService;
        }

        public async Task CreateSubCategoryAsync(SubCategory subcategory, CancellationToken cancellationToken)
        {
            await _decorated.CreateSubCategoryAsync(subcategory, cancellationToken);
            await _cashService.CreateAsync<SubCategory>(
                subcategory.SubCategoryId, 
                subcategory,
                null, 
                cancellationToken);
        }

        public async void DeleteSubCategory(SubCategory subcategory)
        {
            _decorated.DeleteSubCategory(subcategory);
            await _cashService.DeleteByIdAsync<SubCategory>(subcategory.SubCategoryId, new CancellationTokenSource().Token);
        }
        public async Task<IEnumerable<SubCategory>> GetAllSubCategoriesAsync(Guid categoryId, IPagination pagination, CancellationToken cancellationToken)
            => await _decorated.GetAllSubCategoriesAsync(categoryId, pagination, cancellationToken);

        public async Task<SubCategory> GetSubCategoryByIdAsync(Guid categoryId, Guid subcategoryId, CancellationToken cancellationToken)
            => await _cashService.GetByIdAsync<SubCategory>(
                subcategoryId,
                async() => await _decorated.GetSubCategoryByIdAsync(categoryId, subcategoryId, cancellationToken),
                null,
                cancellationToken);

        public Task SaveChangesAsync(CancellationToken cancellationToken) 
            => _decorated.SaveChangesAsync(cancellationToken);
    }
}
