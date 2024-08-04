using ProductCatalog.Application.Application.Commands.Currency.UpdateCurrency;

namespace ProductCatalog.API.Models.Currecny
{
    public class UpdateCurrencyDto : IMapWith<UpdateCurrecnyCommand>
    {
        public Guid CurrencyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCurrencyDto, UpdateCurrecnyCommand>()
                .ForMember(x => x.CurrencyId,
                    opt => opt.MapFrom(y => y.CurrencyId))
                .ForMember(x => x.Name,
                    opt => opt.MapFrom(y => y.Name))
                .ForMember(x => x.Code,
                    opt => opt.MapFrom(y => y.Code));
        }
    }
}
