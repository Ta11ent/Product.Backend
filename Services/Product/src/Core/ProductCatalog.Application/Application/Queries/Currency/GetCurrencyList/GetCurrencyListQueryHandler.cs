using AutoMapper;
using MediatR;
using ProductCatalog.Application.Common.Abstractions;


namespace ProductCatalog.Application.Application.Queries.Currency.GetCurrencyList
{
    public class GetCurrencyListQueryHandler : IRequestHandler<GetCurrencyListQuery, CurrencyListResponse>
    {
        private readonly ICurrencyRepository _repository;
        private readonly IMapper _mapper;
        public GetCurrencyListQueryHandler(ICurrencyRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(ICurrencyRepository));
            _mapper = mapper;
        }
        public async Task<CurrencyListResponse> Handle(GetCurrencyListQuery request, CancellationToken cancellationToken)
        {
            var currenciesdto =
                await _repository.GetAllCurrenciesAsync(request, cancellationToken);
            var currencies = _mapper.Map<List<CurrencyListDto>>(currenciesdto);
             
            return new CurrencyListResponse(currencies, request);
        }
    }
}
