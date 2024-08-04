using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Commands.ROE.UpdateROE
{
    public class UpdateROECommandHandler : IRequestHandler<UpdateROECommand>
    {
        private readonly IProductDbContext _dbContext;
        public UpdateROECommandHandler(IProductDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        public async Task Handle(UpdateROECommand request, CancellationToken cancellationToken)
        {
            var roe = 
                await _dbContext.ROE
                    .FirstOrDefaultAsync(x => x.CurrecnyId == request.CurrencyId
                        && x.ROEId == request.ROEId,
                        cancellationToken);

            if (roe == null)
                throw new NotFoundExceptions(nameof(ROE), request.ROEId);

            roe.Rate = request.Rate;
            roe.DateFrom = request.DateFrom;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
