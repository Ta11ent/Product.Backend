namespace ProductCatalog.Domain
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IEnumerable<SubCategory> SubCategories { get; set; }
    }
}
