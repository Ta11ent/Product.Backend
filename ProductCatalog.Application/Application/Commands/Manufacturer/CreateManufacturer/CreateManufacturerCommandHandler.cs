using MediatR;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Commands.Manufacturer.CreateManufacturer
{
    public class CreateManufacturerCommandHandler : IRequestHandler<CreateManufacturerCommand, Guid>
    {
        private readonly IProductDbContext _dbContext;
        public CreateManufacturerCommandHandler(IProductDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        public async Task<Guid> Handle(CreateManufacturerCommand request, CancellationToken cancellationToken)
        {
            var manufacturer = new Domain.Manufacturer()
            {
                ManufacturerId = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description
            };

            await _dbContext.Manufacturer.AddAsync(manufacturer, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return manufacturer.ManufacturerId;
        }
    }
}
