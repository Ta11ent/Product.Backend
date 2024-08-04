namespace ShoppingCart.Domain
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public DateTime? OrderTime { get; set; }
        public decimal Price { get; set; }
        public bool IsPaid { get; set; }
        public IEnumerable<ProductRange> ProductRanges { get;set; }
    }
}
