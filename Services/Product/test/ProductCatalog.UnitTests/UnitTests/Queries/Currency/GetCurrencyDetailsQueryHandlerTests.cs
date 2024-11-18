using FluentAssertions;
using Moq;
using ProductCatalog.Application.Application.Queries.Currency.GetCurreencyDetails;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.UnitTests.Queries.Currency
{
    public class GetCurrencyDetailsQueryHandlerTests : BaseTestHandler<ICurrencyRepository>
    {
        private readonly GetCurrencyDetailsQuery _query;
        private readonly Domain.Currency _currency;
        public GetCurrencyDetailsQueryHandlerTests() : base()
        {
            _currency = new()
            {
                CurrencyId = Guid.NewGuid(),
                Name = "test name",
                Code = "test code"
            };
            _query = new() { 
                CurrencyId = _currency.CurrencyId
            };
        }
        [Fact]
        public async Task Habdle_Should_ReturnFailureResult_WhenThereIsNoCategory()
        {
            _repository.Setup(
                mock => mock.GetCurrencyByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null!);

            var handler = new GetCurrencyDetailsQueryHandler(_repository.Object, _mapper);

            var caughtException = await Assert.ThrowsAsync<NotFoundExceptions>(() => handler.Handle(_query, default));
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult()
        {
            //Arrange
            _repository.Setup(
               mock => mock.GetCurrencyByIdAsync(
                   It.IsAny<Guid>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(_currency);
            var handler = new GetCurrencyDetailsQueryHandler(_repository.Object, _mapper);
            //Act
            var category = await handler.Handle(_query, default);
            //Assert
            Assert.NotNull(category);
            category.isSuccess.Should().BeTrue();
            Assert.Equal(category.data.CurrencyId, _query.CurrencyId);
            Assert.IsType<CurrencyDetailsDto>(category.data);
        }
    }
}
