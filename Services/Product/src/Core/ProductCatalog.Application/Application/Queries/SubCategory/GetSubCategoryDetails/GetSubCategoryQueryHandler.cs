using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Queries.SubCategory.GetSubCategoryDetails
{
    public class GetSubCategoryQueryHandler : IRequestHandler<GetSubCategoryDetailsQuery, SubCategoryDetailsResponse>
    {
        private readonly IProductDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetSubCategoryQueryHandler(IProductDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException("ProductDbContext");
            _mapper = mapper;
        }

        public async Task<SubCategoryDetailsResponse> Handle(GetSubCategoryDetailsQuery request, CancellationToken cancellationToken)
        {
            var subCategory = 
                await _dbContext.SubCategories
                    .Where(x => x.CategoryId == request.CategoryId 
                        && x.SubCategoryId == request.SubCategoryId)
                    .ProjectTo<SubCategoryDetailsDto>(_mapper.ConfigurationProvider)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(cancellationToken);

            if(subCategory == null)
                throw new NotFoundExceptions(nameof(subCategory), request.SubCategoryId);

            return new SubCategoryDetailsResponse(subCategory);
        }
    }
}
