using ProductCatalog.Application.Application.Models.Product;

namespace ProductCatalog.Application.Common.Interfaces
{
    public interface IProductRepository : IRepository
    {
        Task<Guid> CreateProductAsync(Guid productId);
        Task UpdateProductAsync(UpdateProductCommand productCommand);
        Task<ProductDetailsDto> GetProductDetailsAsync(Guid productId);
        Task<List<ProductLookupDto>> GetProductLookupsAsync();
        Task DeleteProductAsync(Guid productId);
    }
}
