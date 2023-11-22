using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Application.Common.Exceptions;
using ShoppingCart.Application.Common.Models.ProductRange;
using ShoppingCart.Domain;

namespace ShoppingCart.Application.Application
{
    public class ProductRangeRepository : IProductRangeRepository
    {
        private readonly IOrderDbContext _dbContext;

        private bool _disposed = false;
        public ProductRangeRepository(IOrderDbContext dbContext, IMapper mapper) =>
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task<Guid> CreateProductRangeAsync(CreateProductRangeCommand command)
        {
            var productRange = new ProductRange
            {
                ProductRangeId = Guid.NewGuid(),
                ProductId = command.ProductId,
                OrderId = command.OrderId,
                Count = command.Count
            };

            await _dbContext.ProductRanges.AddAsync(productRange);
            await _dbContext.SaveChangesAsync();

            return productRange.ProductRangeId;
        }

        public async Task UpdateProductRageAsync(UpdateProductRangeCommand command)
        {
            var productRange =
                await _dbContext.ProductRanges.FirstOrDefaultAsync(x => x.ProductRangeId == command.ProductRangeId);

            if (productRange is null)
                throw new NotFoundException(nameof(ProductRange), command.ProductRangeId);

            productRange.Count = command.Count;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductRageAsync(Guid productRangeId)
        {
            var productRange = 
                await _dbContext.ProductRanges.FindAsync(new object[] { productRangeId });

            if (productRange is null)
                throw new NotFoundException(nameof(ProductRange), productRangeId);

            _dbContext.ProductRanges.Remove(productRange);
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing) _dbContext.Dispose();
            }
            _disposed = true;
        }
    }
}

