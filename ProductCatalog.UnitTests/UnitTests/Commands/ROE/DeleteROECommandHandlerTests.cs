using Moq;
using ProductCatalog.Application.Application.Commands.ROE.DeleteROE;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.UnitTests.Commands.ROE
{
    public class DeleteROECommandHandlerTests : BaseTestHandler<IROERepository>
    {
        private readonly DeleteROECommand _command;
        public DeleteROECommandHandlerTests() : base() => _command = new() { CurrencyId = Guid.NewGuid(), ROEId = Guid.NewGuid() };
        [Fact]
        public void Habdle_Should_ReturnFailureResult_WhenThereIsNoCategory()
        {
            _repository.Setup(
                x => x.GetROEByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null!);

            var handler = new DeleteROECommandHandler(_repository.Object);

            var caughtException = Assert.ThrowsAsync<NotFoundExceptions>(() => handler.Handle(_command, default));
        }

        [Fact]
        public async Task Hendle_Should_CallDeleteOnRepository()
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
            var handler = new DeleteROECommandHandler(_repository.Object);

            //Act
            await handler.Handle(_command, default);

            //Assert
            _repository.Verify(
                x => x.DeleteROE(
                    It.IsAny<Domain.ROE>()),
                Times.Once);

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
            var handler = new DeleteROECommandHandler(_repository.Object);

            //Act
            await handler.Handle(_command, default);

            //Assert
            _repository.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}
