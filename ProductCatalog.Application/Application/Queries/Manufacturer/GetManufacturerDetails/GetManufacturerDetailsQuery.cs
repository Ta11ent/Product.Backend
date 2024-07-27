using MediatR;

namespace ProductCatalog.Application.Application.Queries.Manufacturer.GetManufacturerDetails
{
    public class GetManufacturerDetailsQuery : IRequest<ManufacturerDetailsResponse>
    {
        public Guid ManufacturerId { get; set; }
    }
}
