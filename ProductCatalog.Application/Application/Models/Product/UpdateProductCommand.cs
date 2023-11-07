namespace ProductCatalog.Application.Application.Models.Product
{
    public class UpdateProductCommand
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
    }
}
