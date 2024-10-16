using FluentAssertions;
using Moq;
using ProductCatalog.Application.Application.Queries.Currency.GetCurrencyList;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Pagination;

namespace ProductCatalog.UnitTests.Queries.Currency
{
    public class GetCurrencyListQueryHandlerTests : BaseTestHandler<ICurrencyRepository>
    {
        private readonly GetCurrencyListQuery _query;
        public GetCurrencyListQueryHandlerTests() : base() => _query = new() { Page = 1, PageSize = 10 };
        [Fact]
        public async Task Handle_Should_ReturnSuccessResult()
        {
            //Arrange
            _repository.Setup(
                mock => mock.GetAllCurrenciesAsync(
                    It.IsAny<IPagination>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Domain.Currency> {
                    new Domain.Currency(){
                    CurrencyId = Guid.NewGuid(),
                    Name = "test name",
                    Code = "test code"
                },
                new Domain.Currency(){
                    CurrencyId = Guid.NewGuid(),
                    Name = "test name1",
                    Code = "test code1"
                }});

            var handle = new GetCurrencyListQueryHandler(_repository.Object, _mapper);
            //Act
            var result = await handle.Handle(_query, default);
            //Assert
            Assert.NotEmpty(result.data);
            result.isSuccess.Should().BeTrue();
            Assert.IsType<List<CurrencyListDto>>(result.data);
            result.meta.count.Should().Be(2);
        }

        [Fact]
        public async Task Handle_Should_ReturnEmptyresult()
        {
            //Arrange 
            _repository.Setup(
                mock => mock.GetAllCurrenciesAsync(
                    It.IsAny<IPagination>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null!);

            var hadle = new GetCurrencyListQueryHandler(_repository.Object, _mapper);
            //Act
            var result = await hadle.Handle(_query, default);
            //
            Assert.Empty(result.data);
        }
    }
}
