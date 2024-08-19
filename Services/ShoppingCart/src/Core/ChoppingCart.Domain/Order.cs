namespace ShoppingCart.Domain
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public int Number { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public IEnumerable<Status> Statuses { get; set; } = new List<Status>();
    }
}
