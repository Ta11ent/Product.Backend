namespace ProductCatalog.cs.Models.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
    }
}
