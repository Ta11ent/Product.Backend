using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Queries.Currency.GetCurreencyDetails
{
    public class GetCurrencyDetailsQueryHandler : IRequestHandler<GetCurrencyDetailsQuery, CurrencyDetailsResponse>
    {
        private readonly IProductDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetCurrencyDetailsQueryHandler(IProductDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }
        public async Task<CurrencyDetailsResponse> Handle(GetCurrencyDetailsQuery request, CancellationToken cancellationToken)
        {
            var currency =
                await _dbContext.Currency
                    .ProjectTo<CurrencyDetailsDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.CurrencyId == request.CurrencyId, cancellationToken);

            if(currency == null)
                throw new NotFoundExceptions(nameof(currency), request.CurrencyId);

            return new CurrencyDetailsResponse(currency);
        }
    }
}
