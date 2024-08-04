using MediatR;

namespace ProductCatalog.Application.Application.Commands.Currency.CreateCurrency
{
    public class CreateCurrencyCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}
