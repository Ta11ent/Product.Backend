using AutoMapper;
using MediatR;
using ProductCatalog.Application.Common.Abstractions;


namespace ProductCatalog.Application.Application.Queries.SubCategory.GetSubCategoryList
{
    public class GetSubCategoryListQueryHandler : IRequestHandler<GetSubCategoryListQuery, SubCategoryListResponse>
    {
        private readonly ISubCategoryRepository _repository;
        private readonly IMapper _mapper;
        public GetSubCategoryListQueryHandler(ISubCategoryRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(ISubCategoryRepository));
            _mapper = mapper;
        }

        public async Task<SubCategoryListResponse> Handle(GetSubCategoryListQuery request, CancellationToken cancellationToken)
        {
            var subCategoriesdto = await _repository.GetAllSubCategoriesAsync(request.CategoryId, request, cancellationToken);
            var subCategories = _mapper.Map<List<SubCategoryListDto>>(subCategoriesdto);

            return new SubCategoryListResponse(subCategories, request);
        }
    }
}
