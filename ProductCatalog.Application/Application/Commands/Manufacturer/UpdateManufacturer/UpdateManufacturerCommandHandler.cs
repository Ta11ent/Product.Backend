using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Commands.Manufacturer.UpdateManufacturer
{
    public class UpdateManufacturerCommandHandler : IRequestHandler<UpdateManufacturerCommand>
    {
        private readonly IProductDbContext _dbContext;
        public UpdateManufacturerCommandHandler(IProductDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        public async Task Handle(UpdateManufacturerCommand request, CancellationToken cancellationToken)
        {
            var manufacturer =
               await _dbContext.Manufacturer
                   .FirstOrDefaultAsync(x => x.ManufacturerId == request.ManufacturerId, cancellationToken);
            if (manufacturer == null)
                throw new NotFoundExceptions(nameof(manufacturer), request.ManufacturerId);

            manufacturer.Name = request.Name;
            manufacturer.Description = request.Description;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
