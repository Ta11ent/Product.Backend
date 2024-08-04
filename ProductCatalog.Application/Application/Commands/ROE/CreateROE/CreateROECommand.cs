using MediatR;

namespace ProductCatalog.Application.Application.Commands.ROE.CreateROE
{
    public class CreateROECommand : IRequest<Guid>
    {
        public Guid CurrecnyId { get; set; }
        public decimal Rate { get; set; }
        public DateTime DateFrom { get; set; }
    }
}
