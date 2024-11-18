using Moq;
using ProductCatalog.Application.Application.Commands.Currency.UpdateCurrency;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.UnitTests.Commands.Currency
{
    public class UpdateCurrencyCommandHandlerTests : BaseTestHandler<ICurrencyRepository>
    {
        private readonly UpdateCurrecnyCommand _command;
        private readonly Domain.Currency _currency;
        public UpdateCurrencyCommandHandlerTests() : base()
        {
            _currency = new()
            {
                CurrencyId = Guid.NewGuid(),
                Name = "test name",
                Code = "test code"
            };
            _command = new()
            {
                CurrencyId = _currency.CurrencyId,
                Name = "test name",
                Code = "test code"
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

            var handler = new UpdateCurrecnyCommandHandler(_repository.Object);

            var caughtException = Assert.ThrowsAsync<NotFoundExceptions>(() => handler.Handle(_command, default));
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
            var handler = new UpdateCurrecnyCommandHandler(_repository.Object);

            //Act
            await handler.Handle(_command, default);

            //Assert
            _repository.Verify(
             x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}

