namespace ProductCatalog.Domain
{
    public class Manufacturer
    {
        public Guid ManufacturerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IEnumerable<Product> Products { get; set; }
    }
}
