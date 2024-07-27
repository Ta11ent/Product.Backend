using MediatR;

namespace ProductCatalog.Application.Application.Commands.Manufacturer.UpdateManufacturer
{
    public class UpdateManufacturerCommand : IRequest
    {
        public Guid ManufacturerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
