using AutoMapper;
using MediatR;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;


namespace ProductCatalog.Application.Application.Queries.ROE.GetROEDetails
{
    public class GetROEDetailsQueryHandler : IRequestHandler<GetROEDetailsQuery, ROEDetailsResponse>
    {
        private readonly IROERepository _repository;
        private readonly IMapper _mapper;
        public GetROEDetailsQueryHandler(IROERepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException("ProductDbContext");
            _mapper = mapper;
        }
        public async Task<ROEDetailsResponse> Handle(GetROEDetailsQuery request, CancellationToken cancellationToken)
        {
            var roedto = await _repository.GetROEByIdAsync(request.CurrencyId, request.ROEId, cancellationToken);
            if (roedto == null)
                throw new NotFoundExceptions(nameof(ROE), request.ROEId);

            var roe = _mapper.Map<ROEDetailsDto>(roedto);
            return new ROEDetailsResponse(roe);
        }
    }
}
