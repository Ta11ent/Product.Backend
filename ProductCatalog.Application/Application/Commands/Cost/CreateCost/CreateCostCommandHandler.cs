using MediatR;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Commands.Cost.CreateCost
{
    public class CreateCostCommandHandler : IRequestHandler<CreateCostCommand, Guid>
    {
        private readonly IProductDbContext _dbContext;
        public CreateCostCommandHandler(IProductDbContext dbContext) =>
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public Task<Guid> Handle(CreateCostCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
