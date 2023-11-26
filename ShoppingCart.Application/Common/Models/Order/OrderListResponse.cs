using ShoppingCart.Application.Common.Response;

namespace ShoppingCart.Application.Common.Models.Order
{
    public class OrderListResponse : PageResponse<List<OrderListDto>>
    {
        public OrderListResponse(List<OrderListDto> orders, Pagination.Pagination pagination)
            : base(orders, pagination) { }
    }
}
