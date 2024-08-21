using AutoMapper;
using MediatR;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;


namespace ProductCatalog.Application.Application.Queries.Currency.GetCurreencyDetails
{
    public class GetCurrencyDetailsQueryHandler : IRequestHandler<GetCurrencyDetailsQuery, CurrencyDetailsResponse>
    {
        private readonly ICurrencyRepository _repository;
        private readonly IMapper _mapper;
        public GetCurrencyDetailsQueryHandler(ICurrencyRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(ICurrencyRepository));
            _mapper = mapper;
        }
        public async Task<CurrencyDetailsResponse> Handle(GetCurrencyDetailsQuery request, CancellationToken cancellationToken)
        {
            var currencydto =
                await _repository.GetCurrencyByIdAsync(request.CurrencyId, cancellationToken);
            if(currencydto == null)
                throw new NotFoundExceptions(nameof(currencydto), request.CurrencyId);

            var currency = _mapper.Map<CurrencyDetailsDto>(currencydto);

            return new CurrencyDetailsResponse(currency);
        }
    }
}
