using ProductCatalog.Application.Common.Pagination;
using ProductCatalog.Application.Common.Response;

namespace ProductCatalog.Application.Application.Queries.Manufacturer.GetManufacturerList
{
    public class ManufacturerListResponse : PageResponse<List<ManufacturerListDto>>
    {
        public ManufacturerListResponse(List<ManufacturerListDto> data, IPagination  pagination)
            : base(data, pagination) { }
    }
}
