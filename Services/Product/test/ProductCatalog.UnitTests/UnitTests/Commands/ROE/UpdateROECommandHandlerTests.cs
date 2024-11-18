using Moq;
using ProductCatalog.Application.Application.Commands.ROE.UpdateROE;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.UnitTests.Commands.ROE
{
    public class UpdateROECommandHandlerTests : BaseTestHandler<IROERepository>
    {
        private readonly UpdateROECommand _command;
        private readonly Domain.ROE _roe;
        public UpdateROECommandHandlerTests() : base()
        {
            _roe = new()
            {
                ROEId = Guid.NewGuid(),
                CurrecnyId = Guid.NewGuid(),
                Rate = 0.023253m,
                DateFrom = DateTime.Now
            };
            _command = new()
            {
                CurrencyId =_roe.CurrecnyId,
                ROEId = _roe.ROEId,
                Rate = 0.2321m,
                DateFrom = DateTime.Now
            };
        }
        [Fact]
        public void Habdle_Should_ReturnFailureResult_WhenThereIsNoCategory()
        {
            _repository.Setup(
                x => x.GetROEByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null!);

            var handler = new UpdateROECommandHandler(_repository.Object);

            var caughtException = Assert.ThrowsAsync<NotFoundExceptions>(() => handler.Handle(_command, default));
        }

        [Fact]
        public async Task Handle_Should_CallSaveChangesOnRepository()
        {
            //Arrange
            _repository.Setup(
               x => x.GetROEByIdAsync(
                   It.IsAny<Guid>(),
                   It.IsAny<Guid>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(_roe);
            var handler = new UpdateROECommandHandler(_repository.Object);

            //Act
            await handler.Handle(_command, default);

            //Assert
            _repository.Verify(
             x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}
