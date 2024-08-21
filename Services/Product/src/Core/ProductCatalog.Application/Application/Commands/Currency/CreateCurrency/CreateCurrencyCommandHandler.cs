using MediatR;
using ProductCatalog.Application.Common.Abstractions;

namespace ProductCatalog.Application.Application.Commands.Currency.CreateCurrency
{
    public class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, Guid>
    {
        private readonly ICurrencyRepository _repository;
        public CreateCurrencyCommandHandler(ICurrencyRepository repository) =>
            _repository = repository ?? throw new ArgumentNullException(nameof(ICurrencyRepository));
        public async Task<Guid> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
        {
            var currecny = new Domain.Currency()
            {
                CurrencyId = Guid.NewGuid(),
                Code = request.Code,
                Name = request.Name,
            };

            await _repository.CreateCurrencyAsync(currecny, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return currecny.CurrencyId;
        }
    }
}
