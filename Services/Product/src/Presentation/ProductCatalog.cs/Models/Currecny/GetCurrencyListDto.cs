using ProductCatalog.Application.Application.Queries.Currency.GetCurrencyList;

namespace ProductCatalog.API.Models.Currecny
{
    public class GetCurrencyListDto : IMapWith<GetCurrencyListQuery>
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetCurrencyListDto, GetCurrencyListQuery>()
                .ForMember(x => x.Page,
                    opt => opt.MapFrom(y => y.Page))
                .ForMember(x => x.PageSize,
                    opt => opt.MapFrom(y => y.PageSize));
        }
    }
}
