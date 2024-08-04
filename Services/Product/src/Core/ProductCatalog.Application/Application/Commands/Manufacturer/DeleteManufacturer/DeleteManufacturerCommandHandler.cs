using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Commands.Manufacturer.DeleteManufacturer
{
    public class DeleteManufacturerCommandHandler : IRequestHandler<DeleteManufacturerCommand>
    {
        private readonly IProductDbContext _dbContext;
        public DeleteManufacturerCommandHandler(IProductDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        public async Task Handle(DeleteManufacturerCommand request, CancellationToken cancellationToken)
        {
            var manufacturer =
                await _dbContext.Manufacturer
                    .FirstOrDefaultAsync(x =>x.ManufacturerId == request.ManufacturerId, cancellationToken);
            if (manufacturer == null)
                throw new NotFoundExceptions(nameof(manufacturer), request.ManufacturerId);

            _dbContext.Manufacturer.Remove(manufacturer);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
