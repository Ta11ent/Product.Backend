using MediatR;

namespace ShoppingCart.Application.Application.Commands.Order.CreateOrder
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
    }
}
