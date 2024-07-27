using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Queries.Manufacturer.GetManufacturerDetails
{
    public class GetManufacturerDetailsQueryHandler : IRequestHandler<GetManufacturerDetailsQuery, ManufacturerDetailsResponse>
    {
        private readonly IProductDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetManufacturerDetailsQueryHandler(IProductDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }
        public async Task<ManufacturerDetailsResponse> Handle(GetManufacturerDetailsQuery request, CancellationToken cancellationToken)
        {
           var manufacturer =
               await _dbContext.Manufacturer
                .Where(c => c.ManufacturerId == request.ManufacturerId)
                .ProjectTo<ManufacturerDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (manufacturer == null)
                throw new NotFoundExceptions(nameof(manufacturer), request.ManufacturerId);

            return new ManufacturerDetailsResponse(manufacturer);
        }
    }
}
