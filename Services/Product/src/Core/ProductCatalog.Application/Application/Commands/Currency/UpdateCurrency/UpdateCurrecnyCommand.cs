using MediatR;

namespace ProductCatalog.Application.Application.Commands.Currency.UpdateCurrency
{
    public class UpdateCurrecnyCommand : IRequest
    {
        public Guid CurrencyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}
