using MediatR;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.Application.Application.Commands.Manufacturer.DeleteManufacturer
{
    public class DeleteManufacturerCommandHandler : IRequestHandler<DeleteManufacturerCommand>
    {
        private readonly IManufacturerRepository _repository;
        public DeleteManufacturerCommandHandler(IManufacturerRepository repository) =>
            _repository = repository ?? throw new ArgumentNullException(nameof(IManufacturerRepository));
        public async Task Handle(DeleteManufacturerCommand request, CancellationToken cancellationToken)
        {
            var manufacturer = await _repository.GetManufacturerByIdAsync(request.ManufacturerId, cancellationToken);
            if (manufacturer == null)
                throw new NotFoundExceptions(nameof(manufacturer), request.ManufacturerId);

            _repository.DeleteManufacturer(manufacturer);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
