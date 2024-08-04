using MediatR;
using ShoppingCart.Application.Common.Pagination;

namespace ShoppingCart.Application.Queries.Order.GetOrderList
{
    public class GetOrderListQuery : PaginationParam, IRequest<OrderListResponse>
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public Guid? UserId { get; set; }
        public bool? IsPaid { get; set; }
    }
}
