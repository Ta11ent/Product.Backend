using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Application.Common.Exceptions;
using ShoppingCart.Application.Common.Helpers;

namespace ShoppingCart.Application.Application.Commands.Order.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, bool>
    {
        private readonly IOrderDbContext _dbContext;
        public UpdateOrderCommandHandler(IOrderDbContext orderDbContext) =>
            _dbContext = orderDbContext ?? throw new ArgumentNullException("OrderDbContext");

        public async Task<bool> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            if (command.Status == Status.InProgress) return false;

            var data =
                await _dbContext.Orders
                .Include(x => x.Statuses)
                .FirstOrDefaultAsync(x => x.OrderId == command.OrderId);
            if (data is null)
                throw new NotFoundException(nameof(Order), command.OrderId!);

            switch (data.Statuses.LastOrDefault()!.TypeOfStatus, command.Status)
            {
                case (Status.InProgress, Status.Refund):
                    throw new InvalidOperationException("It is impossible to return an unpaid Order");
                case (Status.Paid, Status.Canceled):
                    throw new InvalidOperationException("It is impossible to cancel the paid order, it is necessary to issue a refund");
                case (Status.Canceled, Status.Paid):
                    throw new InvalidOperationException("it is not possible to pay for a cancelled Order");
                case (Status.Canceled, Status.Refund):
                    throw new InvalidOperationException("it is not possible to refound for a cancelled Order");
            }

            await _dbContext.Statuses.AddAsync(new Domain.Status()
            {
                StatusId = Guid.NewGuid(),
                OrderId = data.OrderId,
                StatusDate = DateTime.Now,
                TypeOfStatus = command.Status
            });

            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
