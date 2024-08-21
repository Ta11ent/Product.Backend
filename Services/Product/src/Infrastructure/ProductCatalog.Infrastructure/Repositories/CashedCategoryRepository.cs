using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Pagination;
using ProductCatalog.Domain;

namespace ProductCatalog.Infrastructure.Repositories
{
    public class CashedCategoryRepository : ICategoryRepository
    {
        private readonly ICategoryRepository _decorated;
        private readonly ICashService _cashService;
        public CashedCategoryRepository(ICategoryRepository decorated, ICashService cashService)
            => (_decorated, _cashService) = (decorated, cashService);
        public async Task CreateCategoryAsync(Category category, CancellationToken cancellationToken)
        {
            await _decorated.CreateCategoryAsync(category, cancellationToken);
            await _cashService.CreateAsync(
                category.CategoryId,
                category,
                null,
                cancellationToken);
        }
        public async void DeleteCategory(Category category)
        {
            _decorated.DeleteCategory(category);
            await _cashService.DeleteByIdAsync<Category>(category.CategoryId, new CancellationTokenSource().Token);
        }
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(IPagination pagination, CancellationToken cancellationToken)
            => await _decorated.GetAllCategoriesAsync(pagination, cancellationToken);
        public async Task<Category> GetCategoryByIdAsync(Guid categoryId, CancellationToken cancellationToken)
            => await _cashService.GetByIdAsync(
                categoryId,
                async () => await _decorated.GetCategoryByIdAsync(categoryId, cancellationToken),
                null,
                cancellationToken);

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
            => await _decorated.SaveChangesAsync(cancellationToken);
    }
}
