using ShoppingCart.Application.Common.Response;

namespace ShoppingCart.Application.Common.Models.Order
{
    public class OrderDetailsResponse : Response<OrderDetailsDto>
    {
        public OrderDetailsResponse(OrderDetailsDto order)
            : base(order) { }
    }
}
