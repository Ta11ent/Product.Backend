using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Application.Common.Exceptions;
using ShoppingCart.Application.Common.Models.Order;
using ShoppingCart.Domain;

namespace ShoppingCart.Application.Application
{
    public class OrderRepository : IOrderReppository
    {
        private readonly IOrderDbContext _dbContext;
        private readonly IMapper _mapper;

        private bool _disposed = false;

        public OrderRepository(IOrderDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
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
            await _dbContext.SaveChangesAsync();

            return order.OrderId;
        }

        public async Task UpdateOrderAsync(UpdateOrderCommand command)
        {
            var order = 
                await _dbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == command.OrderId);

            if (order is null)
                throw new NotFoundException(nameof(Order), command.OrderId);

            order.OrderTime = command.OrderTime;
            order.IsPaid = command.IsPaid;
 
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(Guid OrderId)
        {
            var order =
                await _dbContext.Orders.FindAsync(new object[] { OrderId });
            if (order is null)
                throw new NotFoundException(nameof(Order), OrderId);

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<OrderDetailsResponse> GetOrderDetailsAsync(Guid OrderId)
        {
            var data =
                await _dbContext.Orders
                .Include(x => x.ProductRanges)
                .ProjectTo<OrderDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.OrderId == OrderId);

            return new OrderDetailsResponse(data);
        }

        public Task<OrderListResponse> GetOrderListAsync(OrderListQuery query)
        {
            throw new NotImplementedException();
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
