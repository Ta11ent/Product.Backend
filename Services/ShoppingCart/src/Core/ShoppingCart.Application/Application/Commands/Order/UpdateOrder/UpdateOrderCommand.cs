using MediatR;

namespace ShoppingCart.Application.Application.Commands.Order.UpdateOrder
{
    public class UpdateOrderCommand : IRequest<bool>
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
