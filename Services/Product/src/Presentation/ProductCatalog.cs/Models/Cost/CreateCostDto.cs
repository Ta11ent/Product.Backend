using ProductCatalog.Application.Application.Commands.Cost.CreateCost;

namespace ProductCatalog.API.Models.Cost
{
    public class CreateCostDto : IMapWith<CreateCostCommand>
    {
        public Guid ProductId { get;set; }
        public decimal Price { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCostDto, CreateCostCommand>()
                .ForMember(x => x.ProductId,
                    opt => opt.MapFrom(y => y.ProductId))
                .ForMember(x => x.Price,
                    opt => opt.MapFrom(y => y.Price));
        }
    }
}
