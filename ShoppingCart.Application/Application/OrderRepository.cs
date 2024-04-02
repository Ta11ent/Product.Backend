using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Application.Common.Exceptions;
using ShoppingCart.Application.Common.Helpers;
using ShoppingCart.Application.Common.Models.Order;
using ShoppingCart.Application.Common.Models.Product;
using ShoppingCart.Application.Common.Models.ProductRange;
using ShoppingCart.Application.Common.Predicate;
using ShoppingCart.Domain;

namespace ShoppingCart.Application.Application
{
    public class OrderRepository : IOrderReppository
    {
        private readonly IOrderDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        private bool _disposed = false;

        public OrderRepository(IOrderDbContext dbContext, IMapper mapper, IProductService productService)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
            _productService = productService;
        }

        public async Task<Guid> CreateOrderAsync(Guid userId)
        {
            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                UserId = userId,
                IsPaid = false
            };

            await _dbContext.Orders.AddAsync(order);

            return order.OrderId;
        }

        public async Task UpdateOrderAsync(UpdateOrderCommand command)
        {
            var order = 
                await _dbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == command.OrderId);

            if (order is null)
                throw new NotFoundException(nameof(Order), command.OrderId);

            order.IsPaid = command.IsPaid;
            if (command.IsPaid)
                order.OrderTime = DateTime.Now;
        }

        public async Task DeleteOrderAsync(Guid OrderId)
        {
            var order =
                await _dbContext.Orders.FindAsync(new object[] { OrderId });
            if (order is null)
                throw new NotFoundException(nameof(Order), OrderId);

            _dbContext.Orders.Remove(order);
        }

        public async Task<OrderDetailsResponse> GetOrderDetailsAsync(Guid OrderId)
        {
            var data =
                await _dbContext.Orders
                .Include(x => x.ProductRanges)
                .ProjectTo<OrderDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.OrderId == OrderId);

            var ProductIds = data!.ProductRanges.Select(x => x.ProductId).Distinct().ToList();

            var productDetails = await _productService
                .GetProductsAsync(QueryBuilder.ConvertToIdString(ProductIds, nameof(ProductDto.ProductId))
                    +QueryBuilder.GeneratePaginationParam(ProductIds.Count));

            foreach (var product in data.ProductRanges)
                AddProductDetails(product, ref productDetails);

            return new OrderDetailsResponse(data!);
        }

        public async Task<OrderListResponse> GetOrderListAsync(OrderListQuery query)
        {
            var predicate = PredicateBuilder.True<Order>();
            var data =
                   await _dbContext.Orders
                   .Include(x => x.ProductRanges)
                   .Where(predicate
                    .And(x => x.OrderTime >= query.DateFrom, query.DateFrom)
                    .And(x => x.OrderTime <= query.DateTo, query.DateTo)
                    .And(x => x.UserId == query.UserId, query.UserId)
                    .And(x => x.IsPaid == query.IsPaid, query.IsPaid))
                   .Skip((query.Page - 1) * query.PageSize)
                   .Take(query.PageSize)
                   .ProjectTo<OrderListDto>(_mapper.ConfigurationProvider)
                   .ToListAsync();


            var ProductIds =  data.SelectMany(x => x.ProductRanges.Select(x => x.ProductId)).Distinct().ToList();
           
            var productDetails = await _productService
                .GetProductsAsync(QueryBuilder.ConvertToIdString(ProductIds, nameof(ProductDto.ProductId))
                    + QueryBuilder.GeneratePaginationParam(ProductIds.Count));

            foreach (var item in data)
                foreach (var product in item.ProductRanges)
                    AddProductDetails(product, ref productDetails);

            //data.SelectMany(x => x.ProductRanges
            //    .Select(y => AddProductDetails(y, ref productDetails)));

            return new OrderListResponse(data, query);
        }

        public async Task SaveAsync() => await _dbContext.SaveChangesAsync();

        private ProductRangeDetailsDto AddProductDetails(ProductRangeDetailsDto product,
            ref IEnumerable<ProductDto> productDetails)
        {
            var pr = productDetails.First(x => x.ProductId == product.ProductId);
            product.Description = pr.Description;
            product.Name = pr.Name;
            product.Available = pr.Available;
            return product;
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
        ~OrderRepository() => Dispose(false);
    }
}
