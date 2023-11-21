using ShoppingCart.Application.Common.Response;

namespace ShoppingCart.Application.Common.Models.Order
{
    public class OrderPageResponse : PageResponse<List<OrderDetailsDto>>
    {
        public OrderPageResponse(List<OrderDetailsDto> orders, Pagination.Pagination pagination)
            : base(orders, pagination) { }
    }
}
