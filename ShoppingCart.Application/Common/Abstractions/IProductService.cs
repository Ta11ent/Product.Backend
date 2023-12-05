using ShoppingCart.Application.Common.Models.Product;

namespace ShoppingCart.Application.Common.Abstractions
{
    public interface IProductService
    {
        Task<ProductDto> GetProductByIdAsync(Guid id);
    }
}
