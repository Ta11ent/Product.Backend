using MediatR;

namespace ShoppingCart.Application.Application.Commands.ProductRange.DeleteProductRange
{
    public class DeleteOrderItemCommand : IRequest
    {
        public Guid OrderItemId { get; set; }
    }
}
