using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Commands.ROE.DeleteROE
{
    public class DeleteROECommandHandler : IRequestHandler<DeleteROECommand>
    {
        private readonly IProductDbContext _dbContext;
        public DeleteROECommandHandler(IProductDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        public async Task Handle(DeleteROECommand request, CancellationToken cancellationToken)
        {
            var roe =
                await _dbContext.ROE
                    .FirstOrDefaultAsync(x => x.CurrecnyId == request.CurrencyId
                        && x.ROEId == request.ROEId,
                        cancellationToken);
            if (roe == null)
                throw new NotFoundExceptions(nameof(ROE), request.ROEId);

            _dbContext.ROE.Remove(roe);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
