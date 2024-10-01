using ProductCatalog.Application.Common.Pagination;
using ProductCatalog.Domain;
using System.Linq.Expressions;

namespace ProductCatalog.Application.Common.Abstractions
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductSale>> GetAllProductsAsync(
            Expression<Func<ProductSale, bool>> expression, 
            IPagination pagination, 
            CancellationToken cancellationToken);

        Task<ProductSale> GetProductByIdAsync(
            Guid productId,
            Expression<Func<ProductSale, bool>> expression,
            CancellationToken cancellationToken);

        Task CreateProductAsync(
            Product product,
            ProductSale productSale,
            Cost cost,
            CancellationToken cancellationToken);

        Task CreateProductCostAsync(Cost cost, CancellationToken cancellationToken);

        void DeleteProduct(ProductSale product);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
