namespace ProductCatalog.Domain
{
    public class Product
    {
        public Guid ProductId { get; private set; } 
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public Guid ManufacturerId { get; private set; }
        public Manufacturer Manufacturer { get; private set; }
        public ProductSale ProductSale { get; private set; }

        public Product Create(string name, string description, Guid manufacturerId)
        {
            ProductId = Guid.NewGuid();
            Name = name;
            Description = description;
            ManufacturerId = manufacturerId;
            return this;
        }
        public Product Update(string name, string description, Guid manufacturerId)
        {
            Name = name;
            Description = description;
            ManufacturerId = manufacturerId;
            return this;
        }
     
    }
}