using MediatR;

namespace ProductCatalog.Application.Application.Commands.Product.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {
        public Guid ProductId { get; set; }
    }
}
