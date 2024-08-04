namespace ProductCatalog.Domain
{
    public class SubCategory
    {
        public Guid SubCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<ProductSale> ProductsForSale { get; set; }
    }
}
