using MediatR;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Commands.ROE.CreateROE
{
    public class CreateROECommandHandler : IRequestHandler<CreateROECommand, Guid>
    {
        private readonly IProductDbContext _dbContext;
        public CreateROECommandHandler(IProductDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        public async Task<Guid> Handle(CreateROECommand request, CancellationToken cancellationToken)
        {
            var roe = new Domain.ROE()
            {
                ROEId = Guid.NewGuid(),
                Rate = request.Rate,
                CurrecnyId = request.CurrecnyId,
                DateFrom = request.DateFrom
            };

            await _dbContext.ROE.AddAsync(roe, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return roe.ROEId;
        }
    }
}
