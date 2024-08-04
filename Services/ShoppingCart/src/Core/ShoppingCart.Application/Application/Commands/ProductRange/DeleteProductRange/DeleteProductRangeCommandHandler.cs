using MediatR;
using ShoppingCart.Application.Common.Abstractions;
using ShoppingCart.Application.Common.Exceptions;

namespace ShoppingCart.Application.Application.Commands.ProductRange.DeleteProductRange
{
    public class DeleteProductRangeCommandHandler : IRequestHandler<DeleteProductRangeCommand>
    {
        private readonly IOrderDbContext _dbContext;
        public DeleteProductRangeCommandHandler(IOrderDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException("OrderDbContext");

        public async Task Handle(DeleteProductRangeCommand command, CancellationToken cancellationToken)
        {
            var productRange =
               await _dbContext.ProductRanges.FindAsync(new object[] { command.ProductRangeId });

            if (productRange is null)
                throw new NotFoundException(nameof(ProductRange), command.ProductRangeId);

            _dbContext.ProductRanges.Remove(productRange);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
