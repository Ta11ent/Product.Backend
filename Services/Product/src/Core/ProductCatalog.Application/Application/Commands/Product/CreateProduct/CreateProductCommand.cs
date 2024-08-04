using MediatR;

namespace ProductCatalog.Application.Application.Commands.Product.CreateProduct
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Guid SubCategoryId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid CurrencyId { get; set; }
        public Guid ManufacturerId { get; set; }
    }
}
