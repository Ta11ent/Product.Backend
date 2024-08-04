using MediatR;

namespace ProductCatalog.Application.Application.Commands.ROE.DeleteROE
{
    public class DeleteROECommand : IRequest
    {
        public Guid CurrencyId {  get; set; }
        public Guid ROEId { get; set; }
    }
}
