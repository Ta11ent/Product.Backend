using ProductCatalog.Application.Application.Commands.Currency.CreateCurrency;

namespace ProductCatalog.API.Models.Currecny
{
    public class CreateCurrencyDto : IMapWith<CreateCurrencyCommand>
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCurrencyDto, CreateCurrencyCommand>()
                .ForMember(x => x.Name,
                    opt => opt.MapFrom(y => y.Name))
                .ForMember(x => x.Code,
                    opt => opt.MapFrom(y => y.Code));
        }
    }
}
