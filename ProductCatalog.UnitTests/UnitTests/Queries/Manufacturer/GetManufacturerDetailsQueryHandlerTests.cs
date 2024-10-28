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
        private readonly Domain.Manufacturer _manufacturer;
        public GetManufacturerDetailsQueryHandlerTests() : base()
        {
            _manufacturer = new()
            {
                ManufacturerId = Guid.NewGuid(),
                Description = "test description",
                Name = "test name"
            };
            _query = new() { 
                ManufacturerId = _manufacturer.ManufacturerId
            };
        }
        [Fact]
        public async Task Habdle_Should_ReturnFailureResult_WhenThereIsNoCategory()
        {
            _repository.Setup(
                mock => mock.GetManufacturerByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null!);

            var handler = new GetManufacturerDetailsQueryHandler(_repository.Object, _mapper);

            var caughtException = await Assert.ThrowsAsync<NotFoundExceptions>(async () => await handler.Handle(_query, default));
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult()
        {
            //Arrange
            _repository.Setup(
               mock => mock.GetManufacturerByIdAsync(
                   It.IsAny<Guid>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(_manufacturer);
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
