using FluentAssertions;
using Moq;
using ProductCatalog.Application.Application.Queries.Manufacturer.GetManufacturerDetails;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.UnitTests.Queries.Manufacturer
{
    public class GetManufacturerDetailsQueryHandlerTests : BaseTestHandler<IManufacturerRepository>
    {
        private readonly GetManufacturerDetailsQuery _query;
        public GetManufacturerDetailsQueryHandlerTests() : base() => _query = new() { ManufacturerId = Guid.NewGuid() };
        [Fact]
        public async Task Habdle_Should_ReturnFailureResult_WhenThereIsNoCategory()
        {
            _repository.Setup(
                mock => mock.GetManufacturerByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null!);

            var handler = new GetManufacturerDetailsQueryHandler(_repository.Object, _mapper);

            var caughtException = Assert.ThrowsAsync<NotFoundExceptions>(() => handler.Handle(_query, default));
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult()
        {
            //Arrange
            _repository.Setup(
               mock => mock.GetManufacturerByIdAsync(
                   It.IsAny<Guid>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(new Domain.Manufacturer()
               {
                   ManufacturerId = _query.ManufacturerId,
                   Description = "test description",
                   Name = "test name"
               });
            var handler = new GetManufacturerDetailsQueryHandler(_repository.Object, _mapper);
            //Act
            var result = await handler.Handle(_query, default);
            //Assert
            Assert.NotNull(result);
            result.isSuccess.Should().BeTrue();
            Assert.Equal(result.data.ManufacturerId, _query.ManufacturerId);
            Assert.IsType<ManufacturerDetailsDto>(result.data);
        }
    }
}
