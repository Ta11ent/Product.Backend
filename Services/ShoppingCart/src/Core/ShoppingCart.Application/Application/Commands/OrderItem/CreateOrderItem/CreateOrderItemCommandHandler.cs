using MassTransit.Internals;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Application.Common.Helpers;

namespace ShoppingCart.Application.Application.Commands.ProductRange.CreateProductRange
{
    public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, Guid>
    {
        private readonly IOrderDbContext _dbContext;
        public CreateOrderItemCommandHandler(IOrderDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException("OrderDbContext");

        public async Task<Guid> Handle(CreateOrderItemCommand command, CancellationToken cancellationToken)
        {
            var order =
                await _dbContext.Orders
                  .Include(x => x.Statuses.OrderByDescending(x => x.StatusDate))
                  .Include(x => x.OrderItems)
                  .OrderByDescending(x => x.Number)
                  .FirstOrDefaultAsync(x => x.UserId == command.UserId, cancellationToken);

            if (order == null || order.Statuses.FirstOrDefault()!.TypeOfStatus != Status.InProgress){
                order = new Domain.Order()
                {
                    OrderId = Guid.NewGuid(),
                    UserId = command.UserId
                };

                var orderTask = _dbContext.Orders.AddAsync(order, cancellationToken).AsTask();
                var orderStatusTask = _dbContext.Statuses.AddAsync(new Domain.Status()
                {
                    StatusId = Guid.NewGuid(),
                    OrderId = order.OrderId,
                    TypeOfStatus = Status.InProgress,
                    StatusDate = DateTime.Now
                }, cancellationToken).AsTask();

                 Task.WhenAll(orderTask, orderStatusTask);
            }

            if (order.OrderItems.Any(x => x.ProductId == command.ProductId))
            {
                var orderItem = order.OrderItems!.FirstOrDefault(x => x.ProductId == command.ProductId);
                orderItem!.Count += command.Count;
            }
            else
            {
                await _dbContext.OrderItems.AddAsync(new Domain.OrderItem()
                {
                    OrderItemId = Guid.NewGuid(),
                    ProductId = command.ProductId,
                    Count = command.Count,
                    OrderId = order.OrderId
                }, cancellationToken);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return order!.OrderId;
        }
    }
}
