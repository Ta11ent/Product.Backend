using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Application.Common.Models;

namespace ProductCatalog.Infrastructure.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IProductDbContext _dbContext;
        private readonly IMapper _mapper;
        public CurrencyService(IProductDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

            public async Task<CurrencyDto> GetCurrentROEofCurrency(string code, CancellationToken token)
            => await _dbContext.Currency
                    .Where(x => x.Code == code)
                        .Include(x => x.ROEs.OrderByDescending(y => y.DateFrom).Take(1))
                        .ProjectTo<CurrencyDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(token);
    }
}
