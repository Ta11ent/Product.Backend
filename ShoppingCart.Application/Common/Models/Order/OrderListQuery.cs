using ShoppingCart.Application.Common.Pagination;

namespace ShoppingCart.Application.Common.Models.Order
{
    public class OrderListQuery : PaginationParam
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public Guid? UserId { get; set; }
    }
}
