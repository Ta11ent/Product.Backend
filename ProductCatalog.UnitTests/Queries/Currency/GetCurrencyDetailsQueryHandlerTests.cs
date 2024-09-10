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
        public GetCurrencyDetailsQueryHandlerTests() : base() => _query = new() { CurrencyId = Guid.NewGuid() };
        [Fact]
        public async Task Habdle_Should_ReturnFailureResult_WhenThereIsNoCategory()
        {
            _repository.Setup(
                mock => mock.GetCurrencyByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null!);

            var handler = new GetCurrencyDetailsQueryHandler(_repository.Object, _mapper);

            var caughtException = Assert.ThrowsAsync<NotFoundExceptions>(() => handler.Handle(_query, default));
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult()
        {
            //Arrange
            _repository.Setup(
               mock => mock.GetCurrencyByIdAsync(
                   It.IsAny<Guid>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(new Domain.Currency()
               {
                   CurrencyId = _query.CurrencyId,
                   Name = "test name",
                   Code = "test code"
               });
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
