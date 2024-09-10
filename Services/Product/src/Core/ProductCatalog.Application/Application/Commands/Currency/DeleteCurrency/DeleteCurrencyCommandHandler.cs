using MediatR;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.Application.Application.Commands.Currency.DeleteCurrency
{
    public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand>
    {
        private readonly ICurrencyRepository _repository;
        public DeleteCurrencyCommandHandler(ICurrencyRepository repository) =>
            _repository = repository ?? throw new ArgumentNullException(nameof(ICurrencyRepository));
        public async Task Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
        {
            var currency = await _repository.GetCurrencyByIdAsync(request.CurrencyId, cancellationToken);
            if (currency == null)
                throw new NotFoundExceptions(nameof(currency), request.CurrencyId);

            _repository.DeleteCurrency(currency);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
