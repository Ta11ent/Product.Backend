using MediatR;
using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Application.Common.Exceptions;

namespace ShoppingCart.Application.Application.Commands.Order.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderDbContext _dbContext;
        public DeleteOrderCommandHandler(IOrderDbContext dbContext)=>
            _dbContext = dbContext ?? throw new ArgumentNullException("OrderDbContext");

        public async Task Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var data = await
                _dbContext.Orders
                .FindAsync(new object[] { command.OrderId }, cancellationToken);

            if (data is null)
                throw new NotFoundException(nameof(Order), command.OrderId);

            _dbContext.Orders.Remove(data);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
