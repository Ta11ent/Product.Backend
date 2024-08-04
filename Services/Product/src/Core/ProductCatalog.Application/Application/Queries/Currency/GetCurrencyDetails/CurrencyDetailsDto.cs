using AutoMapper;
using ProductCatalog.Application.Application.Queries.ROE.GetROEDetails;
using ProductCatalog.Application.Common.Mapping;

namespace ProductCatalog.Application.Application.Queries.Currency.GetCurreencyDetails
{
    public class CurrencyDetailsDto : IMapWith<Domain.Currency>
    {
        public Guid CurrencyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public IEnumerable<ROEDetailsDto> ROEs { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Currency, CurrencyDetailsDto>()
                .ForMember(x => x.CurrencyId,
                    opt => opt.MapFrom(y => y.CurrencyId))
                .ForMember(x => x.Name,
                    opt => opt.MapFrom(y => y.Name))
                .ForMember(x => x.Code,
                    opt => opt.MapFrom(y => y.Code))
                .ForMember(x => x.ROEs,
                    opt => opt.MapFrom(y => y.ROEs));
        }
    }
}
