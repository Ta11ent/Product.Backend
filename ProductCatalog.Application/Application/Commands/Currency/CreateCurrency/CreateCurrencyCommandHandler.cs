using MediatR;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Commands.Currency.CreateCurrency
{
    public class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, Guid>
    {
        private readonly IProductDbContext _dbContext;
        public CreateCurrencyCommandHandler(IProductDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        public async Task<Guid> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
        {
            var currecny = new Domain.Currency()
            {
                CurrencyId = Guid.NewGuid(),
                Code = request.Code,
                Name = request.Name,
            };

            await _dbContext.Currency.AddAsync(currecny);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return currecny.CurrencyId;
        }
    }
}
