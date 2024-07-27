using MediatR;

namespace ProductCatalog.Application.Application.Commands.Manufacturer.CreateManufacturer
{
    public class CreateManufacturerCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
