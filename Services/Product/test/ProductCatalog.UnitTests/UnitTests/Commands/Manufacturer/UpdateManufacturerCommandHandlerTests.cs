using Moq;
using ProductCatalog.Application.Application.Commands.Manufacturer.UpdateManufacturer;
using ProductCatalog.Application.Common.Abstractions;
using ProductCatalog.Application.Common.Exceptions;

namespace ProductCatalog.UnitTests.Commands.Manufacturer
{
    public class UpdateManufacturerCommandHandlerTests : BaseTestHandler<IManufacturerRepository>
    {
        private readonly UpdateManufacturerCommand _command;
        public UpdateManufacturerCommandHandlerTests() : base() => _command = new()
        {
            ManufacturerId = Guid.NewGuid(),
            Description = "test description",
            Name = "test name"
        };
        [Fact]
        public void Habdle_Should_ReturnFailureResult_WhenThereIsNoCategory()
        {
            _repository.Setup(
                x => x.GetManufacturerByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null!);

            var handler = new UpdateManufacturerCommandHandler(_repository.Object);

            var caughtException = Assert.ThrowsAsync<NotFoundExceptions>(() => handler.Handle(_command, default));
        }

        [Fact]
        public async Task Handle_Should_CallSaveChangesOnRepository()
        {
            //Arrange
            _repository.Setup(
               x => x.GetManufacturerByIdAsync(
                   It.IsAny<Guid>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(new Domain.Manufacturer()
               {
                   ManufacturerId = _command.ManufacturerId,
                   Description = _command.Description,
                   Name = _command.Name
               });
            var handler = new UpdateManufacturerCommandHandler(_repository.Object);

            //Act
            await handler.Handle(_command, default);

            //Assert
            _repository.Verify(
             x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}
