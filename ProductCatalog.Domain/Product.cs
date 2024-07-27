namespace ProductCatalog.Domain
{
    public class Product
    {
        public Guid ProductId { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public Guid SubCategoryId { get; set; } 
        public SubCategory SubCategory { get; set; }
        public IEnumerable<Cost> Costs { get; set; }
        public bool Available { get; set; }
    }
}
