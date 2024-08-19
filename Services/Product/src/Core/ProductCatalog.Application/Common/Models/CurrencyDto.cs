using AutoMapper;
using ProductCatalog.Application.Common.Mapping;
using ProductCatalog.Domain;

namespace ProductCatalog.Application.Common.Models
{
    public class CurrencyDto : IMapWith<Currency>
    {
        public string Currency { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public decimal Rate { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Currency, CurrencyDto>()
                .ForMember(x => x.Currency,
                    opt => opt.MapFrom(y => y.Name))
                .ForMember(x => x.Code,
                    opt => opt.MapFrom(y => y.Code))
                .ForMember(x => x.Rate,
                    opt => opt.MapFrom(y => y.ROEs.FirstOrDefault()!.Rate));
        }
    }
}
