using ProductCatalog.Application.Application.Models.Category;

namespace ProductCatalog.Application.Application.Models.Product
{
    public class ProductLookupDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
