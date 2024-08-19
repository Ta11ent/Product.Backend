namespace ShoppingCart.Domain
{
    public class Status
    {
        public Guid StatusId { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public string TypeOfStatus { get; set; } = string.Empty;
        public DateTime StatusDate { get; set; }
    }
}
