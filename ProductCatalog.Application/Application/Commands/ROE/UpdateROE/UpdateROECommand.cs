using MediatR;

namespace ProductCatalog.Application.Application.Commands.ROE.UpdateROE
{
    public class UpdateROECommand : IRequest
    {
        public Guid CurrencyId { get; set; }
        public Guid ROEId { get; set; }
        public decimal Rate { get; set; }
        public DateTime DateFrom { get; set; }
    }
}
