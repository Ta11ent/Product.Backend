using ProductCatalog.Application.Application.Commands.ROE.CreateROE;

namespace ProductCatalog.API.Models.ROE
{
    public class CreateROEDto : IMapWith<CreateROECommand>
    {
        public Guid CurrecnyId { get; set; }
        public decimal Rate { get; set; }
        public DateTime DateFrom { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateROEDto, CreateROECommand>()
                .ForMember(x => x.CurrecnyId,
                    opt => opt.MapFrom(y => y.CurrecnyId))
                .ForMember(x => x.Rate,
                    opt => opt.MapFrom(y => y.Rate))
                .ForMember(x => x.DateFrom,
                    opt => opt.MapFrom(y => y.DateFrom));
        }
    }
}
