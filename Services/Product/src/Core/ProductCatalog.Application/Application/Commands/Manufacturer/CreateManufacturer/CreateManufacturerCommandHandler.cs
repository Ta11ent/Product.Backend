using MediatR;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Interfaces;

namespace ProductCatalog.Application.Application.Commands.Manufacturer.CreateManufacturer
{
    public class CreateManufacturerCommandHandler : IRequestHandler<CreateManufacturerCommand, Guid>
    {
        private readonly IManufacturerRepository _repository;
        public CreateManufacturerCommandHandler(IManufacturerRepository repository) =>
            _repository = repository ?? throw new ArgumentNullException(nameof(IManufacturerRepository));
        public async Task<Guid> Handle(CreateManufacturerCommand request, CancellationToken cancellationToken)
        {
            var manufacturer = new Domain.Manufacturer()
            {
                ManufacturerId = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description
            };

            await _repository.CreateManufacturerAsync(manufacturer, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return manufacturer.ManufacturerId;
        }
    }
}
