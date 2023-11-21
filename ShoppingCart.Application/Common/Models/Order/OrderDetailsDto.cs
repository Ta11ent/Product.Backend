namespace ShoppingCart.Application.Common.Models.Order
{
    public class OrderDetailsDto
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<OrderProductRange> ProductRange { get; set; }
        public DateTime? OrderTime { get; set; }
        public decimal Price { get; set; }
        public bool IsPaid { get; set; }
        
    }

    public class OrderProductRange
    {
        public Guid ProductRangeId { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }
    }
}
