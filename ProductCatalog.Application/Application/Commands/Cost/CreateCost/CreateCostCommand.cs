using MediatR;

namespace ProductCatalog.Application.Application.Commands.Cost.CreateCost
{
    public class CreateCostCommand : IRequest<Guid>
    {
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
    }
}
