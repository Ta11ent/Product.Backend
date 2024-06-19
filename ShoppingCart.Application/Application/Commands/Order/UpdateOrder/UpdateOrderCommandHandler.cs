using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Application.Common.Exceptions;

namespace ShoppingCart.Application.Application.Commands.Order.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderDbContext _dbContext;
        public UpdateOrderCommandHandler(IOrderDbContext orderDbContext)=>
            _dbContext = orderDbContext ?? throw new ArgumentNullException("OrderDbContext");

        public async Task Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var data = await _dbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == command.OrderId);
            if(data is null)
                throw new NotFoundException(nameof(Order), command.OrderId!);

            if (command.IsPaid)
            {
                data.IsPaid = command.IsPaid;
                data.OrderTime = DateTime.Now;

                await _dbContext.SaveChangesAsync(cancellationToken);
            }


        }
    }
}
