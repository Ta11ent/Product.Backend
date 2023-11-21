namespace ShoppingCart.Application.Common.Models.Order
{
    public class OrderListQuery : Pagination.Pagination
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public Guid UserId { get; set; }
    }
}
