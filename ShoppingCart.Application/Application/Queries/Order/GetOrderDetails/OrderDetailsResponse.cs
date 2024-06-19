using ShoppingCart.Application.Common.Response;

namespace ShoppingCart.Application.Application.Queries.Order.GetOrderDetails
{
    public class OrderDetailsResponse : Response<OrderDetailsDto>
    {
        public OrderDetailsResponse(OrderDetailsDto order)
            : base(order) { }
    }
}
