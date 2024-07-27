using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Queries.Manufacturer.GetManufacturerList
{
    public class GetManufacturerListQueryHandler : IRequestHandler<GetManufacturerListQuery, ManufacturerListResponse>
    {
        private readonly IProductDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetManufacturerListQueryHandler(IProductDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }
        public async Task<ManufacturerListResponse> Handle(GetManufacturerListQuery request, CancellationToken cancellationToken)
        {
            var manufacturers = await
               _dbContext.Manufacturer
               .Skip((request.Page - 1) * request.PageSize)
               .Take(request.PageSize)
               .ProjectTo<ManufacturerListDto>(_mapper.ConfigurationProvider)
               .ToListAsync(cancellationToken);

            return new ManufacturerListResponse(manufacturers, request);
        }
    }
}
