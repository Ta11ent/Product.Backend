using ShoppingCart.Application.Common.Models.Product;

namespace ShoppingCart.Application.Common.Abstractions
{
    internal interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
