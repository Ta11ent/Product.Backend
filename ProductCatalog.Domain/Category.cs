namespace ProductCatalog.Domain
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
