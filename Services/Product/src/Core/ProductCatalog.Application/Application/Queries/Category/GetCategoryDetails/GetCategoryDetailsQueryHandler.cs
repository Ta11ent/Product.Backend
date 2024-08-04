using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Queries.Category.GetCategoryDetails
{
    public class GetCategoryDetailsQueryHandler : IRequestHandler<GetCategoryDetailsQuery, CategoryDetailsResponse>
    {
        private readonly IProductDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetCategoryDetailsQueryHandler(IProductDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        public async Task<CategoryDetailsResponse> Handle(GetCategoryDetailsQuery request, CancellationToken cancellationToken)
        {
            var category = await
                _dbContext.Categories
                .Where(c => c.CategoryId == request.CategoryId)
                .ProjectTo<CategoryDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (category == null)
                throw new NotFoundExceptions(nameof(category), request.CategoryId);

            return new CategoryDetailsResponse(category);
        }
    }
}
