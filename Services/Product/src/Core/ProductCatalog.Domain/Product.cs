namespace ProductCatalog.Domain
{
    public class Product
    {
        public Guid ProductId { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public ProductSale ProductSale { get; set; }
    }
}
