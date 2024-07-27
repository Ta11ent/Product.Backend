using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Queries.SubCategory.GetSubCategoryList
{
    public class GetSubCategoryListQueryHandler : IRequestHandler<GetSubCategoryListQuery, SubCategoryListResponse>
    {
        private readonly IProductDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetSubCategoryListQueryHandler(IProductDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException("ProductDbContext");
            _mapper = mapper;
        }

        public async Task<SubCategoryListResponse> Handle(GetSubCategoryListQuery request, CancellationToken cancellationToken)
        {
            var subCategories =
                await _dbContext.SubCategories
                    .Where(x => x.CategoryId == request.CategoryId)
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .OrderBy(x => x.Name)
                    .ProjectTo<SubCategoryListDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

            return new SubCategoryListResponse(subCategories, request);
        }
    }
}
