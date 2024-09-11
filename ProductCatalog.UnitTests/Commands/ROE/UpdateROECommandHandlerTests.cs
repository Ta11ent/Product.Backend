using Moq;
using ProductCatalog.Application.Application.Commands.ROE.UpdateROE;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.UnitTests.Commands.ROE
{
    public class UpdateROECommandHandlerTests : BaseTestHandler<IROERepository>
    {
        private readonly UpdateROECommand _command;
        public UpdateROECommandHandlerTests() : base() => _command = new()
        {
            CurrencyId = Guid.NewGuid(),
            ROEId = Guid.NewGuid(),
            Rate = 0.2321m,
            DateFrom = DateTime.Now
        };
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
               .ReturnsAsync(new Domain.ROE()
               {
                   ROEId = Guid.NewGuid(),
                   CurrecnyId = _command.CurrencyId,
                   Rate = 0.023253m,
                   DateFrom = DateTime.Now
               });
            var handler = new UpdateROECommandHandler(_repository.Object);

            //Act
            await handler.Handle(_command, default);

            //Assert
            _repository.Verify(
             x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}
