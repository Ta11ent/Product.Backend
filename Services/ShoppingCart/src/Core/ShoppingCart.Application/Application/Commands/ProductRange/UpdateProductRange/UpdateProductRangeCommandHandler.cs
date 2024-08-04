using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Application.Common.Exceptions;

namespace ShoppingCart.Application.Application.Commands.ProductRange.UpdateProductRange
{
    public class UpdateProductRangeCommandHandler : IRequestHandler<UpdateProductRangeCommand>
    {
        private readonly IOrderDbContext _dbContext;
        public UpdateProductRangeCommandHandler(IOrderDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException("OrderDbContext");

        public async Task Handle(UpdateProductRangeCommand command, CancellationToken cancellationToken)
        {
            var productRange =
                await _dbContext.ProductRanges.FirstOrDefaultAsync(x => x.ProductRangeId == command.ProductRangeId);

            if (productRange is null)
                throw new NotFoundException(nameof(ProductRange), command.ProductRangeId);

            productRange.Count = command.Count;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
