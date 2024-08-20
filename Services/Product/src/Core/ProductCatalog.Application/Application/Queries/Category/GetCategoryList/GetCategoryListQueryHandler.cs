using AutoMapper;
using MediatR;
using ProductCatalog.Application.Common.Abstractions;

namespace ProductCatalog.Application.Application.Queries.Category.GetCategoryList
{
    public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, CategoryListResponse>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        public GetCategoryListQueryHandler(ICategoryRepository repository, IMapper mapper) { 
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CategoryListResponse> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            var categoriesdto = await _repository.GetAllCategoriesAsync(request, cancellationToken);
            var categories = _mapper.Map<List<CategoryListDto>>(categoriesdto);

            return new CategoryListResponse(categories, request);
        }
    }
}
