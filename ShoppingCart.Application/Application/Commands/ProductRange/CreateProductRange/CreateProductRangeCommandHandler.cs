using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Common.Abstractions;

namespace ShoppingCart.Application.Application.Commands.ProductRange.CreateProductRange
{
    public class CreateProductRangeCommandHandler : IRequestHandler<CreateProductRangeCommand, Guid>
    {
        private readonly IOrderDbContext _dbContext;
        public CreateProductRangeCommandHandler(IOrderDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException("OrderDbContext");

        public async Task<Guid> Handle(CreateProductRangeCommand command, CancellationToken cancellationToken)
        {
            var order = _dbContext.Orders
                .FirstOrDefault(x => x.UserId == command.UserId && x.IsPaid == false);

            if (order is null) {
                order = new Domain.Order
                {
                    OrderId = Guid.NewGuid(),
                    UserId = command.UserId,
                    IsPaid = false
                };
                await _dbContext.Orders.AddAsync(order);
            }

            Domain.ProductRange productRange = _dbContext.ProductRanges
                    .FirstOrDefaultAsync(x => x.OrderId == order.OrderId
                                        && x.ProductId == command.ProductId).Result!;

            if (productRange is null)
            {
                productRange = new Domain.ProductRange
                {
                    ProductRangeId = Guid.NewGuid(),
                    ProductId = command.ProductId,
                    OrderId = order.OrderId,
                    Count = command.Count
                };
                await _dbContext.ProductRanges.AddAsync(productRange);
            }
            else
            {
                productRange.Count = command.Count;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return productRange.ProductRangeId;
        }
    }
}
