using ProductCatalog.Application.Application.Queries.Manufacturer.GetManufacturerList;

namespace ProductCatalog.API.Models.Manufacturer
{
    public class GetManufacturerListDto : IMapWith<GetManufacturerListQuery>
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetManufacturerListDto, GetManufacturerListQuery>()
                .ForMember(x => x.Page,
                    opt => opt.MapFrom(y => y.Page))
                .ForMember(x => x.PageSize,
                    opt => opt.MapFrom(y => y.PageSize));
        }
    }
}
