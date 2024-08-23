using MediatR;
using ProductCatalog.Application.Common.Abstractions;

namespace ProductCatalog.Application.Application.Commands.ROE.CreateROE
{
    public class CreateROECommandHandler : IRequestHandler<CreateROECommand, Guid>
    {
        private readonly IROERepository _repository;
        public CreateROECommandHandler(IROERepository repository) =>
            _repository = repository ?? throw new ArgumentNullException(nameof(IROERepository));
        public async Task<Guid> Handle(CreateROECommand request, CancellationToken cancellationToken)
        {
            var roe = new Domain.ROE()
            {
                ROEId = Guid.NewGuid(),
                Rate = request.Rate,
                CurrecnyId = request.CurrecnyId,
                DateFrom = request.DateFrom
            };

            await _repository.CreateROEAsync(roe, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return roe.ROEId;
        }
    }
}
