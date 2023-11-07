namespace ProductCatalog.Application.Application.Models.Product
{
    public class CreateProductCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
    }
}
