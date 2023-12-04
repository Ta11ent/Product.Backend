using ShoppingCart.Application.Common.Models.Product;

namespace ShoppingCart.Application.Common.Abstractions
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto> GetProductByIdAsync(Guid id);
    }
}
