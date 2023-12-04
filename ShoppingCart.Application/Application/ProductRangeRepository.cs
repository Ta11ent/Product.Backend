using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IOrderReppository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        private bool _disposed = false;
        public ProductRangeRepository(IOrderDbContext dbContext,
            IOrderReppository orderRepository, IMapper mapper, IProductService productService) {

            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _orderRepository = orderRepository;
            _mapper = mapper;
            _productService = productService;
        }

        public async Task<ProductRangeDetailsResponse> GetProductRangeAsync(Guid Id)
        {
            var productRange = 
                await _dbContext.ProductRanges
                .ProjectTo<ProductRangeDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.ProductRangeId == Id);

            if (productRange is null)
                throw new NotFoundException(nameof(productRange), Id);
            else
            {
                var productDetails =
                    await _productService.GetProductByIdAsync(Id);

                productRange.Name = productDetails.Name;
                productRange.Description = productDetails.Description;
            }

            return new ProductRangeDetailsResponse(productRange);
        }

        public async Task<Guid> CreateProductRangeAsync(CreateProductRangeCommand command)
        {
            var order = _dbContext.Orders
                .FirstOrDefault(x => x.UserId == command.UserId && x.IsPaid == false);

            Guid orderId = order is null
                ? await _orderRepository.CreateOrderAsync(command.UserId)
                : order.OrderId;

            ProductRange productRange = _dbContext.ProductRanges
                    .FirstOrDefaultAsync(x => x.OrderId == orderId && x.ProductId == command.ProductId).Result!;

            if(productRange is null)
            {
                productRange = new ProductRange
                {
                    ProductRangeId = Guid.NewGuid(),
                    ProductId = command.ProductId,
                    OrderId = orderId,
                    Count = command.Count
                };
                await _dbContext.ProductRanges.AddAsync(productRange);
                return productRange.ProductRangeId;
            }
            else
            {
                await UpdateProductRageAsync(new UpdateProductRangeCommand
                {
                    ProductRangeId = productRange.ProductRangeId,
                    Count = productRange.Count + command.Count
                });
            }
            return productRange.ProductRangeId;
        }

        public async Task UpdateProductRageAsync(UpdateProductRangeCommand command)
        {
            var productRange =
                await _dbContext.ProductRanges.FirstOrDefaultAsync(x => x.ProductRangeId == command.ProductRangeId);

            if (productRange is null)
                throw new NotFoundException(nameof(ProductRange), command.ProductRangeId);

            productRange.Count = command.Count;
        }

        public async Task DeleteProductRageAsync(Guid productRangeId)
        {
            var productRange = 
                await _dbContext.ProductRanges.FindAsync(new object[] { productRangeId });

            if (productRange is null)
                throw new NotFoundException(nameof(ProductRange), productRangeId);

            _dbContext.ProductRanges.Remove(productRange);
        }
        public async Task SaveAsync() => await _dbContext.SaveChangesAsync();

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

        ~ProductRangeRepository() => Dispose(false);
    }
}

