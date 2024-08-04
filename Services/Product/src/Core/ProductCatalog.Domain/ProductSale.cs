namespace ProductCatalog.Domain
{
    public class ProductSale
    {
        public Guid ProductSaleId { get; set; }
        //public Guid CategoryId { get; set; }
        //public Category Category { get; set; }
        public Guid SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        public Guid ProductId {  get; set; }
        public Product Product { get; set; }
        public IEnumerable<Cost> Costs { get; set; }
        public bool Available { get; set; }
    }
}
