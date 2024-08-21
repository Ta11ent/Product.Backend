using AutoMapper;
using MediatR;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.Application.Application.Queries.SubCategory.GetSubCategoryDetails
{
    public class GetSubCategoryQueryHandler : IRequestHandler<GetSubCategoryDetailsQuery, SubCategoryDetailsResponse>
    {
        private readonly ISubCategoryRepository _repostory;
        private readonly IMapper _mapper;
        public GetSubCategoryQueryHandler(ISubCategoryRepository repository, IMapper mapper)
        {
            _repostory = repository ?? throw new ArgumentNullException(nameof(ISubCategoryRepository));
            _mapper = mapper;
        }

        public async Task<SubCategoryDetailsResponse> Handle(GetSubCategoryDetailsQuery request, CancellationToken cancellationToken)
        {
            var subCategorydto = await _repostory.GetSubCategoryByIdAsync(
                request.CategoryId, 
                request.SubCategoryId, 
                cancellationToken);

            if(subCategorydto == null)
                throw new NotFoundExceptions(nameof(Domain.SubCategory), request.SubCategoryId);

            var subCategory = _mapper.Map<SubCategoryDetailsDto>(subCategorydto);

            return new SubCategoryDetailsResponse(subCategory);
        }
    }
}
