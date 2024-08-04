using ProductCatalog.Application.Common.Response;

namespace ProductCatalog.Application.Application.Queries.Manufacturer.GetManufacturerDetails
{
    public class ManufacturerDetailsResponse : Response<ManufacturerDetailsDto>
    {
        public ManufacturerDetailsResponse(ManufacturerDetailsDto data) : base(data) { }
    }
}
