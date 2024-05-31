namespace MessageService.Models.Context
{
    public class ProductRangeDetailsDto
    {
        public Guid ProductRangeId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Available { get; set; }
        public int Count { get; set; }
    }
}
