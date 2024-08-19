using MediatR;

namespace ShoppingCart.Application.Application.Commands.ProductRange.UpdateProductRange
{
    public class UpdateOrderItemCommand : IRequest
    {
        public Guid OrderItemId { get; set; }
        public int Count { get; set; }
    }
}
