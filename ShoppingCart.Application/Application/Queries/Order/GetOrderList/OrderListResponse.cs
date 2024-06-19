using ShoppingCart.Application.Common.Pagination;
using ShoppingCart.Application.Common.Response;

namespace ShoppingCart.Application.Queries.Order.GetOrderList
{
    public class OrderListResponse : PageResponse<List<OrderListDto>>
    {
        public OrderListResponse(List<OrderListDto> orders, PaginationParam pagination)
            : base(orders, pagination) { }
    }
}
