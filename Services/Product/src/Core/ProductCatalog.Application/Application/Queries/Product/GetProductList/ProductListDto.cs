using AutoMapper;
using ProductCatalog.Application.Common.Mapping;

namespace ProductCatalog.Application.Application.Queries.Product.GetProductList
{
    public class ProductListDto : IMapWith<Domain.ProductSale>
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Ccy { get; set; } = string.Empty;
        public decimal Rate { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.ProductSale, ProductListDto>()
                .ForMember(x => x.ProductId,
                    opt => opt.MapFrom(y => y.Product.ProductSale.ProductSaleId))
                .ForMember(x => x.Name,
                    opt => opt.MapFrom(y => y.Product.Name))
                .ForMember(x => x.Description,
                    opt => opt.MapFrom(y => y.Product.Description))
                .ForMember(x => x.Manufacturer,
                    opt => opt.MapFrom(y => y.Product.Manufacturer.Name))
                .ForMember(x => x.Price,
                    opt => opt.MapFrom(y => y.Costs
                        .OrderByDescending(d => d.DatePrice)
                        .FirstOrDefault()!.Price))
                .ForMember(x => x.Ccy,
                    opt => opt.MapFrom(y => y.Costs
                        .OrderByDescending(d => d.DatePrice)
                        .FirstOrDefault()!.Currency.Code))
                .ForMember(x => x.Rate,
                    opt => opt.MapFrom(y => y.Costs
                        .FirstOrDefault()!.Currency
                        .ROEs.FirstOrDefault()!.Rate));
        }
    }
}
