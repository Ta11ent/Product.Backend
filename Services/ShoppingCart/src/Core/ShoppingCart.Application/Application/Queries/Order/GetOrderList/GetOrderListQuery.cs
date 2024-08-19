using MediatR;
using ShoppingCart.Application.Common.Pagination;

namespace ShoppingCart.Application.Queries.Order.GetOrderList
{
    public class GetOrderListQuery : PaginationParam, IRequest<OrderListResponse>
    {
        public Guid? UserId { get; set; }
        public string? Status {  get; set; } 
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string? Ccy { get; set; }
    }
}
