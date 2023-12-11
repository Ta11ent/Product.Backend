namespace ProductCatalog.Domain
{
    public class Cost
    {
        public Guid PriceId { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public DateTime DatePrice { get; set; }
    }
}
