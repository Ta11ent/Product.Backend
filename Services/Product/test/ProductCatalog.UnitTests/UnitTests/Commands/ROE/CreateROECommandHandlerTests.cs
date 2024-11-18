using FluentAssertions;
using Moq;
using ProductCatalog.Application.Application.Commands.ROE.CreateROE;
using ProductCatalog.Application.Common.Abstractions;

namespace ProductCatalog.UnitTests.Commands.ROE
{
    public class CreateROECommandHandlerTests : BaseTestHandler<IROERepository>
    {
        private readonly CreateROECommand _command;
        public CreateROECommandHandlerTests() : base() => _command = new()
        {
            CurrecnyId = Guid.NewGuid(),
            Rate = 0.22325m,
            DateFrom = DateTime.Now,
        };

        [Fact]
        public async Task Handle_Should_CallAddOnRepository()
        {
            //Arrange
            var handler = new CreateROECommandHandler(_repository.Object);
            //Act
            var result = await handler.Handle(_command, default);
            //Assert
            _repository.Verify(
                x => x.CreateROEAsync(
                    It.IsAny<Domain.ROE>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult()
        {
            //Arrange
            var handler = new CreateROECommandHandler(_repository.Object);
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
            var handler = new CreateROECommandHandler(_repository.Object);
            //Act
            var result = await handler.Handle(_command, default);
            //Assert
            _repository.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}
