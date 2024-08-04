using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Queries.Currency.GetCurrencyList
{
    public class GetCurrencyListQueryHandler : IRequestHandler<GetCurrencyListQuery, CurrencyListResponse>
    {
        private readonly IProductDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetCurrencyListQueryHandler(IProductDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }
        public async Task<CurrencyListResponse> Handle(GetCurrencyListQuery request, CancellationToken cancellationToken)
        {
            var currencies = await
               _dbContext.Currency
               .Skip((request.Page - 1) * request.PageSize)
               .Take(request.PageSize)
               .ProjectTo<CurrencyListDto>(_mapper.ConfigurationProvider)
               .ToListAsync(cancellationToken);

            return new CurrencyListResponse(currencies, request);
        }
    }
}
