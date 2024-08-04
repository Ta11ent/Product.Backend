using AutoMapper;
using ProductCatalog.Application.Application.Queries.Currency.GetCurreencyDetails;
using ProductCatalog.Application.Common.Mapping;

namespace ProductCatalog.Application.Application.Queries.Currency.GetCurrencyList
{
    public class CurrencyListDto : IMapWith<Domain.Currency>
    {
        public Guid CurrencyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Currency, CurrencyListDto>()
                .ForMember(x => x.CurrencyId,
                    opt => opt.MapFrom(y => y.CurrencyId))
                .ForMember(x => x.Name,
                    opt => opt.MapFrom(y => y.Name))
                .ForMember(x => x.Code,
                    opt => opt.MapFrom(y => y.Code));
        }
    }
}
