using Moq;
using ProductCatalog.Application.Application.Commands.Currency.DeleteCurrency;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.UnitTests.Commands.Currency
{
    public class DeleteCurrrencyCommandHandlerTests : BaseTestHandler<ICurrencyRepository>
    {
        private readonly DeleteCurrencyCommand _command;
        private readonly Domain.Currency _currency;
        public DeleteCurrrencyCommandHandlerTests() : base()
        {
            _currency = new() {
                CurrencyId = Guid.NewGuid(),
                Name = "test name",
                Code = "test code"
            };
            _command = new() { 
                CurrencyId = _currency.CurrencyId
            };
        }
        [Fact]
        public void Habdle_Should_ReturnFailureResult_WhenThereIsNoCategory()
        {
            _repository.Setup(
                x => x.GetCurrencyByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null!);

            var handler = new DeleteCurrencyCommandHandler(_repository.Object);

            var caughtException = Assert.ThrowsAsync<NotFoundExceptions>(() => handler.Handle(_command, default));
        }

        [Fact]
        public async Task Hendle_Should_CallDeleteOnRepository()
        {
            //Arrange
            _repository.Setup(
               x => x.GetCurrencyByIdAsync(
                   It.IsAny<Guid>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(_currency);
            var handler = new DeleteCurrencyCommandHandler(_repository.Object);

            //Act
            await handler.Handle(_command, default);

            //Assert
            _repository.Verify(
                x => x.DeleteCurrency(
                    It.IsAny<Domain.Currency>()),
                Times.Once);

        }

        [Fact]
        public async Task Handle_Should_CallSaveChangesOnRepository()
        {
            //Arrange
            _repository.Setup(
               x => x.GetCurrencyByIdAsync(
                   It.IsAny<Guid>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(_currency);
            var handler = new DeleteCurrencyCommandHandler(_repository.Object);

            //Act
            await handler.Handle(_command, default);

            //Assert
            _repository.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}
