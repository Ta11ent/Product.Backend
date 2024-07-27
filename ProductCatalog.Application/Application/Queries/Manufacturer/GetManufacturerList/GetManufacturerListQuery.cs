using MediatR;
using ProductCatalog.Application.Common.Pagination;

namespace ProductCatalog.Application.Application.Queries.Manufacturer.GetManufacturerList
{
    public class GetManufacturerListQuery : Pagination, IRequest<ManufacturerListResponse>
    {}
}
