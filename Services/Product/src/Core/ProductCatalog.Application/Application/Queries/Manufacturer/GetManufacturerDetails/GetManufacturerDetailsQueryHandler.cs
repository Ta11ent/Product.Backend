using AutoMapper;
using MediatR;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.Application.Application.Queries.Manufacturer.GetManufacturerDetails
{
    public class GetManufacturerDetailsQueryHandler : IRequestHandler<GetManufacturerDetailsQuery, ManufacturerDetailsResponse>
    {
        private readonly IManufacturerRepository _repository;
        private readonly IMapper _mapper;
        public GetManufacturerDetailsQueryHandler(IManufacturerRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(IManufacturerRepository));
            _mapper = mapper;
        }
        public async Task<ManufacturerDetailsResponse> Handle(GetManufacturerDetailsQuery request, CancellationToken cancellationToken)
        {
            var manufacturer = await _repository.GetManufacturerByIdAsync(request.ManufacturerId, cancellationToken);

            if (manufacturer == null)
                throw new NotFoundExceptions(nameof(manufacturer), request.ManufacturerId);
            var manufacturerdto = _mapper.Map<ManufacturerDetailsDto>(manufacturer);

            return new ManufacturerDetailsResponse(manufacturerdto);
        }
    }
}
