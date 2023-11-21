using ShoppingCart.Application.Common.Models.ProductRange;

namespace ShoppingCart.Application.Common.Abstractions
{
    public interface IProductRangeRepository
    {
        Task<Guid> CreateProductRangeAsync(CreateProductRangeCommand command);
        Task UpdateProductRageAsync(UpdateProductRangeCommand command);
        Task DeleteProductRageAsync(Guid productRangeId);
    }
}
