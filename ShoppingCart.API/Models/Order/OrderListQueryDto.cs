using ShoppingCart.Application.Common.Models.Order;

namespace ShoppingCart.API.Models.Order
{
    public class OrderListQueryDto : IMapWith<OrderListQuery>
    {
        public int? Page { get; set; }
        public int? PageSize { get; set;}
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public Guid? UserId { get; set; }
        public bool? IsPaid { get; set; }
    }
}
