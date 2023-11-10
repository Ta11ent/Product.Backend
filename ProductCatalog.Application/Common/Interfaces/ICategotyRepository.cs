using ProductCatalog.Application.Application.Models.Category;

namespace ProductCatalog.Application.Common.Interfaces
{
    public interface ICategotyRepository : IRepository
    {
        Task<Guid> CreateCategoryAsync(Guid categoryId);
        Task UpdateCategoryAsync(UpdateCategoryCommand categoryCommand);
        Task<CategoryDetailsDto> GetCategoryDetailsAsync(Guid categoryId);
        Task<List<CategoryLookupDto>> GetCategoryLookupsAsync();
        Task DeleteCategoryAsync(Guid categoryId);
    }
}
