using MediatR;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Commands.Cost.CreateCost
{
    public class CreateCostCommandHandler : IRequestHandler<CreateCostCommand, Guid>
    {
        private readonly IProductDbContext _dbContext;
        public CreateCostCommandHandler(IProductDbContext dbContext) =>
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task<Guid> Handle(CreateCostCommand command, CancellationToken cancellationToken)
        {
            var price = new Domain.Cost
            {
                PriceId = Guid.NewGuid(),
                ProductId = command.ProductId,
                Price = command.Price,
                DatePrice = DateTime.Now
            };

            await _dbContext.Costs.AddAsync(price);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return price.PriceId;
        }
    }
}
