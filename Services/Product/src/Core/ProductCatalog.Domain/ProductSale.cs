namespace ProductCatalog.Domain
{
    public class ProductSale
    {
        public Guid ProductSaleId { get; private set; }
        public Guid SubCategoryId { get; private set; }
        public SubCategory SubCategory { get; private set; }
        public Guid ProductId {  get; private set; }
        public Product Product { get; private set; }
        public IEnumerable<Cost> Costs { get; private set; }
        public bool Available { get; private set; }

        public ProductSale Create(Guid subCategoryId, Guid productId, bool avalible = true)
        {
            ProductSaleId = Guid.NewGuid();
            SubCategoryId = subCategoryId;
            ProductId = productId;  
            Available = avalible;
            return this;
        }

        public ProductSale Create(Product product, Cost cost)
        {
            Product = product;
            Costs = new List<Cost>() { cost };
            return this;
        }
        public ProductSale Update(Guid subCategoryId, bool avalible)
        {
            SubCategoryId = subCategoryId;
            Available = avalible;
            return this;
        }
    }

   
}
