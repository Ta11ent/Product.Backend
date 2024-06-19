using MediatR;

namespace ShoppingCart.Application.Application.Commands.ProductRange.UpdateProductRange
{
    public class UpdateProductRangeCommand : IRequest
    {
        public Guid ProductRangeId { get; set; }
        public int Count { get; set; }
    }
}
