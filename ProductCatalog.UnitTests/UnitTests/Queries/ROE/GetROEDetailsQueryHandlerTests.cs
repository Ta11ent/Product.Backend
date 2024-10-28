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
        private readonly Domain.ROE _roe;
        public GetROEDetailsQueryHandlerTests() : base()
        {
            _roe = new()
            {
                ROEId = Guid.NewGuid(),
                CurrecnyId = Guid.NewGuid(),
                Rate = 0.023253m,
                DateFrom = DateTime.Now
            };
            _query = new() { 
                CurrencyId = _roe.CurrecnyId, 
                ROEId = _roe.ROEId
            };
        }
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

            var caughtException = 
                await Assert.ThrowsAsync<NotFoundExceptions>(
                    async () => await handler.Handle(_query, default));
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
               .ReturnsAsync(_roe);
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
