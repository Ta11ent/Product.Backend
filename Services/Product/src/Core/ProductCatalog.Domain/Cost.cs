namespace ProductCatalog.Domain
{
    public class Cost
    {
        public Guid CostId { get; private set; }
        public Guid ProductSaleId { get; private set; }
        public ProductSale ProductSale { get; private set; }
        public decimal Price { get; private set; }
        public Guid CurrencyId { get; private set; }
        public Currency Currency { get; private set; }
        public DateTime DatePrice { get; private set; }

        public Cost Create(Guid productSaleId, decimal price, Guid currencyId)
        {
            CostId = Guid.NewGuid();
            ProductSaleId = productSaleId;
            Price = price;
            CurrencyId = currencyId;
            DatePrice = DateTime.Now;
            return this;
        }
    }
}
