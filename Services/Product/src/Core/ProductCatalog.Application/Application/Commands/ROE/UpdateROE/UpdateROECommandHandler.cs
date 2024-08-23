using MediatR;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.Application.Application.Commands.ROE.UpdateROE
{
    public class UpdateROECommandHandler : IRequestHandler<UpdateROECommand>
    {
        private readonly IROERepository _repository;
        public UpdateROECommandHandler(IROERepository repository) =>
            _repository = repository ?? throw new ArgumentNullException(nameof(IROERepository));
        public async Task Handle(UpdateROECommand request, CancellationToken cancellationToken)
        {
            var roe = await _repository.GetROEByIdAsync(request.CurrencyId, request.ROEId, cancellationToken);
            if (roe == null)
                throw new NotFoundExceptions(nameof(ROE), request.ROEId);

            roe.Rate = request.Rate;
            roe.DateFrom = request.DateFrom;

            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
