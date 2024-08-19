using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Application.Common.Exceptions;
using ShoppingCart.Application.Common.Helpers;

namespace ShoppingCart.Application.Application.Commands.ProductRange.DeleteProductRange
{
    public class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand>
    {
        private readonly IOrderDbContext _dbContext;
        public DeleteOrderItemCommandHandler(IOrderDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException("OrderDbContext");

        public async Task Handle(DeleteOrderItemCommand command, CancellationToken cancellationToken)
        {
            var order = 
                await _dbContext.Orders
                    .Include(x => x.OrderItems)
                    .Include(x => x.Statuses)
                    .FirstOrDefaultAsync(x => x.OrderItems.Any(y => y.OrderItemId == command.OrderItemId));

            if (order is null)
                throw new NotFoundException("OrderItem", command.OrderItemId);

            var lastOrderStatus = order.Statuses.LastOrDefault()!.TypeOfStatus;
            if (lastOrderStatus != Status.InProgress)
                throw new InvalidOperationException($"It is impossible to delete order item, because order was {lastOrderStatus}");

            if (order.OrderItems.Count() == 1)
                _dbContext.Orders.Remove(order);
            else
                _dbContext.OrderItems.Remove(order.OrderItems.FirstOrDefault(x => x.OrderItemId == command.OrderItemId)!);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
