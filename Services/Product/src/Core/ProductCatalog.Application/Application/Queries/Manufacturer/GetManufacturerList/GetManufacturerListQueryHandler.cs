using AutoMapper;
using MediatR;
using ProductCatalog.Application.Common.Abstractions;

namespace ProductCatalog.Application.Application.Queries.Manufacturer.GetManufacturerList
{
    public class GetManufacturerListQueryHandler : IRequestHandler<GetManufacturerListQuery, ManufacturerListResponse>
    {
        private readonly IManufacturerRepository _repository;
        private readonly IMapper _mapper;
        public GetManufacturerListQueryHandler(IManufacturerRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(IManufacturerRepository));
            _mapper = mapper;
        }
        public async Task<ManufacturerListResponse> Handle(GetManufacturerListQuery request, CancellationToken cancellationToken)
        {
            var manufacturers = await _repository.GetAllManufacturersAsync(request, cancellationToken);
            var manufacturersdto = _mapper.Map<List<ManufacturerListDto>>(manufacturers);

            return new ManufacturerListResponse(manufacturersdto, request);
        }
    }
}
