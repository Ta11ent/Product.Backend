namespace SharedModels
{
    public class OrderDetails
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public IEnumerable<ProductRangeDetails> ProductRanges { get; set; }
        public DateTime? OrderTime { get; set; }
        public decimal Price { get; set; }
    }
}
