namespace MessageService.Models.Context
{
    public class OrderDetailsDto
    {
        public Guid OrderId { get; set; }
        public UserOrderDetailsDto User { get; set; } 
        public IEnumerable<ProductRangeDetailsDto> ProductRanges { get; set; }
        public DateTime? OrderTime { get; set; }
        public decimal Price { get; set; }
        public bool IsPaid { get; set; }
    }
}
