using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Application.Common.Exceptions;

namespace ShoppingCart.Application.Application.Commands.ProductRange.UpdateProductRange
{
    public class UpdateOrderItemCommandHandler : IRequestHandler<UpdateOrderItemCommand>
    {
        private readonly IOrderDbContext _dbContext;
        public UpdateOrderItemCommandHandler(IOrderDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException("OrderDbContext");

        public async Task Handle(UpdateOrderItemCommand command, CancellationToken cancellationToken)
        {
            var orderItem =
                await _dbContext.OrderItems.FirstOrDefaultAsync(x => x.OrderItemId == command.OrderItemId, cancellationToken);

            if (orderItem is null)
                throw new NotFoundException(nameof(orderItem), command.OrderItemId);

            orderItem.Count = command.Count;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
