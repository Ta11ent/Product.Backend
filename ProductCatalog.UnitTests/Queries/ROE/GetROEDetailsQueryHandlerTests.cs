using FluentAssertions;
using Moq;
using ProductCatalog.Application.Application.Queries.ROE.GetROEDetails;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.UnitTests.Queries.ROE
{
    public class GetROEDetailsQueryHandlerTests : BaseTestHandler<IROERepository>
    {
        private readonly GetROEDetailsQuery _query;
        public GetROEDetailsQueryHandlerTests() : base() => _query = new() { CurrencyId = Guid.NewGuid(), ROEId = Guid.NewGuid() };
        [Fact]
        public async Task Habdle_Should_ReturnFailureResult_WhenThereIsNoCategory()
        {
            _repository.Setup(
                mock => mock.GetROEByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null!);

            var handler = new GetROEDetailsQueryHandler(_repository.Object, _mapper);

            var caughtException = Assert.ThrowsAsync<NotFoundExceptions>(() => handler.Handle(_query, default));
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult()
        {
            //Arrange
            _repository.Setup(
               mock => mock.GetROEByIdAsync(
                   It.IsAny<Guid>(),
                   It.IsAny<Guid>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(new Domain.ROE()
               {
                   ROEId = _query.ROEId,
                   CurrecnyId = _query.CurrencyId,
                   Rate = 0.023253m,
                   DateFrom = DateTime.Now
               });
            var handler = new GetROEDetailsQueryHandler(_repository.Object, _mapper);
            //Act
            var result = await handler.Handle(_query, default);
            //Assert
            Assert.NotNull(result);
            result.isSuccess.Should().BeTrue();
            Assert.Equal(result.data.ROEId, _query.ROEId);
            Assert.IsType<ROEDetailsDto>(result.data);
        }
    }
}
