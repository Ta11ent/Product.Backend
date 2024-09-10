using FluentAssertions;
using Moq;
using ProductCatalog.Application.Application.Commands.Currency.CreateCurrency;
using ProductCatalog.Application.Common.Abstractions;

namespace ProductCatalog.UnitTests.Commands.Currency
{
    public class CreateCurrencyCommandHandlerTests : BaseTestHandler<ICurrencyRepository>
    {
        private readonly CreateCurrencyCommand _command;
        public CreateCurrencyCommandHandlerTests() : base() => _command = new() { Code = "USD", Name = "United States Dollar" };
        public async Task Handle_Should_CallAddOnRepository()
        {
            //Arrange
            var handler = new CreateCurrencyCommandHandler(_repository.Object);
            //Act
            var result = await handler.Handle(_command, default);
            //Assert
            _repository.Verify(
                mock => mock.CreateCurrencyAsync(
                    It.IsAny<Domain.Currency>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult()
        {
            //Arrange
            var handler = new CreateCurrencyCommandHandler(_repository.Object);
            //Act
            var result = await handler.Handle(_command, default);
            //Assert
            result.Should().NotBeEmpty();
            Assert.IsType<Guid>(result);
        }

        [Fact]
        public async Task Handle_Should_CallSaveChanges()
        {
            //Arrange
            var handler = new CreateCurrencyCommandHandler(_repository.Object);
            //Act
            var result = await handler.Handle(_command, default);
            //Assert
            _repository.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}
