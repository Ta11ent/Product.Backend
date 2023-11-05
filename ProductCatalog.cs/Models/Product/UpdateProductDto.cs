namespace ProductCatalog.cs.Models.Product
{
    public class UpdateProductDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
    }
}
