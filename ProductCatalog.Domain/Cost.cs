namespace ProductCatalog.Domain
{
    public class Cost
    {
        public Guid CostId { get; set; }
        public Guid ProductSaleId { get; set; }
        public ProductSale ProductSale { get; set; }
        public decimal Price { get; set; }
        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public DateTime DatePrice { get; set; }
    }
}
