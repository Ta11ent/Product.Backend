using MediatR;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.Application.Application.Commands.Manufacturer.UpdateManufacturer
{
    public class UpdateManufacturerCommandHandler : IRequestHandler<UpdateManufacturerCommand>
    {
        private readonly IManufacturerRepository _repository;
        public UpdateManufacturerCommandHandler(IManufacturerRepository repository) =>
            _repository = repository ?? throw new ArgumentNullException(nameof(IManufacturerRepository));
        public async Task Handle(UpdateManufacturerCommand request, CancellationToken cancellationToken)
        {
            var manufacturer = await _repository.GetManufacturerByIdAsync(request.ManufacturerId, cancellationToken);
            if (manufacturer == null)
                throw new NotFoundExceptions(nameof(manufacturer), request.ManufacturerId);

            manufacturer.Name = request.Name;
            manufacturer.Description = request.Description;

            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
