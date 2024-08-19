using MediatR;
using ShoppingCart.Application.Common.Abstractions;

namespace ShoppingCart.Application.Application.Commands.Order.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderDbContext _dbContext;
        public CreateOrderCommandHandler(IOrderDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException("OrderDbContext");

        public async Task<Guid> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = new Domain.Order
            {
                OrderId = Guid.NewGuid(),
                UserId = command.UserId,
            };

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return order.OrderId;
        }
    }
}
