namespace SharedModels
{
    public class OrderDetails
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public IEnumerable<OrderItemDetails> OrderItems { get; set; } = new List<OrderItemDetails>();
        public decimal Price { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
