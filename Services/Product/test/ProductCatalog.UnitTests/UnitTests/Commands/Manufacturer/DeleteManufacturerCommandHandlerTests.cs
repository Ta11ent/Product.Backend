using Moq;
using ProductCatalog.Application.Application.Commands.Manufacturer.DeleteManufacturer;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.UnitTests.Commands.Manufacturer
{
    public class DeleteManufacturerCommandHandlerTests : BaseTestHandler<IManufacturerRepository>
    {
        private readonly DeleteManufacturerCommand _command;
        private readonly Domain.Manufacturer _manufacturer;
        public DeleteManufacturerCommandHandlerTests() : base()
        {
            _manufacturer = new() {
                ManufacturerId = Guid.NewGuid(),
                Name = "test name",
                Description = "test description"
            };
            _command = new() { 
                ManufacturerId = _manufacturer.ManufacturerId
            };
           
        }
        [Fact]
        public void Habdle_Should_ReturnFailureResult_WhenThereIsNoCategory()
        {
            _repository.Setup(
                x => x.GetManufacturerByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null!);

            var handler = new DeleteManufacturerCommandHandler(_repository.Object);

            var caughtException = Assert.ThrowsAsync<NotFoundExceptions>(() => handler.Handle(_command, default));
        }

        [Fact]
        public async Task Hendle_Should_CallDeleteOnRepository()
        {
            //Arrange
            _repository.Setup(
               x => x.GetManufacturerByIdAsync(
                   It.IsAny<Guid>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(_manufacturer);
            var handler = new DeleteManufacturerCommandHandler(_repository.Object);

            //Act
            await handler.Handle(_command, default);

            //Assert
            _repository.Verify(
                x => x.DeleteManufacturer(
                    It.IsAny<Domain.Manufacturer>()),
                Times.Once);

        }

        [Fact]
        public async Task Handle_Should_CallSaveChangesOnRepository()
        {
            //Arrange
            _repository.Setup(
               x => x.GetManufacturerByIdAsync(
                   It.IsAny<Guid>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(_manufacturer);
            var handler = new DeleteManufacturerCommandHandler(_repository.Object);

            //Act
            await handler.Handle(_command, default);

            //Assert
            _repository.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}
