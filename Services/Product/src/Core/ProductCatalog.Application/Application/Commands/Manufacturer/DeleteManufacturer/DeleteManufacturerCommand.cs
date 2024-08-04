using MediatR;

namespace ProductCatalog.Application.Application.Commands.Manufacturer.DeleteManufacturer
{
    public class DeleteManufacturerCommand : IRequest
    {
        public Guid ManufacturerId { get; set; }
    }
}
