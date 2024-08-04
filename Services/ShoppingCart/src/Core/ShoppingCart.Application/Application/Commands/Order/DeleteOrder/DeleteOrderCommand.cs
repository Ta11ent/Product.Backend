using MediatR;

namespace ShoppingCart.Application.Application.Commands.Order.DeleteOrder
{
    public class DeleteOrderCommand : IRequest
    {
        public Guid OrderId { get; set; }
    }
}
