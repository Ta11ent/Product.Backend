using MediatR;

namespace ProductCatalog.Application.Application.Commands.Currency.DeleteCurrency
{
    public class DeleteCurrencyCommand : IRequest
    {
        public Guid CurrencyId { get; set; }
    }
}
