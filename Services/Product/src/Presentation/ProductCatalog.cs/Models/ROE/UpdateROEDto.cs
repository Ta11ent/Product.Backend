using ProductCatalog.Application.Application.Commands.ROE.UpdateROE;

namespace ProductCatalog.API.Models.ROE
{
    public class UpdateROEDto : IMapWith<UpdateROECommand>
    {
        public Guid CurrecnyId { get; set; }
        public Guid ROEId { get; set; }
        public decimal Rate { get; set; }
        public DateTime DateFrom { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateROEDto, UpdateROECommand>()
                .ForMember(x => x.CurrencyId,
                    opt => opt.MapFrom(y => y.CurrecnyId))
                .ForMember(x => x.ROEId,
                    opt => opt.MapFrom(y => y.ROEId))
                .ForMember(x => x.Rate,
                    opt => opt.MapFrom(y => y.Rate))
                .ForMember(x => x.DateFrom,
                    opt => opt.MapFrom(y => y.DateFrom));
        }
    }
}
