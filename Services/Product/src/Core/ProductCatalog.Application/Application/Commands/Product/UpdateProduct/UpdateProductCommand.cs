using MediatR;

namespace ProductCatalog.Application.Application.Commands.Product.UpdateProduct
{
    public class UpdateProductCommand : IRequest
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid SubCategoryId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ManufacturerId { get; set; }
        public bool? Available { get; set; }
        public decimal Price { get; set; }
    }
}
