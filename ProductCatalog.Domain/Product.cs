namespace ProductCatalog.Domain
{
    public class Product
    {
        public Guid ProductId { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public Guid CategoryId { get; set; } 
        public Category Category { get; set; }
        public List<Cost> Costs { get; set; }
    }
}
