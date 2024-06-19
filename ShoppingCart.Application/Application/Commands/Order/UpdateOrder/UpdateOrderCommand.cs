using MediatR;

namespace ShoppingCart.Application.Application.Commands.Order.UpdateOrder
{
    public class UpdateOrderCommand : IRequest
    {
        public Guid? OrderId { get; set; }
        public bool IsPaid { get; set; }
    }
}
