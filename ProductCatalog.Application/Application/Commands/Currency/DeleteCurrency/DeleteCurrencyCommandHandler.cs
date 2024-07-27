using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Commands.Currency.DeleteCurrency
{
    public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand>
    {
        private readonly IProductDbContext _dbContext;
        public DeleteCurrencyCommandHandler(IProductDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        public async Task Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
        {
            var currency =
                await _dbContext.Currency
                    .FirstOrDefaultAsync(x => x.CurrencyId == request.CurrencyId, cancellationToken);
            if (currency == null)
                throw new NotFoundExceptions(nameof(currency), request.CurrencyId);

            _dbContext.Currency.Remove(currency);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
