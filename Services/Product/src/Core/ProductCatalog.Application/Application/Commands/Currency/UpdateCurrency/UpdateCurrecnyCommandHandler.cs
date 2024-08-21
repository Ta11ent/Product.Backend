using MediatR;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.Application.Application.Commands.Currency.UpdateCurrency
{
    public class UpdateCurrecnyCommandHandler : IRequestHandler<UpdateCurrecnyCommand>
    {
        private readonly ICurrencyRepository _repository;
        public UpdateCurrecnyCommandHandler(ICurrencyRepository repository) =>
            _repository = repository ?? throw new ArgumentNullException(nameof(ICurrencyRepository));
        public async Task Handle(UpdateCurrecnyCommand request, CancellationToken cancellationToken)
        {
            var currency = await _repository.GetCurrencyByIdAsync(request.CurrencyId, cancellationToken);
            if (currency == null)
                throw new NotFoundExceptions(nameof(currency), request.CurrencyId);

            currency.Name = request.Name;
            currency.Code = request.Code;

            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
