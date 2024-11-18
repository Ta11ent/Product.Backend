using FluentAssertions;
using Moq;
using ProductCatalog.Application.Application.Commands.Manufacturer.CreateManufacturer;
using ProductCatalog.Application.Common.Abstractions;

namespace ProductCatalog.UnitTests.Commands.Manufacturer
{
    public class CreateManufacturerCommandHandlerTests : BaseTestHandler<IManufacturerRepository>
    {
        private readonly CreateManufacturerCommand _command;
        public CreateManufacturerCommandHandlerTests() : base() => _command = new()
        {
            Description = "test description",
            Name = "test name"
        };
        [Fact]
        public async Task Handle_Should_CallAddOnRepository()
        {
            //Arrange
            var handler = new CreateManufacturerCommandHandler(_repository.Object);
            //Act
            var result = await handler.Handle(_command, default);
            //Assert
            _repository.Verify(
                x => x.CreateManufacturerAsync(
                    It.IsAny<Domain.Manufacturer>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult()
        {
            //Arrange
            var handler = new CreateManufacturerCommandHandler(_repository.Object);
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
            var handler = new CreateManufacturerCommandHandler(_repository.Object);
            //Act
            var result = await handler.Handle(_command, default);
            //Assert
            _repository.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}
