using MediatR;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.Application.Application.Commands.ROE.DeleteROE
{
    public class DeleteROECommandHandler : IRequestHandler<DeleteROECommand>
    {
        private readonly IROERepository _repository;
        public DeleteROECommandHandler(IROERepository repository) =>
            _repository = repository ?? throw new ArgumentNullException(nameof(IROERepository));
        public async Task Handle(DeleteROECommand request, CancellationToken cancellationToken)
        {
            var roe = await _repository.GetROEByIdAsync(request.CurrencyId, request.ROEId, cancellationToken);
            if (roe == null)
                throw new NotFoundExceptions(nameof(ROE), request.ROEId);

            _repository.DeleteROE(roe);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
