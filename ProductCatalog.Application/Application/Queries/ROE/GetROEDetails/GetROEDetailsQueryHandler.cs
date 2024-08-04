using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Queries.ROE.GetROEDetails
{
    public class GetROEDetailsQueryHandler : IRequestHandler<GetROEDetailsQuery, ROEDetailsResponse>
    {
        private readonly IProductDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetROEDetailsQueryHandler(IProductDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException("ProductDbContext");
            _mapper = mapper;
        }
        public async Task<ROEDetailsResponse> Handle(GetROEDetailsQuery request, CancellationToken cancellationToken)
        {
            var roe =
                await _dbContext.ROE
                    .Where(x => x.CurrecnyId == request.CurrencyId
                        && x.ROEId == request.ROEId)
                    .ProjectTo<ROEDetailsDto>(_mapper.ConfigurationProvider)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(cancellationToken);

            if (roe == null)
                throw new NotFoundExceptions(nameof(ROE), request.ROEId);

            return new ROEDetailsResponse(roe);
        }
    }
}
