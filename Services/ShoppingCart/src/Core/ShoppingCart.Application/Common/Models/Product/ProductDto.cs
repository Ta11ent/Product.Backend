namespace ShoppingCart.Application.Common.Models.Product
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Available { get; set; }
    }
}
