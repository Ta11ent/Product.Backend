using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Pagination;
using ProductCatalog.Domain;
using System.Linq.Expressions;

namespace ProductCatalog.Infrastructure.Repositories
{
    public class CashedProductRepository : IProductRepository
    {
        private readonly IProductRepository _decorated;
        private readonly ICashService _cashService;
        public CashedProductRepository(IProductRepository decorated, ICashService cashService)
        {
            _decorated = decorated;
            _cashService = cashService;
        }
        public async Task CreateProductAsync(Product product, ProductSale productSale, Cost cost, CancellationToken cancellationToken)
        {
            await _decorated.CreateProductAsync(product, productSale, cost, cancellationToken);
            await _cashService.CreateAsync<ProductSale>(
                productSale.ProductSaleId,
                productSale.Create(product, cost),
                null,
                cancellationToken);
        }
        public async Task CreateProductCostAsync(Cost cost, CancellationToken cancellationToken)
            => await _decorated.CreateProductCostAsync(cost, cancellationToken);

        public void DeleteProduct(ProductSale product)
            => _decorated.DeleteProduct(product);

        public async Task<IEnumerable<ProductSale>> GetAllProductsAsync(
            Expression<Func<ProductSale, bool>> expression,
            IPagination pagination,
            CancellationToken cancellationToken) 
            => await _decorated.GetAllProductsAsync(expression, pagination, cancellationToken);


        public async Task<ProductSale> GetProductByIdAsync(
            Guid productId, Expression<Func<ProductSale, bool>> expression,
            CancellationToken cancellationToken)
            => await _cashService.GetByIdAsync<ProductSale>(
                productId,
                async() => await _decorated.GetProductByIdAsync(productId, expression, cancellationToken),
                null,
                cancellationToken);

        public Task SaveChangesAsync(CancellationToken cancellationToken)
            => _decorated.SaveChangesAsync(cancellationToken);
    }
}
