using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Application.Common.Pagination;
using ProductCatalog.Domain;
using System.Linq.Expressions;

namespace ProductCatalog.Infrastructure.Repositories
{
    public sealed class ProductRepository : BaseRepository, IProductRepository
    { 
        public ProductRepository(IProductDbContext dbContext) : base(dbContext) { }

        public async Task CreateProductAsync(
            Product product,
            ProductSale productSale,
            Cost cost,
            CancellationToken cancellationToken)
        {
            var tasks = new Task[3]
            {
                new Task(() =>_dbContext.Products.AddAsync(product, cancellationToken)),
                new Task(() => _dbContext.ProductSale.AddAsync(productSale, cancellationToken)),
                new Task(() => _dbContext.Costs.AddAsync(cost, cancellationToken))
            };

            try
            {
                foreach (Task task in tasks) task.Start();
                await Task.WhenAll(tasks);
            }
            catch (Exception ex) {
                throw new Exception("An error occured while creating the product", ex); 
            }
        }

        public async Task<IEnumerable<ProductSale>> GetAllProductsAsync(
            Expression<Func<ProductSale, bool>> expression,
            IPagination pagination, 
            CancellationToken cancellationToken)
        {
            var products = await
                _dbContext.ProductSale
                .Include(x => x.Product)
                    .ThenInclude(x => x.Manufacturer)
                .Include(x => x.Costs.OrderByDescending(y => y.DatePrice).Take(1))
                    .ThenInclude(x => x.Currency)
                        .ThenInclude(x => x.ROEs
                            .OrderByDescending(x => x.DateFrom).Take(1))
                .Where(expression)
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .OrderBy(x => x.Product.Name)
                .ToListAsync(cancellationToken);

            return products;
        }

        public async Task<ProductSale> GetProductByIdAsync(
            Guid productId,
            Expression<Func<ProductSale, bool>> expression,
            CancellationToken cancellationToken)
        {
            var product = await
                _dbContext.ProductSale
                .Include(x => x.SubCategory)
                    .ThenInclude(x => x.Category)
                .Include(x => x.Costs
                    .OrderByDescending(y => y.DatePrice).Take(1))
                    .ThenInclude(x => x.Currency)
                        .ThenInclude(x => x.ROEs
                            .OrderByDescending(x => x.DateFrom).Take(1))
                .Include(x => x.Product)
                    .ThenInclude(x => x.Manufacturer)
            .Where(expression)
            .FirstOrDefaultAsync(cancellationToken);

            return product!;
        }

        public async Task CreateProductCostAsync(Cost cost, CancellationToken cancellationToken) 
            => await _dbContext.Costs.AddAsync(cost, cancellationToken);

        public void DeleteProduct(ProductSale product) => product.Update(product.SubCategoryId, false);

       
    }
}
