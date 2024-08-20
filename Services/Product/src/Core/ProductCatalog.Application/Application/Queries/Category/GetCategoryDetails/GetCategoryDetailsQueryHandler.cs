using AutoMapper;
using MediatR;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.Application.Application.Queries.Category.GetCategoryDetails
{
    public class GetCategoryDetailsQueryHandler : IRequestHandler<GetCategoryDetailsQuery, CategoryDetailsResponse>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        public GetCategoryDetailsQueryHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(ICategoryRepository));
            _mapper = mapper;
        }

        public async Task<CategoryDetailsResponse> Handle(GetCategoryDetailsQuery request, CancellationToken cancellationToken)
        {
            var categorydto = await _repository.GetCategoryByIdAsync(request.CategoryId, cancellationToken);
            var category = _mapper.Map<CategoryDetailsDto>(categorydto);

            if (category == null)
                throw new NotFoundExceptions(nameof(category), request.CategoryId);

            return new CategoryDetailsResponse(category);
        }
    }
}
