using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Commands.Currency.UpdateCurrency
{
    public class UpdateCurrecnyCommandHandler : IRequestHandler<UpdateCurrecnyCommand>
    {
        private readonly IProductDbContext _dbContext;
        public UpdateCurrecnyCommandHandler(IProductDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        public async Task Handle(UpdateCurrecnyCommand request, CancellationToken cancellationToken)
        {
            var currency =
                await _dbContext.Currency
                    .FirstOrDefaultAsync(x => x.CurrencyId == request.CurrencyId, cancellationToken);
            if (currency == null)
                throw new NotFoundExceptions(nameof(currency), request.CurrencyId);

            currency.Name = request.Name;
            currency.Code = request.Code;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
