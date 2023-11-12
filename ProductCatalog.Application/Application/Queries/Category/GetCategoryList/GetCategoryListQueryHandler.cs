using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Queries.Category.GetCategoryList
{
    public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, GetCategoryListResponse>
    {
        private readonly IProductDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetCategoryListQueryHandler(IProductDbContext dbContext, IMapper mapper) { 
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }
        public async Task<GetCategoryListResponse> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            var categories = await
                _dbContext.Categories
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<CategoryListDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GetCategoryListResponse(categories, request);
        }
    }
}
