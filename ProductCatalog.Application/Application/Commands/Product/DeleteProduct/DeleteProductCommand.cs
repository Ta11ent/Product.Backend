using MediatR;

namespace ProductCatalog.Application.Application.Commands.Product.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {
        public Guid SubCategoryId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ProductId { get; set; }
    }
}
