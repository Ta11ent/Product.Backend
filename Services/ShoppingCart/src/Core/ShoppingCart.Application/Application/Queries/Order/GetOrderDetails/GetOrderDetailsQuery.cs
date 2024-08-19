using MediatR;

namespace ShoppingCart.Application.Application.Queries.Order.GetOrderDetails
{
    public class GetOrderDetailsQuery : IRequest<OrderDetailsResponse>
    {
        public Guid OrderId { get; set; }
        public string? Ccy { get;set; }
    }
}
