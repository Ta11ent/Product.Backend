using MediatR;

namespace ShoppingCart.Application.Application.Commands.ProductRange.DeleteProductRange
{
    public class DeleteProductRangeCommand : IRequest
    {
        public Guid ProductRangeId { get; set; }
    }
}
